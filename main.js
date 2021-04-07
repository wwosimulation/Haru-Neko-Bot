// R E Q U I R E M E N T

const fs = require("fs");
const Discord = require("discord.js");
const {Client, Collection} = require("discord.js");
const _client = new Client({disableMentions: "everyone"});
const db = require("./db.js");
const func = require("./function.js");
const nekoslife = require("nekos.life");
const emotes = require("./Another/emoji.js");
const welcome = require("./CommandFunction/guildwelcome.js");

_client.commands = new Collection();
_client.aliases = new Collection();

const token = fs.readFileSync("./token/discordbot.txt", "utf8")

_client.login(token);

// C O M M A N D S H A N D L E R

["command"].forEach(handler => {
    require(`./handler/${handler}`)(_client);
});

// C L I E N T R E A D Y
var countingtimes = 1;
_client.on("ready", () => {
    console.log(`${_client.user.tag} is Online and on ${_client.guilds.cache.size} servers!`);

    setInterval( () => {
        const activity = (msg, type) => { if(!type) type = "PLAYING";_client.user.setActivity(msg, {type: type})}
        if(countingtimes == 1) activity("with Neko's Family.");
        if(countingtimes == 2) activity(`on ${_client.guilds.cache.size} Servers.`, "WATCHING");
        if(countingtimes == 3) activity(`${_client.users.cache.filter( u => !u.bot).size} Users.`, "LISTENING");
        if(countingtimes == 4) activity(`-invite`, "STREAMING");
        if(countingtimes == 4) countingtimes = 0;
        countingtimes++;
    }, 15000);
});
 
// C L I E N T J O I N (Member)

_client.on("guildMemberAdd", async (member) => {
    var memmacun = _client.guilds.cache.get("530689610313891840").members.cache.get(member.id);//Get Members of guild Ma Cún.
    var memwwo = _client.guilds.cache.get("465795320526274561").members.cache.get(member.id);// Get Members of guild WWO Simulation.

    if(member.guild.id == 530689610313891840){ //Guild Ma Cún.
        await member.guild.channels.cache.get("530689610313891843").send("Chào mừng "+member.toString()+" đã đến với **Ma Sói Discord - Ma Cún**!");
        await member.roles.add(member.guild.roles.cache.find(x => x.name == "Members"));
        await member.roles.add(member.guild.roles.cache.find(x => x.name == "Dân Làng"));

        const embed = new Discord.MessageEmbed()
        .setAuthor(member.guild.name, member.guild.iconURL)
        .addField("Ma Sói Discord - Ma Cún", `Chào mừng bạn đã đến với Server Ma Cún.\nVui lòng hãy đọc các thông tin ở dưới đây để có thể hiểu biết về Server.\n- Luật Server: <#530699541192638475> \n- Luật Ma Sói: <#587643757441187858> \n- Cách Chơi Ma Sói: <#584981323920179200> \nChúc bạn vui vẻ trong Server!`)
        .setTimestamp()
        .setFooter("By Ma Cún Staff Team")
        .setColor("#00FF00");
        member.send(embed);
        return;
    }// End lines of Guild Ma Cún.

    if(member.guild.id == 580555457983152149){ //Guild Ma Cún - Game Server.
        //member.roles.cache.has("rolename");
        member.roles.add(member.guild.roles.cache.get("580556269765656587"));//Default role
        if(memmacun.roles.cache.has("582788440161124353"))//id of dj role
        { member.roles.add(member.guild.roles.cache.find(x => x.name == "DJ")); }
        if(memmacun.roles.cache.has("534583471704899585"))//id of quản trò role
        { member.roles.add(member.guild.roles.cache.find(x => x.name == "Quản Trò")); }
        return;
    }// End lines of Guild Ma Cún - Game Server.
    /*
    if(member.guild.id == 465795320526274561){ //Guild WWO Simulation.
        if (member.id == 326306397635477504){ member.roles.add(member.guild.roles.cache.get("627546767516237827")); }
        var c = "Welcome "+member.toString()+" to the Werewolf Online Simulation server in Discord! To get started, invite your friends to play together, and ping any online Game Narrator. They will set up a game for you. If you do not want to be pinged when a game starts, go to <#606123783605977108> and click on 🎮! Also, do make sure to check <#606123774978293772>, <#606123778589851648> and ! We hope you have a nice time here!\n**BTW**, if you just joined and cannot talk in <#606123800253431808> or any other channel, your account has been automatically flagged as suspicious. If you joined less than 20 minutes ago, please check your DMs with <@372022813839851520> for the link to verify yourself. If you joined more than 20 minutes ago, go to <#618162028770623508>. The instructions on how to proceed will be there.";
        var check = _client.users.get("155149108183695360").presence.status;
        if (check == "offline")
        { member.guild.channels.cache.get("606123796872560670").send(c);
            member.roles.add(member.guild.roles.cache.get("606123686633799680"));
            member.roles.add(member.guild.roles.cache.get("606123691889393705"));
            member.roles.add(member.guild.roles.cache.get("606167032425218084"));
        }
        return;
    }// End lines of Guild WWO Simulation.

    if(member.guild.id == 472261911526768642){ //Guild WWO Simulation - Game Server
        var check = "no";

        if(memwwo.roles.cache.has("606123676668133428"))//id of Joining role
        {check = "yes";}
        if(memwwo.roles.cache.has("606123620732895232"))//id of Mini Narrator role
        {check = "yes";}
        if(memwwo.roles.cache.has("606123619999023114"))//id of Game Narrator role
        {check = "yes";}

        if(check == "no"){
            member.kick();
            member.send('You must have "Joining" role in WWO Simulation to join **WWO Simulation - Game Server**!');
            
            const embed = new Discord.MessageEmbed()
            .setAuthor(member.guild.name, member.guild.iconURL)
            .addField("Join Dectected.", `**User:** ${_client.users.get(member.id).toString()}\n**Joining role:** No`)
            .setTimestamp()
            .setFooter(`${_client.users.get(member.id).tag} has been kicked`, _client.users.get(member.id).avatarURL)
            .setColor("#FF0000");
            _client.guilds.cache.get("465795320526274561").channels.cache.get("606123748738859008").send(embed);
        }
        else {
            var check = _client.users.cache.get("155149108183695360").presence.status;
            if (check == "offline"){
                member.roles.add(member.guild.roles.cache.get("606131215526789120"));
                if(memwwo.roles.cache.has("606123620732895232")) { member.roles.add(member.guild.roles.cache.get("606155761286119425")); }//Mini Narrator
                if(memwwo.roles.cache.has("606123619999023114")) { member.roles.add(member.guild.roles.cache.get("606140995897393164")); }//Game Narrator
            }
        }
        return;
    }// End lines of Guild WWO Simulation - Game Server.
    */
    const check2 = await db.GuildWelcome(member.guild.id, "type");
    if (check2 != 0){
        const type = await db.GuildWelcome(member.guild.id, "type");
        const channel = await db.GuildWelcome(member.guild.id, "channel");
        const msg = await db.GuildWelcome(member.guild.id, "msg");
        await welcome.Welcome(member, channel, type, null, msg, _client);
    }
    const check3 = await db.GuildWelcome(member.guild.id, "dmtype")
    if (check3 != 0){
        const type = await db.GuildWelcome(member.guild.id, "dmtype");
        const msg = await db.GuildWelcome(member.guild.id, "dm");
        await welcome.Welcome(member, "dm", type, null, msg, _client).catch( err => console.log(err) );
    }
});

