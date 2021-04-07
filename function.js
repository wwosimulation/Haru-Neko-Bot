const Discord = require("discord.js");
const crypto = require("crypto");

module.exports = {
    noPerm: function(message, perm){
        const embed = new Discord.MessageEmbed()
        .addField("Error!", "This command require permissions: `"+perm+"`.")
        .setColor("#FF0000");
        return message.channel.send(embed);
    },

    Error: function(message, perm){
        const embed = new Discord.MessageEmbed()
        .addField("Error!", ""+perm+"")
        .setColor("#FF0000");
        return message.channel.send(embed);
    },

    noPremium: function(message){
        const embed = new Discord.MessageEmbed()
        .addField("Error!", "This command require: `Guild Premium`.")
        .setColor("#FF0000");
        return message.channel.send(embed);
    },

    randomHex: function(n){
        if (n <= 0) {
            return '';
        }
        var rs = '';
        try {
            rs = crypto.randomBytes(Math.ceil(n/2)).toString('hex').slice(0,n);
            /* note: could do this non-blocking, but still might fail */
        }
        catch(ex) {
            /* known exception cause: depletion of entropy info for randomBytes */
            console.error('Exception generating random string: ' + ex);
            /* weaker random fallback */
            rs = '';
            var r = n % 8, q = (n-r)/8, i;
            for(i = 0; i < q; i++) {
                rs += Math.random().toString(16).slice(2);
            }
            if(r > 0){
                rs += Math.random().toString(16).slice(2,i);
            }
        }
        return rs;
    },

    formatDate: async function(date){
        
    }
}