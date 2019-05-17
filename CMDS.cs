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
using System.Threading.Tasks;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.IO;
using Discord.Addons.Interactive;
using Neko_Test.Modules;

namespace Neko_Test
{
    public class CMDS : ModuleBase<SocketCommandContext>
    {
        Random rnd = new Random();
        [Command("cmds"), Alias("CommandList")]
        [RequireBotPermission(Discord.GuildPermission.EmbedLinks)]
        public async Task ListCommand()
        {
            var embed = new EmbedBuilder();
            {
                embed.WithAuthor($"Neko's Command Support WWO Simulation. \n \n");
                embed.AddField($"Game Server: -gnarrate, -gnarrator, -gn, -gnhelper", "Usage: -gnarrate to become Narrator Trainee (GN Helper - Game Server Only) \n Usage: -gn to get GN role in Game Server (Game Narrator - WWO Simulation Only) \n Usage: -gnhelper to get GN Helper role in Game Server (GN Helper - WWO Simulation Only)");
                embed.AddField($"-gwhost, -gwcancel, -win <Solo Team>, -vwin, -wwin", "...");
                embed.AddField($"More soon!", "...");

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
        public async Task morning(String gamemode = null, [Remainder] string check = null)
        {
            SocketGuildUser User1 = Context.User as SocketGuildUser;
            if (!User1.GuildPermissions.ManageRoles)
            {
                await Context.Channel.SendMessageAsync(":x: Your Permissions is Missing.");
                return;
            }
            else if (Context.Guild.Id != 472261911526768642)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in WWO Simulation - Game Server!");
                return;
            }
            else if (GlobalFunction.gamestatus == "hosting")
            {
                return;
            }
            else if (gamemode == "classic" & check == null || gamemode == "sandbox" & check == null || gamemode == "random" & check != null || gamemode == "Classic" & check == null || gamemode == "Sandbox" & check == null || gamemode == "Random" & check != null)
            {
                /*HttpClient dude = new HttpClient();
                var clonestream = new MemoryStream();
                Stream stream = await dude.GetStreamAsync("https://cdn.discordapp.com/attachments/465962284062081025/477541867911774238/Untitled95.jpg");
                stream.CopyTo(clonestream);
                Image pic = new Image(clonestream);
                clonestream.Position = 0;
                await Context.Guild.ModifyAsync(x => x.Icon = pic);*/
                await Context.Client.GetGuild(465795320526274561).GetTextChannel(549193422817329156).SendMessageAsync("Game now starting, you can no longer join.");
                await Context.Client.GetGuild(465795320526274561).GetTextChannel(549242357741256705).SendMessageAsync("Game Start has been announced.");
                await Context.Client.GetGuild(472261911526768642).GetTextChannel(549202043517272064).SendMessageAsync("Game Start has been announced.");
                GlobalFunction.gamestatus = "hosting";
            }
        }
        [Command("night")]
        [Alias("testnight")]
        [RequireBotPermission(Discord.GuildPermission.ManageGuild)]
        public async Task night()
        {
            SocketGuildUser User1 = Context.User as SocketGuildUser;
            if (!User1.GuildPermissions.ManageRoles)
            {
                await Context.Channel.SendMessageAsync(":x: Your Permissions is Missing.");
                return;
            }
            else if (Context.Guild.Id != 472261911526768642)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in WWO Simulation - Game Server!");
                return;
            }
            else
            {
                /*HttpClient dude = new HttpClient();
                var clonestream = new MemoryStream();
                Stream stream = await dude.GetStreamAsync("https://cdn.discordapp.com/attachments/465962284062081025/477541867911774238/Untitled95.jpg");
                stream.CopyTo(clonestream);
                Image pic = new Image(clonestream);
                clonestream.Position = 0;
                await Context.Guild.ModifyAsync(x => x.Icon = pic);*/
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
                await Context.Channel.SendMessageAsync(":x: Your Permissions is Missing.");
                return;
            }
            else if (Context.Guild.Id != 472261911526768642)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in WWO Simulation - Game Server!");
                return;
            }
            else
            {
                /*HttpClient dude = new HttpClient();
                var clonestream = new MemoryStream();
                Stream stream = await dude.GetStreamAsync("https://cdn.discordapp.com/attachments/465962284062081025/477541867911774238/Untitled95.jpg");
                stream.CopyTo(clonestream);
                Image pic = new Image(clonestream);
                clonestream.Position = 0;
                await Context.Guild.ModifyAsync(x => x.Icon = pic);*/
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
                await Context.Channel.SendMessageAsync(":x: Your Permissions is Missing.");
                return;
            }
            else if (Context.Guild.Id != 472261911526768642)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in WWO Simulation - Game Server!");
                return;
            }
            else
            {
                /*HttpClient dude = new HttpClient();
                var clonestream = new MemoryStream();
                Stream stream = await dude.GetStreamAsync("https://cdn.discordapp.com/attachments/465962284062081025/477541867911774238/Untitled95.jpg");
                stream.CopyTo(clonestream);
                Image pic = new Image(clonestream);
                clonestream.Position = 0;
                await Context.Guild.ModifyAsync(x => x.Icon = pic);*/
                if (GlobalFunction.gamecodes != null & GlobalFunction.wons != null)
                {
                    await Context.Client.GetGuild(465795320526274561).GetTextChannel(549193422817329156).SendMessageAsync("Game " + GlobalFunction.gamecodes + " ended - " + GlobalFunction.wons + " won the match!");
                    GlobalFunction.gamemodes = null;
                    GlobalFunction.wons = null;
                    GlobalFunction.gamestatus = null;
                }
            }
        }
        [Command("gnarrate")]
        [RequireBotPermission(Discord.GuildPermission.ManageRoles)]
        public async Task gnarrate()
        {
            if (Context.Guild.Id != 472261911526768642)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in WWO Simulation - Game Server!");
                return;
            }
            else if (!Context.Guild.Roles.FirstOrDefault(x => x.Name == "Alive").Members.Contains(Context.User) & Context.Guild.Roles.FirstOrDefault(x => x.Name == "GN Helper").Members.Contains(Context.User))
            {
                await (Context.User as IGuildUser).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Narrator Trainee"));
            }
        }
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
        public async Task gnhelperrole()
        {
            var WWO = Context.Client.GetGuild(465795320526274561);
            var user = WWO.GetUser(Context.User.Id);

            if (Context.Guild.Id != 472261911526768642)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in WWO Simulation - Game Server!");
                return;
            }
            else if (user.Roles.Any(x => x.Name == "GN Helper"))
            {
                await (Context.User as IGuildUser).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "GN Helper"));
                await Context.Channel.SendMessageAsync("Role Added!");
            }
            else await Context.Channel.SendMessageAsync("Your GN Helper role is Missing in Main - WWO Simulation!");
        }
        [Command("test")]
        public async Task testgc([Remainder] string gc)
        {
            if (gc == "del" & Context.User.Id == 454492255932252160)
            {
                Context.Channel.SendMessageAsync("Game Code removed");
                GlobalFunction.gamecodes = null;
            }
            else if (gc == "show" & Context.User.Id == 454492255932252160)
            {
                Context.Channel.SendMessageAsync("Game Code is " + GlobalFunction.gamecodes + "");
            }
            else if (gc == "test" & Context.User.Id == 454492255932252160)
            {
                Context.Channel.SendMessageAsync("<#549192241017520140>");
            }
            else if (GlobalFunction.gamecodes == null & Context.User.Id == 454492255932252160)
            {
                GlobalFunction.gamecodes = gc;
                Context.Channel.SendMessageAsync("New Game Code is " + GlobalFunction.gamecodes + "");
            }
        }
        [Command("gwhost")]
        public async Task gwhostcode([Remainder] string gc = null)
        {
            var WWO = Context.Client.GetGuild(465795320526274561);
            var user = WWO.GetUser(Context.User.Id);

            if (Context.Guild.Id != 465795320526274561)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in Main - WWO Simulation!");
                return;
            }
            else if (user.Roles.Any(x => x.Name == "Game Narrator") || user.Roles.Any(x => x.Name == "GN Helper"))
            {
                if (gc == null)
                {
                    await Context.Channel.SendMessageAsync("Game Code is Missing, Please write your Game Code to start Host Game.");
                    return;
                }
                else if (GlobalFunction.gamecodes == null)
                {
                    GlobalFunction.gamecodes = gc;
                    await Context.Client.GetGuild(465795320526274561).GetTextChannel(549193422817329156).SendMessageAsync(Context.Guild.Roles.FirstOrDefault(x=> x.Name== "Player").Mention+", we are now starting game " + GlobalFunction.gamecodes + "! Our host will be " + Context.User.Mention + ". Type `-join " + GlobalFunction.gamecodes + "` to enter the game in <#549193241367543838>. If you don't want to get pinged for future games, type `?player` in <#549192241017520140>.");
                }
                else await Context.Channel.SendMessageAsync("Game Code `" + GlobalFunction.gamecodes + "` has been hosted, so can't change game code until game end!");
            }
            else await Context.Channel.SendMessageAsync("Your Game Narrator or GN Helper role is Missing in Main - WWO Simulation!");
        }
        [Command("gwcancel")]
        public async Task gwcancel()
        {
            var WWO = Context.Client.GetGuild(465795320526274561);
            var user = WWO.GetUser(Context.User.Id);

            if (Context.Guild.Id != 465795320526274561)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in Main - WWO Simulation!");
                return;
            }
            else if (user.Roles.Any(x => x.Name == "Game Narrator") || user.Roles.Any(x => x.Name == "GN Helper"))
            {
                if (GlobalFunction.gamecodes != null)
                {
                    await Context.Client.GetGuild(465795320526274561).GetTextChannel(549193422817329156).SendMessageAsync("Game " + GlobalFunction.gamecodes + " was cancelled, sorry for any inconvenience caused.");
                    GlobalFunction.gamecodes = null;
                }
                else await Context.Channel.SendMessageAsync("No game hosting.");
            }
            else await Context.Channel.SendMessageAsync("Your Game Narrator or GN Helper role is Missing in Main - WWO Simulation!");
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
                await Context.Channel.SendMessageAsync(":x: Your Permissions is Missing.");
                return;
            }
            else
            {
                if (GlobalFunction.gamecodes != null)
                {
                    GlobalFunction.wons = "Village";
                    await Context.Client.GetGuild(472261911526768642).GetTextChannel(549198145779793930).SendMessageAsync("Game Over - Villagers Win!");
                }
            }
        }
        [Command("wwin")]
        public async Task wwin()
        {
            SocketGuildUser User1 = Context.User as SocketGuildUser;
            if (Context.Guild.Id != 472261911526768642)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in Main - WWO Simulation!");
                return;
            }
            else if (!User1.GuildPermissions.ManageRoles)
            {
                await Context.Channel.SendMessageAsync(":x: Your Permissions is Missing.");
                return;
            }
            else
            {
                if (GlobalFunction.gamecodes != null)
                {
                    GlobalFunction.wons = "Werewolves";
                    await Context.Client.GetGuild(472261911526768642).GetTextChannel(549198145779793930).SendMessageAsync("Game Over - Werewolves Win!");
                }
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
                await Context.Channel.SendMessageAsync(":x: Your Permissions is Missing.");
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
                await Context.Client.GetGuild(472261911526768642).GetTextChannel(549198145779793930).SendMessageAsync("Game Over - " + team + " Win!");
            }
            else
            {
                if (GlobalFunction.gamecodes != null)
                {
                GlobalFunction.wons = team;
                await Context.Client.GetGuild(472261911526768642).GetTextChannel(549198145779793930).SendMessageAsync("Game Over - " + team + " Win!");
                }
            }
        }
        [Command("join")]
        public async Task join([Remainder] string gamecd = null)
        {
            SocketGuildUser User1 = Context.User as SocketGuildUser;
            if (Context.Guild.Id != 472261911526768642 & Context.Channel.Id != 549193241367543838)
            {
                await Context.Channel.SendMessageAsync("This Command is Only work in <#549193241367543838> of Main - WWO Simulation!");
                return;
            }
            else if (GlobalFunction.gamecodes == null)
            {
                Context.Channel.SendMessageAsync("No game hosting at this time.");
            }
            else if (gamecd == null)
            {
                Context.Channel.SendMessageAsync("Game Code to join is Missing, Please check Game Code in <#549193422817329156>.");
            }
            else if (GlobalFunction.gamecodes != null & gamecd == GlobalFunction.gamecodes)
            {
                await (Context.User as IGuildUser).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Joining"));
            }
            else await Context.Channel.SendMessageAsync(Context.User.Mention + ", Your game code to join is wrong (Game code is `" + GlobalFunction.gamecodes + "`).");
        }
        [Command("reset")]
        public async Task resetstats()
        {
            SocketGuildUser User1 = Context.User as SocketGuildUser;
            if (User1.GuildPermissions.Administrator || Context.User.Id == 454492255932252160)
            {
                GlobalFunction.gamecodes = null;
                Context.Channel.SendMessageAsync("Game Code has been reseted");
                GlobalFunction.wons = null;
                Context.Channel.SendMessageAsync("Team won has been reseted");
                GlobalFunction.gamestatus = null;
                Context.Channel.SendMessageAsync("Game Status has been reseted");
            }
            else
            {
                await Context.Channel.SendMessageAsync(":x: Your Permissions is Missing (Administrator Permission).");
                return;
            }
        }
    }
}
