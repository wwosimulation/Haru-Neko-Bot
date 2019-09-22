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

        [Command("bophieu")]
        public async Task bophieunguoichoi(IGuildUser user = null)
        {
            if (Context.Guild.Id == 580555457983152149)
            {
                var embed = new EmbedBuilder();
                if (Context.Channel.Id != 580563096544739331 || Context.Channel.Id != 580564753982816256)
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
                    else if (GlobalFunctionMaCun.gamestatus >= 4 || GlobalFunctionMaCun.treo != 0 & GlobalFunctionMaCun.gamestatus == 3)
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
                        string nickname = user.Nickname;
                        int num = Int32.Parse(nickname);
                        await GlobalFunctionGame.voting(Context.User.Id, num);
                        await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu treo " + Context.Guild.GetUser(user.Id).Nickname + "");
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
                            if (Context.User.Id != GlobalFunctionMaCun.dongbang & Context.User.Id != GlobalFunctionMaCun.moi)
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
                            if (Context.User.Id == GlobalFunctionMaCun.dongbang)
                            {
                                embed.AddField($"Lỗi!", "Bạn không thể dùng lệnh khi bị Đóng Băng.");
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
                                            embed.AddField($"Hệ Thống!", "Người chơi số " + user.Nickname + " là " + GlobalFunctionMaCun.nameroles + ".");
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
                                else if (Context.Channel.Id == 583828385659355147 & Context.User.Id != GlobalFunctionMaCun.dongbang & Context.User.Id != GlobalFunctionMaCun.moi || Context.Channel.Id == 583828385659355147 & Context.User.Id != GlobalFunctionMaCun.moi)
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
                                            embed.AddField($"Hệ Thống!", "Vai Trò của người chơi số " + user.Nickname + " là " + GlobalFunctionMaCun.nameroles + ".");
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
        [Command("kiemtra")]
        public async Task detectivecheck(IGuildUser user = null, IGuildUser user2 = null)
        {
            if (Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 591478522111983616)
            {
                var embed = new EmbedBuilder();
                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User))
                {
                    if (user == null)
                    {
                        embed.AddField($"Hệ Thống!", "-kiemtra (Số Người Chơi 1) (Số Người Chơi 2)");
                        embed.WithColor(new Discord.Color(255, 50, 255));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (user2 == null)
                    {
                        embed.AddField($"Hệ Thống!", "-kiemtra (Số Người Chơi 1) (Số Người Chơi 2)");
                        embed.WithColor(new Discord.Color(255, 50, 255));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user) & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user2))
                    {
                        if (GlobalFunctionMaCun.gamestatus == 1)
                        {
                            if (GlobalFunctionMaCun.thamtu == 0)
                            {
                                embed.AddField($"Lỗi!", "Bạn không thể kiểm tra 1 đêm 2 lần.");
                                embed.WithColor(new Discord.Color(255, 0, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                            else if (Context.User.Id == GlobalFunctionMaCun.dongbang)
                            {
                                embed.AddField($"Lỗi!", "Bạn không thể dùng lệnh khi bị Đóng Băng.");
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
                            else if (user2.Id == Context.User.Id)
                            {
                                embed.AddField($"Lỗi!", "Bạn không thể kiểm tra bản thân.");
                                embed.WithColor(new Discord.Color(255, 0, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                            else
                            {
                                string check1 = null;
                                string check2 = null;

                                await GlobalFunctionMaCun.rolesid(user.Id, "phuphep");
                                if (GlobalFunctionMaCun.checkphuphep == 0)
                                {
                                    await GlobalFunctionMaCun.rolesid(user.Id, "aura");
                                    check1 = GlobalFunctionMaCun.nameroles;
                                }
                                else check1 = "Ác";

                                await GlobalFunctionMaCun.rolesid(user2.Id, "phuphep");
                                if (GlobalFunctionMaCun.checkphuphep == 0)
                                {
                                    await GlobalFunctionMaCun.rolesid(user2.Id, "aura");
                                    check2 = GlobalFunctionMaCun.nameroles;
                                }
                                else check2 = "Ác";

                                if (check1 == check2)
                                {
                                    embed.AddField($"Hệ Thống!", "Người Chơi " + user.Nickname + " và " + user2.Nickname + " **Cùng Phe**.");
                                    embed.WithColor(new Discord.Color(0, 255, 0));
                                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                                }
                                else
                                {
                                    embed.AddField($"Hệ Thống!", "Người Chơi " + user.Nickname + " và " + user2.Nickname + " **Khác Phe**.");
                                    embed.WithColor(new Discord.Color(0, 255, 0));
                                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                                }

                                GlobalFunctionMaCun.thamtu--;
                            }

                        }
                        else
                        {
                            embed.AddField($"Lỗi!", "Lệnh này chỉ có thể sử dụng vào đêm.");
                            embed.WithColor(new Discord.Color(255, 0, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                    }
                    else
                    {
                        embed.AddField($"Lỗi!", "Bạn không thể kiểm tra người chơi không tham gia trận hoặc đã chết.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                }
            }
            else return;
        }
        [Command("can")]
        public async Task werewolfkill(IGuildUser user = null)
        {
            if (Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 580574451834290176 || Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 580574522361774081 || Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 580574634811064342 || Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 583828385659355147 || Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 589462982275235860 || Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 591478711774085143)
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
                                if (Context.Channel.Id == 580574451834290176 || Context.Channel.Id == 591478711774085143)
                                {
                                    if (user.Id == GlobalFunctionMaCun.plr6 || user.Id == GlobalFunctionMaCun.plr11 || user.Id == GlobalFunctionMaCun.plr16 || user.Id == GlobalFunctionMaCun.plr17 & GlobalFunctionMaCun.caubesoi == 1 || user.Id == GlobalFunctionMaCun.plr21 || user.Id == GlobalFunctionMaCun.plr4)
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
                                    if (user.Id == GlobalFunctionMaCun.plr4 || user.Id == GlobalFunctionMaCun.plr11 || user.Id == GlobalFunctionMaCun.plr16 || user.Id == GlobalFunctionMaCun.plr17 & GlobalFunctionMaCun.caubesoi == 1 || user.Id == GlobalFunctionMaCun.plr21)
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
                                    if (user.Id == GlobalFunctionMaCun.plr4 || user.Id == GlobalFunctionMaCun.plr6 || user.Id == GlobalFunctionMaCun.plr16 || user.Id == GlobalFunctionMaCun.plr17 & GlobalFunctionMaCun.caubesoi == 1 || user.Id == GlobalFunctionMaCun.plr21)
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
                                    if (user.Id == GlobalFunctionMaCun.plr4 || user.Id == GlobalFunctionMaCun.plr6 || user.Id == GlobalFunctionMaCun.plr11 || user.Id == GlobalFunctionMaCun.plr17 & GlobalFunctionMaCun.caubesoi == 1 || user.Id == GlobalFunctionMaCun.plr21)
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
                                    if (user.Id == GlobalFunctionMaCun.plr6 || user.Id == GlobalFunctionMaCun.plr11 || user.Id == GlobalFunctionMaCun.plr16 || user.Id == GlobalFunctionMaCun.plr16 || user.Id == GlobalFunctionMaCun.plr21)
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
                        if (Context.User.Id == GlobalFunctionMaCun.dongbang & Context.User.Id == GlobalFunctionMaCun.moi)
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
                OverwritePermissions chophepsoi = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Allow, readMessageHistory: PermValue.Allow);
                var embed = new EmbedBuilder();
                var song = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống");
                var chet = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết");
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
                        if (Context.User.Id == GlobalFunctionMaCun.dongbang & Context.User.Id == GlobalFunctionMaCun.moi)
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
                            if (GlobalFunctionMaCun.deadroles == 1)
                            {
                                await GlobalFunctionMaCun.rolesid(user.Id, "ten");
                                await GlobalFunctionMaCun.rolesid(user.Id, "idrole");
                                await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Phù Thùy đã đầu độc và giết chết " + Context.Guild.GetUser(user.Id).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                            }
                            else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Phù Thùy đã đầu độc và giết chết " + Context.Guild.GetUser(user.Id).Nickname + ".");
                            if (GlobalFunctionMaCun.caubehoangda == user.Id & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                            {
                                GlobalFunctionMaCun.Muctieucaubehoangda = 1;
                            }
                            if (user.Id == GlobalFunctionMaCun.plr7) GlobalFunctionMaCun.Thosanchet = 1;
                            if (user.Id == GlobalFunctionMaCun.plr18) GlobalFunctionMaCun.Giaoxuchet = 1;
                            if (user.Id == GlobalFunctionMaCun.plr4 || user.Id == GlobalFunctionMaCun.plr6 || user.Id == GlobalFunctionMaCun.plr11 || user.Id == GlobalFunctionMaCun.plr16 || user.Id == GlobalFunctionMaCun.plr21) GlobalFunctionMaCun.phesoi--;
                            else GlobalFunctionMaCun.phedan--;

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
                OverwritePermissions chophepsoi = new OverwritePermissions(viewChannel: PermValue.Allow, sendMessages: PermValue.Allow, readMessageHistory: PermValue.Allow);
                var embed = new EmbedBuilder();
                var song = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống");
                var chet = Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết");
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
                                if (GlobalFunctionMaCun.deadroles == 1)
                                {
                                    await GlobalFunctionMaCun.rolesid(user.Id, "ten");
                                    await GlobalFunctionMaCun.rolesid(user.Id, "idrole");
                                    await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + Context.Guild.GetUser(user.Id).Nickname + " (" + GlobalFunctionMaCun.nameroles + ").");
                                }
                                else await Context.Client.GetGuild(580555457983152149).GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + Context.Guild.GetUser(user.Id).Nickname + ".");
                                if (GlobalFunctionMaCun.caubehoangda == user.Id & GlobalFunctionMaCun.caubesoi == 0 & Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.plr17)))
                                {
                                    GlobalFunctionMaCun.Muctieucaubehoangda = 1;
                                }
                                if (user.Id == GlobalFunctionMaCun.plr7) GlobalFunctionMaCun.Thosanchet = 1;
                                if (user.Id == GlobalFunctionMaCun.plr18) GlobalFunctionMaCun.Giaoxuchet = 1;
                                if (user.Id == GlobalFunctionMaCun.plr4 || user.Id == GlobalFunctionMaCun.plr6 || user.Id == GlobalFunctionMaCun.plr11 || user.Id == GlobalFunctionMaCun.plr16 || user.Id == GlobalFunctionMaCun.plr21) GlobalFunctionMaCun.phesoi--;
                                else GlobalFunctionMaCun.phedan--;

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
                            if (Context.User.Id != GlobalFunctionMaCun.dongbang & Context.User.Id != GlobalFunctionMaCun.moi)
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
                            if (Context.User.Id != GlobalFunctionMaCun.dongbang & Context.User.Id != GlobalFunctionMaCun.moi)
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
                            if (Context.User.Id != GlobalFunctionMaCun.dongbang & Context.User.Id != GlobalFunctionMaCun.moi)
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
                            if (Context.User.Id != GlobalFunctionMaCun.dongbang & Context.User.Id != GlobalFunctionMaCun.moi)
                            {
                                if (user.Id == Context.User.Id)
                                {
                                    embed.AddField($"Lỗi!", "Bạn không thể sử dụng lệnh này lên bản thân.");
                                    embed.WithColor(new Discord.Color(255, 0, 0));
                                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                                }
                                else
                                {
                                    var b = "yes";
                                    if (GlobalFunctionMaCun.giaoxuplr != null)
                                    {
                                        string name = GlobalFunctionMaCun.giaoxuplr;
                                        string[] name2 = name.Split(" ");
                                        foreach (var name3 in name2)
                                        {
                                            var getuser = Context.Guild.Users.FirstOrDefault(x => x.Nickname == name3 + "");
                                            if (getuser != null)
                                            {
                                                var getuser2 = Context.Guild.Users.FirstOrDefault(x => x.Nickname == name3 + "").Id;
                                                if (user.Id == getuser2)
                                                {
                                                    b = "no";
                                                }
                                            }
                                        }
                                    }
                                    if (b == "yes")
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

        [Command("nuoi")]
        public async Task nguoimeonconconnuuoi(IGuildUser user = null)
        {
            if (Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 591478522111983616)
            {
                var embed = new EmbedBuilder();
                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User))
                {
                    if (user == null)
                    {
                        embed.AddField($"Hệ Thống!", "-nuoi (Số Người Chơi Muốn Cho Làm Con Nuôi)");
                        embed.WithColor(new Discord.Color(255, 50, 255));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))
                    {
                        if (GlobalFunctionMaCun.gamestatus == 1)
                        {
                            if (Context.User.Id != GlobalFunctionMaCun.dongbang & Context.User.Id != GlobalFunctionMaCun.moi)
                            {
                                if (user.Id == Context.User.Id)
                                {
                                    embed.AddField($"Lỗi!", "Bạn không thể sử dụng lệnh này lên bản thân.");
                                    embed.WithColor(new Discord.Color(255, 0, 0));
                                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                                }
                                else if (GlobalFunctionMaCun.luothoisinh <= 0)
                                {
                                    embed.AddField($"Lỗi!", "Bạn đã chọn con nuôi rồi nên bạn không thể chọn người khác.");
                                    embed.WithColor(new Discord.Color(255, 0, 0));
                                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                                }
                                else
                                {
                                    GlobalFunctionMaCun.nuoi = user.Id;
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

        [Command("canchet")]
        public async Task soidaohoacanchet(IGuildUser user = null)
        {
            if (Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 591478711774085143)
            {
                var embed = new EmbedBuilder();
                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User))
                {
                    if (user == null)
                    {
                        embed.AddField($"Hệ Thống!", "-canchet (Số Người Chơi Muốn Cắn)");
                        embed.WithColor(new Discord.Color(255, 50, 255));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))
                    {
                        if (GlobalFunctionMaCun.gamestatus == 1)
                        {
                            if (Context.User.Id != GlobalFunctionMaCun.moi)
                            {
                                if (user.Id == Context.User.Id)
                                {
                                    embed.AddField($"Lỗi!", "Bạn không thể sử dụng lệnh này lên bản thân.");
                                    embed.WithColor(new Discord.Color(255, 0, 0));
                                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                                }
                                else
                                {
                                    GlobalFunctionMaCun.canchet = user.Id;
                                    await Context.Message.AddReactionAsync(Emote.Parse("<:dachon:580977712689053696>"));
                                }
                            }
                            else
                            {
                                embed.AddField($"Lỗi!", "Bạn không thể cắn khi bị Gái Điếm mời.");
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

        [Command("pha")]
        public async Task breakvote(IGuildUser user = null)
        {
            if (Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 591478642966528020)
            {
                var embed = new EmbedBuilder();
                if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User))
                {
                    if (user == null)
                    {
                        embed.AddField($"Hệ Thống!", "-pha (Số Người Chơi Muốn Phá Phiếu)");
                        embed.WithColor(new Discord.Color(255, 50, 255));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(user))
                    {
                        if (GlobalFunctionMaCun.gamestatus == 3)
                        {
                            if (user.Id == Context.User.Id)
                            {
                                embed.AddField($"Lỗi!", "Bạn không thể sử dụng lệnh này lên bản thân.");
                                embed.WithColor(new Discord.Color(255, 0, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                            else if (GlobalFunctionMaCun.pha != 1)
                            {
                                embed.AddField($"Lỗi!", "Bạn chỉ được phá phiếu 1 lần 1 người mỗi ngày.");
                                embed.WithColor(new Discord.Color(255, 0, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                            else
                            {
                                await GlobalFunctionGame.voting(user.Id, 0);
                                GlobalFunctionMaCun.pha = user.Id;
                                await Context.Message.AddReactionAsync(Emote.Parse("<:dachon:580977712689053696>"));
                            }
                        }
                        else
                        {
                            embed.AddField($"Lỗi!", "Lệnh này chỉ có thể sử dụng vào lúc bỏ phiếu.");
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
