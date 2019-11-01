const {readdirSync} = require("fs");

const ascii = require("ascii-table");

const table = new ascii().setHeading("Command", "Load Status");

module.exports = (_client) => {
    readdirSync("./commands/").forEach(dir => {
       const commands = readdirSync(`./commands/${dir}`).filter(f => f.endsWith(".js"));

       for (let file of commands){
           let pull = require(`../commands/${dir}/${file}`);

           if (pull.name){
               _client.commands.set(pull.name, pull);
               table.addRow(file, 'Good!');
            } else {
               table.addRow(file, 'Error -> Missing something?');
               continue;
           }

           if (pull.aliases && Array.isArray(pull.aliases))
                pull.aliases.forEach(alias => _client.aliases.set(alias, pull.name));

       }

    });
    
    console.log(table.toString());
}