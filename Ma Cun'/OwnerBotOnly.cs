using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Drawing;
using Neko_Test.Core.UserAccounts;
using Neko_Test.Core.UserAccounts10;
using Neko_Test.Core.Scores;
using System.Globalization;
using System.Windows.Forms;
using Neko_Test.ModulesMaCun;
using Neko_Test.Modules;
using Newtonsoft.Json;

using Discord.WebSocket;
using System.Diagnostics;

namespace Neko_Test.Ma_Cun_
{
    public class OwnerBotOnly : ModuleBase<SocketCommandContext>
    {
        Random rnd = new Random();

        [Command("console")]
        public async Task consoleforuseraccount(SocketUser User = null, int Number1 = 0, int Number2 = 0, ulong Number3 = 0)
        {
            if (Context.User.Id == 454492255932252160)
            {
                var embed = new EmbedBuilder();
                if (User == null)
                {
                    embed.AddField($"Console!", "-console (User) (Number1) (Number2) (Number3)");
                    embed.WithColor(new Discord.Color(255, 50, 255));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (Number1 == 0)
                {
                    embed.AddField($"Error!", "Number1 is Missing! / 1-Coin • 2-Roses • 3-PlrRoses • 4-BlockCommand.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (Number2 == 0)
                {
                    embed.AddField($"Error!", "Number2 is Missing! / 1-Add • 2-Remove");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (Number3 == 0 & Number1 != 4)
                {
                    embed.AddField($"Error!", "Number3 is Missing! / Context Number");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    var user = UserAccounts.GetAccount(User);
                    if (Number2 == 1)
                    {
                        if (Number1 == 1)
                        {
                            user.points = user.points + Number3;
                            embed.AddField($"Console!", "Added " + Number3 + " Coins to Account " + User.Username + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        if (Number1 == 2)
                        {
                            user.roses = user.roses + Number3;
                            embed.AddField($"Console!", "Added " + Number3 + " Roses to Account " + User.Username + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        if (Number1 == 3)
                        {
                            user.plrroses = user.plrroses + Number3;
                            embed.AddField($"Console!", "Added " + Number3 + " Plr Roses to Account " + User.Username + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                    }
                    if (Number2 == 2)
                    {
                        if (Number1 == 1)
                        {
                            user.points = user.points - Number3;
                            embed.AddField($"Console!", "Removed " + Number3 + " Coins to Account " + User.Username + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        if (Number1 == 2)
                        {
                            user.roses = user.roses - Number3;
                            embed.AddField($"Console!", "Removed " + Number3 + " Roses to Account " + User.Username + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        if (Number1 == 3)
                        {
                            user.plrroses = user.plrroses - Number3;
                            embed.AddField($"Console!", "Removed " + Number3 + " Plr Roses to Account " + User.Username + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                    }
                    UserAccounts.SaveAccounts();
                }
            }
            else return;
        }



        [Command("PM")]
        public async Task PMSOMEONE(IGuildUser user = null, [Remainder] string text = null)
        {
            var embed = new EmbedBuilder();
            if (Context.User.Id == 454492255932252160)
            {
                if (user == null)
                {
                    embed.AddField($"Error!", "User is Missing.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (text == null)
                {
                    embed.AddField($"Error!", "Text is Missing.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    await Context.Client.GetUser(user.Id).SendMessageAsync("" + text + "");
                }
            }
            else return;
        }

        [Command("perm")]
        public async Task permissionforuser(SocketUser User = null, int Number1 = 0, int Number2 = 0)
        {
            if (Context.User.Id == 454492255932252160)
            {
                var embed = new EmbedBuilder();
                if (User == null)
                {
                    embed.AddField($"Permission!", "-perm (User) (Number1) (Number2)");
                    embed.WithColor(new Discord.Color(255, 50, 255));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (Number1 == 0)
                {
                    embed.AddField($"Error - Number1 is Missing!", "1 - Emote.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    var config = UserAccounts10.GetAccount(User);
                    if (Number1 == 1)
                    {
                        if (Number2 == 0)
                        {
                            embed.AddField($"Error - Number2 is Missing!", "1 - Allow.\n2 - Not Allow.");
                            embed.WithColor(new Discord.Color(255, 0, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        if (Number2 == 1)
                        {
                            config.emote = true;
                            embed.AddField($"Permission!", "Allowed " + User.Username + " to use Emotes.");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        if (Number2 == 2)
                        {
                            config.emote = false;
                            embed.AddField($"Permission!", "Removed Allow " + User.Username + " to use Emotes.");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                    }
                    UserAccounts10.SaveAccounts();
                }
            }
            else return;
        }

        [Command("commandperm")]
        public async Task CommandPermission(int Number1 = 0, int Number2 = 0)
        {
            if (Context.User.Id == 454492255932252160)
            {
                var embed = new EmbedBuilder();
                if (Number1 == 0)
                {
                    embed.AddField($"Command Permission!", "-commandperm (Number1) (Number2)\n1 - Command Werewolf Online Vietnam.");
                    embed.WithColor(new Discord.Color(255, 50, 255));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (Number2 == 0)
                {
                    embed.AddField($"Error!", "Number2 is Missing! / 1-Allow • 2-Deny.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    if (Number2 == 1)
                    {
                        if (Number1 == 1)
                        {
                            GlobalFunctionMaCun.blockcommand = true;
                            embed.AddField($"Command Permission!", "Commands now are Blocked.");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                    }
                    if (Number2 == 2)
                    {
                        if (Number1 == 1)
                        {
                            GlobalFunctionMaCun.blockcommand = false;
                            embed.AddField($"Command Permission!", "Commands now are Un-Blocked.");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                    }
                }
            }
            else return;
        }

        [Command("cam")]
        public async Task bannedplayerfromwerewolfgame(SocketUser User = null, [Remainder] string Reason = null)
        {
            if (Context.User.Id == 454492255932252160)
            {
                var embed = new EmbedBuilder();
                if (User == null)
                {
                    embed.AddField($"Error!", "User to Ban is Missing.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (Reason == null)
                {
                    embed.AddField($"Error!", "Reason to Ban is Missing.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    var user = UserAccounts10.GetAccount(User);
                    if (user.None1 == null)
                    {
                        user.None1 = Reason;
                        embed.AddField($"Banned!", "Ban Successfully!\nUser name: " + User.Username + "\nUser ID: " + User.Id + "\nReason: " + Reason + "");
                        embed.WithColor(new Discord.Color(0, 255, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else
                    {
                        embed.AddField($"Error!", "That User is already banned.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    UserAccounts10.SaveAccounts();
                }
            }
            else if (Context.Client.GetGuild(530689610313891840).GetUser(Context.User.Id).Roles.Any(x => x.Name == "Admin"))
            {
                var embed = new EmbedBuilder();
                if (User == null)
                {
                    embed.AddField($"Error!", "User to Ban is Missing.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (Reason == null)
                {
                    embed.AddField($"Error!", "Reason to Ban is Missing.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    var user = UserAccounts10.GetAccount(User);
                    if (user.None1 == null)
                    {
                        user.None1 = Reason;
                        embed.AddField($"Banned!", "Ban Successfully!\nUser name: " + User.Username + "\nUser ID: " + User.Id + "\nReason: " + Reason + "");
                        embed.WithColor(new Discord.Color(0, 255, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else
                    {
                        embed.AddField($"Error!", "That User is already banned.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    UserAccounts10.SaveAccounts();
                }
            }
            else return;
        }


        [Command("bocam")]
        public async Task unbannedplayerfromwerewolfgame(SocketUser User = null)
        {
            if (Context.User.Id == 454492255932252160)
            {
                var embed = new EmbedBuilder();
                if (User == null)
                {
                    embed.AddField($"Error!", "User to Un-Ban is Missing.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    var user = UserAccounts10.GetAccount(User);
                    if (user.None1 != null)
                    {
                        user.None1 = null;
                        embed.AddField($"Un-Banned!", "Un-Ban Successfully!\nUser name: " + User.Username + "\nUser ID: " + User.Id + "");
                        embed.WithColor(new Discord.Color(0, 255, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else
                    {
                        embed.AddField($"Error!", "That User is not banned.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    UserAccounts10.SaveAccounts();
                }
            }
            else if (Context.Client.GetGuild(530689610313891840).GetUser(Context.User.Id).Roles.Any(x => x.Name == "Admin"))
            {
                var embed = new EmbedBuilder();
                if (User == null)
                {
                    embed.AddField($"Error!", "User to Un-Ban is Missing.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    var user = UserAccounts10.GetAccount(User);
                    if (user.None1 != null)
                    {
                        user.None1 = null;
                        embed.AddField($"Un-Banned!", "Un-Ban Successfully!\nUser name: " + User.Username + "\nUser ID: " + User.Id + "");
                        embed.WithColor(new Discord.Color(0, 255, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else
                    {
                        embed.AddField($"Error!", "That User is not banned.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    UserAccounts10.SaveAccounts();
                }
            }
            else return;
        }

        [Command("transfer")]
        public async Task tranferforuseraccount(SocketUser User1 = null, SocketUser User2 = null)
        {
            if (Context.User.Id == 454492255932252160)
            {
                var embed = new EmbedBuilder();
                if (User1 == null)
                {
                    embed.AddField($"Transfer!", "-transfer (User1) (User2)");
                    embed.WithColor(new Discord.Color(255, 50, 255));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (User2 == null)
                {
                    embed.AddField($"Error!", "User2 is Missing!");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    var user1 = UserAccounts.GetAccount(User1);
                    var user2 = UserAccounts.GetAccount(User2);

                    user2.points = user2.points + user1.points;
                    user2.roses = user2.roses + user1.roses;
                    user2.plrroses = user2.plrroses + user1.plrroses;

                    user1.points = 0;
                    user1.roses = 0;
                    user1.plrroses = 0;

                    embed.AddField($"Transfer Successfully!", $"Transfered {Context.Client.GetUser(user1.ID).Username} to Account {Context.Client.GetUser(user2.ID).Username}");
                    embed.WithColor(new Discord.Color(0, 255, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());

                    UserAccounts.SaveAccounts();
                }
            }
            else return;
        }

        [Command("removeaccount")]
        public async Task removeforuseraccount(SocketUser User = null)
        {
            if (Context.User.Id == 454492255932252160)
            {
                var embed = new EmbedBuilder();
                if (User == null)
                {
                    embed.AddField($"Transfer!", "-removeaccount (User1)");
                    embed.WithColor(new Discord.Color(255, 50, 255));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    var user1 = UserAccounts.GetAccount(User);
                    user1.points = 0;
                    user1.roses = 0;
                    user1.plrroses = 0;

                    embed.AddField($"Removed Successfully!", $"Removed Account {Context.Client.GetUser(user1.ID).Username}");
                    embed.WithColor(new Discord.Color(0, 255, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());

                    UserAccounts.SaveAccounts();
                }
            }
            else return;
        }


        [Command("choose")]
        public async Task chatsgetfromglobalfunction(string name, ulong id)
        {
            if (Context.User.Id == 454492255932252160)
            {
                if (id == 1)
                {
                    if (name.ToLower() == "guild")
                    {
                        GlobalFunctionMaCun.guildid = Context.Guild.Id;
                        await ReplyAsync("Done.");
                    }
                    else if (name.ToLower() == "channel")
                    {
                        GlobalFunctionMaCun.channelid = Context.Channel.Id;
                        await ReplyAsync("Done.");
                    }
                    else await ReplyAsync("Guild or Channel.");
                }
                else
                {
                    if (name.ToLower() == "guild")
                    {
                        var check = Context.Client.GetGuild(id);
                        if (check == null)
                        {
                            await ReplyAsync("Error, Guild not found.");
                        }
                        else
                        {
                            GlobalFunctionMaCun.guildid = id;
                            await ReplyAsync("Done.");
                        }
                    }
                    else if (name.ToLower() == "channel")
                    {
                        if (GlobalFunctionMaCun.guildid == 0)
                        {
                            await ReplyAsync("Guild is Missing.");
                        }
                        else
                        {
                            var check = Context.Client.GetGuild(GlobalFunctionMaCun.guildid).GetTextChannel(id);
                            if (check == null)
                            {
                                await ReplyAsync("Error, Channel not found from Guild " + Context.Client.Guilds.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.guildid).Name + ".");
                            }
                            else
                            {
                                GlobalFunctionMaCun.channelid = id;
                                await ReplyAsync("Done.");
                            }
                        }
                    }
                }
            }
            else return;
        }

        [Command("chats")]
        public async Task chatsfromglobalfunction([Remainder]string text = null)
        {
            if (Context.User.Id == 454492255932252160)
            {
                if (GlobalFunctionMaCun.guildid == 0)
                {
                    await ReplyAsync("Guild is Missing.");
                }
                else if (GlobalFunctionMaCun.channelid == 0)
                {
                    await ReplyAsync("Channel is Missing.");
                }
                else if (text == null)
                {
                    await ReplyAsync("Text is Missing.");
                }
                else
                {
                    string msg = text;
                    int checknum = GlobalFunction.MaxEmotes;
                    while (checknum >= 1)
                    {
                        var ab = $"{checknum}";
                        var ac = "-" + checknum + "";
                        GlobalFunction.emotesstring(ab);
                        msg = msg.Replace($"{ac}", $"{GlobalFunction.emote}");
                        checknum--;
                    }
                    await Context.Client.GetGuild(GlobalFunctionMaCun.guildid).GetTextChannel(GlobalFunctionMaCun.channelid).SendMessageAsync(msg);
                }
            }
            else return;
        }


        [Command(".")]
        public async Task testtsomething([Remainder]string text = null)
        {
            if (Context.User.Id == 454492255932252160)
            {
                string line = null;
                var texts = text.Split(" ");
                foreach (var t in texts)
                {
                    line = $"{line}\n{t}";
                }
                await ReplyAsync($"{line}");
            }
            else return;
        }

        [Command("..")]
        public async Task a([Remainder]string text = null)
        {
            if (Context.User.Id == 454492255932252160)
            {
                string line = null;
                var texts = text.Split(" ");
                foreach (var t in texts)
                {
                    await GlobalFunctionMaCun.rolestring(t, "ten");
                    line = $"{line}\n{GlobalFunctionMaCun.nameroles}";
                }
                await ReplyAsync($"{line}");
            }
            else return;
        }
        [Command("...")]
        public async Task absas()
        {
            if (Context.User.Id == 454492255932252160)
            {
                var embed = new EmbedBuilder();
                string ga = null;
                embed.AddField("Giáo Xứ!", $"{ga}");
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
            else return;
        }

        [Command("-")]
        public async Task testtsomething2([Remainder]SocketChannel channel = null)
        {
            if (Context.User.Id == 454492255932252160)
            {
                string line = null;
                line = $"{channel}";
                await ReplyAsync($"{line}");
            }
            else return;
        }


        [Command("line")]
        public async Task linees()
        {
            string c = null;
            if (c == null)
            {
                if (c == null)
                {

                  

                }
            }
        }

        [Command("iamdev")]
        [RequireBotPermission(Discord.GuildPermission.ManageRoles)]
        public async Task devrole()
        {
            if (Context.Guild.Id == 580555457983152149 & Context.User.Id == 454492255932252160)
            {
                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Bot Dev").Members.Contains(Context.User))
                {
                    await (Context.User as IGuildUser).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Bot Dev"));
                    await ReplyAsync($"{Context.User.Username}-Sama, Bot Dev role has been removed.");
                }
                else
                {
                    await (Context.User as IGuildUser).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Bot Dev"));
                    await ReplyAsync($"{Context.User.Username}-Sama, Bot Dev role has been added.");
                }
            }
            else return;
        }

        [Command("sorttop")]
        public async Task sortelementfortop(string optional = null, [Remainder] string text = null)
        {
            if (Context.User.Id != 454492255932252160)
            {
                return;
            }
            else if (optional == null)
            {
                await ReplyAsync($"Optional is Missing!\n[Optionals: firstlast, lastfirst]");
            }
            else if (text == null)
            {
                if (optional.ToLower() == "sort")
                {
                    var abc = File.ReadAllText("Scores.json");
                    var another = JsonConvert.DeserializeObject<List<Score>>(abc);
                    var result = another.OrderByDescending(x => x.diem).ToArray();
                    int bla = another.Count();
                    int bal = 0;
                    int lol = 0;
                    string abb = null;
                    while (bla > 27)
                    {
                        bal++;
                        abb = $"{abb}\nTop {bal} - STT: {result[lol].ID} - {result[lol].diem}";
                        bla--;
                        lol++;
                    }
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.AddField("Bảng Xếp Hạng lớp 11A17", $"{abb}");
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (optional.ToLower() == "sort2")
                {
                    var abc = File.ReadAllText("Scores.json");
                    var another = JsonConvert.DeserializeObject<List<Score>>(abc);
                    var result = another.OrderByDescending(x => x.diem).ToArray();
                    int bla = another.Count();
                    int bal = 27;
                    int lol = 27;
                    string abb = null;
                    while (bla > 27)
                    {
                        bal++;
                        abb = $"{abb}\nTop {bal} - STT: {result[lol].ID} - {result[lol].diem}";
                        bla--;
                        lol++;
                    }
                    EmbedBuilder embed = new EmbedBuilder();
                    embed.AddField("Bảng Xếp Hạng lớp 11A17", $"{abb}");
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else await ReplyAsync("Text to Add Sort Element is Missing!");
            }
            else
            {
                if (optional.ToLower() == "new")
                {
                    ulong count = 0;
                    string[] texts = text.Split("-");
                    foreach (var x in texts)
                    {
                        count++;
                        var scores = Scores.GetAccount(count);
                        var getscore = Double.Parse(x);
                        scores.diem = getscore;
                    }
                    await ReplyAsync("Added!");
                }
                Scores.SaveAccounts();
            }
        }

        [Command("sortsingle")]
        public async Task sortelementssinglecode([Remainder]string text = null)
        {
            if (text == null)
            {
                await ReplyAsync("Text to Sort Elements is Missing!");
            }
            else
            {
                List<double> list = new List<double>();
                int top = 0;
                string result = null;
                string[] texts = text.Split("-");
                foreach(var x in texts)
                {
                    double parsedouble = Double.Parse(x);
                    list.Add(parsedouble);
                }
                list.Sort();
                int count = list.Count();
                while (count > 0)
                {
                    top++;
                    result = $"{result}\nTop {top} - {list[count - 1]}";
                    count--;
                }
                await ReplyAsync($"{result}");
            }
        }

        [Command("textchange")]
        public async Task testsomethingsss([Remainder]string c = null)
        {
            if (c == null)
            {
                await ReplyAsync("Nothing to change.");
            }
            else
            {
                int call = 0;
                string name = null;
                while (call < 51)
                {
                    call++;
                    string nameget = c;
                    string newname = nameget.Replace("{count}", $"{call}");
                    if (name == null) name = newname;
                    else name = $"{name}\n{newname}";
                    if (call == 25)
                    {
                        await ReplyAsync($"{name}");
                        name = null;
                    }
                    else if (call == 50)
                    {
                        await ReplyAsync($"{name}");
                    }
                }
            }
        }

        [Command("textchange2")]
        public async Task testsomethingsss2([Remainder]string c = null)
        {
            if (c == null)
            {
                await ReplyAsync("Nothing to change.");
            }
            else
            {
                int call = 0;
                string name = null;
                while (call < 51)
                {
                    call++;
                    string nameget = c;
                    string newname = nameget.Replace("{count}", $"{call}");
                    if (name == null) name = newname;
                    else name = $" {name} || {newname}";

                    if (call == 25)
                    {
                        await ReplyAsync($"`if ({name}`");
                        name = null;
                    }
                    else if (call == 50)
                    {
                        await ReplyAsync($"`{name})`");
                    }
                }
            }
        }

        /*[Command("kickall")]
        [RequireBotPermission(Discord.GuildPermission.KickMembers)]
        public async Task enmeyharder()
        {
            if (Context.Guild.Id == 580555457983152149 & Context.User.Id == 454492255932252160)
            {
                await ReplyAsync("Follow Command of Master, I'll kick all player in here.");
                ulong num = 10000000000000000000;
                while (num > 0)
                {
                    if (Context.Guild.Users.FirstOrDefault(x => x.Id == num) != null)
                    {
                        if (!Context.Guild.GetUser(num).Roles.Any(x => x.Name == "Quản Trò"))
                        {
                            var users = Context.Guild.GetUser(num);
                            await users.KickAsync();
                        }
                    }
                    num--;
                }
                await ReplyAsync("I'm Done.");
            }
            else return;
        }*/

        /*[Command("rainbow")]
         private async Task rainbowroles([Remainder] IRole roles = null)
         {
             var embed = new EmbedBuilder();
             SocketGuildUser User1 = Context.User as SocketGuildUser;
             if (!User1.GuildPermissions.ManageRoles)
             {
                 embed.AddField($"Error!", "Permission is Missing (ManageRoles Permission).");
                 embed.WithColor(new Discord.Color(255, 0, 0));
                 await Context.Channel.SendMessageAsync("", false, embed.Build());
             }
             else if (roles == null)
             {
                 embed.AddField($"Error!", "Name of Roles to make Rainbow is Missing.");
                 embed.WithColor(new Discord.Color(255, 0, 0));
                 await Context.Channel.SendMessageAsync("", false, embed.Build());
             }
             else
             {
                 await ReplyAsync("Done! " + Context.User.Mention + " The program will continue for about forever changing the color of role for 1 time per 15 seconds!");

                 for (int i = 999999999; i > 0; i--)
                 {
                     int R = rnd.Next(0, 255);
                     int G = rnd.Next(0, 255);
                     int B = rnd.Next(0, 255);

                     Discord.Color newcolor = new Discord.Color(R, G, B);
                     await roles.ModifyAsync(a => a.Color = newcolor);
                     await Task.Delay(15000);
                 }
             }
         }*/

    }
}
