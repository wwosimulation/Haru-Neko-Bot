const Discord = require("discord.js");
const Jimp = require("jimp");
const db = require("../db.js");
const func = require("../function.js");

module.exports = {
    //Future.
    Welcome: async function(message, channel, type, tocheck, msg, _client, _client2){
        var MSG = `${msg}`;
        let checkpingwhenjoin = false;
        var forreview = tocheck == "review" ? `After Set/Change, Your ${channel=="dm"?"DM Welcome":"Welcome Message"} look like this:\n\n` : "";
        if (type.toLowerCase() == "message" || type.toLowerCase() == "embed"){
            if (MSG.includes("{user}")) { MSG = MSG.replace("{user}", `${message}`); }
            if (MSG.includes("{user.tag}")) { MSG = MSG.replace("{user.tag}", `${message.tag}`); }
            if (MSG.includes("{user.avatar}")) { MSG = MSG.replace("{user.avatar}", `${message.user.avatarURL}`); }
            if (MSG.includes("{user.username}")) { MSG = MSG.replace("{user.username}", `${message.username}`); }
            if (MSG.includes("{user.id}")) { MSG = MSG.replace("{user.id}", `${message.id}`); }
            if (MSG.includes("{server}")) { MSG = MSG.replace("{server}", `${message.guild.name}`); }
            if (MSG.includes("{server.avatar}")) { MSG = MSG.replace("{server.avatar}", `${message.guild.iconURL}`); }
            if (MSG.includes("{server.id}")) { MSG = MSG.replace("{server.id}", `${message.guild.id}`); }

            if (type.toLowerCase() == "embed"){
                const embed = new Discord.MessageEmbed();
                if (MSG.includes("{pingwhenjoin}")) { MSG = MSG.replace("{pingwhenjoin}", ``); checkpingwhenjoin = true; }
                if (MSG.includes("{color:random}")) { MSG = MSG.replace("{color:random}", ``); embed.setColor(`${func.randomHex(6)}`); }
                if (MSG.includes("{time}")) { MSG = MSG.replace("{time}", ``); embed.setTimestamp(); }
                var matchfooter = new RegExp('{footer:"(.*)", (.*)}');
                var footer = MSG.match(matchfooter);
                if(footer){
                    if (MSG.includes(`{footer:"${footer[1]}", ${footer[2]}}`)) {
                        MSG = MSG.replace(`{footer:"${footer[1]}", ${footer[2]}}`, ''); 
                        embed.setFooter(footer[1], footer[2])                 
                        }
                }
                var matchfooter = new RegExp('{footer:"(.*)"}');
                var footer = MSG.match(matchfooter);
                if(footer){
                    if (MSG.includes(`{footer:"${footer[1]}"}`)) {
                        MSG = MSG.replace(`{footer:"${footer[1]}"}`, ''); 
                        embed.setFooter(footer[1])
                        }
                }
                var matchcolor = new RegExp('{color:(.*)}');
                var color = MSG.match(matchcolor);
                if(color){
                    if (MSG.includes(`{color:${color[1]}}`)) {
                        MSG = MSG.replace(`{color:${color[1]}}`, ''); 
                        embed.setColor(color[1])                 
                        }
                }
                var matchauthor = new RegExp('{title:"(.*)", (.*)}');
                var author = MSG.match(matchauthor);
                if (author){
                    if (MSG.includes(`{title:"${author[1]}", ${author[2]}}`)) {
                    MSG = MSG.replace(`{title:"${author[1]}", ${author[2]}}`, ''); 
                    embed.setAuthor(author[1], author[2])        
                    }
                }
                var matchauthor = new RegExp('{title:"(.*)"}');
                var author = MSG.match(matchauthor);
                if (author){
                    if (MSG.includes(`{title:"${author[1]}"}`)) {
                    MSG = MSG.replace(`{title:"${author[1]}"}`, ''); 
                    embed.setAuthor(author[1])        
                    }
                }
                
                if(tocheck == "length") {
                    if(MSG.length > 1024) return "Error2";
                    else return;
                }
                embed.setDescription(MSG);

                if (channel == "dm" & tocheck != "review") return message.send(embed);

                if (channel!="dm"& tocheck != "review") {return _client.guilds.cache.get(message.guild.id).channels.cache.get(channel).send(`${forreview}${checkpingwhenjoin == true ? message : ""}`,embed);}
                else return _client2.channel.send(`${forreview}${checkpingwhenjoin == true ? message : ""}`, embed);
            }

            if (type.toLowerCase() == "message"){
                if(tocheck == "length") {
                    if(MSG.length > 2048) return "Error2";
                    else return;
                }
            }

            }

            if (channel == "dm" & tocheck != "review") return message.send(MSG);

            if (channel!="dm" & tocheck != "review") {return message.guild.channels.cache.get(channel).send(`${forreview}${MSG}`);}
            else return _client2.channel.send(`${forreview}${MSG}`);
        }


}