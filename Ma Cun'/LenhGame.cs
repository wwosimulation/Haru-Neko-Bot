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
using Neko_Test.ModulesMaCun;

namespace Neko_Test
{
   public class LenhGame : ModuleBase<SocketCommandContext>
    {
        [Command("batdau"), Alias("start")]
        public async Task batdaugame(string test = null)
        {
            if (Context.Guild.Id == 580555457983152149)
            {
                var embed = new EmbedBuilder();
                SocketGuildUser User1 = Context.User as SocketGuildUser;
                if (!User1.GuildPermissions.ManageRoles)
                {
                    embed.AddField($"Lỗi!", "Người sử dụng cần có Quyền Điều Hành Role.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    OverwritePermissions chophep = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Allow, readMessageHistory: PermValue.Allow);
                    OverwritePermissions khongchophep = new OverwritePermissions(viewChannel: PermValue.Deny, sendMessages: PermValue.Deny, readMessageHistory: PermValue.Deny);
                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580557883931099138).AddPermissionOverwriteAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"), khongchophep.Modify());
                    GlobalFunctionMaCun.gamestatus = 1;
                }
            }
            else return;
        }
        [Command("addrole"), Alias("themvaitro")]
        public async Task themvaitrochonguoichoi(IGuildUser user = null, int num = 0)
        {
            if (Context.Guild.Id == 580555457983152149)
            {
                OverwritePermissions chophep = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Allow, readMessageHistory: PermValue.Allow);
                var embed = new EmbedBuilder();
                SocketGuildUser User1 = Context.User as SocketGuildUser;
                if (!User1.GuildPermissions.ManageRoles)
                {
                    embed.AddField($"Lỗi!", "Người sử dụng cần có Quyền Điều Hành Role.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    if (user == null)
                    {
                        embed.AddField($"Lỗi!", "Bạn chưa chọn Người Chơi để đưa vai trò.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (!Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))
                    {
                        embed.AddField($"Lỗi!", "Người Chơi này không tham gia Game.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (num == 0)
                    {
                        embed.AddField($"Lỗi!", "Bạn chưa chọn vai trò cho Người Chơi này (Sử dụng -danhsachvaitro để hiện ra số của từng vai trò).");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (num <= 0 || num >= 14)
                    {
                        embed.AddField($"Lỗi!", "Vai Trò nằm trong khoảng 1 đến 13 (Sử dụng -danhsachvaitro để hiện ra số của từng vai trò).");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (GlobalFunctionMaCun.plr1 == user.Id || GlobalFunctionMaCun.plr2 == user.Id || GlobalFunctionMaCun.plr3 == user.Id || GlobalFunctionMaCun.plr4 == user.Id || GlobalFunctionMaCun.plr5 == user.Id || GlobalFunctionMaCun.plr6 == user.Id || GlobalFunctionMaCun.plr7 == user.Id || GlobalFunctionMaCun.plr8 == user.Id || GlobalFunctionMaCun.plr9 == user.Id || GlobalFunctionMaCun.plr10 == user.Id || GlobalFunctionMaCun.plr11 == user.Id || GlobalFunctionMaCun.plr12 == user.Id || GlobalFunctionMaCun.plr13 == user.Id)
                    {
                        embed.AddField($"Lỗi", "Người Chơi đã có Vai Trò.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else
                    {
                        if ( GlobalFunctionMaCun.channel1 == 0 & num == 1)//580574363930198021
                        {
                            GlobalFunctionMaCun.plr1 = user.Id;
                            GlobalFunctionMaCun.channel1 = 1;
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - Bảo Vệ.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Bảo Vệ cho "+user+"");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel2 == 0 & num == 2)//580574739391578112
                        {
                            GlobalFunctionMaCun.plr2 = user.Id;
                            GlobalFunctionMaCun.channel2 = 1;
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - Thầy Bói.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574739391578112).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Thầy Bói cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel3 == 0 & num == 3)//580574427712847872
                        {
                            GlobalFunctionMaCun.plr3 = user.Id;
                            GlobalFunctionMaCun.channel3 = 1;
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - Dân Làng.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574427712847872).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Dân Làng cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel4 == 0 & num == 4)//580574451834290176 - 580564753982816256
                        {
                            GlobalFunctionMaCun.plr = user.Id;
                            GlobalFunctionMaCun.channel4 = 1;
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " Sói Thường.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574451834290176).AddPermissionOverwriteAsync(user, chophep.Modify());
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Sói Thường cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel5 == 0 & num == 5)// 580574497514586137
                        {
                            GlobalFunctionMaCun.plr5 = user.Id;
                            GlobalFunctionMaCun.channel5 = 1;
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - Già Làng.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574497514586137).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Già Làng cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel6 == 0 & num == 6)//580574522361774081 - 580564753982816256
                        {
                            GlobalFunctionMaCun.plr6 = user.Id;
                            GlobalFunctionMaCun.channel6 = 1;
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - Sói Phù Thủy.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574522361774081).AddPermissionOverwriteAsync(user, chophep.Modify());
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Sói Phù Thủy cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel7 == 0 & num == 7)//580574545606475782
                        {
                            GlobalFunctionMaCun.plr7 = user.Id;
                            GlobalFunctionMaCun.channel7 = 1;
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - Thợ Săn.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574545606475782).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Thợ Săn cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel8 == 0 & num == 8)//580574572483706891
                        {
                            GlobalFunctionMaCun.plr8 = user.Id;
                            GlobalFunctionMaCun.channel8 = 1;
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - Thằng Ngố.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574572483706891).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Thằng Ngố cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel9 == 0 & num == 9)//580574598677135390
                        {
                            GlobalFunctionMaCun.plr9 = user.Id;
                            GlobalFunctionMaCun.channel9 = 1;
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - Phù Thủy.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Phùy Thủy cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel10 == 0 & num == 10)//580574616645402656
                        {
                            GlobalFunctionMaCun.plr10 = user.Id;
                            GlobalFunctionMaCun.channel10 = 1;
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - Xạ Thủ.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574616645402656).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Xạ Thủ cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel11 == 0 & num == 11)//580574634811064342
                        {
                            GlobalFunctionMaCun.plr11 = user.Id;
                            GlobalFunctionMaCun.channel11 = 1;
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - Sói Băng.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574634811064342).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Sói Băng cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel12 == 0 & num == 12)//580574414660435982
                        {
                            GlobalFunctionMaCun.plr12 = user.Id;
                            GlobalFunctionMaCun.channel12 = 1;
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - Tiên Tri.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574414660435982).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Tiên Tri cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel13 == 0 & num == 13)//580574812662136836
                        {
                            GlobalFunctionMaCun.plr13 = user.Id;
                            GlobalFunctionMaCun.channel13 = 1;
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - Sát Nhân.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Sát Nhân cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else
                        {
                            embed.AddField($"Lỗi", " Vai Trò đã có Người Chơi.");
                            embed.WithColor(new Discord.Color(255, 0, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                    }
                }
            }
            else return;
        }
        [Command("listrole"), Alias("danhsachvaitro")]
        public async Task danhsachvaitro()
        {
            if (Context.Guild.Id == 580555457983152149)
            {
                var embed = new EmbedBuilder();
                SocketGuildUser User1 = Context.User as SocketGuildUser;
                if (!User1.GuildPermissions.ManageRoles)
                {
                    embed.AddField($"Lỗi!", "Người sử dụng cần có Quyền Điều Hành Role.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    embed.AddField($"Danh Sách Vai Trò - Số của từng Vai Trò!", "1. Bảo Vệ\n2. Thầy Bói\n3. Dân\n4. Ma Sói\n5. Già Làng\n6. Sói Phùy Thủy\n7. Thợ Săn\n8. Thằng Ngố\n9. Phù Thủy\n10. Xạ Thủ\n11. Sói Băng\n12. Tiên Tri\n13. Sát Nhân");
                    embed.WithColor(new Discord.Color(255, 50, 255));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
            else return;
        }
   }
}
