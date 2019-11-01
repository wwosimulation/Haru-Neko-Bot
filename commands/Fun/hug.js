const client = require("nekos.life");
const nekoslife = new client();
const func = require("../../function.js");
const Discord = require("discord.js");

module.exports = {
    name: "hug",
    category: "Fun",
    description: "Returns Hug Gif someone to someone.",
    run: async(_client, message, args) => {
        if(!args[0]) return func.Error(message, "You must choose User to Hug.");
        const getuser = message.guild.member( message.mentions.users.first() || message.guild.members.get(args[0]) || message.guild.members.find("displayName", args.join(" ")) );
        if(!getuser) return func.Error(message, "User not Found.");
        
        const hug = await nekoslife.sfw.hug();
        const embed = new Discord.RichEmbed()
        .setDescription(`Hey ${getuser.toString()}, ${message.author.toString()} Hugged you!`)
        .setImage(hug.url)
        .setTimestamp()
        .setFooter("Powered by nekos.life")
        .setColor("#FF33FF");
        await message.channel.send(embed)
    }
}