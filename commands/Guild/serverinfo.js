const bg = require("../../CommandFunction/guildbackgrounds.js");
const db = require("../../db.js");
const Discord = require("discord.js");

module.exports = {
    name: "serverinfo",
    category: "Guild",
    description: "Returns Server Informations.",
    run: async(_client, message, args) => {
        const premium = db.GuildInfo(message.guild.id, "perm");
        const background = db.GuildBackground(message.guild.id, "bg");

        if (db.GuildInfo(message.guild.id, "perm") == "No" && db.GuildBackground(message.guild.id, "bg") != "0"){
            await db.GuildBackground(message.guild.id, "bg", "0");
            await db.GuildBackground(message.guild.id, "color", "0");
            await db.Save("GuildBackground");
        }

        if (db.GuildBackground(message.guild.id, "bg") != "0"){
            const serverinfo = await message.channel.send("Image Generating...");
            await bg.Backgrounds(message, background);
            await serverinfo.delete();
            return;
        }
        else {
            const server = message.guild;

            var getmembers = message.guild.members.size;
            var getusercount = message.guild.members.cache.filter(member => !member.user.bot).size;

            const servercreatedon = `${message.guild.createdAt}`.replace("GMT+0000 (UTC)", " ");

            const embed = new Discord.MessageEmbed()

            .setAuthor(`${server.name}`, server.iconURL)
            .setThumbnail(server.iconURL)
            .addField(`Owner`, server.owner.toString(), true)
            .addField(`Region`, server.region, true)
            .addField(`Members`, server.members.size, true)
            .addField(`Users`, getusercount, true)
            .addField(`Bots`, Math.floor(getmembers-getusercount), true)
            .addField(`Roles`, server.roles.size, true)
            .addField(`Server Created At`, servercreatedon, true)
            .setFooter(`Server ID ${server.id}`)
            .setColor("#FF33FF");

            return message.channel.sendEmbed(embed);
        }
    }
}