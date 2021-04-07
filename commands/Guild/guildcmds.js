const Discord = require("discord.js");
const db = require("../../db.js");

module.exports = {
    name: "guildcmds",
    aliases: ["guildhelp"],
    category: "Guild",
    description: "Get Guild Help Command.",
    run: async(_client, message, args) => {
        const prefix = await db.GuildInfo(message.guild.id, "prefix");
        const embed = new Discord.MessageEmbed()
        .setAuthor("Guild Commands.")
        .addField(`${prefix}guildstatus`, "__Get Guild Status (Guild Premium, Premium Timeleft, Guild Money...)__.")
        .addField(`${prefix}guildsettings`, "__Settings this guild from Commands or Function available__.\n`Requirement: ManageServer permission.`")
        .addField(`${prefix}guildbackgrounds`, "__Show List Backgrounds to use `-guildsettings serverinfo`__.\n`Requirement: Guild Premium.`")
        .setColor("#FF33FF")
        .setTimestamp()
        .setFooter(`Requested by ${message.author.tag}`, message.author.avatarURL);

        message.channel.send(embed).catch( err => {
            message.send(`Hey, I think you missing me about Send Message Permission, before use this Command again, give me permission to send message to <#${message.channel.id}>`).catch(err =>{
                return;
            });
        });
    }
}