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
        [Command("thamgia")]
        [RequireBotPermission(Discord.GuildPermission.ManageRoles)]
        public async Task thamgia()
        {
            var embed = new EmbedBuilder();
            if (Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 580555887324954635)
                {
                if (GlobalFunctionMaCun.gamestatus == 0 & !Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User) & !Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết").Members.Contains(Context.User))
                {
                    int checkplayer = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Count();
                    checkplayer++;
                    if (checkplayer <= 13)
                    {
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
                else
                {
                    embed.AddField($"Lỗi!", "Game đã bắt đầu nên bạn không thể Tham Gia (Dùng -khangia nếu bạn muốn xem).");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
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
                        if ( GlobalFunctionMaCun.channel1 == 0 & num == 1)//580574363930198021
                        {
                            GlobalFunctionMaCun.plr1 = user.Id;
                            GlobalFunctionMaCun.channel1 = 1;
                            GlobalFunctionMaCun.phedan++;
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
                            GlobalFunctionMaCun.phedan++;
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
                            GlobalFunctionMaCun.phedan++;
                            await Context.Guild.GetTextChannel(580699915156455424).SendMessageAsync("" + user.Nickname + " - Dân Làng.");
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580574427712847872).AddPermissionOverwriteAsync(user, chophep.Modify());

                            embed.AddField($"Hệ Thống!", "Đã đưa vai trò Dân Làng cho " + user + "");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.channel4 == 0 & num == 4)//580574451834290176 - 580564753982816256
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
                        else if (GlobalFunctionMaCun.channel5 == 0 & num == 5)// 580574497514586137
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
                        else if (GlobalFunctionMaCun.channel6 == 0 & num == 6)//580574522361774081 - 580564753982816256
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
                        else if (GlobalFunctionMaCun.channel7 == 0 & num == 7)//580574545606475782
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
                        else if (GlobalFunctionMaCun.channel8 == 0 & num == 8)//580574572483706891
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
                        else if (GlobalFunctionMaCun.channel9 == 0 & num == 9)//580574598677135390
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
                        else if (GlobalFunctionMaCun.channel10 == 0 & num == 10)//580574616645402656
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
                        else if (GlobalFunctionMaCun.channel11 == 0 & num == 11)//580574634811064342
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
                        else if (GlobalFunctionMaCun.channel12 == 0 & num == 12)//580574414660435982
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
                        else if (GlobalFunctionMaCun.channel13 == 0 & num == 13)//580574812662136836
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
                    if (GlobalFunctionMaCun.gamestatus == 3)
                    {
                        if (user.ToString() == "0")
                        {
                            Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Do Quản Trò có sự nhầm lẫn nên đã bỏ treo.");
                            GlobalFunctionMaCun.treo = 0;
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
// ======================================== KHU VỰC LỆNH CHO TỪNG VAI TRÒ ===========================================================
        [Command("baove")]
        public async Task bgprotect(IGuildUser user = null)
        {
            if (Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 580574363930198021)
            {
                var embed = new EmbedBuilder();
                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User))
                {
                    if (user == null)
                    {
                        embed.AddField($"Hệ Thống!", "-baove (Số Người Chơi Muốn Bảo Vệ)");
                        embed.WithColor(new Discord.Color(255, 50, 255));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))
                    {
                        if (GlobalFunctionMaCun.gamestatus == 1)
                        {
                            if (Context.User.Id != GlobalFunctionMaCun.dongbang)
                            {
                                if (user.Id == Context.User.Id)
                                {
                                    await Context.Message.AddReactionAsync(Emote.Parse("<:dachon:580977712689053696>"));
                                }
                                else
                                {
                                    GlobalFunctionMaCun.baoveplr = user.Id;
                                    await Context.Message.AddReactionAsync(Emote.Parse("<:dachon:580977712689053696>"));
                                }
                            }
                            else
                            {
                                embed.AddField($"Lỗi!", "Bạn không thể bảo vệ khi bị Đóng Băng.");
                                embed.WithColor(new Discord.Color(255, 0, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                        }
                        else
                        {
                            embed.AddField($"Lỗi!", "Lệnh này chỉ có thể sử dụng vào ban đêm.");
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
                else
                {
                    embed.AddField($"Lỗi!", "Bạn không thể sử dụng khi đã chết.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
            else return;
        }
        [Command("soi")]
        public async Task seerandauracheck(IGuildUser user = null)
        {
            if (Context.Guild.Id == 580555457983152149)
            {
                var embed = new EmbedBuilder();
                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User))
                {
                    if (user == null)
                    {
                        embed.AddField($"Hệ Thống!", "-soi (Số Người Chơi Muốn Kiểm Tra)");
                        embed.WithColor(new Discord.Color(255, 50, 255));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))
                    {
                        if (GlobalFunctionMaCun.gamestatus == 1)
                        {
                            if (Context.User.Id == GlobalFunctionMaCun.dongbang)
                            {
                                embed.AddField($"Lỗi!", "Bạn không thể Soi khi bị Đóng Băng.");
                                embed.WithColor(new Discord.Color(255, 0, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                            else if (user.Id == Context.User.Id)
                            {
                                embed.AddField($"Lỗi!", "Bạn không thể kiểm tra bản thân.");
                                embed.WithColor(new Discord.Color(255, 0, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                            else
                            {
                                if (Context.Channel.Id == 580574739391578112 & Context.User.Id != GlobalFunctionMaCun.dongbang)
                                {
                                    if (GlobalFunctionMaCun.thayboi == 1)
                                    {
                                        if (user.Id == GlobalFunctionMaCun.plr1 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Người chơi số "+user.Nickname+" là Thiện.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.thayboi--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr2 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Người chơi số " + user.Nickname + " là Thiện.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.thayboi--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr3 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Người chơi số " + user.Nickname + " là Thiện.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.thayboi--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr4 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Người chơi số " + user.Nickname + " là Ác.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.thayboi--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr5 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Người chơi số " + user.Nickname + " là Thiện.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.thayboi--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr6 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Người chơi số " + user.Nickname + " là Ác.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.thayboi--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr7 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Người chơi số " + user.Nickname + " là Không Rõ.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.thayboi--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr8 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Người chơi số " + user.Nickname + " là Không Rõ.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.thayboi--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr9 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Người chơi số " + user.Nickname + " là Thiện.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.thayboi--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr10 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Người chơi số " + user.Nickname + " là Không Rõ.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.thayboi--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr11 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Người chơi số " + user.Nickname + " là Không Rõ.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.thayboi--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr12 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Người chơi số " + user.Nickname + " là Thiện.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.thayboi--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr13 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Người chơi số " + user.Nickname + " là Không Rõ.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.thayboi--;
                                        }
                                        else
                                        {
                                            embed.AddField($"Hệ Thống!", "Người chơi số " + user.Nickname + " là Ác.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.thayboi--;
                                        }
                                    }
                                    else
                                    {
                                        embed.AddField($"Lỗi!", "Bạn không thể soi 1 đêm 2 người.");
                                        embed.WithColor(new Discord.Color(255, 0, 0));
                                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                                    }
                                }
                                else if (Context.Channel.Id == 580574414660435982 & Context.User.Id != GlobalFunctionMaCun.dongbang)
                                {
                                    if (GlobalFunctionMaCun.tientri == 1)
                                    {
                                        if (user.Id == GlobalFunctionMaCun.plr1 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Vai Trò của người chơi số " + user.Nickname + " là Bảo Vệ.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.tientri--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr2 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Vai Trò của người chơi số " + user.Nickname + " là Tiên Tri.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.tientri--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr3 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Vai Trò của người chơi số " + user.Nickname + " là Dân Làng.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.tientri--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr4 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Vai Trò của người chơi số " + user.Nickname + " là Sói Thường.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.tientri--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr5 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Vai Trò của người chơi số " + user.Nickname + " là Già Làng.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.tientri--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr6 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Vai Trò của người chơi số " + user.Nickname + " là Sói Phù Thủy.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.tientri--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr7 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Vai Trò của người chơi số " + user.Nickname + " là Thợ Săn.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.tientri--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr8 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Vai Trò của người chơi số " + user.Nickname + " là Thằng Ngố.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.tientri--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr9 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Vai Trò của người chơi số " + user.Nickname + " là Phù Thủy.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.tientri--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr10 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Vai Trò của người chơi số " + user.Nickname + " là Xạ Thủ.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.tientri--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr11 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Người chơi số " + user.Nickname + " là Sói Băng.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.tientri--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr12 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Người chơi số " + user.Nickname + " là Tiên Tri.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.tientri--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr13 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Người chơi số " + user.Nickname + " là Sát Nhân.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.tientri--;
                                        }
                                        else
                                        {
                                            embed.AddField($"Hệ Thống!", "Người chơi số " + user.Nickname + " là Sói Phù Thủy.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.tientri--;
                                        }
                                    }
                                    else
                                    {
                                        embed.AddField($"Lỗi!", "Bạn không thể soi 1 đêm 2 người.");
                                        embed.WithColor(new Discord.Color(255, 0, 0));
                                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                                    }
                                }
                                else
                                {
                                    embed.AddField($"Lỗi!", "Bạn không thể soi khi bị Đóng Băng.");
                                    embed.WithColor(new Discord.Color(255, 0, 0));
                                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                                }
                            }
                        }
                        else
                        {
                            embed.AddField($"Lỗi!", "Lệnh này chỉ có thể sử dụng vào ban đêm.");
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
                else
                {
                    embed.AddField($"Lỗi!", "Bạn không thể sử dụng khi đã chết.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
            else return;
        }
        [Command("can")]
        public async Task werewolfkill(IGuildUser user = null)
        {
            if (Context.Guild.Id == 580555457983152149)
            {
                var embed = new EmbedBuilder();
                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User))
                {
                    if (user == null)
                    {
                        embed.AddField($"Hệ Thống!", "-can (Số Người Chơi Muốn Cắn)");
                        embed.WithColor(new Discord.Color(255, 50, 255));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))
                    {
                        if (GlobalFunctionMaCun.gamestatus == 1)
                        {
                            if (user.Id == Context.User.Id)
                            {
                                embed.AddField($"Lỗi!", "Bạn không thể sử dụng lệnh này lên bản thân.");
                                embed.WithColor(new Discord.Color(255, 0, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                            else
                            {
                                if (Context.Channel.Id == 580574522361774081)
                                {
                                    if (user.Id == GlobalFunctionMaCun.plr4 || user.Id == GlobalFunctionMaCun.plr11)
                                    {
                                        embed.AddField($"Lỗi!", "Bạn không thể cắn người cùng phe.");
                                        embed.WithColor(new Discord.Color(255, 0, 0));
                                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                                    }
                                    else if (GlobalFunctionMaCun.phesoi <= 1)
                                    {
                                        GlobalFunctionMaCun.can = user.Id;
                                        await Context.Message.AddReactionAsync(Emote.Parse("<:dachon:580977712689053696>"));
                                        await Context.Guild.GetTextChannel(580564753982816256).SendMessageAsync("" + (Context.User as IGuildUser).Nickname + " Đã bỏ phiếu Cắn người chơi " + user.Nickname + "");
                                    }
                                    else
                                    {
                                        embed.AddField($"Lỗi!", "Lệnh này chỉ có thể sử dụng khi bạn là con sói cuối cùng.");
                                        embed.WithColor(new Discord.Color(255, 0, 0));
                                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                                    }
                                }
                                else if (Context.Channel.Id == 580574451834290176)
                                {
                                    if (user.Id == GlobalFunctionMaCun.plr6 || user.Id == GlobalFunctionMaCun.plr11)
                                    {
                                        embed.AddField($"Lỗi!", "Bạn không thể cắn người cùng phe.");
                                        embed.WithColor(new Discord.Color(255, 0, 0));
                                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                                    }
                                    else
                                    {
                                        GlobalFunctionMaCun.can = user.Id;
                                        await Context.Message.AddReactionAsync(Emote.Parse("<:dachon:580977712689053696>"));
                                        await Context.Guild.GetTextChannel(580564753982816256).SendMessageAsync("" + (Context.User as IGuildUser).Nickname + " Đã bỏ phiếu Cắn người chơi " + user.Nickname + "");
                                    }
                                }
                                else if (Context.Channel.Id == 580574634811064342)
                                {
                                    if (user.Id == GlobalFunctionMaCun.plr4 || user.Id == GlobalFunctionMaCun.plr6)
                                    {
                                        embed.AddField($"Lỗi!", "Bạn không thể cắn người cùng phe.");
                                        embed.WithColor(new Discord.Color(255, 0, 0));
                                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                                    }
                                    else
                                    {
                                        GlobalFunctionMaCun.can = user.Id;
                                        await Context.Message.AddReactionAsync(Emote.Parse("<:dachon:580977712689053696>"));
                                        await Context.Guild.GetTextChannel(580564753982816256).SendMessageAsync("" + (Context.User as IGuildUser).Nickname + " Đã bỏ phiếu Cắn người chơi " + user.Nickname + "");
                                    }
                                }
                                else return;
                            }
                        }
                        else
                        {
                            embed.AddField($"Lỗi!", "Lệnh này chỉ có thể sử dụng vào ban đêm.");
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
                else
                {
                    embed.AddField($"Lỗi!", "Bạn không thể sử dụng khi đã chết.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
            else return;
        }
        [Command("phuphep")]
        public async Task wolfsshaman(IGuildUser user = null)
        {
            if (Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 580574522361774081)
            {
                var embed = new EmbedBuilder();
                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User))
                {
                    if (user == null)
                    {
                        embed.AddField($"Hệ Thống!", "-phuphep (Số Người Chơi Muốn Phù Phép)");
                        embed.WithColor(new Discord.Color(255, 50, 255));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))
                    {
                        if (GlobalFunctionMaCun.gamestatus >= 2)
                        {
                            if (user.Id == Context.User.Id)
                                {
                                embed.AddField($"Lỗi!", "Bạn không thể sử dụng lệnh này lên bản thân.");
                                embed.WithColor(new Discord.Color(255, 0, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                                }   
                                else if (GlobalFunctionMaCun.phesoi <= 1)
                                {
                                embed.AddField($"Lỗi!", "Bạn không thể phù phép khi bạn là con sói cuối cùng.");
                                embed.WithColor(new Discord.Color(255, 0, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                                }
                                else if (user.Id == GlobalFunctionMaCun.plr4 || user.Id == GlobalFunctionMaCun.plr11)
                                {
                                embed.AddField($"Lỗi!", "Bạn không thể phù phép với người cùng Phe.");
                                embed.WithColor(new Discord.Color(255, 0, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                                } 
                                else
                                {
                                    GlobalFunctionMaCun.phuphep = user.Id;
                                    await Context.Message.AddReactionAsync(Emote.Parse("<:dachon:580977712689053696>"));
                                }
                        }
                        else
                        {
                            embed.AddField($"Lỗi!", "Lệnh này chỉ có thể sử dụng vào ban ngày.");
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
                else
                {
                    embed.AddField($"Lỗi!", "Bạn không thể sử dụng khi đã chết.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
            else return;
        }
        [Command("keo")]
        public async Task thosankeo(IGuildUser user = null)
        {
            if (Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 580574545606475782)
            {
                var embed = new EmbedBuilder();
                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User))
                {
                    if (user == null)
                    {
                        embed.AddField($"Hệ Thống!", "-keo (Số Người Chơi Muốn Kéo)");
                        embed.WithColor(new Discord.Color(255, 50, 255));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))
                    {
                        if (Context.User.Id == GlobalFunctionMaCun.dongbang)
                        {
                            embed.AddField($"Lỗi!", "Bạn không thể dùng lệnh khi bị Đóng Băng.");
                            embed.WithColor(new Discord.Color(255, 0, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (user.Id == Context.User.Id)
                            {
                            embed.AddField($"Lỗi!", "Bạn không thể sử dụng lệnh này lên bản thân.");
                            embed.WithColor(new Discord.Color(255, 0, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                            else
                            {
                                GlobalFunctionMaCun.keo = user.Id;
                                await Context.Message.AddReactionAsync(Emote.Parse("<:dachon:580977712689053696>"));
                            }
                    }
                    else
                    {
                        embed.AddField($"Lỗi!", "Người Chơi đó không tham gia hoặc đã chết.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                }
                else
                {
                    embed.AddField($"Lỗi!", "Bạn không thể sử dụng khi đã chết.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
            else return;
        }
        [Command("daudoc")]
        public async Task phuthuydaudoc(IGuildUser user = null)
        {
            if (Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 580574598677135390)
            {
                var embed = new EmbedBuilder();
                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User))
                {
                    if (user == null)
                    {
                        embed.AddField($"Hệ Thống!", "-daudoc (Số Người Chơi Muốn Đầu Độc)");
                        embed.WithColor(new Discord.Color(255, 50, 255));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))
                    {
                        if (Context.User.Id == GlobalFunctionMaCun.dongbang)
                        {
                            embed.AddField($"Lỗi!", "Bạn không thể đầu độc khi bị Đóng Băng.");
                            embed.WithColor(new Discord.Color(255, 0, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (user.Id == Context.User.Id)
                        {
                            embed.AddField($"Lỗi!", "Bạn không thể sử dụng lệnh này lên bản thân.");
                            embed.WithColor(new Discord.Color(255, 0, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.daycount == 1)
                        {
                            embed.AddField($"Lỗi!", "Bạn không thể sử dụng lệnh vào đêm đầu.");
                            embed.WithColor(new Discord.Color(255, 0, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.gamestatus != 1)
                        {
                            embed.AddField($"Lỗi!", "Lệnh này chỉ có thể sử dụng vào ban đêm.");
                            embed.WithColor(new Discord.Color(255, 0, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.thuocdoc >= 1 )
                        {
                            GlobalFunctionMaCun.thuocdoc--;
                            if (user.Id == GlobalFunctionMaCun.plr1)
                            {
                                await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + user.Nickname + ".");
                                await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                GlobalFunctionMaCun.baoveplr = 0;
                                GlobalFunctionMaCun.phedan--;
                            }
                            else if (user.Id == GlobalFunctionMaCun.plr4)
                            {
                                await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + user.Nickname + ".");
                                await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                GlobalFunctionMaCun.phesoi--;
                            }
                            else if (user.Id == GlobalFunctionMaCun.plr6)
                            {
                                await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + user.Nickname + ".");
                                await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                GlobalFunctionMaCun.phesoi--;
                            }
                            else if (user.Id == GlobalFunctionMaCun.plr7)
                            {
                                await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + user.Nickname + ".");
                                await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.keo != 0)
                                {
                                    if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr1)
                                    {
                                        await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.baoveplr = 0;
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
                                        GlobalFunctionMaCun.cuu = 0;
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
                                        GlobalFunctionMaCun.dam = 0;
                                    }
                                    else
                                    {
                                        await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phedan--;
                                    }
                                }
                                else return;
                            }
                            else if (user.Id == GlobalFunctionMaCun.plr11)
                            {
                                await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + user.Nickname + ".");
                                await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                GlobalFunctionMaCun.phesoi--;
                            }
                            else if (user.Id == GlobalFunctionMaCun.plr13)
                            {
                                await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + user.Nickname + ".");
                                await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                GlobalFunctionMaCun.phethu3--;
                                GlobalFunctionMaCun.dam = 0;
                            }
                            else
                            {
                                await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + user.Nickname + ".");
                                await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                GlobalFunctionMaCun.phedan--;
                            }
                        }
                        else
                        {
                            embed.AddField($"Lỗi!", "Bạn không còn thuốc để đầu độc.");
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
                else
                {
                    embed.AddField($"Lỗi!", "Bạn không thể sử dụng khi đã chết.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
            else return;
        }
        [Command("ban")]
        public async Task xathuban(IGuildUser user = null)
        {
            if (Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 580574616645402656)
            {
                var embed = new EmbedBuilder();
                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User))
                {
                    if (user == null)
                    {
                        embed.AddField($"Hệ Thống!", "-ban (Số Người Chơi Muốn Bắn)");
                        embed.WithColor(new Discord.Color(255, 50, 255));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))
                    {
                        if (user.Id == Context.User.Id)
                        {
                            embed.AddField($"Lỗi!", "Bạn không thể sử dụng lệnh này lên bản thân.");
                            embed.WithColor(new Discord.Color(255, 0, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (GlobalFunctionMaCun.gamestatus >= 2)
                        {
                            if (GlobalFunctionMaCun.danxathu >= 1)
                            {
                                GlobalFunctionMaCun.danxathu--;
                                if (user.Id == GlobalFunctionMaCun.plr1)
                                {
                                    await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + user.Nickname + ".");
                                    await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                    await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                    GlobalFunctionMaCun.baoveplr = 0;
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else if (user.Id == GlobalFunctionMaCun.plr4)
                                {
                                    await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + user.Nickname + ".");
                                    await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                    await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                    GlobalFunctionMaCun.phesoi--;
                                }
                                else if (user.Id == GlobalFunctionMaCun.plr6)
                                {
                                    await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + user.Nickname + ".");
                                    await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                    await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                    GlobalFunctionMaCun.phesoi--;
                                }
                                else if (user.Id == GlobalFunctionMaCun.plr7)
                                {
                                    await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + user.Nickname + ".");
                                    await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                    await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.keo != 0)
                                    {
                                        if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr1)
                                        {
                                            await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.baoveplr = 0;
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
                                            GlobalFunctionMaCun.cuu = 0;
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
                                            GlobalFunctionMaCun.dam = 0;
                                        }
                                        else
                                        {
                                            await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phedan--;
                                        }
                                    }
                                    else return;
                                }
                                else if (user.Id == GlobalFunctionMaCun.plr11)
                                {
                                    await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + user.Nickname + ".");
                                    await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                    await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                    GlobalFunctionMaCun.phesoi--;
                                }
                                else if (user.Id == GlobalFunctionMaCun.plr13)
                                {
                                    await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + user.Nickname + ".");
                                    await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                    await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                    GlobalFunctionMaCun.phethu3--;
                                    GlobalFunctionMaCun.dam = 0;
                                }
                                else
                                {
                                    await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + user.Nickname + ".");
                                    await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                    await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                    GlobalFunctionMaCun.phedan--;
                                }
                            }
                            else
                            {
                                embed.AddField($"Lỗi!", "Bạn không còn đạn để bắn.");
                                embed.WithColor(new Discord.Color(255, 0, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                        }
                        else
                        {
                            embed.AddField($"Lỗi!", "Lệnh này chỉ có thể sử dụng vào ban ngày.");
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
                else
                {
                    embed.AddField($"Lỗi!", "Bạn không thể sử dụng khi đã chết.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
            else return;
        }
        [Command("cuu")]
        public async Task thuoccuunguoi(IGuildUser user = null)
        {
            if (Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 580574598677135390)
            {
                var embed = new EmbedBuilder();
                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User))
                {
                    if (user == null)
                    {
                        embed.AddField($"Hệ Thống!", "-cuu (Số Người Chơi Muốn Cứu Bằn Thuốc");
                        embed.WithColor(new Discord.Color(255, 50, 255));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))
                    {
                        if (GlobalFunctionMaCun.gamestatus == 1)
                        {
                            if (Context.User.Id != GlobalFunctionMaCun.dongbang)
                            {if (user.Id == Context.User.Id)
                                {
                                        embed.AddField($"Lỗi!", "Bạn không thể sử dụng lệnh này lên bản thân.");
                                        embed.WithColor(new Discord.Color(255, 0, 0));
                                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                                }
                                else if (GlobalFunctionMaCun.thuoccuu >= 1)
                                {
                                    GlobalFunctionMaCun.cuu = user.Id;
                                    await Context.Message.AddReactionAsync(Emote.Parse("<:dachon:580977712689053696>"));
                                }
                                else
                                {
                                    embed.AddField($"Lỗi!", "Bạn không còn thuốc để cứu.");
                                    embed.WithColor(new Discord.Color(255, 0, 0));
                                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                                }
                            }
                            else
                            {
                                embed.AddField($"Lỗi!", "Bạn không thể dùng thuốc cứu khi bị Đóng Băng.");
                                embed.WithColor(new Discord.Color(255, 0, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                        }
                        else
                        {
                            embed.AddField($"Lỗi!", "Lệnh này chỉ có thể sử dụng vào ban đêm.");
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
                else
                {
                    embed.AddField($"Lỗi!", "Bạn không thể sử dụng khi đã chết.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
            else return;
        }
        [Command("dongbang")]
        public async Task dongbangnguoichoi(IGuildUser user = null)
        {
            if (Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 580574634811064342)
            {
                var embed = new EmbedBuilder();
                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User))
                {

                    if (user == null)
                    {
                        embed.AddField($"Hệ Thống!", "-dongbang (Số Người Chơi Muốn Đóng Băng");
                        embed.WithColor(new Discord.Color(255, 50, 255));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))
                    {
                        if (GlobalFunctionMaCun.gamestatus >= 2)
                        {
                            if (user.Id == Context.User.Id)
                            {
                                embed.AddField($"Lỗi!", "Bạn không thể sử dụng lệnh này lên bản thân.");
                                embed.WithColor(new Discord.Color(255, 0, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                            else if (user.Id == GlobalFunctionMaCun.plr4 || user.Id == GlobalFunctionMaCun.plr11)
                            {
                                embed.AddField($"Lỗi!", "Bạn không thể đóng băng với người cùng Phe.");
                                embed.WithColor(new Discord.Color(255, 0, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                            else
                            {
                                GlobalFunctionMaCun.dongbang = user.Id;
                                await Context.Message.AddReactionAsync(Emote.Parse("<:dachon:580977712689053696>"));
                            }
                        }
                        else
                        {
                            embed.AddField($"Lỗi!", "Lệnh này chỉ có thể sử dụng vào ban ngày.");
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
                else
                {
                    embed.AddField($"Lỗi!", "Bạn không thể sử dụng khi đã chết.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
            else return;
        }
        [Command("dam")]
        public async Task damnguoichoi(IGuildUser user = null)
        {
            if (Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 580574812662136836)
            {
                var embed = new EmbedBuilder();
                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User))
                {

                    if (user == null)
                    {
                        embed.AddField($"Hệ Thống!", "-dam (Số Người Chơi Muốn Đâm");
                        embed.WithColor(new Discord.Color(255, 50, 255));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))
                    {
                        if (GlobalFunctionMaCun.gamestatus == 1)
                        {
                            if (Context.User.Id != GlobalFunctionMaCun.dongbang)
                            {
                                if (user.Id == Context.User.Id)
                                {
                                    embed.AddField($"Lỗi!", "Bạn không thể sử dụng lệnh này lên bản thân.");
                                    embed.WithColor(new Discord.Color(255, 0, 0));
                                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                                }
                                else
                                {
                                    GlobalFunctionMaCun.dam = user.Id;
                                    await Context.Message.AddReactionAsync(Emote.Parse("<:dachon:580977712689053696>"));
                                }
                            }
                            else
                            {
                                embed.AddField($"Lỗi!", "Bạn không thể đâm người khác khi bị Đóng Băng.");
                                embed.WithColor(new Discord.Color(255, 0, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                        }
                        else
                        {
                            embed.AddField($"Lỗi!", "Lệnh này chỉ có thể sử dụng vào ban đêm.");
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
                else
                {
                    embed.AddField($"Lỗi!", "Bạn không thể sử dụng khi đã chết.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
            else return;
        }
        [Command("tesst")]
        public async Task testgame(string text = null)
        {
            if (text == "day")
            {
                GlobalFunctionMaCun.gamestatus = 2;
            }
            else if (text == "night")
            {
                GlobalFunctionMaCun.gamestatus = 1;
                GlobalFunctionMaCun.daycount++;
            }
            else return;
        }
    }
}
