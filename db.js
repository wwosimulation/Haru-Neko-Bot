const fs = require("fs");

const GuildInfo = JSON.parse(fs.readFileSync("./dbjson/GuildInfo.json", "utf8"));
const GuildBackground = JSON.parse(fs.readFileSync("./dbjson/GuildBackground.json", "utf8"));
const GuildWelcome = JSON.parse(fs.readFileSync("./dbjson/GuildWelcome.json", "utf8"));
const User = JSON.parse(fs.readFileSync("./dbjson/User.json", "utf8"));
const RedeemCode = JSON.parse(fs.readFileSync("./dbjson/RedeemCode.json", "utf8"));

module.exports = {
    GuildInfo: function(id, func, value){
        if (!GuildInfo[id]){
            GuildInfo[id] = {};
            GuildInfo[id].prefix = "-";
            GuildInfo[id].perm = "No"
            GuildInfo[id].permtimeleft = 0;
            GuildInfo[id].money = 0;
            this.Save("GuildInfo");
        }
        if (!value){
            if (func == "prefix") return GuildInfo[id].prefix;
            if (func == "perm") return GuildInfo[id].perm;
            if (func == "permtimeleft") return GuildInfo[id].permtimeleft;
            if (func == "money") return GuildInfo[id].money;
        }
        else if (value) {
            if (value == "0"){
                if (func == "perm") return GuildInfo[id].perm = parseInt(value);
                if (func == "permtimeleft") return GuildInfo[id].permtimeleft = parseInt(value);
                if (func == "money") return GuildInfo[id].money = parseInt(value);
                return;
            }
            if (func == "prefix") return GuildInfo[id].prefix = value;
            if (func == "perm") return GuildInfo[id].perm = value;
            if (func == "permtimeleft") return GuildInfo[id].permtimeleft = value;
            if (func == "money") return GuildInfo[id].money = value;
        }
        else return id;
    },

    GuildBackground: function(id, func, value){
        if (!GuildBackground[id]){
            GuildBackground[id] = {};
            GuildBackground[id].bg = 0;
            GuildBackground[id].color = 0;
            GuildBackground[id].font = 0;
            GuildBackground[id].size = 0;
            this.Save("GuildBackground");
        }
        if (!value){
            if (func == "bg") return GuildBackground[id].bg;
            if (func == "color") return GuildBackground[id].color;
            if (func == "font") return GuildBackground[id].font;
            if (func == "size") return GuildBackground[id].size;
        }
        else if (value) {
            if (value == "0"){
                if (func == "bg") return GuildBackground[id].bg = parseInt(value);
                if (func == "color") return GuildBackground[id].color = parseInt(value);
                if (func == "font") return GuildBackground[id].font = parseInt(value);
                if (func == "size") return GuildBackground[id].size = parseInt(value);
                return;
            }
            if (func == "bg") return GuildBackground[id].bg = value;
            if (func == "color") return GuildBackground[id].color = value;
            if (func == "font") return GuildBackground[id].font = value;
            if (func == "size") return GuildBackground[id].size = value;
        }
        else return id;
    },

    GuildWelcome: function(id, func, value){
        if (!GuildWelcome[id]){
            GuildWelcome[id] = {};
            GuildWelcome[id].type = 0;
            GuildWelcome[id].msg = 0;
            GuildWelcome[id].channel = 0;
            GuildWelcome[id].infobackgrounds = 0;
            GuildWelcome[id].dm = 0;
            GuildWelcome[id].dmtype = 0;
            this.Save("GuildWelcome");
        }
        if (!value){
            if (func == "type") return GuildWelcome[id].type;
            if (func == "msg") return GuildWelcome[id].msg;
            if (func == "channel") return GuildWelcome[id].channel;
            if (func == "infobackgrounds") return GuildWelcome[id].infobackgrounds;
            if (func == "dm") return GuildWelcome[id].dm;
            if (func == "dmtype") return GuildWelcome[id].dmtype;
        }
        else if (value) {
            if (value == "0"){
                if (func == "type") return GuildWelcome[id].type = parseInt(value);
                if (func == "msg") return GuildWelcome[id].msg = parseInt(value);
                if (func == "channel") return GuildWelcome[id].channel = parseInt(value);
                if (func == "infobackgrounds") return GuildWelcome[id].infobackgrounds = parseInt(value);
                if (func == "dm") return GuildWelcome[id].dm = parseInt(value);
                if (func == "dmtype") return GuildWelcome[id].dmtype = parseInt(value);
                return;
            }
            if (func == "type") return GuildWelcome[id].type = value;
            if (func == "msg") return GuildWelcome[id].msg = value;
            if (func == "channel") return GuildWelcome[id].channel = value;
            if (func == "infobackgrounds") return GuildWelcome[id].infobackgrounds = value;
            if (func == "dm") return GuildWelcome[id].dm = value;
            if (func == "dmtype") return GuildWelcome[id].dmtype = value;
        }
        else return id;
    },

    User: function(id, func, value){
        if (!User[id]){
            User[id] = {};
            User[id].registeredat = Date.now();
            User[id].money = 0;
            User[id].emojiown = 0;
            this.Save("User");
        }
        else {
            if(User[id].registeredat == 0) User[id].registeredat = Date.now();
            this.Save("User");
        }
        if (!value){
            if(func == "registeredat") return User[id].registeredat;
            if(func == "money") return User[id].money;
            if(func == "emojiown") return User[id].emojiown;
        }
        else if (value) {
            if (value == "0"){
                if(func == "registeredat") return User[id].registeredat = parseInt(value);
                if(func == "money") return User[id].money = parseInt(value);
                if(func == "emojiown") return User[id].emojiown = parseInt(value);
                return;
            }
            if(func == "registeredat") return User[id].registeredat = value;
            if(func == "money") return User[id].money = value;
            if(func == "emojiown") return User[id].emojiown = value;
        }
        else return id;
    },

    RedeemCode: function(code, func, itemnames, itemvalues, messages){
        if(func == "add"){
            {
                RedeemCode[code] = {};
                RedeemCode[code].itemname = itemnames;
                RedeemCode[code].itemvalue = itemvalues;
                RedeemCode[code].message = messages;
                this.Save("RedeemCode");
            }
        }
        else if(func == "remove"){
            if(!RedeemCode[code]) return "Error1";
            else {
                delete RedeemCode[code];
                this.Save("RedeemCode")
            }
        }
        else if(func == "redeem"){
            if(!RedeemCode[code]) return "Error1";
            else {
                if(itemnames == "1") return RedeemCode[code].itemname;
                if(itemnames == "2") return RedeemCode[code].itemvalue;
                if(itemnames == "3") return RedeemCode[code].message;
                if(itemnames == "delete") {delete RedeemCode[code]; this.Save("RedeemCode");}
            }
        }
        else throw new Error("Only accept `add` or `remove`.")
    },

    Save: function(file){
        if (file == "GuildInfo"){
            fs.writeFile(`./dbjson/GuildInfo.json`, JSON.stringify(GuildInfo), (err => {
                console.log(err);
            }));
        }
        else if (file == "GuildBackground"){
            fs.writeFile(`./dbjson/GuildBackground.json`, JSON.stringify(GuildBackground), (err => {
                console.log(err);
            }));
        }
        else if (file == "GuildWelcome"){
            fs.writeFile(`./dbjson/GuildWelcome.json`, JSON.stringify(GuildWelcome), (err => {
                console.log(err);
            }));
        }
        else if (file == "User"){
            fs.writeFile(`./dbjson/User.json`, JSON.stringify(User), (err => {
                console.log(err);
            }));
        }
        else if (file == "RedeemCode"){
            fs.writeFile(`./dbjson/RedeemCode.json`, JSON.stringify(RedeemCode), (err => {
                console.log(err);
            }));
        }
        else return;
    }
}