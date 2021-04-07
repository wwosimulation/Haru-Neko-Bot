const Discord = require("discord.js");
const db = require("../../db.js");

module.exports = {
    name: "guildstatus",
    category: "Guild",
    description: "Show Status of Guild",
    run: async(_client, message, args) => {
        var bg;
        if (db.GuildBackground(message.guild.id, "bg") != 0) { bg = `[Here](${db.GuildBackground(message.guild.id, "bg")})`}
        else bg = "Nothing";
        const embed = new Discord.MessageEmbed()
        .setAuthor(`${message.guild.name}'s Guild Status`, message.guild.iconURL)
        .setDescription(`Premium Status: ${db.GuildInfo(message.guild.id, "perm")}\nTime Left: ${db.GuildInfo(message.guild.id, "permtimeleft")}\nGuild Money: ${db.GuildInfo(message.guild.id, "money")}\nGuild Background: ${bg}\nGB-Color: ${db.GuildBackground(message.guild.id, "color")}`)
        .setTimestamp()
        .setFooter(`Requested by ${message.author.tag}`, message.author.avatarURL)
        .setColor("#FF33FF");
        message.channel.send(embed);
    }
}