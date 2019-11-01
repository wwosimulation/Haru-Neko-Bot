const client = require("nekos.life");
const nekoslife = new client();
const func = require("../../function.js");
const Discord = require("discord.js");

module.exports = {
    name: "foxgirl",
    category: "Anime",
    description: "Show Fox Girl Image.",
    run: async(_client, message, args) => {
        const fox = await nekoslife.sfw.foxGirl();

        const embed = new Discord.RichEmbed()
        .setTitle("**Fox （╹◡╹）♡**")
        .setImage(fox.url)
        .setTimestamp()
        .setFooter("Powered by nekos.life")
        .setColor("#FF33FF");
        await message.channel.send(embed)
    }
}