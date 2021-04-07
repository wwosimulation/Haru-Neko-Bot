const func = require("../../function.js");
const emote = require("../../Another/emoji.js");
const db = require("../../db.js");
const Discord = require("discord.js");

module.exports = {
    name: "e",
    category: "Fun",
    description: "Return Emoji as Message.",
    run: async(_client, message, args) => {
        
        if(!args[0]){
            const embed = new Discord.MessageEmbed()
            .setAuthor("Command Help.")
            .setDescription("<> - Required | [] - Optional")
            .addField(""+prefix+"e <Emoji ID>", "\n**Guides:**\n- Emoji ID: ID of Emote\n`(ID in Command -emojilist)`\n**Note:** Only can use when you own that Emoji.")
            .setColor("#00FFFF");
            return message.channel.send(embed);
        }

        var emotes = await emote.Emotes(`${args[0]}`);

        if(emotes == "Error") return func.Error(message, "Emoji ID `"+args[0]+"` is Not Available.");

        var check = false;
        var getuseremoji = await db.User(message.author.id, "emojiown");
        if (getuseremoji == "Full Access") {check = true;}
        else if (getuseremoji == 0 ) {check = false;}
        else {
            var useremoji = getuseremoji.split(" ");
            useremoji.forEach(e => {
                if(e == args[0]) {check = true; useremoji = "";}
            });
        }

        if (check == false) return func.Error(message, "You are not own Emoji ID `"+args[0]+"`");

        return message.channel.send(emotes);
    }
}