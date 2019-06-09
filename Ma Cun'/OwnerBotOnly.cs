﻿using Discord;
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
using System.Globalization;
using System.Windows.Forms;

using Discord.WebSocket;
using System.Diagnostics;

namespace Neko_Test.Ma_Cun_
{
    public class OwnerBotOnly : ModuleBase<SocketCommandContext>
    {
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
                    embed.AddField($"Error!", "Number1 is Missing! / 1-Coin _ 2-Roses _ 3-PlrRoses");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (Number2 == 0)
                {
                    embed.AddField($"Error!", "Number2 is Missing! / 1-Add _ 2-Remove");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (Number3 == 0)
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
                        embed.AddField($"Banned!", "Ban Successfully!\nUser name: "+User.Username+"\nUser ID: "+User.Id+"\nReason: "+Reason+"");
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
    }
}
