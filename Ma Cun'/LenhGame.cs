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
using Neko_Test.ModulesMaCunGame;

namespace Neko_Test
{
    public class LenhGame : ModuleBase<SocketCommandContext>
    {


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
                            if (Context.User.Id != GlobalFunctionMaCun.dongbang || Context.User.Id != GlobalFunctionMaCun.moi)
                            {
                                if (user.Id == Context.User.Id)
                                {
                                    GlobalFunctionMaCun.baoveplr = 0;
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
                                embed.AddField($"Lỗi!", "Bạn không thể bảo vệ khi bị Đóng Băng hoặc bị Gái Điếm mời.");
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
        public async Task seerandauracheck(IGuildUser user = null)//583828385659355147
        {
            if (Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 580574739391578112 || Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 580574414660435982 || Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 583828385659355147)
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
                            if (Context.User.Id == GlobalFunctionMaCun.dongbang || Context.User.Id == GlobalFunctionMaCun.moi)
                            {
                                embed.AddField($"Lỗi!", "Bạn không thể dùng lệnh khi bị Đóng Băng hoặc bị Gái Điếm Mời.");
                                embed.WithColor(new Discord.Color(255, 0, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                            if (Context.User.Id == GlobalFunctionMaCun.moi)
                            {
                                embed.AddField($"Lỗi!", "Bạn không thể Soi khi bị Gái Điếm mời.");
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
                                if (Context.Channel.Id == 580574739391578112 & Context.User.Id != GlobalFunctionMaCun.dongbang || Context.Channel.Id == 580574739391578112 & Context.User.Id != GlobalFunctionMaCun.moi)
                                {
                                    if (GlobalFunctionMaCun.thayboi == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(user.Id, "phuphep");
                                        if (GlobalFunctionMaCun.checkphuphep == 0)
                                        {
                                            await GlobalFunctionMaCun.rolesid(user.Id, "aura");
                                            embed.AddField($"Hệ Thống!", "Người chơi số " + user.Nickname + " là "+GlobalFunctionMaCun.nameroles+".");
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
                                else if (Context.Channel.Id == 580574414660435982 & Context.User.Id != GlobalFunctionMaCun.dongbang || Context.Channel.Id == 580574414660435982 & Context.User.Id != GlobalFunctionMaCun.moi)
                                {
                                    if (GlobalFunctionMaCun.tientri == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(user.Id, "phuphep");
                                        if (GlobalFunctionMaCun.checkphuphep == 0)
                                        {
                                            await GlobalFunctionMaCun.rolesid(user.Id, "ten");
                                            embed.AddField($"Hệ Thống!", "Vai Trò của người chơi số " + user.Nickname + " là " + GlobalFunctionMaCun.nameroles + ".");
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
                                else if (Context.Channel.Id == 583828385659355147 & Context.User.Id != GlobalFunctionMaCun.dongbang || Context.User.Id != GlobalFunctionMaCun.moi || Context.Channel.Id == 583828385659355147 & Context.User.Id != GlobalFunctionMaCun.moi)
                                {
                                    if (GlobalFunctionMaCun.soitri == 1)
                                    {
                                        if (user.Id == GlobalFunctionMaCun.plr4 || user.Id == GlobalFunctionMaCun.plr6 || user.Id == GlobalFunctionMaCun.plr11 || user.Id == GlobalFunctionMaCun.plr17 & GlobalFunctionMaCun.caubesoi == 1)
                                        {
                                            embed.AddField($"Lỗi!", "Bạn không thể Soi người cùng phe.");
                                            embed.WithColor(new Discord.Color(255, 0, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                        }
                                        else if (GlobalFunctionMaCun.chucnangsoi == 1)
                                        {
                                            embed.AddField($"Lỗi!", "Bạn không thể soi khi đã từ bỏ chức năng của mình.");
                                            embed.WithColor(new Discord.Color(255, 0, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                        }
                                        else
                                        {
                                            await GlobalFunctionMaCun.rolesid(user.Id, "ten");
                                            embed.AddField($"Hệ Thống!", "Vai Trò của người chơi số " + user.Nickname + " là "+GlobalFunctionMaCun.nameroles+".");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.soitri--;
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
                                    embed.AddField($"Lỗi!", "Bạn không thể dùng lệnh khi bị Đóng Băng hoặc bị Gái Điếm Mời.");
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
            if (Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 580574451834290176 || Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 580574522361774081 || Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 580574634811064342 || Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 583828385659355147 || Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 589462982275235860)
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
                                if (Context.Channel.Id == 580574451834290176)
                                {
                                    if (user.Id == GlobalFunctionMaCun.plr6 || user.Id == GlobalFunctionMaCun.plr11 || user.Id == GlobalFunctionMaCun.plr16 || user.Id == GlobalFunctionMaCun.plr17 & GlobalFunctionMaCun.caubesoi == 1)
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
                                else if (Context.Channel.Id == 580574522361774081)
                                {
                                    if (user.Id == GlobalFunctionMaCun.plr4 || user.Id == GlobalFunctionMaCun.plr11 || user.Id == GlobalFunctionMaCun.plr16 || user.Id == GlobalFunctionMaCun.plr17 & GlobalFunctionMaCun.caubesoi == 1)
                                    {
                                        embed.AddField($"Lỗi!", "Bạn không thể cắn người cùng phe.");
                                        embed.WithColor(new Discord.Color(255, 0, 0));
                                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                                    }
                                    else if (GlobalFunctionMaCun.chucnangphuphep == 1)
                                    {
                                        GlobalFunctionMaCun.can = user.Id;
                                        await Context.Message.AddReactionAsync(Emote.Parse("<:dachon:580977712689053696>"));
                                        await Context.Guild.GetTextChannel(580564753982816256).SendMessageAsync("" + (Context.User as IGuildUser).Nickname + " Đã bỏ phiếu Cắn người chơi " + user.Nickname + "");
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
                                else if (Context.Channel.Id == 580574634811064342)
                                {
                                    if (user.Id == GlobalFunctionMaCun.plr4 || user.Id == GlobalFunctionMaCun.plr6 || user.Id == GlobalFunctionMaCun.plr16 || user.Id == GlobalFunctionMaCun.plr17 & GlobalFunctionMaCun.caubesoi == 1)
                                    {
                                        embed.AddField($"Lỗi!", "Bạn không thể cắn người cùng phe.");
                                        embed.WithColor(new Discord.Color(255, 0, 0));
                                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                                    }
                                    else if (GlobalFunctionMaCun.chucnangdongbang == 1)
                                    {
                                        GlobalFunctionMaCun.can = user.Id;
                                        await Context.Message.AddReactionAsync(Emote.Parse("<:dachon:580977712689053696>"));
                                        await Context.Guild.GetTextChannel(580564753982816256).SendMessageAsync("" + (Context.User as IGuildUser).Nickname + " Đã bỏ phiếu Cắn người chơi " + user.Nickname + "");
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
                                else if (Context.Channel.Id == 583828385659355147)
                                {
                                    if (user.Id == GlobalFunctionMaCun.plr4 || user.Id == GlobalFunctionMaCun.plr6 || user.Id == GlobalFunctionMaCun.plr11 || user.Id == GlobalFunctionMaCun.plr17 & GlobalFunctionMaCun.caubesoi == 1)
                                    {
                                        embed.AddField($"Lỗi!", "Bạn không thể cắn người cùng phe.");
                                        embed.WithColor(new Discord.Color(255, 0, 0));
                                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                                    }
                                    else if (GlobalFunctionMaCun.chucnangsoi == 1)
                                    {
                                        GlobalFunctionMaCun.can = user.Id;
                                        await Context.Message.AddReactionAsync(Emote.Parse("<:dachon:580977712689053696>"));
                                        await Context.Guild.GetTextChannel(580564753982816256).SendMessageAsync("" + (Context.User as IGuildUser).Nickname + " Đã bỏ phiếu Cắn người chơi " + user.Nickname + "");
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
                                else if (Context.Channel.Id == 589462982275235860 & GlobalFunctionMaCun.caubesoi == 1)
                                {
                                    if (user.Id == GlobalFunctionMaCun.plr6 || user.Id == GlobalFunctionMaCun.plr11 || user.Id == GlobalFunctionMaCun.plr16 || user.Id == GlobalFunctionMaCun.plr16)
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
                            else if (GlobalFunctionMaCun.chucnangphuphep == 1)
                            {
                                embed.AddField($"Lỗi!", "Bạn không thể phù phép khi đã từ bỏ chức năng của mình.");
                                embed.WithColor(new Discord.Color(255, 0, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                            else if (GlobalFunctionMaCun.phesoi <= 1)
                            {
                                embed.AddField($"Lỗi!", "Bạn không thể phù phép khi bạn là con sói cuối cùng.");
                                embed.WithColor(new Discord.Color(255, 0, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                            else if (user.Id == GlobalFunctionMaCun.plr4 || user.Id == GlobalFunctionMaCun.plr11 || user.Id == GlobalFunctionMaCun.plr16 || user.Id == GlobalFunctionMaCun.plr17 & GlobalFunctionMaCun.caubesoi == 1)
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
                        if (Context.User.Id == GlobalFunctionMaCun.dongbang || Context.User.Id == GlobalFunctionMaCun.moi)
                        {
                            embed.AddField($"Lỗi!", "Bạn không thể dùng lệnh khi bị Đóng Băng hoặc bị Gái Điếm mời.");
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
                        if (Context.User.Id == GlobalFunctionMaCun.dongbang || Context.User.Id == GlobalFunctionMaCun.moi)
                        {
                            embed.AddField($"Lỗi!", "Bạn không thể đầu độc khi bị Đóng Băng hoặc bị Gái Điếm mời.");
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
                        else if (GlobalFunctionMaCun.thuocdoc >= 1)
                        {
                            GlobalFunctionMaCun.thuocdoc--;
                            if (user.Id == GlobalFunctionMaCun.plr1)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(user.Id, "ten");
                                    await GlobalFunctionMaCun.rolesid(user.Id, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + user.Nickname + ".");
                                await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                GlobalFunctionMaCun.baoveplr = 0;
                                GlobalFunctionMaCun.phedan--;
                            }
                            else if (user.Id == GlobalFunctionMaCun.plr4)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(user.Id, "ten");
                                    await GlobalFunctionMaCun.rolesid(user.Id, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + user.Nickname + ".");
                                await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                GlobalFunctionMaCun.phesoi--;
                            }
                            else if (user.Id == GlobalFunctionMaCun.plr6)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(user.Id, "ten");
                                    await GlobalFunctionMaCun.rolesid(user.Id, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + user.Nickname + ".");
                                await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                GlobalFunctionMaCun.phesoi--;
                            }
                            else if (user.Id == GlobalFunctionMaCun.plr7)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(user.Id, "ten");
                                    await GlobalFunctionMaCun.rolesid(user.Id, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + user.Nickname + ".");
                                await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                GlobalFunctionMaCun.phedan--;
                                if (GlobalFunctionMaCun.keo != 0)
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
                                        GlobalFunctionMaCun.baoveplr = 0;
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
                                        GlobalFunctionMaCun.cuu = 0;
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
                                        GlobalFunctionMaCun.dam = 0;
                                    }
                                    else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr14)
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
                                        GlobalFunctionMaCun.moi = 0;
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
                                    else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr17)
                                    {
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "ten");
                                            await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "idrole");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            if (GlobalFunctionMaCun.caubesoi == 1)
                                            {
                                                GlobalFunctionMaCun.phesoi--;
                                            }
                                            else GlobalFunctionMaCun.phedan--;
                                        }
                                        else
                                        {
                                            if (GlobalFunctionMaCun.caubesoi == 1)
                                            {
                                                await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                                await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                GlobalFunctionMaCun.phesoi--;
                                            }
                                            else
                                            {
                                                await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                                await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                GlobalFunctionMaCun.phesoi--;
                                            }
                                        }
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
                                        if (GlobalFunctionMaCun.giaoxu1 != 0)
                                        {
                                            if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu1)))
                                            {
                                                var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu1);
                                                await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                if (GlobalFunctionMaCun.deadroles == 1)
                                                {
                                                    GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu1, "ten");
                                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                }
                                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                GlobalFunctionMaCun.giaoxu1 = 0;
                                            }
                                            else GlobalFunctionMaCun.giaoxu1 = 0;
                                        }
                                        if (GlobalFunctionMaCun.giaoxu2 != 0)
                                        {
                                            if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu2)))
                                            {
                                                var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu2);
                                                await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                if (GlobalFunctionMaCun.deadroles == 1)
                                                {
                                                    GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu2, "ten");
                                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                }
                                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                GlobalFunctionMaCun.giaoxu2 = 0;
                                            }
                                            else GlobalFunctionMaCun.giaoxu2 = 0;
                                        }
                                        if (GlobalFunctionMaCun.giaoxu3 != 0)
                                        {
                                            if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu3)))
                                            {
                                                var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu3);
                                                await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                if (GlobalFunctionMaCun.deadroles == 1)
                                                {
                                                    GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu3, "ten");
                                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                }
                                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                GlobalFunctionMaCun.giaoxu3 = 0;
                                            }
                                            else GlobalFunctionMaCun.giaoxu3 = 0;
                                        }
                                        if (GlobalFunctionMaCun.giaoxu4 != 0)
                                        {
                                            if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu4)))
                                            {
                                                var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu4);
                                                await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                if (GlobalFunctionMaCun.deadroles == 1)
                                                {
                                                    GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu4, "ten");
                                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                }
                                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                GlobalFunctionMaCun.giaoxu4 = 0;
                                            }
                                            else GlobalFunctionMaCun.giaoxu4 = 0;
                                        }
                                        if (GlobalFunctionMaCun.giaoxu5 != 0)
                                        {
                                            if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu5)))
                                            {
                                                var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu5);
                                                await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                if (GlobalFunctionMaCun.deadroles == 1)
                                                {
                                                    GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu5, "ten");
                                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                }
                                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                GlobalFunctionMaCun.giaoxu5 = 0;
                                            }
                                            else GlobalFunctionMaCun.giaoxu5 = 0;
                                        }
                                        if (GlobalFunctionMaCun.giaoxu6 != 0)
                                        {
                                            if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu6)))
                                            {
                                                var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu6);
                                                await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                if (GlobalFunctionMaCun.deadroles == 1)
                                                {
                                                    GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu6, "ten");
                                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                }
                                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                GlobalFunctionMaCun.giaoxu6 = 0;
                                            }
                                            else GlobalFunctionMaCun.giaoxu6 = 0;
                                        }
                                        if (GlobalFunctionMaCun.giaoxu7 != 0)
                                        {
                                            if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu7)))
                                            {
                                                var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu7);
                                                await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                if (GlobalFunctionMaCun.deadroles == 1)
                                                {
                                                    GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu7, "ten");
                                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                }
                                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                GlobalFunctionMaCun.giaoxu7 = 0;
                                            }
                                            else GlobalFunctionMaCun.giaoxu7 = 0;
                                        }
                                        if (GlobalFunctionMaCun.giaoxu8 != 0)
                                        {
                                            if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu8)))
                                            {
                                                var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu8);
                                                await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                if (GlobalFunctionMaCun.deadroles == 1)
                                                {
                                                    GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu8, "ten");
                                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                }
                                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                GlobalFunctionMaCun.giaoxu8 = 0;
                                            }
                                            else GlobalFunctionMaCun.giaoxu8 = 0;
                                        }
                                        if (GlobalFunctionMaCun.giaoxu9 != 0)
                                        {
                                            if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu9)))
                                            {
                                                var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu9);
                                                await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                if (GlobalFunctionMaCun.deadroles == 1)
                                                {
                                                    GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu9, "ten");
                                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                }
                                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                GlobalFunctionMaCun.giaoxu9 = 0;
                                            }
                                            else GlobalFunctionMaCun.giaoxu9 = 0;
                                        }
                                        if (GlobalFunctionMaCun.giaoxu10 != 0)
                                        {
                                            if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu10)))
                                            {
                                                var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu10);
                                                await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                if (GlobalFunctionMaCun.deadroles == 1)
                                                {
                                                    GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu10, "ten");
                                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                }
                                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                GlobalFunctionMaCun.giaoxu10 = 0;
                                            }
                                            else GlobalFunctionMaCun.giaoxu10 = 0;
                                        }
                                        if (GlobalFunctionMaCun.giaoxu11 != 0)
                                        {
                                            if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu11)))
                                            {
                                                var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu11);
                                                await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                if (GlobalFunctionMaCun.deadroles == 1)
                                                {
                                                    GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu11, "ten");
                                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                }
                                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                GlobalFunctionMaCun.giaoxu11 = 0;
                                            }
                                            else GlobalFunctionMaCun.giaoxu11 = 0;
                                        }
                                        if (GlobalFunctionMaCun.giaoxu12 != 0)
                                        {
                                            if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu12)))
                                            {
                                                var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu12);
                                                await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                if (GlobalFunctionMaCun.deadroles == 1)
                                                {
                                                    GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu12, "ten");
                                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                }
                                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                GlobalFunctionMaCun.giaoxu12 = 0;
                                            }
                                            else GlobalFunctionMaCun.giaoxu12 = 0;
                                        }
                                        if (GlobalFunctionMaCun.giaoxu13 != 0)
                                        {
                                            if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu13)))
                                            {
                                                var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu13);
                                                await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                if (GlobalFunctionMaCun.deadroles == 1)
                                                {
                                                    GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu13, "ten");
                                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                }
                                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                GlobalFunctionMaCun.giaoxu13 = 0;
                                            }
                                            else GlobalFunctionMaCun.giaoxu13 = 0;
                                        }
                                        if (GlobalFunctionMaCun.giaoxu14 != 0)
                                        {
                                            if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu14)))
                                            {
                                                var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu14);
                                                await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                if (GlobalFunctionMaCun.deadroles == 1)
                                                {
                                                    GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu14, "ten");
                                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                }
                                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                GlobalFunctionMaCun.giaoxu14 = 0;
                                            }
                                            else GlobalFunctionMaCun.giaoxu14 = 0;
                                        }
                                        if (GlobalFunctionMaCun.giaoxu15 != 0)
                                        {
                                            if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu15)))
                                            {
                                                var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu15);
                                                await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                if (GlobalFunctionMaCun.deadroles == 1)
                                                {
                                                    GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu15, "ten");
                                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                }
                                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                GlobalFunctionMaCun.giaoxu15 = 0;
                                            }
                                            else GlobalFunctionMaCun.giaoxu15 = 0;
                                        }
                                        if (GlobalFunctionMaCun.giaoxu16 != 0)
                                        {
                                            if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu16)))
                                            {
                                                var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu16);
                                                await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                if (GlobalFunctionMaCun.deadroles == 1)
                                                {
                                                    GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu16, "ten");
                                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                }
                                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                GlobalFunctionMaCun.giaoxu16 = 0;
                                            }
                                            else GlobalFunctionMaCun.giaoxu16 = 0;
                                        }
                                        if (GlobalFunctionMaCun.giaoxu17 != 0)
                                        {
                                            if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu17)))
                                            {
                                                var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu17);
                                                await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                if (GlobalFunctionMaCun.deadroles == 1)
                                                {
                                                    GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu17, "ten");
                                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                }
                                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                GlobalFunctionMaCun.giaoxu17 = 0;
                                            }
                                            else GlobalFunctionMaCun.giaoxu17 = 0;
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
                                    }
                                }
                                else return;
                            }
                            else if (user.Id == GlobalFunctionMaCun.plr11)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(user.Id, "ten");
                                    await GlobalFunctionMaCun.rolesid(user.Id, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + user.Nickname + ".");
                                await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                GlobalFunctionMaCun.phesoi--;
                            }
                            else if (user.Id == GlobalFunctionMaCun.plr13)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(user.Id, "ten");
                                    await GlobalFunctionMaCun.rolesid(user.Id, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + user.Nickname + ".");
                                await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                GlobalFunctionMaCun.phethu3--;
                                GlobalFunctionMaCun.dam = 0;
                            }
                            else if (user.Id == GlobalFunctionMaCun.plr14)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(user.Id, "ten");
                                    await GlobalFunctionMaCun.rolesid(user.Id, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + user.Nickname + ".");
                                await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                GlobalFunctionMaCun.phedan--;
                                GlobalFunctionMaCun.moi = 0;
                            }
                            else if (user.Id == GlobalFunctionMaCun.plr16)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(user.Id, "ten");
                                    await GlobalFunctionMaCun.rolesid(user.Id, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + user.Nickname + ".");
                                await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                GlobalFunctionMaCun.phesoi--;
                            }
                            else if (user.Id == GlobalFunctionMaCun.plr17)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(user.Id, "ten");
                                    await GlobalFunctionMaCun.rolesid(user.Id, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + Context.Guild.GetUser(user.Id).Nickname + ".");
                                await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                if (GlobalFunctionMaCun.caubesoi == 1)
                                {
                                    GlobalFunctionMaCun.phesoi--;
                                }
                                else GlobalFunctionMaCun.phedan--;
                            }
                            else if (user.Id == GlobalFunctionMaCun.plr18)
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(user.Id, "ten");
                                    await GlobalFunctionMaCun.rolesid(user.Id, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + Context.Guild.GetUser(user.Id).Nickname + ".");
                                await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                GlobalFunctionMaCun.phethu3--;
                                if (GlobalFunctionMaCun.giaoxu1 != 0)
                                {
                                    if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu1)))
                                    {
                                        var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu1);
                                        await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu1, "ten");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        }
                                        else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        GlobalFunctionMaCun.giaoxu1 = 0;
                                    }
                                    else GlobalFunctionMaCun.giaoxu1 = 0;
                                }
                                if (GlobalFunctionMaCun.giaoxu2 != 0)
                                {
                                    if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu2)))
                                    {
                                        var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu2);
                                        await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu2, "ten");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        }
                                        else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        GlobalFunctionMaCun.giaoxu2 = 0;
                                    }
                                    else GlobalFunctionMaCun.giaoxu2 = 0;
                                }
                                if (GlobalFunctionMaCun.giaoxu3 != 0)
                                {
                                    if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu3)))
                                    {
                                        var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu3);
                                        await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu3, "ten");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        }
                                        else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        GlobalFunctionMaCun.giaoxu3 = 0;
                                    }
                                    else GlobalFunctionMaCun.giaoxu3 = 0;
                                }
                                if (GlobalFunctionMaCun.giaoxu4 != 0)
                                {
                                    if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu4)))
                                    {
                                        var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu4);
                                        await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu4, "ten");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        }
                                        else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        GlobalFunctionMaCun.giaoxu4 = 0;
                                    }
                                    else GlobalFunctionMaCun.giaoxu4 = 0;
                                }
                                if (GlobalFunctionMaCun.giaoxu5 != 0)
                                {
                                    if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu5)))
                                    {
                                        var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu5);
                                        await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu5, "ten");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        }
                                        else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        GlobalFunctionMaCun.giaoxu5 = 0;
                                    }
                                    else GlobalFunctionMaCun.giaoxu5 = 0;
                                }
                                if (GlobalFunctionMaCun.giaoxu6 != 0)
                                {
                                    if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu6)))
                                    {
                                        var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu6);
                                        await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu6, "ten");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        }
                                        else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        GlobalFunctionMaCun.giaoxu6 = 0;
                                    }
                                    else GlobalFunctionMaCun.giaoxu6 = 0;
                                }
                                if (GlobalFunctionMaCun.giaoxu7 != 0)
                                {
                                    if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu7)))
                                    {
                                        var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu7);
                                        await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu7, "ten");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        }
                                        else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        GlobalFunctionMaCun.giaoxu7 = 0;
                                    }
                                    else GlobalFunctionMaCun.giaoxu7 = 0;
                                }
                                if (GlobalFunctionMaCun.giaoxu8 != 0)
                                {
                                    if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu8)))
                                    {
                                        var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu8);
                                        await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu8, "ten");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        }
                                        else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        GlobalFunctionMaCun.giaoxu8 = 0;
                                    }
                                    else GlobalFunctionMaCun.giaoxu8 = 0;
                                }
                                if (GlobalFunctionMaCun.giaoxu9 != 0)
                                {
                                    if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu9)))
                                    {
                                        var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu9);
                                        await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu9, "ten");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        }
                                        else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        GlobalFunctionMaCun.giaoxu9 = 0;
                                    }
                                    else GlobalFunctionMaCun.giaoxu9 = 0;
                                }
                                if (GlobalFunctionMaCun.giaoxu10 != 0)
                                {
                                    if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu10)))
                                    {
                                        var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu10);
                                        await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu10, "ten");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        }
                                        else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        GlobalFunctionMaCun.giaoxu10 = 0;
                                    }
                                    else GlobalFunctionMaCun.giaoxu10 = 0;
                                }
                                if (GlobalFunctionMaCun.giaoxu11 != 0)
                                {
                                    if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu11)))
                                    {
                                        var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu11);
                                        await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu11, "ten");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        }
                                        else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        GlobalFunctionMaCun.giaoxu11 = 0;
                                    }
                                    else GlobalFunctionMaCun.giaoxu11 = 0;
                                }
                                if (GlobalFunctionMaCun.giaoxu12 != 0)
                                {
                                    if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu12)))
                                    {
                                        var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu12);
                                        await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu12, "ten");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        }
                                        else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        GlobalFunctionMaCun.giaoxu12 = 0;
                                    }
                                    else GlobalFunctionMaCun.giaoxu12 = 0;
                                }
                                if (GlobalFunctionMaCun.giaoxu13 != 0)
                                {
                                    if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu13)))
                                    {
                                        var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu13);
                                        await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu13, "ten");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        }
                                        else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        GlobalFunctionMaCun.giaoxu13 = 0;
                                    }
                                    else GlobalFunctionMaCun.giaoxu13 = 0;
                                }
                                if (GlobalFunctionMaCun.giaoxu14 != 0)
                                {
                                    if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu14)))
                                    {
                                        var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu14);
                                        await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu14, "ten");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        }
                                        else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        GlobalFunctionMaCun.giaoxu14 = 0;
                                    }
                                    else GlobalFunctionMaCun.giaoxu14 = 0;
                                }
                                if (GlobalFunctionMaCun.giaoxu15 != 0)
                                {
                                    if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu15)))
                                    {
                                        var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu15);
                                        await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu15, "ten");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        }
                                        else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        GlobalFunctionMaCun.giaoxu15 = 0;
                                    }
                                    else GlobalFunctionMaCun.giaoxu15 = 0;
                                }
                                if (GlobalFunctionMaCun.giaoxu16 != 0)
                                {
                                    if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu16)))
                                    {
                                        var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu16);
                                        await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu16, "ten");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        }
                                        else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        GlobalFunctionMaCun.giaoxu16 = 0;
                                    }
                                    else GlobalFunctionMaCun.giaoxu16 = 0;
                                }
                                if (GlobalFunctionMaCun.giaoxu17 != 0)
                                {
                                    if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu17)))
                                    {
                                        var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu17);
                                        await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu17, "ten");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        }
                                        else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                        GlobalFunctionMaCun.giaoxu17 = 0;
                                    }
                                    else GlobalFunctionMaCun.giaoxu17 = 0;
                                }
                            }
                            else
                            {
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(user.Id, "ten");
                                    await GlobalFunctionMaCun.rolesid(user.Id, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + user.Nickname + ".");
                                await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                GlobalFunctionMaCun.phedan--;
                            }

                            OverwritePermissions chophepsoi = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Allow, readMessageHistory: PermValue.Allow);
                            if (GlobalFunctionMaCun.caubehoangda != 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                            {
                                if (!Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.caubehoangda)) & GlobalFunctionMaCun.caubesoi == 0)
                                {
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.GetUser(GlobalFunctionMaCun.plr17) as IGuildUser, chophepsoi.Modify());
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                    GlobalFunctionMaCun.phesoi++;
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.caubesoi = 1;
                                    GlobalFunctionMaCun.caubehoangda = 0;
                                }
                                else
                                {
                                    return;
                                }
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
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(user.Id, "ten");
                                        await GlobalFunctionMaCun.rolesid(user.Id, "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + user.Nickname + ".");
                                    await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                    await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                    GlobalFunctionMaCun.baoveplr = 0;
                                    GlobalFunctionMaCun.phedan--;
                                }
                                else if (user.Id == GlobalFunctionMaCun.plr4)
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(user.Id, "ten");
                                        await GlobalFunctionMaCun.rolesid(user.Id, "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + user.Nickname + ".");
                                    await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                    await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                    GlobalFunctionMaCun.phesoi--;
                                }
                                else if (user.Id == GlobalFunctionMaCun.plr6)
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(user.Id, "ten");
                                        await GlobalFunctionMaCun.rolesid(user.Id, "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + user.Nickname + ".");
                                    await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                    await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                    GlobalFunctionMaCun.phesoi--;
                                }
                                else if (user.Id == GlobalFunctionMaCun.plr7)
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(user.Id, "ten");
                                        await GlobalFunctionMaCun.rolesid(user.Id, "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + user.Nickname + ".");
                                    await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                    await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                    GlobalFunctionMaCun.phedan--;
                                    if (GlobalFunctionMaCun.keo != 0)
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
                                            GlobalFunctionMaCun.baoveplr = 0;
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
                                            GlobalFunctionMaCun.cuu = 0;
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
                                            GlobalFunctionMaCun.dam = 0;
                                        }
                                        else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr14)
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
                                            GlobalFunctionMaCun.moi = 0;
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
                                        else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr17)
                                        {
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "ten");
                                                await GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.keo, "idrole");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết chung.");
                                                await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                if (GlobalFunctionMaCun.caubesoi == 1)
                                                {
                                                    GlobalFunctionMaCun.phesoi--;
                                                }
                                                else GlobalFunctionMaCun.phedan--;
                                            }
                                            else
                                            {
                                                if (GlobalFunctionMaCun.caubesoi == 1)
                                                {
                                                    await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                                    await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                    await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                    GlobalFunctionMaCun.phesoi--;
                                                }
                                                else
                                                {
                                                    await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                                    await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                    await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                    GlobalFunctionMaCun.phesoi--;
                                                }
                                            }
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
                                            if (GlobalFunctionMaCun.giaoxu1 != 0)
                                            {
                                                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu1)))
                                                {
                                                    var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu1);
                                                    await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                    await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                    if (GlobalFunctionMaCun.deadroles == 1)
                                                    {
                                                        GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu1, "ten");
                                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    }
                                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    GlobalFunctionMaCun.giaoxu1 = 0;
                                                }
                                                else GlobalFunctionMaCun.giaoxu1 = 0;
                                            }
                                            if (GlobalFunctionMaCun.giaoxu2 != 0)
                                            {
                                                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu2)))
                                                {
                                                    var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu2);
                                                    await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                    await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                    if (GlobalFunctionMaCun.deadroles == 1)
                                                    {
                                                        GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu2, "ten");
                                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    }
                                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    GlobalFunctionMaCun.giaoxu2 = 0;
                                                }
                                                else GlobalFunctionMaCun.giaoxu2 = 0;
                                            }
                                            if (GlobalFunctionMaCun.giaoxu3 != 0)
                                            {
                                                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu3)))
                                                {
                                                    var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu3);
                                                    await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                    await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                    if (GlobalFunctionMaCun.deadroles == 1)
                                                    {
                                                        GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu3, "ten");
                                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    }
                                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    GlobalFunctionMaCun.giaoxu3 = 0;
                                                }
                                                else GlobalFunctionMaCun.giaoxu3 = 0;
                                            }
                                            if (GlobalFunctionMaCun.giaoxu4 != 0)
                                            {
                                                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu4)))
                                                {
                                                    var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu4);
                                                    await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                    await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                    if (GlobalFunctionMaCun.deadroles == 1)
                                                    {
                                                        GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu4, "ten");
                                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    }
                                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    GlobalFunctionMaCun.giaoxu4 = 0;
                                                }
                                                else GlobalFunctionMaCun.giaoxu4 = 0;
                                            }
                                            if (GlobalFunctionMaCun.giaoxu5 != 0)
                                            {
                                                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu5)))
                                                {
                                                    var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu5);
                                                    await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                    await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                    if (GlobalFunctionMaCun.deadroles == 1)
                                                    {
                                                        GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu5, "ten");
                                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    }
                                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    GlobalFunctionMaCun.giaoxu5 = 0;
                                                }
                                                else GlobalFunctionMaCun.giaoxu5 = 0;
                                            }
                                            if (GlobalFunctionMaCun.giaoxu6 != 0)
                                            {
                                                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu6)))
                                                {
                                                    var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu6);
                                                    await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                    await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                    if (GlobalFunctionMaCun.deadroles == 1)
                                                    {
                                                        GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu6, "ten");
                                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    }
                                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    GlobalFunctionMaCun.giaoxu6 = 0;
                                                }
                                                else GlobalFunctionMaCun.giaoxu6 = 0;
                                            }
                                            if (GlobalFunctionMaCun.giaoxu7 != 0)
                                            {
                                                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu7)))
                                                {
                                                    var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu7);
                                                    await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                    await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                    if (GlobalFunctionMaCun.deadroles == 1)
                                                    {
                                                        GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu7, "ten");
                                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    }
                                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    GlobalFunctionMaCun.giaoxu7 = 0;
                                                }
                                                else GlobalFunctionMaCun.giaoxu7 = 0;
                                            }
                                            if (GlobalFunctionMaCun.giaoxu8 != 0)
                                            {
                                                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu8)))
                                                {
                                                    var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu8);
                                                    await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                    await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                    if (GlobalFunctionMaCun.deadroles == 1)
                                                    {
                                                        GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu8, "ten");
                                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    }
                                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    GlobalFunctionMaCun.giaoxu8 = 0;
                                                }
                                                else GlobalFunctionMaCun.giaoxu8 = 0;
                                            }
                                            if (GlobalFunctionMaCun.giaoxu9 != 0)
                                            {
                                                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu9)))
                                                {
                                                    var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu9);
                                                    await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                    await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                    if (GlobalFunctionMaCun.deadroles == 1)
                                                    {
                                                        GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu9, "ten");
                                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    }
                                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    GlobalFunctionMaCun.giaoxu9 = 0;
                                                }
                                                else GlobalFunctionMaCun.giaoxu9 = 0;
                                            }
                                            if (GlobalFunctionMaCun.giaoxu10 != 0)
                                            {
                                                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu10)))
                                                {
                                                    var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu10);
                                                    await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                    await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                    if (GlobalFunctionMaCun.deadroles == 1)
                                                    {
                                                        GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu10, "ten");
                                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    }
                                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    GlobalFunctionMaCun.giaoxu10 = 0;
                                                }
                                                else GlobalFunctionMaCun.giaoxu10 = 0;
                                            }
                                            if (GlobalFunctionMaCun.giaoxu11 != 0)
                                            {
                                                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu11)))
                                                {
                                                    var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu11);
                                                    await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                    await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                    if (GlobalFunctionMaCun.deadroles == 1)
                                                    {
                                                        GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu11, "ten");
                                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    }
                                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    GlobalFunctionMaCun.giaoxu11 = 0;
                                                }
                                                else GlobalFunctionMaCun.giaoxu11 = 0;
                                            }
                                            if (GlobalFunctionMaCun.giaoxu12 != 0)
                                            {
                                                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu12)))
                                                {
                                                    var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu12);
                                                    await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                    await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                    if (GlobalFunctionMaCun.deadroles == 1)
                                                    {
                                                        GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu12, "ten");
                                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    }
                                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    GlobalFunctionMaCun.giaoxu12 = 0;
                                                }
                                                else GlobalFunctionMaCun.giaoxu12 = 0;
                                            }
                                            if (GlobalFunctionMaCun.giaoxu13 != 0)
                                            {
                                                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu13)))
                                                {
                                                    var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu13);
                                                    await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                    await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                    if (GlobalFunctionMaCun.deadroles == 1)
                                                    {
                                                        GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu13, "ten");
                                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    }
                                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    GlobalFunctionMaCun.giaoxu13 = 0;
                                                }
                                                else GlobalFunctionMaCun.giaoxu13 = 0;
                                            }
                                            if (GlobalFunctionMaCun.giaoxu14 != 0)
                                            {
                                                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu14)))
                                                {
                                                    var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu14);
                                                    await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                    await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                    if (GlobalFunctionMaCun.deadroles == 1)
                                                    {
                                                        GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu14, "ten");
                                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    }
                                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    GlobalFunctionMaCun.giaoxu14 = 0;
                                                }
                                                else GlobalFunctionMaCun.giaoxu14 = 0;
                                            }
                                            if (GlobalFunctionMaCun.giaoxu15 != 0)
                                            {
                                                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu15)))
                                                {
                                                    var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu15);
                                                    await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                    await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                    if (GlobalFunctionMaCun.deadroles == 1)
                                                    {
                                                        GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu15, "ten");
                                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    }
                                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    GlobalFunctionMaCun.giaoxu15 = 0;
                                                }
                                                else GlobalFunctionMaCun.giaoxu15 = 0;
                                            }
                                            if (GlobalFunctionMaCun.giaoxu16 != 0)
                                            {
                                                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu16)))
                                                {
                                                    var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu16);
                                                    await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                    await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                    if (GlobalFunctionMaCun.deadroles == 1)
                                                    {
                                                        GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu16, "ten");
                                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    }
                                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    GlobalFunctionMaCun.giaoxu16 = 0;
                                                }
                                                else GlobalFunctionMaCun.giaoxu16 = 0;
                                            }
                                            if (GlobalFunctionMaCun.giaoxu17 != 0)
                                            {
                                                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu17)))
                                                {
                                                    var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu17);
                                                    await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                                    await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                                    if (GlobalFunctionMaCun.deadroles == 1)
                                                    {
                                                        GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu17, "ten");
                                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    }
                                                    else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                                    GlobalFunctionMaCun.giaoxu17 = 0;
                                                }
                                                else GlobalFunctionMaCun.giaoxu17 = 0;
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
                                        }
                                    }
                                    else return;
                                }
                                else if (user.Id == GlobalFunctionMaCun.plr11)
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(user.Id, "ten");
                                        await GlobalFunctionMaCun.rolesid(user.Id, "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + user.Nickname + ".");
                                    await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                    await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                    GlobalFunctionMaCun.phesoi--;
                                }
                                else if (user.Id == GlobalFunctionMaCun.plr13)
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(user.Id, "ten");
                                        await GlobalFunctionMaCun.rolesid(user.Id, "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + user.Nickname + ".");
                                    await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                    await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                    GlobalFunctionMaCun.phethu3--;
                                    GlobalFunctionMaCun.dam = 0;
                                }
                                else if (GlobalFunctionMaCun.treo == GlobalFunctionMaCun.plr14)
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(user.Id, "ten");
                                        await GlobalFunctionMaCun.rolesid(user.Id, "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + user.Nickname + ".");
                                    await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                    await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.moi = 0;
                                }
                                else if (user.Id == GlobalFunctionMaCun.plr16)
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(user.Id, "ten");
                                        await GlobalFunctionMaCun.rolesid(user.Id, "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + user.Nickname + ".");
                                    await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                    await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                    GlobalFunctionMaCun.phesoi--;
                                }
                                else if (user.Id == GlobalFunctionMaCun.plr17)
                                {
                                    if (GlobalFunctionMaCun.caubesoi == 1)
                                    {
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            await GlobalFunctionMaCun.rolesid(user.Id, "ten");
                                            await GlobalFunctionMaCun.rolesid(user.Id, "idrole");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                        }
                                        else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + user.Nickname + ".");
                                        await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phesoi--;
                                    }
                                    else
                                    {
                                        if (GlobalFunctionMaCun.deadroles == 1)
                                        {
                                            await GlobalFunctionMaCun.rolesid(user.Id, "ten");
                                            await GlobalFunctionMaCun.rolesid(user.Id, "idrole");
                                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                        }
                                        else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + user.Nickname + ".");
                                        await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phedan--;
                                    }
                                }
                                else if (user.Id == GlobalFunctionMaCun.plr18)
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(user.Id, "ten");
                                        await GlobalFunctionMaCun.rolesid(user.Id, "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + user.Nickname + ".");
                                    await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                    await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                    GlobalFunctionMaCun.phethu3--;
                                    if (GlobalFunctionMaCun.giaoxu1 != 0)
                                    {
                                        if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu1)))
                                        {
                                            var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu1);
                                            await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu1, "ten");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            }
                                            else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            GlobalFunctionMaCun.giaoxu1 = 0;
                                        }
                                        else GlobalFunctionMaCun.giaoxu1 = 0;
                                    }
                                    if (GlobalFunctionMaCun.giaoxu2 != 0)
                                    {
                                        if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu2)))
                                        {
                                            var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu2);
                                            await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu2, "ten");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            }
                                            else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            GlobalFunctionMaCun.giaoxu2 = 0;
                                        }
                                        else GlobalFunctionMaCun.giaoxu2 = 0;
                                    }
                                    if (GlobalFunctionMaCun.giaoxu3 != 0)
                                    {
                                        if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu3)))
                                        {
                                            var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu3);
                                            await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu3, "ten");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            }
                                            else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            GlobalFunctionMaCun.giaoxu3 = 0;
                                        }
                                        else GlobalFunctionMaCun.giaoxu3 = 0;
                                    }
                                    if (GlobalFunctionMaCun.giaoxu4 != 0)
                                    {
                                        if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu4)))
                                        {
                                            var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu4);
                                            await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu4, "ten");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            }
                                            else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            GlobalFunctionMaCun.giaoxu4 = 0;
                                        }
                                        else GlobalFunctionMaCun.giaoxu4 = 0;
                                    }
                                    if (GlobalFunctionMaCun.giaoxu5 != 0)
                                    {
                                        if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu5)))
                                        {
                                            var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu5);
                                            await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu5, "ten");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            }
                                            else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            GlobalFunctionMaCun.giaoxu5 = 0;
                                        }
                                        else GlobalFunctionMaCun.giaoxu5 = 0;
                                    }
                                    if (GlobalFunctionMaCun.giaoxu6 != 0)
                                    {
                                        if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu6)))
                                        {
                                            var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu6);
                                            await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu6, "ten");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            }
                                            else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            GlobalFunctionMaCun.giaoxu6 = 0;
                                        }
                                        else GlobalFunctionMaCun.giaoxu6 = 0;
                                    }
                                    if (GlobalFunctionMaCun.giaoxu7 != 0)
                                    {
                                        if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu7)))
                                        {
                                            var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu7);
                                            await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu7, "ten");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            }
                                            else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            GlobalFunctionMaCun.giaoxu7 = 0;
                                        }
                                        else GlobalFunctionMaCun.giaoxu7 = 0;
                                    }
                                    if (GlobalFunctionMaCun.giaoxu8 != 0)
                                    {
                                        if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu8)))
                                        {
                                            var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu8);
                                            await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu8, "ten");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            }
                                            else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            GlobalFunctionMaCun.giaoxu8 = 0;
                                        }
                                        else GlobalFunctionMaCun.giaoxu8 = 0;
                                    }
                                    if (GlobalFunctionMaCun.giaoxu9 != 0)
                                    {
                                        if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu9)))
                                        {
                                            var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu9);
                                            await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu9, "ten");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            }
                                            else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            GlobalFunctionMaCun.giaoxu9 = 0;
                                        }
                                        else GlobalFunctionMaCun.giaoxu9 = 0;
                                    }
                                    if (GlobalFunctionMaCun.giaoxu10 != 0)
                                    {
                                        if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu10)))
                                        {
                                            var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu10);
                                            await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu10, "ten");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            }
                                            else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            GlobalFunctionMaCun.giaoxu10 = 0;
                                        }
                                        else GlobalFunctionMaCun.giaoxu10 = 0;
                                    }
                                    if (GlobalFunctionMaCun.giaoxu11 != 0)
                                    {
                                        if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu11)))
                                        {
                                            var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu11);
                                            await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu11, "ten");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            }
                                            else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            GlobalFunctionMaCun.giaoxu11 = 0;
                                        }
                                        else GlobalFunctionMaCun.giaoxu11 = 0;
                                    }
                                    if (GlobalFunctionMaCun.giaoxu12 != 0)
                                    {
                                        if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu12)))
                                        {
                                            var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu12);
                                            await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu12, "ten");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            }
                                            else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            GlobalFunctionMaCun.giaoxu12 = 0;
                                        }
                                        else GlobalFunctionMaCun.giaoxu12 = 0;
                                    }
                                    if (GlobalFunctionMaCun.giaoxu13 != 0)
                                    {
                                        if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu13)))
                                        {
                                            var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu13);
                                            await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu13, "ten");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            }
                                            else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            GlobalFunctionMaCun.giaoxu13 = 0;
                                        }
                                        else GlobalFunctionMaCun.giaoxu13 = 0;
                                    }
                                    if (GlobalFunctionMaCun.giaoxu14 != 0)
                                    {
                                        if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu14)))
                                        {
                                            var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu14);
                                            await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu14, "ten");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            }
                                            else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            GlobalFunctionMaCun.giaoxu14 = 0;
                                        }
                                        else GlobalFunctionMaCun.giaoxu14 = 0;
                                    }
                                    if (GlobalFunctionMaCun.giaoxu15 != 0)
                                    {
                                        if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu15)))
                                        {
                                            var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu15);
                                            await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu15, "ten");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            }
                                            else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            GlobalFunctionMaCun.giaoxu15 = 0;
                                        }
                                        else GlobalFunctionMaCun.giaoxu15 = 0;
                                    }
                                    if (GlobalFunctionMaCun.giaoxu16 != 0)
                                    {
                                        if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu16)))
                                        {
                                            var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu16);
                                            await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu16, "ten");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            }
                                            else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            GlobalFunctionMaCun.giaoxu16 = 0;
                                        }
                                        else GlobalFunctionMaCun.giaoxu16 = 0;
                                    }
                                    if (GlobalFunctionMaCun.giaoxu17 != 0)
                                    {
                                        if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.GetUser(GlobalFunctionMaCun.giaoxu17)))
                                        {
                                            var user123 = Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.giaoxu17);
                                            await user123.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            await user123.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            if (GlobalFunctionMaCun.deadroles == 1)
                                            {
                                                GlobalFunctionMaCun.rolesid(GlobalFunctionMaCun.giaoxu17, "ten");
                                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " (" + GlobalFunctionMaCun.nameroles + ") chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            }
                                            else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Thành Viên Giáo Xứ " + user123.Nickname + " chết cùng Đức Cha và trở thành Thần Tiên biết Bay...!");
                                            GlobalFunctionMaCun.giaoxu17 = 0;
                                        }
                                        else GlobalFunctionMaCun.giaoxu17 = 0;
                                    }
                                }
                                else
                                {
                                    if (GlobalFunctionMaCun.deadroles == 1)
                                    {
                                        await GlobalFunctionMaCun.rolesid(user.Id, "ten");
                                        await GlobalFunctionMaCun.rolesid(user.Id, "idrole");
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + Context.Guild.GetUser(GlobalFunctionMaCun.idroles).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                    }
                                    else await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + user.Nickname + ".");
                                    await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                    await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                    GlobalFunctionMaCun.phedan--;
                                }

                                OverwritePermissions chophepsoi = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Allow, readMessageHistory: PermValue.Allow);

                                if (GlobalFunctionMaCun.caubehoangda != 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    if (!Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.caubehoangda)) & GlobalFunctionMaCun.caubesoi == 0)
                                    {
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).AddPermissionOverwriteAsync(Context.Guild.GetUser(GlobalFunctionMaCun.plr17) as IGuildUser, chophepsoi.Modify());
                                        await Context.Client.GetGuild(580555457983152149).GetTextChannel(589462982275235860).SendMessageAsync("Thủ Lĩnh của bạn đã chết, từ giờ bạn sẽ thuộc về phe Ma Sói.\nSử dụng -can (Số Người Chơi Muốn Cắn) để cắn người chơi\n<#580564753982816256> để xem những người cùng phe sói.");
                                        GlobalFunctionMaCun.phesoi++;
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.caubesoi = 1;
                                        GlobalFunctionMaCun.caubehoangda = 0;
                                    }
                                    else
                                    {
                                        return;
                                    }
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
                            if (Context.User.Id != GlobalFunctionMaCun.dongbang || Context.User.Id != GlobalFunctionMaCun.moi)
                            {
                                if (user.Id == Context.User.Id)
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
                                embed.AddField($"Lỗi!", "Bạn không thể dùng thuốc cứu khi bị Đóng Băng hoặc bị Gái Điếm mời.");
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
                            else if (GlobalFunctionMaCun.chucnangdongbang == 1)
                            {
                                embed.AddField($"Lỗi!", "Bạn không thể đóng băng khi đã từ bỏ chức năng của mình.");
                                embed.WithColor(new Discord.Color(255, 0, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                            if (GlobalFunctionMaCun.lastdongbang == GlobalFunctionMaCun.dongbang)
                            {
                                embed.AddField($"Lỗi!", "Bạn không thể đóng băng 1 người 2 ngày.");
                                embed.WithColor(new Discord.Color(255, 0, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                            else if (user.Id == GlobalFunctionMaCun.plr4 || user.Id == GlobalFunctionMaCun.plr6 || user.Id == GlobalFunctionMaCun.plr16 || user.Id == GlobalFunctionMaCun.plr17 & GlobalFunctionMaCun.caubesoi == 1)
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
                            if (Context.User.Id != GlobalFunctionMaCun.dongbang || Context.User.Id != GlobalFunctionMaCun.moi)
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
                                embed.AddField($"Lỗi!", "Bạn không thể đâm người khác khi bị Đóng Băng hoặc bị Gái Điếm mời.");
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
        [Command("moi")]
        public async Task gaidiemmoi(IGuildUser user = null)
        {
            if (Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 583828253681254400)
            {
                var embed = new EmbedBuilder();
                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User))
                {
                    if (user == null)
                    {
                        embed.AddField($"Hệ Thống!", "-moi (Số Người Chơi Muốn Mời Vào Đêm Nay)");
                        embed.WithColor(new Discord.Color(255, 50, 255));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))
                    {
                        if (GlobalFunctionMaCun.gamestatus != 1)
                        {
                            if (user.Id == Context.User.Id)
                            {
                                embed.AddField($"Lỗi!", "Bạn không thể sử dụng lệnh này lên bản thân.");
                                embed.WithColor(new Discord.Color(255, 0, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                            if (GlobalFunctionMaCun.lastmoi == GlobalFunctionMaCun.moi)
                            {
                                embed.AddField($"Lỗi!", "Bạn không thể mời 1 người 2 ngày.");
                                embed.WithColor(new Discord.Color(255, 0, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                            else
                            {
                                GlobalFunctionMaCun.moi = user.Id;
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
        [Command("hoisinh")]
        public async Task donghoisinh(IGuildUser user = null)
        {
            if (Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 583828359394492427)
            {
                var embed = new EmbedBuilder();
                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User))
                {
                    if (user == null)
                    {
                        embed.AddField($"Hệ Thống!", "-hoisinh (Số Người Chơi Muốn Hồi Sinh)");
                        embed.WithColor(new Discord.Color(255, 50, 255));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết").Members.Contains(user))
                    {
                        if (GlobalFunctionMaCun.gamestatus == 1)
                        {
                            if (user.Id == Context.User.Id)
                            {
                                embed.AddField($"Lỗi!", "Bạn không thể sử dụng lệnh này lên bản thân.");
                                embed.WithColor(new Discord.Color(255, 0, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                            else if (GlobalFunctionMaCun.luothoisinh >= 1)
                            {
                                GlobalFunctionMaCun.hoisinh = user.Id;
                                await Context.Message.AddReactionAsync(Emote.Parse("<:dachon:580977712689053696>"));
                            }
                            else
                            {
                                embed.AddField($"Lỗi!", "Bạn đã hết lượt hồi sinh.");
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
                        embed.AddField($"Lỗi!", "Người Chơi đó không tham gia còn sống.");
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
        [Command("tubochucnang")]
        public async Task removepermofmyself()
        {
            if (Context.Guild.Id == 580555457983152149)
            {
                var embed = new EmbedBuilder();
                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User))
                {
                    if (GlobalFunctionMaCun.gamestatus == 1)
                    {
                        if (Context.Channel.Id == 580574522361774081)
                        {
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã từ bỏ chức năng của mình để có thể cắn người chơi.");
                            GlobalFunctionMaCun.chucnangphuphep = 1;
                        }
                        else if (Context.Channel.Id == 580574634811064342)
                        {
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã từ bỏ chức năng của mình để có thể cắn người chơi.");
                            GlobalFunctionMaCun.chucnangdongbang = 1;
                        }
                        else if (Context.Channel.Id == 583828385659355147)
                        {
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã từ bỏ chức năng của mình để có thể cắn người chơi.");
                            GlobalFunctionMaCun.chucnangsoi = 1;
                        }
                        else return;
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
                    embed.AddField($"Lỗi!", "Bạn không thể sử dụng khi đã chết.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
            }
            else return;
        }

        [Command("thulinh")]
        public async Task thulinhcuacaubehoangda(IGuildUser user = null)
        {
            if (Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 589462982275235860)
            {
                var embed = new EmbedBuilder();
                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User))
                {
                    if (user == null)
                    {
                        embed.AddField($"Hệ Thống!", "-thulinh (Số Người Chơi Muốn Cho Làm Thủ Lĩnh");
                        embed.WithColor(new Discord.Color(255, 50, 255));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))
                    {
                        if (GlobalFunctionMaCun.gamestatus == 1)
                        {
                            if (Context.User.Id != GlobalFunctionMaCun.dongbang || Context.User.Id != GlobalFunctionMaCun.moi)
                            {
                                if (user.Id == Context.User.Id)
                                {
                                    embed.AddField($"Lỗi!", "Bạn không thể sử dụng lệnh này lên bản thân.");
                                    embed.WithColor(new Discord.Color(255, 0, 0));
                                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                                }
                                else if (GlobalFunctionMaCun.caubesoi == 0)
                                {
                                    GlobalFunctionMaCun.caubehoangda = user.Id;
                                    await Context.Message.AddReactionAsync(Emote.Parse("<:dachon:580977712689053696>"));
                                }
                                else
                                {
                                    embed.AddField($"Lỗi!", "Bạn đã thành sói nên bạn không thể chọn thủ lĩnh.");
                                    embed.WithColor(new Discord.Color(255, 0, 0));
                                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                                }
                            }
                            else
                            {
                                embed.AddField($"Lỗi!", "Bạn không thể chọn thủ lĩnh khi bị Đóng Băng hoặc bị Gái Điếm mời.");
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



        [Command("chon")]
        public async Task thanhviengiaoxu(IGuildUser user = null)
        {
            if (Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 589463015212974080)
            {
                var embed = new EmbedBuilder();
                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User))
                {
                    if (user == null)
                    {
                        embed.AddField($"Hệ Thống!", "-chon (Số Người Chơi Muốn Cho Làm Thành Viên Giáo Xứ");
                        embed.WithColor(new Discord.Color(255, 50, 255));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))
                    {
                        if (GlobalFunctionMaCun.gamestatus == 1)
                        {
                            if (Context.User.Id != GlobalFunctionMaCun.dongbang || Context.User.Id != GlobalFunctionMaCun.moi)
                            {
                                if (user.Id == Context.User.Id)
                                {
                                    embed.AddField($"Lỗi!", "Bạn không thể sử dụng lệnh này lên bản thân.");
                                    embed.WithColor(new Discord.Color(255, 0, 0));
                                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                                }
                                else
                                {
                                    await GlobalFunctionGame.rolegiaoxu(user.Id);
                                    if (GlobalFunctionMaCun.checkgiaoxu == 0)
                                    {
                                        GlobalFunctionMaCun.giaoxu = user.Id;
                                        await Context.Message.AddReactionAsync(Emote.Parse("<:dachon:580977712689053696>"));
                                    }
                                    else
                                    {
                                        embed.AddField($"Lỗi!", "Người Chơi đó đã có trong Thành Viên Giáo Xứ.");
                                        embed.WithColor(new Discord.Color(255, 0, 0));
                                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                                    }
                                }
                            }
                            else
                            {
                                embed.AddField($"Lỗi!", "Bạn không thể chọn người chơi vào thành đường khi bị Đóng Băng hoặc bị Gái Điếm mời.");
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
            else if (text == "player")
            {
                int checkplayer = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Count();
                GlobalFunctionMaCun.plr = checkplayer;
            }
            else if (text == "camnoi")
            {
                OverwritePermissions khongchophep2 = new OverwritePermissions(viewChannel: PermValue.Allow, connect: PermValue.Allow, speak: PermValue.Deny);
                await Context.Client.GetGuild(580555457983152149).GetVoiceChannel(580566264171331597).AddPermissionOverwriteAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"), khongchophep2.Modify());
            }
            else if (text == "dcnoi")
            {
                OverwritePermissions chophep2 = new OverwritePermissions(viewChannel: PermValue.Allow, connect: PermValue.Allow, speak: PermValue.Allow);
                await Context.Client.GetGuild(580555457983152149).GetVoiceChannel(580566264171331597).AddPermissionOverwriteAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"), chophep2.Modify());
            }

            else return;
        }
    }
}
