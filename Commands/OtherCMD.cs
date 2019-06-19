using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Diagnostics;
using Neko_Test.CommandHandler;
using Neko_Test.Modules;
using Neko_Test.Core.UserAccounts10;

namespace Neko_Test
{
    public class OtherCMD : ModuleBase<SocketCommandContext>
    {
        Random rnd = new Random();

        [Command("copy"), Alias("say")]
        [RequireBotPermission(Discord.GuildPermission.ManageMessages)]
        public async Task copy([Remainder] string message = null)
        {
            SocketGuildUser User1 = Context.User as SocketGuildUser;
            if (!User1.GuildPermissions.ManageMessages & !User1.GuildPermissions.ManageRoles)
            {
                await Context.Channel.SendMessageAsync("Your Permissions is Missing (ManageMessages and ManageRoles Permission).");
                return;
            }
            else if (message == null)
            {
                await Context.Channel.SendMessageAsync("Message is Missing, Please write your message.");
                return;
            }
            else
            {
                await Context.Message.DeleteAsync();
                await Context.Channel.SendMessageAsync(message);
            }
        }
        [Command("copy2"), Alias("say2")]
        [RequireBotPermission(Discord.GuildPermission.ManageMessages)]
        public async Task copywithembed([Remainder] string message = null)
        {
            SocketGuildUser User1 = Context.User as SocketGuildUser;
            if (!User1.GuildPermissions.ManageMessages & !User1.GuildPermissions.ManageRoles)
            {
                await Context.Channel.SendMessageAsync("Your Permissions is Missing (ManageMessages and ManageRoles Permission).");
                return;
            }
            else if (message == null)
            {
                await Context.Channel.SendMessageAsync("Message is Missing, Please write your message.");
                return;
            }
            else
            {
                await Context.Message.DeleteAsync();
                var embed = new EmbedBuilder();
                {
                    embed.WithTitle(""+message+"");
                    int Re = rnd.Next(0, 255);
                    int Ge = rnd.Next(0, 255);
                    int Be = rnd.Next(0, 255);
                    embed.WithColor(new Discord.Color(Re, Ge, Be));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
        }

        [Command("ping"), Alias("latency")]
        public async Task pong()
        {
            var embed = new EmbedBuilder();
            {
                embed.AddField($"Pong!", $"My latency {Context.Client.Latency}ms");
                embed.WithColor(new Discord.Color(0, 255, 255));
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
        }

        [Command("chat")]
        public async Task PMTOCHANNEL(IGuildChannel channel = null, [Remainder] string text = null)
        {
            SocketGuildUser User1 = Context.User as SocketGuildUser;
            if (User1.GuildPermissions.ManageRoles)
            {
                var embed = new EmbedBuilder();
                if (channel == null)
                {
                    embed.AddField($"Error!", "Channel is Missing.");
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
                    await Context.Guild.GetTextChannel(channel.Id).SendMessageAsync("" + text + "");
                }
            }
            else return;
        }

        /*        [Command("test3")]
                public async Task testremoveallrole(ITextChannel Channel, Func<IGuildUser, bool> predicate)
                {
                    IGuildUser[] alluser;
                    alluser = (await Channel.GetUsersAsync().FlattenAsync().ConfigureAwait(false)).Where(predicate).ToArray();
                    {
                            var getall = new List<IGuildUser>();
                            foreach (var x in getall)
                                {
                                     getall.Add(x);
                                }
                            await (getall as IGuildUser).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Joining"));
                    }
                }*/
        [Command("clear")]
        [Alias("clr")]
        [RequireBotPermission(Discord.GuildPermission.ManageMessages)]
        public async Task prune()
        {
            SocketGuildUser User1 = Context.User as SocketGuildUser;
            if (!User1.GuildPermissions.ManageMessages)
            {
                await Context.Channel.SendMessageAsync("Your Permissions is Missing (ManageMessages Permission).");
                return;
            }
            else
            {
                IEnumerable<IMessage> nonPinnedMessages = await Context.Guild.GetTextChannel(Context.Channel.Id).GetMessagesAsync(500).FlattenAsync();
                await Context.Guild.GetTextChannel(Context.Channel.Id).DeleteMessagesAsync(nonPinnedMessages.Where(x => x.IsPinned == false));
            }
        }
        [Command("avatar")]
        public async Task avataruser(SocketUser user = null)
        {
            ImageFormat image = ImageFormat.Auto;
            ushort size = 512;
            var embed = new EmbedBuilder();
            if (user == null || user == Context.User)
            {
                embed.WithAuthor($"Your Avatar!");
                embed.WithImageUrl($"{Context.User.GetAvatarUrl(image, size)}");
                embed.WithFooter($"Requested by {Context.User.Username}", Context.User.GetAvatarUrl());
                embed.WithColor(new Discord.Color(255, 50, 255));
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
            else
            {
                embed.WithAuthor($"{user.Username}#{user.Discriminator}'s Avatar!", user.GetAvatarUrl());
                embed.WithImageUrl(user.GetAvatarUrl(image, size));
                embed.WithFooter($"Requested by {Context.User.Username}", Context.User.GetAvatarUrl());
                embed.WithColor(new Discord.Color(255, 50, 255));
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
        }
        [Command("rmall")]
        [RequireBotPermission(Discord.GuildPermission.ManageRoles)]
        public async Task rmallrolejoining(string text = null) 
        {
            SocketGuildUser User1 = Context.User as SocketGuildUser;
            if (!User1.GuildPermissions.ManageRoles)
            {
                await Context.Channel.SendMessageAsync("Your Permissions is Missing (ManageRoles Permission).");
                return;
            }
            else
            {
                if (Context.User.Id == 454492255932252160)
                {
                    //x.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(b => b.Name == "Joining"));
                    //if (x.Roles.FirstOrDefault(c => c.Name == "Hi"))
                    /*if (Context.Guild.Roles.FirstOrDefault(d => d.Name == "Hi").Members.Contains(x))
                    {
                        await (x as IGuildUser).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(b => b.Name == "Hi"));
                        await Context.Channel.SendMessageAsync("Removed for " + x + "");
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync("No one have Hi role.");
                        return;
                    }
                    users.FindAll(x);*/
                    /*if (Context.Guild.GetUser(x.Id).Roles.Any(a => a.Name == "Hi"))
                        {
                        await x.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(s => s.Name == "Hi"));
                        }
                    else
                    {
                        await Context.Channel.SendMessageAsync("No ome have Hi role");
                        return;
                    }*/
                    //users.Add(x);
                    //var g = Context.Guild.Users.FirstOrDefault(a => a.Username == x + "");
                    if (text == null)
                    {
                        var GetAllUser = Context.Guild.Users;
                        foreach (var x in GetAllUser)
                        {
                            var g = Context.Guild.GetUser(x.Id).Roles.Any(a => a.Name == "Hi");
                            if (g == true)
                            {
                                await Context.Channel.SendMessageAsync("" + g + " (" + x + ")");
                                await  x.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(c => c.Name == "Hi"));
                            }
                        }

                    }
                    else if (text == "owa")
                    {
                        var GetAllUser = Context.Client.GetGuild(465795320526274561).Users;
                        foreach (var x in GetAllUser)
                        {
                            var g = Context.Client.GetGuild(465795320526274561).GetUser(x.Id).Roles.Any(a => a.Name == "Joining");
                            if (g == true)
                            {
                                await  x.RemoveRoleAsync(Context.Client.GetGuild(465795320526274561).Roles.FirstOrDefault(c => c.Name == "Joining"));
                            }
                        }
                    }
                    else return;
                    }
                    //var b = Context.Guild.Users.FirstOrDefault(v => v.Id == users);
                    //await Context.Channel.SendMessageAsync("List: " + b + "");
                    /*while (users != null)
                    {
                        var g = Context.Guild.Users.FirstOrDefault(a => a.Username == x +"");
                        await Context.Channel.SendMessageAsync("" + g + "");                   
                       //var a = Context.Guild.GetUser(x.Id).Roles.FirstOrDefault(ma => ma.Name == "Hi");
                        users.Remove(g);
                    }*/
                    /*await Context.Channel.SendMessageAsync("" + users + "");
                    while (users != null)
                    {
                        var al = Context.Guild.Users.FirstOrDefault(m => m.Nickname == users + "");
                        await al.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(n => n.Name == "Hi"));
                        await Context.Channel.SendMessageAsync("Removed. (" + al + ")");
                        users.Remove(al);
                    }*/
                }
                //await Context.Client.GetGuild(465795320526274561).GetUser(users).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Joining"));
                //var b = Context.Guild.Users;
                //b.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Hi"));
                //await Context.Channel.SendMessageAsync("Executed. ("+users+")");
            }
        /*[Command("tessst")]
        public async Task abc()
        {
            IEnumerable<IGuildUser> test = await Context.Channel.GetUsersAsync().FlattenAsync();
        }*/
        /*[Command("removeroleall")]
        public async Task removerole(IChannel channel)
        {
            IAsyncEnumerator<IReadOnlyCollection<IUser>> getusers = channel.GetUsersAsync().FlattenAsync();
            List<IUser> alluser = new List<IUser>();
            foreach (var x in getusers)
            {

            }
            //await Context.Client.GetGuild(465795320526274561).GetUser(GetUsers).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Jailed"));
        }*/
        /*        [Command("removeroleall")]
                public async Task removerole(IChannel channel, IEnumerable<IGuildUser> getusers)
                {
                    //List<áable<IReadOnlyCollection<IUser>>>[] getusers;
                    //var getusers = channel.GetUsersAsync().FlattenAsync();
                    var alluser = new List<IUser>();
                    foreach (var x in getusers)
                    {
                        alluser.Add(x);
                    }
                    await Context.Channel.SendMessageAsync(""+alluser+"");
                    //await Context.Client.GetGuild(465795320526274561).GetUser(alluser).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Jailed"));
                    //await Context.Channel. 
                }*/

        [Command("listemotes")]
        [Alias("emotelist", "listemote")]
        private async Task showlistofemote([Optional]int num)
        {
            var check = UserAccounts10.GetAccount((Context.User as SocketUser));
            if (check.emote == true)
            {
                var embed = new EmbedBuilder();
                if (num > 2)
                {
                    embed.AddField($"Error!", "List only 1 page.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (num < 0)
                {
                    embed.AddField($"Error!", "Page Nunber can't be lower than 0.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    var maxpage = 2;

                    if (num == 1 || num == 0)
                    {
                        num = 1;
                        embed.WithAuthor($"Haru Neko - Emote List!\n \n");
                        embed.WithDescription($"Use -emote <Number> to use that emote." +
                            $"\n  1 - {Emote.Parse("<:TohruWeary:585492969025568799>")} / 11 - {Emote.Parse("<:GWpdnlaugh:587152172161040396>")}" +
                            $"\n 2 - {Emote.Parse("<:remsleepy:585492968182644758>")} / 12 - {Emote.Parse("<:GWpdnXD:587152173922648070>")}" +
                            $"\n 3 - {Emote.Parse("<:remBlush:585492968228519937>")} / 13 - {Emote.Parse("<:ReimuFacePalm:587152173595623424>")}" +
                            $"\n 4 - {Emote.Parse("<:LoveHeart:585492967880523821>")} / 14 - {Emote.Parse("<:Naisu:587265138843844608>")}" +
                            $"\n 5 - {Emote.Parse("<:Kya:585507397993234556>")} / 15 - {Emote.Parse("<:SenkoThinking:589049035055169536>")}" +
                            $"\n 6 - {Emote.Parse("<:kannaWave:585492969008791580>")} / 16 - {Emote.Parse("<:SenkoPlease:589049212239347753>")}" +
                            $"\n 7 - {Emote.Parse("<:kannaPeek:585492968807464984>")} / 17 - {Emote.Parse("<:SenkoListening:589049338622246922>")}" +
                            $"\n 8 - {Emote.Parse("<:kannanom:585492968962523160>")} / 18 - {Emote.Parse("<:SenkoHi:589049277754507265>")}" +
                            $"\n 9 - {Emote.Parse("<:pillowYes:585492967649837197>")} / 19 - {Emote.Parse("<:SenkoBlush:589049141183905797>")}" +
                            $"\n10 - {Emote.Parse("<:pillowNo:585492968274657300>")} / 20 - {Emote.Parse("<:VampySmug:590121887707693058>")}");
                        embed.WithFooter($"Requested by {Context.User.Username} - Page {num}/{maxpage}", Context.User.GetAvatarUrl());

                        embed.WithColor(new Discord.Color(255, 50, 255));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    if (num == 2)
                    {
                        embed.WithAuthor($"Haru Neko - Emote List!\n \n");
                        embed.WithDescription($"Use -emote <Number> to use that emote." +
                            $"\n21 - {Emote.Parse("<:RaphiOhM:590121887963676673>")}" +
                            $"\n22 - {Emote.Parse("<:RaphiWink:590121888043237390>")}" +
                            $"\n23 - {Emote.Parse("<:WhoDesu:590121888655605762>")}" +
                            $"\n24 - {Emote.Parse("<:WOW:590149641241231384>")}" +
                            $"\n25 - {Emote.Parse("<:WannaSee:590149643376001024>")}" +
                            $"\n26 - {Emote.Parse("<:owoAwoo:590149640356233220>")}");
                        embed.WithFooter($"Requested by {Context.User.Username} - Page {num}/{maxpage}", Context.User.GetAvatarUrl());

                        embed.WithColor(new Discord.Color(255, 50, 255));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                }
            }
            UserAccounts10.SaveAccounts();
        }

        [Command("emote")]
        [Alias("e")]
        private async Task emotetime(int num = 0)
        {
            var check = UserAccounts10.GetAccount((Context.User as SocketUser));
            if (check.emote == true)
            {
                var embed = new EmbedBuilder();
                if (num == 0)
                {
                    embed.AddField($"Emote!", "-emote <Number Emote>");
                    embed.WithColor(new Discord.Color(255, 50, 255));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    if (num == 1)
                    {
                        await Context.Channel.SendMessageAsync($"{Emote.Parse("<:TohruWeary:585492969025568799>")}");
                    }
                    else if (num == 2)
                    {
                        await Context.Channel.SendMessageAsync($"{Emote.Parse("<:remsleepy:585492968182644758>")}");
                    }
                    else if (num == 3)
                    {
                        await Context.Channel.SendMessageAsync($"{Emote.Parse("<:remBlush:585492968228519937>")}");
                    }
                    else if (num == 4)
                    {
                        await Context.Channel.SendMessageAsync($"{Emote.Parse("<:LoveHeart:585492967880523821>")}");
                    }
                    else if (num == 5)
                    {
                        await Context.Channel.SendMessageAsync($"{Emote.Parse("<:Kya:585507397993234556>")}");
                    }
                    else if (num == 6)
                    {
                        await Context.Channel.SendMessageAsync($"{Emote.Parse("<:kannaWave:585492969008791580>")}");
                    }
                    else if (num == 7)
                    {
                        await Context.Channel.SendMessageAsync($"{Emote.Parse("<:kannaPeek:585492968807464984>")}");
                    }
                    else if (num == 8)
                    {
                        await Context.Channel.SendMessageAsync($"{Emote.Parse("<:kannanom:585492968962523160>")}");
                    }
                    else if (num == 9)
                    {
                        await Context.Channel.SendMessageAsync($"{Emote.Parse("<:pillowYes:585492967649837197>")}");
                    }
                    else if (num == 10)
                    {
                        await Context.Channel.SendMessageAsync($"{Emote.Parse("<:pillowNo:585492968274657300>")}");
                    }
                    else if (num == 11)
                    {
                        await Context.Channel.SendMessageAsync($"{Emote.Parse("<:GWpdnlaugh:587152172161040396>")}");
                    }
                    else if (num == 12)
                    {
                        await Context.Channel.SendMessageAsync($"{Emote.Parse("<:GWpdnXD:587152173922648070>")}");
                    }
                    else if (num == 13)
                    {
                        await Context.Channel.SendMessageAsync($"{Emote.Parse("<:ReimuFacePalm:587152173595623424>")}");
                    }
                    else if (num == 14)
                    {
                        await Context.Channel.SendMessageAsync($"{Emote.Parse("<:Naisu:587265138843844608>")}");
                    }
                    else if (num == 15)
                    {
                        await Context.Channel.SendMessageAsync($"{Emote.Parse("<:SenkoThinking:589049035055169536>")}");
                    }
                    else if (num == 16)
                    {
                        await Context.Channel.SendMessageAsync($"{Emote.Parse("<:SenkoPlease:589049212239347753>")}");
                    }
                    else if (num == 17)
                    {
                        await Context.Channel.SendMessageAsync($"{Emote.Parse("<:SenkoListening:589049338622246922>")}");
                    }
                    else if (num == 18)
                    {
                        await Context.Channel.SendMessageAsync($"{Emote.Parse("<:SenkoHi:589049277754507265>")}");
                    }
                    else if (num == 19)
                    {
                        await Context.Channel.SendMessageAsync($"{Emote.Parse("<:SenkoBlush:589049141183905797>")}");
                    }
                    else if (num == 20)
                    {
                        await Context.Channel.SendMessageAsync($"{Emote.Parse("<:VampySmug:590121887707693058>")}");
                    }
                    else if (num == 21)
                    {
                        await Context.Channel.SendMessageAsync($"{Emote.Parse("<:RaphiOhM:590121887963676673>")}");
                    }
                    else if (num == 22)
                    {
                        await Context.Channel.SendMessageAsync($"{Emote.Parse("<:RaphiWink:590121888043237390>")}");
                    }
                    else if (num == 23)
                    {
                        await Context.Channel.SendMessageAsync($"{Emote.Parse("<:WhoDesu:590121888655605762>")}");
                    }
                    else if (num == 24)
                    {
                        await Context.Channel.SendMessageAsync($"{Emote.Parse("<:WOW:590149641241231384>")}");
                    }
                    else if (num == 25)
                    {
                        await Context.Channel.SendMessageAsync($"{Emote.Parse("<:WannaSee:590149643376001024>")}");
                    }
                    else if (num == 26)
                    {
                        await Context.Channel.SendMessageAsync($"{Emote.Parse("<:owoAwoo:590149640356233220>")}");
                    }
                    else
                    {
                        embed.AddField($"Error!", "Emote Number doesn't exist, use -listemote to get list of emotes.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                }
            }
            UserAccounts10.SaveAccounts();
        }






    }
}