// C L I E N T R E M O V E (Member)

_client.on('guildMemberRemove', async (member) =>{

    if(member.guild.id == 530689610313891840){
        member.guild.channels.cache.get("530689610313891843").send("Tạm biệt **"+_client.users.get(member.id).tag+"** và cảm ơn bạn đã tham gia!");
    }

    if (member.guild.id == 472261911526768642){
        if (GlobalFunction.prototype.hoster == member.id){
            makeannounce("none");
            GlobalFunction.prototype.hoster = null;
            setTimeout( () => {
                _client.guilds.cache.get("472261911526768642").channels.cache.get("606422958721859585").send("= = = = = = E N D = = = = = =");
            }, 1000);
        }
    }

});

// C L I E N T C O M M A N D

_client.on("message", async (message) => {
    if (message.author.bot) return;

    let dmargs = message.content.slice("-".length).trim().split(/ +/g);
    let dmcmd = dmargs.shift().toLowerCase();

    if(dmcmd == "cfs"){
        if(message.guild) return;
        var checkguilduser = _client.guilds.cache.get("599521687792386088").members.cache.get(message.author.id);
        var tsnguild = _client.guilds.cache.get("599521687792386088");
        if(checkguilduser){
            if(!dmargs[0]) {
                const embed = new Discord.MessageEmbed()
                .addField("Error!", "Message is Missing.")
                .setColor("FF0000");
                message.author.send(embed);
            }
            else if(dmargs.join(" ").length > 1024){
                const embed = new Discord.MessageEmbed()
                .addField("Error!", "Message too long.")
                .setColor("FF0000");
                message.author.send(embed);
            }
            else {
                const getcount = fs.readFileSync("./tsncfscount.txt", "utf8");
                const convertcount = parseInt(getcount);
                const color = func.randomHex(6);
                const embed = new Discord.MessageEmbed()
                .setTitle(":love_letter: THE Social Network Confession :love_letter:")
                .setDescription(dmargs.join(" "))
                .setTimestamp()
                .setFooter(`Confession Dev by ${_client.users.get("454492255932252160").tag} | CFS#${convertcount}`, _client.users.get("454492255932252160").avatarURL)
                .setColor(`#${color}`);
                tsnguild.channels.cache.get("634023560276475906").send(embed);
                
                const embed2 = new Discord.MessageEmbed()
                .addField("Confession!", "Your Message have been sent and will appear in <#634023560276475906>.")
                .setColor("#00FF00");
                message.author.send(embed2);

                const embed3 = new Discord.MessageEmbed()
                .addField(`Confession ID #${convertcount}`, `Sent by ${_client.users.get(message.author.id).toString()}`)
                .setColor("00FFFF");
                tsnguild.channels.cache.get("634024994355150861").send(embed3);

                await fs.writeFile("./tsncfscount.txt", `${Math.floor(convertcount + 1)}`);
            }
        }
        else return;
    }

    if (!message.guild) return;

    if(message.content.toLowerCase() == "~nya"){
        const client = require('nekos.life');
        const neko = new client();

        const nya = await neko.sfw.catText();

        message.channel.send(`${message.author.toString()}, Nya~!! ${nya.cat}`);
    }

    const prefix = db.GuildInfo(message.guild.id, "prefix");

    if (message.content == _client.user || message.content == _client.user.tag || message.content == `<@${_client.user.id}>` || message.content == `<@!${_client.user.id}>`) return message.channel.send(message.author.toString()+", My Prefix of this guild is `"+prefix+"`");

    if (!message.content.startsWith(prefix)) return;
    if (!message.member) message.member = await message.guild.fetchMember(message);

    const args = message.content.slice(prefix.length).trim().split(/ +/g);
    const cmd = args.shift().toLowerCase();
    
    if (cmd.length == 0) return;

    let command = _client.commands.get(cmd);
    if (!command) command = _client.commands.get(_client.aliases.get(cmd));

    if (command)
        command.run(_client, message, args);

    if (cmd == "a"){
        message.channel.send("A lại nà :3");
    }

    if (cmd == "addcode"){
        if(message.author.id != 454492255932252160) return;
        if(!args[0]) {
            const embed = new Discord.MessageEmbed()
            .setAuthor("Command Help.")
            .setDescription("<> - Required | [] - Optional")
            .addField(""+prefix+"addcode <NewCode> <Name> <Value> [Message]", "\n**Guides:** Nothing.")
            .setColor("#00FFFF");
            return message.channel.send(embed);
        }
        if(args[1].toLowerCase() == "emoji"){
            if(args.slice(2).join(" ") == "Full Access") {
                var check = await db.RedeemCode(args[0], "add");
                if(check == "Error2") return func.Error(message, "This Code is Available.");
                else {
                    await db.RedeemCode(args[0], "add", "Emoji", "Full Access");
                    message.channel.send("Added.");
                }
            }
            else{
                var hasparse = parseInt(args[2]);
                if(typeof hasparse != "number") return func.Error(message, "Value of Emoji must be Number.");
                var check = await db.RedeemCode(args[0], "add");
                if(check == "Error2") return func.Error(message, "This Code is Available.");
                else if (await emotes.Emotes(`${args[2]}`) == "Error") return func.Error("This Emoji is Not Available.");
                else {
                    await db.RedeemCode(args[0], "add", "Emoji", args[2]);
                    message.channel.send("Added.");
                }
            }
        }
        else return func.Error(message, "Only accept `emoji`.");
    }

    if (cmd == "removecode"){
        if(message.author.id != 454492255932252160) return;
        if(!args[0]) {
            const embed = new Discord.MessageEmbed()
            .setAuthor("Command Help.")
            .setDescription("<> - Required | [] - Optional")
            .addField(""+prefix+"removecode <Code>", "\n**Guides:** Nothing.")
            .setColor("#00FFFF");
            return message.channel.send(embed);
        }
        if(await db.RedeemCode(args[0], "remove") == "Error1") return func.Error(message, "This Code is Not Available.");
        else {
            await db.RedeemCode(args[0], "remove");
            return message.channel.send("Removed.");
        }
    }

    if(cmd == "random"){
        if(!args[0]) return;
        var a = parseInt(args[0]);
        if(typeof a != "number") return;
        return message.channel.send(func.randomHex(a));
    }

    /*if(cmd == "abs"){
        const m = msg => console.log(msg);
        m(`Server ID: ${message.guild.id}`);
        m(`Not Verify: ${message.guild.roles.cache.find("name", "Not Verify").id}`);
        m(`Verified: ${message.guild.roles.cache.find("name", "Verified").id}`);
    }*/
    if(message.guild.id == 636778027254284298){//Haru Neko Guild ID.
        if(cmd == "verify"){
            var notverify = "636779797279801357";
            var verified = "636782830302527502";
            if (message.guild.member(message.author).roles.cache.has(notverify)){
                await message.guild.member(message.author).roles.add(message.guild.roles.cache.get(verified));
                await message.guild.member(message.author).roles.remove(message.guild.roles.cache.get(notverify));
            }
        }
    }

    if(cmd == "invite"){
        var msg = `Hello, Sorry because I can't ready to join Server, but you can join https://discord.gg/p78wxxN to get Help from Staff!`;
        var botinviteperm = "https://discordapp.com/api/oauth2/authorize?client_id=586758956924534784&permissions=67493057&scope=bot";
        var vote = "https://bots.discord.gl/bot/586758956924534784";
        var serverlink = "https://discord.gg/p78wxxN";
        const embed = new Discord.MessageEmbed()
        .setAuthor("Haru Neko", _client.user.avatarURL)
        .setDescription(`Hello ${message.author.toString()}, Here is your choose:\n\n[Invite Me](${botinviteperm})\n[Vote For Me](${vote})\n[Join Haru Neko's Server](${serverlink})\n\nYou can get Help from Staff when join Haru Neko's Server if you have any questions.`)
        .setColor("#FF33FF")
        message.author.send(embed).catch( err => {
            message.channel.send(`${message.author.toString()}, Your DM (Direct Message) are Closed, Please Open it then I can send Invite Informations!`);
        });
    }

    if(cmd === "bo" || cmd === "botowner"){
        if (message.author.id != 454492255932252160 & message.author.id != 628825440538198019 & message.author.id != 372372133088591872 & message.author.id != 531662657690927105) return;
        if(!args[0]) return message.channel.send(`Your choice.`);
        let arg0 = args[0];
        let arg1 = args[1];
        let arg2 = args[2];
        if(arg0.toLowerCase() == "setactivity" || arg0.toLowerCase() == "sa"){
            if(!arg1) return message.channel.send(`Syntax: ${Prefix}${cmd} ${arg0} <stream/listen/watch/play> <Name Activity>`);
            if(!arg2) return message.channel.send(`Syntax: ${Prefix}${cmd} ${arg0} ${arg1} <Name Activity>`);
            if(arg1.toLowerCase() == "stream" || arg1.toLowerCase() == "streamming") return _client.user.setActivity(args.slice(2).join(" "), {type: "STREAMING"});
            else if(arg1.toLowerCase() == "listen" || arg1.toLowerCase() == "listening") return _client.user.setActivity(args.slice(2).join(" "), {type: "LISTENING"});
            else if(arg1.toLowerCase() == "watch" || arg1.toLowerCase() == "watching") return _client.user.setActivity(args.slice(2).join(" "), {type: "WATCHING"});
            else if(arg1.toLowerCase() == "play" || arg1.toLowerCase() == "playing") return _client.user.setActivity(args.slice(2).join(" "), {type: "PLAYING"});
            else return message.channel.send(`Syntax: ${Prefix}${cmd} ${arg0} <stream/listen/watch/play> <Name Activity>`);
        }
        else if (arg0.toLowerCase() == "simsimicount" || arg0.toLowerCase() == "ssmc"){
            if(!arg1) return message.channel.send(`Syntax: ${Prefix}${cmd} ${arg0} <show/set> <Number Requested>`);
            if(!arg2 & arg1.toLowerCase() != "show") return message.channel.send(`Syntax: ${Prefix}${cmd} ${arg0} ${arg1} <Number Requested>`);
            let count = fs.readFileSync("./simsimicount.txt", "utf8");
            let count2 = fs.readFileSync("./wwosimsimicount.txt", "utf8");
            let mathcount1 = parseInt(count);
            let mathcount2 = parseInt(count2);
            if(arg1.toLowerCase() == "show") { message.channel.send(`**__API Requested.__**\nMa Cún has ${count}. \nWWO Simulation has ${count2}.\nTotal: ${Math.floor(mathcount1 + mathcount2)}.`); }
            else if(arg1.toLowerCase() == "set") {
                let tocount = args.slice(2).join(" ");
                let parsecount = parseInt(tocount);
                fs.writeFile("./simsimicount.txt", parsecount.toString());
                message.channel.send(`Set value to ${parsecount}`);
            }
            else return message.channel.send(`Syntax: ${Prefix}${cmd} ${arg0} <show/set> <Number Requested>`);
        }
        else if (arg0.toLowerCase() == "neko"){
            /*const request = require("request");
            request('https://nekos.life/api/neko', function (error, response,body) {
                if(!error && response.statusCode == 200){
                    const gettext = JSON.parse(body); 
                    const embed = new Discord.MessageEmbed()
                    .setTitle("**Neko ╰(´︶`)╯♡**")
                    .setImage(gettext.neko)
                    .setTimestamp()
                    .setFooter("Powered by nekos.life")
                    .setColor("#FF33FF");
                    
                    message.channel.sendEmbed(embed);
                }
                else if (error) { message.channel.send(error); }
                else message.channel.send(response.statusMessage);
            });*/
            if (!arg1) {
                const client = require('nekos.life');
                const neko = new client();

                var nekos = await neko.sfw.neko();

                const embed = new Discord.MessageEmbed()

                    .setTitle("**Neko ╰(´︶`)╯♡**")
                    .setImage(nekos.url)
                    .setTimestamp()
                    .setFooter("Powered by nekos.life")
                    .setColor("#FF33FF");
                    
                    message.channel.sendEmbed(embed);
            }
            else if(arg1.toLowerCase() == "link" || arg1.toLowerCase() == "url"){
                const client = require('nekos.life');
                const neko = new client();

                var nekos = await neko.sfw.neko();

                message.channel.send(nekos.url);
            }
            else return;
        }
        else if (arg0.toLowerCase() == "setavatar"){
            if (!arg1) return message.channel.send(`Syntax: ${prefix}${cmd} ${arg0} <AvatarURL>`);
            
            _client.user.setAvatar(args.slice(1).join(" ")).catch( err => {
                message.channel.send(err);
            });

            message.channel.send("Done.");
        }
        else if (arg0.toLowerCase() == "preview"){
            const embed = new Discord.MessageEmbed()
            .setAuthor(message.guild.name, message.guild.iconURL)
            .addField("Ma Sói Discord - Ma Cún", `Chào mừng bạn đã đến với Server Ma Cún.\nVui lòng hãy đọc các thông tin ở dưới đây để có thể hiểu biết về Server.\n- Luật Server: <#530699541192638475> \n- Luật Ma Sói: <#587643757441187858> \n- Cách Chơi Ma Sói: <#584981323920179200> \nChúc bạn vui vẻ trong Server!`)
            .setTimestamp()
            .setFooter("By Ma Cún Staff Team")
            .setColor("#00FF00");
            message.channel.send(embed);
        }
        else if(arg0.toLowerCase() == "guildpremium"){
            if(!arg1) return message.channel.send(`Syntax: ${Prefix}${cmd} ${arg0} <Add/Remove> [Time]`);
            if(arg2) return message.channel.send(`Time will appear coming soon...`);
            if(arg1.toLowerCase() == "add"){
                db.GuildInfo(message.guild.id, "perm", "Premium");
                db.GuildInfo(message.guild.id, "permtimeleft", "Forever");
                const embed = new Discord.MessageEmbed()
                .setAuthor(message.guild.name, message.guild.iconURL)
                .addField("Guild Premium!", "Added Premium to this Guild and Time Left of premium is Forever.")
                .setTimestamp()
                .setFooter("")
                .setColor("#00FF00");
                message.channel.send(embed);

                db.Save("GuildInfo");
            }
            if(arg1.toLowerCase() == "remove"){
                db.GuildInfo(message.guild.id, "perm", "No");
                db.GuildInfo(message.guild.id, "permtimeleft", 0);
                const embed = new Discord.MessageEmbed()
                .setAuthor(message.guild.name, message.guild.iconURL)
                .addField("Guild Premium!", "Removed Premium of this Guild and Time Left of premium is 0.")
                .setTimestamp()
                .setFooter("")
                .setColor("#00FF00");
                message.channel.send(embed);

                db.Save("GuildInfo");
            }
        }
        else if (arg0.toLowerCase() == "tes"){
            var link = `[Click Here](${args.slice(1).join(" ")})`;

            const embed = new Discord.MessageEmbed()
            .setAuthor(link)
            .setDescription(link)
            .addField(link, link);
            message.channel.send(embed);
        }
        else if (arg0.toLowerCase() == "image"){
            const bg = require("./CommandFunction/guildbackgrounds.js");
            bg.Backgrounds(message, args[1], "review", args[2]);
        }
        else if (arg0.toLowerCase() == "date"){
            message.channel.send(Date.now());
            message.channel.send(Date.prototype.getTime());
        }
        else if (arg0.toLowerCase() == "welcome"){
            const welcome = require("./CommandFunction/guildwelcome.js");
            welcome.Welcome(message, null, args[1], null, args.slice(2).join(" "));
        }
        else if (arg0.toLowerCase() == "split"){
            var hasparse = parseInt(arg1)
            countup = hasparse;
            var msg = null;
            var c = args.slice(2).join(" ").split(" ");
            c.forEach( c => {
                if(msg == null) msg = `if(e=="${countup}"){return "${c}";}`;
                else msg = `${msg}\nelse if(e=="${countup}"){return "${c}";}`;
                countup++;
            });
            return message.channel.send("`"+msg+"`").catch(()=>{return message.channel.send("Rut gon emoji lai di =.=");})
        }
        else if (arg0.toLowerCase() == "user"){
            if (!arg1) return;
            if (arg1 == "add"){
                await db.User(message.author.id);
                await message.channel.send("Added.")
                await db.Save("User");
            }
            if (arg1 == "remove"){
                await db.User(message.author.id, "remove");
                await message.channel.send("Removed.")
                await db.Save("User");
            }
        }
        else return message.channel.send("Data from Command not Found!");
    }

    if(cmd === "s"){

        if(message.guild.id != 530689610313891840) return;

        if(message.channel.id != 631469262095122442) return;

        if(!args[0]) return message.channel.send(`${message.author.toString()}, không có lời gì để nhắn thì đừng dùng lệnh =.=`).then(msg => msg.delete(5000));

        const simkey = fs.readFileSync("./token/simsimi.txt", "utf8");

        var request = require('request');

        var headers = {
            'Content-Type': 'application/json',
            'x-api-key': simkey
        };
        
        var dataString = `{ "utext": "${args.join(" ")}", "lang": "vn", "atext_bad_prob_max": 0.0 }`;
        
        var options = {
            url: 'https://wsapi.simsimi.com/190410/talk',
            method: 'POST',
            headers: headers,
            body: dataString
        };
        
        request(options, function (error, response, body) {
            if (!error && response.statusCode == 200) {
                let mymsg = JSON.parse(body);
                message.channel.send(`${message.author.toString()}\n${mymsg.atext}`);

                let count = fs.readFileSync("./simsimicount.txt", "utf8");
                let parsecount = parseInt(count);
                let mathcount = Math.floor(parsecount + 1);
                fs.writeFile("./simsimicount.txt", mathcount.toString());
            }
        });
        
        //await message.channel.send(returnmsg).catch(err => message.channel.send(err));
    }

    //var c = message.guild.channels.cache.get("cc").memberPermissions(member.author).hasPermission("SEND_MESSAGES");













    // ======================================== W W O S I M U L A T I O N ========================================

    var wwosim = _client.guilds.cache.get("465795320526274561");
    var wwosimgs = _client.guilds.cache.get("472261911526768642");
    // Role ID.
    var player = "606123686633799680";
    var rankedwarn = "606123691889393705";
    var joining = "606123676668133428";
    var gamenarrator = "606123619999023114";
    var mininarrator = "606123620732895232";
    // Channel ID.
    var gamewarning = "606123818305585167";
    // User ID.
    var narratorbot = "470020987866578964";
    // Informations.

    if (message.guild.id == 465795320526274561){//WWO Sim Guild
        
        var user = message.author;
        
        // Commands.        
        if (cmd == "gwhost") {

            if (message.channel.id != 606123748738859008) return;
            if (!message.guild.members.cache.get(user.id).roles.cache.has(gamenarrator) & !message.guild.members.cache.get(user.id).roles.cache.has(mininarrator)) return message.channel.send("This Command require Game Narrator or Mini Narrator roles.");

            var check = message.guild.members.cache.get(narratorbot).presence.status;

            if (GlobalFunction.prototype.gamecode != null){
                await message.channel.send("Game Code `"+GlobalFunction.prototype.gamecode+"` is Hosting.");
            }
            else if(args[0].toLowerCase() == "manual") {
                GlobalFunction.prototype.gamecode = args.slice(1).join(" ");
                GlobalFunction.prototype.hoster = user.id;
                await message.guild.roles.cache.get(player).setMentionable(true);
                await message.guild.channels.cache.get(gamewarning).send(""+message.guild.roles.cache.get(player).toString()+", we are now starting game "+args.slice(1).join(" ")+"! Our host will be "+message.author.toString()+". Type `-join "+args.slice(1).join(" ")+"` to enter the game in <#606123821656702987>. If you don't want to get pinged for future games, go to <#606123783605977108> and click on the :video_game: Icon.");
                await message.guild.channels.cache.get(gamewarning).send("Note: Game will start with Manual.");
                await message.guild.roles.cache.get(player).setMentionable(false);

                await wwosimgs.channels.cache.get("606422958721859585").send("= = = = = S T A R T = = = = =");
            }
            else if (check == "offline")
            {
                await message.channel.send("Narrator Bot is Offline so you can't host at this time.");
            }
            else
            {
                GlobalFunction.prototype.gamecode = args.join(" ");
                GlobalFunction.prototype.hoster = user.id;
                await message.guild.roles.cache.get(player).setMentionable(true);
                await message.guild.channels.cache.get(gamewarning).send(""+message.guild.roles.cache.get(player).toString()+", we are now starting game "+args.join(" ")+"! Our host will be "+message.author.toString()+". Type `-join "+args.join(" ")+"` to enter the game in <#606123821656702987>. If you don't want to get pinged for future games, go to <#606123783605977108> and click on the :video_game: Icon.");
                await message.guild.roles.cache.get(player).setMentionable(false);

                await wwosimgs.channels.cache.get("606422958721859585").send("= = = = = S T A R T = = = = =");
            }
        }

        if (cmd == "rwhost") {

            if (message.channel.id != 606123748738859008) return;
            if (!message.guild.members.cache.get(user.id).roles.cache.has(gamenarrator)) return message.channel.send("This Command require Game Narrator role.");

            var check = message.guild.members.cache.get(narratorbot).presence.status;

            if (GlobalFunction.prototype.gamecode != null){
                await message.channel.send("Game Code `"+GlobalFunction.prototype.gamecode+"` is Hosting.");
            }
            
            else if(args[0].toLowerCase() == "manual") {
                let count = fs.readFileSync("./rankedseason.txt", "utf8");
                GlobalFunction.prototype.hoster = user.id;
                GlobalFunction.prototype.gamecode = `RS.${count}[${args.join(" ")}]`;
                await message.guild.roles.cache.get(rankedwarn).setMentionable(true);
                await message.guild.channels.cache.get(gamewarning).send(""+message.guild.roles.cache.get(rankedwarn).toString()+", we are now starting game "+GlobalFunction.prototype.gamecode+"! Our host will be "+message.author.toString()+". Type `-join "+GlobalFunction.prototype.gamecode+"` to enter the game in <#606123821656702987>. If you don't want to get pinged for future games, go to <#606123783605977108> and click on the :video_game: Icon.");
                await message.guild.channels.cache.get(gamewarning).send("Note: Game will start with Manual.");
                await message.guild.roles.cache.get(rankedwarn).setMentionable(false);

                await wwosimgs.channels.cache.get("606422958721859585").send("= = = = = S T A R T = = = = =");
            }
            else if (check == "offline")
            {
                await message.channel.send("Narrator Bot is Offline so you can't host at this time.");
            }
            else
            {
                let count = fs.readFileSync("./rankedseason.txt", "utf8");
                GlobalFunction.prototype.hoster = user.id;
                GlobalFunction.prototype.gamecode = `RS.${count}[${args.join(" ")}]`;
                await message.guild.roles.cache.get(rankedwarn).setMentionable(true);
                await message.guild.channels.cache.get(gamewarning).send(""+message.guild.roles.cache.get(rankedwarn).toString()+", we are now starting game "+GlobalFunction.prototype.gamecode+"! Our host will be "+message.author.toString()+". Type `-join "+GlobalFunction.prototype.gamecode+"` to enter the game in <#606123821656702987>. If you don't want to get pinged for future games, go to <#606123783605977108> and click on the :video_game: Icon.");
                await message.guild.roles.cache.get(rankedwarn).setMentionable(false);
            
                await wwosimgs.channels.cache.get("606422958721859585").send("= = = = = S T A R T = = = = =");
            }
        }

        if (cmd == "gwcancel" || cmd == "rwcancel"){
            if (message.channel.id != 606123748738859008) return;
            if (cmd == "rwcancel" & !message.guild.members.cache.get(user.id).roles.cache.has(gamenarrator)) return message.channel.send("This Command require Game Narrator role.");
            if (!message.guild.members.cache.get(user.id).roles.cache.has(gamenarrator) & !message.guild.members.cache.get(user.id).roles.cache.has(mininarrator)) return message.channel.send("This Command require Game Narrator or Mini Narrator roles.");
            if (GlobalFunction.prototype.gamecode == null) return message.channel.send("No Game Hosting at this time.");

            await message.guild.channels.cache.get(gamewarning).send("Game "+GlobalFunction.prototype.gamecode+" was cancelled, sorry for any inconvenience caused");
            GlobalFunction.prototype.gamecode = null;
            GlobalFunction.prototype.hoster = null;

            var alluser = message.guild.members;
            alluser.forEach(e => {
                if (e.roles.cache.has(joining)){
                    e.roles.remove(message.guild.roles.cache.get(joining));
                }
            });
        }

        if(cmd == "join"){
            if(message.guild.id != wwosim.id) return;
            if(GlobalFunction.prototype.gamecode == null) return message.channel.send("No Game Hosting at this time.");
            if(message.channel.id != 606123821656702987) return message.channel.send("This Command only work when use in <#606123821656702987>");
            var gc = ""+GlobalFunction.prototype.gamecode+"";
            if(gc.toLowerCase() != args.join(" ").toLowerCase()) return message.channel.send("Game Code to join is wrong! (Game code is `"+GlobalFunction.prototype.gamecode+"`)")

            message.guild.members.cache.get(message.author.id).roles.add(message.guild.roles.cache.get(joining));
            message.guild.channels.cache.get("606123824743841793").send(`${message.author.tag} joins match ${GlobalFunction.prototype.gamecode}\nUser ID: ${message.author.id}`);
        }

        if (cmd == "ss"){
        
            if(message.channel.id != 633343924693368865) return;
            if(!args[0]) return message.channel.send(`${message.author.toString()}, Do not use this command if you don't want to chat with me =.=`).then(msg => msg.delete(5000));
    
            const simkey = fs.readFileSync("./token/simsimi.txt", "utf8");
    
            var request = require('request');
    
            var headers = {
                'Content-Type': 'application/json',
                'x-api-key': simkey
            };
            
            var dataString = `{ "utext": "${args.join(" ")}", "lang": "en", "atext_bad_prob_max": 0.0 }`;
            
            var options = {
                url: 'https://wsapi.simsimi.com/190410/talk',
                method: 'POST',
                headers: headers,
                body: dataString
            };
            
            request(options, function (error, response, body) {
                if (!error && response.statusCode == 200) {
                    let mymsg = JSON.parse(body);
                    message.channel.send(`${message.author.toString()}\n${mymsg.atext}`);
    
                    let count = fs.readFileSync("./wwosimsimicount.txt", "utf8");
                    let parsecount = parseInt(count);
                    let mathcount = Math.floor(parsecount + 1);
                    fs.writeFile("./wwosimsimicount.txt", mathcount.toString());
                }
            });
            }    

    }

    if (message.guild.id == 472261911526768642){//WWO Sim Game Server Guild

        var user = message.author;

        if (cmd == "srole"){
            if (!message.guild.members.cache.get(user.id).hasPermission("MANAGE_ROLES")) return message.channel.send("This Command require ManageRole permission.");
            if (GlobalFunction.prototype.gamestatus == "hosting") return;
            
            GlobalFunction.prototype.gamestatus = "hosting";
            await message.channel.send("Game Start has been announced.");
            await wwosim.channels.cache.get("606123748738859008").send("Game Start has been announced.");
            await wwosim.channels.cache.get(gamewarning).send("Game now starting, you can no longer join.");
        }

        if (cmd == "end" || cmd == "nee"){            
            if (!message.guild.members.cache.get(user.id).hasPermission("MANAGE_ROLES")) return message.channel.send("This Command require ManageRole permission.");
            
            if (GlobalFunction.prototype.gamestatus == "hosting" & GlobalFunction.prototype.gamecode != null & GlobalFunction.prototype.won != null){
                await wwosim.channels.cache.get(gamewarning).send(`Game ${GlobalFunction.prototype.gamecode} ended - ${GlobalFunction.prototype.won} won the match!`);
                GlobalFunction.prototype.gamecode = null;
                GlobalFunction.prototype.won = null;
                GlobalFunction.prototype.gamestatus = null;

                var allusersim = wwosim.members;
                allusersim.forEach( e => {
                    if (wwosim.members.cache.get(e.id).roles.cache.has(joining)){
                        wwosim.members.cache.get(e.id).roles.remove(wwosim.roles.cache.get(joining));
                    }
                });

                makeannounce("start");
            }
            else if (GlobalFunction.prototype.gamestatus == "hosting" & GlobalFunction.prototype.gamecode != null & GlobalFunction.prototype.won == null){
                message.channel.send(`${message.author.toString()}, Can't Announce game end. (Team to win is Missing).\nAfter choose team to win, use -endt to announce game end.`);
            }
            else if (GlobalFunction.prototype.gamecode != null & GlobalFunction.prototype.won == null)
            {
                GlobalFunction.prototype.gamestatus = null;
            }
            else
            {
                GlobalFunction.prototype.gamecode = null;
                GlobalFunction.prototype.gamestatus = null;
                GlobalFunction.prototype.won = null;
                GlobalFunction.prototype.hoster = null;
            }
        }

        if (cmd == "endt"){
            if (!message.guild.members.cache.get(user.id).hasPermission("MANAGE_ROLES")) return message.channel.send("This Command require ManageRole permission.");
            if (GlobalFunction.prototype.gamecode == null) return message.channel.send("You need use me to host then command will work.");
            if (GlobalFunction.prototype.gamestatus != "hosting") return message.channel.send("Game is not Starting.");
            else if (GlobalFunction.prototype.gamestatus == "hosting" & GlobalFunction.prototype.gamecode != null & GlobalFunction.prototype.won != null)
            {
                await wwosim.channels.cache.get(gamewarning).send(`Game ${GlobalFunction.prototype.gamecode} ended - ${GlobalFunction.prototype.won} won the match!`);
                GlobalFunction.prototype.gamecode = null;
                GlobalFunction.prototype.won = null;
                GlobalFunction.prototype.gamestatus = null;

                var allusersim = wwosim.members;
                allusersim.forEach( e => {
                    if (wwosim.members.cache.get(e.id).roles.cache.has(joining)){
                        wwosim.members.cache.get(e.id).roles.remove(wwosim.roles.cache.get(joining));
                    }
                });
                makeannounce("start");
            }
            else return message.channel.send(`${message.author.toString()}, Choose team to won before use this Command.`);
        }

        if (cmd == "ends"){
            if (!message.guild.members.cache.get(user.id).hasPermission("MANAGE_ROLES")) return message.channel.send("This Command require ManageRole permission.");
            if (GlobalFunction.prototype.hoster == null) return;
            GlobalFunction.prototype.hoster = null;
            wwosimgs.channels.cache.get("606422958721859585").send("= = = = = = E N D = = = = = =");
            makeannounce("none");
        }

        if (cmd == "getplayers"){
            if (!message.guild.members.cache.get(user.id).hasPermission("MANAGE_ROLES")) return message.channel.send("This Command require ManageRole permission.");
            
            var counting = 1;
            var result = "";
            while(counting <= 16)
            {
                var letcount = ""+counting+"";
                var checkuser = await message.guild.members.find("nickname", ""+letcount+"");
                if (checkuser){
                    if (result == "") { result = `${letcount} - ${checkuser.user.tag}`; }
                    else { result = `${result}\n${letcount} - ${checkuser.user.tag}` }
                }
                counting++;
            }
            message.channel.send(result);
        }

        if (cmd == "vwin"){
            if (!message.guild.members.cache.get(user.id).hasPermission("MANAGE_ROLES")) return message.channel.send("This Command require ManageRole permission.");
            if(GlobalFunction.prototype.gamecode == null) return;
            if(GlobalFunction.prototype.gamestatus == null) return;

            GlobalFunction.prototype.won = "Village";
            message.guild.channels.cache.get("606132999389708330").send(`Game Over - ${GlobalFunction.prototype.won} won!`);
        }

        if (cmd == "wwin"){
            if (!message.guild.members.cache.get(user.id).hasPermission("MANAGE_ROLES")) return message.channel.send("This Command require ManageRole permission.");
            if(GlobalFunction.prototype.gamecode == null) return;
            if(GlobalFunction.prototype.gamestatus == null) return;

            GlobalFunction.prototype.won = "Werewolves";
            message.guild.channels.cache.get("606132999389708330").send(`Game Over - ${GlobalFunction.prototype.won} won!`);
        }

        if(cmd == "win"){
            if (!message.guild.members.cache.get(user.id).hasPermission("MANAGE_ROLES")) return message.channel.send("This Command require ManageRole permission.");
            if(GlobalFunction.prototype.gamecode == null) return;
            if(GlobalFunction.prototype.gamestatus == null) return;

            GlobalFunction.prototype.won = args.join(" ");
            message.guild.channels.cache.get("606132999389708330").send(`Game Over - ${GlobalFunction.prototype.won} won!`);
        }

    }//WWO Sim Game Server Guild

    if (message.guild.id == wwosim.id || message.guild.id == wwosimgs.id){
        if (cmd == "rs"){
            if (message.guild.id != wwosim.id & message.guild.id != wwosimgs.id) return;
            if (!wwosim.members.cache.get(message.author.id).hasPermission("ADMINISTRATOR")) return message.channel.send("This Command require Administrator permission in WWO Simulation.");
    
            var count = fs.readFileSync("./rankedseason.txt", "utf8");
            if (!args[0]) {
                const embed = new Discord.MessageEmbed()
                .addField("Ranked Game!", `Now is Season ${count}`)
                .setColor("#00FF00");
                message.channel.send(embed);
            }
            else {
                fs.writeFile("./rankedseason.txt", args.join(" "), ()=>{return;});
                const embed = new Discord.MessageEmbed()
                .addField("Ranked Game!", `Changed to Season ${args.join(" ")}`)
                .setColor("#00FF00");
                message.channel.send(embed);
            }
        }
    
        if (cmd == "kickasync") {
            if (!message.guild.members.cache.get(message.author.id).hasPermission("KICK_MEMBERS")) return message.channel.send("This Command require KickMember permission.");
            if (!args[0]) return;
            if (args[0].toLowerCase() != "yes") return;
    
            var counting = 16;
            message.channel.send("Kicking players with Nickname 16 to 1.");
            while(counting >= 1)
            {
                var letcount = ""+counting+"";
                var checkuser = message.guild.members.find("nickname", ""+letcount+"");
                if (checkuser){
                    checkuser.kick(`-kickasync by ${message.author.tag}`);             
                }
                counting--;
            }
            message.channel.send("Done.");
        }
    
        if (cmd == "recover"){
            if (message.guild.id != wwosim.id & message.guild.id != wwosimgs.id) return;
            if (!wwosim.members.cache.get(message.author.id).hasPermission("MANAGE_ROLES")) return message.channel.send("This Command require ManageRole permission.");
    
            if (!args[0]) {
                const embed = new Discord.MessageEmbed()
                .addField("Recover!\n-recover <Number> <Text>", `1 - Game Hoster (ID or Mention).\n2 - Game Code.\n3 - Team Won.\n4 - Game Status (Default when hosting is "hosting").`)
                .setColor("#00FF00");
                message.channel.send(embed);
            }
            else if (!args[1]){
                const embed = new Discord.MessageEmbed()
                    .addField("Error!", `Text is Missing.`)
                    .setColor("#FF0000");
                    message.channel.send(embed);
            }
            else{
                if (args[1].toLowerCase() == "null") {
                    if (args[0] == "1"){
                        GlobalFunction.prototype.hoster = null;
                        const embed = new Discord.MessageEmbed()
                            .addField("Done!", `Game Hoster now changed to null.`)
                            .setColor("#00FF00");
                            message.channel.send(embed);
                    }
                    else if (args[0] == "2"){
                        GlobalFunction.prototype.gamecode = null;
                        const embed = new Discord.MessageEmbed()
                            .addField("Done!", `Game Code now changed to null.`)
                            .setColor("#00FF00");
                            message.channel.send(embed);
                    }
                    else if (args[0] == "3"){
                        GlobalFunction.prototype.won = null;
                        const embed = new Discord.MessageEmbed()
                            .addField("Done!", `Team Won now changed to null.`)
                            .setColor("#00FF00");
                            message.channel.send(embed);
                    }
                    else if (args[0] == "4"){
                        GlobalFunction.prototype.gamestatus = null;
                        const embed = new Discord.MessageEmbed()
                            .addField("Done!", `Game Status now changed to null.`)
                            .setColor("#00FF00");
                            message.channel.send(embed);
                    }
                    else {
                        const embed = new Discord.MessageEmbed()
                            .addField("Error!", "`"+args[0]+"` from Number not Found.")
                            .setColor("#FF0000");
                            message.channel.send(embed);
                    }
                }
                else {
                    if (args[0] == "1"){
                        let getuser = message.guild.member(message.mentions.users.first() || message.guild.members.cache.get(args[1]) || message.guild.members.find("displayName", args.slice(1).join(" ")));
                        if (!getuser) { 
                            const embed = new Discord.MessageEmbed()
                            .addField("Error!", `Can't Recover!\nReason: User not Found.`)
                            .setColor("#FF0000");
                            message.channel.send(embed);
                        }
                        else {
                            GlobalFunction.prototype.hoster = getuser.id;
                            const embed = new Discord.MessageEmbed()
                            .addField("Done!", `Game Hoster now changed to ${getuser.toString()}`)
                            .setColor("#00FF00");
                            message.channel.send(embed);
                        }
                    }
                    else if (args[0] == "2"){
                        GlobalFunction.prototype.gamecode = args.slice(1).join(" ");
                        const embed = new Discord.MessageEmbed()
                            .addField("Done!", `Game Code now changed to ${args.slice(1).join(" ")}`)
                            .setColor("#00FF00");
                            message.channel.send(embed);
                    }
                    else if (args[0] == "3"){
                        GlobalFunction.prototype.won = args.slice(1).join(" ");
                        const embed = new Discord.MessageEmbed()
                            .addField("Done!", `Team Won now changed to ${args.slice(1).join(" ")}`)
                            .setColor("#00FF00");
                            message.channel.send(embed);
                    }
                    else if (args[0] == "4"){
                        if(args[1].toLowerCase() == "hosting") {
                            GlobalFunction.prototype.gamestatus = "hosting";
                            const embed = new Discord.MessageEmbed()
                            .addField("Done!", `Game Status now changed to hosting.`)
                            .setColor("#00FF00");
                            message.channel.send(embed);
                        }
                        else {
                            const embed = new Discord.MessageEmbed()
                            .addField("Error!", "Game Status only accept `hosting` or `null`.")
                            .setColor("#FF0000");
                            message.channel.send(embed);
                        }
                    }
                    else {
                        const embed = new Discord.MessageEmbed()
                            .addField("Error!", "`"+args[0]+"` from Number not Found.")
                            .setColor("#FF0000");
                            message.channel.send(embed);
                    }
                }
            }
        }
    
        if(cmd == "status"){
            if (message.guild.id != wwosim.id & message.guild.id != wwosimgs.id) return;
            if (!wwosim.members.cache.get(message.author.id).hasPermission("MANAGE_ROLES")) return message.channel.send("This Command require ManageRole permission.");
    
            var userr;
            if (GlobalFunction.prototype.hoster != null) { userr = message.guild.members.cache.get(GlobalFunction.prototype.hoster).toString(); }
            else userr = null;
    
            const embed = new Discord.MessageEmbed()
            .addField("Status of GlobalFunction - Running", `Game Hoster: ${userr}\nGame Code: ${GlobalFunction.prototype.gamecode}\nTeam Won: ${GlobalFunction.prototype.won}\nGame Status: ${GlobalFunction.prototype.gamestatus}`)
            .setColor("#00FFFF");
            message.channel.send(embed);
        }
    }
 
    // ============================================ E N D W W O S I M ============================================


});// E N D C L I E N T C O M M A N D

// C L A S S

class GlobalFunction { 
    constructor(msg) { //WWO Simulation.
        this.hoster = msg;
        this.gamecode = msg;
        this.gamestatus = msg;
        this.won = msg; 
    } //End WWO Simulation.
}

var timeannounce;
function getmakeannounce() {
    timeannounce = setTimeout(function(){
        _client.guilds.cache.get("472261911526768642").channels.cache.get("606422958721859585").send("= = = = = = E N D = = = = = ="); 
        GlobalFunction.prototype.hoster = null;
    }, 30000);
}
function makeannounce(msg) {
    if (msg == "start") return getmakeannounce();
    else return clearTimeout(timeannounce)
}