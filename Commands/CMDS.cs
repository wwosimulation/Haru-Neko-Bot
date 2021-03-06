using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Discord.Addons.Interactive;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.IO;
using Neko_Test.Modules;

namespace Neko_Test
{
    public class CMDS : ModuleBase<SocketCommandContext>
    {
        Random rnd = new Random();

        public IGuildUser Users { get; private set; }

        [Command("cmds"), Alias("CommandList")]
        [RequireBotPermission(Discord.GuildPermission.EmbedLinks)]
        public async Task ListCommand()
        {
            var embed = new EmbedBuilder();
            if (Context.Guild.Id == 516128488999354368)
            {
                embed.AddField($"Lỗi!", "Đéo Có Lệnh Để Show :)))");
                embed.WithColor(new Discord.Color(255, 50, 255));
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
            else
            {
                embed.WithAuthor($"Neko's Command. \n \n");
                //embed.WithAuthor($"=== WWO Simulation ===\n");
                embed.AddField($"WWO Simulation - Commands: -gwhost, -gwcancel, -rwhost, -rwcancel", "Usage: -gwhost <Game Code> to start Host Game (Require: Game Narrator or Mini Narrator role)\n       -gwcancel to cancel Game (Require: Game Narrator or Mini Narrator role) \n       -rwhost <Game Code> to start Host Rank Game (Require: Game Narrator role)\n       -rwcancel to cancel Rank Game (Require: Game Narrator role)");
                //embed.WithTitle($"=== Game Server ===");
                embed.AddField($"Game Server - Commands: -gnhelper, -gn, -dj", "Usage: -gnhelper to get Mini Narrator role (Require: Mini Narrator role in WWO Simulation)\n       -gn to get GN role (Require: Game Narrator role in WWO Simulation)\n       -dj to get DJ role (Require: DJ role in WWO - Simulation)");
                //embed.WithTitle($"=== Another Commands ===");
                embed.AddField($"Another Commands: -recover, -change, -reset, -stats, -clr", "Usage: -recover <Num> <Text> to change Game's Info (Require: ManageRoles Permission)\n       -change <User> <New Nickname> to change Nick Name of User (Require: ManageNicknames Permission)\n       -reset to reset Status Running in GlobalFunction (Require: Game Narrator or Mini Narrator role in WWO Simulation)\n       -stats to show Status Running in GlobalFunction (Require: Game Narrator or Mini Narrator role in WWO Simulation)\n       -clr to Bulk Delete Messages channel (Require: ManageMessages Permission)");
                embed.AddField($"More Soon", "...");

                int Re = rnd.Next(0, 255);
                int Ge = rnd.Next(0, 255);
                int Be = rnd.Next(0, 255);

                embed.WithColor(new Discord.Color(255, 50, 255));

                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
        }

        [Command("srole")]
        [Alias("testsrole")]
        [RequireBotPermission(Discord.GuildPermission.ManageGuild)]
        public async Task morning(string gamemode = null, [Remainder] string check = null)
        {
            gamemode.ToLower();
            SocketGuildUser User1 = Context.User as SocketGuildUser;
            if (Context.Guild.Id != 472261911526768642)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in WWO Simulation - Game Server!");
                return;
            }
            else if (!User1.GuildPermissions.ManageRoles)
            {
                await Context.Channel.SendMessageAsync("Your Permissions is Missing (ManageRole Permission).");
                return;
            }
            else if (GlobalFunction.gamestatus == "hosting")
            {
                await Context.Client.SetGameAsync("Game Started");
                return;
            }
            else if (gamemode == "classic" & check == null || gamemode == "sandbox" & check == null || gamemode == "random" & check != null || gamemode == "Classic" & check == null || gamemode == "Sandbox" & check == null || gamemode == "Random" & check != null)
            {
                /*HttpClient dude = new HttpClient();
                var clonestream = new MemoryStream();
                Stream stream = await dude.GetStreamAsync("https://cdn.discordapp.com/attachments/470026969963167767/474664582850543616/20180802_164341.jpg");
                stream.CopyTo(clonestream);
                Image pic = new Image(clonestream);
                clonestream.Position = 0;
                await Context.Guild.ModifyAsync(x => x.Icon = pic);*/
                await Context.Client.GetGuild(465795320526274561).GetTextChannel(606123818305585167).SendMessageAsync("Game now starting, you can no longer join.");
                await Context.Client.GetGuild(465795320526274561).GetTextChannel(606123748738859008).SendMessageAsync("Game Start has been announced.");
                await Context.Client.GetGuild(472261911526768642).GetTextChannel(606131484532801549).SendMessageAsync("Game Start has been announced.");
                GlobalFunction.gamestatus = "hosting";
                GlobalFunction.gametime = "night";
                await Context.Client.SetGameAsync("Game Started");
            }
        }



        [Command("night")]
        [Alias("testnight")]
        [RequireBotPermission(Discord.GuildPermission.ManageGuild)]
        public async Task night()
        {
            SocketGuildUser User1 = Context.User as SocketGuildUser;
            if (Context.Guild.Id != 472261911526768642)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in WWO Simulation - Game Server!");
                return;
            }
            else if (!User1.GuildPermissions.ManageRoles)
            {
                await Context.Channel.SendMessageAsync("Your Permissions is Missing (ManageRole Permission).");
                return;
            }
            else
            {
                /*if (GlobalFunction.gametime != "night")
                {
                    HttpClient dude = new HttpClient();
                    var clonestream = new MemoryStream();
                    Stream stream = await dude.GetStreamAsync("https://cdn.discordapp.com/attachments/470026969963167767/474664582850543616/20180802_164341.jpg");
                    stream.CopyTo(clonestream);
                    Image pic = new Image(clonestream);
                    clonestream.Position = 0;
                    await Context.Guild.ModifyAsync(x => x.Icon = pic);
                }*/
                if (GlobalFunction.gametime == "day" & GlobalFunction.gamestatus == "hosting" & GlobalFunction.gamecodes != null)
                {
                    GlobalFunction.gametime = "night";
                    if (GlobalFunction.jailed != 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Alive").Members.Contains(Context.Guild.GetUser(GlobalFunction.jailed)))
                    {
                        await Context.Client.GetGuild(465795320526274561).GetUser(GlobalFunction.jailed).AddRoleAsync(Context.Client.GetGuild(465795320526274561).Roles.FirstOrDefault(x => x.Name == "Jailed"));
                    }
                }
                else
                {
                    GlobalFunction.gametime = "night";
                    if (GlobalFunction.jailed != 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Alive").Members.Contains(Context.Guild.GetUser(GlobalFunction.jailed)))
                    {
                        await Context.Client.GetGuild(465795320526274561).GetUser(GlobalFunction.jailed).AddRoleAsync(Context.Client.GetGuild(465795320526274561).Roles.FirstOrDefault(x => x.Name == "Jailed"));
                    }
                }
            }

        }
        [Command("day")]
        [Alias("testday")]
        [RequireBotPermission(Discord.GuildPermission.ManageGuild)]
        public async Task day()
        {
            SocketGuildUser User1 = Context.User as SocketGuildUser;
            if (!User1.GuildPermissions.ManageRoles)
            {
                await Context.Channel.SendMessageAsync("Your Permissions is Missing (ManageRole Permission).");
                return;
            }
            else if (Context.Guild.Id != 472261911526768642)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in WWO Simulation - Game Server!");
                return;
            }
            else
            {
                /*if (GlobalFunction.gametime != "day")
                {
                    HttpClient dude = new HttpClient();
                    var clonestream = new MemoryStream();
                    Stream stream = await dude.GetStreamAsync("https://cdn.discordapp.com/attachments/470026969963167767/474664247100571678/Logopit_1533238665529.jpg");
                    stream.CopyTo(clonestream);
                    Image pic = new Image(clonestream);
                    clonestream.Position = 0;
                    await Context.Guild.ModifyAsync(x => x.Icon = pic);
                }*/
                if (GlobalFunction.gametime == "night" & GlobalFunction.gamestatus == "hosting" & GlobalFunction.gamecodes != null)
                {
                    GlobalFunction.gametime = "day";
                    if (GlobalFunction.jailed != 0)
                    {
                        await Context.Client.GetGuild(465795320526274561).GetUser(GlobalFunction.jailed).RemoveRoleAsync(Context.Client.GetGuild(465795320526274561).Roles.FirstOrDefault(x => x.Name == "Jailed"));
                    }
                }
                else
                {
                    GlobalFunction.gametime = "day";
                    if (GlobalFunction.jailed != 0)
                    {
                        await Context.Client.GetGuild(465795320526274561).GetUser(GlobalFunction.jailed).RemoveRoleAsync(Context.Client.GetGuild(465795320526274561).Roles.FirstOrDefault(x => x.Name == "Jailed"));
                    }
                }
            }
        }
        [Command("vt")]
        [Alias("testvt")]
        public async Task vt()
        {
            SocketGuildUser User1 = Context.User as SocketGuildUser;
            if (Context.Guild.Id != 472261911526768642)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in WWO Simulation - Game Server!");
                return;
            }
            else if (!User1.GuildPermissions.ManageRoles)
            {
                await Context.Channel.SendMessageAsync("Your Permissions is Missing (ManageRole Permission).");
                return;
            }
            else
            {
                if (GlobalFunction.gametime == "day" & GlobalFunction.gamestatus == "hosting" & GlobalFunction.gamecodes != null)
                {
                    return;
                }
                else return;
            }
        }
        [Command("end")]
        [Alias("testend")]
        [RequireBotPermission(Discord.GuildPermission.ManageGuild)]
        public async Task end()
        {
            SocketGuildUser User1 = Context.User as SocketGuildUser;
            if (!User1.GuildPermissions.ManageRoles)
            {
                await Context.Channel.SendMessageAsync("Your Permissions is Missing (ManageRole Permission).");
                return;
            }
            else if (Context.Guild.Id != 472261911526768642)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in WWO Simulation - Game Server!");
                return;
            }
            else
            {
                /*if (GlobalFunction.gametime == "night" & GlobalFunction.gametime == "day")
                {
                    HttpClient dude = new HttpClient();
                    var clonestream = new MemoryStream();
                    Stream stream = await dude.GetStreamAsync("https://cdn.discordapp.com/attachments/465962284062081025/477541867911774238/Untitled95.jpg");
                    stream.CopyTo(clonestream);
                    Image pic = new Image(clonestream);
                    clonestream.Position = 0;
                    await Context.Guild.ModifyAsync(x => x.Icon = pic);
                }*/
                var embed = new EmbedBuilder();
                if (GlobalFunction.gamestatus == "hosting" & GlobalFunction.gamecodes != null & GlobalFunction.wons != null)
                {

                    await Context.Client.GetGuild(465795320526274561).GetTextChannel(606123818305585167).SendMessageAsync("Game " + GlobalFunction.gamecodes + " ended - " + GlobalFunction.wons + " won the match!");
                    GlobalFunction.gamecodes = null;
                    GlobalFunction.wons = null;
                    GlobalFunction.gamestatus = null;
                    GlobalFunction.gametime = null;
                    GlobalFunction.jailer = 0;
                    GlobalFunction.jailed = 0;
                    GlobalFunction.jailerammo = 0;
                    GlobalFunction.daycount = 0;
                    GlobalFunction.matchresult = null;
                    GlobalFunction.matchmember = null;
                    await Context.Client.SetGameAsync("No Game Hosting");
                    
                    IEnumerable<IMessage> nonPinnedMessages = await Context.Guild.GetTextChannel(606132422601474049).GetMessagesAsync(1000).FlattenAsync();
                    await Context.Guild.GetTextChannel(606132422601474049).DeleteMessagesAsync(nonPinnedMessages.Where(x => x.IsPinned == false));
                    //Bulk Delete Messages Channel #player-commands in Werewolf Online Simulation - Game Server.

                    IEnumerable<IMessage> nonPinnedMessages2 = await Context.Guild.GetTextChannel(606148110682423306).GetMessagesAsync(1000).FlattenAsync();
                    await Context.Guild.GetTextChannel(606148110682423306).DeleteMessagesAsync(nonPinnedMessages2.Where(x => x.IsPinned == false));
                    //Bulk Delete Messages Channel #Timer in Werewolf Online Simulation - Game Server.

                    IEnumerable<IMessage> nonPinnedMessages3 = await Context.Guild.GetTextChannel(606138364168634378).GetMessagesAsync(1000).FlattenAsync();
                    await Context.Guild.GetTextChannel(606138364168634378).DeleteMessagesAsync(nonPinnedMessages3.Where(x => x.IsPinned == false));
                    //Bulk Delete Messages Channel #music-log in Werewolf Online Simulation - Game Server.
                    
                    var GetAllUser = Context.Client.GetGuild(465795320526274561).Users;
                    foreach (var x in GetAllUser)
                    {
                        var g = Context.Client.GetGuild(465795320526274561).GetUser(x.Id).Roles.Any(a => a.Name == "Joining");
                        if (g == true)
                        {
                            await x.RemoveRoleAsync(Context.Client.GetGuild(465795320526274561).Roles.FirstOrDefault(c => c.Name == "Joining"));
                        }
                    }
                }
                /*else if (GlobalFunction.gamestatus == "hosting" & GlobalFunction.gamecodes != null & GlobalFunction.wons != null)
                {
                    
                    embed.AddField($"Result of Match "+GlobalFunction.gamecodes+" \n \n", "" + GlobalFunction.matchmember + "\n" + GlobalFunction.matchresult + "");
                    embed.WithColor(new Discord.Color(0, 255, 0));
                    await Context.Client.GetGuild(465795320526274561).GetTextChannel(606123818305585167).SendMessageAsync("", false, embed.Build());

                    await Context.Client.GetGuild(465795320526274561).GetTextChannel(606123818305585167).SendMessageAsync("Game " + GlobalFunction.gamecodes + " ended - " + GlobalFunction.wons + " won the match!");
                    GlobalFunction.gamemodes = null;
                    GlobalFunction.wons = null;
                    GlobalFunction.gamestatus = null;
                    GlobalFunction.gametime = null;
                    GlobalFunction.jailer = 0;
                    GlobalFunction.jailed = 0;
                    GlobalFunction.jailerammo = 0;
                    GlobalFunction.daycount = 0;
                    GlobalFunction.matchresult = null;
                    GlobalFunction.matchmember = null;
                    await Context.Client.SetGameAsync("No Game Hosting");

                    var GetAllUser = Context.Client.GetGuild(465795320526274561).Users;
                    foreach (var x in GetAllUser)
                    {
                        var g = Context.Client.GetGuild(465795320526274561).GetUser(x.Id).Roles.Any(a => a.Name == "Joining");
                        if (g == true)
                        {
                            x.RemoveRoleAsync(Context.Client.GetGuild(465795320526274561).Roles.FirstOrDefault(c => c.Name == "Joining"));
                        }
                    }

                    IEnumerable<IMessage> nonPinnedMessages = await Context.Guild.GetTextChannel(559650561981415424).GetMessagesAsync(1000).FlattenAsync();
                    await Context.Guild.GetTextChannel(559650561981415424).DeleteMessagesAsync(nonPinnedMessages.Where(x => x.IsPinned == false));
                    //Bulk Delete Messages Channel #player-commands in Werewolf Online Simulation - Game Server.

                    IEnumerable<IMessage> nonPinnedMessages2 = await Context.Guild.GetTextChannel(556385557983395840).GetMessagesAsync(1000).FlattenAsync();
                    await Context.Guild.GetTextChannel(556385557983395840).DeleteMessagesAsync(nonPinnedMessages2.Where(x => x.IsPinned == false));
                    //Bulk Delete Messages Channel #Timer in Werewolf Online Simulation - Game Server.

                    IEnumerable<IMessage> nonPinnedMessages3 = await Context.Guild.GetTextChannel(549202652689596427).GetMessagesAsync(1000).FlattenAsync();
                    await Context.Guild.GetTextChannel(549202652689596427).DeleteMessagesAsync(nonPinnedMessages3.Where(x => x.IsPinned == false));
                    //Bulk Delete Messages Channel #music-log in Werewolf Online Simulation - Game Server.
                }*/
                else if (GlobalFunction.gamestatus == "hosting" & GlobalFunction.gamecodes != null & GlobalFunction.wons == null)
                {
                    await Context.Guild.GetTextChannel(606131484532801549).SendMessageAsync(Context.User.Mention+", Can't Announce game end. (Team to Win is Missing)");
                }
                else if (GlobalFunction.gamecodes != null & GlobalFunction.wons == null)
                {
                    GlobalFunction.gamestatus = null;
                    GlobalFunction.gametime = null;
                    GlobalFunction.jailer = 0;
                    GlobalFunction.jailed = 0;
                    GlobalFunction.jailerammo = 0;
                    GlobalFunction.daycount = 0;
                    GlobalFunction.matchresult = null;
                    GlobalFunction.matchmember = null;
                    await Context.Client.SetGameAsync("Waiting For Player");
                }
                else
                {
                    GlobalFunction.gamecodes = null;
                    GlobalFunction.wons = null;
                    GlobalFunction.gamestatus = null;
                    GlobalFunction.gametime = null;
                    GlobalFunction.jailer = 0;
                    GlobalFunction.jailed = 0;
                    GlobalFunction.jailerammo = 0;
                    GlobalFunction.daycount = 0;
                    GlobalFunction.matchresult = null;
                    GlobalFunction.matchmember = null;
                    await Context.Client.SetGameAsync("No Game Hosting");
                }
            }
        }
        [Command("addplayer")]
        [RequireBotPermission(Discord.GuildPermission.ManageRoles)]
        public async Task themvaitrochonguoichoi(IGuildUser user = null, IGuildChannel channel = null)
        {
            if (Context.Guild.Id == 472261911526768642 || Context.Guild.Id == 584044607910969386)
            {
                OverwritePermissions chophep = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Allow, readMessageHistory: PermValue.Allow);
                var embed = new EmbedBuilder();
                SocketGuildUser User1 = Context.User as SocketGuildUser;
                if (!User1.GuildPermissions.ManageRoles)
                {
                    embed.AddField($"Error!", "User need ManageRoles Permission.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    if (user == null)
                    {
                        embed.AddField($"Error!", "User to add is Missing.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (channel == null)
                    {
                        embed.AddField($"Error!", "Channel to add User is Missing.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else
                    {
                        await Context.Client.GetGuild(Context.Guild.Id).GetTextChannel(channel.Id).AddPermissionOverwriteAsync(user, chophep.Modify());
                        await Context.Channel.SendMessageAsync("Added " + user.Nickname + " to <#" + channel.Id + ">.");
                    }
                }
            }
            else return;
        }
        [Command("dj")]
        [RequireBotPermission(Discord.GuildPermission.ManageRoles)]
        public async Task djrole()
        {
            var WWO = Context.Client.GetGuild(465795320526274561);
            var user = WWO.GetUser(Context.User.Id);

            if (Context.Guild.Id != 472261911526768642)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in WWO Simulation - Game Server!");
                return;
            }
            else if (user.Roles.Any(x => x.Name == "DJ"))
            {
                await (Context.User as IGuildUser).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "DJ"));
                await Context.Channel.SendMessageAsync("Role Added!");
            }
            else await Context.Channel.SendMessageAsync("Your DJ role is Missing in Main - WWO Simulation!");
        }
        /*[Command("gnarrate")]
        [RequireBotPermission(Discord.GuildPermission.ManageRoles)]
        public async Task gnarrate()
        {
            if (Context.Guild.Id != 472261911526768642)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in WWO Simulation - Game Server!");
                return;
            }
            else if (!Context.Guild.Roles.FirstOrDefault(x => x.Name == "Alive").Members.Contains(Context.User) & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Mini Narrator").Members.Contains(Context.User))
            {
                await (Context.User as IGuildUser).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Narrator Trainee"));
            }
        }*/
        [Command("gnarrator")]
        [RequireBotPermission(Discord.GuildPermission.ManageRoles)]
        public async Task gnarrator()
        {
            if (Context.Guild.Id != 472261911526768642)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in WWO Simulation - Game Server!");
                return;
            }
            else if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Narrator Trainee").Members.Contains(Context.User))
            {
                await (Context.User as IGuildUser).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Narrator Trainee"));
            }
        }
        [Command("gn")]
        [RequireBotPermission(Discord.GuildPermission.ManageRoles)]
        public async Task gnrole()
        {
            var WWO = Context.Client.GetGuild(465795320526274561);
            var user = WWO.GetUser(Context.User.Id);

            if (Context.Guild.Id != 472261911526768642)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in WWO Simulation - Game Server!");
                return;
            }
            else if (user.Roles.Any(x => x.Name == "Game Narrator"))
            {
                await (Context.User as IGuildUser).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "GN"));
                await Context.Channel.SendMessageAsync("Role Added!");
            }
            else await Context.Channel.SendMessageAsync("Your Game Narrator role is Missing in Main - WWO Simulation!");
        }
        [Command("gnhelper")]
        [RequireBotPermission(Discord.GuildPermission.ManageRoles)]
        public async Task gnhelperrole()
        {
            var WWO = Context.Client.GetGuild(465795320526274561);
            var user = WWO.GetUser(Context.User.Id);

            if (Context.Guild.Id != 472261911526768642)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in WWO Simulation - Game Server!");
                return;
            }
            else if (user.Roles.Any(x => x.Name == "Mini Narrator"))
            {
                await (Context.User as IGuildUser).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Mini GN"));
                await Context.Channel.SendMessageAsync("Role Added!");
            }
            else await Context.Channel.SendMessageAsync("Your Mini Narrator role is Missing in Main - WWO Simulation!");
        }
        [Command("kowwo")]
        [RequireBotPermission(Discord.GuildPermission.ManageRoles)]
        public async Task kowworole()
        {
            var WWO = Context.Client.GetGuild(465795320526274561);
            var user = WWO.GetUser(Context.User.Id);

            if (Context.Guild.Id != 472261911526768642)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in WWO Simulation - Game Server!");
                return;
            }
            else if (user.Roles.Any(x => x.Name == "KOWWO"))
            {
                string role = "King of WWO";
                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == role).Members.Contains(Context.User))
                {
                    await (Context.User as IGuildUser).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == role));
                    await ReplyAsync($"{role} role has been removed.");
                }
                else
                {
                    await (Context.User as IGuildUser).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == role));
                    await ReplyAsync($"{role} role has been added.");
                }
            }
            else await Context.Channel.SendMessageAsync("Your KOWWO role is Missing in Main - WWO Simulation!");
        }
        [Command("test")]
        public async Task testgc([Remainder] string gc)
        {
            var WWO = Context.Client.GetGuild(465795320526274561);
            var user = WWO.GetUser(Context.User.Id);
            var checkbotstatus = Context.Client.GetUser(470020987866578964);

            if (gc == "del" & Context.User.Id == 454492255932252160)
            {
                await Context.Channel.SendMessageAsync("Game Code removed");
                GlobalFunction.gamecodes = null;
            }
            else if (gc == "show" & Context.User.Id == 454492255932252160)
            {
                await Context.Channel.SendMessageAsync("Game Code is " + GlobalFunction.gamecodes + "");
            }
            else if (gc == "test" & Context.User.Id == 454492255932252160)
            {
                await Context.Channel.SendMessageAsync("<#549192241017520140>");
            }
            else if (gc == "start" & Context.User.Id == 454492255932252160)
            {
                GlobalFunction.gamecodes = "checkbug";
                GlobalFunction.gametime = "day";
                GlobalFunction.gamestatus = "hosting";
            }
            else if (gc == "rac" & Context.User.Id == 454492255932252160)
            {
                var a = Context.Client.GetGuild(465795320526274561).GetUser(Context.Client.CurrentUser.Id);
                await (Context.Client.CurrentUser as IGuildUser).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Joining"));
                await Context.Channel.SendMessageAsync("" + Context.Client.CurrentUser.Id + "");
                //await (Context.User as IGuildUser).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Narrator Trainee"));
            }//🏆
            else if (gc == "status")
            {
                await Context.Channel.SendMessageAsync("Status of Narrator Bot is " + checkbotstatus.Status + "");
            }
            else if (gc == "icon")
            {
                await Context.Channel.SendMessageAsync("🏆");
            }
            else if (gc == "aaa" & Context.User.Id == 454492255932252160)
            {
                GlobalFunction.gametime = "day";
                GlobalFunction.gamestatus = "hosting";
            }
            else if (Context.User.Id == 454492255932252160)
            {
                await Context.Client.SetGameAsync("" + gc + "");
            }
        }
        [Command("test2")]
        public async Task testgetidfromnickname(IGuildUser user)
        {
            await Context.Channel.SendMessageAsync("ID " + user.Id + " - " + user.Nickname + "#" + user.Discriminator + "");
        }
        [Command("gwhost")]
        public async Task gwhostcode(string mn = null, [Remainder] string gc = null)
        {
            var WWO = Context.Client.GetGuild(465795320526274561);
            var user = WWO.GetUser(Context.User.Id);
            var checkbotstatus = Context.Client.GetUser(470020987866578964);

            if (Context.Guild.Id != 465795320526274561)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in Main - WWO Simulation!");
                return;
            }
            else if (checkbotstatus.Status == UserStatus.Offline & mn.ToLower() != "manual")
            {
                await Context.Channel.SendMessageAsync("Narrator Bot is Offline, so you can't host at this time!");
                return;
            }
            else if (user.Roles.Any(x => x.Name == "Game Narrator") || user.Roles.Any(x => x.Name == "Mini Narrator"))
            {
                if (mn == null)
                {
                    await Context.Channel.SendMessageAsync("Game Code is Missing, Please write your Game Code to start Host Game.");
                    return;
                }
                else if (mn.ToLower() != "manual")
                {
                    if (gc == null)
                    {
                        GlobalFunction.gamecodes = mn;
                    }
                    else GlobalFunction.gamecodes = "" + mn + " " + gc + "";
                    Context.Guild.Roles.FirstOrDefault(x => x.Id == 606123686633799680).ModifyAsync(y => y.Mentionable = true);
                    await Context.Client.GetGuild(465795320526274561).GetTextChannel(606123818305585167).SendMessageAsync(Context.Guild.Roles.FirstOrDefault(x => x.Id == 606123686633799680).Mention + ", we are now starting game " + GlobalFunction.gamecodes + "! Our host will be " + Context.User.Mention + ". Type `-join " + GlobalFunction.gamecodes + "` to enter the game in <#606123821656702987>. If you don't want to get pinged for future games, come to <#578997340019490826> and reaction icon :video_game:.");
                    await Context.Client.GetGuild(472261911526768642).GetTextChannel(606422958721859585).SendMessageAsync("= = = = = S T A R T = = = = =");
                    await Context.Client.SetGameAsync("Waiting For Player");
                    Context.Guild.Roles.FirstOrDefault(x => x.Id == 606123686633799680).ModifyAsync(y => y.Mentionable = false);
                }
                else if (mn.ToLower() == "manual")
                {
                    if (gc == null)
                    {
                        await Context.Channel.SendMessageAsync("Game Code is Missing, Please write your Game Code to start Host Game.");//.Id == 606123686633799680 - Ping PIayer role.
                    }
                    else
                    {
                        GlobalFunction.gamecodes = gc;
                        Context.Guild.Roles.FirstOrDefault(x => x.Id == 606123686633799680).ModifyAsync(y => y.Mentionable = true);
                        await Context.Client.GetGuild(465795320526274561).GetTextChannel(606123818305585167).SendMessageAsync(Context.Guild.Roles.FirstOrDefault(x => x.Id == 606123686633799680).Mention + ", we are now starting game " + GlobalFunction.gamecodes + "! Our host will be " + Context.User.Mention + ". Type `-join " + GlobalFunction.gamecodes + "` to enter the game in <#606123821656702987>. If you don't want to get pinged for future games, come to <#578997340019490826> and reaction icon :video_game:.");
                        await Context.Client.GetGuild(472261911526768642).GetTextChannel(606422958721859585).SendMessageAsync("= = = = = S T A R T = = = = =");
                        await Context.Client.GetGuild(465795320526274561).GetTextChannel(606123818305585167).SendMessageAsync("Note: game start with Manual.");
                        await Context.Client.SetGameAsync("Waiting For Player");
                        Context.Guild.Roles.FirstOrDefault(x => x.Id == 606123686633799680).ModifyAsync(y => y.Mentionable = false);
                    }
                }
                else await Context.Channel.SendMessageAsync("Game Code `" + GlobalFunction.gamecodes + "` has been hosted, so can't change game code until game end!");
            }
            else await Context.Channel.SendMessageAsync("Your Game Narrator or Mini Narrator role is Missing in Main - WWO Simulation!");
        }
        [Command("gwcancel")]
        [Alias("rwcancel")]
        public async Task gwcancel()
        {
            var WWO = Context.Client.GetGuild(465795320526274561);
            var user = WWO.GetUser(Context.User.Id);

            if (Context.Guild.Id != 465795320526274561)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in Main - WWO Simulation!");
                return;
            }
            else if (user.Roles.Any(x => x.Name == "Game Narrator") || user.Roles.Any(x => x.Name == "Mini Narrator"))
            {
                if (GlobalFunction.gamecodes != null)
                {
                    await Context.Client.GetGuild(465795320526274561).GetTextChannel(606123818305585167).SendMessageAsync("Game " + GlobalFunction.gamecodes + " was cancelled, sorry for any inconvenience caused.");
                    await Context.Client.SetGameAsync("No Game Hosting");
                    GlobalFunction.gamecodes = null;
                    var GetAllUser = Context.Guild.Users;
                    foreach (var x in GetAllUser)
                    {
                        var g = Context.Guild.GetUser(x.Id).Roles.Any(a => a.Name == "Joining");
                        if (g == true)
                        {
                            await x.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(c => c.Name == "Joining"));
                        }
                    }
                }
                else await Context.Channel.SendMessageAsync("No game hosting.");
            }
            else await Context.Channel.SendMessageAsync("Your Game Narrator or Mini Narrator role is Missing in Main - WWO Simulation!");
        }
        [Command("rwhost")]
        public async Task rankhostcode([Remainder] string gc = null)
        {
            var rs = File.ReadAllText($"{GlobalFunction.filelocal}RankedSeason.txt");
            var WWO = Context.Client.GetGuild(465795320526274561);
            var user = WWO.GetUser(Context.User.Id);
            var checkbotstatus = Context.Client.GetUser(470020987866578964);

            if (Context.Guild.Id != 465795320526274561)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in Main - WWO Simulation!");
                return;
            }
            else if (checkbotstatus.Status == UserStatus.Offline)
            {
                await Context.Channel.SendMessageAsync("Narrator Bot is Offline, so you can't host at this time!");
                return;
            }
            else if (user.Roles.Any(x => x.Name == "Game Narrator"))
            {
                if (gc == null)
                {
                    await Context.Channel.SendMessageAsync("Game Code is Missing, Please write your Game Code to start Host Game.");
                    return;
                }
                else if (GlobalFunction.gamecodes == null)
                {
                    GlobalFunction.gamecodes = "RS."+rs+"[" + gc + "]";
                    Context.Guild.Roles.FirstOrDefault(x => x.Name == "Ranked Warn").ModifyAsync(y => y.Mentionable = true);
                    await Context.Client.GetGuild(465795320526274561).GetTextChannel(606123818305585167).SendMessageAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Ranked Warn").Mention + ", we are now starting game " + GlobalFunction.gamecodes + "! Our host will be " + Context.User.Mention + ". Type `-join " + GlobalFunction.gamecodes + "` to enter the game in <#606123821656702987>. If you don't want to get pinged for future games, come to <#578997340019490826> and reaction icon 🏆.");
                    await Context.Client.GetGuild(472261911526768642).GetTextChannel(606422958721859585).SendMessageAsync("= = = = = S T A R T = = = = =");
                    await Context.Client.SetGameAsync("Waiting For Player");
                    Context.Guild.Roles.FirstOrDefault(x => x.Name == "Ranked Warn").ModifyAsync(y => y.Mentionable = false);
                }
                else await Context.Channel.SendMessageAsync("Game Code `" + GlobalFunction.gamecodes + "` has been hosted, so can't change game code until game end!");
            }
            else await Context.Channel.SendMessageAsync("Your Game Narrator role is Missing in Main - WWO Simulation!");
        }
        [Command("vwin")]
        public async Task vwin()
        {
            SocketGuildUser User1 = Context.User as SocketGuildUser;
            if (Context.Guild.Id != 472261911526768642)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in Main - WWO Simulation!");
                return;
            }
            else if (!User1.GuildPermissions.ManageRoles)
            {
                await Context.Channel.SendMessageAsync("Your Permissions is Missing (ManageRole Permission).");
                return;
            }
            else
            {
                if (GlobalFunction.gamecodes != null)
                {
                    GlobalFunction.wons = "Village";
                    await Context.Client.GetGuild(472261911526768642).GetTextChannel(606132999389708330).SendMessageAsync("Game Over - Villagers Win!");
                    await Context.Client.SetGameAsync("Game Ended");
                    var embed = new EmbedBuilder();
                    var GetAllUser = Context.Client.GetGuild(465795320526274561).Users;
                    foreach (var x in GetAllUser)
                    {
                        var g = Context.Client.GetGuild(465795320526274561).GetUser(x.Id).Roles.Any(a => a.Name == "Joining");
                        if (g == true)
                        {
                            await x.RemoveRoleAsync(Context.Client.GetGuild(465795320526274561).Roles.FirstOrDefault(c => c.Name == "Joining"));
                        }
                    }
                }
                else return;
            }
        }
        [Command("wwin")]
        public async Task wwin()
        {
            SocketGuildUser User1 = Context.User as SocketGuildUser;
            if (Context.Guild.Id != 472261911526768642)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in Game Server - WWO Simulation!");
                return;
            }
            else if (!User1.GuildPermissions.ManageRoles)
            {
                await Context.Channel.SendMessageAsync("Your Permissions is Missing (ManageRole Permission).");
                return;
            }
            else
            {
                if (GlobalFunction.gamecodes != null)
                {
                    GlobalFunction.wons = "Werewolves";
                    await Context.Client.GetGuild(472261911526768642).GetTextChannel(606132999389708330).SendMessageAsync("Game Over - Werewolves Win!");
                    await Context.Client.SetGameAsync("Game Ended");
                    var embed = new EmbedBuilder();
                    var GetAllUser = Context.Client.GetGuild(465795320526274561).Users;
                    foreach (var x in GetAllUser)
                    {
                        var g = Context.Client.GetGuild(465795320526274561).GetUser(x.Id).Roles.Any(a => a.Name == "Joining");
                        if (g == true)
                        {
                            await x.RemoveRoleAsync(Context.Client.GetGuild(465795320526274561).Roles.FirstOrDefault(c => c.Name == "Joining"));
                        }
                    }
                }
                else return;
            }
        }
        [Command("win")]
        public async Task win([Remainder] string team = null)
        {
            SocketGuildUser User1 = Context.User as SocketGuildUser;
            if (Context.Guild.Id != 472261911526768642)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in Main - WWO Simulation!");
                return;
            }
            else if (!User1.GuildPermissions.ManageRoles)
            {
                await Context.Channel.SendMessageAsync("Your Permissions is Missing (ManageRole Permission).");
                return;
            }
            else if (team == null)
            {
                await Context.Channel.SendMessageAsync("Team to won the match is Missing, Please write your team to know which team won.");
                return;
            }
            else if (GlobalFunction.wons == null & GlobalFunction.gamecodes != null)
            {
                GlobalFunction.wons = team;
                await Context.Client.GetGuild(472261911526768642).GetTextChannel(606132999389708330).SendMessageAsync("Game Over - " + team + " Win!");
                await Context.Client.SetGameAsync("Game Ended");
                var embed = new EmbedBuilder();
                var GetAllUser = Context.Client.GetGuild(465795320526274561).Users;
                foreach (var x in GetAllUser)
                {
                    var g = Context.Client.GetGuild(465795320526274561).GetUser(x.Id).Roles.Any(a => a.Name == "Joining");
                    if (g == true)
                    {
                        await x.RemoveRoleAsync(Context.Client.GetGuild(465795320526274561).Roles.FirstOrDefault(c => c.Name == "Joining"));
                    }
                }
            }
            else
            {
                if (GlobalFunction.gamecodes != null)
                {
                    GlobalFunction.wons = team;
                    await Context.Client.GetGuild(472261911526768642).GetTextChannel(606131484532801549).SendMessageAsync("Game Over - " + team + " Win!");
                    await Context.Client.SetGameAsync("Game Ended");
                    var embed = new EmbedBuilder();
                    var GetAllUser = Context.Client.GetGuild(465795320526274561).Users;
                    foreach (var x in GetAllUser)
                    {
                        var g = Context.Client.GetGuild(465795320526274561).GetUser(x.Id).Roles.Any(a => a.Name == "Joining");
                        if (g == true)
                        {
                            await x.RemoveRoleAsync(Context.Client.GetGuild(465795320526274561).Roles.FirstOrDefault(c => c.Name == "Joining"));
                        }
                    }
                }
                else return;
            }
        }
        [Command("join")]
        [RequireBotPermission(Discord.GuildPermission.ManageRoles)]
        public async Task join([Remainder] string gamecd = null)
        {
            SocketGuildUser User1 = Context.User as SocketGuildUser;
            if (Context.Guild.Id != 472261911526768642 & Context.Channel.Id != 606123821656702987)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in <#606123821656702987> of Main - WWO Simulation!");
                return;
            }
            else if (GlobalFunction.gamecodes == null)
            {
                await Context.Channel.SendMessageAsync("No game hosting at this time.");
            }
            else if (gamecd == null)
            {
                await Context.Channel.SendMessageAsync("Game Code to join is Missing, Please check Game Code in <#606123818305585167>.");
            }
            else if (gamecd.ToLower() == (GlobalFunction.gamecodes).ToLower())
            {
                await (Context.User as IGuildUser).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Joining"));
                await Context.Guild.GetTextChannel(606123824743841793).SendMessageAsync($"{Context.User.Username} joined match {GlobalFunction.gamecodes}\nUser ID: {Context.User.Id}");
            }
            else await Context.Channel.SendMessageAsync(Context.User.Mention + ", Your game code to join is wrong (Game code is `" + GlobalFunction.gamecodes + "`).");
        }
        [Command("resets")]
        public async Task resetstats()
        {
            SocketGuildUser User1 = Context.User as SocketGuildUser;
            if (Context.Client.GetGuild(465795320526274561).GetUser(Context.User.Id).Roles.Any(x => x.Name == "Game Narrator") || Context.Client.GetGuild(465795320526274561).GetUser(Context.User.Id).Roles.Any(x => x.Name == "Mini Narrator"))
            {
                GlobalFunction.gamecodes = null;
                await Context.Channel.SendMessageAsync("Game Code has been reseted");
                GlobalFunction.wons = null;
                await Context.Channel.SendMessageAsync("Team won has been reseted");
                GlobalFunction.gamestatus = null;
                GlobalFunction.jailed = 0;
                GlobalFunction.jailer = 0;
                GlobalFunction.jailerammo = 0;
                GlobalFunction.daycount = 0;
                GlobalFunction.matchmember = null;
                GlobalFunction.matchresult = null;
                await Context.Channel.SendMessageAsync("Game Status has been reseted");
                await Context.Client.SetGameAsync("No Game Hosting");
            }
            else
            {
                await Context.Channel.SendMessageAsync("Your Game Narrator or Mini Narrator role is Missing in WWO Simulation.");
                return;
            }
        }
        /*        [Command("jail")]
                public async Task jailplayer(IGuildUser user)
                {
                    if (Context.Guild.Id != 472261911526768642)
                        {
                            return;
                        }
                    else
                    {
                        if (Context.Channel.Id == 549261355216273419)
                        {
                            if (user.Id != Context.User.Id)
                            {
                                if (GlobalFunction.gametime == "day" & Context.Guild.GetUser(Context.User.Id).Roles.Any(x => x.Name == "Alive") & Context.Guild.GetUser(user.Id).Roles.Any(x => x.Name == "Alive"))
                                {
                                    GlobalFunction.jailer = Context.User.Id;
                                    GlobalFunction.jailed = user.Id;
                                }
                                else return;
                            }
                            else return;
                        }
                        else return;
                    }
                }
                [Command("shoot")]
                public async Task jailershoot(IGuildUser user)
                {
                    if (Context.Guild.Id != 472261911526768642)
                    {
                        return;
                    }
                    else if (GlobalFunction.jailerammo == 0)
                    {
                        return;
                    }
                    else
                    {
                        if (Context.Channel.Id == 549261355216273419)
                        {
                            if (user.Id != Context.User.Id)
                            {
                                if (GlobalFunction.jailed == user.Id)
                                {
                                    if (GlobalFunction.gametime == "night" & Context.Guild.GetUser(Context.User.Id).Roles.Any(x => x.Name == "Alive") & Context.Guild.GetUser(user.Id).Roles.Any(x => x.Name == "Alive"))
                                    {
                                        GlobalFunction.jailerammo--;
                                        await Context.Client.GetGuild(465795320526274561).GetUser(GlobalFunction.jailed).RemoveRoleAsync(Context.Client.GetGuild(465795320526274561).Roles.FirstOrDefault(x => x.Name == "Jailed"));
                                        GlobalFunction.jailed = 0;
                                    }
                                    else return;
                                }
                                else return;
                            }
                            else return;
                        }
                        else return;
                    }
                }*/
        [Command("stats")]
        public async Task ShowStatsAllGlobalFunction()
        {
            if (Context.Client.GetGuild(465795320526274561).GetUser(Context.User.Id).Roles.Any(x => x.Name == "Game Narrator") || Context.Client.GetGuild(465795320526274561).GetUser(Context.User.Id).Roles.Any(x => x.Name == "Mini Narrator"))
            {
                var embed = new EmbedBuilder();
                {
                    //embed.WithTitle($"Status Running In GlobalFunction.");
                    embed.AddField($"Status Running In GlobalFunction. \n \n", "Game Code: " + GlobalFunction.gamecodes + "\nTeam Won: " + GlobalFunction.wons + "\nGame Status: " + GlobalFunction.gamestatus + "\nGame Time: " + GlobalFunction.gametime + "");
                    //embed.WithDescription($"Russian Roullete started for None {Emote.Parse("<:coin:475781084584607745>")}!");
                    embed.WithColor(new Discord.Color(0, 255, 255));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync("Your Game Narrator or Mini Narrator role is Missing in WWO Simulation.");
                return;
            }
        }
        [Command("change")]
        [RequireBotPermission(Discord.GuildPermission.ManageNicknames)]
        public async Task changenickname(IGuildUser user = null, [Remainder] string nickname = null)
        {
            SocketGuildUser User1 = Context.User as SocketGuildUser;
            if (!User1.GuildPermissions.ManageNicknames)
            {
                await Context.Channel.SendMessageAsync("Your Permissions is Missing (ManageNicknames Permission).");
                return;
            }
            else if (user == null)
            {
                await Context.Channel.SendMessageAsync("User to change is Missing, Please write User to change nick name.");
                return;
            }
            else if (nickname == null)
            {
                await Context.Channel.SendMessageAsync("Nick name to change is Missing, Please write your new Nick name for user.");
                return;
            }
            else
            {
                await Context.Guild.GetUser(user.Id).ModifyAsync(x => x.Nickname = nickname);
                await Context.Channel.SendMessageAsync("Changed nick name of " + user.Username + "#" + user.Discriminator + " to " + nickname + "");
            }
        }
        [Command("recover")]
        public async Task ShowStatsAllGlobalFunction(int num = 0, [Remainder] string text = null)
        {
            var embed = new EmbedBuilder();
            {
                SocketGuildUser User1 = Context.User as SocketGuildUser;
                if (!User1.GuildPermissions.ManageRoles)
                {
                    await Context.Channel.SendMessageAsync("Your Permissions is Missing (ManageRoles Permission).");
                    return;
                }
                else if (num <= 0 || num > 4 || num == 0)
                {
                    embed.WithAuthor($"Recover Game Config. \n \n ");
                    embed.AddField($"Commands: -recover <Num> <Text>", "1 - Game Code.\n2 - Game Status (default when hosting is: hosting).\n3 - Game Time (day or night).\n4 - Game Async for Me (default when not host is: No Game Hosting).");
                    embed.WithColor(new Discord.Color(0, 255, 255));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (text == null)
                {
                    embed.AddField($"Error!", "Your Text to recover is Missing, Please write your text to recover the num.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (text == "null")
                {
                    if (num == 1)
                    {
                        embed.AddField($"Done!", "Game Code now changed to null");
                        embed.WithColor(new Discord.Color(0, 255, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                        GlobalFunction.gamecodes = null;
                    }
                    else if (num == 2)
                    {
                        embed.AddField($"Done!", "Game Status now changed to null");
                        embed.WithColor(new Discord.Color(0, 255, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                        GlobalFunction.gamestatus = null;
                    }
                    else if (num == 3)
                    {
                        embed.AddField($"Done!", "Game Time now changed to null");
                        embed.WithColor(new Discord.Color(0, 255, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                        GlobalFunction.gametime = null;
                    }
                    else if (num == 4)
                    {
                        embed.AddField($"Done!", "Game Async for Me now changed to No Game Hosting");
                        embed.WithColor(new Discord.Color(0, 255, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                        await Context.Client.SetGameAsync("No Game Hosting");
                    }
                }
                else if (num == 1)
                {
                    embed.AddField($"Done!", "Game Code now changed to " + text + "");
                    embed.WithColor(new Discord.Color(0, 255, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    GlobalFunction.gamecodes = text;
                }
                else if (num == 2)
                {
                    embed.AddField($"Done!", "Game Status now changed to " + text + "");
                    embed.WithColor(new Discord.Color(0, 255, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    GlobalFunction.gamestatus = text;
                }
                else if (num == 3)
                {
                    embed.AddField($"Done!", "Game Time now changed to " + text + "");
                    embed.WithColor(new Discord.Color(0, 255, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    GlobalFunction.gametime = text;
                }
                else if (num == 4)
                {
                    embed.AddField($"Done!", "Game Async for Me now changed to " + text + "");
                    embed.WithColor(new Discord.Color(0, 255, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    await Context.Client.SetGameAsync(text);
                }
                else
                {
                    embed.WithAuthor($"Recover Game Config. \n \n ");
                    embed.AddField($"Commands: -recover <Num> <Text>", "1 - Game Code.\n2 - Game Status (default when hosting is: hosting).\n3 - Game Time (day or night).\n4 - Game Async for Me (default when not host is: No Game Hosting).");
                    embed.WithColor(new Discord.Color(0, 255, 255));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
        }
        [Command("update")]
        public async Task updatecommand([Remainder]string text = null)
        {
            if (Context.User.Id != 454492255932252160)
            {
                return;
            }
            else
            {
                await Context.Client.GetGuild(465795320526274561).GetTextChannel(606123759514025985).SendMessageAsync(text);
                await Context.Client.GetGuild(465795320526274561).GetTextChannel(606123760944414721).SendMessageAsync(text);
            }
        }
        /*[Command("add")]
        public async Task addmatchresult([Remainder] string text = null)
        {
            if (Context.Guild.Id != 472261911526768642)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in WWO Simulation - Game Server!");
                return;
            }
            else if (GlobalFunction.gamecodes == null)
            {
                await Context.Channel.SendMessageAsync("This Command only can use when Host Game with Command -gwhost");
                return;
            }
            else if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Narrator").Members.Contains(Context.User) || Context.Guild.Roles.FirstOrDefault(x => x.Name == "Narrator Trainee").Members.Contains(Context.User))
            {
                if (text == null)
                {
                    await Context.Channel.SendMessageAsync("Text to add Match Result is Missing, Please write your text to add to Match Result");
                    return;
                }
                else if (text == "reset")
                {
                    if (Context.Channel.Id == 606131484532801549)
                    {
                        await Context.Channel.SendMessageAsync(GlobalFunction.matchresult);
                        await Context.Channel.SendMessageAsync("Match Result is above and has been Removed!");
                        GlobalFunction.matchresult = null;
                        return;
                    }
                    else
                    {
                        await Context.Guild.GetTextChannel(606131484532801549).SendMessageAsync(GlobalFunction.matchresult);
                        await Context.Channel.SendMessageAsync("Match Result in <#606131484532801549> and has been Removed!");
                        GlobalFunction.matchresult = null;
                        return;
                    }
                }
                else
                { 
                GlobalFunction.matchresult = "" + GlobalFunction.matchresult + "\n" + text + "";
                await Context.Channel.SendMessageAsync("Line Of Match Result Added!\n" + text + "");
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync("Your Narrator or Narrator Trainee Role is Missing.");
                return;
            }
        }
        [Command("addmember")]
        [Alias("addmem")]
        public async Task addmatchmember([Remainder] string text = null)
        {
            if (Context.Guild.Id != 472261911526768642)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in WWO Simulation - Game Server!");
                return;
            }
            else if (GlobalFunction.gamecodes == null)
            {
                await Context.Channel.SendMessageAsync("This Command only can use when Host Game with Command -gwhost");
                return;
            }
            else if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Narrator").Members.Contains(Context.User) || Context.Guild.Roles.FirstOrDefault(x => x.Name == "Narrator Trainee").Members.Contains(Context.User))
            {
                if (text == null)
                {
                    await Context.Channel.SendMessageAsync("Text to add Match Member is Missing, Please write your text to add to Match Member");
                    return;
                }
                else if (GlobalFunction.matchmember != null)
                {
                    GlobalFunction.matchmember = "" + text + "";
                    await Context.Channel.SendMessageAsync("Match Member Changed!\n\n" + text + "");
                    return;
                }
                else
                {
                    GlobalFunction.matchmember = "" + text + "";
                    await Context.Channel.SendMessageAsync("Match Member Added!\n\n" + text + "");
                    return;
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync("Your Narrator or Narrator Trainee Role is Missing.");
                return;
            }
        }
        [Command("show")]
        public async Task showmatchresult()
        {
            if (Context.Guild.Id != 472261911526768642)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in WWO Simulation - Game Server!");
                return;
            }
            else if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Narrator").Members.Contains(Context.User) || Context.Guild.Roles.FirstOrDefault(x => x.Name == "Narrator Trainee").Members.Contains(Context.User))
            {
                if (GlobalFunction.matchresult == null)
                {
                    await Context.Channel.SendMessageAsync("Nothing to show Match Result.");
                    return;
                }
                else
                {
                    await Context.Channel.SendMessageAsync(GlobalFunction.matchresult);
                    return;
                }
            }
            else
            {
                await Context.Channel.SendMessageAsync("Your Narrator or Narrator Trainee Role is Missing.");
                return;
            }
        }*/
        [Command("kickasync")]
        [RequireBotPermission(Discord.GuildPermission.KickMembers)]
        public async Task kickallplayeringuild(string abc = null)
        {
            SocketGuildUser User = Context.User as SocketGuildUser;
            var embed = new EmbedBuilder();
            if (!User.GuildPermissions.ManageRoles)
            {
                embed.AddField($"Error!", "Your Permissions is Missing (ManageRoles Permission).");
                embed.WithColor(new Discord.Color(255, 0, 0));
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
            else if (abc.ToLower() == "yes")
            {
                await ReplyAsync("Kicking players with Nickname from 16 to 1, It'll take a while.");
                var a = 16;
                while (a > 0)
                {
                    var g = Context.Guild.Users.FirstOrDefault(x => x.Nickname == a + "");
                    if (g != null)
                    {
                        await g.KickAsync();
                    }
                    a--;
                    if (a <= 0)
                    {
                        await ReplyAsync("Done!");
                    }
                }
            }
            else return;
        }

        [Command("rankedseason")]
        [Alias("rs")]
        public async Task RankedSeason([Remainder]string text = null)
        {
            if (Context.Guild.Id == 465795320526274561 || Context.Guild.Id == 472261911526768642)
            {
                var rs = File.ReadAllText($"{GlobalFunction.filelocal}RankedSeason.txt");
                var embed = new EmbedBuilder();
                SocketGuildUser User1 = Context.User as SocketGuildUser;
                if (!User1.GuildPermissions.ManageRoles)
                {
                    embed.AddField($"Error!", "Permission is Missing (ManageRoles Permissions).");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (text == null)
                {
                    embed.AddField($"Ranked Game!", $"Now is Season {rs}");
                    embed.WithColor(new Discord.Color(0, 255, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    File.WriteAllText($"{GlobalFunction.filelocal}RankedSeason.txt", $"{text}");

                    embed.AddField($"Ranked Game!", $"Changed to Season {text}");
                    embed.WithColor(new Discord.Color(0, 255, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
            else return;
        }

        [Command("getplayers")]
        public async Task getplayerfrom1to16()
        {
            if (Context.Guild.Id != 472261911526768642)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in Game Server - WWO Simulation!");
                return;
            }
            else
            {
                int count = 1;
                string result = null;
                while (count <= 16)
                {
                    var c = $"{count}";
                    var m = Context.Guild.Users.FirstOrDefault(x => x.Nickname == c);
                    if (m != null)
                    {
                        if (result == null)
                        {
                            result = $"{count} - {m.Username}#{m.Discriminator}";
                        }
                        else
                        {
                            result = $"{result}\n{count} - {m.Username}#{m.Discriminator}";
                        }
                    }
                    count++;
                }
                if (result == null)
                {
                    await ReplyAsync($"Not Found - No players with Nickname from 1 to 16.");
                }
                else
                {
                    await ReplyAsync($"{result}");
                }
            }
        }

    }
}
