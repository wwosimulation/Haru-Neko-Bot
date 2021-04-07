const Discord = require("discord.js");
const func = require("../../function.js");
const db = require("../../db.js");
const emote = require("../../Another/emoji.js");
module.exports = {
    name: "emojilist",
    aliases: ["emotelist", "elist"],
    category: "Help",
    description: "Show List of Emotes.",
    run: async(_client, message, args) => {
        const prefix = db.GuildInfo(message.guild.id, "prefix");
        if(!args[0]){
            const embed = new Discord.MessageEmbed()
            .setAuthor("Command Help.")
            .setDescription("<> - Required | [] - Optional")
            .addField(""+prefix+"emojilist <Image/Gif> <Page>", "**Note:** Only can use when you own that Emoji.")
            .setColor("#00FFFF");
            return message.channel.send(embed);
        }
        if (args[0].toLowerCase() == "image"){
            const embed = new Discord.MessageEmbed();
            if (!args[1]) return func.Error(message, "Page is Missing.");
            var hasparse = parseInt(args[1])
            if(typeof hasparse != "number") return func.Error(message, "Page must be a Number.");
            var end = Math.floor(hasparse * 20);
            if (end >= 1001) return func.Error(message, "This Page is Not Available.");
            if (end <= 0) return func.Error(message, "This Page is Not Available.");
            var start = Math.floor(end - 19);
            var msg = null;
            while(start <= end){
                var getemote = await emote.Emotes(`${start}`);
                if (getemote == "Error") {start = end;}
                else {
                    if (msg == null) msg = `${start} - ${getemote}`;
                    else msg = `${msg}\n${start} - ${getemote}`;
                }
                start++;
            }
            if (msg == null) return func.Error(message, "This Page is Not Available.");
            
            embed.setAuthor("Emotes List - ID.");
            embed.setDescription(msg);
            embed.setColor("#FF33FF");
            embed.setTimestamp();
            embed.setFooter(`Requested by ${message.author.tag} | Emoji Image Page ${args[1]}`, message.author.avatarURL);
            return message.channel.send(embed);
        }
        else if (args[0].toLowerCase() == "gif"){
            const embed = new Discord.MessageEmbed();
            if (!args[1]) return func.Error(message, "Page is Missing.");
            var hasparse = parseInt(args[1])
            if(typeof hasparse != "number") return func.Error(message, "Page must be a Number.");
            var end = Math.floor((hasparse * 20) + 1000);
            if (end <= 1000) return func.Error(message, "This Page is Not Available.");
            var start = Math.floor(end - 19);
            var msg = null;
            while(start <= end){
                var getemote = await emote.Emotes(`${start}`);
                if (getemote == "Error") {start = end;}
                else {
                    if (msg == null) msg = `${start} - ${getemote}`;
                    else msg = `${msg}\n${start} - ${getemote}`;
                }
                start++;
            }
            if (msg == null) return func.Error(message, "This Page is Not Available.");
            
            embed.setAuthor("Emotes List - ID.");
            embed.setDescription("Emotes on Above.");
            embed.setColor("#FF33FF");
            embed.setTimestamp();
            embed.setFooter(`Requested by ${message.author.tag} | Emoji Gif Page ${args[1]}`, message.author.avatarURL);
            return message.channel.send(`${msg}`, embed);
        }
        else return func.Error(message, "Only accept `Image` or `Gif`");
    }
}