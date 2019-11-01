const Discord = require("discord.js");
const db = require("../../db.js");
const bg = require("../../CommandFunction/guildbackgrounds.js");
const func = require("../../function.js");

module.exports = {
    name: "guildbackgrounds",
    aliases: ["listbackgrounds", "backgroundslist"],
    category: "Guild",
    description: "Show Backrounds ID List.",
    run: async(_client, message, args) => {
        const checkpremium = db.GuildInfo(message.guild.id, "perm");
        if (checkpremium != "Premium") return func.noPremium(message);
        
        var BGList = null;
        var BGList2 = null;
        var count = 1;

        while(await bg.Backgrounds(message, count, "list") != count){
            if (count <= 5){
                if (BGList == null) BGList = `${count} - [Link Background](${await bg.Backgrounds(message, count, "list")})`;
                else BGList = `${BGList}\n${count} - [Link Background](${await bg.Backgrounds(message, count, "list")})`;
                count++;
            }
            else if (count <= 10){
                if (BGList2 == null) BGList2 = `${count} - [Link Background](${await bg.Backgrounds(message, count, "list")})`;
                else BGList2 = `${BGList2}\n${count} - [Link Background](${await bg.Backgrounds(message, count, "list")})`;
                count++;
            }
        }

        const embed = new Discord.RichEmbed()
        .setAuthor("Guild Backgrounds List - ID.")
        .addField(`ID 1-5`, `${BGList}`)
        .addField(`ID 6-10`, `${BGList2}`)
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