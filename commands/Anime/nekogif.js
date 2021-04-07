const client = require("nekos.life");
const nekoslife = new client();
const func = require("../../function.js");
const Discord = require("discord.js");

module.exports = {
    name: "nekogif",
    category: "Anime",
    description: "Show Neko Gif.",
    run: async(_client, message, args) => {
        const neko = await nekoslife.sfw.nekoGif();

        const embed = new Discord.MessageEmbed()
        .setTitle("**Neko ╰(´︶`)╯♡**")
        .setImage(neko.url)
        .setTimestamp()
        .setFooter("Powered by nekos.life")
        .setColor("#FF33FF");
        await message.channel.send(embed)
    }
}