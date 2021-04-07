const translate = require("@vitalets/google-translate-api");
const Discord = require("discord.js");
const func = require("../../function.js");
const db = require("../../db.js");
module.exports = {
    name: "translate",
    category: "Help",
    description: "Translate any languages to English.",
    run: async(_client, message, args) => {

        if(db.GuildInfo(message.guild.id, "perm") != "Premium") return func.noPremium(message);
        var prefix = await db.GuildInfo(message.guild.id, "prefix");
        if(!args[0]){
            const embed = new Discord.MessageEmbed()
            .setAuthor("Command Help.")
            .setDescription("<> - Required | [] - Optional")
            .addField(""+prefix+"translate <Message>", "**Guides:**\n- Message: Write your Messages to Translate.\n**Note:** Input and Output Messages can't be longer than 1024 words.")
            .setColor("#00FFFF");
            return message.channel.send(embed);
        }

        translate(args.join(" "), {to: "en"}).then(res => {
            var thelength = `${res.text}`;
            var inputlength = `${args.join(" ")}`;
            if(thelength.length >= 1024) return func.Error(message, "Message to Translate too Long.");
            if(inputlength.length >= 1024) return func.Error(message, "Message to Translate too Long.");
            
            const embed = new Discord.MessageEmbed()
            .setAuthor("Translation.")
            .setDescription(`Requested by ${message.author}`)
            .addField(`From ${translate.languages[res.from.language.iso]}`, `${args.join(" ")}`)
            .addField(`To English`, `${res.text}`)
            .setColor(func.randomHex(6));

            return message.channel.send(embed).catch(()=>{});
        }).catch(err=>{func.Error(message, err)})
    }
}