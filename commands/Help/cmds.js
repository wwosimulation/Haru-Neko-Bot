const Discord = require("discord.js");
const db = require("../../db.js");
module.exports = {
    name: "cmds",
    aliases: ["help", "commands"],
    category: "Help",
    description: "Returns Help Commands List.",
    run: async(_client, message, args) => {
        const prefix = await db.GuildInfo(message.guild.id, "prefix");
        const embed = new Discord.MessageEmbed();
        var a0 = args[0];
        if(!a0 || a0){
            embed
            .setAuthor("Haru Neko's Commands")
            .setDescription("$ - Permissions Requirement.")
            .addField("Guild Commands.", "("+prefix+") `guildsettings ($), setprefix ($), serverinfo`")
            .addField("Anime Commands.", "("+prefix+") `neko, nekogif, foxgirl`")
            .addField("Fun Commands.", "("+prefix+") `kiss, hug, nya, slap`")
            .addField("Help Commands.", "("+prefix+") `cmds, variablereferece, translate ($), emojilist`")
            .addField("Search Commands.", "("+prefix+") `azurlane`")
            .addField("Haru Neko Commands.", "("+prefix+") `ping, botinfo, invite, redeem, e ($)`")
            .addField("More?", "Come to channel #feedback in Server by command to get invite `-invite`")
            .setColor("#FF33FF");
        }
        return message.channel.send(embed);
    }
}