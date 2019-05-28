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

namespace Neko_Test.Ma_Cun_
{
    public class LenhQuanTro : ModuleBase<SocketCommandContext>
    {
        [Command("quantro")]
        [RequireBotPermission(Discord.GuildPermission.ManageRoles)]
        public async Task quantro()
        {
            var embed = new EmbedBuilder();
            if (Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 580555887324954635)
            {
                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Quản Trò - Game").Members.Contains(Context.User))
                {
                    await (Context.User as IGuildUser).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Quản Trò - Game"));
                }
                else if (!Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User) & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Quản Trò").Members.Contains(Context.User))
                {
                    await (Context.User as IGuildUser).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Quản Trò - Game"));
                }
                else if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User) & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Quản Trò").Members.Contains(Context.User))
                {
                    embed.AddField($"Lỗi!", "Bạn không thể sử dụng lệnh này khi đã tham gia.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    embed.AddField($"Lỗi!", "Người sử dụng cần có role Quản Trò.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
            else return;
        }
        [Command("mogame")]
        public async Task mogame()
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
                    GlobalFunctionMaCun.mogame = "damo";
                    embed.AddField("Hệ Thống!", "Đã cho phép người chơi tham gia đăng ký Game.");
                    embed.WithColor(new Discord.Color(0, 255, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
            else return;
        }
        [Command("donggame")]
        public async Task donggame()
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
                    GlobalFunctionMaCun.mogame = "donggame";
                    embed.AddField("Hệ Thống!", "Đã hủy cho phép người chơi tham gia đăng ký Game.");
                    embed.WithColor(new Discord.Color(0, 255, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
            else return;
        }
        [Command("thamgia")]
        [RequireBotPermission(Discord.GuildPermission.ManageRoles)]
        public async Task thamgia()
        {
            var embed = new EmbedBuilder();
            if (Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 580555887324954635)
            {
                if (GlobalFunctionMaCun.gamestatus != 0)
                {
                    embed.AddField($"Lỗi!", "Game đã bắt đầu nên bạn không thể Tham Gia (Dùng -khangia nếu bạn muốn xem).");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (GlobalFunctionMaCun.mogame == null)
                {
                    embed.AddField($"Lỗi!", "Không có game nào được mở nên không bạn thể tham gia.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (GlobalFunctionMaCun.mogame == "donggame")
                {
                    embed.AddField($"Lỗi!", "Quản Trò đã chốt người tham gia nên không bạn thể đăng ký tham gia.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (GlobalFunctionMaCun.gamestatus == 0 & !Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User) & !Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết").Members.Contains(Context.User))
                {
                    int checkplayer = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Count();
                    checkplayer++;
                    if (checkplayer <= 13)
                    {
                        await (Context.User as IGuildUser).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Người Chơi"));
                        await (Context.User as IGuildUser).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                        await (Context.User as IGuildUser).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Khán Giả"));
                        await (Context.User as IGuildUser).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Quản Trò - Game"));
                        var guild = Context.Client.GetGuild(580555457983152149);
                        var user = guild.GetUser(Context.User.Id);

                        await user.ModifyAsync(x => x.Nickname = checkplayer.ToString());
                    }
                    else
                    {
                        embed.AddField($"Lỗi!", "Game đã đủ Người Chơi nên bạn không thể Tham Gia (Dùng -khangia nếu bạn muốn xem).");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                }
                else return;
            }
            else return;
        }
        [Command("khangia")]
        [RequireBotPermission(Discord.GuildPermission.ManageRoles)]
        public async Task khangia()
        {
            var embed = new EmbedBuilder();
            if (Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 580555887324954635)
            {
                if (!Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User) & !Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết").Members.Contains(Context.User))
                {
                    await (Context.User as IGuildUser).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Người Chơi"));
                    await (Context.User as IGuildUser).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Khán Giả"));
                }
                else if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết").Members.Contains(Context.User))
                {
                    embed.AddField($"Lỗi!", "Bạn không thể sử dụng lệnh này khi đã Chết.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else return;
            }
            else return;
        }
        [Command("batdau"), Alias("start")]
        public async Task batdaugame()
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
                    int checkplayer = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Count();
                    GlobalFunctionMaCun.plr = checkplayer;
                    GlobalFunctionMaCun.thuoccuu = 1;
                    GlobalFunctionMaCun.thuocdoc = 1;
                    GlobalFunctionMaCun.khien = 1;
                    GlobalFunctionMaCun.danxathu = 1;
                    GlobalFunctionMaCun.daycount = 1;
                    GlobalFunctionMaCun.gialang = 1;
                    GlobalFunctionMaCun.thayboi = 1;
                    GlobalFunctionMaCun.tientri = 1;
                    GlobalFunctionMaCun.lastdongbang = 1;
                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm thứ Nhất Bắt Đầu!");
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
                        if (GlobalFunctionMaCun.channel1 == 0 & num == 1 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))//580574363930198021
                        {
                            GlobalFunctionMaCun.plr1 = user.Id;
                            GlobalFunctionMaCun.channel1 = 1;
                            GlobalFunctionMaCun.phedan++;
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - Bảo Vệ.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Bảo Vệ cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel2 == 0 & num == 2 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))//580574739391578112
                        {
                            GlobalFunctionMaCun.plr2 = user.Id;
                            GlobalFunctionMaCun.channel2 = 1;
                            GlobalFunctionMaCun.phedan++;
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - Thầy Bói.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574739391578112).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Thầy Bói cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel3 == 0 & num == 3 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))//580574427712847872
                        {
                            GlobalFunctionMaCun.plr3 = user.Id;
                            GlobalFunctionMaCun.channel3 = 1;
                            GlobalFunctionMaCun.phedan++;
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - Dân Làng.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574427712847872).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Dân Làng cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel4 == 0 & num == 4 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))//580574451834290176 - 580564753982816256
                        {
                            GlobalFunctionMaCun.plr4 = user.Id;
                            GlobalFunctionMaCun.channel4 = 1;
                            GlobalFunctionMaCun.phesoi++;
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - Sói Thường.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574451834290176).AddPermissionOverwriteAsync(user, chophep.Modify());
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Sói Thường cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel5 == 0 & num == 5 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))// 580574497514586137
                        {
                            GlobalFunctionMaCun.plr5 = user.Id;
                            GlobalFunctionMaCun.channel5 = 1;
                            GlobalFunctionMaCun.phedan++;
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - Già Làng.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574497514586137).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Già Làng cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel6 == 0 & num == 6 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))//580574522361774081 - 580564753982816256
                        {
                            GlobalFunctionMaCun.plr6 = user.Id;
                            GlobalFunctionMaCun.channel6 = 1;
                            GlobalFunctionMaCun.phesoi++;
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - Sói Phù Thủy.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574522361774081).AddPermissionOverwriteAsync(user, chophep.Modify());
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Sói Phù Thủy cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel7 == 0 & num == 7 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))//580574545606475782
                        {
                            GlobalFunctionMaCun.plr7 = user.Id;
                            GlobalFunctionMaCun.channel7 = 1;
                            GlobalFunctionMaCun.phedan++;
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - Thợ Săn.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574545606475782).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Thợ Săn cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel8 == 0 & num == 8 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))//580574572483706891
                        {
                            GlobalFunctionMaCun.plr8 = user.Id;
                            GlobalFunctionMaCun.channel8 = 1;
                            GlobalFunctionMaCun.phedan++;
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - Thằng Ngố.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574572483706891).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Thằng Ngố cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel9 == 0 & num == 9 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))//580574598677135390
                        {
                            GlobalFunctionMaCun.plr9 = user.Id;
                            GlobalFunctionMaCun.channel9 = 1;
                            GlobalFunctionMaCun.phedan++;
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - Phù Thủy.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Phùy Thủy cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel10 == 0 & num == 10 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))//580574616645402656
                        {
                            GlobalFunctionMaCun.plr10 = user.Id;
                            GlobalFunctionMaCun.channel10 = 1;
                            GlobalFunctionMaCun.phedan++;
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - Xạ Thủ.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574616645402656).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Xạ Thủ cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel11 == 0 & num == 11 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))//580574634811064342
                        {
                            GlobalFunctionMaCun.plr11 = user.Id;
                            GlobalFunctionMaCun.channel11 = 1;
                            GlobalFunctionMaCun.phesoi++;
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - Sói Băng.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574634811064342).AddPermissionOverwriteAsync(user, chophep.Modify());
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Sói Băng cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel12 == 0 & num == 12 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))//580574414660435982
                        {
                            GlobalFunctionMaCun.plr12 = user.Id;
                            GlobalFunctionMaCun.channel12 = 1;
                            GlobalFunctionMaCun.phedan++;
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - Tiên Tri.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574414660435982).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Tiên Tri cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel13 == 0 & num == 13 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))//580574812662136836
                        {
                            GlobalFunctionMaCun.plr13 = user.Id;
                            GlobalFunctionMaCun.channel13 = 1;
                            GlobalFunctionMaCun.phethu3++;
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
                    embed.AddField($"Danh Sách Vai Trò - Số của từng Vai Trò!", "1. Bảo Vệ - Thiện\n2. Thầy Bói - Thện\n3. Dân - Thiện\n4. Ma Sói - Ác\n5. Già Làng - Thiện\n6. Sói Phùy Thủy - Ác\n7. Thợ Săn - Không Rõ\n8. Thằng Ngố - Không Rõ\n9. Phù Thủy - Thiện\n10. Xạ Thủ - Không Rõ\n11. Sói Băng - Không Rõ\n12. Tiên Tri - Thiện\n13. Sát Nhân - Không Rõ");
                    embed.WithColor(new Discord.Color(255, 50, 255));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
            else return;
        }
        [Command("treo")]
        public async Task treonguoichoi(IGuildUser user)
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
                else if (user == null)
                {
                    embed.AddField($"Hệ Thống - Lệnh Quản Trò", "-treo (Số Người Chơi Bị Nhiều Phiếu Treo)");
                    embed.WithColor(new Discord.Color(255, 50, 255));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))
                {
                    if (GlobalFunctionMaCun.gamestatus >= 3)
                    {
                        if (user.Id == GlobalFunctionMaCun.treo)
                        {
                            Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Do Quản Trò có sự nhầm lẫn nên đã bỏ treo " + Context.Guild.GetUser(GlobalFunctionMaCun.treo).Nickname + ".");
                            GlobalFunctionMaCun.treo = 0;
                            GlobalFunctionMaCun.gamestatus = 3;
                        }
                        else
                        {
                            GlobalFunctionMaCun.treo = user.Id;
                            Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Dân Làng đã đưa " + user.Mention + " lên giá treo, Bây giờ " + user.Mention + " phải đưa ra lý do để không bị giết.");
                        }
                    }
                    else
                    {
                        embed.AddField($"Lỗi!", "Sử dụng -vote để bắt đầu bỏ phiếu trước khi sử dụng lệnh này.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                }
                else
                {
                    embed.AddField($"Lỗi!", "Người Chơi đó không tham gia hoặc đã chết.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
            else return;
        }
        [Command("vote")]
        public async Task votetwice()
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
                    if (GlobalFunctionMaCun.gamestatus == 2)
                    {
                        await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("" + Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Mention + ", Hãy bắt đầu bỏ phiếu cho người chơi lên giá treo, sử dụng -bophieu (Số Người Chơi Muốn Lên Giá Treo) trong Channel riêng để bỏ phiếu.");
                        GlobalFunctionMaCun.gamestatus++;
                    }
                    else if (GlobalFunctionMaCun.treo != 0 & GlobalFunctionMaCun.gamestatus == 3)
                    {
                        await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("" + Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Mention + ", Hãy bắt đầu bỏ phiếu sống và chết cho " + Context.Guild.GetUser(GlobalFunctionMaCun.treo).Nickname + ", sử dụng -song [Để Sống] hoặc -chet [Để Giết] trong Channel riêng để bỏ phiếu.");
                        GlobalFunctionMaCun.gamestatus++;
                    }
                    else if (GlobalFunctionMaCun.treo == 0 & GlobalFunctionMaCun.gamestatus == 3)
                    {
                        embed.AddField($"Lỗi!", "Nếu không ai bị treo thì sử dụng -dem để bắt đầu cho Dân Làng ngủ.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (GlobalFunctionMaCun.gamestatus == 1 || GlobalFunctionMaCun.gamestatus == 0)
                    {
                        embed.AddField($"Lỗi!", "Đang Đêm hoặc Chưa bắt đầu.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else
                    {
                        embed.AddField($"Lỗi!", "Sau khi người chơi bỏ phiếu sống hoặc chết thì hãy dùng -dem để bắt đầu cho Dân Làng ngủ (Tính theo phiếu bầu).");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                }
            }
            else return;
        }
        [Command("bophieu")]
        public async Task bophieunguoichoi(IGuildUser user = null)
        {
            if (Context.Guild.Id == 580555457983152149)
            {
                var embed = new EmbedBuilder();
                if (Context.Channel.Id != 580563096544739331 || Context.Channel.Id != 580557883931099138 || Context.Channel.Id != 580555887324954635 || Context.Channel.Id != 580565718987309101 || Context.Channel.Id != 580564753982816256)
                {
                    if (user == null)
                    {
                        embed.AddField($"Hệ Thống!", "-bophieu (Số Người Chơi Muốn Bỏ Phiếu)");
                        embed.WithColor(new Discord.Color(255, 50, 255));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    if (user.Id == Context.User.Id)
                    {
                        embed.AddField($"Lỗi!", "Bạn không thể dùng lệnh này với bản thân.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (GlobalFunctionMaCun.gamestatus == 0 || GlobalFunctionMaCun.gamestatus == 1 || GlobalFunctionMaCun.gamestatus == 2)
                    {
                        embed.AddField($"Lỗi!", "Bạn không thể bỏ phiếu vào thời gian này.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (GlobalFunctionMaCun.gamestatus >= 4)
                    {
                        embed.AddField($"Lỗi!", "Việc bỏ phiếu đã qua nên bạn không thể bỏ phiếu vào thời gian này.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (!Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User))
                    {
                        embed.AddField($"Lỗi!", "Bạn không thể bỏ phiếu khi đã chết.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (!Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))
                    {
                        embed.AddField($"Lỗi!", "Người đó không tham gia hoặc đã chết.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else
                    {
                        await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu treo " + Context.Guild.GetUser(user.Id).Nickname + "");
                        return;
                    }
                }
                else
                {
                    embed.AddField($"Lỗi!", "Lệnh này chỉ được dùng trong Channel riêng.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
            else return;
        }
        [Command("song")]
        public async Task bophieusongchonguoichoi()
        {
            if (Context.Guild.Id == 580555457983152149)
            {
                var embed = new EmbedBuilder();
                if (Context.Channel.Id != 580563096544739331 || Context.Channel.Id != 580557883931099138 || Context.Channel.Id != 580555887324954635 || Context.Channel.Id != 580565718987309101 || Context.Channel.Id != 580564753982816256)
                {
                    if (GlobalFunctionMaCun.gamestatus == 0 || GlobalFunctionMaCun.gamestatus == 1 || GlobalFunctionMaCun.gamestatus == 2 || GlobalFunctionMaCun.gamestatus == 3)
                    {
                        embed.AddField($"Lỗi!", "Bạn không thể bỏ phiếu sống hoặc chết vào thời gian này.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (GlobalFunctionMaCun.treo == Context.User.Id)
                    {
                        embed.AddField($"Lỗi!", "Bạn không thể bỏ phiếu khi bị lên giá treo.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (!Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User))
                    {
                        embed.AddField($"Lỗi!", "Bạn không thể bỏ phiếu khi đã chết.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else
                    {
                        if (GlobalFunctionMaCun.channel1 == 1 & GlobalFunctionMaCun.plr1 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel1++;
                            GlobalFunctionMaCun.votesong++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Sống__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel2 == 1 & GlobalFunctionMaCun.plr2 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel2++;
                            GlobalFunctionMaCun.votesong++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Sống__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel3 == 1 & GlobalFunctionMaCun.plr3 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel3++;
                            GlobalFunctionMaCun.votesong++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Sống__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel4 == 1 & GlobalFunctionMaCun.plr4 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel4++;
                            GlobalFunctionMaCun.votesong++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Sống__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel5 == 1 & GlobalFunctionMaCun.plr5 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel5++;
                            GlobalFunctionMaCun.votesong++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Sống__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel6 == 1 & GlobalFunctionMaCun.plr6 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel6++;
                            GlobalFunctionMaCun.votesong++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Sống__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel7 == 1 & GlobalFunctionMaCun.plr7 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel7++;
                            GlobalFunctionMaCun.votesong++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Sống__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel8 == 1 & GlobalFunctionMaCun.plr8 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel8++;
                            GlobalFunctionMaCun.votesong++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Sống__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel9 == 1 & GlobalFunctionMaCun.plr9 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel9++;
                            GlobalFunctionMaCun.votesong++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Sống__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel10 == 1 & GlobalFunctionMaCun.plr10 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel10++;
                            GlobalFunctionMaCun.votesong++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Sống__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel11 == 1 & GlobalFunctionMaCun.plr11 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel11++;
                            GlobalFunctionMaCun.votesong++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Sống__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel12 == 1 & GlobalFunctionMaCun.plr12 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel12++;
                            GlobalFunctionMaCun.votesong++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Sống__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel13 == 1 & GlobalFunctionMaCun.plr13 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel13++;
                            GlobalFunctionMaCun.votesong++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Sống__**.");
                            return;
                        }
                        else
                        {
                            embed.AddField($"Lỗi!", "Bạn đã bỏ phiếu rồi.");
                            embed.WithColor(new Discord.Color(255, 0, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                    }
                }
                else
                {
                    embed.AddField($"Lỗi!", "Lệnh này chỉ được dùng trong Channel riêng.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
            else return;
        }
        [Command("chet")]
        public async Task bophieuchetchonguoichoi()
        {
            if (Context.Guild.Id == 580555457983152149)
            {
                var embed = new EmbedBuilder();
                if (Context.Channel.Id != 580563096544739331 || Context.Channel.Id != 580557883931099138 || Context.Channel.Id != 580555887324954635 || Context.Channel.Id != 580565718987309101 || Context.Channel.Id != 580564753982816256)
                {
                    if (GlobalFunctionMaCun.gamestatus == 0 || GlobalFunctionMaCun.gamestatus == 1 || GlobalFunctionMaCun.gamestatus == 2 || GlobalFunctionMaCun.gamestatus == 3)
                    {
                        embed.AddField($"Lỗi!", "Bạn không thể bỏ phiếu sống hoặc chết vào thời gian này.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (GlobalFunctionMaCun.treo == Context.User.Id)
                    {
                        embed.AddField($"Lỗi!", "Bạn không thể bỏ phiếu khi bị lên giá treo.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (!Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User))
                    {
                        embed.AddField($"Lỗi!", "Bạn không thể bỏ phiếu khi đã chết.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else
                    {
                        if (GlobalFunctionMaCun.channel1 == 1 & GlobalFunctionMaCun.plr1 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel1++;
                            GlobalFunctionMaCun.votechet++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Chết__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel2 == 1 & GlobalFunctionMaCun.plr2 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel2++;
                            GlobalFunctionMaCun.votechet++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Chết__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel3 == 1 & GlobalFunctionMaCun.plr3 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel3++;
                            GlobalFunctionMaCun.votechet++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Chết__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel4 == 1 & GlobalFunctionMaCun.plr4 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel4++;
                            GlobalFunctionMaCun.votechet++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Chết__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel5 == 1 & GlobalFunctionMaCun.plr5 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel5++;
                            GlobalFunctionMaCun.votechet++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Chết__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel6 == 1 & GlobalFunctionMaCun.plr6 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel6++;
                            GlobalFunctionMaCun.votechet++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Chết__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel7 == 1 & GlobalFunctionMaCun.plr7 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel7++;
                            GlobalFunctionMaCun.votechet++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Chết__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel8 == 1 & GlobalFunctionMaCun.plr8 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel8++;
                            GlobalFunctionMaCun.votechet++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Chết__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel9 == 1 & GlobalFunctionMaCun.plr9 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel9++;
                            GlobalFunctionMaCun.votechet++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Chết__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel10 == 1 & GlobalFunctionMaCun.plr10 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel10++;
                            GlobalFunctionMaCun.votechet++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Chết__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel11 == 1 & GlobalFunctionMaCun.plr11 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel11++;
                            GlobalFunctionMaCun.votechet++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Chết__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel12 == 1 & GlobalFunctionMaCun.plr12 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel12++;
                            GlobalFunctionMaCun.votechet++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Chết__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel13 == 1 & GlobalFunctionMaCun.plr13 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel13++;
                            GlobalFunctionMaCun.votechet++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Chết__**.");
                            return;
                        }
                        else
                        {
                            embed.AddField($"Lỗi!", "Bạn đã bỏ phiếu rồi.");
                            embed.WithColor(new Discord.Color(255, 0, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                    }
                }
                else
                {
                    embed.AddField($"Lỗi!", "Lệnh này chỉ được dùng trong Channel riêng.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
            else return;
        }
        [Command("dem")]
        public async Task calangngu()
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
                    if (GlobalFunctionMaCun.treo == 0 & GlobalFunctionMaCun.gamestatus == 3)
                    {
                        OverwritePermissions chophep = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Allow);
                        OverwritePermissions khongchophep = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Deny);
                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).AddPermissionOverwriteAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"), khongchophep.Modify());
                        await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Vì Dân làng không chọn người để treo hoặc phiếu bị hòa nên cả làng đi ngủ.");
                        GlobalFunctionMaCun.channel1 = 1;
                        GlobalFunctionMaCun.channel2 = 1;
                        GlobalFunctionMaCun.channel3 = 1;
                        GlobalFunctionMaCun.channel4 = 1;
                        GlobalFunctionMaCun.channel5 = 1;
                        GlobalFunctionMaCun.channel6 = 1;
                        GlobalFunctionMaCun.channel7 = 1;
                        GlobalFunctionMaCun.channel8 = 1;
                        GlobalFunctionMaCun.channel9 = 1;
                        GlobalFunctionMaCun.channel10 = 1;
                        GlobalFunctionMaCun.channel11 = 1;
                        GlobalFunctionMaCun.channel12 = 1;
                        GlobalFunctionMaCun.channel13 = 1;
                        GlobalFunctionMaCun.gamestatus = 1;
                        GlobalFunctionMaCun.thayboi = 1;
                        GlobalFunctionMaCun.tientri = 1;
                        GlobalFunctionMaCun.votesong = 0;
                        GlobalFunctionMaCun.votechet = 0;
                        GlobalFunctionMaCun.daycount++;
                        if (GlobalFunctionMaCun.dongbang != 0)
                        {
                            GlobalFunctionMaCun.lastdongbang = GlobalFunctionMaCun.dongbang;
                        }
                        if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr1)
                        {
                            await Context.Guild.GetTextChannel(580574363930198021).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                        }
                        else if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr2)
                        {
                            await Context.Guild.GetTextChannel(580574739391578112).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                        }
                        else if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr7)
                        {
                            await Context.Guild.GetTextChannel(580574545606475782).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                        }
                        else if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr9)
                        {
                            await Context.Guild.GetTextChannel(580574598677135390).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                        }
                        else if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr12)
                        {
                            await Context.Guild.GetTextChannel(580574414660435982).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                        }
                        else if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr13)
                        {
                            await Context.Guild.GetTextChannel(580574812662136836).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                        }
                        else return;
                    }
                    else if (GlobalFunctionMaCun.treo != 0 & GlobalFunctionMaCun.gamestatus == 3)
                    {
                        embed.AddField($"Lỗi!", "Vì có người bị treo nên sử dụng -vote để bỏ phiếu sống hoặc chết trước khi đêm (Nếu có sự nhầm lẫn trong việc treo thì sử dụng -treo [Số Người Chơi Vừa Bị Treo] để bỏ người bị treo).");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (GlobalFunctionMaCun.treo != 0 & GlobalFunctionMaCun.gamestatus == 4)
                    {
                        if (GlobalFunctionMaCun.votesong >= GlobalFunctionMaCun.votechet)
                        {
                            OverwritePermissions khongchophep = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Deny);
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).AddPermissionOverwriteAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"), khongchophep.Modify());
                            await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Vì số phiếu Sống nhiều hơn số phiếu Chết hoặc cả 2 bằng nhau nên cả làng đi ngủ.");
                            GlobalFunctionMaCun.channel1 = 1;
                            GlobalFunctionMaCun.channel2 = 1;
                            GlobalFunctionMaCun.channel3 = 1;
                            GlobalFunctionMaCun.channel4 = 1;
                            GlobalFunctionMaCun.channel5 = 1;
                            GlobalFunctionMaCun.channel6 = 1;
                            GlobalFunctionMaCun.channel7 = 1;
                            GlobalFunctionMaCun.channel8 = 1;
                            GlobalFunctionMaCun.channel9 = 1;
                            GlobalFunctionMaCun.channel10 = 1;
                            GlobalFunctionMaCun.channel11 = 1;
                            GlobalFunctionMaCun.channel12 = 1;
                            GlobalFunctionMaCun.channel13 = 1;
                            GlobalFunctionMaCun.gamestatus = 1;
                            GlobalFunctionMaCun.votesong = 0;
                            GlobalFunctionMaCun.votechet = 0;
                            GlobalFunctionMaCun.treo = 0;
                            GlobalFunctionMaCun.thayboi = 1;
                            GlobalFunctionMaCun.tientri = 1;
                            GlobalFunctionMaCun.daycount++;
                            if (GlobalFunctionMaCun.dongbang != 0)
                            {
                                GlobalFunctionMaCun.lastdongbang = GlobalFunctionMaCun.dongbang;
                            }
                            if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr1)
                            {
                                await Context.Guild.GetTextChannel(580574363930198021).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr2)
                            {
                                await Context.Guild.GetTextChannel(580574739391578112).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr7)
                            {
                                await Context.Guild.GetTextChannel(580574545606475782).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr9)
                            {
                                await Context.Guild.GetTextChannel(580574598677135390).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr12)
                            {
                                await Context.Guild.GetTextChannel(580574414660435982).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr13)
                            {
                                await Context.Guild.GetTextChannel(580574812662136836).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else return;
                        }
                        else
                        {
                            OverwritePermissions khongchophep = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Deny);
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).AddPermissionOverwriteAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"), khongchophep.Modify());
                            if (GlobalFunctionMaCun.treo == GlobalFunctionMaCun.plr1)
                            {
                                await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Dân Làng đã treo và giết chết " + Context.Guild.GetUser(GlobalFunctionMaCun.treo).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.treo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                await Context.Guild.GetUser(GlobalFunctionMaCun.treo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                GlobalFunctionMaCun.phedan--;
                            }
                            else if (GlobalFunctionMaCun.treo == GlobalFunctionMaCun.plr4)
                            {
                                await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Dân Làng đã treo và giết chết " + Context.Guild.GetUser(GlobalFunctionMaCun.treo).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.treo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                await Context.Guild.GetUser(GlobalFunctionMaCun.treo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                GlobalFunctionMaCun.phesoi--;
                            }
                            else if (GlobalFunctionMaCun.treo == GlobalFunctionMaCun.plr6)
                            {
                                await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Dân Làng đã treo và giết chết " + Context.Guild.GetUser(GlobalFunctionMaCun.treo).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.treo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                await Context.Guild.GetUser(GlobalFunctionMaCun.treo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                GlobalFunctionMaCun.phesoi--;
                            }
                            else if (GlobalFunctionMaCun.treo == GlobalFunctionMaCun.plr7)
                            {
                                await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Dân Làng đã treo và giết chết " + Context.Guild.GetUser(GlobalFunctionMaCun.treo).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.treo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                await Context.Guild.GetUser(GlobalFunctionMaCun.treo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.keo != 0)
                                {
                                    if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr1)
                                    {
                                        await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phedan--;
                                    }
                                    else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr4)
                                    {
                                        await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phesoi--;
                                    }
                                    else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr6)
                                    {
                                        await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phesoi--;
                                    }
                                    else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr9)
                                    {
                                        await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phedan--;
                                    }
                                    else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr11)
                                    {
                                        await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phesoi--;
                                    }
                                    else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr13)
                                    {
                                        await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phethu3--;
                                    }
                                    else
                                    {
                                        await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phedan--;
                                    }
                                }
                            }
                            else if (GlobalFunctionMaCun.treo == GlobalFunctionMaCun.plr11)
                            {
                                await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Dân Làng đã treo và giết chết " + Context.Guild.GetUser(GlobalFunctionMaCun.treo).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.treo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                await Context.Guild.GetUser(GlobalFunctionMaCun.treo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                GlobalFunctionMaCun.phesoi--;
                            }
                            else if (GlobalFunctionMaCun.treo == GlobalFunctionMaCun.plr13)
                            {
                                await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Dân Làng đã treo và giết chết " + Context.Guild.GetUser(GlobalFunctionMaCun.treo).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.treo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                await Context.Guild.GetUser(GlobalFunctionMaCun.treo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                GlobalFunctionMaCun.phethu3--;
                            }
                            else
                            {
                                await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Dân Làng đã treo và giết chết " + Context.Guild.GetUser(GlobalFunctionMaCun.treo).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.treo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                await Context.Guild.GetUser(GlobalFunctionMaCun.treo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                GlobalFunctionMaCun.phedan--;
                            }
                            if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr1)
                            {
                                await Context.Guild.GetTextChannel(580574363930198021).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr2)
                            {
                                await Context.Guild.GetTextChannel(580574739391578112).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr7)
                            {
                                await Context.Guild.GetTextChannel(580574545606475782).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr9)
                            {
                                await Context.Guild.GetTextChannel(580574598677135390).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr12)
                            {
                                await Context.Guild.GetTextChannel(580574414660435982).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr13)
                            {
                                await Context.Guild.GetTextChannel(580574812662136836).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            Task.Delay(1000);
                            GlobalFunctionMaCun.channel1 = 1;
                            GlobalFunctionMaCun.channel2 = 1;
                            GlobalFunctionMaCun.channel3 = 1;
                            GlobalFunctionMaCun.channel4 = 1;
                            GlobalFunctionMaCun.channel5 = 1;
                            GlobalFunctionMaCun.channel6 = 1;
                            GlobalFunctionMaCun.channel7 = 1;
                            GlobalFunctionMaCun.channel8 = 1;
                            GlobalFunctionMaCun.channel9 = 1;
                            GlobalFunctionMaCun.channel10 = 1;
                            GlobalFunctionMaCun.channel11 = 1;
                            GlobalFunctionMaCun.channel12 = 1;
                            GlobalFunctionMaCun.channel13 = 1;
                            GlobalFunctionMaCun.gamestatus = 1;
                            GlobalFunctionMaCun.votesong = 0;
                            GlobalFunctionMaCun.votechet = 0;
                            GlobalFunctionMaCun.treo = 0;
                            GlobalFunctionMaCun.thayboi = 1;
                            GlobalFunctionMaCun.tientri = 1;
                            GlobalFunctionMaCun.daycount++;
                            if (GlobalFunctionMaCun.dongbang != 0)
                            {
                                GlobalFunctionMaCun.lastdongbang = GlobalFunctionMaCun.dongbang;
                            }
                        }
                    }
                    else return;
                }
            }
            else return;
        }
        [Command("sang")]
        public async Task calangday()
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
                else if (!Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.can)) & !Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.dam)))
                {
                    OverwritePermissions chophep = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Allow);
                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).AddPermissionOverwriteAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"), chophep.Modify());
                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ngày Thứ " + GlobalFunctionMaCun.daycount + " bắt đầu, " + Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Mention + " dậy thảo luận.");
                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                    GlobalFunctionMaCun.can = 0;
                    GlobalFunctionMaCun.dam = 0;
                    GlobalFunctionMaCun.cuu = 0;
                    GlobalFunctionMaCun.baoveplr = 0;
                    GlobalFunctionMaCun.dongbang = 0;
                    GlobalFunctionMaCun.phuphep = 0;
                    GlobalFunctionMaCun.gamestatus = 2;
                }
                else
                {
                    var song = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống");
                    var chet = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết");
                    OverwritePermissions chophep = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Allow);
                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).AddPermissionOverwriteAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"), chophep.Modify());
                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ngày Thứ " + GlobalFunctionMaCun.daycount + " bắt đầu, " + Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Mention + " dậy thảo luận.");
                    if (GlobalFunctionMaCun.dam != 0)
                    {
                        if (GlobalFunctionMaCun.dam == GlobalFunctionMaCun.can)
                        {
                            if (GlobalFunctionMaCun.plr2 == GlobalFunctionMaCun.dam)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr2)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr2)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr2)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr2).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr2).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr2).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                            }
                            else if (GlobalFunctionMaCun.plr3 == GlobalFunctionMaCun.dam)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr3)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr3)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr3)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr3).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr3).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr3).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                            }
                            else if (GlobalFunctionMaCun.plr4 == GlobalFunctionMaCun.dam)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr4)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr4)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr4)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr4).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr4).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr4).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phesoi--;
                                }
                            }
                            else if (GlobalFunctionMaCun.plr5 == GlobalFunctionMaCun.dam)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr5)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr5)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr5)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else if (GlobalFunctionMaCun.gialang >= 1)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574497514586137).SendMessageAsync("Đêm qua bạn đã bị tấn công, nếu đợt tiếp theo bị tấn công bạn sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.gialang--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr5).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr5).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr5).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                            }
                            else if (GlobalFunctionMaCun.plr6 == GlobalFunctionMaCun.dam)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr6)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr6)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr6)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr6).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr6).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr6).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phesoi--;
                                }
                            }
                            else if (GlobalFunctionMaCun.plr7 == GlobalFunctionMaCun.dam)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr7)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr7)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr7)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr7).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr7).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr7).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.keo != 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.keo)))
                                    {
                                        if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr1)
                                        {
                                            await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phedan--;
                                        }
                                        else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr4)
                                        {
                                            await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phesoi--;
                                        }
                                        else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr6)
                                        {
                                            await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phesoi--;
                                        }
                                        else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr9)
                                        {
                                            await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phedan--;
                                        }
                                        else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr11)
                                        {
                                            await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phesoi--;
                                        }
                                        else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr13)
                                        {
                                            await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phethu3--;
                                        }
                                        else
                                        {
                                            await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phedan--;
                                        }
                                    }
                                }
                            }
                            else if (GlobalFunctionMaCun.plr8 == GlobalFunctionMaCun.dam)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr8)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr8)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr8)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr8).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr8).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr8).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                            }
                            else if (GlobalFunctionMaCun.plr9 == GlobalFunctionMaCun.dam)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr9)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr9)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr9)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr9).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr9).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr9).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                            }
                            else if (GlobalFunctionMaCun.plr10 == GlobalFunctionMaCun.dam)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr10)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr10)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr10)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr10).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr10).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr10).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                            }
                            else if (GlobalFunctionMaCun.plr11 == GlobalFunctionMaCun.dam)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr11)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr11)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr11)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr11).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr11).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr11).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phesoi--;
                                }
                            }
                            else if (GlobalFunctionMaCun.plr12 == GlobalFunctionMaCun.dam)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr12)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr12)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr12)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr12).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr12).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr12).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                            }
                            else if (GlobalFunctionMaCun.plr1 == GlobalFunctionMaCun.dam)
                            {
                                if (GlobalFunctionMaCun.khien >= 1)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr1)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                            }
                            else return;
                        }
                        else
                        {
                            if (GlobalFunctionMaCun.plr2 == GlobalFunctionMaCun.dam)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr2)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr2)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr2)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr2).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr2).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr2).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                            }
                            else if (GlobalFunctionMaCun.plr3 == GlobalFunctionMaCun.dam)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr3)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr3)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr3)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr3).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr3).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr3).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                            }
                            else if (GlobalFunctionMaCun.plr4 == GlobalFunctionMaCun.dam)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr4)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr4)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr4)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr4).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr4).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr4).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phesoi--;
                                }
                            }
                            else if (GlobalFunctionMaCun.plr5 == GlobalFunctionMaCun.dam)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr5)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr5)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr5)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else if (GlobalFunctionMaCun.gialang >= 1)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574497514586137).SendMessageAsync("Đêm qua bạn đã bị tấn công, nếu đợt tiếp theo bị tấn công bạn sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.gialang--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr5).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr5).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr5).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                            }
                            else if (GlobalFunctionMaCun.plr6 == GlobalFunctionMaCun.dam)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr6)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr6)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr6)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr6).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr6).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr6).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phesoi--;
                                }
                            }
                            else if (GlobalFunctionMaCun.plr7 == GlobalFunctionMaCun.dam)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr7)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr7)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr7)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr7).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr7).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr7).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.keo != 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.keo)))
                                    {
                                        if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr1)
                                        {
                                            await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phedan--;
                                        }
                                        else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr4)
                                        {
                                            await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phesoi--;
                                        }
                                        else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr6)
                                        {
                                            await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phesoi--;
                                        }
                                        else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr9)
                                        {
                                            await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phedan--;
                                        }
                                        else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr11)
                                        {
                                            await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phesoi--;
                                        }
                                        else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr13)
                                        {
                                            await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phethu3--;
                                        }
                                        else
                                        {
                                            await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phedan--;
                                        }
                                    }
                                }
                            }
                            else if (GlobalFunctionMaCun.plr8 == GlobalFunctionMaCun.dam)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr8)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr8)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr8)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr8).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr8).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr8).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                            }
                            else if (GlobalFunctionMaCun.plr9 == GlobalFunctionMaCun.dam)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr9)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr9)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr9)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr9).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr9).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr9).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                            }
                            else if (GlobalFunctionMaCun.plr10 == GlobalFunctionMaCun.dam)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr10)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr10)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr10)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr10).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr10).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr10).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                            }
                            else if (GlobalFunctionMaCun.plr11 == GlobalFunctionMaCun.dam)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr11)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr11)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr11)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr11).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr11).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr11).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phesoi--;
                                }
                            }
                            else if (GlobalFunctionMaCun.plr12 == GlobalFunctionMaCun.dam)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr12)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr12)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr12)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr12).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr12).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr12).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                            }
                            else if (GlobalFunctionMaCun.plr1 == GlobalFunctionMaCun.dam)
                            {
                                if (GlobalFunctionMaCun.khien >= 1)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr1)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                            }
                            else return;
                        }
                    }
                    if (GlobalFunctionMaCun.can != 0)
                    {
                        if (GlobalFunctionMaCun.can == GlobalFunctionMaCun.dam)
                        {
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                        }
                        else
                        {
                            if (GlobalFunctionMaCun.plr2 == GlobalFunctionMaCun.can)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr2)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr2)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr2)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr2).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr2).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr2).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                            }
                            else if (GlobalFunctionMaCun.plr3 == GlobalFunctionMaCun.can)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr3)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr3)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr3)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr3).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr3).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr3).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                            }
                            else if (GlobalFunctionMaCun.plr5 == GlobalFunctionMaCun.can)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr5)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr5)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.gialang >= 1)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574497514586137).SendMessageAsync("Đêm qua bạn đã bị tấn công, nếu đợt tiếp theo bị tấn công bạn sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.gialang--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr5)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr5).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr5).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr5).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                            }
                            else if (GlobalFunctionMaCun.plr7 == GlobalFunctionMaCun.can)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr7)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr7)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr7)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr7).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr7).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr7).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.keo != 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.keo)))
                                    {
                                        if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr1)
                                        {
                                            await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phedan--;
                                        }
                                        else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr4)
                                        {
                                            await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phesoi--;
                                        }
                                        else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr6)
                                        {
                                            await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phesoi--;
                                        }
                                        else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr9)
                                        {
                                            await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phedan--;
                                        }
                                        else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr11)
                                        {
                                            await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phesoi--;
                                        }
                                        else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr13)
                                        {
                                            await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phethu3--;
                                        }
                                        else
                                        {
                                            await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phedan--;
                                        }
                                    }
                                }
                            }
                            else if (GlobalFunctionMaCun.plr8 == GlobalFunctionMaCun.can)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr8)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr8)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr8)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr8).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr8).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr8).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                            }
                            else if (GlobalFunctionMaCun.plr9 == GlobalFunctionMaCun.can)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr9)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr9)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr9)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr9).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr9).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr9).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                            }
                            else if (GlobalFunctionMaCun.plr10 == GlobalFunctionMaCun.can)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr10)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr10)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr10)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr10).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr10).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr10).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                            }
                            else if (GlobalFunctionMaCun.plr12 == GlobalFunctionMaCun.can)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr12)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr12)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr12)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr12).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr12).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr12).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                            }
                            else if (GlobalFunctionMaCun.plr1 == GlobalFunctionMaCun.can)
                            {
                                if (GlobalFunctionMaCun.khien >= 1)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr1)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                            }
                            else if (GlobalFunctionMaCun.plr13 == GlobalFunctionMaCun.can)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr13)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị cắn hoặc bị đâm sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr13)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr13)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                }
                            }
                            else return;
                        }
                    }
                    if (GlobalFunctionMaCun.can == 0)
                    {
                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                    }
                    GlobalFunctionMaCun.can = 0;
                    GlobalFunctionMaCun.dam = 0;
                    GlobalFunctionMaCun.cuu = 0;
                    GlobalFunctionMaCun.baoveplr = 0;
                    GlobalFunctionMaCun.dongbang = 0;
                    GlobalFunctionMaCun.phuphep = 0;
                    GlobalFunctionMaCun.gamestatus = 2;
                }
            }
            else return;
        }
        [Command("ketthuc")]
        public async Task ketthuctrandau()
        {
            var embed = new EmbedBuilder();
            SocketGuildUser User1 = Context.User as SocketGuildUser;
            if (Context.Guild.Id != 580555457983152149)
            {
                return;
            }
            else if (!User1.GuildPermissions.ManageRoles)
            {
                embed.AddField($"Lỗi!", "Người sử dụng cần có Quyền Điều Hành Role.");
                embed.WithColor(new Discord.Color(255, 0, 0));
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
            else
            {
                OverwritePermissions chophep = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Allow, readMessageHistory: PermValue.Allow);
                OverwritePermissions khongchophep = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Deny);
                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580557883931099138).AddPermissionOverwriteAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"), chophep.Modify());
                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).AddPermissionOverwriteAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"), khongchophep.Modify());
                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("" + Context.Guild.Roles.FirstOrDefault(x => x.Name == "Người Chơi").Mention + ", Cảm ơn đã tham gia! (Sau khi có tin nhắn này thì người chơi sẽ tự động bị kick sau 10s)");
                GlobalFunctionMaCun.channel1 = 0;
                GlobalFunctionMaCun.channel2 = 0;
                GlobalFunctionMaCun.channel3 = 0;
                GlobalFunctionMaCun.channel4 = 0;
                GlobalFunctionMaCun.channel5 = 0;
                GlobalFunctionMaCun.channel6 = 0;
                GlobalFunctionMaCun.channel7 = 0;
                GlobalFunctionMaCun.channel8 = 0;
                GlobalFunctionMaCun.channel9 = 0;
                GlobalFunctionMaCun.channel10 = 0;
                GlobalFunctionMaCun.channel11 = 0;
                GlobalFunctionMaCun.channel12 = 0;
                GlobalFunctionMaCun.channel13 = 0;
                GlobalFunctionMaCun.plr1 = 0;
                GlobalFunctionMaCun.plr2 = 0;
                GlobalFunctionMaCun.plr3 = 0;
                GlobalFunctionMaCun.plr4 = 0;
                GlobalFunctionMaCun.plr5 = 0;
                GlobalFunctionMaCun.plr6 = 0;
                GlobalFunctionMaCun.plr7 = 0;
                GlobalFunctionMaCun.plr8 = 0;
                GlobalFunctionMaCun.plr9 = 0;
                GlobalFunctionMaCun.plr10 = 0;
                GlobalFunctionMaCun.plr11 = 0;
                GlobalFunctionMaCun.plr12 = 0;
                GlobalFunctionMaCun.plr13 = 0;
                GlobalFunctionMaCun.treo = 0;
                GlobalFunctionMaCun.thayboi = 0;
                GlobalFunctionMaCun.tientri = 0;
                GlobalFunctionMaCun.gialang = 0;
                GlobalFunctionMaCun.lastdongbang = 0;
                GlobalFunctionMaCun.dongbang = 0;
                GlobalFunctionMaCun.baoveplr = 0;
                GlobalFunctionMaCun.danxathu = 0;
                GlobalFunctionMaCun.thuoccuu = 0;
                GlobalFunctionMaCun.thuocdoc = 0;
                GlobalFunctionMaCun.khien = 0;
                GlobalFunctionMaCun.can = 0;
                GlobalFunctionMaCun.dam = 0;
                GlobalFunctionMaCun.cuu = 0;
                GlobalFunctionMaCun.phuphep = 0;
                GlobalFunctionMaCun.votesong = 0;
                GlobalFunctionMaCun.votechet = 0;
                GlobalFunctionMaCun.cuu = 0;
                GlobalFunctionMaCun.gamestatus = 0;
                GlobalFunctionMaCun.daycount = 0;
                GlobalFunctionMaCun.phethu3 = 0;
                GlobalFunctionMaCun.phesoi = 0;
                GlobalFunctionMaCun.phedan = 0;
                GlobalFunctionMaCun.mogame = null;

                await Task.Delay(10000);
                while (GlobalFunctionMaCun.plr > 0)
                {
                    var g = Context.Guild.Users.FirstOrDefault(x => x.Nickname == GlobalFunctionMaCun.plr + "");
                    if (g != null)
                    {
                        await g.KickAsync();
                    }
                    GlobalFunctionMaCun.plr--;
                }

                IEnumerable<IMessage> nonPinnedMessages = await Context.Guild.GetTextChannel(580563096544739331).GetMessagesAsync(500).FlattenAsync();
                await Context.Guild.GetTextChannel(580563096544739331).DeleteMessagesAsync(nonPinnedMessages.Where(x => x.IsPinned == false));

                IEnumerable<IMessage> nonPinnedMessages2 = await Context.Guild.GetTextChannel(580564164687298609).GetMessagesAsync(200).FlattenAsync();
                await Context.Guild.GetTextChannel(580564164687298609).DeleteMessagesAsync(nonPinnedMessages2.Where(x => x.IsPinned == false));

                IEnumerable<IMessage> nonPinnedMessages3 = await Context.Guild.GetTextChannel(580564753982816256).GetMessagesAsync(200).FlattenAsync();
                await Context.Guild.GetTextChannel(580564753982816256).DeleteMessagesAsync(nonPinnedMessages3.Where(x => x.IsPinned == false));

                IEnumerable<IMessage> nonPinnedMessages4 = await Context.Guild.GetTextChannel(580565718987309101).GetMessagesAsync(200).FlattenAsync();
                await Context.Guild.GetTextChannel(580565718987309101).DeleteMessagesAsync(nonPinnedMessages4.Where(x => x.IsPinned == false));

                IEnumerable<IMessage> nonPinnedMessages5 = await Context.Guild.GetTextChannel(580699915156455424).GetMessagesAsync(200).FlattenAsync();
                await Context.Guild.GetTextChannel(580699915156455424).DeleteMessagesAsync(nonPinnedMessages5.Where(x => x.IsPinned == false));



                IEnumerable<IMessage> baove = await Context.Guild.GetTextChannel(580574363930198021).GetMessagesAsync(200).FlattenAsync();
                await Context.Guild.GetTextChannel(580574363930198021).DeleteMessagesAsync(baove.Where(x => x.IsPinned == false));

                IEnumerable<IMessage> thayboi = await Context.Guild.GetTextChannel(580574739391578112).GetMessagesAsync(200).FlattenAsync();
                await Context.Guild.GetTextChannel(580574739391578112).DeleteMessagesAsync(thayboi.Where(x => x.IsPinned == false));

                IEnumerable<IMessage> dan = await Context.Guild.GetTextChannel(580574427712847872).GetMessagesAsync(200).FlattenAsync();
                await Context.Guild.GetTextChannel(580574427712847872).DeleteMessagesAsync(dan.Where(x => x.IsPinned == false));

                IEnumerable<IMessage> soithuong = await Context.Guild.GetTextChannel(580574451834290176).GetMessagesAsync(200).FlattenAsync();
                await Context.Guild.GetTextChannel(580574451834290176).DeleteMessagesAsync(soithuong.Where(x => x.IsPinned == false));

                IEnumerable<IMessage> gialang = await Context.Guild.GetTextChannel(580574497514586137).GetMessagesAsync(200).FlattenAsync();
                await Context.Guild.GetTextChannel(580574497514586137).DeleteMessagesAsync(gialang.Where(x => x.IsPinned == false));

                IEnumerable<IMessage> soiphuthuy = await Context.Guild.GetTextChannel(580574522361774081).GetMessagesAsync(200).FlattenAsync();
                await Context.Guild.GetTextChannel(580574522361774081).DeleteMessagesAsync(soiphuthuy.Where(x => x.IsPinned == false));

                IEnumerable<IMessage> thosan = await Context.Guild.GetTextChannel(580574545606475782).GetMessagesAsync(200).FlattenAsync();
                await Context.Guild.GetTextChannel(580574545606475782).DeleteMessagesAsync(thosan.Where(x => x.IsPinned == false));

                IEnumerable<IMessage> thangngo = await Context.Guild.GetTextChannel(580574572483706891).GetMessagesAsync(200).FlattenAsync();
                await Context.Guild.GetTextChannel(580574572483706891).DeleteMessagesAsync(thangngo.Where(x => x.IsPinned == false));

                IEnumerable<IMessage> phuthuy = await Context.Guild.GetTextChannel(580574598677135390).GetMessagesAsync(200).FlattenAsync();
                await Context.Guild.GetTextChannel(580574598677135390).DeleteMessagesAsync(phuthuy.Where(x => x.IsPinned == false));

                IEnumerable<IMessage> xathu = await Context.Guild.GetTextChannel(580574616645402656).GetMessagesAsync(200).FlattenAsync();
                await Context.Guild.GetTextChannel(580574616645402656).DeleteMessagesAsync(xathu.Where(x => x.IsPinned == false));

                IEnumerable<IMessage> soibang = await Context.Guild.GetTextChannel(580574634811064342).GetMessagesAsync(200).FlattenAsync();
                await Context.Guild.GetTextChannel(580574634811064342).DeleteMessagesAsync(soibang.Where(x => x.IsPinned == false));

                IEnumerable<IMessage> tientri = await Context.Guild.GetTextChannel(580574414660435982).GetMessagesAsync(200).FlattenAsync();
                await Context.Guild.GetTextChannel(580574414660435982).DeleteMessagesAsync(tientri.Where(x => x.IsPinned == false));

                IEnumerable<IMessage> satnhan = await Context.Guild.GetTextChannel(580574812662136836).GetMessagesAsync(200).FlattenAsync();
                await Context.Guild.GetTextChannel(580574812662136836).DeleteMessagesAsync(satnhan.Where(x => x.IsPinned == false));
            }
        }

        [Command("thang")]
        public async Task phethang([Remainder] string phe = null)
        {
            var embed = new EmbedBuilder();
            SocketGuildUser User1 = Context.User as SocketGuildUser;
            if (Context.Guild.Id != 580555457983152149)
            {
                return;
            }
            else if (!User1.GuildPermissions.ManageRoles)
            {
                embed.AddField($"Lỗi!", "Người sử dụng cần có Quyền Điều Hành Role.");
                embed.WithColor(new Discord.Color(255, 0, 0));
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
            else if (phe == null)
            {
                embed.AddField($"Lỗi!", "Bạn thiếu phe cho việc thắng trận.");
                embed.WithColor(new Discord.Color(255, 0, 0));
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
            else
            {
                OverwritePermissions chophep = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Allow);
                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).AddPermissionOverwriteAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"), chophep.Modify());
                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Game kết thúc, " + phe + " thắng.");
            }
        }
    }
}
