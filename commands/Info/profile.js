const Discord = require("discord.js");
const db = require("../../db.js");
const func = require("../../function.js");
module.exports = {
    name: "cmds",
    aliases: ["help", "commands"],
    category: "Help",
    description: "Returns Help Commands List.",
    run: async(_client, message, args) => {
        return func.Error("This feature is Coming Soon.");
    }
}