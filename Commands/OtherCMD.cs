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
                if (num > 1)
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
                    if (num == 1 || num == 0)
                    {
                        num = 1;
                        embed.WithAuthor($"Haru Neko - Emote List!\n \n");
                        embed.WithDescription($"Use -emote <Number> to use that emote.\n1 - {Emote.Parse("<:TohruWeary:585492969025568799>")}\n2 - {Emote.Parse("<:remsleepy:585492968182644758>")}\n3 - {Emote.Parse("<:remBlush:585492968228519937>")}\n4 - {Emote.Parse("<:LoveHeart:585492967880523821>")}\n5 - {Emote.Parse("<:Kya:585507397993234556>")}\n6 - {Emote.Parse("<:kannaWave:585492969008791580>")}\n7 - {Emote.Parse("<:kannaPeek:585492968807464984>")}\n8 - {Emote.Parse("<:kannanom:585492968962523160>")}\n9 - {Emote.Parse("<:pillowYes:585492967649837197>")}\n10 - {Emote.Parse("<:pillowNo:585492968274657300>")}\n");
                        embed.WithFooter($"Requested by {Context.User.Username} - Page 1/1", Context.User.GetAvatarUrl());

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
