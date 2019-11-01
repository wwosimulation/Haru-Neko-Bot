const Discord = require("discord.js");
const Jimp = require("jimp");
const db = require("../db.js");

module.exports = {
    //Future.
    Backgrounds: async function(message, value, tocheck, color){
        if (value == 1) value = "https://media.discordapp.net/attachments/633994129537237003/634759585533657108/image0.jpg?width=1920&height=1080";
        if (value == 2) value = "https://media.discordapp.net/attachments/633994129537237003/634759670887743503/image0.jpg?width=1920&height=1080";
        if (value == 3) value = "https://media.discordapp.net/attachments/633994129537237003/634787781586976798/967e5a1e0d7078684ffc3fd1e4e64811.png?width=1920&height=1080";
        if (value == 4) value = "https://media.discordapp.net/attachments/633994129537237003/636529025338179614/CePBJbdspBwbr3AKxLCNGzYbpi27LPcIbH2-1vKp0YsK4yHGBsJsu6-TiYbEKY4qPPxCXwtijN6kjdf1dhq4LWZLofUFqkkVXcwJ.png?width=1920&height=1080";
        if (value == 5) value = "https://media.discordapp.net/attachments/633994129537237003/636529299092144128/87205.png?width=1920&height=1080"
        if (value == 6) value = "https://media.discordapp.net/attachments/633994129537237003/636529829235392512/Anime-Landscape-Wallpaper-HD.png?width=1920&height=1080";
        if (value == 7) value = "https://media.discordapp.net/attachments/633994129537237003/636531221022769173/18136-epic-anime-wallpapers-1920x1080-fuer-macbook.png?width=1920&height=1080";
        if (value == 8) value = "https://media.discordapp.net/attachments/633994129537237003/636531525852200980/wp3442876.png?width=1920&height=1080";
        if (value == 9) value = "https://media.discordapp.net/attachments/633994129537237003/636531879880949760/607699.png?width=1920&height=1080";
        if (value == 10) value = "https://media.discordapp.net/attachments/633994129537237003/636532240548888607/549582.png?width=1920&height=1080";
        if (tocheck == "list") return value;
        if (value) {

            const abg = await Jimp.read(value).catch( (err) => {
                return "Error";
            });
            if (abg == "Error") return "Error";
            if (tocheck == "yes") return;
            var font;
            if(tocheck != "review" & tocheck != "yes"){
                var scolor = await db.GuildBackground(message.guild.id, "color");
                var colors = `${scolor}`;
                if (colors.toLowerCase() == "white") font = await Jimp.loadFont(Jimp.FONT_SANS_64_WHITE);
                if (colors.toLowerCase() == "black") font = await Jimp.loadFont(Jimp.FONT_SANS_64_BLACK);
            }
            else {
                if(!color) color = "black";
                if (color.toLowerCase() != "black" & color.toLowerCase() != "white") return "Error2";
                if (color.toLowerCase() == "white") font = await Jimp.loadFont(Jimp.FONT_SANS_64_WHITE);
                if (color.toLowerCase() == "black") font = await Jimp.loadFont(Jimp.FONT_SANS_64_BLACK);
            }
            const background = new Jimp(1920, 1080);
            //White Background
            const sbg = await Jimp.read("https://media.discordapp.net/attachments/634788544166101004/635439325143629824/unknown.png?width=1920&height=1080");
            const guildimage = await Jimp.read(message.guild.iconURL);

            abg.resize(1920, 1080);
            sbg.resize(510, 510);
            guildimage.resize(500, 500);
            await background.composite(abg, 0, 0);
            await background.composite(sbg, 45, 45);
            await background.composite(guildimage, 50, 50);

            const print = (x, y, msg) => background.print(font, x, y, msg, background.bitmap.width, background.bitmap.height);

            var mainY = 50;
            var CountY = Math.floor(510 / 6);
            var getmembers = message.guild.members.size;
            var getusercount = message.guild.members.filter(member => !member.user.bot).size;
            
            print(575, mainY, `Server Name: ${message.guild.name}`);
            print(575, mainY+CountY, `Server Owner: ${message.guild.owner.user.tag}`);
            print(575, mainY+(Math.floor(CountY * 2)), `Region: ${message.guild.region}`);
            print(575, mainY+(Math.floor(CountY * 3)), `Members: ${getmembers} | Users: ${getusercount} | Bots: ${Math.floor(getmembers-getusercount)}`) ;
            print(575, mainY+(Math.floor(CountY * 4)), `Channels: ${message.guild.channels.size} | Roles: ${message.guild.roles.size}`);
            const servercreatedon = `${message.guild.createdAt}`.replace("GMT+0000 (UTC)", " ");
            print(575, mainY+(Math.floor(CountY * 5)), `Server Created At: ${servercreatedon}`);
            
            background.getBuffer(Jimp.MIME_PNG, (err, buffer) => {
                if(err) return "Error";

                return message.channel.send(`Server ID: ${message.guild.id}`,{file: buffer});
            });
        }
        else return "Error";
    }


}