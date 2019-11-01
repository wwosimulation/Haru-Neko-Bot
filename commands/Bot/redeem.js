const func = require("../../function.js");
const db = require("../../db.js");
const Discord = require("discord.js");

module.exports = {
    name: "redeem",
    category: "Bot",
    description: "Redeem Code.",
    run: async(_client, message, args) => {
        const prefix = db.GuildInfo(message.guild.id, "prefix");
        if(!args[0]){
            const embed = new Discord.RichEmbed()
            .setAuthor("Command Help.")
            .setDescription("<> - Required | [] - Optional")
            .addField(""+prefix+"redeem <Code>", "\n**Guides:**\n- Code: Special Code that you got from Neko.")
            .setColor("#00FFFF");
            return message.channel.send(embed);
        }
        else {
            var code = args.join(" ");
            var codes = await db.RedeemCode(code, "redeem");
            if (codes == "Error1") return func.Error(message, "Code `"+code+"` is Not Available or Had Been Redeemed.");
            const embed = new Discord.RichEmbed();

            var names = await db.RedeemCode(code, "redeem", "1");
            var values = await db.RedeemCode(code, "redeem", "2");
            var msgs = await db.RedeemCode(code, "redeem", "3");

            var name = `${names}`;
            var value = `${values}`;
            var msg = `${msgs}`;

            if (name.toLowerCase() == "emoji"){

                var checkemotess = false;
                var getuseremojis = await db.User(message.author.id, "emojiown");
                var getuseremoji = `${getuseremojis}`;
                if (getuseremoji == "Full Access") {checkemotess = true;}
                else {
                    var useremojis = getuseremoji.split(" ");
                    useremojis.forEach(e => {
                        if(e == value) {checkcheckemotess = true; useremojis = "";}
                    });
                }
                
                if(checkemotess == true) return func.Error(message, "You already owned that Emoji so can't Redeem.")

                if (value.toLowerCase() == "full access"){

                    await db.User(message.author.id, "emojiown", "Full Access");
                    await db.RedeemCode(code, "redeem", "delete");
                    await db.Save("User");

                    embed.setAuthor("Redeem Successfully.")
                    embed.setDescription("**Code:** `"+code+"`\n**Name:** "+name+"\n**Value:** "+value+"")
                    embed.setColor("#00FF00");
                    embed.setTimestamp();
                    embed.setFooter(`Redeemed by ${message.author.tag}`);

                    return message.channel.send(embed);
                }
                else {
                    var checkemojiuser = await db.User(message.author.id, "emojiown");
                    if (checkemojiuser == 0) await db.User(message.author.id, "emojiown", `${value}`);
                    else await db.User(message.author.id, "emojiown", `${checkemojiuser} ${value}`);
                    await db.RedeemCode(code, "redeem", "delete");
                    await db.Save("User");

                    embed.setAuthor("Redeem Successfully.")
                    embed.setDescription("**Code:** `"+code+"`\n**Name:** "+name+"\n**Value:** ID "+value+"")
                    embed.setColor("#00FF00");
                    embed.setTimestamp();
                    embed.setFooter(`Redeemed by ${message.author.tag}`);

                    return message.channel.send(embed);
                }
            }
            else {
                embed.setAuthor("Redeem Successfully.")
                embed.setDescription("**Code:** `"+code+"`\n**Name:** "+name+"\n**Message:** "+value+"")
                embed.setColor("#00FF00");
                embed.setTimestamp();
                embed.setFooter(`Redeemed by ${message.author.tag}`);

                await db.RedeemCode(code, "redeem", "delete");

                return message.channel.send(embed);
            }
        }
    }
}