const Discord = require("discord.js");
const db = require("../../db.js");
const bg = require("../../CommandFunction/guildbackgrounds.js");
const welcome = require("../../CommandFunction/guildwelcome.js");
const func = require("../../function.js");

module.exports = {
    name: "guildsettings",
    aliases: ["guildsetting"],
    category: "Guild",
    description: "Settings to set Guild.",
    run: async(_client, message, args) => {
        if (!message.guild.member(message.author).hasPermission("MANAGE_GUILD")) return func.noPerm(message, "Manage Server");
        const prefix = db.GuildInfo(message.guild.id, "prefix");
        if (!args[0]){
            const embed = new Discord.MessageEmbed()
            .setAuthor("Guild Settings.")
            .addField(`${prefix}guildsettings serverinfo`, "Change show Embed or Image of Command `-serverinfo`.\n`Requirement: Guild Premium.` (`-guildstatus`).")
            .addField(`${prefix}guildsettings welcome`, "Welcome new User by Function `Welcome Message`.")
            .addField(`${prefix}guildsettings dmwelcome`, "DM user when Join by Function `Welcome Message`.")
            .setColor("#FF33FF")
            .setTimestamp()
            .setFooter(`Requested by ${message.author.tag}`, message.author.avatarURL);
            await message.channel.send(embed);
        }
        else {
            const checkpremium = db.GuildInfo(message.guild.id, "perm");

            if (args[0].toLowerCase() == "serverinfo"){
                if (checkpremium != "Premium") return func.noPremium(message);
                if (!args[1]) {
                    const embed = new Discord.MessageEmbed()
                    .setAuthor("Command Help.")
                    .setDescription("<> - Required | [] - Optional")
                    .addField(""+prefix+"guildsettings "+args[0]+" <ID/Link> [Color]", "**Commands Support:**\n`"+prefix+"guildsettings "+args[0]+" review <ID/Link> [Color]`\n`"+prefix+"guildsettings "+args[0]+" default` (Set to 0 - Embed)\n**Guides:**\n- Link: must be large 1920x1080\n- Color: <White/Black> - Color of Words")
                    .setColor("#00FFFF");
                    message.channel.send(embed);
                    return;
                }
                
                if (args[1].toLowerCase() == "review"){
                    const getbg = await bg.Backgrounds(message, args[2], "yes", args[3]);
                    if (getbg == "Error") {
                        const embed = new Discord.MessageEmbed()
                        .addField("Error.", "ID / Links is not available.")
                        .setColor("#FF0000");
                        await message.channel.send(embed);
                    }
                    else if (getbg == "Error2") {
                        const embed = new Discord.MessageEmbed()
                        .addField("Error.", "Color not Found.")
                        .setColor("#FF0000");
                        await message.channel.send(embed);
                    }
                    else{
                        var m = await message.channel.send(`After change, your background look like this:\n(Please wait for Uploading...)`);
                        await bg.Backgrounds(message, args[2], "review", args[3]);
                        //await bg.Backgrounds(message, args.slice(2).join(" "));
                        await m.edit("After change, your background look like this:");
                    }
                }
                else {
                    if (args[1] == "0" || args[1].toLowerCase() == "default") {
                        const embed = new Discord.MessageEmbed()
                        .addField("Background Change Successfully.", "Changed Background to default.")
                        .setColor("#00FF00");
                        message.channel.send(embed);
                        db.GuildBackground(message.guild.id, "bg", "0");
                        db.GuildBackground(message.guild.id, "color", "0");
                        db.Save("GuildBackground");
                    }
                    else {
                        const getbg = await bg.Backgrounds(message, args[1], "yes", args[2]);
                        if (getbg == "Error") {
                            const embed = new Discord.MessageEmbed()
                            .addField("Error.", "ID / Links is not available.")
                            .setColor("#FF0000");
                            await message.channel.send(embed);
                        }
                        else if (getbg == "Error2") {
                            const embed = new Discord.MessageEmbed()
                            .addField("Error.", "Color not Found.")
                            .setColor("#FF0000");
                            await message.channel.send(embed);
                        } 
                        else{
                            const info = i => { if (i.length >= 5) return "Link";
                                else return "ID"; }
                            const in4 = i => { if (args[1].length >= 5) return "[Click Here]";
                                else return `[${args[1]}]`; }
                            const cl = c => { if(!c) return "Black";
                                else return c; }
                            const embed = new Discord.MessageEmbed()
                            .addField("Background Change Successfully.", "Changed Background to "+info(args[1])+": "+in4()+"("+await bg.Backgrounds(message, args[1], "list")+")\nWords Color: "+cl(args[2])+".")
                            .setColor("#00FF00");
                            message.channel.send(embed);
                            await db.GuildBackground(message.guild.id, "bg", await bg.Backgrounds(message, args[1], "list"));
                            await db.GuildBackground(message.guild.id, "color", cl(args[2]));
                            await db.Save("GuildBackground");
                        }
                    }
                }
            }

            else if (args[0].toLowerCase() == "welcome"){
                const type = args[2];
                const msg = args[3];
                if (!args[1]) {
                    const embed = new Discord.MessageEmbed()
                    .setAuthor("Command Help.")
                    .setDescription("<> - Required | [] - Optional")
                    .addField(""+prefix+"guildsettings "+args[0]+" <Channel> <Type> <Message>", "**Commands Support:**\n`"+prefix+"guildsettings "+args[0]+" review <Type> <Message>`\n`"+prefix+"guildsettings "+args[0]+" off` (Turn Off this Feature)\n`"+prefix+"guildsettings "+args[0]+" show` (Show current Welcome settings of Server)\n**Guides:**\n- Channel: ID or Mention Channel to set place of Message\n- Type: <Message/Embed/Image>\n- Message: Write Welcome Message.\n`Variable Reference by Command -variablereference`")
                    .setColor("#00FFFF");
                    message.channel.send(embed);
                    return;
                }


                if (args[1].toLowerCase() == "show"){
                    const check2 = await db.GuildWelcome(message.guild.id, "type");
                    const msg = check2 == 0 ? "Nothing Yet." : "Type: "+await db.GuildWelcome(message.guild.id, "type")+"\nMessage to <#"+await db.GuildWelcome(message.guild.id, "channel")+">\nMessage:\n`"+db.GuildWelcome(message.guild.id, "msg")+"`";
                    const embed = new Discord.MessageEmbed()
                    .setAuthor(`${message.guild.name}'s Current Welcome Message Settings`, message.guild.iconURL)
                    .setDescription(msg)
                    .setTimestamp()
                    .setFooter(`Requested by ${message.author.tag}`, message.author.avatarURL)
                    .setColor("#FF33FF");
                    message.channel.send(embed);
                }
                else if (args[1].toLowerCase() == "off"){
                    const embed = new Discord.MessageEmbed()
                    .addField("Welcome Message Change Successfully.", "Welcome Message has been turned Off")
                    .setColor("#00FF00");
                    message.channel.send(embed);
                    await db.GuildWelcome(message.guild.id, "type", "0");
                    await db.GuildWelcome(message.guild.id, "channel", "0");
                    await db.GuildWelcome(message.guild.id, "msg", "0");
                    await db.Save("GuildWelcome");
                }
                else if (args[1].toLowerCase() == "review"){
                    if(!type){
                        const embed = new Discord.MessageEmbed()
                        .addField("Error.", "Type is Missing.")
                        .setColor("#FF0000");
                        await message.channel.send(embed);
                    }
                    else if (type.toLowerCase() != "message" & type.toLowerCase() != "embed"){
                        const embed = new Discord.MessageEmbed()
                        .addField("Error.", "Only accept `message` and `embed`")
                        .setColor("#FF0000");
                        await message.channel.send(embed);
                    }
                    else if (!msg){
                        const embed = new Discord.MessageEmbed()
                        .addField("Error.", "Message is Missing.")
                        .setColor("#FF0000");
                        await message.channel.send(embed);
                    }
                    else if (await welcome.Welcome(_client.guilds.cache.get(message.guild.id).member(message.author), null, type.toLowerCase() == "message" ? "message" : "embed", "length", args.slice(3).join(" ")) == "Error2"){
                        const embed = new Discord.MessageEmbed()
                        .addField("Error.", "Message too long (Your Message longer than "+type.toLowerCase() == "message" ? "2048" : "1024"+" words).")
                        .setColor("#FF0000");
                        await message.channel.send(embed);
                    }
                    else {
                        await welcome.Welcome(_client.guilds.cache.get(message.guild.id).member(message.author), message.channel.id, type, "review", args.slice(3).join(" "), _client, message);
                    }
                }
                else {
                    const channel = args[1];
                    const type = args[2];
                    const msg = args[3];
                    if(!channel){
                        const embed = new Discord.MessageEmbed()
                        .addField("Error.", "Channel is Missing.")
                        .setColor("#FF0000");
                        await message.channel.send(embed);
                        return;
                    }
                    var clear = new RegExp('<#(.*)>');
                    var clearspecialchannel = channel.match(clear);
                    if(clearspecialchannel){
                        clearspecialchannel = clearspecialchannel[1];
                    }
                    else clearspecialchannel = channel;
                    const checkchannel = message.guild.channels.cache.get(clearspecialchannel);
                    if(checkchannel){
                        if(!message.guild.channels.cache.get(clearspecialchannel).memberPermissions(_client.user).hasPermission("SEND_MESSAGES")){
                            const embed = new Discord.MessageEmbed()
                            .addField("Error.", "You are Missing something... that I don't have permission to send message to <#"+channelid+">")
                            .setColor("#FF0000");
                            await message.channel.send(embed);
                            return;
                        }
                    }
                    else {
                        const embed = new Discord.MessageEmbed()
                        .addField("Error.", "Channel not Found.")
                        .setColor("#FF0000");
                        await message.channel.send(embed);
                        return;
                    }
                    if(!type){
                        const embed = new Discord.MessageEmbed()
                        .addField("Error.", "Type is Missing.")
                        .setColor("#FF0000");
                        await message.channel.send(embed);
                    }
                    else if (type.toLowerCase() != "message" & type.toLowerCase() != "embed"){
                        const embed = new Discord.MessageEmbed()
                        .addField("Error.", "Only accept `message` and `embed`")
                        .setColor("#FF0000");
                        await message.channel.send(embed);
                    }
                    else if (!msg){
                        const embed = new Discord.MessageEmbed()
                        .addField("Error.", "Message is Missing.")
                        .setColor("#FF0000");
                        await message.channel.send(embed);
                    }
                    else if (await welcome.Welcome(_client.guilds.cache.get(message.guild.id).member(message.author), null, type.toLowerCase() == "message" ? "message" : "embed", "length", args.slice(3).join(" ")) == "Error2"){
                        const embed = new Discord.MessageEmbed()
                        .addField("Error.", "Message too long (Your Message longer than "+type.toLowerCase() == "message" ? "2048" : "1024"+" words).")
                        .setColor("#FF0000");
                        await message.channel.send(embed);
                    }
                    else {
                        const embed = new Discord.MessageEmbed()
                        .addField("Welcome Message Change Successfully.", "To know current settings, use `"+prefix+"guildsettings welcome show`")
                        .setColor("#00FF00");
                        message.channel.send(embed);
                        await db.GuildWelcome(message.guild.id, "type", type);
                        await db.GuildWelcome(message.guild.id, "channel", clearspecialchannel);
                        await db.GuildWelcome(message.guild.id, "msg", args.slice(3).join(" "));
                        await db.Save("GuildWelcome");
                    }
                }
            }
            else if (args[0].toLowerCase() == "dmwelcome"){
                const type = args[1];
                const msg = args[2];
                if (!args[1]) {
                    const embed = new Discord.MessageEmbed()
                    .setAuthor("Command Help.")
                    .setDescription("<> - Required | [] - Optional")
                    .addField(""+prefix+"guildsettings "+args[0]+" <Type> <Message>", "**Commands Support:**\n`"+prefix+"guildsettings "+args[0]+" review <Type> <Message>`\n`"+prefix+"guildsettings "+args[0]+" off` (Turn Off this Feature)\n`"+prefix+"guildsettings "+args[0]+" show` (Show current DM Welcome settings of Server)\n**Guides:**\n- Type: <Message/Embed>\n- Message: Write DM Welcome.\n`Variable Reference by Command -variablereference`")
                    .setColor("#00FFFF");
                    message.channel.send(embed);
                    return;
                }

                if (args[1].toLowerCase() == "show"){
                    const check2 = await db.GuildWelcome(message.guild.id, "dmtype");
                    const msg = check2 == 0 ? "Nothing Yet." : "Type: "+await db.GuildWelcome(message.guild.id, "dmtype")+"\nMessage:\n`"+db.GuildWelcome(message.guild.id, "dm")+"`";
                    const embed = new Discord.MessageEmbed()
                    .setAuthor(`${message.guild.name}'s Current DM Welcome Settings`, message.guild.iconURL)
                    .setDescription(msg)
                    .setTimestamp()
                    .setFooter(`Requested by ${message.author.tag}`, message.author.avatarURL)
                    .setColor("#FF33FF");
                    message.channel.send(embed);
                }
                else if (args[1].toLowerCase() == "off"){
                    const embed = new Discord.MessageEmbed()
                    .addField("DM Welcome Change Successfully.", "DM Welcome has been turned Off")
                    .setColor("#00FF00");
                    message.channel.send(embed);
                    await db.GuildWelcome(message.guild.id, "dmtype", "0");
                    await db.GuildWelcome(message.guild.id, "dm", "0");
                    await db.Save("GuildWelcome");
                }
                else if (args[1].toLowerCase() == "review"){
                    if(!args[2]){
                        const embed = new Discord.MessageEmbed()
                        .addField("Error.", "Type is Missing.")
                        .setColor("#FF0000");
                        await message.channel.send(embed);
                    }
                    else if (args[2].toLowerCase() != "message" & args[2].toLowerCase() != "embed"){
                        const embed = new Discord.MessageEmbed()
                        .addField("Error.", "Only accept `message` and `embed`")
                        .setColor("#FF0000");
                        await message.channel.send(embed);
                    }
                    else if (!args[3]){
                        const embed = new Discord.MessageEmbed()
                        .addField("Error.", "Message is Missing.")
                        .setColor("#FF0000");
                        await message.channel.send(embed);
                    }
                    else if (await welcome.Welcome(_client.guilds.cache.get(message.guild.id).member(message.author), null, args[2].toLowerCase() == "message" ? "message" : "embed", "length", args.slice(3).join(" ")) == "Error2"){
                        const embed = new Discord.MessageEmbed()
                        .addField("Error.", "Message too long (Your Message longer than "+args[2].toLowerCase() == "message" ? "2048" : "1024"+" words).")
                        .setColor("#FF0000");
                        await message.channel.send(embed);
                    }
                    else {
                        await welcome.Welcome(_client.guilds.cache.get(message.guild.id).member(message.author), "dm", args[2], "review", args.slice(3).join(" "), _client, message);
                    }
                }
                else {
                    if(!type){
                        const embed = new Discord.MessageEmbed()
                        .addField("Error.", "Type is Missing.")
                        .setColor("#FF0000");
                        await message.channel.send(embed);
                    }
                    else if (type.toLowerCase() != "message" & type.toLowerCase() != "embed"){
                        const embed = new Discord.MessageEmbed()
                        .addField("Error.", "Only accept `message` and `embed`")
                        .setColor("#FF0000");
                        await message.channel.send(embed);
                    }
                    else if (!msg){
                        const embed = new Discord.MessageEmbed()
                        .addField("Error.", "Message is Missing.")
                        .setColor("#FF0000");
                        await message.channel.send(embed);
                    }
                    else if (await welcome.Welcome(_client.guilds.cache.get(message.guild.id).member(message.author), null, type.toLowerCase() == "message" ? "message" : "embed", "length", args.slice(2).join(" ")) == "Error2"){
                        const embed = new Discord.MessageEmbed()
                        .addField("Error.", "Message too long (Your Message longer than "+args[2].toLowerCase() == "message" ? "2048" : "1024"+" words).")
                        .setColor("#FF0000");
                        await message.channel.send(embed);
                    }
                    else {
                        const embed = new Discord.MessageEmbed()
                        .addField("DM Welcome Change Successfully.", "To know current settings, use `"+prefix+"guildsettings dmwelcome show`")
                        .setColor("#00FF00");
                        message.channel.send(embed);
                        await db.GuildWelcome(message.guild.id, "dmtype", type);
                        await db.GuildWelcome(message.guild.id, "dm", args.slice(2).join(" "));
                        await db.Save("GuildWelcome");
                    }
                }
            }

            else {
                const embed = new Discord.MessageEmbed()
                .addField("Error.", "`"+args[0]+"` not Found.")
                .setColor("#FF0000");
                await message.channel.send(embed);
            }

        }
    }
}