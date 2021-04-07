const Discord = require("discord.js");
const db = require("../../db.js");
const func = require("../../function.js");

module.exports = {
    name: "setprefix",
    aliases: ["prefix"],
    category: "Guild",
    description: "Set Prefix for Guild.",
    run: async(_client, message, args) => {
        const prefix = db.GuildInfo(message.guild.id, "prefix");
        if (!message.guild.members.cache.get(message.author.id).hasPermission("MANAGE_GUILD")) return func.noPerm(message, "Manage Server");
        const embed = new Discord.MessageEmbed()
        .addField("Prefix.", "My Prefix of this Server is `"+prefix+"`")
        .addField("Command Help.", "`"+prefix+"setprefix <New Prefix>` to set prefix to this Server.\n`"+prefix+"setprefix reset` to set default prefix to `-`")
        .setColor("#00FF00");
        if (!args[0]) return message.channel.send(embed);
        if (args[0].toLowerCase() == "reset") {
            const embed = new Discord.MessageEmbed()
            .addField("Prefix Change Successfully.", "New Prefix of this Server is `-`")
            .setColor("#00FFFF");
            message.channel.send(embed);

            db.GuildInfo(message.guild.id, "prefix", "-");
        }
        else {
            const embed = new Discord.MessageEmbed()
            .addField("Prefix Change Successfully.", "New Prefix of this Server is `"+args.join(" ")+"`")
            .setColor("#00FFFF");
            message.channel.send(embed);
            
            db.GuildInfo(message.guild.id, "prefix", args.join(" "));
        }  
    }
}