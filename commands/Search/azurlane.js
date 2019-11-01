const {AzurLane} = require("azurlane");
const azurlane = new AzurLane();
const Discord = require("discord.js");
const func = require("../../function.js");
module.exports = {
    name: "azurlane",
    category: "Search",
    description: "Returns Informations Character of Azur Lane.",
    run: async(_client, message, args) => {
        if(!args[0]) return func.Error(message, "Name of Character is Missing");
        const c = await azurlane.ship(args.join(" ")).catch( err => {
            return func.Error(message, "Character Name `"+args.join(" ")+"` not Found.");
        });

        const embed = new Discord.RichEmbed()
        .setAuthor(`Character Informations.`)
        .setThumbnail(c.thumbnail)
        .setDescription(`ID: ${c.id}\nNames: ${c.names.en}\nJP: ${c.names.jp}\nBuild Time: ${c.buildTime}\nRarity: ${c.rarity}\nStar(s): ${c.stars.value}\nClass: ${c.class}\nNationality: ${c.nationality}\nHull Type: ${c.hullType}`)
        .setColor("#00FFFF")
        .setTimestamp()
        .setFooter("Powered by AzurLane");

        await message.channel.send(embed).catch(()=>{});
    }
}