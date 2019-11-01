const client = require("nekos.life");
const nekoslife = new client();
const Discord = require("discord.js");

module.exports = {
    name: "nya",
    category: "Fun",
    description: "Returns Nya Message.",
    run: async(_client, message, args) => {
        const nya = await nekoslife.sfw.catText();
        await message.channel.send(message.author.toString()+", Nya~!! "+nya.cat+"");
    }
}