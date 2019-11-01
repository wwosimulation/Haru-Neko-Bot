const Discord = require("discord.js");
module.exports = {
    name: "botinfo",
    category: "Info",
    description: "Return Informations of Haru Neko.",
    run: async(_client, message, args) => {
        const nekocyan = "454492255932252160";
        const nekocyans = _client.users.get(nekocyan);
        const botcreatedAt = `${_client.user.createdAt}`.replace("GMT+0000 (UTC)", "");
        const embed = new Discord.RichEmbed()
        .setAuthor("Haru Neko's Informations.")
        .setThumbnail(_client.user.avatarURL)
        .addField("Full Name", _client.user.tag, true)
        .addField("Created On", botcreatedAt, true)
        .addField("Using Libary", "Discord.js", true)
        .addField("Watching", `On ${_client.guilds.size} Servers`, true)
        .addField("Listening", `To ${_client.users.filter( u => !u.bot).size} Users.`, true)
        .setFooter(`Dev by ${nekocyans.tag}`, nekocyans.avatarURL)
        .setColor("#FF33FF");
        return message.channel.send(embed);
    }
}