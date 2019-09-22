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
using Neko_Test.Core.UserAccounts10;
using Neko_Test.ModulesMaCun;
using Neko_Test.ModulesMaCunGame;

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
                else if (GlobalFunctionMaCun.blockcommand == true)
                {
                    embed.AddField($"Lỗi!", "Lệnh hiện tại đang bị khóa.");
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
                var User = UserAccounts10.GetAccount(Context.User as SocketUser);
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
                else if (User.None1 != null)
                {
                    embed.AddField($"Lỗi!", "Bạn đã bị cấm tham gia đăng ký vì lý do: " + User.None1 + "");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (GlobalFunctionMaCun.gamestatus == 0 & !Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User) & !Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết").Members.Contains(Context.User))
                {
                    int checkplayer = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Count();
                    checkplayer++;
                    if (checkplayer <= 16)
                    {
                        if (!Context.Guild.Roles.FirstOrDefault(x => x.Name == "Người Chơi").Members.Contains(Context.User))
                        {
                            await (Context.User as IGuildUser).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Người Chơi"));
                        }
                        if (!Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User))
                        {
                            await (Context.User as IGuildUser).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                        }
                        if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Khán Giả").Members.Contains(Context.User))
                        {
                            await (Context.User as IGuildUser).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Khán Giả"));
                        }
                        if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Quản Trò - Game").Members.Contains(Context.User))
                        {
                            await (Context.User as IGuildUser).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Quản Trò - Game"));
                        }

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
                if (GlobalFunctionMaCun.mogame == null)
                {
                    embed.AddField($"Lỗi!", "Không có game nào được mở nên không bạn thể theo dõi.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (!Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User) & !Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết").Members.Contains(Context.User))
                {
                    if (!Context.Guild.Roles.FirstOrDefault(x => x.Name == "Người Chơi").Members.Contains(Context.User))
                    {
                        await (Context.User as IGuildUser).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Người Chơi"));
                    }
                    if (!Context.Guild.Roles.FirstOrDefault(x => x.Name == "Khán Giả").Members.Contains(Context.User))
                    {
                        await (Context.User as IGuildUser).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Khán Giả"));
                    }
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
                var embed2 = new EmbedBuilder();
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
                    OverwritePermissions khongchophep2 = new OverwritePermissions(viewChannel: PermValue.Allow, connect: PermValue.Allow, speak: PermValue.Deny, useVoiceActivation: PermValue.Deny);
                    await Context.Client.GetGuild(580555457983152149).GetVoiceChannel(580566264171331597).AddPermissionOverwriteAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"), khongchophep2.Modify());
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
                    GlobalFunctionMaCun.soitri = 1;
                    GlobalFunctionMaCun.luothoisinh = 1;
                    GlobalFunctionMaCun.lastmoi = 1;
                    GlobalFunctionMaCun.lastdongbang = 1;
                    GlobalFunctionMaCun.pha = 1;
                    GlobalFunctionMaCun.luotcanchet = 1;
                    GlobalFunctionMaCun.luotnuoi = 1;


                    if (GlobalFunctionMaCun.showroles == 1)
                    {
                        await GlobalFunctionGame.showgameroles();
                        embed2.AddField("Danh Sách Vai Trò Trong Trận!", $"{GlobalFunctionMaCun.nameroles}");
                        embed2.WithColor(new Discord.Color(0, 255, 255));
                        await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("", false, embed2.Build());
                    }
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
                else if (GlobalFunctionMaCun.blockcommand == true)
                {
                    embed.AddField($"Lỗi!", "Lệnh hiện tại đang bị khóa.");
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
                    else if (num <= 0 || num >= 21)
                    {
                        embed.AddField($"Lỗi!", "Vai Trò nằm trong khoảng 1 đến 21 (Sử dụng -danhsachvaitro để hiện ra số của từng vai trò).");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (GlobalFunctionMaCun.plr1 == user.Id || GlobalFunctionMaCun.plr2 == user.Id || GlobalFunctionMaCun.plr3 == user.Id || GlobalFunctionMaCun.plr4 == user.Id || GlobalFunctionMaCun.plr5 == user.Id || GlobalFunctionMaCun.plr6 == user.Id || GlobalFunctionMaCun.plr7 == user.Id || GlobalFunctionMaCun.plr8 == user.Id || GlobalFunctionMaCun.plr9 == user.Id || GlobalFunctionMaCun.plr10 == user.Id || GlobalFunctionMaCun.plr11 == user.Id || GlobalFunctionMaCun.plr12 == user.Id || GlobalFunctionMaCun.plr13 == user.Id || GlobalFunctionMaCun.plr14 == user.Id || GlobalFunctionMaCun.plr15 == user.Id || GlobalFunctionMaCun.plr16 == user.Id || GlobalFunctionMaCun.plr17 == user.Id || GlobalFunctionMaCun.plr18 == user.Id || GlobalFunctionMaCun.plr19 == user.Id || GlobalFunctionMaCun.plr20 == user.Id || GlobalFunctionMaCun.plr21 == user.Id)
                    {
                        embed.AddField($"Lỗi", "Người Chơi đã có Vai Trò.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else
                    {
                        GlobalFunctionMaCun.game = "ok";
                        int numberofplayer = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Count();
                        while (numberofplayer > 0)
                        {
                            var g = Context.Guild.Users.FirstOrDefault(x => x.Nickname == numberofplayer + "");
                            var i = Context.Guild.Users.FirstOrDefault(x => x.Nickname == numberofplayer + "").Id;
                            if (g != null)
                            {
                                var User = UserAccounts10.GetAccount((Context.Guild.Users.FirstOrDefault(x => x.Nickname == numberofplayer + "")) as SocketUser);
                                if (User.None1 != null)
                                {
                                    GlobalFunctionMaCun.game = "notok";
                                    embed.AddField($"Lỗi!", $"Người chơi {numberofplayer} đã bị cấm chơi nên không thể thêm vai trò cho người chơi khác.");
                                }
                                var user1 = Context.Client.GetGuild(530689610313891840).GetUser(i);
                                if (user1 == null)
                                {
                                    GlobalFunctionMaCun.game = "notok";
                                    embed.AddField($"Lỗi!", $"Người chơi {numberofplayer} không có trong Main - Server nên không thể thêm vai trò cho người chơi khác.");
                                }
                            }
                            numberofplayer--;
                            if (numberofplayer <= 0 & GlobalFunctionMaCun.game == "notok")
                            {
                                embed.WithColor(new Discord.Color(255, 0, 0));
                                //await Context.Guild.GetTextChannel(580558295023222784).SendMessageAsync("", false, embed.Build());
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                        }
                        if (GlobalFunctionMaCun.game == "notok")
                        {
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel1 == 0 & num == 1 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))//580574363930198021
                        {
                            await GlobalFunctionMaCun.givenroles(num.ToString(), user.Id);
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "ten");
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "channelid");
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - " + GlobalFunctionMaCun.nameroles + ".");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(GlobalFunctionMaCun.channelroles).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Bảo Vệ cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel2 == 0 & num == 2 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))//580574739391578112
                        {
                            await GlobalFunctionMaCun.givenroles(num.ToString(), user.Id);
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "ten");
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "channelid");
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - " + GlobalFunctionMaCun.nameroles + ".");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(GlobalFunctionMaCun.channelroles).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Thầy Bói cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel3 == 0 & num == 3 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))//580574427712847872
                        {
                            await GlobalFunctionMaCun.givenroles(num.ToString(), user.Id);
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "ten");
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "channelid");
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - " + GlobalFunctionMaCun.nameroles + ".");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(GlobalFunctionMaCun.channelroles).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Dân Làng cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel4 == 0 & num == 4 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))//580574451834290176 - 580564753982816256
                        {
                            await GlobalFunctionMaCun.givenroles(num.ToString(), user.Id);
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "ten");
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "channelid");
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - " + GlobalFunctionMaCun.nameroles + ".");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(GlobalFunctionMaCun.channelroles).AddPermissionOverwriteAsync(user, chophep.Modify());
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Sói Thường cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel5 == 0 & num == 5 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))// 580574497514586137
                        {
                            await GlobalFunctionMaCun.givenroles(num.ToString(), user.Id);
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "ten");
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "channelid");
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - " + GlobalFunctionMaCun.nameroles + ".");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(GlobalFunctionMaCun.channelroles).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Già Làng cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel6 == 0 & num == 6 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))//580574522361774081 - 580564753982816256
                        {
                            await GlobalFunctionMaCun.givenroles(num.ToString(), user.Id);
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "ten");
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "channelid");
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - " + GlobalFunctionMaCun.nameroles + ".");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(GlobalFunctionMaCun.channelroles).AddPermissionOverwriteAsync(user, chophep.Modify());
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Sói Phù Thủy cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel7 == 0 & num == 7 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))//580574545606475782
                        {
                            await GlobalFunctionMaCun.givenroles(num.ToString(), user.Id);
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "ten");
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "channelid");
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - " + GlobalFunctionMaCun.nameroles + ".");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(GlobalFunctionMaCun.channelroles).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Thợ Săn cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel8 == 0 & num == 8 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))//580574572483706891
                        {
                            await GlobalFunctionMaCun.givenroles(num.ToString(), user.Id);
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "ten");
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "channelid");
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - " + GlobalFunctionMaCun.nameroles + ".");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(GlobalFunctionMaCun.channelroles).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Thằng Ngố cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel9 == 0 & num == 9 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))//580574598677135390
                        {
                            await GlobalFunctionMaCun.givenroles(num.ToString(), user.Id);
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "ten");
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "channelid");
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - " + GlobalFunctionMaCun.nameroles + ".");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(GlobalFunctionMaCun.channelroles).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Phùy Thủy cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel10 == 0 & num == 10 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))//580574616645402656
                        {
                            await GlobalFunctionMaCun.givenroles(num.ToString(), user.Id);
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "ten");
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "channelid");
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - " + GlobalFunctionMaCun.nameroles + ".");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(GlobalFunctionMaCun.channelroles).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Xạ Thủ cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel11 == 0 & num == 11 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))//580574634811064342
                        {
                            await GlobalFunctionMaCun.givenroles(num.ToString(), user.Id);
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "ten");
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "channelid");
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - " + GlobalFunctionMaCun.nameroles + ".");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(GlobalFunctionMaCun.channelroles).AddPermissionOverwriteAsync(user, chophep.Modify());
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Sói Băng cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel12 == 0 & num == 12 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))//580574414660435982
                        {
                            await GlobalFunctionMaCun.givenroles(num.ToString(), user.Id);
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "ten");
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "channelid");
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - " + GlobalFunctionMaCun.nameroles + ".");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(GlobalFunctionMaCun.channelroles).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Tiên Tri cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel13 == 0 & num == 13 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))//580574812662136836
                        {
                            await GlobalFunctionMaCun.givenroles(num.ToString(), user.Id);
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "ten");
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "channelid");
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - " + GlobalFunctionMaCun.nameroles + ".");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(GlobalFunctionMaCun.channelroles).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Sát Nhân cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel14 == 0 & num == 14 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))//583828253681254400
                        {
                            await GlobalFunctionMaCun.givenroles(num.ToString(), user.Id);
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "ten");
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "channelid");
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - " + GlobalFunctionMaCun.nameroles + ".");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(GlobalFunctionMaCun.channelroles).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Gái Điếm cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel15 == 0 & num == 15 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))//583828359394492427
                        {
                            await GlobalFunctionMaCun.givenroles(num.ToString(), user.Id);
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "ten");
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "channelid");
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - " + GlobalFunctionMaCun.nameroles + ".");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(GlobalFunctionMaCun.channelroles).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Thầy Đồng cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel16 == 0 & num == 16 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))//583828385659355147
                        {
                            await GlobalFunctionMaCun.givenroles(num.ToString(), user.Id);
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "ten");
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "channelid");
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - " + GlobalFunctionMaCun.nameroles + ".");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(GlobalFunctionMaCun.channelroles).AddPermissionOverwriteAsync(user, chophep.Modify());
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Sói Tri cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel17 == 0 & num == 17 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))//589462982275235860
                        {
                            await GlobalFunctionMaCun.givenroles(num.ToString(), user.Id);
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "ten");
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "channelid");
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - " + GlobalFunctionMaCun.nameroles + ".");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(GlobalFunctionMaCun.channelroles).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Cậu Bé Hoang Dã cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel18 == 0 & num == 18 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))//589463015212974080
                        {
                            OverwritePermissions abs = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Deny, readMessageHistory: PermValue.Allow);
                            await GlobalFunctionMaCun.givenroles(num.ToString(), user.Id);
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "ten");
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "channelid");
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - " + GlobalFunctionMaCun.nameroles + ".");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(GlobalFunctionMaCun.channelroles).AddPermissionOverwriteAsync(user, chophep.Modify());
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(590016837262245895).AddPermissionOverwriteAsync(user, abs.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Giáo Xứ cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel19 == 0 & num == 19 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))
                        {
                            await GlobalFunctionMaCun.givenroles(num.ToString(), user.Id);
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "ten");
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "channelid");
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - " + GlobalFunctionMaCun.nameroles + ".");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(GlobalFunctionMaCun.channelroles).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò " + GlobalFunctionMaCun.nameroles + " cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel20 == 0 & num == 20 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))
                        {
                            await GlobalFunctionMaCun.givenroles(num.ToString(), user.Id);
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "ten");
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "channelid");
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - " + GlobalFunctionMaCun.nameroles + ".");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(GlobalFunctionMaCun.channelroles).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò " + GlobalFunctionMaCun.nameroles + " cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel21 == 0 & num == 21 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))
                        {
                            await GlobalFunctionMaCun.givenroles(num.ToString(), user.Id);
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "ten");
                            await GlobalFunctionMaCun.rolestring(num.ToString(), "channelid");
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - " + GlobalFunctionMaCun.nameroles + ".");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(GlobalFunctionMaCun.channelroles).AddPermissionOverwriteAsync(user, chophep.Modify());
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò " + GlobalFunctionMaCun.nameroles + " cho " + user + "");
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
        [Command("gamerole2"), Alias("vaitrogame2")]
        public async Task themvaitrochonguoichoi2([Remainder]string roless = null)
        {
            if (Context.Guild.Id == 580555457983152149)
            {
                OverwritePermissions chophep = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Allow, readMessageHistory: PermValue.Allow);
                var embed = new EmbedBuilder();
                var embed2 = new EmbedBuilder();
                SocketGuildUser User1 = Context.User as SocketGuildUser;
                if (!User1.GuildPermissions.ManageRoles)
                {
                    embed.AddField($"Lỗi!", "Người sử dụng cần có Quyền Điều Hành Role.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (GlobalFunctionMaCun.blockcommand == true)
                {
                    embed.AddField($"Lỗi!", "Lệnh hiện tại đang bị khóa.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    GlobalFunctionMaCun.game = "ok";
                    int numberofplayer = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Count();
                    while (numberofplayer > 0)
                    {
                        var g = Context.Guild.Users.FirstOrDefault(x => x.Nickname == numberofplayer + "");
                        var i = Context.Guild.Users.FirstOrDefault(x => x.Nickname == numberofplayer + "").Id;
                        if (g != null)
                        {
                            var User = UserAccounts10.GetAccount((Context.Guild.Users.FirstOrDefault(x => x.Nickname == numberofplayer + "")) as SocketUser);
                            if (User.None1 != null)
                            {
                                GlobalFunctionMaCun.game = "notok";
                                embed.AddField($"Lỗi!", $"Người chơi {numberofplayer} đã bị cấm chơi nên không thể thêm vai trò cho người chơi khác.");
                            }
                            var user1 = Context.Client.GetGuild(530689610313891840).GetUser(i);
                            if (user1 == null)
                            {
                                GlobalFunctionMaCun.game = "notok";
                                embed.AddField($"Lỗi!", $"Người chơi {numberofplayer} không có trong Main - Server nên không thể thêm vai trò cho người chơi khác.");
                            }
                        }
                        numberofplayer--;
                        if (numberofplayer <= 0 & GlobalFunctionMaCun.game == "notok")
                        {
                            embed.WithColor(new Discord.Color(255, 0, 0));
                            //await Context.Guild.GetTextChannel(580558295023222784).SendMessageAsync("", false, embed.Build());
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                    }

                    if (GlobalFunctionMaCun.game == "ok")
                    {
                        var rolecheck = roless;
                        var rolesplit = rolecheck.Split(" ");
                        int checknumber = 0;
                        int checkplayer = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Count();
                        string ready = "yes";

                        foreach (var h in rolesplit)
                        {
                            checknumber++;
                            await GlobalFunctionMaCun.rolestring(h, "ten");
                            if (GlobalFunctionMaCun.nameroles == null)
                            {
                                ready = "no";
                            }
                        }

                        if (checknumber != checkplayer)
                        {
                            embed.AddField($"Lỗi!", $"Số Vai Trò và Số Người Chơi có role Sống không cân bằng (Thiếu hoặc Dư vai trò).");
                            embed.WithColor(new Discord.Color(255, 0, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (ready == "no")
                        {
                            embed.AddField($"Lỗi!", $"Trong random có số Vai Trò hoặc tên Vai Trò không hợp lệ.");
                            embed.WithColor(new Discord.Color(255, 0, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else
                        {
                            var number = checkplayer;
                            Random a = new Random();
                            Random j = new Random();
                            List<int> randomList = new List<int>();
                            int n = 1;
                            int i = 1;
                            int v = 1;
                            number++;
                            if (GlobalFunctionMaCun.plr1p != 0)
                            {
                                randomList.Add(GlobalFunctionMaCun.plr1p);
                            }
                            if (GlobalFunctionMaCun.plr2p != 0)
                            {
                                randomList.Add(GlobalFunctionMaCun.plr2p);
                            }
                            if (GlobalFunctionMaCun.plr3p != 0)
                            {
                                randomList.Add(GlobalFunctionMaCun.plr3p);
                            }
                            if (GlobalFunctionMaCun.plr4p != 0)
                            {
                                randomList.Add(GlobalFunctionMaCun.plr4p);
                            }
                            if (GlobalFunctionMaCun.plr5p != 0)
                            {
                                randomList.Add(GlobalFunctionMaCun.plr5p);
                            }
                            if (GlobalFunctionMaCun.plr6p != 0)
                            {
                                randomList.Add(GlobalFunctionMaCun.plr6p);
                            }
                            if (GlobalFunctionMaCun.plr7p != 0)
                            {
                                randomList.Add(GlobalFunctionMaCun.plr7p);
                            }
                            if (GlobalFunctionMaCun.plr8p != 0)
                            {
                                randomList.Add(GlobalFunctionMaCun.plr8p);
                            }
                            if (GlobalFunctionMaCun.plr9p != 0)
                            {
                                randomList.Add(GlobalFunctionMaCun.plr9p);
                            }
                            if (GlobalFunctionMaCun.plr10p != 0)
                            {
                                randomList.Add(GlobalFunctionMaCun.plr10p);
                            }
                            if (GlobalFunctionMaCun.plr11p != 0)
                            {
                                randomList.Add(GlobalFunctionMaCun.plr11p);
                            }
                            if (GlobalFunctionMaCun.plr12p != 0)
                            {
                                randomList.Add(GlobalFunctionMaCun.plr12p);
                            }
                            if (GlobalFunctionMaCun.plr13p != 0)
                            {
                                randomList.Add(GlobalFunctionMaCun.plr13p);
                            }
                            if (GlobalFunctionMaCun.plr14p != 0)
                            {
                                randomList.Add(GlobalFunctionMaCun.plr14p);
                            }
                            if (GlobalFunctionMaCun.plr15p != 0)
                            {
                                randomList.Add(GlobalFunctionMaCun.plr15p);
                            }
                            if (GlobalFunctionMaCun.plr16p != 0)
                            {
                                randomList.Add(GlobalFunctionMaCun.plr16p);
                            }
                            if (GlobalFunctionMaCun.plr17p != 0)
                            {
                                randomList.Add(GlobalFunctionMaCun.plr17p);
                            }
                            if (GlobalFunctionMaCun.plr18p != 0)
                            {
                                randomList.Add(GlobalFunctionMaCun.plr18p);
                            }
                            if (GlobalFunctionMaCun.plr19p != 0)
                            {
                                randomList.Add(GlobalFunctionMaCun.plr19p);
                            }
                            if (GlobalFunctionMaCun.plr20p != 0)
                            {
                                randomList.Add(GlobalFunctionMaCun.plr20p);
                            }
                            if (GlobalFunctionMaCun.plr21p != 0)
                            {
                                randomList.Add(GlobalFunctionMaCun.plr21p);
                            }

                            if (number <= 21 & number > 4)
                            {
                                var rrll = roless.Split(" ");
                                foreach (var rl in rrll)
                                {
                                    i++;
                                    if (i > number)
                                    {
                                        i = v;
                                    }
                                    while (i > v)
                                    {
                                        n = a.Next(1, number);
                                        if (!randomList.Contains(n))
                                        {
                                            randomList.Add(n);
                                            v++;
                                            {
                                                await GlobalFunctionMaCun.rolestring(rl, "getroles");
                                                await GlobalFunctionMaCun.rolestring(rl, "ten");
                                                await GlobalFunctionMaCun.rolestring(rl, "channelid");

                                                if (GlobalFunctionMaCun.roleavailble == 0)
                                                {
                                                    var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == n + "");
                                                    embed.AddField($"{n} - {GlobalFunctionMaCun.nameroles}.", "" + user.Username + "");
                                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(GlobalFunctionMaCun.channelroles).AddPermissionOverwriteAsync(user, chophep.Modify());
                                                    await GlobalFunctionMaCun.givenroles(rl, user.Id);
                                                    await GlobalFunctionMaCun.rolesid(user.Id, "aura");
                                                    if (GlobalFunctionMaCun.nameroles == "Ác" || user.Id == GlobalFunctionMaCun.plr11)
                                                    {
                                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(user, chophep.Modify());
                                                        GlobalFunctionMaCun.phesoi++;
                                                    }
                                                }
                                                else
                                                {
                                                    var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == GlobalFunctionMaCun.roleavailble + "");
                                                    embed.AddField($"{GlobalFunctionMaCun.roleavailble} - {GlobalFunctionMaCun.nameroles}.", "" + user.Username + "");
                                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(GlobalFunctionMaCun.channelroles).AddPermissionOverwriteAsync(user, chophep.Modify());
                                                    await GlobalFunctionMaCun.givenroles(rl, user.Id);
                                                    await GlobalFunctionMaCun.rolesid(user.Id, "aura");
                                                    if (GlobalFunctionMaCun.nameroles == "Ác" || user.Id == GlobalFunctionMaCun.plr11)
                                                    {
                                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(user, chophep.Modify());
                                                        GlobalFunctionMaCun.phesoi++;
                                                    }
                                                }
                                            }

                                        }
                                    }
                                }
                                embed.WithColor(new Discord.Color(0, 255, 255));
                                await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("", false, embed.Build());



                                OverwritePermissions chophep2 = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Allow, readMessageHistory: PermValue.Allow);
                                OverwritePermissions khongchophep2 = new OverwritePermissions(viewChannel: PermValue.Deny, sendMessages: PermValue.Deny, readMessageHistory: PermValue.Deny);
                                OverwritePermissions khongchophep3 = new OverwritePermissions(viewChannel: PermValue.Allow, connect: PermValue.Allow, speak: PermValue.Deny, useVoiceActivation: PermValue.Deny);
                                await Context.Client.GetGuild(580555457983152149).GetVoiceChannel(580566264171331597).AddPermissionOverwriteAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"), khongchophep3.Modify());
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580557883931099138).AddPermissionOverwriteAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"), khongchophep2.Modify());
                                GlobalFunctionMaCun.gamestatus = 1;
                                GlobalFunctionMaCun.plr = checkplayer;
                                GlobalFunctionMaCun.thuoccuu = 1;
                                GlobalFunctionMaCun.thuocdoc = 1;
                                GlobalFunctionMaCun.khien = 1;
                                GlobalFunctionMaCun.danxathu = 1;
                                GlobalFunctionMaCun.daycount = 1;
                                GlobalFunctionMaCun.gialang = 1;
                                GlobalFunctionMaCun.thayboi = 1;
                                GlobalFunctionMaCun.tientri = 1;
                                GlobalFunctionMaCun.soitri = 1;
                                GlobalFunctionMaCun.thamtu = 1;
                                GlobalFunctionMaCun.luothoisinh = 1;
                                GlobalFunctionMaCun.lastmoi = 1;
                                GlobalFunctionMaCun.lastdongbang = 1;
                                GlobalFunctionMaCun.pha = 1;
                                GlobalFunctionMaCun.luotcanchet = 1;
                                GlobalFunctionMaCun.luotnuoi = 1;

                                if (GlobalFunctionMaCun.showroles == 1)
                                {
                                    await GlobalFunctionGame.showgameroles();
                                    embed2.AddField("Danh Sách Vai Trò Trong Trận!", $"{GlobalFunctionMaCun.nameroles}");
                                    embed2.WithColor(new Discord.Color(0, 255, 255));
                                    await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("", false, embed2.Build());
                                }

                                if (GlobalFunctionMaCun.plr18 != 0)
                                {
                                    var abss = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr18);
                                    OverwritePermissions abs = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Deny, readMessageHistory: PermValue.Allow);
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(590016837262245895).AddPermissionOverwriteAsync(abss, abs.Modify());
                                }

                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm thứ Nhất Bắt Đầu!");
                            }
                        }

                    }
                }
            }
            else return;
        }
        [Command("gamerole"), Alias("vaitrogame")]
        public async Task themvaitrochonguoichoi()
        {
            if (Context.Guild.Id == 580555457983152149)
            {
                OverwritePermissions chophep = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Allow, readMessageHistory: PermValue.Allow);
                var embed = new EmbedBuilder();
                var embed2 = new EmbedBuilder();
                SocketGuildUser User1 = Context.User as SocketGuildUser;
                if (!User1.GuildPermissions.ManageRoles)
                {
                    embed.AddField($"Lỗi!", "Người sử dụng cần có Quyền Điều Hành Role.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (GlobalFunctionMaCun.blockcommand == true)
                {
                    embed.AddField($"Lỗi!", "Lệnh hiện tại đang bị khóa.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    GlobalFunctionMaCun.game = "ok";
                    int numberofplayer = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Count();
                    while (numberofplayer > 0)
                    {
                        var g = Context.Guild.Users.FirstOrDefault(x => x.Nickname == numberofplayer + "");
                        var i = Context.Guild.Users.FirstOrDefault(x => x.Nickname == numberofplayer + "").Id;
                        if (g != null)
                        {
                            var User = UserAccounts10.GetAccount((Context.Guild.Users.FirstOrDefault(x => x.Nickname == numberofplayer + "")) as SocketUser);
                            if (User.None1 != null)
                            {
                                GlobalFunctionMaCun.game = "notok";
                                embed.AddField($"Lỗi!", $"Người chơi {numberofplayer} đã bị cấm chơi nên không thể thêm vai trò cho người chơi khác.");
                            }
                            var user1 = Context.Client.GetGuild(530689610313891840).GetUser(i);
                            if (user1 == null)
                            {
                                GlobalFunctionMaCun.game = "notok";
                                embed.AddField($"Lỗi!", $"Người chơi {numberofplayer} không có trong Main - Server nên không thể thêm vai trò cho người chơi khác.");
                            }
                        }
                        numberofplayer--;
                        if (numberofplayer <= 0 & GlobalFunctionMaCun.game == "notok")
                        {
                            embed.WithColor(new Discord.Color(255, 0, 0));
                            //await Context.Guild.GetTextChannel(580558295023222784).SendMessageAsync("", false, embed.Build());
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                    }

                    if (GlobalFunctionMaCun.game == "ok")
                    {

                        int checkplayer = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Count();
                        var number = checkplayer;
                        Random a = new Random();
                        Random j = new Random();
                        List<int> randomList = new List<int>();
                        int n = 1;
                        int i = 1;
                        number++;
                        if (GlobalFunctionMaCun.plr1p != 0)
                        {
                            randomList.Add(GlobalFunctionMaCun.plr1p);
                        }
                        if (GlobalFunctionMaCun.plr2p != 0)
                        {
                            randomList.Add(GlobalFunctionMaCun.plr2p);
                        }
                        if (GlobalFunctionMaCun.plr3p != 0)
                        {
                            randomList.Add(GlobalFunctionMaCun.plr3p);
                        }
                        if (GlobalFunctionMaCun.plr4p != 0)
                        {
                            randomList.Add(GlobalFunctionMaCun.plr4p);
                        }
                        if (GlobalFunctionMaCun.plr5p != 0)
                        {
                            randomList.Add(GlobalFunctionMaCun.plr5p);
                        }
                        if (GlobalFunctionMaCun.plr6p != 0)
                        {
                            randomList.Add(GlobalFunctionMaCun.plr6p);
                        }
                        if (GlobalFunctionMaCun.plr7p != 0)
                        {
                            randomList.Add(GlobalFunctionMaCun.plr7p);
                        }
                        if (GlobalFunctionMaCun.plr8p != 0)
                        {
                            randomList.Add(GlobalFunctionMaCun.plr8p);
                        }
                        if (GlobalFunctionMaCun.plr9p != 0)
                        {
                            randomList.Add(GlobalFunctionMaCun.plr9p);
                        }
                        if (GlobalFunctionMaCun.plr10p != 0)
                        {
                            randomList.Add(GlobalFunctionMaCun.plr10p);
                        }
                        if (GlobalFunctionMaCun.plr11p != 0)
                        {
                            randomList.Add(GlobalFunctionMaCun.plr11p);
                        }
                        if (GlobalFunctionMaCun.plr12p != 0)
                        {
                            randomList.Add(GlobalFunctionMaCun.plr12p);
                        }
                        if (GlobalFunctionMaCun.plr13p != 0)
                        {
                            randomList.Add(GlobalFunctionMaCun.plr13p);
                        }
                        if (GlobalFunctionMaCun.plr14p != 0)
                        {
                            randomList.Add(GlobalFunctionMaCun.plr14p);
                        }
                        if (GlobalFunctionMaCun.plr15p != 0)
                        {
                            randomList.Add(GlobalFunctionMaCun.plr15p);
                        }
                        if (GlobalFunctionMaCun.plr16p != 0)
                        {
                            randomList.Add(GlobalFunctionMaCun.plr16p);
                        }
                        if (GlobalFunctionMaCun.plr17p != 0)
                        {
                            randomList.Add(GlobalFunctionMaCun.plr17p);
                        }
                        if (GlobalFunctionMaCun.plr18p != 0)
                        {
                            randomList.Add(GlobalFunctionMaCun.plr18p);
                        }
                        if (GlobalFunctionMaCun.plr19p != 0)
                        {
                            randomList.Add(GlobalFunctionMaCun.plr19p);
                        }
                        if (GlobalFunctionMaCun.plr20p != 0)
                        {
                            randomList.Add(GlobalFunctionMaCun.plr20p);
                        }
                        if (GlobalFunctionMaCun.plr21p != 0)
                        {
                            randomList.Add(GlobalFunctionMaCun.plr21p);
                        }

                        if (number <= 21 & number > 4)
                        {
                            while (i < number)
                            {
                                n = a.Next(1, number);
                                if (!randomList.Contains(n))
                                {
                                    randomList.Add(n);
                                    i++;
                                    if (i == 2)
                                    {
                                        if (GlobalFunctionMaCun.plr1p == 0)
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == n + "");
                                            embed.AddField($"" + n + " - Bảo Vệ.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr1 = user.Id;
                                            GlobalFunctionMaCun.channel1 = 1;
                                            GlobalFunctionMaCun.phedan++;
                                        }
                                        else
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == GlobalFunctionMaCun.plr1p + "");
                                            embed.AddField($"" + GlobalFunctionMaCun.plr1p + " - Bảo Vệ.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr1 = user.Id;
                                            GlobalFunctionMaCun.channel1 = 1;
                                            GlobalFunctionMaCun.phedan++;
                                        }
                                    }
                                    if (i == 3)
                                    {
                                        if (GlobalFunctionMaCun.plr2p == 0)
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == n + "");
                                            embed.AddField($"" + n + " - Thầy Bói.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574739391578112).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr2 = user.Id;
                                            GlobalFunctionMaCun.channel2 = 1;
                                            GlobalFunctionMaCun.phedan++;
                                        }
                                        else
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == GlobalFunctionMaCun.plr2p + "");
                                            embed.AddField($"" + GlobalFunctionMaCun.plr2p + " - Thầy Bói.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574739391578112).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr2 = user.Id;
                                            GlobalFunctionMaCun.channel2 = 1;
                                            GlobalFunctionMaCun.phedan++;
                                        }
                                    }
                                    if (i == 4)
                                    {
                                        if (GlobalFunctionMaCun.plr3p == 0)
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == n + "");
                                            embed.AddField($"" + n + " - Dân Làng.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574427712847872).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr3 = user.Id;
                                            GlobalFunctionMaCun.channel3 = 1;
                                            GlobalFunctionMaCun.phedan++;
                                        }
                                        else
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == GlobalFunctionMaCun.plr3p + "");
                                            embed.AddField($"" + GlobalFunctionMaCun.plr3p + " - Dân Làng.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574427712847872).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr3 = user.Id;
                                            GlobalFunctionMaCun.channel3 = 1;
                                            GlobalFunctionMaCun.phedan++;
                                        }
                                    }
                                    if (i == 5)
                                    {
                                        if (GlobalFunctionMaCun.plr4p == 0)
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == n + "");
                                            embed.AddField($"" + n + " - Sói Thường.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574451834290176).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr4 = user.Id;
                                            GlobalFunctionMaCun.channel4 = 1;
                                            GlobalFunctionMaCun.phesoi++;
                                        }
                                        else
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == GlobalFunctionMaCun.plr4p + "");
                                            embed.AddField($"" + GlobalFunctionMaCun.plr4p + " - Sói Thường.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574451834290176).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr4 = user.Id;
                                            GlobalFunctionMaCun.channel4 = 1;
                                            GlobalFunctionMaCun.phesoi++;
                                        }
                                    }
                                    if (i == 6)
                                    {
                                        if (GlobalFunctionMaCun.plr5p == 0)
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == n + "");
                                            embed.AddField($"" + n + " - Già Làng.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574497514586137).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr5 = user.Id;
                                            GlobalFunctionMaCun.channel5 = 1;
                                            GlobalFunctionMaCun.phedan++;
                                        }
                                        else
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == GlobalFunctionMaCun.plr5p + "");
                                            embed.AddField($"" + GlobalFunctionMaCun.plr5p + " - Già Làng.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574497514586137).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr5 = user.Id;
                                            GlobalFunctionMaCun.channel5 = 1;
                                            GlobalFunctionMaCun.phedan++;
                                        }
                                    }
                                    if (i == 7)
                                    {
                                        if (GlobalFunctionMaCun.plr6p == 0)
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == n + "");
                                            embed.AddField($"" + n + " - Sói Phù Thủy.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574522361774081).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr6 = user.Id;
                                            GlobalFunctionMaCun.channel6 = 1;
                                            GlobalFunctionMaCun.phesoi++;
                                        }
                                        else
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == GlobalFunctionMaCun.plr6p + "");
                                            embed.AddField($"" + GlobalFunctionMaCun.plr6p + " - Sói Phù Thủy.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574522361774081).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr6 = user.Id;
                                            GlobalFunctionMaCun.channel6 = 1;
                                            GlobalFunctionMaCun.phesoi++;
                                        }
                                    }
                                    if (i == 8)
                                    {
                                        if (GlobalFunctionMaCun.plr7p == 0)
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == n + "");
                                            embed.AddField($"" + n + " - Thợ Săn.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574545606475782).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr7 = user.Id;
                                            GlobalFunctionMaCun.channel7 = 1;
                                            GlobalFunctionMaCun.phedan++;
                                        }
                                        else
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == GlobalFunctionMaCun.plr7p + "");
                                            embed.AddField($"" + GlobalFunctionMaCun.plr7p + " - Thợ Săn.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574545606475782).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr7 = user.Id;
                                            GlobalFunctionMaCun.channel7 = 1;
                                            GlobalFunctionMaCun.phedan++;
                                        }
                                    }
                                    if (i == 9)
                                    {
                                        if (GlobalFunctionMaCun.plr8p == 0)
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == n + "");
                                            embed.AddField($"" + n + " - Thằng Ngố.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574572483706891).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr8 = user.Id;
                                            GlobalFunctionMaCun.channel8 = 1;
                                            GlobalFunctionMaCun.phedan++;
                                        }
                                        else
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == GlobalFunctionMaCun.plr8p + "");
                                            embed.AddField($"" + GlobalFunctionMaCun.plr8p + " - Thằng Ngố.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574572483706891).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr8 = user.Id;
                                            GlobalFunctionMaCun.channel8 = 1;
                                            GlobalFunctionMaCun.phedan++;
                                        }
                                    }
                                    if (i == 10)
                                    {
                                        if (GlobalFunctionMaCun.plr9p == 0)
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == n + "");
                                            embed.AddField($"" + n + " - Phù Thủy.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr9 = user.Id;
                                            GlobalFunctionMaCun.channel9 = 1;
                                            GlobalFunctionMaCun.phedan++;
                                        }
                                        else
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == GlobalFunctionMaCun.plr9p + "");
                                            embed.AddField($"" + GlobalFunctionMaCun.plr9p + " - Phù Thủy.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr9 = user.Id;
                                            GlobalFunctionMaCun.channel9 = 1;
                                            GlobalFunctionMaCun.phedan++;
                                        }
                                    }
                                    if (i == 11)
                                    {
                                        if (GlobalFunctionMaCun.plr10p == 0)
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == n + "");
                                            embed.AddField($"" + n + " - Xạ Thủ.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574616645402656).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr10 = user.Id;
                                            GlobalFunctionMaCun.channel10 = 1;
                                            GlobalFunctionMaCun.phedan++;
                                        }
                                        else
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == GlobalFunctionMaCun.plr10p + "");
                                            embed.AddField($"" + GlobalFunctionMaCun.plr10p + " - Xạ Thủ.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574616645402656).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr10 = user.Id;
                                            GlobalFunctionMaCun.channel10 = 1;
                                            GlobalFunctionMaCun.phedan++;
                                        }
                                    }
                                    if (i == 12)
                                    {
                                        if (GlobalFunctionMaCun.plr11p == 0)
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == n + "");
                                            embed.AddField($"" + n + " - Sói Băng.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574634811064342).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr11 = user.Id;
                                            GlobalFunctionMaCun.channel11 = 1;
                                            GlobalFunctionMaCun.phesoi++;
                                        }
                                        else
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == GlobalFunctionMaCun.plr11p + "");
                                            embed.AddField($"" + GlobalFunctionMaCun.plr11p + " - Sói Băng.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574634811064342).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr11 = user.Id;
                                            GlobalFunctionMaCun.channel11 = 1;
                                            GlobalFunctionMaCun.phesoi++;
                                        }
                                    }
                                    if (i == 13)
                                    {
                                        if (GlobalFunctionMaCun.plr12p == 0)
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == n + "");
                                            embed.AddField($"" + n + " - Tiên Tri.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574414660435982).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr12 = user.Id;
                                            GlobalFunctionMaCun.channel12 = 1;
                                            GlobalFunctionMaCun.phedan++;
                                        }
                                        else
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == GlobalFunctionMaCun.plr12p + "");
                                            embed.AddField($"" + GlobalFunctionMaCun.plr12p + " - Tiên Tri.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574414660435982).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr12 = user.Id;
                                            GlobalFunctionMaCun.channel12 = 1;
                                            GlobalFunctionMaCun.phedan++;
                                        }
                                    }
                                    if (i == 14)
                                    {
                                        if (GlobalFunctionMaCun.plr13p == 0)
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == n + "");
                                            embed.AddField($"" + n + " - Sát Nhân.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr13 = user.Id;
                                            GlobalFunctionMaCun.channel13 = 1;
                                            GlobalFunctionMaCun.phethu3++;
                                        }
                                        else
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == GlobalFunctionMaCun.plr13p + "");
                                            embed.AddField($"" + GlobalFunctionMaCun.plr13p + " - Sát Nhân.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr13 = user.Id;
                                            GlobalFunctionMaCun.channel13 = 1;
                                            GlobalFunctionMaCun.phethu3++;
                                        }
                                    }
                                    if (i == 15)
                                    {
                                        if (GlobalFunctionMaCun.plr14p == 0)
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == n + "");
                                            embed.AddField($"" + n + " - Gái Điếm.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(583828253681254400).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr14 = user.Id;
                                            GlobalFunctionMaCun.channel14 = 1;
                                            GlobalFunctionMaCun.phedan++;
                                        }
                                        else
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == GlobalFunctionMaCun.plr14p + "");
                                            embed.AddField($"" + GlobalFunctionMaCun.plr14p + " - Gái Điếm.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(583828253681254400).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr14 = user.Id;
                                            GlobalFunctionMaCun.channel14 = 1;
                                            GlobalFunctionMaCun.phedan++;
                                        }
                                    }
                                    if (i == 16)
                                    {
                                        if (GlobalFunctionMaCun.plr15p == 0)
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == n + "");
                                            embed.AddField($"" + n + " - Thầy Đồng.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(583828359394492427).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr15 = user.Id;
                                            GlobalFunctionMaCun.channel15 = 1;
                                            GlobalFunctionMaCun.phedan++;
                                        }
                                        else
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == GlobalFunctionMaCun.plr15p + "");
                                            embed.AddField($"" + GlobalFunctionMaCun.plr15p + " - Thầy Đồng.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(583828359394492427).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr15 = user.Id;
                                            GlobalFunctionMaCun.channel15 = 1;
                                            GlobalFunctionMaCun.phedan++;
                                        }
                                    }
                                    if (i == 17)
                                    {
                                        if (GlobalFunctionMaCun.plr16p == 0)
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == n + "");
                                            embed.AddField($"" + n + " - Sói Tri.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(583828385659355147).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr16 = user.Id;
                                            GlobalFunctionMaCun.channel16 = 1;
                                            GlobalFunctionMaCun.phesoi++;
                                        }
                                        else
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == GlobalFunctionMaCun.plr16p + "");
                                            embed.AddField($"" + GlobalFunctionMaCun.plr16p + " - Sói Tri.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(583828385659355147).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr16 = user.Id;
                                            GlobalFunctionMaCun.channel16 = 1;
                                            GlobalFunctionMaCun.phesoi++;
                                        }
                                    }
                                    if (i == 18)
                                    {
                                        if (GlobalFunctionMaCun.plr17p == 0)
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == n + "");
                                            embed.AddField($"" + n + " - Cậu Bé Hoang Dã.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr17 = user.Id;
                                            GlobalFunctionMaCun.channel17 = 1;
                                            GlobalFunctionMaCun.phedan++;
                                        }
                                        else
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == GlobalFunctionMaCun.plr16p + "");
                                            embed.AddField($"" + GlobalFunctionMaCun.plr17p + " - Cậu Bé Hoang Dã.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr17 = user.Id;
                                            GlobalFunctionMaCun.channel17 = 1;
                                            GlobalFunctionMaCun.phedan++;
                                        }
                                    }
                                    if (i == 19)
                                    {
                                        OverwritePermissions abs = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Deny, readMessageHistory: PermValue.Allow);
                                        if (GlobalFunctionMaCun.plr18p == 0)
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == n + "");
                                            embed.AddField($"" + n + " - Giáo Xứ.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(589463015212974080).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(590016837262245895).AddPermissionOverwriteAsync(user, abs.Modify());
                                            GlobalFunctionMaCun.plr18 = user.Id;
                                            GlobalFunctionMaCun.channel18 = 1;
                                            GlobalFunctionMaCun.phethu3++;
                                        }
                                        else
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == GlobalFunctionMaCun.plr18p + "");
                                            embed.AddField($"" + GlobalFunctionMaCun.plr18p + " - Giáo Xứ.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(589463015212974080).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(590016837262245895).AddPermissionOverwriteAsync(user, abs.Modify());
                                            GlobalFunctionMaCun.plr18 = user.Id;
                                            GlobalFunctionMaCun.channel18 = 1;
                                            GlobalFunctionMaCun.phethu3++;
                                        }
                                    }
                                    if (i == 20)
                                    {
                                        if (GlobalFunctionMaCun.plr19p == 0)
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == n + "");
                                            embed.AddField($"" + n + " - Thám Tử.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(591478522111983616).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr19 = user.Id;
                                            GlobalFunctionMaCun.channel19 = 1;
                                            GlobalFunctionMaCun.phedan++;
                                        }
                                        else
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == GlobalFunctionMaCun.plr19p + "");
                                            embed.AddField($"" + GlobalFunctionMaCun.plr19p + " - Thám Tử.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(591478522111983616).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr19 = user.Id;
                                            GlobalFunctionMaCun.channel19 = 1;
                                            GlobalFunctionMaCun.phedan++;
                                        }
                                    }
                                    if (i == 21)
                                    {
                                        if (GlobalFunctionMaCun.plr20p == 0)
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == n + "");
                                            embed.AddField($"" + n + " - Kẻ Tài Lanh.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(591478642966528020).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr20 = user.Id;
                                            GlobalFunctionMaCun.channel20 = 1;
                                            GlobalFunctionMaCun.phedan++;
                                        }
                                        else
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == GlobalFunctionMaCun.plr20p + "");
                                            embed.AddField($"" + GlobalFunctionMaCun.plr20p + " - Kẻ Tài Lanh.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(591478642966528020).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr20 = user.Id;
                                            GlobalFunctionMaCun.channel20 = 1;
                                            GlobalFunctionMaCun.phedan++;
                                        }
                                    }
                                    if (i == 22)
                                    {
                                        if (GlobalFunctionMaCun.plr21p == 0)
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == n + "");
                                            embed.AddField($"" + n + " - Sói Đào Hoa.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(591478711774085143).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr21 = user.Id;
                                            GlobalFunctionMaCun.channel21 = 1;
                                            GlobalFunctionMaCun.phesoi++;
                                        }
                                        else
                                        {
                                            var user = Context.Guild.Users.FirstOrDefault(x => x.Nickname == GlobalFunctionMaCun.plr21p + "");
                                            embed.AddField($"" + GlobalFunctionMaCun.plr21p + " - Sói Đào Hoa.", "" + user.Username + "");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(591478711774085143).AddPermissionOverwriteAsync(user, chophep.Modify());
                                            GlobalFunctionMaCun.plr21 = user.Id;
                                            GlobalFunctionMaCun.channel21 = 1;
                                            GlobalFunctionMaCun.phesoi++;
                                        }
                                    }
                                }
                            }
                        }
                        embed.WithColor(new Discord.Color(0, 255, 255));
                        await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("", false, embed.Build());

                        OverwritePermissions chophep2 = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Allow, readMessageHistory: PermValue.Allow);
                        OverwritePermissions khongchophep2 = new OverwritePermissions(viewChannel: PermValue.Deny, sendMessages: PermValue.Deny, readMessageHistory: PermValue.Deny);
                        OverwritePermissions khongchophep3 = new OverwritePermissions(viewChannel: PermValue.Allow, connect: PermValue.Allow, speak: PermValue.Deny, useVoiceActivation: PermValue.Deny);
                        await Context.Client.GetGuild(580555457983152149).GetVoiceChannel(580566264171331597).AddPermissionOverwriteAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"), khongchophep3.Modify());
                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580557883931099138).AddPermissionOverwriteAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"), khongchophep2.Modify());
                        GlobalFunctionMaCun.gamestatus = 1;
                        int checkplayer2 = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Count();
                        GlobalFunctionMaCun.plr = checkplayer2;
                        GlobalFunctionMaCun.thuoccuu = 1;
                        GlobalFunctionMaCun.thuocdoc = 1;
                        GlobalFunctionMaCun.khien = 1;
                        GlobalFunctionMaCun.danxathu = 1;
                        GlobalFunctionMaCun.daycount = 1;
                        GlobalFunctionMaCun.gialang = 1;
                        GlobalFunctionMaCun.thayboi = 1;
                        GlobalFunctionMaCun.tientri = 1;
                        GlobalFunctionMaCun.soitri = 1;
                        GlobalFunctionMaCun.thamtu = 1;
                        GlobalFunctionMaCun.luothoisinh = 1;
                        GlobalFunctionMaCun.lastmoi = 1;
                        GlobalFunctionMaCun.lastdongbang = 1;
                        GlobalFunctionMaCun.pha = 1;
                        GlobalFunctionMaCun.luotcanchet = 1;
                        GlobalFunctionMaCun.luotnuoi = 1;

                        if (GlobalFunctionMaCun.showroles == 1)
                        {
                            await GlobalFunctionGame.showgameroles();
                            embed2.AddField("Danh Sách Vai Trò Trong Trận!", $"{GlobalFunctionMaCun.nameroles}");
                            embed2.WithColor(new Discord.Color(0, 255, 255));
                            await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("", false, embed2.Build());
                        }
                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm thứ Nhất Bắt Đầu!");
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
                    embed.AddField($"Danh Sách Vai Trò - Số của từng Vai Trò!", "1. Bảo Vệ - Thiện - Phe Dân\n" +
                        "2. Thầy Bói - Thện - Phe Dân\n" +
                        "3. Dân - Thiện - Phe Dân\n" +
                        "4. Ma Sói - Ác - Phe Sói\n" +
                        "5. Già Làng - Thiện - Phe Dân\n" +
                        "6. Sói Phùy Thủy - Ác - Phe Sói\n" +
                        "7. Thợ Săn - Không Rõ - Phe Dân\n" +
                        "8. Thằng Ngố - Không Rõ - Phe Thứ 3\n" +
                        "9. Phù Thủy - Thiện - Phe Dân\n" +
                        "10. Xạ Thủ - Không Rõ - Phe Dân\n" +
                        "11. Sói Băng - Không Rõ - Phe Sói\n" +
                        "12. Tiên Tri - Thiện - Phe Dân\n" +
                        "13. Sát Nhân - Không Rõ - Phe Thứ 3\n" +
                        "14. Gái Điếm - Không Rõ - Phe Dân\n" +
                        "15. Thầy Đồng - Không Rõ - Phe Dân\n" +
                        "16. Sói Tri - Ác - Phe Sói\n" +
                        "17. Cậu Bé Hoang Dã - Thiện - Phe Dân\n" +
                        "18. Giáo Xứ - Không Rõ - Phe Thứ 3\n" +
                        "19. Thám Tử - Thiện - Phe Dân\n" +
                        "20. Kẻ Tài Lanh - Không Rõ - Phe Dân\n" +
                        "21. Sói Đào Hoa - Ác - Phe Sói" +
                        "");
                    embed.WithColor(new Discord.Color(255, 50, 255));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
            else return;
        }
        [Command("treo")]
        public async Task treonguoichoi(SocketUser user = null)
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
                    List<int> voted = new List<int>();
                    if (GlobalFunctionMaCun.plr1p != 0) { voted.Add(GlobalFunctionMaCun.plr1p); }
                    if (GlobalFunctionMaCun.plr2p != 0) { voted.Add(GlobalFunctionMaCun.plr2p); }
                    if (GlobalFunctionMaCun.plr3p != 0) { voted.Add(GlobalFunctionMaCun.plr3p); }
                    if (GlobalFunctionMaCun.plr4p != 0) { voted.Add(GlobalFunctionMaCun.plr4p); }
                    if (GlobalFunctionMaCun.plr5p != 0) { voted.Add(GlobalFunctionMaCun.plr5p); }
                    if (GlobalFunctionMaCun.plr6p != 0) { voted.Add(GlobalFunctionMaCun.plr6p); }
                    if (GlobalFunctionMaCun.plr7p != 0) { voted.Add(GlobalFunctionMaCun.plr7p); }
                    if (GlobalFunctionMaCun.plr8p != 0) { voted.Add(GlobalFunctionMaCun.plr8p); }
                    if (GlobalFunctionMaCun.plr9p != 0) { voted.Add(GlobalFunctionMaCun.plr9p); }
                    if (GlobalFunctionMaCun.plr10p != 0) { voted.Add(GlobalFunctionMaCun.plr10p); }
                    if (GlobalFunctionMaCun.plr11p != 0) { voted.Add(GlobalFunctionMaCun.plr11p); }
                    if (GlobalFunctionMaCun.plr12p != 0) { voted.Add(GlobalFunctionMaCun.plr12p); }
                    if (GlobalFunctionMaCun.plr13p != 0) { voted.Add(GlobalFunctionMaCun.plr13p); }
                    if (GlobalFunctionMaCun.plr14p != 0) { voted.Add(GlobalFunctionMaCun.plr14p); }
                    if (GlobalFunctionMaCun.plr15p != 0) { voted.Add(GlobalFunctionMaCun.plr15p); }
                    if (GlobalFunctionMaCun.plr16p != 0) { voted.Add(GlobalFunctionMaCun.plr16p); }
                    if (GlobalFunctionMaCun.plr17p != 0) { voted.Add(GlobalFunctionMaCun.plr17p); }
                    if (GlobalFunctionMaCun.plr18p != 0) { voted.Add(GlobalFunctionMaCun.plr18p); }
                    if (GlobalFunctionMaCun.plr19p != 0) { voted.Add(GlobalFunctionMaCun.plr19p); }
                    if (GlobalFunctionMaCun.plr20p != 0) { voted.Add(GlobalFunctionMaCun.plr20p); }
                    if (GlobalFunctionMaCun.plr21p != 0) { voted.Add(GlobalFunctionMaCun.plr21p); }

                    var q = voted.GroupBy(x => x).Select(g => new { Value = g.Key, Count = g.Count() }).OrderByDescending(x => x.Count);
                    var c = q.Max(x => x.Count);
                    int alive = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Count();
                    string aliveasstring = $"{alive}";
                    string getstring = null;
                    int dah = 0;
                    foreach (var x in q)
                    {
                        var v = x.Value;

                        if (x.Count == c)
                        {
                            if (v != 0)
                            {
                                if (dah >= 0)
                                {
                                    dah++;
                                    getstring = $"{v}";
                                }
                            }
                        }
                    }
                    double avliveasdouble = Double.Parse(aliveasstring);
                    double another = avliveasdouble / 2;
                    if (another != 1 || another != 2 || another != 3 || another != 4 || another != 5 || another != 6 || another != 7 || another != 8)
                    {
                        another = another - 0.5;
                    }

                    var userss = getstring;

                    if (c < another)
                    {
                        embed.AddField($"Lỗi!", "Số phiếu nhiều nhất so với Tổng Người Chơi có Role Sống không đủ để treo bất cứ Người Chơi nào (Sử dụng -dem để ngủ).");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else
                    {
                        var getuser = Context.Guild.Users.FirstOrDefault(x => x.Nickname == userss + "");
                        GlobalFunctionMaCun.treo = getuser.Id;
                        await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Dân Làng đã đưa " + getuser.Mention + " lên giá treo, Bây giờ " + getuser.Mention + " phải đưa ra lý do để không bị giết.");
                    }
                }
                else if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))
                {
                    if (GlobalFunctionMaCun.gamestatus >= 3)
                    {
                        if (user.Id == GlobalFunctionMaCun.treo)
                        {
                            await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Do Quản Trò có sự nhầm lẫn nên đã bỏ treo " + Context.Guild.GetUser(GlobalFunctionMaCun.treo).Nickname + ".");
                            GlobalFunctionMaCun.treo = 0;
                            GlobalFunctionMaCun.gamestatus = 3;
                        }
                        else
                        {
                            GlobalFunctionMaCun.treo = user.Id;
                            await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Dân Làng đã đưa " + user.Mention + " lên giá treo, Bây giờ " + user.Mention + " phải đưa ra lý do để không bị giết.");
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
                        GlobalFunctionMaCun.plr1p = 0;
                        GlobalFunctionMaCun.plr2p = 0;
                        GlobalFunctionMaCun.plr3p = 0;
                        GlobalFunctionMaCun.plr4p = 0;
                        GlobalFunctionMaCun.plr5p = 0;
                        GlobalFunctionMaCun.plr6p = 0;
                        GlobalFunctionMaCun.plr7p = 0;
                        GlobalFunctionMaCun.plr8p = 0;
                        GlobalFunctionMaCun.plr9p = 0;
                        GlobalFunctionMaCun.plr10p = 0;
                        GlobalFunctionMaCun.plr11p = 0;
                        GlobalFunctionMaCun.plr12p = 0;
                        GlobalFunctionMaCun.plr13p = 0;
                        GlobalFunctionMaCun.plr14p = 0;
                        GlobalFunctionMaCun.plr15p = 0;
                        GlobalFunctionMaCun.plr16p = 0;
                        GlobalFunctionMaCun.plr17p = 0;
                        GlobalFunctionMaCun.plr18p = 0;
                        GlobalFunctionMaCun.plr19p = 0;
                        GlobalFunctionMaCun.plr20p = 0;
                        GlobalFunctionMaCun.plr21p = 0;
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
                        else if (GlobalFunctionMaCun.channel14 == 1 & GlobalFunctionMaCun.plr14 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel14++;
                            GlobalFunctionMaCun.votesong++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Sống__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel15 == 1 & GlobalFunctionMaCun.plr15 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel15++;
                            GlobalFunctionMaCun.votesong++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Sống__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel16 == 1 & GlobalFunctionMaCun.plr16 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel16++;
                            GlobalFunctionMaCun.votesong++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Sống__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel17 == 1 & GlobalFunctionMaCun.plr17 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel17++;
                            GlobalFunctionMaCun.votesong++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Sống__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel18 == 1 & GlobalFunctionMaCun.plr18 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel18++;
                            GlobalFunctionMaCun.votesong++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Sống__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel19 == 1 & GlobalFunctionMaCun.plr19 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel19++;
                            GlobalFunctionMaCun.votesong++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Sống__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel20 == 1 & GlobalFunctionMaCun.plr20 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel20++;
                            GlobalFunctionMaCun.votesong++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Sống__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel21 == 1 & GlobalFunctionMaCun.plr21 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel21++;
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
                        else if (GlobalFunctionMaCun.channel14 == 1 & GlobalFunctionMaCun.plr14 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel14++;
                            GlobalFunctionMaCun.votechet++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Chết__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel15 == 1 & GlobalFunctionMaCun.plr15 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel15++;
                            GlobalFunctionMaCun.votechet++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Chết__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel16 == 1 & GlobalFunctionMaCun.plr16 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel16++;
                            GlobalFunctionMaCun.votechet++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Chết__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel17 == 1 & GlobalFunctionMaCun.plr17 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel17++;
                            GlobalFunctionMaCun.votechet++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Chết__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel18 == 1 & GlobalFunctionMaCun.plr18 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel18++;
                            GlobalFunctionMaCun.votechet++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Chết__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel19 == 1 & GlobalFunctionMaCun.plr19 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel19++;
                            GlobalFunctionMaCun.votechet++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Chết__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel20 == 1 & GlobalFunctionMaCun.plr20 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel20++;
                            GlobalFunctionMaCun.votechet++;
                            await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Chết__**.");
                            return;
                        }
                        else if (GlobalFunctionMaCun.channel21 == 1 & GlobalFunctionMaCun.plr21 == Context.User.Id)
                        {
                            GlobalFunctionMaCun.channel21++;
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
                        OverwritePermissions chophepsoi = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Allow, readMessageHistory: PermValue.Allow);
                        OverwritePermissions chophep = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Allow);
                        OverwritePermissions khongchophep = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Deny);
                        OverwritePermissions khongchophep2 = new OverwritePermissions(viewChannel: PermValue.Allow, connect: PermValue.Allow, speak: PermValue.Deny, useVoiceActivation: PermValue.Deny);
                        await Context.Client.GetGuild(580555457983152149).GetVoiceChannel(580566264171331597).AddPermissionOverwriteAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"), khongchophep2.Modify());
                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).AddPermissionOverwriteAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"), khongchophep.Modify());
                        await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Vì Dân làng không chọn người để treo hoặc phiếu bị hòa nên cả làng đi ngủ.");

                        await GlobalFunctionGame.vedem();

                        if (GlobalFunctionMaCun.moi != 0 & !Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.plr14)))
                        {
                            GlobalFunctionMaCun.moi = 0;
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
                        else if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr15)
                        {
                            await Context.Guild.GetTextChannel(583828359394492427).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                        }
                        else if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr17)
                        {
                            await Context.Guild.GetTextChannel(589462982275235860).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                        }
                        else if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr17)
                        {
                            await Context.Guild.GetTextChannel(589463015212974080).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                        }
                        else if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr19)
                        {
                            await Context.Guild.GetTextChannel(591478522111983616).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                        }
                        else if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr20)
                        {
                            await Context.Guild.GetTextChannel(591478642966528020).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                        }

                        if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr1)
                        {
                            await Context.Guild.GetTextChannel(580574363930198021).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung nên bạn không thể sử dụng lệnh vào đêm nay.");
                        }
                        else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr2)
                        {
                            await Context.Guild.GetTextChannel(580574739391578112).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung nên bạn không thể sử dụng lệnh vào đêm nay.");
                        }
                        else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr3)
                        {
                            await Context.Guild.GetTextChannel(583828385659355147).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                        }
                        else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr4)
                        {
                            await Context.Guild.GetTextChannel(583828385659355147).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                        }
                        else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr5)
                        {
                            await Context.Guild.GetTextChannel(583828385659355147).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                        }
                        else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr6)
                        {
                            await Context.Guild.GetTextChannel(583828385659355147).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                        }
                        else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr7)
                        {
                            await Context.Guild.GetTextChannel(580574545606475782).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung nên bạn không thể sử dụng lệnh vào đêm nay.");
                        }
                        else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr8)
                        {
                            await Context.Guild.GetTextChannel(583828385659355147).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                        }
                        else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr9)
                        {
                            await Context.Guild.GetTextChannel(580574598677135390).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung nên bạn không thể sử dụng lệnh vào đêm nay.");
                        }
                        else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr10)
                        {
                            await Context.Guild.GetTextChannel(583828385659355147).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                        }
                        else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr11)
                        {
                            await Context.Guild.GetTextChannel(583828385659355147).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                        }
                        else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr12)
                        {
                            await Context.Guild.GetTextChannel(580574414660435982).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung nên bạn không thể sử dụng lệnh vào đêm nay.");
                        }
                        else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr13)
                        {
                            await Context.Guild.GetTextChannel(580574812662136836).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung nên bạn không thể sử dụng lệnh vào đêm nay.");
                        }
                        else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr4)
                        {
                            await Context.Guild.GetTextChannel(583828385659355147).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                        }
                        else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr15)
                        {
                            await Context.Guild.GetTextChannel(583828359394492427).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung nên bạn không thể sử dụng lệnh vào đêm nay.");
                        }
                        else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr16)
                        {
                            await Context.Guild.GetTextChannel(583828385659355147).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                        }
                        else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr17)
                        {
                            await Context.Guild.GetTextChannel(589462982275235860).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung nên bạn không thể sử dụng lệnh vào đêm nay.");
                        }
                        else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr18)
                        {
                            await Context.Guild.GetTextChannel(589463015212974080).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung nên bạn không thể sử dụng lệnh vào đêm nay.");
                        }
                        else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr19)
                        {
                            await Context.Guild.GetTextChannel(589463015212974080).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung nên bạn không thể sử dụng lệnh vào đêm nay.");
                        }
                        else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr20)
                        {
                            await Context.Guild.GetTextChannel(589463015212974080).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                        }
                        else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr21)
                        {
                            await Context.Guild.GetTextChannel(589463015212974080).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                        }
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
                            OverwritePermissions chophepsoi = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Allow, readMessageHistory: PermValue.Allow);
                            OverwritePermissions khongchophep = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Deny);
                            OverwritePermissions khongchophep2 = new OverwritePermissions(viewChannel: PermValue.Allow, connect: PermValue.Allow, speak: PermValue.Deny, useVoiceActivation: PermValue.Deny);
                            await Context.Client.GetGuild(580555457983152149).GetVoiceChannel(580566264171331597).AddPermissionOverwriteAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"), khongchophep2.Modify());
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).AddPermissionOverwriteAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"), khongchophep.Modify());
                            await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Vì số phiếu Sống nhiều hơn số phiếu Chết hoặc cả 2 bằng nhau nên cả làng đi ngủ.");

                            await GlobalFunctionGame.vedem();

                            if (GlobalFunctionMaCun.moi != 0 & !Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.plr14)))
                            {
                                GlobalFunctionMaCun.moi = 0;
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
                            else if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr15)
                            {
                                await Context.Guild.GetTextChannel(583828359394492427).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr17)
                            {
                                await Context.Guild.GetTextChannel(589462982275235860).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr17)
                            {
                                await Context.Guild.GetTextChannel(589463015212974080).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr19)
                            {
                                await Context.Guild.GetTextChannel(591478522111983616).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr20)
                            {
                                await Context.Guild.GetTextChannel(591478642966528020).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }

                            if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr1)
                            {
                                await Context.Guild.GetTextChannel(580574363930198021).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr2)
                            {
                                await Context.Guild.GetTextChannel(580574739391578112).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr3)
                            {
                                await Context.Guild.GetTextChannel(583828385659355147).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr4)
                            {
                                await Context.Guild.GetTextChannel(583828385659355147).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr5)
                            {
                                await Context.Guild.GetTextChannel(583828385659355147).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr6)
                            {
                                await Context.Guild.GetTextChannel(583828385659355147).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr7)
                            {
                                await Context.Guild.GetTextChannel(580574545606475782).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr8)
                            {
                                await Context.Guild.GetTextChannel(583828385659355147).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr9)
                            {
                                await Context.Guild.GetTextChannel(580574598677135390).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr10)
                            {
                                await Context.Guild.GetTextChannel(583828385659355147).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr11)
                            {
                                await Context.Guild.GetTextChannel(583828385659355147).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr12)
                            {
                                await Context.Guild.GetTextChannel(580574414660435982).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr13)
                            {
                                await Context.Guild.GetTextChannel(580574812662136836).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr4)
                            {
                                await Context.Guild.GetTextChannel(583828385659355147).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr15)
                            {
                                await Context.Guild.GetTextChannel(583828359394492427).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr16)
                            {
                                await Context.Guild.GetTextChannel(583828385659355147).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr17)
                            {
                                await Context.Guild.GetTextChannel(589462982275235860).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr18)
                            {
                                await Context.Guild.GetTextChannel(589463015212974080).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr19)
                            {
                                await Context.Guild.GetTextChannel(589463015212974080).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr20)
                            {
                                await Context.Guild.GetTextChannel(589463015212974080).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr21)
                            {
                                await Context.Guild.GetTextChannel(589463015212974080).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                            }
                        }
                        else
                        {
                            OverwritePermissions chophepsoi = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Allow, readMessageHistory: PermValue.Allow);
                            OverwritePermissions khongchophep = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Deny);
                            OverwritePermissions khongchophep2 = new OverwritePermissions(viewChannel: PermValue.Allow, connect: PermValue.Allow, speak: PermValue.Deny, useVoiceActivation: PermValue.Deny);
                            await Context.Client.GetGuild(580555457983152149).GetVoiceChannel(580566264171331597).AddPermissionOverwriteAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"), khongchophep2.Modify());
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).AddPermissionOverwriteAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"), khongchophep.Modify());
                            var song = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống");
                            var chet = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết");
                            if (GlobalFunctionMaCun.treo != 0)
                            {
                                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.treo)))
                                {
                                    if (GlobalFunctionMaCun.treo != 0)
                                    {
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.treo, "ten");
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.treo, "idrole");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Dân Làng đã treo và giết chết " + Context.Guild.GetUser(GlobalFunctionMaCun.treo).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                        }
                                        else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Dân Làng đã treo và giết chết " + Context.Guild.GetUser(GlobalFunctionMaCun.treo).Nickname + ".");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.treo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.treo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        if (GlobalFunctionMaCun.treo == GlobalFunctionMaCun.plr4 || GlobalFunctionMaCun.treo == GlobalFunctionMaCun.plr6 || GlobalFunctionMaCun.treo == GlobalFunctionMaCun.plr11 || GlobalFunctionMaCun.treo == GlobalFunctionMaCun.plr16 || GlobalFunctionMaCun.treo == GlobalFunctionMaCun.plr21) GlobalFunctionMaCun.phesoi--;
                                        else GlobalFunctionMaCun.phedan--;
                                        if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.moi & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                        {
                                            GlobalFunctionMaCun.Muctieucaubehoangda = 1;
                                        }
                                        if (GlobalFunctionMaCun.treo == GlobalFunctionMaCun.plr7) GlobalFunctionMaCun.Thosanchet = 1;
                                        if (GlobalFunctionMaCun.treo == GlobalFunctionMaCun.plr18) GlobalFunctionMaCun.Giaoxuchet = 1;
                                    }
                                    if (GlobalFunctionMaCun.Thosanchet == 1)
                                    {
                                        GlobalFunctionMaCun.Thosanchet = 0;
                                        if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.keo)))
                                        {
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "ten");
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "idrole");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết chung.");
                                            }
                                            else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(chet);
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(song);
                                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.keo & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                            {
                                                GlobalFunctionMaCun.Muctieucaubehoangda = 1;
                                            }
                                            if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr18) GlobalFunctionMaCun.Giaoxuchet = 1;
                                            if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr4 || GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr6 || GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr11 || GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr16 || GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr21) GlobalFunctionMaCun.phesoi--;
                                            else GlobalFunctionMaCun.phedan--;
                                        }
                                    }

                                    if (GlobalFunctionMaCun.Giaoxuchet == 1)
                                    {
                                        GlobalFunctionMaCun.Giaoxuchet = 0;
                                        string thanhviengiaoxus = GlobalFunctionMaCun.giaoxuplr;
                                        string[] thanhviengiaoxu = thanhviengiaoxus.Split(" ");
                                        foreach (var x in thanhviengiaoxu)
                                        {
                                            var getuser = Context.Guild.Users.FirstOrDefault((y => y.Nickname == x + ""));
                                            if (getuser != null)
                                            {
                                                if (Context.Guild.Roles.FirstOrDefault(y => y.Name == "Sống").Members.Contains(Context.User))
                                                {
                                                    if (GlobalFunctionMaCun.deadroles == 1)
                                                    {
                                                        await GlobalFunctionMaCun.rolesid(getuser.Id, "ten");
                                                        await GlobalFunctionMaCun.rolesid(getuser.Id, "idrole");
                                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đức Cha Giáo Xứ đã chết, Thành Viên Giáo Xứ - " + Context.Guild.GetUser(getuser.Id).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") đã tự sát và trở thành thiên thần biết bay...");
                                                    }
                                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đức Cha Giáo Xứ đã chết, Thành Viên Giáo Xứ - " + Context.Guild.GetUser(getuser.Id).Nickname + " đã tự sát và trở thành thiên thần biết bay...");
                                                    await Context.Guild.GetUser(getuser.Id).AddRoleAsync(chet);
                                                    await Context.Guild.GetUser(getuser.Id).RemoveRoleAsync(song);
                                                    GlobalFunctionMaCun.phedan--;
                                                    if (GlobalFunctionMaCun.caubehoangda == getuser.Id & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(y => y.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(y => y.Id == GlobalFunctionMaCun.plr17)))
                                                    {
                                                        GlobalFunctionMaCun.Muctieucaubehoangda = 1;
                                                    }
                                                    if (getuser.Id == GlobalFunctionMaCun.plr7) GlobalFunctionMaCun.Thosanchet = 1;
                                                }
                                            }
                                        }
                                    }

                                    if (GlobalFunctionMaCun.Thosanchet == 1)
                                    {
                                        GlobalFunctionMaCun.Thosanchet = 0;
                                        if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.keo)))
                                        {
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "ten");
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "idrole");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết chung.");
                                            }
                                            else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(chet);
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(song);
                                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.keo & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                            {
                                                GlobalFunctionMaCun.Muctieucaubehoangda = 1;
                                            }
                                            if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr18) GlobalFunctionMaCun.Giaoxuchet = 1;
                                            if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr4 || GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr6 || GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr11 || GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr16 || GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr21) GlobalFunctionMaCun.phesoi--;
                                            else GlobalFunctionMaCun.phedan--;
                                        }
                                    }

                                    if (GlobalFunctionMaCun.Giaoxuchet == 1)
                                    {
                                        if (GlobalFunctionMaCun.giaoxuplr != null)
                                        {
                                            GlobalFunctionMaCun.Giaoxuchet = 0;
                                            string thanhviengiaoxus = GlobalFunctionMaCun.giaoxuplr;
                                            string[] thanhviengiaoxu = thanhviengiaoxus.Split(" ");
                                            foreach (var x in thanhviengiaoxu)
                                            {
                                                var getuser = Context.Guild.Users.FirstOrDefault(y => y.Nickname == x + "");
                                                if (getuser != null)
                                                {
                                                    if (Context.Guild.Roles.FirstOrDefault(y => y.Name == "Sống").Members.Contains(Context.User))
                                                    {
                                                        if (GlobalFunctionMaCun.deadroles == 1)
                                                        {
                                                            await GlobalFunctionMaCun.rolesid(getuser.Id, "ten");
                                                            await GlobalFunctionMaCun.rolesid(getuser.Id, "idrole");
                                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đức Cha Giáo Xứ đã chết, Thành Viên Giáo Xứ - " + Context.Guild.GetUser(getuser.Id).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") đã tự sát và trở thành thiên thần biết bay...");
                                                        }
                                                        else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đức Cha Giáo Xứ đã chết, Thành Viên Giáo Xứ - " + Context.Guild.GetUser(getuser.Id).Nickname + " đã tự sát và trở thành thiên thần biết bay...");
                                                        await Context.Guild.GetUser(getuser.Id).AddRoleAsync(chet);
                                                        await Context.Guild.GetUser(getuser.Id).RemoveRoleAsync(song);
                                                        GlobalFunctionMaCun.phedan--;
                                                        if (GlobalFunctionMaCun.caubehoangda == getuser.Id & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(y => y.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(y => y.Id == GlobalFunctionMaCun.plr17)))
                                                        {
                                                            GlobalFunctionMaCun.Muctieucaubehoangda = 1;
                                                        }
                                                        if (getuser.Id == GlobalFunctionMaCun.plr7) GlobalFunctionMaCun.Thosanchet = 1;
                                                    }
                                                }
                                            }
                                        }
                                    }

                                    if (GlobalFunctionMaCun.Thosanchet == 1)
                                    {
                                        GlobalFunctionMaCun.Thosanchet = 0;
                                        if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.keo)))
                                        {
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "ten");
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "idrole");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết chung.");
                                            }
                                            else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(chet);
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(song);
                                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.keo & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                            {
                                                GlobalFunctionMaCun.Muctieucaubehoangda = 1;
                                            }
                                            if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr18) GlobalFunctionMaCun.Giaoxuchet = 1;
                                            if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr4 || GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr6 || GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr11 || GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr16 || GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr21) GlobalFunctionMaCun.phesoi--;
                                            else GlobalFunctionMaCun.phedan--;
                                        }
                                    }

                                    if (GlobalFunctionMaCun.Muctieucaubehoangda == 1)
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync($"{Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17).Mention} đã gia nhập ma sói.");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }

                                }
                                else
                                {
                                    await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Vì đối tượng bị treo đã chết trước đó nên cả làng đi ngủ.");
                                }
                            }

                            if (GlobalFunctionMaCun.moi != 0 & !Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.plr14)))
                            {
                                GlobalFunctionMaCun.moi = 0;
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
                            else if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr15)
                            {
                                await Context.Guild.GetTextChannel(583828359394492427).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr17)
                            {
                                await Context.Guild.GetTextChannel(589462982275235860).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr17)
                            {
                                await Context.Guild.GetTextChannel(589463015212974080).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr19)
                            {
                                await Context.Guild.GetTextChannel(591478522111983616).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.dongbang != 0 & GlobalFunctionMaCun.dongbang == GlobalFunctionMaCun.plr20)
                            {
                                await Context.Guild.GetTextChannel(591478642966528020).SendMessageAsync("Bạn đã bị đóng băng nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }

                            if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr1)
                            {
                                await Context.Guild.GetTextChannel(580574363930198021).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr2)
                            {
                                await Context.Guild.GetTextChannel(580574739391578112).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr3)
                            {
                                await Context.Guild.GetTextChannel(583828385659355147).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr4)
                            {
                                await Context.Guild.GetTextChannel(583828385659355147).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr5)
                            {
                                await Context.Guild.GetTextChannel(583828385659355147).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr6)
                            {
                                await Context.Guild.GetTextChannel(583828385659355147).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr7)
                            {
                                await Context.Guild.GetTextChannel(580574545606475782).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr8)
                            {
                                await Context.Guild.GetTextChannel(583828385659355147).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr9)
                            {
                                await Context.Guild.GetTextChannel(580574598677135390).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr10)
                            {
                                await Context.Guild.GetTextChannel(583828385659355147).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr11)
                            {
                                await Context.Guild.GetTextChannel(583828385659355147).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr12)
                            {
                                await Context.Guild.GetTextChannel(580574414660435982).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr13)
                            {
                                await Context.Guild.GetTextChannel(580574812662136836).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr4)
                            {
                                await Context.Guild.GetTextChannel(583828385659355147).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr15)
                            {
                                await Context.Guild.GetTextChannel(583828359394492427).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr16)
                            {
                                await Context.Guild.GetTextChannel(583828385659355147).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr17)
                            {
                                await Context.Guild.GetTextChannel(589462982275235860).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr18)
                            {
                                await Context.Guild.GetTextChannel(589463015212974080).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr19)
                            {
                                await Context.Guild.GetTextChannel(589463015212974080).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung nên bạn không thể sử dụng lệnh vào đêm nay.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr20)
                            {
                                await Context.Guild.GetTextChannel(589463015212974080).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                            }
                            else if (GlobalFunctionMaCun.moi != 0 & GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr21)
                            {
                                await Context.Guild.GetTextChannel(589463015212974080).SendMessageAsync("Bạn đã bị Gái Điếm mời ngủ chung.");
                            }



                            GlobalFunctionGame.vedem();

                        }
                    }
                    else return;
                }
            }
            else return;
        }
        /*[Command("sang")]
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
                else
                {
                    var song = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống");
                    var chet = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết");
                    OverwritePermissions chophepsoi = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Allow, readMessageHistory: PermValue.Allow);
                    OverwritePermissions chophep = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Allow, attachFiles: PermValue.Deny);
                    OverwritePermissions chophep2 = new OverwritePermissions(viewChannel: PermValue.Allow, connect: PermValue.Allow, speak: PermValue.Allow);
                    await Context.Client.GetGuild(580555457983152149).GetVoiceChannel(580566264171331597).AddPermissionOverwriteAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"), chophep2.Modify());
                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).AddPermissionOverwriteAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"), chophep.Modify());
                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ngày Thứ " + GlobalFunctionMaCun.daycount + " bắt đầu, " + Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Mention + " dậy thảo luận.");

                    if (GlobalFunctionMaCun.dam == GlobalFunctionMaCun.can)
                    {
                        GlobalFunctionMaCun.can = 0;
                    }


                    if (GlobalFunctionMaCun.dam != 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.dam)))
                    {
                        if (GlobalFunctionMaCun.plr2 == GlobalFunctionMaCun.dam)
                        {
                            if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr2)
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
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
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                    await GlobalFunctionMaCun.rolestring("1", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr2)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                    await GlobalFunctionMaCun.rolestring("14", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "ten");
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr2).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr2).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr2).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr2 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                        }
                        else if (GlobalFunctionMaCun.plr3 == GlobalFunctionMaCun.dam)
                        {
                            if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr3)
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
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
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                    await GlobalFunctionMaCun.rolestring("1", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr3)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                    await GlobalFunctionMaCun.rolestring("14", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "ten");
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr3).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr3).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr3).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr3 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                        }
                        else if (GlobalFunctionMaCun.plr4 == GlobalFunctionMaCun.dam)
                        {
                            if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr4)
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
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
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                    await GlobalFunctionMaCun.rolestring("1", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr4)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                    await GlobalFunctionMaCun.rolestring("14", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "ten");
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr4).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr4).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr4).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phesoi--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr4 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                        }
                        else if (GlobalFunctionMaCun.plr5 == GlobalFunctionMaCun.dam)
                        {
                            if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr5)
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
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
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                    await GlobalFunctionMaCun.rolestring("1", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr5)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                    await GlobalFunctionMaCun.rolestring("14", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else if (GlobalFunctionMaCun.gialang >= 1)
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574497514586137).SendMessageAsync("Đêm qua bạn đã bị tấn công, nếu đợt tiếp theo bị tấn công bạn sẽ chết.");
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                GlobalFunctionMaCun.gialang--;
                            }
                            else
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "ten");
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr5).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr5).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr5).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr5 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                        }
                        else if (GlobalFunctionMaCun.plr6 == GlobalFunctionMaCun.dam)
                        {
                            if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr6)
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
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
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                    await GlobalFunctionMaCun.rolestring("1", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr6)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                    await GlobalFunctionMaCun.rolestring("14", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "ten");
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr6).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr6).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr6).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phesoi--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr6 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                        }
                        else if (GlobalFunctionMaCun.plr7 == GlobalFunctionMaCun.dam)
                        {
                            if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr7)
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
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
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                    await GlobalFunctionMaCun.rolestring("1", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr7)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                    await GlobalFunctionMaCun.rolestring("14", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "ten");
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr7).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr7).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr7).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr7 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                                if (GlobalFunctionMaCun.keo != 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.keo)))
                                {
                                    if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr1)
                                    {
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "ten");
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "idrole");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết chung.");
                                        }
                                        else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phedan--;
                                    }
                                    else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr4)
                                    {
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "ten");
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "idrole");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết chung.");
                                        }
                                        else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phesoi--;
                                    }
                                    else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr6)
                                    {
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "ten");
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "idrole");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết chung.");
                                        }
                                        else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phesoi--;
                                    }
                                    else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr9)
                                    {
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "ten");
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "idrole");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết chung.");
                                        }
                                        else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phedan--;
                                    }
                                    else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr11)
                                    {
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "ten");
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "idrole");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết chung.");
                                        }
                                        else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phesoi--;
                                    }
                                    else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr13)
                                    {
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "ten");
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "idrole");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết chung.");
                                        }
                                        else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phethu3--;
                                    }
                                    else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr16)
                                    {
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "ten");
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "idrole");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết chung.");
                                        }
                                        else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phesoi--;
                                    }
                                    else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr17 & GlobalFunctionMaCun.caubesoi == 1)
                                    {
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "ten");
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "idrole");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết chung.");
                                        }
                                        else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phesoi--;
                                    }
                                    else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr18)
                                    {
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "ten");
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "idrole");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết chung.");
                                        }
                                        else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phethu3--;
                                    }
                                    else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr21)
                                    {
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "ten");
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "idrole");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết chung.");
                                        }
                                        else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phesoi--;
                                    }
                                    else
                                    {
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "ten");
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "idrole");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết chung.");
                                        }
                                        else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phedan--;
                                    }


                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.keo & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }

                                }
                            }
                        }
                        else if (GlobalFunctionMaCun.plr8 == GlobalFunctionMaCun.dam)
                        {
                            if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr8)
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
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
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                    await GlobalFunctionMaCun.rolestring("1", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr8)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                    await GlobalFunctionMaCun.rolestring("14", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "ten");
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr8).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr8).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr8).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr8 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                        }
                        else if (GlobalFunctionMaCun.plr9 == GlobalFunctionMaCun.dam)
                        {
                            if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr9)
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
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
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                    await GlobalFunctionMaCun.rolestring("1", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr9)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                    await GlobalFunctionMaCun.rolestring("14", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "ten");
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr9).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr9).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr9).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr9 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                        }
                        else if (GlobalFunctionMaCun.plr10 == GlobalFunctionMaCun.dam)
                        {
                            if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr10)
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
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
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                    await GlobalFunctionMaCun.rolestring("1", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr10)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                    await GlobalFunctionMaCun.rolestring("14", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "ten");
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr10).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr10).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr10).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr10 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                        }
                        else if (GlobalFunctionMaCun.plr11 == GlobalFunctionMaCun.dam)
                        {
                            if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr11)
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
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
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                    await GlobalFunctionMaCun.rolestring("1", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr11)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                    await GlobalFunctionMaCun.rolestring("14", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "ten");
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr11).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr11).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr11).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phesoi--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr11 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                        }
                        else if (GlobalFunctionMaCun.plr12 == GlobalFunctionMaCun.dam)
                        {
                            if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr12)
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
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
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                    await GlobalFunctionMaCun.rolestring("1", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr12)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                    await GlobalFunctionMaCun.rolestring("14", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "ten");
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr12).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr12).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr12).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr12 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                        }
                        else if (GlobalFunctionMaCun.plr14 == GlobalFunctionMaCun.dam)
                        {
                            if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr14)
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công nữa thì bạn sẽ chết.");
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                GlobalFunctionMaCun.khien--;
                            }
                            else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr14)
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                GlobalFunctionMaCun.thuoccuu--;
                            }
                            else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr14)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                    await GlobalFunctionMaCun.rolestring("1", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else
                            {
                                if (GlobalFunctionMaCun.moi != 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.moi)))
                                {
                                    if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.plr1 == GlobalFunctionMaCun.moi)
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công nữa thì bạn sẽ chết.");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                        GlobalFunctionMaCun.khien--;
                                    }
                                    else if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.moi)
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công nữa thì bạn sẽ chết.");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                        GlobalFunctionMaCun.khien--;
                                    }
                                    else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.moi)
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                        GlobalFunctionMaCun.thuoccuu--;
                                    }
                                    else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.moi)
                                    {
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                            await GlobalFunctionMaCun.rolestring("1", "idrole");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                        }
                                        else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                        GlobalFunctionMaCun.phedan--;
                                        if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                        {
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                            GlobalFunctionMaCun.phesoi++;
                                            GlobalFunctionMaCun.phedan--;
                                            GlobalFunctionMaCun.caubesoi = 1;
                                            GlobalFunctionMaCun.caubehoangda = 0;
                                        }
                                    }
                                    if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr1)
                                    {
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "ten");
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "idrole");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                        }
                                        else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.moi).Nickname + ".");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.baoveplr = 0;
                                        GlobalFunctionMaCun.phedan--;
                                        if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                        {
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                            GlobalFunctionMaCun.phesoi++;
                                            GlobalFunctionMaCun.phedan--;
                                            GlobalFunctionMaCun.caubesoi = 1;
                                            GlobalFunctionMaCun.caubehoangda = 0;
                                        }
                                    }
                                    else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr4)
                                    {
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "ten");
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "idrole");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                        }
                                        else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.moi).Nickname + ".");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phesoi--;
                                        if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr4 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                        {
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                            GlobalFunctionMaCun.phesoi++;
                                            GlobalFunctionMaCun.phedan--;
                                            GlobalFunctionMaCun.caubesoi = 1;
                                            GlobalFunctionMaCun.caubehoangda = 0;
                                        }
                                    }
                                    else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr6)
                                    {
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "ten");
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "idrole");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                        }
                                        else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.moi).Nickname + ".");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phesoi--;
                                        if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr6 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                        {
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                            GlobalFunctionMaCun.phesoi++;
                                            GlobalFunctionMaCun.phedan--;
                                            GlobalFunctionMaCun.caubesoi = 1;
                                            GlobalFunctionMaCun.caubehoangda = 0;
                                        }
                                    }
                                    else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr9)
                                    {
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "ten");
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "idrole");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                        }
                                        else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.moi).Nickname + ".");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.cuu = 0;
                                        if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr9 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                        {
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                            GlobalFunctionMaCun.phesoi++;
                                            GlobalFunctionMaCun.phedan--;
                                            GlobalFunctionMaCun.caubesoi = 1;
                                            GlobalFunctionMaCun.caubehoangda = 0;
                                        }
                                    }
                                    else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr11)
                                    {
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "ten");
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "idrole");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                        }
                                        else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.moi).Nickname + ".");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phesoi--;
                                        if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr11 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                        {
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                            GlobalFunctionMaCun.phesoi++;
                                            GlobalFunctionMaCun.phedan--;
                                            GlobalFunctionMaCun.caubesoi = 1;
                                            GlobalFunctionMaCun.caubehoangda = 0;
                                        }
                                    }
                                    else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr13)
                                    {
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "ten");
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "idrole");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                        }
                                        else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.moi).Nickname + ".");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phethu3--;
                                        GlobalFunctionMaCun.dam = 0;
                                        if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr13 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                        {
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                            GlobalFunctionMaCun.phesoi++;
                                            GlobalFunctionMaCun.phedan--;
                                            GlobalFunctionMaCun.caubesoi = 1;
                                            GlobalFunctionMaCun.caubehoangda = 0;
                                        }
                                    }
                                    else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr16)
                                    {
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "ten");
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "idrole");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                        }
                                        else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.moi).Nickname + ".");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phesoi--;
                                        if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr16 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                        {
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                            GlobalFunctionMaCun.phesoi++;
                                            GlobalFunctionMaCun.phedan--;
                                            GlobalFunctionMaCun.caubesoi = 1;
                                            GlobalFunctionMaCun.caubehoangda = 0;
                                        }
                                    }
                                    else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr17 & GlobalFunctionMaCun.caubesoi == 1)
                                    {
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "ten");
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "idrole");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                        }
                                        else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.moi).Nickname + ".");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phesoi--;
                                    }
                                    else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr18)
                                    {
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "ten");
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "idrole");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                        }
                                        else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.moi).Nickname + ".");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phethu3--;
                                        if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr18 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                        {
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                            GlobalFunctionMaCun.phesoi++;
                                            GlobalFunctionMaCun.phedan--;
                                            GlobalFunctionMaCun.caubesoi = 1;
                                            GlobalFunctionMaCun.caubehoangda = 0;
                                        }
                                    }
                                    else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr21)
                                    {
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "ten");
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "idrole");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                        }
                                        else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.moi).Nickname + ".");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phesoi--;
                                        if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr21 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                        {
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                            GlobalFunctionMaCun.phesoi++;
                                            GlobalFunctionMaCun.phedan--;
                                            GlobalFunctionMaCun.caubesoi = 1;
                                            GlobalFunctionMaCun.caubehoangda = 0;
                                        }
                                    }
                                    else
                                    {
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "ten");
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "idrole");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                        }
                                        else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.moi).Nickname + ".");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phedan--;
                                        if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.moi & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                        {
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                            GlobalFunctionMaCun.phesoi++;
                                            GlobalFunctionMaCun.phedan--;
                                            GlobalFunctionMaCun.caubesoi = 1;
                                            GlobalFunctionMaCun.caubehoangda = 0;
                                        }
                                    }
                                }
                                else
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "ten");
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.moi & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                            }
                        }
                        else if (GlobalFunctionMaCun.plr15 == GlobalFunctionMaCun.dam)
                        {
                            if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr15)
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                GlobalFunctionMaCun.khien--;
                            }
                            else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr15)
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                GlobalFunctionMaCun.thuoccuu--;
                            }
                            else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr15)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                    await GlobalFunctionMaCun.rolestring("1", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr15)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                    await GlobalFunctionMaCun.rolestring("14", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "ten");
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr15).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr15).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr15).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr15 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                        }
                        else if (GlobalFunctionMaCun.plr16 == GlobalFunctionMaCun.dam)
                        {
                            if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr16)
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                GlobalFunctionMaCun.khien--;
                            }
                            else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr16)
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                GlobalFunctionMaCun.thuoccuu--;
                            }
                            else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr16)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                    await GlobalFunctionMaCun.rolestring("1", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr16)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                    await GlobalFunctionMaCun.rolestring("14", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "ten");
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr16).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr16).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr16).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phesoi--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr16 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                        }
                        else if (GlobalFunctionMaCun.plr17 == GlobalFunctionMaCun.dam)
                        {
                            if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr17)
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                GlobalFunctionMaCun.khien--;
                            }
                            else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr17)
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                GlobalFunctionMaCun.thuoccuu--;
                            }
                            else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr17)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                    await GlobalFunctionMaCun.rolestring("1", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr17)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                    await GlobalFunctionMaCun.rolestring("14", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else
                            {
                                if (GlobalFunctionMaCun.caubesoi == 1)
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "ten");
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr17).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr17).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr17).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phesoi--;
                                }
                                else
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "ten");
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr17).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr17).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr17).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                }
                            }
                        }
                        else if (GlobalFunctionMaCun.plr18 == GlobalFunctionMaCun.dam)
                        {
                            if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr18)
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                GlobalFunctionMaCun.khien--;
                            }
                            else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr18)
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                GlobalFunctionMaCun.thuoccuu--;
                            }
                            else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr18)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                    await GlobalFunctionMaCun.rolestring("1", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr18)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                    await GlobalFunctionMaCun.rolestring("14", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "ten");
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr18).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr18).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr18).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phethu3--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr18 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                        }
                        else if (GlobalFunctionMaCun.plr19 == GlobalFunctionMaCun.dam)
                        {
                            if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr19)
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                GlobalFunctionMaCun.khien--;
                            }
                            else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr19)
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                GlobalFunctionMaCun.thuoccuu--;
                            }
                            else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr19)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                    await GlobalFunctionMaCun.rolestring("1", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr19)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                    await GlobalFunctionMaCun.rolestring("14", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr19 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "ten");
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr19).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr19).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr19).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr19 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                        }
                        else if (GlobalFunctionMaCun.plr20 == GlobalFunctionMaCun.dam)
                        {
                            if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr20)
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                GlobalFunctionMaCun.khien--;
                            }
                            else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr20)
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                GlobalFunctionMaCun.thuoccuu--;
                            }
                            else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr20)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                    await GlobalFunctionMaCun.rolestring("1", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr20)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                    await GlobalFunctionMaCun.rolestring("14", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "ten");
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr20).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr20).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr20).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr20 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                        }
                        else if (GlobalFunctionMaCun.plr21 == GlobalFunctionMaCun.dam)
                        {
                            if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr21)
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                GlobalFunctionMaCun.khien--;
                            }
                            else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr21)
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                GlobalFunctionMaCun.thuoccuu--;
                            }
                            else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr21)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                    await GlobalFunctionMaCun.rolestring("1", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr21)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                    await GlobalFunctionMaCun.rolestring("14", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "ten");
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr21).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr21).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr21).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phesoi--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr21 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                        }
                        else if (GlobalFunctionMaCun.plr1 == GlobalFunctionMaCun.dam)
                        {
                            if (GlobalFunctionMaCun.khien >= 1)
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                GlobalFunctionMaCun.khien--;
                            }
                            else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr1)
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                GlobalFunctionMaCun.thuoccuu--;
                            }
                            else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr1)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                    await GlobalFunctionMaCun.rolestring("14", "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                            else
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "ten");
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                            }
                        }
                        else GlobalFunctionMaCun.dam = 0;
                    }
                    if (GlobalFunctionMaCun.can != 0)
                    {
                        if (!Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.can)))
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
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
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
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                        await GlobalFunctionMaCun.rolestring("1", "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                                else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr2)
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                        await GlobalFunctionMaCun.rolestring("14", "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                                else
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "ten");
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr2).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr2).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr2).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.sometest = 1;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr2 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                            }
                            else if (GlobalFunctionMaCun.plr3 == GlobalFunctionMaCun.can)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr3)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
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
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                        await GlobalFunctionMaCun.rolestring("1", "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                                else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr3)
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                        await GlobalFunctionMaCun.rolestring("14", "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                                else
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "ten");
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr3).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr3).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr3).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr3 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                            }
                            else if (GlobalFunctionMaCun.plr5 == GlobalFunctionMaCun.can)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr5)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
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
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr5)
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                        await GlobalFunctionMaCun.rolestring("1", "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                                else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr5)
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                        await GlobalFunctionMaCun.rolestring("14", "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                                else if (GlobalFunctionMaCun.gialang >= 1)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574497514586137).SendMessageAsync("Đêm qua bạn đã bị tấn công, nếu đợt tiếp theo bị tấn công bạn sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.gialang--;
                                }
                                else
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "ten");
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr5).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr5).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr5).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr5 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                            }
                            else if (GlobalFunctionMaCun.plr7 == GlobalFunctionMaCun.can)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr7)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
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
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                        await GlobalFunctionMaCun.rolestring("1", "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                                else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr7)
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                        await GlobalFunctionMaCun.rolestring("14", "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                                else
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "ten");
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr7).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr7).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr7).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr7 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                    if (GlobalFunctionMaCun.keo != 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.keo)))
                                    {
                                        if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr1)
                                        {
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "ten");
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "idrole");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết chung.");
                                            }
                                            else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phedan--;
                                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                            {
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                                GlobalFunctionMaCun.phesoi++;
                                                GlobalFunctionMaCun.phedan--;
                                                GlobalFunctionMaCun.caubesoi = 1;
                                                GlobalFunctionMaCun.caubehoangda = 0;
                                            }
                                        }
                                        else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr4)
                                        {
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "ten");
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "idrole");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết chung.");
                                            }
                                            else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phesoi--;
                                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr4 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                            {
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                                GlobalFunctionMaCun.phesoi++;
                                                GlobalFunctionMaCun.phedan--;
                                                GlobalFunctionMaCun.caubesoi = 1;
                                                GlobalFunctionMaCun.caubehoangda = 0;
                                            }
                                        }
                                        else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr6)
                                        {
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "ten");
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "idrole");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết chung.");
                                            }
                                            else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phesoi--;
                                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr6 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                            {
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                                GlobalFunctionMaCun.phesoi++;
                                                GlobalFunctionMaCun.phedan--;
                                                GlobalFunctionMaCun.caubesoi = 1;
                                                GlobalFunctionMaCun.caubehoangda = 0;
                                            }
                                        }
                                        else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr9)
                                        {
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "ten");
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "idrole");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết chung.");
                                            }
                                            else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phedan--;
                                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr9 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                            {
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                                GlobalFunctionMaCun.phesoi++;
                                                GlobalFunctionMaCun.phedan--;
                                                GlobalFunctionMaCun.caubesoi = 1;
                                                GlobalFunctionMaCun.caubehoangda = 0;
                                            }
                                        }
                                        else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr11)
                                        {
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "ten");
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "idrole");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết chung.");
                                            }
                                            else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phesoi--;
                                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr11 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                            {
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                                GlobalFunctionMaCun.phesoi++;
                                                GlobalFunctionMaCun.phedan--;
                                                GlobalFunctionMaCun.caubesoi = 1;
                                                GlobalFunctionMaCun.caubehoangda = 0;
                                            }
                                        }
                                        else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr13)
                                        {
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "ten");
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "idrole");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết chung.");
                                            }
                                            else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phethu3--;
                                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr13 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                            {
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                                GlobalFunctionMaCun.phesoi++;
                                                GlobalFunctionMaCun.phedan--;
                                                GlobalFunctionMaCun.caubesoi = 1;
                                                GlobalFunctionMaCun.caubehoangda = 0;
                                            }
                                        }
                                        else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr16)
                                        {
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "ten");
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "idrole");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết chung.");
                                            }
                                            else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phesoi--;
                                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr16 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                            {
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                                GlobalFunctionMaCun.phesoi++;
                                                GlobalFunctionMaCun.phedan--;
                                                GlobalFunctionMaCun.caubesoi = 1;
                                                GlobalFunctionMaCun.caubehoangda = 0;
                                            }
                                        }
                                        else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr17 & GlobalFunctionMaCun.caubesoi == 1)
                                        {
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "ten");
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "idrole");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết chung.");
                                            }
                                            else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phesoi--;
                                        }
                                        else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr18)
                                        {
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "ten");
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "idrole");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết chung.");
                                            }
                                            else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phethu3--;
                                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr18 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                            {
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                                GlobalFunctionMaCun.phesoi++;
                                                GlobalFunctionMaCun.phedan--;
                                                GlobalFunctionMaCun.caubesoi = 1;
                                                GlobalFunctionMaCun.caubehoangda = 0;
                                            }
                                        }
                                        else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr21)
                                        {
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "ten");
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "idrole");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết chung.");
                                            }
                                            else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phesoi--;
                                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr21 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                            {
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                                GlobalFunctionMaCun.phesoi++;
                                                GlobalFunctionMaCun.phedan--;
                                                GlobalFunctionMaCun.caubesoi = 1;
                                                GlobalFunctionMaCun.caubehoangda = 0;
                                            }
                                        }
                                        else
                                        {
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "ten");
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "idrole");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết chung.");
                                            }
                                            else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phedan--;
                                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.keo & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                            {
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                                GlobalFunctionMaCun.phesoi++;
                                                GlobalFunctionMaCun.phedan--;
                                                GlobalFunctionMaCun.caubesoi = 1;
                                                GlobalFunctionMaCun.caubehoangda = 0;
                                            }
                                        }
                                    }
                                }
                            }
                            else if (GlobalFunctionMaCun.plr8 == GlobalFunctionMaCun.can)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr8)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
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
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                        await GlobalFunctionMaCun.rolestring("1", "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                                else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr8)
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                        await GlobalFunctionMaCun.rolestring("14", "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                                else
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "ten");
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr8).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr8).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr8).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr8 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                            }
                            else if (GlobalFunctionMaCun.plr9 == GlobalFunctionMaCun.can)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr9)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
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
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                        await GlobalFunctionMaCun.rolestring("1", "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                                else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr9)
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                        await GlobalFunctionMaCun.rolestring("14", "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                                else
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "ten");
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr9).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr9).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr9).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr9 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                            }
                            else if (GlobalFunctionMaCun.plr10 == GlobalFunctionMaCun.can)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr10)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
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
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                        await GlobalFunctionMaCun.rolestring("1", "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                                else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr10)
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                        await GlobalFunctionMaCun.rolestring("14", "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                                else
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "ten");
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr10).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr10).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr10).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr10 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                            }
                            else if (GlobalFunctionMaCun.plr12 == GlobalFunctionMaCun.can)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr12)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
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
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                        await GlobalFunctionMaCun.rolestring("1", "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                                else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr12)
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                        await GlobalFunctionMaCun.rolestring("14", "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                                else
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "ten");
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr12).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr12).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr12).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr12 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                            }
                            else if (GlobalFunctionMaCun.plr14 == GlobalFunctionMaCun.can)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr14)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr14)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr14)
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                        await GlobalFunctionMaCun.rolestring("1", "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                                else
                                {
                                    if (GlobalFunctionMaCun.moi != 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.moi)))
                                    {
                                        if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.plr1 == GlobalFunctionMaCun.moi)
                                        {
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                            GlobalFunctionMaCun.khien--;
                                        }
                                        else if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.moi)
                                        {
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                            GlobalFunctionMaCun.khien--;
                                        }
                                        else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.moi)
                                        {
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                            GlobalFunctionMaCun.thuoccuu--;
                                        }
                                        else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.moi)
                                        {
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                                await GlobalFunctionMaCun.rolestring("1", "idrole");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                            }
                                            else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                            GlobalFunctionMaCun.phedan--;
                                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                            {
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                                GlobalFunctionMaCun.phesoi++;
                                                GlobalFunctionMaCun.phedan--;
                                                GlobalFunctionMaCun.caubesoi = 1;
                                                GlobalFunctionMaCun.caubehoangda = 0;
                                            }
                                        }
                                        else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr1)
                                        {
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "ten");
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "idrole");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                            }
                                            else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.moi).Nickname + ".");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.moi).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.moi).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.baoveplr = 0;
                                            GlobalFunctionMaCun.phedan--;
                                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                            {
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                                GlobalFunctionMaCun.phesoi++;
                                                GlobalFunctionMaCun.phedan--;
                                                GlobalFunctionMaCun.caubesoi = 1;
                                                GlobalFunctionMaCun.caubehoangda = 0;
                                            }
                                        }
                                        else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr4)
                                        {
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "ten");
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "idrole");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                            }
                                            else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.moi).Nickname + ".");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.moi).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.moi).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phesoi--;
                                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr4 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                            {
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                                GlobalFunctionMaCun.phesoi++;
                                                GlobalFunctionMaCun.phedan--;
                                                GlobalFunctionMaCun.caubesoi = 1;
                                                GlobalFunctionMaCun.caubehoangda = 0;
                                            }
                                        }
                                        else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr6)
                                        {
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "ten");
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "idrole");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                            }
                                            else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.moi).Nickname + ".");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.moi).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.moi).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phesoi--;
                                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr6 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                            {
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                                GlobalFunctionMaCun.phesoi++;
                                                GlobalFunctionMaCun.phedan--;
                                                GlobalFunctionMaCun.caubesoi = 1;
                                                GlobalFunctionMaCun.caubehoangda = 0;
                                            }
                                        }
                                        else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr9)
                                        {
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "ten");
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "idrole");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                            }
                                            else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.moi).Nickname + ".");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.moi).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.moi).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phedan--;
                                            GlobalFunctionMaCun.cuu = 0;
                                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr9 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                            {
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                                GlobalFunctionMaCun.phesoi++;
                                                GlobalFunctionMaCun.phedan--;
                                                GlobalFunctionMaCun.caubesoi = 1;
                                                GlobalFunctionMaCun.caubehoangda = 0;
                                            }
                                        }
                                        else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr11)
                                        {
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "ten");
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "idrole");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                            }
                                            else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.moi).Nickname + ".");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.moi).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.moi).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phesoi--;
                                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr11 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                            {
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                                GlobalFunctionMaCun.phesoi++;
                                                GlobalFunctionMaCun.phedan--;
                                                GlobalFunctionMaCun.caubesoi = 1;
                                                GlobalFunctionMaCun.caubehoangda = 0;
                                            }
                                        }
                                        else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr13)
                                        {
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                        }
                                        else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr16)
                                        {
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "ten");
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "idrole");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                            }
                                            else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.moi).Nickname + ".");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.moi).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.moi).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phesoi--;
                                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr16 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                            {
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                                GlobalFunctionMaCun.phesoi++;
                                                GlobalFunctionMaCun.phedan--;
                                                GlobalFunctionMaCun.caubesoi = 1;
                                                GlobalFunctionMaCun.caubehoangda = 0;
                                            }
                                        }
                                        else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr17 & GlobalFunctionMaCun.caubesoi == 1)
                                        {
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "ten");
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "idrole");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                            }
                                            else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.moi).Nickname + ".");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.moi).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.moi).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phesoi--;
                                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr17 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                            {
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                                GlobalFunctionMaCun.phesoi++;
                                                GlobalFunctionMaCun.phedan--;
                                                GlobalFunctionMaCun.caubesoi = 1;
                                                GlobalFunctionMaCun.caubehoangda = 0;
                                            }
                                        }
                                        else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr18)
                                        {
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "ten");
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "idrole");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                            }
                                            else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.moi).Nickname + ".");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.moi).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.moi).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phethu3--;
                                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr18 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                            {
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                                GlobalFunctionMaCun.phesoi++;
                                                GlobalFunctionMaCun.phedan--;
                                                GlobalFunctionMaCun.caubesoi = 1;
                                                GlobalFunctionMaCun.caubehoangda = 0;
                                            }
                                        }
                                        else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr21)
                                        {
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "ten");
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "idrole");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                            }
                                            else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.moi).Nickname + ".");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.moi).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.moi).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phesoi--;
                                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr21 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                            {
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                                GlobalFunctionMaCun.phesoi++;
                                                GlobalFunctionMaCun.phedan--;
                                                GlobalFunctionMaCun.caubesoi = 1;
                                                GlobalFunctionMaCun.caubehoangda = 0;
                                            }
                                        }
                                        else
                                        {
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "ten");
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "idrole");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                            }
                                            else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.moi).Nickname + ".");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.moi).AddRoleAsync(chet);
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.moi).RemoveRoleAsync(song);
                                            GlobalFunctionMaCun.phedan--;
                                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.moi & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                            {
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                                GlobalFunctionMaCun.phesoi++;
                                                GlobalFunctionMaCun.phedan--;
                                                GlobalFunctionMaCun.caubesoi = 1;
                                                GlobalFunctionMaCun.caubehoangda = 0;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "ten");
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "idrole");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                        }
                                        else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                        GlobalFunctionMaCun.phedan--;
                                        if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                        {
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                            GlobalFunctionMaCun.phesoi++;
                                            GlobalFunctionMaCun.phedan--;
                                            GlobalFunctionMaCun.caubesoi = 1;
                                            GlobalFunctionMaCun.caubehoangda = 0;
                                        }
                                    }
                                }
                            }
                            else if (GlobalFunctionMaCun.plr15 == GlobalFunctionMaCun.can)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr15)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr15)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr15)
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                        await GlobalFunctionMaCun.rolestring("1", "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                                else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr15)
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                        await GlobalFunctionMaCun.rolestring("14", "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                                else
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "ten");
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr15).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr15).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr15).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr15 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                            }
                            else if (GlobalFunctionMaCun.plr17 == GlobalFunctionMaCun.can)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr17)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr17)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr17)
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                        await GlobalFunctionMaCun.rolestring("1", "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                                else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr17)
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                        await GlobalFunctionMaCun.rolestring("14", "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                                else
                                {
                                    if (GlobalFunctionMaCun.caubesoi == 1)
                                    {
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "ten");
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "idrole");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                        }
                                        else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr17).Nickname + ".");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.plr17).AddRoleAsync(chet);
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.plr17).RemoveRoleAsync(song);
                                        GlobalFunctionMaCun.phesoi--;
                                    }
                                    else
                                    {
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "ten");
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "idrole");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                        }
                                        else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr17).Nickname + ".");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.plr17).AddRoleAsync(chet);
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.plr17).RemoveRoleAsync(song);
                                        GlobalFunctionMaCun.phedan--;
                                    }
                                }
                            }
                            else if (GlobalFunctionMaCun.plr18 == GlobalFunctionMaCun.can)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr18)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr18)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr18)
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                        await GlobalFunctionMaCun.rolestring("1", "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                                else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr18)
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                        await GlobalFunctionMaCun.rolestring("14", "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                                else
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "ten");
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr18).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr18).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr18).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phethu3--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr18 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                            }
                            else if (GlobalFunctionMaCun.plr19 == GlobalFunctionMaCun.can)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr19)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr19)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr19)
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                        await GlobalFunctionMaCun.rolestring("1", "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                                else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr19)
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                        await GlobalFunctionMaCun.rolestring("14", "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                                else
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "ten");
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr19).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr19).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr19).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr19 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                            }
                            else if (GlobalFunctionMaCun.plr20 == GlobalFunctionMaCun.can)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr20)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.khien--;
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1 & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.plr20)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    GlobalFunctionMaCun.thuoccuu--;
                                }
                                else if (GlobalFunctionMaCun.khien == 0 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr20)
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                        await GlobalFunctionMaCun.rolestring("1", "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                                else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr20)
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                        await GlobalFunctionMaCun.rolestring("14", "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                                else
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "ten");
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr20).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr20).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr20).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr20 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                            }
                            else if (GlobalFunctionMaCun.plr1 == GlobalFunctionMaCun.can)
                            {
                                if (GlobalFunctionMaCun.khien >= 1)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
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
                                else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr1)
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                        await GlobalFunctionMaCun.rolestring("14", "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                                else
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "ten");
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                            }
                            else if (GlobalFunctionMaCun.plr13 == GlobalFunctionMaCun.can)
                            {
                                if (GlobalFunctionMaCun.khien >= 1 & GlobalFunctionMaCun.baoveplr == GlobalFunctionMaCun.plr13)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
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
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                        await GlobalFunctionMaCun.rolestring("1", "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                    if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                }
                                else
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                                }
                            }
                        }
                    }

                    if (GlobalFunctionMaCun.can == 0)
                    {
                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                    }

                    if (GlobalFunctionMaCun.canchet != 0)
                    {
                        if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.canchet)))
                        {
                            if (GlobalFunctionMaCun.deadroles == 1)
                            {
                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.canchet, "ten");
                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.canchet, "idrole");
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sói Đào Hoa sừ dụng hack và cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                            }
                            else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sói Đào Hoa sừ dụng hack và cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.canchet).Nickname + ".");
                            await Context.Guild.GetUser(GlobalFunctionMaCun.canchet).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                            await Context.Guild.GetUser(GlobalFunctionMaCun.canchet).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                            GlobalFunctionMaCun.luotcanchet--;
                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.canchet & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                GlobalFunctionMaCun.phesoi++;
                                GlobalFunctionMaCun.phedan--;
                                GlobalFunctionMaCun.caubesoi = 1;
                                GlobalFunctionMaCun.caubehoangda = 0;
                            }
                        }
                        else
                        {
                            await Context.Client.GetGuild(591478711774085143).GetTextChannel(580563096544739331).SendMessageAsync("Người Chơi đã chết trước khi bạn cắn, đừng lo lắng, vì bạn có thể cắn lại khi về đêm.");
                        }
                    }

                    if (GlobalFunctionMaCun.hoisinh != 0)
                    {
                        var plrspawn = Context.Guild.GetUser(GlobalFunctionMaCun.hoisinh);
                        if (GlobalFunctionMaCun.deadroles == 1)
                        {
                            await GlobalFunctionMaCun.rolesid(plrspawn.Id, "ten");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thầy Đồng đã hồi sinh " + Context.Guild.GetUser(plrspawn.Id).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                        }
                        else Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thầy Đồng đã hồi sinh " + plrspawn.Nickname + ".");
                        await plrspawn.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                        await plrspawn.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                        if (GlobalFunctionMaCun.hoisinh == GlobalFunctionMaCun.plr1)
                        {
                            GlobalFunctionMaCun.khien = 1;
                            GlobalFunctionMaCun.phedan++;
                        }
                        else if (GlobalFunctionMaCun.hoisinh == GlobalFunctionMaCun.plr4 || GlobalFunctionMaCun.hoisinh == GlobalFunctionMaCun.plr6 || GlobalFunctionMaCun.hoisinh == GlobalFunctionMaCun.plr11 || GlobalFunctionMaCun.hoisinh == GlobalFunctionMaCun.plr16 || GlobalFunctionMaCun.hoisinh == GlobalFunctionMaCun.plr21 || GlobalFunctionMaCun.hoisinh == GlobalFunctionMaCun.plr17 & GlobalFunctionMaCun.caubesoi == 1)
                        {
                            GlobalFunctionMaCun.phesoi++;
                        }
                        else if (GlobalFunctionMaCun.hoisinh == GlobalFunctionMaCun.plr13)
                        {
                            GlobalFunctionMaCun.phethu3++;
                        }
                        else
                        {
                            GlobalFunctionMaCun.phedan++;
                        }

                        GlobalFunctionMaCun.hoisinh = 0;
                        GlobalFunctionMaCun.luothoisinh--;
                    }

                    /*if (!Context.Guild.GetUser(GlobalFunctionMaCun.plr18).Roles.Any(x => x.Name == "Sống") & GlobalFunctionMaCun.giaoxuplr != null)
                    {
                        string name = GlobalFunctionMaCun.giaoxuplr;
                        string[] name2 = name.Split(" ");
                        foreach (var name3 in name2)
                        {
                            var getuser = Context.Guild.Users.FirstOrDefault(x => x.Nickname == name3 + "");
                            if (getuser != null)
                            {
                                var getuser2 = Context.Guild.Users.FirstOrDefault(x => x.Nickname == name3 + "").Id;
                                if (Context.Guild.GetUser(getuser2).Roles.Any(x => x.Name == "Sống"))
                                {
                                    await getuser.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                    await getuser.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        GlobalFunctionMaCun.rolesid(getuser2, "ten");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + name3 + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + name3 + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                }
                            }
                        }
                        GlobalFunctionMaCun.giaoxuplr = null;
                    }

                    OverwritePermissions chophepgiaoxu = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Deny, readMessageHistory: PermValue.Allow);
                    if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.plr18)) & GlobalFunctionMaCun.giaoxu != 0)
                    {
                        var giaoxu = GlobalFunctionMaCun.giaoxu;
                        var plr4 = GlobalFunctionMaCun.plr4;
                        var plr6 = GlobalFunctionMaCun.plr6;
                        var plr11 = GlobalFunctionMaCun.plr11;
                        var plr13 = GlobalFunctionMaCun.plr13;
                        var plr16 = GlobalFunctionMaCun.plr16;
                        var plr17 = GlobalFunctionMaCun.plr17;
						var plr21 = GlobalFunctionMaCun.plr21;

                        if (giaoxu == plr4 || giaoxu == plr6 || giaoxu == plr11 || giaoxu == plr13 || giaoxu == plr16 || giaoxu == plr17 & GlobalFunctionMaCun.caubesoi == 1 & giaoxu == plr21)
                        {
                            GlobalFunctionMaCun.giaoxu = 0;
                        }
                        else if (!Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu)))
                        {
                            GlobalFunctionMaCun.giaoxu = 0;
                        }
                        else
                        {
                            var user3 = Context.Guild.GetUser(giaoxu);
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(590016837262245895).AddPermissionOverwriteAsync(user3, chophepgiaoxu.Modify());
                            await GlobalFunctionMaCun.rolesid(giaoxu, "channelid");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(GlobalFunctionMaCun.channelroles).SendMessageAsync("Bạn dã được Đức Cha Giáo Xứ mời vào Thánh Đường, từ giờ bạn sẽ thắng cùng phe Giáo Xứ.\n<#590016837262245895> Để xem các thành viên của giáo xứ.");

                            if (GlobalFunctionMaCun.giaoxuplr == null) { GlobalFunctionMaCun.giaoxuplr = $"{user3.Nickname.ToString()}"; }
                            else GlobalFunctionMaCun.giaoxuplr = $"{GlobalFunctionMaCun.giaoxuplr} {user3.Nickname.ToString()}";

                        }
                    }

                    if (GlobalFunctionMaCun.nuoi != 0 & GlobalFunctionMaCun.lamme == 1)
                    {
                        if (!Context.Guild.GetUser(GlobalFunctionMaCun.plr19).Roles.Any(x => x.Name == "Sống") & Context.Guild.GetUser(GlobalFunctionMaCun.nuoi).Roles.Any(x => x.Name == "Sống"))
                        {
                            if (GlobalFunctionMaCun.deadroles == 1)
                            {
                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.nuoi, "ten");
                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.nuoi, "idrole");
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Vì không còn mẹ để nương tựa nên " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") đã tự sát.");
                            }
                            else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Vì không còn mẹ để nương tựa nên " + Context.Guild.GetUser(GlobalFunctionMaCun.nuoi).Nickname + " đã tự sát.");
                            await Context.Guild.GetUser(GlobalFunctionMaCun.nuoi).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                            await Context.Guild.GetUser(GlobalFunctionMaCun.nuoi).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                            GlobalFunctionMaCun.nuoi = 0;
                            GlobalFunctionMaCun.lamme = 0;
                        }
                        else if (Context.Guild.GetUser(GlobalFunctionMaCun.plr19).Roles.Any(x => x.Name == "Sống") & Context.Guild.GetUser(GlobalFunctionMaCun.nuoi).Roles.Any(x => x.Name == "Sống"))
                        {
                            GlobalFunctionMaCun.lamme = 1;
                        }
                        else if (Context.Guild.GetUser(GlobalFunctionMaCun.plr19).Roles.Any(x => x.Name == "Sống") & !Context.Guild.GetUser(GlobalFunctionMaCun.nuoi).Roles.Any(x => x.Name == "Sống"))
                        {
                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr19, "channelid");
                            await Context.Guild.GetTextChannel(GlobalFunctionMaCun.channelroles).SendMessageAsync("Vì đứa con của bạn đã chết nên từ giờ bạn sẽ thằng cùng Phe Dân Làng.");
                            GlobalFunctionMaCun.nuoi = 0;
                        }
                    }

                    if (GlobalFunctionMaCun.dongbang != 0)
                    {
                        GlobalFunctionMaCun.lastdongbang = GlobalFunctionMaCun.dongbang;
                    }
                    if (GlobalFunctionMaCun.moi != 0)
                    {
                        GlobalFunctionMaCun.lastmoi = GlobalFunctionMaCun.moi;
                    }
                    GlobalFunctionMaCun.can = 0;
                    GlobalFunctionMaCun.dam = 0;
                    GlobalFunctionMaCun.cuu = 0;
                    GlobalFunctionMaCun.moi = 0;
                    GlobalFunctionMaCun.giaoxu = 0;
                    GlobalFunctionMaCun.baoveplr = 0;
                    GlobalFunctionMaCun.dongbang = 0;
                    GlobalFunctionMaCun.phuphep = 0;
                    GlobalFunctionMaCun.canchet = 0;
                    GlobalFunctionMaCun.gamestatus = 2;

                    if (GlobalFunctionMaCun.sometest == 1)
                    {
                        await ReplyAsync("Lệnh được thực thi, 2 đã chết.");
                    }
                }
            }
            else return;
        }*/
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
                else
                {
                    var song = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống");
                    var chet = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết");
                    OverwritePermissions chophepsoi = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Allow, readMessageHistory: PermValue.Allow);
                    OverwritePermissions chophep = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Allow, attachFiles: PermValue.Deny);
                    OverwritePermissions chophep2 = new OverwritePermissions(viewChannel: PermValue.Allow, connect: PermValue.Allow, speak: PermValue.Allow, useVoiceActivation: PermValue.Allow);
                    await Context.Client.GetGuild(580555457983152149).GetVoiceChannel(580566264171331597).AddPermissionOverwriteAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"), chophep2.Modify());
                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).AddPermissionOverwriteAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"), chophep.Modify());
                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ngày Thứ " + GlobalFunctionMaCun.daycount + " bắt đầu, " + Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Mention + " dậy thảo luận.");

                    if (GlobalFunctionMaCun.dam == GlobalFunctionMaCun.can)
                    {
                        GlobalFunctionMaCun.can = 0;
                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                    }

                    if (GlobalFunctionMaCun.dam != 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User))
                    {
                        if (GlobalFunctionMaCun.dam == GlobalFunctionMaCun.baoveplr & GlobalFunctionMaCun.khien >= 1)
                        {
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                            GlobalFunctionMaCun.khien--;
                        }
                        if (GlobalFunctionMaCun.dam == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.khien >= 1)
                        {
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                            GlobalFunctionMaCun.khien--;
                        }
                        else if (GlobalFunctionMaCun.dam == GlobalFunctionMaCun.baoveplr & GlobalFunctionMaCun.dam == GlobalFunctionMaCun.moi & GlobalFunctionMaCun.khien >= 1)
                        {
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                            GlobalFunctionMaCun.khien--;
                        }
                        else if (GlobalFunctionMaCun.dam == GlobalFunctionMaCun.cuu & GlobalFunctionMaCun.thuoccuu >= 1)
                        {
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                            GlobalFunctionMaCun.thuoccuu--;
                        }
                        else if (GlobalFunctionMaCun.dam == GlobalFunctionMaCun.cuu & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.moi & GlobalFunctionMaCun.thuoccuu >= 1)
                        {
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                            GlobalFunctionMaCun.thuoccuu--;
                        }
                        else if (GlobalFunctionMaCun.dam == GlobalFunctionMaCun.plr5 & GlobalFunctionMaCun.gialang >= 1)
                        {
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574497514586137).SendMessageAsync("Đêm qua bạn đã bị tấn công, nếu đợt tiếp theo còn bị tấn công nữa thì bạn sẽ chết.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574812662136836).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                            GlobalFunctionMaCun.gialang--;
                        }
                        else if (GlobalFunctionMaCun.dam == GlobalFunctionMaCun.moi)
                        {
                            if (GlobalFunctionMaCun.deadroles == 1)
                            {
                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "idrole");
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                            }
                            else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                            await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                            await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                            GlobalFunctionMaCun.phedan--;
                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                            {
                                GlobalFunctionMaCun.Muctieucaubehoangda = 1;
                            }
                        }
                        else if (GlobalFunctionMaCun.dam == GlobalFunctionMaCun.baoveplr & GlobalFunctionMaCun.khien == 0)
                        {
                            if (GlobalFunctionMaCun.deadroles == 1)
                            {
                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "idrole");
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                            }
                            else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                            await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                            await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                            GlobalFunctionMaCun.phedan--;
                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                            {
                                GlobalFunctionMaCun.Muctieucaubehoangda = 1;
                            }
                        }
                        else
                        {
                            if (GlobalFunctionMaCun.dam == GlobalFunctionMaCun.plr4)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "ten");
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.dam).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.dam).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.dam).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.dam).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phesoi--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.dam & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    GlobalFunctionMaCun.Muctieucaubehoangda = 1;
                                }
                            }
                            else if (GlobalFunctionMaCun.dam == GlobalFunctionMaCun.plr6)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "ten");
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.dam).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.dam).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.dam).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.dam).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phesoi--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.dam & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    GlobalFunctionMaCun.Muctieucaubehoangda = 1;
                                }
                            }
                            else if (GlobalFunctionMaCun.dam == GlobalFunctionMaCun.plr11)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "ten");
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.dam).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.dam).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.dam).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.dam).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phesoi--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.dam & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    GlobalFunctionMaCun.Muctieucaubehoangda = 1;
                                }
                            }
                            else if (GlobalFunctionMaCun.dam == GlobalFunctionMaCun.plr16)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "ten");
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.dam).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.dam).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.dam).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.dam).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phesoi--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.dam & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    GlobalFunctionMaCun.Muctieucaubehoangda = 1;
                                }
                            }
                            else if (GlobalFunctionMaCun.dam == GlobalFunctionMaCun.plr21)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "ten");
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.dam).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.dam).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.dam).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.dam).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phesoi--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.dam & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    GlobalFunctionMaCun.Muctieucaubehoangda = 1;
                                }
                            }
                            else if (GlobalFunctionMaCun.dam == GlobalFunctionMaCun.plr17 & GlobalFunctionMaCun.caubesoi == 1)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "ten");
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.dam).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.dam).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.dam).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.dam).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phesoi--;
                            }
                            else if (GlobalFunctionMaCun.dam == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.moi != 0)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "ten");
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.moi, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.moi).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.moi).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.moi).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.moi).RemoveRoleAsync(song);
                                if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr4 || GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr6 || GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr11 || GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr16 || GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr21) GlobalFunctionMaCun.phesoi--;
                                else GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.dam & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    GlobalFunctionMaCun.Muctieucaubehoangda = 1;
                                }
                                if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr7) GlobalFunctionMaCun.Thosanchet = 1;
                                if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr18) GlobalFunctionMaCun.Giaoxuchet = 1;
                            }
                            else
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "ten");
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.dam, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.dam).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Sát Nhân đã đâm chết " + Context.Guild.GetUser(GlobalFunctionMaCun.dam).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.dam).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.dam).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.dam & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    GlobalFunctionMaCun.Muctieucaubehoangda = 1;
                                }
                                if (GlobalFunctionMaCun.dam == GlobalFunctionMaCun.plr7) GlobalFunctionMaCun.Thosanchet = 1;
                                if (GlobalFunctionMaCun.dam == GlobalFunctionMaCun.plr18) GlobalFunctionMaCun.Giaoxuchet = 1;
                            }
                        }
                    }

                    if (GlobalFunctionMaCun.can != 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User))
                    {
                        if (GlobalFunctionMaCun.can == GlobalFunctionMaCun.baoveplr & GlobalFunctionMaCun.khien >= 1)
                        {
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                            GlobalFunctionMaCun.khien--;
                        }
                        if (GlobalFunctionMaCun.can == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.khien >= 1)
                        {
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                            GlobalFunctionMaCun.khien--;
                        }
                        else if (GlobalFunctionMaCun.can == GlobalFunctionMaCun.baoveplr & GlobalFunctionMaCun.can == GlobalFunctionMaCun.moi & GlobalFunctionMaCun.khien >= 1)
                        {
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                            GlobalFunctionMaCun.khien--;
                        }
                        else if (GlobalFunctionMaCun.can == GlobalFunctionMaCun.cuu & GlobalFunctionMaCun.thuoccuu >= 1)
                        {
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                            GlobalFunctionMaCun.thuoccuu--;
                        }
                        else if (GlobalFunctionMaCun.can == GlobalFunctionMaCun.cuu & GlobalFunctionMaCun.cuu == GlobalFunctionMaCun.moi & GlobalFunctionMaCun.thuoccuu >= 1)
                        {
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574598677135390).SendMessageAsync("Đêm qua bạn người mà bạn cứu đã bị tấn công, bình thuốc cứu của bạn phát huy tác dụng.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                            GlobalFunctionMaCun.thuoccuu--;
                        }
                        else if (GlobalFunctionMaCun.can == GlobalFunctionMaCun.plr5 & GlobalFunctionMaCun.gialang >= 1)
                        {
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574497514586137).SendMessageAsync("Đêm qua bạn đã bị tấn công, nếu đợt tiếp theo còn bị tấn công nữa thì bạn sẽ chết.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                            GlobalFunctionMaCun.gialang--;
                        }
                        else if (GlobalFunctionMaCun.can == GlobalFunctionMaCun.moi)
                        {
                            if (GlobalFunctionMaCun.can == GlobalFunctionMaCun.plr13)
                            {
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                            }
                            else
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "ten");
                                    await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr14, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr14).Nickname + ".");
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).AddRoleAsync(chet);
                                await Context.Guild.GetUser(GlobalFunctionMaCun.plr14).RemoveRoleAsync(song);
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr14 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    GlobalFunctionMaCun.Muctieucaubehoangda = 1;
                                }
                            }
                        }
                        else if (GlobalFunctionMaCun.can == GlobalFunctionMaCun.baoveplr & GlobalFunctionMaCun.khien == 0)
                        {
                            if (GlobalFunctionMaCun.deadroles == 1)
                            {
                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "ten");
                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.plr1, "idrole");
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                            }
                            else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.plr1).Nickname + ".");
                            await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).AddRoleAsync(chet);
                            await Context.Guild.GetUser(GlobalFunctionMaCun.plr1).RemoveRoleAsync(song);
                            GlobalFunctionMaCun.phedan--;
                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.plr1 & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                            {
                                GlobalFunctionMaCun.Muctieucaubehoangda = 1;
                            }
                        }
                        else if (GlobalFunctionMaCun.can == GlobalFunctionMaCun.plr13)
                        {
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574363930198021).SendMessageAsync("Đêm qua bạn đã bị tấn công hoặc người mà bạn bảo vệ đã bị tấn công nên bạn bị mất đi 1 bảo vệ, nếu còn bị tấn công thì bạn sẽ chết.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("Mục Tiêu của bạn không thể bị tiêu diệt.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đêm qua không ai bị giết bởi Ma Sói.");
                        }
                        else
                        {
                            if (GlobalFunctionMaCun.deadroles == 1)
                            {
                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "ten");
                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.can, "idrole");
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.can).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                            }
                            else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Ma Sói đã cắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.can).Nickname + ".");
                            await Context.Guild.GetUser(GlobalFunctionMaCun.can).AddRoleAsync(chet);
                            await Context.Guild.GetUser(GlobalFunctionMaCun.can).RemoveRoleAsync(song);
                            GlobalFunctionMaCun.phedan--;
                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.can & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                            {
                                GlobalFunctionMaCun.Muctieucaubehoangda = 1;
                            }
                            if (GlobalFunctionMaCun.can == GlobalFunctionMaCun.plr7) GlobalFunctionMaCun.Thosanchet = 1;
                            if (GlobalFunctionMaCun.can == GlobalFunctionMaCun.plr18) GlobalFunctionMaCun.Giaoxuchet = 1;
                        }
                    }

                    if (GlobalFunctionMaCun.Thosanchet == 1)
                    {
                        GlobalFunctionMaCun.Thosanchet = 0;
                        if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.keo)))
                        {
                            if (GlobalFunctionMaCun.deadroles == 1)
                            {
                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "ten");
                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "idrole");
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết chung.");
                            }
                            else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(chet);
                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(song);
                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.keo & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                            {
                                GlobalFunctionMaCun.Muctieucaubehoangda = 1;
                            }
                            if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr18) GlobalFunctionMaCun.Giaoxuchet = 1;
                            if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr4 || GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr6 || GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr11 || GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr16 || GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr21) GlobalFunctionMaCun.phesoi--;
                            else GlobalFunctionMaCun.phedan--;
                        }
                    }

                    if (GlobalFunctionMaCun.Giaoxuchet == 0)
                    {
                        if (GlobalFunctionMaCun.giaoxu != 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu)))
                        {
                            OverwritePermissions chophepgiaoxu = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Deny, readMessageHistory: PermValue.Allow);
                            if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.plr18)) & GlobalFunctionMaCun.giaoxu != 0)
                            {
                                var giaoxu = GlobalFunctionMaCun.giaoxu;
                                var plr4 = GlobalFunctionMaCun.plr4;
                                var plr6 = GlobalFunctionMaCun.plr6;
                                var plr11 = GlobalFunctionMaCun.plr11;
                                var plr13 = GlobalFunctionMaCun.plr13;
                                var plr16 = GlobalFunctionMaCun.plr16;
                                var plr17 = GlobalFunctionMaCun.plr17;
                                var plr21 = GlobalFunctionMaCun.plr21;

                                if (giaoxu == plr4 || giaoxu == plr6 || giaoxu == plr11 || giaoxu == plr13 || giaoxu == plr16 || giaoxu == plr17 & GlobalFunctionMaCun.caubesoi == 1 & giaoxu == plr21)
                                {
                                    GlobalFunctionMaCun.giaoxu = 0;
                                }
                                else if (!Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu)))
                                {
                                    GlobalFunctionMaCun.giaoxu = 0;
                                }
                                else
                                {
                                    var user3 = Context.Guild.GetUser(giaoxu);
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(590016837262245895).AddPermissionOverwriteAsync(user3, chophepgiaoxu.Modify());
                                    await GlobalFunctionMaCun.rolesid(giaoxu, "channelid");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(590016837262245895).SendMessageAsync($"{Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu).Mention} đã tham gia giáo xứ!");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(GlobalFunctionMaCun.channelroles).SendMessageAsync("Bạn dã được Đức Cha Giáo Xứ mời vào Thánh Đường, từ giờ bạn sẽ thắng cùng phe Giáo Xứ.\n<#590016837262245895> Để xem các thành viên của giáo xứ.");

                                    if (GlobalFunctionMaCun.giaoxuplr == null) { GlobalFunctionMaCun.giaoxuplr = $"{user3.Nickname.ToString()}"; }
                                    else GlobalFunctionMaCun.giaoxuplr = $"{GlobalFunctionMaCun.giaoxuplr} {user3.Nickname.ToString()}";

                                }
                            }
                        }
                    }

                    if (GlobalFunctionMaCun.Giaoxuchet == 1)
                    {
                        GlobalFunctionMaCun.Giaoxuchet = 0;
                        string thanhviengiaoxus = GlobalFunctionMaCun.giaoxuplr;
                        string[] thanhviengiaoxu = thanhviengiaoxus.Split(" ");
                        foreach (var x in thanhviengiaoxu)
                        {
                            var getuser = Context.Guild.Users.FirstOrDefault((y => y.Nickname == x + ""));
                            if (getuser != null)
                            {
                                if (Context.Guild.Roles.FirstOrDefault(y => y.Name == "Sống").Members.Contains(Context.User))
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(getuser.Id, "ten");
                                        await GlobalFunctionMaCun.rolesid(getuser.Id, "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đức Cha Giáo Xứ đã chết, Thành Viên Giáo Xứ - " + Context.Guild.GetUser(getuser.Id).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") đã tự sát và trở thành thiên thần biết bay...");
                                    }
                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đức Cha Giáo Xứ đã chết, Thành Viên Giáo Xứ - " + Context.Guild.GetUser(getuser.Id).Nickname + " đã tự sát và trở thành thiên thần biết bay...");
                                    await Context.Guild.GetUser(getuser.Id).AddRoleAsync(chet);
                                    await Context.Guild.GetUser(getuser.Id).RemoveRoleAsync(song);
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.caubehoangda == getuser.Id & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(y => y.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(y => y.Id == GlobalFunctionMaCun.plr17)))
                                    {
                                        GlobalFunctionMaCun.Muctieucaubehoangda = 1;
                                    }
                                    if (getuser.Id == GlobalFunctionMaCun.plr7) GlobalFunctionMaCun.Thosanchet = 1;
                                }
                            }
                        }
                    }

                    if (GlobalFunctionMaCun.Thosanchet == 1)
                    {
                        GlobalFunctionMaCun.Thosanchet = 0;
                        if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.keo)))
                        {
                            if (GlobalFunctionMaCun.deadroles == 1)
                            {
                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "ten");
                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "idrole");
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết chung.");
                            }
                            else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(chet);
                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(song);
                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.keo & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                            {
                                GlobalFunctionMaCun.Muctieucaubehoangda = 1;
                            }
                            if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr18) GlobalFunctionMaCun.Giaoxuchet = 1;
                            if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr4 || GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr6 || GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr11 || GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr16 || GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr21) GlobalFunctionMaCun.phesoi--;
                            else GlobalFunctionMaCun.phedan--;
                        }
                    }

                    if (GlobalFunctionMaCun.Giaoxuchet == 1)
                    {
                        if (GlobalFunctionMaCun.giaoxuplr != null)
                        {
                            GlobalFunctionMaCun.Giaoxuchet = 0;
                            string thanhviengiaoxus = GlobalFunctionMaCun.giaoxuplr;
                            string[] thanhviengiaoxu = thanhviengiaoxus.Split(" ");
                            foreach (var x in thanhviengiaoxu)
                            {
                                var getuser = Context.Guild.Users.FirstOrDefault(y => y.Nickname == x + "");
                                if (getuser != null)
                                {
                                    if (Context.Guild.Roles.FirstOrDefault(y => y.Name == "Sống").Members.Contains(Context.User))
                                    {
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            await GlobalFunctionMaCun.rolesid(getuser.Id, "ten");
                                            await GlobalFunctionMaCun.rolesid(getuser.Id, "idrole");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đức Cha Giáo Xứ đã chết, Thành Viên Giáo Xứ - " + Context.Guild.GetUser(getuser.Id).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") đã tự sát và trở thành thiên thần biết bay...");
                                        }
                                        else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Đức Cha Giáo Xứ đã chết, Thành Viên Giáo Xứ - " + Context.Guild.GetUser(getuser.Id).Nickname + " đã tự sát và trở thành thiên thần biết bay...");
                                        await Context.Guild.GetUser(getuser.Id).AddRoleAsync(chet);
                                        await Context.Guild.GetUser(getuser.Id).RemoveRoleAsync(song);
                                        GlobalFunctionMaCun.phedan--;
                                        if (GlobalFunctionMaCun.caubehoangda == getuser.Id & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(y => y.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(y => y.Id == GlobalFunctionMaCun.plr17)))
                                        {
                                            GlobalFunctionMaCun.Muctieucaubehoangda = 1;
                                        }
                                        if (getuser.Id == GlobalFunctionMaCun.plr7) GlobalFunctionMaCun.Thosanchet = 1;
                                    }
                                }
                            }
                        }
                    }

                    if (GlobalFunctionMaCun.Thosanchet == 1)
                    {
                        GlobalFunctionMaCun.Thosanchet = 0;
                        if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.keo)))
                        {
                            if (GlobalFunctionMaCun.deadroles == 1)
                            {
                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "ten");
                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "idrole");
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết chung.");
                            }
                            else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(chet);
                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(song);
                            if (GlobalFunctionMaCun.caubehoangda == GlobalFunctionMaCun.keo & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                            {
                                GlobalFunctionMaCun.Muctieucaubehoangda = 1;
                            }
                            if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr18) GlobalFunctionMaCun.Giaoxuchet = 1;
                            if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr4 || GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr6 || GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr11 || GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr16 || GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr21) GlobalFunctionMaCun.phesoi--;
                            else GlobalFunctionMaCun.phedan--;
                        }
                    }

                    if (GlobalFunctionMaCun.Muctieucaubehoangda == 1)
                    {
                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17), chophepsoi.Modify());
                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync($"{Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17).Mention} đã gia nhập ma sói.");
                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                        GlobalFunctionMaCun.phesoi++;
                        GlobalFunctionMaCun.phedan--;
                        GlobalFunctionMaCun.caubesoi = 1;
                        GlobalFunctionMaCun.caubehoangda = 0;
                    }


                    if (GlobalFunctionMaCun.hoisinh != 0)
                    {
                        var plrspawn = Context.Guild.GetUser(GlobalFunctionMaCun.hoisinh);
                        if (GlobalFunctionMaCun.deadroles == 1)
                        {
                            await GlobalFunctionMaCun.rolesid(plrspawn.Id, "ten");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thầy Đồng đã hồi sinh " + Context.Guild.GetUser(plrspawn.Id).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                        }
                        else Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thầy Đồng đã hồi sinh " + plrspawn.Nickname + ".");
                        await plrspawn.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                        await plrspawn.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                        if (GlobalFunctionMaCun.hoisinh == GlobalFunctionMaCun.plr1)
                        {
                            GlobalFunctionMaCun.khien = 1;
                            GlobalFunctionMaCun.phedan++;
                        }
                        else if (GlobalFunctionMaCun.hoisinh == GlobalFunctionMaCun.plr4 || GlobalFunctionMaCun.hoisinh == GlobalFunctionMaCun.plr6 || GlobalFunctionMaCun.hoisinh == GlobalFunctionMaCun.plr11 || GlobalFunctionMaCun.hoisinh == GlobalFunctionMaCun.plr16 || GlobalFunctionMaCun.hoisinh == GlobalFunctionMaCun.plr21 || GlobalFunctionMaCun.hoisinh == GlobalFunctionMaCun.plr17 & GlobalFunctionMaCun.caubesoi == 1)
                        {
                            GlobalFunctionMaCun.phesoi++;
                        }
                        else if (GlobalFunctionMaCun.hoisinh == GlobalFunctionMaCun.plr13)
                        {
                            GlobalFunctionMaCun.phethu3++;
                        }
                        else
                        {
                            GlobalFunctionMaCun.phedan++;
                        }
                        GlobalFunctionMaCun.hoisinh = 0;
                        GlobalFunctionMaCun.luothoisinh--;
                    }

                    if (GlobalFunctionMaCun.dongbang != 0)
                    {
                        GlobalFunctionMaCun.lastdongbang = GlobalFunctionMaCun.dongbang;
                    }
                    if (GlobalFunctionMaCun.moi != 0)
                    {
                        GlobalFunctionMaCun.lastmoi = GlobalFunctionMaCun.moi;
                    }
                    if (GlobalFunctionMaCun.dongbang == 0)
                    {
                        GlobalFunctionMaCun.lastdongbang = GlobalFunctionMaCun.dongbang;
                    }
                    if (GlobalFunctionMaCun.moi == 0)
                    {
                        GlobalFunctionMaCun.lastmoi = GlobalFunctionMaCun.moi;
                    }

                    GlobalFunctionMaCun.can = 0;
                    GlobalFunctionMaCun.dam = 0;
                    GlobalFunctionMaCun.cuu = 0;
                    GlobalFunctionMaCun.moi = 0;
                    GlobalFunctionMaCun.giaoxu = 0;
                    GlobalFunctionMaCun.baoveplr = 0;
                    GlobalFunctionMaCun.dongbang = 0;
                    GlobalFunctionMaCun.phuphep = 0;
                    GlobalFunctionMaCun.canchet = 0;
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
                GlobalFunctionMaCun.channel14 = 0;
                GlobalFunctionMaCun.channel15 = 0;
                GlobalFunctionMaCun.channel16 = 0;
                GlobalFunctionMaCun.channel17 = 0;
                GlobalFunctionMaCun.channel18 = 0;
                GlobalFunctionMaCun.channel19 = 0;
                GlobalFunctionMaCun.channel20 = 0;
                GlobalFunctionMaCun.channel21 = 0;
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
                GlobalFunctionMaCun.plr14 = 0;
                GlobalFunctionMaCun.plr15 = 0;
                GlobalFunctionMaCun.plr16 = 0;
                GlobalFunctionMaCun.plr17 = 0;
                GlobalFunctionMaCun.plr18 = 0;
                GlobalFunctionMaCun.plr19 = 0;
                GlobalFunctionMaCun.plr20 = 0;
                GlobalFunctionMaCun.plr21 = 0;
                GlobalFunctionMaCun.plr1p = 0;
                GlobalFunctionMaCun.plr2p = 0;
                GlobalFunctionMaCun.plr3p = 0;
                GlobalFunctionMaCun.plr4p = 0;
                GlobalFunctionMaCun.plr5p = 0;
                GlobalFunctionMaCun.plr6p = 0;
                GlobalFunctionMaCun.plr7p = 0;
                GlobalFunctionMaCun.plr8p = 0;
                GlobalFunctionMaCun.plr9p = 0;
                GlobalFunctionMaCun.plr10p = 0;
                GlobalFunctionMaCun.plr11p = 0;
                GlobalFunctionMaCun.plr12p = 0;
                GlobalFunctionMaCun.plr13p = 0;
                GlobalFunctionMaCun.plr14p = 0;
                GlobalFunctionMaCun.plr15p = 0;
                GlobalFunctionMaCun.plr16p = 0;
                GlobalFunctionMaCun.plr17p = 0;
                GlobalFunctionMaCun.plr18p = 0;
                GlobalFunctionMaCun.plr19p = 0;
                GlobalFunctionMaCun.plr20p = 0;
                GlobalFunctionMaCun.plr21p = 0;
                GlobalFunctionMaCun.giaoxu = 0;
                GlobalFunctionMaCun.giaoxuplr = null;
                GlobalFunctionMaCun.treo = 0;
                GlobalFunctionMaCun.thayboi = 0;
                GlobalFunctionMaCun.tientri = 0;
                GlobalFunctionMaCun.soitri = 0;
                GlobalFunctionMaCun.gialang = 0;
                GlobalFunctionMaCun.lastdongbang = 0;
                GlobalFunctionMaCun.lastmoi = 0;
                GlobalFunctionMaCun.dongbang = 0;
                GlobalFunctionMaCun.moi = 0;
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
                GlobalFunctionMaCun.luothoisinh = 0;
                GlobalFunctionMaCun.hoisinh = 0;
                GlobalFunctionMaCun.mogame = null;
                GlobalFunctionMaCun.game = null;
                GlobalFunctionMaCun.chucnangphuphep = 0;
                GlobalFunctionMaCun.chucnangdongbang = 0;
                GlobalFunctionMaCun.chucnangsoi = 0;
                GlobalFunctionMaCun.deadroles = 0;
                GlobalFunctionMaCun.showroles = 0;
                GlobalFunctionMaCun.nameroles = null;
                GlobalFunctionMaCun.channelroles = 0;
                GlobalFunctionMaCun.idroles = 0;
                GlobalFunctionMaCun.roleavailble = 0;
                GlobalFunctionMaCun.caubesoi = 0;
                GlobalFunctionMaCun.caubehoangda = 0;
                GlobalFunctionMaCun.checkgiaoxu = 0;
                GlobalFunctionMaCun.pha = 0;
                GlobalFunctionMaCun.nuoi = 0;
                GlobalFunctionMaCun.lamme = 0;
                GlobalFunctionMaCun.canchet = 0;
                GlobalFunctionMaCun.luotcanchet = 0;

                await Task.Delay(10000);
                int playernumber = 16;
                while (playernumber > 0)
                {
                    var g = Context.Guild.Users.FirstOrDefault(x => x.Nickname == playernumber + "");
                    if (g != null)
                    {
                        await g.KickAsync();
                    }
                    playernumber--;
                }

                IEnumerable<IMessage> nonPinnedMessages = await Context.Guild.GetTextChannel(580563096544739331).GetMessagesAsync(1000).FlattenAsync();
                await Context.Guild.GetTextChannel(580563096544739331).DeleteMessagesAsync(nonPinnedMessages.Where(x => x.IsPinned == false));

                IEnumerable<IMessage> nonPinnedMessages2 = await Context.Guild.GetTextChannel(580564164687298609).GetMessagesAsync(1000).FlattenAsync();
                await Context.Guild.GetTextChannel(580564164687298609).DeleteMessagesAsync(nonPinnedMessages2.Where(x => x.IsPinned == false));

                IEnumerable<IMessage> nonPinnedMessages3 = await Context.Guild.GetTextChannel(580564753982816256).GetMessagesAsync(1000).FlattenAsync();
                await Context.Guild.GetTextChannel(580564753982816256).DeleteMessagesAsync(nonPinnedMessages3.Where(x => x.IsPinned == false));

                IEnumerable<IMessage> nonPinnedMessages4 = await Context.Guild.GetTextChannel(580565718987309101).GetMessagesAsync(1000).FlattenAsync();
                await Context.Guild.GetTextChannel(580565718987309101).DeleteMessagesAsync(nonPinnedMessages4.Where(x => x.IsPinned == false));

                IEnumerable<IMessage> nonPinnedMessages5 = await Context.Guild.GetTextChannel(580699915156455424).GetMessagesAsync(1000).FlattenAsync();
                await Context.Guild.GetTextChannel(580699915156455424).DeleteMessagesAsync(nonPinnedMessages5.Where(x => x.IsPinned == false));

                IEnumerable<IMessage> nonPinnedMessages6 = await Context.Guild.GetTextChannel(580557883931099138).GetMessagesAsync(1000).FlattenAsync();
                await Context.Guild.GetTextChannel(580557883931099138).DeleteMessagesAsync(nonPinnedMessages6.Where(x => x.IsPinned == false));

                IEnumerable<IMessage> nonPinnedMessages7 = await Context.Guild.GetTextChannel(590016837262245895).GetMessagesAsync(1000).FlattenAsync();
                await Context.Guild.GetTextChannel(590016837262245895).DeleteMessagesAsync(nonPinnedMessages7.Where(x => x.IsPinned == false));

                IEnumerable<IMessage> nonPinnedMessages8 = await Context.Guild.GetTextChannel(582774971428896768).GetMessagesAsync(1000).FlattenAsync();
                await Context.Guild.GetTextChannel(582774971428896768).DeleteMessagesAsync(nonPinnedMessages8.Where(x => x.IsPinned == false));



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

                IEnumerable<IMessage> gaidiem = await Context.Guild.GetTextChannel(583828253681254400).GetMessagesAsync(200).FlattenAsync();
                await Context.Guild.GetTextChannel(583828253681254400).DeleteMessagesAsync(gaidiem.Where(x => x.IsPinned == false));

                IEnumerable<IMessage> thaydong = await Context.Guild.GetTextChannel(583828359394492427).GetMessagesAsync(200).FlattenAsync();
                await Context.Guild.GetTextChannel(583828359394492427).DeleteMessagesAsync(thaydong.Where(x => x.IsPinned == false));

                IEnumerable<IMessage> soitri = await Context.Guild.GetTextChannel(583828385659355147).GetMessagesAsync(200).FlattenAsync();
                await Context.Guild.GetTextChannel(583828385659355147).DeleteMessagesAsync(soitri.Where(x => x.IsPinned == false));

                IEnumerable<IMessage> caubehoangda = await Context.Guild.GetTextChannel(589462982275235860).GetMessagesAsync(200).FlattenAsync();
                await Context.Guild.GetTextChannel(589462982275235860).DeleteMessagesAsync(caubehoangda.Where(x => x.IsPinned == false));

                IEnumerable<IMessage> giaoxu = await Context.Guild.GetTextChannel(589463015212974080).GetMessagesAsync(200).FlattenAsync();
                await Context.Guild.GetTextChannel(589463015212974080).DeleteMessagesAsync(giaoxu.Where(x => x.IsPinned == false));

                IEnumerable<IMessage> thamtu = await Context.Guild.GetTextChannel(591478522111983616).GetMessagesAsync(200).FlattenAsync();
                await Context.Guild.GetTextChannel(591478522111983616).DeleteMessagesAsync(thamtu.Where(x => x.IsPinned == false));

                IEnumerable<IMessage> ketailanh = await Context.Guild.GetTextChannel(591478642966528020).GetMessagesAsync(200).FlattenAsync();
                await Context.Guild.GetTextChannel(591478642966528020).DeleteMessagesAsync(ketailanh.Where(x => x.IsPinned == false));

                IEnumerable<IMessage> soidaohoa = await Context.Guild.GetTextChannel(591478711774085143).GetMessagesAsync(200).FlattenAsync();
                await Context.Guild.GetTextChannel(591478711774085143).DeleteMessagesAsync(soidaohoa.Where(x => x.IsPinned == false));

                embed.AddField($"Hệ Thống!", "Đã dọn dẹp xong - sẵn sàng cho trận đấu mới.");
                embed.WithColor(new Discord.Color(0, 255, 0));
                await Context.Guild.GetTextChannel(580555887324954635).SendMessageAsync("", false, embed.Build());
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
                OverwritePermissions chophep2 = new OverwritePermissions(viewChannel: PermValue.Allow, connect: PermValue.Allow, speak: PermValue.Allow);
                await Context.Client.GetGuild(580555457983152149).GetVoiceChannel(580566264171331597).AddPermissionOverwriteAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"), chophep2.Modify());
                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).AddPermissionOverwriteAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"), chophep.Modify());
                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Game kết thúc, " + phe + " thắng.");
            }
        }


        [Command("caidatgame")]
        [Alias("gamesettings")]
        public async Task SettingsForWerewolfGame(int Number1 = 0, int Number2 = 0)
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
                else if (Number1 == 0)
                {
                    string a = null;
                    string b = null;
                    if (GlobalFunctionMaCun.deadroles == 1)
                    {
                        a = "Bật";
                    }
                    else a = "Tắt";

                    if (GlobalFunctionMaCun.showroles == 1)
                    {
                        b = "Bật";
                    }
                    else b = "Tắt";

                    embed.WithAuthor("Cài Đặt Trận Đấu - Match Settings");
                    embed.AddField($"Lệnh", "-caidatgame (Number1) (Number2)\n1 - Hiện Vai Trò khi Chết\n2 - Danh Sách Vai Trò khi Game mới bắt đầu.");
                    embed.AddField($"Cài Đặt Hiện Tại", $"Hiện Vai Trò Khi Chết\n-> {a}.\nDanh Sách Vai Trò khi Game Bắt Đầu\n-> {b}.");
                    embed.WithColor(new Discord.Color(255, 50, 255));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (Number2 == 0)
                {
                    embed.AddField($"Lỗi!", "Number2 bị thiếu!\n1 - Bật\n2 - Tắt.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    if (Number2 == 1)
                    {
                        if (Number1 == 1)
                        {
                            GlobalFunctionMaCun.deadroles = 1;
                        }
                        if (Number1 == 2)
                        {
                            GlobalFunctionMaCun.showroles = 1;
                        }
                    }
                    if (Number2 == 2)
                    {
                        if (Number1 == 1)
                        {
                            GlobalFunctionMaCun.deadroles = 0;
                        }
                        if (Number1 == 2)
                        {
                            GlobalFunctionMaCun.showroles = 0;
                        }
                    }
                    string a = null;
                    string b = null;
                    if (GlobalFunctionMaCun.deadroles == 1)
                    {
                        a = "Bật";
                    }
                    else a = "Tắt";

                    if (GlobalFunctionMaCun.showroles == 1)
                    {
                        b = "Bật";
                    }
                    else b = "Tắt";

                    embed.AddField($"Cài Đặt Trận Đấu - Match Settings", $"Hiện Vai Trò Khi Chết\n-> {a}.\nDanh Sách Vai Trò khi Game Bắt Đầu\n-> {b}.");
                    embed.WithColor(new Discord.Color(0, 255, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
            else return;
        }

    }
}
