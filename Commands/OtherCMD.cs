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
                    embed.WithTitle($""+message+"");
                    int Re = rnd.Next(0, 255);
                    int Ge = rnd.Next(0, 255);
                    int Be = rnd.Next(0, 255);
                    embed.WithColor(new Discord.Color(Re, Ge, Be));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
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
    }
}
