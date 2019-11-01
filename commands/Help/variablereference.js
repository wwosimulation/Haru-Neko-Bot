const Discord = require("discord.js");
const db = require("../../db.js");
module.exports = {
    name: "variablereference",
    category: "Help",
    description: "Returns Help of Variable Reference from Welcome Message.",
    run: async(_client, message, args) => {
        const prefix = db.GuildInfo(message.guild.id, "prefix");
        const max = 2;
        const page = args[0];
        const embed = new Discord.RichEmbed();
        if(!page) {
            embed.setAuthor("Command Help.");
            embed.setDescription("<> - Required | [] - Optional\n\n**"+prefix+"variablereference <1-2/embed>**");
            embed.setColor("#00FFFF");
            return message.channel.send(embed);
        }
        if (page == "1"){
            
            embed.addField("Variable Reference.", "**`{user}` return Mention User.**\n**Eg:** "+message.author.toString()
            +"\n\n**`{user.username}` return Username**\n**Eg:** "+message.author.username
            +"\n\n**`{user.tag}` return Username and Discriminator.**\n**Eg:** "+message.author.tag
            +"\n\n**`{user.id}` return ID User.**\n**Eg:** "+message.author.id
            +"\n\n**`{user.avatar}` return AvatarURL of User.**\n**Eg:** [Your Avatar Link]("+message.author.avatarURL+")");
            embed.setColor("FFF000");
            embed.setTimestamp();
            embed.setFooter(`Requested by ${message.author.tag} | Page ${page}/${max}`, message.author.avatarURL);
        }
        else if (page == "2"){
            embed.addField("Variable Reference.", "**`{server}` return Server Name**\n**Eg:** "+message.guild.name
            +"\n\n**`{server.id}` return Server ID**\n**Eg:** "+message.guild.id
            +"\n\n**`{server.avatar}` return ImageURL of Server**\n**Eg:** [Your Image of Server Link]("+message.guild.iconURL+")");
            embed.setColor("FFF000");
            embed.setTimestamp();
            embed.setFooter(`Requested by ${message.author.tag} | Page ${page}/${max}`, message.author.avatarURL);
        }
        else if (page.toLowerCase() == "embed"){
            var S1 = '"$1"';
            embed.setAuthor("Variable Reference.");
            embed.setDescription("**__NOTE:__** When you use Embed for Welcome Message, you must be follow step below or your Embed will be Break.");
            embed.addField(`{title:"$1", $2}`, "$1 - Your Message.\n$2 - Must be Avatar/Image URL.\n(Can use `{user.avatar}` or `{server.avatar}`)\n**Note:** If you don't want $2, Just only `{title:"+S1+"}`\n|==========|");
            embed.addField(`{color:$1}`, "$1 - [Hex Color](https://www.color-hex.com/)(Can use `{color:random}` for random Color)\n|==========|");
            embed.addField(`{time}`, "Embed Time.\n(Timestamp)\n|==========|");
            embed.addField(`{pingwhenjoin}`, "Mention User as Message.\n|==========|");
            embed.addField(`{footer:"$1", $2}`, "$1 - Your Message.\n$2 - Must be Avatar/Image URL.\n(Can use `{user.avatar}` or `{server.avatar}`)\n**Note:** If you don't want $2, Just only `{footer:"+S1+"}`\n|==========|");
            embed.setColor("FFF000");
            embed.setTimestamp();
            embed.setFooter(`Requested by ${message.author.tag} | Page ${page}`, message.author.avatarURL);
        }
        else {
            embed.addField("Error.", "This Page is Not Available.");
            embed.setColor("FF0000");
            embed.setTimestamp();
            embed.setFooter(`Requested by ${message.author.tag}`, message.author.avatarURL);
        }
        
        
        return message.channel.send(embed);
    }
}