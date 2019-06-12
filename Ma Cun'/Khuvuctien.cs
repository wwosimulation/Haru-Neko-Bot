using Discord;
using Discord.Commands;
using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Drawing;
using Neko_Test.Core.UserAccounts;
using System.Globalization;
using System.Windows.Forms;
using Newtonsoft.Json;

using Discord.WebSocket;
using System.Diagnostics;

namespace Neko_Test.Ma_Cun_
{
    public class Khuvuctien : ModuleBase<SocketCommandContext>
    {
        [Command("themtien")]
        public async Task addmoneyforplayer(SocketUser user = null, ulong number = 0)
        {
            if (Context.Guild.Id == 580555457983152149 || Context.Guild.Id == 530689610313891840)
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
                    embed.AddField($"Lỗi!", "Bạn chưa chọn Người Chơi để đưa tiền.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (number <= 0)
                {
                    embed.AddField($"Lỗi!", "Bạn không thể đưa tiền dưới hoặc bằng 0 xu.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else//546314357991145502 - GN Logs Ma Cún
                {
                    var accounts = UserAccounts.GetAccount(user);
                    accounts.points = accounts.points + number;
                    embed.AddField($"Hệ Thống!", $"Đã thêm {number}{Emote.Parse("<:coin:584231931835580419>")} cho {user.Username}, giờ {user.Username} có {accounts.points}{Emote.Parse("<:coin:584231931835580419>")}.");
                    embed.WithColor(new Discord.Color(0, 255, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    await Context.Client.GetGuild(530689610313891840).GetTextChannel(546314357991145502).SendMessageAsync(Context.User.Username + " Used: -themtien " + user.Username + " [" + number + "].");
                    UserAccounts.SaveAccounts();
                }
            }
            else return;
        }
        [Command("xoatien")]
        public async Task removemoneyforplayer(SocketUser user = null, ulong number = 0)
        {
            if (Context.Guild.Id == 580555457983152149 || Context.Guild.Id == 530689610313891840)
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
                    embed.AddField($"Lỗi!", "Bạn chưa chọn Người Chơi để xóa tiền.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (number < 0)
                {
                    embed.AddField($"Lỗi!", "Bạn không thể xóa tiền dưới hoặc bằng 0 xu.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else//546314357991145502
                {
                    var accounts = UserAccounts.GetAccount(user);
                    accounts.points = accounts.points - number;
                    embed.AddField($"Hệ Thống!", $"Đã xóa {number}{Emote.Parse("<:coin:584231931835580419>")} của {user.Username}, giờ {user.Username} có {accounts.points}{Emote.Parse("<:coin:584231931835580419>")}.");
                    embed.WithColor(new Discord.Color(0, 255, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    await Context.Client.GetGuild(530689610313891840).GetTextChannel(546314357991145502).SendMessageAsync(Context.User.Username + " Used: -xoatien " + user.Username + " [" + number + "].");
                    UserAccounts.SaveAccounts();
                }
            }
            else return;
        }
        [Command("tien")]
        [Alias("tuido")]
        public async Task inventory([Optional]SocketUser user)
        {
            if (Context.Guild.Id == 580555457983152149 || Context.Guild.Id == 530689610313891840)
            {
                var embed = new EmbedBuilder();
                if (user == null || user == (Context.User as SocketUser))
                {
                    var accounts = UserAccounts.GetAccount(Context.User);
                    embed.AddField($"Túi đồ của bạn!", $"Tổng xu có: {accounts.points}{Emote.Parse("<:coin:584231931835580419>")}.\nHoa có: {accounts.roses}{Emote.Parse("<:rose:584250710284304384>")}.\nHoa được tặng: {accounts.plrroses}{Emote.Parse("<:rose:584250710284304384>")}.");
                    embed.WithColor(new Discord.Color(0, 255, 255));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    var accounts = UserAccounts.GetAccount(user);
                    embed.AddField($"Túi đồ của {user.Username}!", $"Tổng xu có: {accounts.points}{Emote.Parse("<:coin:584231931835580419>")}.\nHoa có: {accounts.roses}{Emote.Parse("<:rose:584250710284304384>")}.\nHoa được tặng: {accounts.plrroses}{Emote.Parse("<:rose:584250710284304384>")}.");
                    embed.WithColor(new Discord.Color(0, 255, 255));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                UserAccounts.SaveAccounts();
            }
            else
            {
                var embed = new EmbedBuilder();
                if (user == null || user == (Context.User as SocketUser))
                {
                    var accounts = UserAccounts.GetAccount(Context.User);
                    embed.AddField($"Your Inventory!", $"Coins: {accounts.points}{Emote.Parse("<:coin:584231931835580419>")}.\nRoses: {accounts.roses}{Emote.Parse("<:rose:584250710284304384>")}.\nPlayer Given Roses: {accounts.plrroses}{Emote.Parse("<:rose:584250710284304384>")}.");
                    embed.WithColor(new Discord.Color(0, 255, 255));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    var accounts = UserAccounts.GetAccount(user);
                    embed.AddField($"Inventory of {user.Username}!", $"Coins: {accounts.points}{Emote.Parse("<:coin:584231931835580419>")}.\nnRoses: {accounts.roses}{Emote.Parse("<:rose:584250710284304384>")}.\nPlayer Given Roses: {accounts.plrroses}{Emote.Parse("<:rose:584250710284304384>")}.");
                    embed.WithColor(new Discord.Color(0, 255, 255));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                UserAccounts.SaveAccounts();
            }
        }
        [Command("Daily")]
        [Alias("hangngay", "thuongngay")]
        public async Task daily()
        {
            if (Context.Guild.Id == 530689610313891840)
            {
                var embed = new EmbedBuilder();
                if (DataStorage.paircount(Context.User + DateTime.Now.ToLongDateString()) == true)
                {
                    DataStorage.addpairs(Context.User + DateTime.Now.ToLongDateString(), "Oof");
                    var accounts = UserAccounts.GetAccount(Context.User);
                    ulong daily = 10;
                    accounts.points = accounts.points + daily;
                    embed.AddField($"Daily!", $"Chào mừng {Context.User.Username} đã quay trở lại, bạn được thưởng {daily}{Emote.Parse("<:coin:584231931835580419>")}.");
                    embed.WithColor(new Discord.Color(0, 255, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    if (60 - DateTime.Now.Minute == 60 || 60 - DateTime.Now.Minute == 0)
                    {
                        embed.AddField($"Lỗi!", $"Bạn đã nhận thưởng ngày hôm nay, hãy quay lại sau {23 - DateTime.Now.Hour} giờ.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (24 - DateTime.Now.Hour != 0 || 24 - DateTime.Now.Hour != 24 || 60 - DateTime.Now.Minute != 60 || 60 - DateTime.Now.Minute != 0)
                    {
                        embed.AddField($"Lỗi!", $"Bạn đã nhận thưởng ngày hôm nay, hãy quay lại sau {23 - DateTime.Now.Hour} giờ {60 - DateTime.Now.Minute} phút.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                }
                UserAccounts.SaveAccounts();
            }
            else return;
            /*{
                var embed = new EmbedBuilder();
                if (DataStorage.paircount(Context.User + DateTime.Now.ToLongDateString()) == true)
                {
                    DataStorage.addpairs(Context.User + DateTime.Now.ToLongDateString(), "Oof");
                    var accounts = UserAccounts.GetAccount(Context.User);
                    ulong daily = 10;
                    accounts.points = accounts.points + daily;
                    embed.AddField($"Daily!", $"Welcome Back {Context.User.Username}, You get {daily}{Emote.Parse("<:coin:584231931835580419>")} for daily.");
                    embed.WithColor(new Discord.Color(0, 255, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    if (60 - DateTime.Now.Minute == 60 || 60 - DateTime.Now.Minute == 0)
                    {
                        embed.AddField($"Error!", $"You already got your daily, come back later in {23 - DateTime.Now.Hour} hours.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    else if (24 - DateTime.Now.Hour != 0 || 24 - DateTime.Now.Hour != 24 || 60 - DateTime.Now.Minute != 60 || 60 - DateTime.Now.Minute != 0)
                    {
                        embed.AddField($"Error!", $"You already got your daily, come back later in {23 - DateTime.Now.Hour} Hours {60 - DateTime.Now.Minute} Minutes.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                }
                UserAccounts.SaveAccounts();
            }*/
        }
        [Command("chotien")]
        public async Task givemoneyforplayer(SocketUser user = null, ulong number = 0)
        {
            if (Context.Guild.Id == 580555457983152149 || Context.Guild.Id == 530689610313891840)
            {
                var embed = new EmbedBuilder();
                if (user == null)
                {
                    embed.AddField($"Lỗi!", "Bạn chưa chọn Người Chơi để cho tiền.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (user == (Context.User as SocketUser))
                {
                    embed.AddField($"Lỗi!", "Bạn sử dụng lệnh này lên bản thân.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (number <= 0)
                {
                    embed.AddField($"Lỗi!", "Bạn không thể cho tiền dưới hoặc bằng 0 xu.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else//546314357991145502
                {
                    var accounts = UserAccounts.GetAccount(Context.User);
                    var plraccounts = UserAccounts.GetAccount(user);
                    if (accounts.points >= number)
                    {
                        accounts.points = accounts.points - number;
                        plraccounts.points = plraccounts.points + number;
                        embed.AddField($"Hệ Thống!", $"Đã đưa {number}{Emote.Parse("<:coin:584231931835580419>")} cho {user.Username}, giờ:\nBạn có: {accounts.points}{Emote.Parse("<:coin:584231931835580419>")}.\n{user.Username} có: {plraccounts.points}{Emote.Parse("<:coin:584231931835580419>")}.");
                        embed.WithColor(new Discord.Color(0, 255, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                        await Context.Client.GetGuild(530689610313891840).GetTextChannel(546314357991145502).SendMessageAsync(Context.User.Username + " Used: -chotien " + user.Username + " [" + number + "].");
                    }
                    else
                    {
                        embed.AddField($"Lỗi!", "Bạn không thể cho tiền người khác khi số tiền cho lớn hơn số tiền của bạn hiện tại.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    UserAccounts.SaveAccounts();
                }
            }
            else return;
        }
        [Command("cuahang")]
        [Alias("store", "shop")]
        private async Task shopping([Optional]int num)
        {
            if (Context.Guild.Id == 530689610313891840)
            {
                var embed = new EmbedBuilder();
                if (num > 2)
                {
                    embed.AddField($"Lỗi!", "Cửa Hàng chỉ có 2 trang.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (num < 0)
                {
                    embed.AddField($"Lỗi!", "Số trang không thể thấp hơn 0.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    if (num == 1 || num == 0)
                    {
                        num = 1;
                        embed.WithAuthor($"Cửa Hàng Xu - Ma Sói Online - Ma Cún\n \n");
                        embed.AddField($"DJ role - 200 {Emote.Parse("<:coin:584231931835580419>")}", "Muốn nghe một chút nhạc trong Game Server? mua nó thôi!");
                        embed.AddField($"Hoa - 25 {Emote.Parse("<:coin:475781084584607745>")} / 1 {Emote.Parse("<:rose:584250710284304384>")}", "Tặng hoa cho người chơi? (Sử dụng -tanghoa khi đang choi Ma Sói tại Game Server).");
                        embed.WithFooter($"Yêu cầu bởi {Context.User.Username} - Trang 1/2", Context.User.GetAvatarUrl());

                        embed.WithColor(new Discord.Color(0, 255, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    if (num == 2)
                    {
                        embed.WithAuthor($"Cửa Hàng Hoa - Ma Sói Online - Ma Cún\n \n");
                        embed.WithTitle("Tạm thời trang 2 không có gì nên hãy quay lại sau nhé.");
                        embed.WithFooter($"Yêu cầu bởi {Context.User.Username} - Trang 2/2", Context.User.GetAvatarUrl());

                        embed.WithColor(new Discord.Color(255, 50, 255));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                }
            }
            else return;
        }
        [Command("danhsachtrochoi")]
        [Alias("trochoi", "games")]
        private async Task danhsachtrochoi()
        {
            if (Context.Guild.Id == 530689610313891840)
            {
                var embed = new EmbedBuilder();

                embed.WithAuthor($"Danh Sách Trò Chơi - Ma Sói Online - Ma Cún\n \n");
                embed.AddField("Cò Quay Nga - Russian Roulette", "Bạn sẽ có 1 viên đạn trong súng quay 6 viên, liệu rằng bạn có may mắn sống sót?\n(Chi tiết truy cập: https://vi.wikipedia.org/wiki/Cò_quay_Nga)");
                embed.WithFooter($"Yêu cầu bởi {Context.User.Username} - Trang 1/1", Context.User.GetAvatarUrl());

                embed.WithColor(new Discord.Color(0, 255, 0));
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
            else return;
        }
        [Command("mua")]
        [Alias("buy", "muado")]
        private async Task muadooshop(string item, [Optional]string tenthem, [Optional]ulong soluong)
        {
            if (Context.Guild.Id == 530689610313891840)
            {
                var embed = new EmbedBuilder();
                if (item == null)
                {
                    embed.AddField($"Hệ Thống!", "-mua (Vật Phẩm) [Số lượng nếu là hoa]");

                    embed.WithColor(new Discord.Color(255, 50, 255));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                if (item.ToLower() == "dj")
                {
                    if (tenthem == "role" || tenthem == "Role" || tenthem == null)
                    {
                        var user = UserAccounts.GetAccount(Context.User);
                        if (user.points >= 200)
                        {
                            if (!Context.Guild.Roles.FirstOrDefault(x => x.Name == "DJ").Members.Contains(Context.User))
                            {
                                var role = Context.Guild.Roles.FirstOrDefault(x => x.Name == "DJ");
                                await (Context.User as IGuildUser).AddRoleAsync(role);
                                user.points = user.points - 200;
                                embed.AddField($"Hệ Thống!", $"Đã mua DJ thành công! số dư còn lại của bạn là {user.points}{Emote.Parse("<:coin:584231931835580419>")}.");
                                embed.WithColor(new Discord.Color(0, 255, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                            else
                            {
                                embed.AddField($"Lỗi!", "Bạn có DJ rùi mà, mua gì mua lắm vậy, cháy hàng đó .-.");
                                embed.WithColor(new Discord.Color(255, 0, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                        }
                        else
                        {
                            embed.AddField($"Lỗi!", $"Bạn không đủ tiền để mua DJ.");
                            embed.WithColor(new Discord.Color(255, 0, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                    }
                    else return;
                }
                if (item.ToLower() == "hoa")
                {
                    if (tenthem == "soluong" || tenthem == "Soluong" || tenthem == "SoLuong")
                    {
                        var user = UserAccounts.GetAccount(Context.User);
                        if (soluong <= 0)
                        {
                            embed.AddField($"Hệ Thống!", "-mua hoa soluong [Số lượng hoa muốn mua]");

                            embed.WithColor(new Discord.Color(255, 50, 255));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (soluong == 1 & user.points >= 25)
                        {
                            user.points = user.points - 25;
                            user.roses = user.roses + 1;
                            embed.AddField($"Hệ Thống!", $"Đã mua 1 bông hoa với giá 25{Emote.Parse("<:coin:584231931835580419>")}! số dư còn lại của bạn là {user.points}{Emote.Parse("<:coin:584231931835580419>")}.");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else if (soluong >= 2)
                        {
                            var check = 25 * soluong;
                            if (user.points >= check)
                            {
                                user.points = user.points - check;
                                user.roses = user.roses + soluong;
                                embed.AddField($"Hệ Thống!", $"Đã mua {soluong} bông hoa với giá {check}{Emote.Parse("<:coin:584231931835580419>")}! số dư còn lại của bạn là {user.points}{Emote.Parse("<:coin:584231931835580419>")}.");
                                embed.WithColor(new Discord.Color(0, 255, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                            else
                            {
                                embed.AddField($"Lỗi!", $"Bạn không đủ tiền để mua {soluong} bông hoa ({check}{Emote.Parse("<:coin:584231931835580419>")}).");
                                embed.WithColor(new Discord.Color(255, 0, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                        }
                        else
                        {
                            embed.AddField($"Lỗi!", $"Bạn không đủ tiền để mua 1 bông hoa.");
                            embed.WithColor(new Discord.Color(255, 0, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                    }
                    else
                    {
                        var user = UserAccounts.GetAccount(Context.User);
                        if (user.points >= 25)
                        {
                            user.points = user.points - 25;
                            user.roses = user.roses + 1;
                            embed.AddField($"Hệ Thống!", $"Đã mua 1 bông hoa với giá 25{Emote.Parse("<:coin:584231931835580419>")}! số dư còn lại của bạn là {user.points}{Emote.Parse("<:coin:584231931835580419>")}.");
                            embed.WithColor(new Discord.Color(0, 255, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                        else
                        {
                            embed.AddField($"Lỗi!", $"Bạn không đủ tiền để mua 1 bông hoa.");
                            embed.WithColor(new Discord.Color(255, 0, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                    }
                }
                UserAccounts.SaveAccounts();
            }
            else return;
        }
        [Command("tanghoa")]
        public async Task givemoneyforplayer(SocketUser user = null)
        {
            if (Context.Guild.Id == 580555457983152149)
            {
                var embed = new EmbedBuilder();
                if (user == null)
                {
                    embed.AddField($"Lỗi!", "Bạn chưa chọn Người Chơi để tặng hoa.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (user == (Context.User as SocketUser))
                {
                    embed.AddField($"Lỗi!", "Bạn sử dụng lệnh này lên bản thân.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (!Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống").Members.Contains(Context.User))
                {
                    embed.AddField($"Lỗi!", "Bạn phải ở trong Game mới có thể sử dụng lệnh này.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết").Members.Contains(Context.User))
                {
                    embed.AddField($"Lỗi!", "Bạn không thể tặng hoa khi đã chết.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    var accounts = UserAccounts.GetAccount(Context.User);
                    var plraccounts = UserAccounts.GetAccount(user);
                    if (accounts.roses >= 1)
                    {
                        accounts.roses = accounts.roses - 1;
                        plraccounts.plrroses = plraccounts.plrroses + 1;
                        embed.AddField($"Hệ Thống!", $"{Context.User.Username} đã tặng hoa cho {user.Username}!");
                        embed.WithColor(new Discord.Color(0, 255, 0));
                        await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync($"{Emote.Parse("<:rose:584250710284304384>")}{Emote.Parse("<:rose:584250710284304384>")}{Emote.Parse("<:rose:584250710284304384>")}{Emote.Parse("<:rose:584250710284304384>")}{Emote.Parse("<:rose:584250710284304384>")}");
                        await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("", false, embed.Build());
                        await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync($"{Emote.Parse("<:rose:584250710284304384>")}{Emote.Parse("<:rose:584250710284304384>")}{Emote.Parse("<:rose:584250710284304384>")}{Emote.Parse("<:rose:584250710284304384>")}{Emote.Parse("<:rose:584250710284304384>")}");
                        await Context.Client.GetGuild(530689610313891840).GetTextChannel(546314357991145502).SendMessageAsync(Context.User.Username + " Used: -tanghoa " + user.Username + "");
                    }
                    else
                    {
                        embed.AddField($"Lỗi!", "Bạn không còn hoa, nếu bạn muốn tặng hoa người chơi thì hãy mua hoa.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    UserAccounts.SaveAccounts();
                }
            }
            else return;
        }

        [Command("bangxephang")]
        [Alias("leaderboard", "bxh")]
        public async Task leaderboard(string text = null)
        {
            if (Context.Guild.Id == 530689610313891840)
            {
                if (text == null || text.ToLower() == "xu" || text.ToLower() == "tien")
                {
                    var embed = new EmbedBuilder();
                    var abc = File.ReadAllText("UserAccounts.json");
                    var another = JsonConvert.DeserializeObject<List<UserAccount>>(abc);
                    var result = another.OrderByDescending(x => x.points).ToArray();
                    var top1 = "Top 1: " + Context.Client.GetUser(result[0].ID).Username + " - " + result[0].points + "" + Emote.Parse("<:coin:584231931835580419>") + "";
                    var top2 = "Top 2: " + Context.Client.GetUser(result[1].ID).Username + " - " + result[1].points + "" + Emote.Parse("<:coin:584231931835580419>") + "";
                    var top3 = "Top 3: " + Context.Client.GetUser(result[2].ID).Username + " - " + result[2].points + "" + Emote.Parse("<:coin:584231931835580419>") + "";
                    var top4 = "Top 4: " + Context.Client.GetUser(result[3].ID).Username + " - " + result[3].points + "" + Emote.Parse("<:coin:584231931835580419>") + "";
                    var top5 = "Top 5: " + Context.Client.GetUser(result[4].ID).Username + " - " + result[4].points + "" + Emote.Parse("<:coin:584231931835580419>") + "";
                    var top6 = "Top 6: " + Context.Client.GetUser(result[5].ID).Username + " - " + result[5].points + "" + Emote.Parse("<:coin:584231931835580419>") + "";
                    var top7 = "Top 7: " + Context.Client.GetUser(result[6].ID).Username + " - " + result[6].points + "" + Emote.Parse("<:coin:584231931835580419>") + "";
                    var top8 = "Top 8: " + Context.Client.GetUser(result[7].ID).Username + " - " + result[7].points + "" + Emote.Parse("<:coin:584231931835580419>") + "";
                    var top9 = "Top 9: " + Context.Client.GetUser(result[8].ID).Username + " - " + result[8].points + "" + Emote.Parse("<:coin:584231931835580419>") + "";
                    var top10 = "Top 10: " + Context.Client.GetUser(result[9].ID).Username + " - " + result[9].points + "" + Emote.Parse("<:coin:584231931835580419>") + "";

                    embed.AddField("Bảng Xếp Hạng - Top 10 Xu - Ma Cún!", $"{top1}\n{top2}\n{top3}\n{top4}\n{top5}\n{top6}\n{top7}\n{top8}\n{top9}\n{top10}");
                    embed.WithColor(new Discord.Color(0, 255, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else if (text.ToLower() == "hoa" || text.ToLower() == "rose" || text.ToLower() == "roses")
                {
                    var embed = new EmbedBuilder();
                    var abc = File.ReadAllText("UserAccounts.json");
                    var another = JsonConvert.DeserializeObject<List<UserAccount>>(abc);
                    var result = another.OrderByDescending(x => x.plrroses).ToArray();
                    var top1 = "Top 1: " + Context.Client.GetUser(result[0].ID).Username + " - " + result[0].plrroses + "" + Emote.Parse("<:rose:584250710284304384>") + "";
                    var top2 = "Top 2: " + Context.Client.GetUser(result[1].ID).Username + " - " + result[1].plrroses + "" + Emote.Parse("<:rose:584250710284304384>") + "";
                    var top3 = "Top 3: " + Context.Client.GetUser(result[2].ID).Username + " - " + result[2].plrroses + "" + Emote.Parse("<:rose:584250710284304384>") + "";
                    var top4 = "Top 4: " + Context.Client.GetUser(result[3].ID).Username + " - " + result[3].plrroses + "" + Emote.Parse("<:rose:584250710284304384>") + "";
                    var top5 = "Top 5: " + Context.Client.GetUser(result[4].ID).Username + " - " + result[4].plrroses + "" + Emote.Parse("<:rose:584250710284304384>") + "";
                    var top6 = "Top 6: " + Context.Client.GetUser(result[5].ID).Username + " - " + result[5].plrroses + "" + Emote.Parse("<:rose:584250710284304384>") + "";
                    var top7 = "Top 7: " + Context.Client.GetUser(result[6].ID).Username + " - " + result[6].plrroses + "" + Emote.Parse("<:rose:584250710284304384>") + "";
                    var top8 = "Top 8: " + Context.Client.GetUser(result[7].ID).Username + " - " + result[7].plrroses + "" + Emote.Parse("<:rose:584250710284304384>") + "";
                    var top9 = "Top 9: " + Context.Client.GetUser(result[8].ID).Username + " - " + result[8].plrroses + "" + Emote.Parse("<:rose:584250710284304384>") + "";
                    var top10 = "Top 10: " + Context.Client.GetUser(result[9].ID).Username + " - " + result[9].plrroses + "" + Emote.Parse("<:rose:584250710284304384>") + "";

                    embed.AddField("Bảng Xếp Hạng - Top 10 Hoa - Ma Cún!", $"{top1}\n{top2}\n{top3}\n{top4}\n{top5}\n{top6}\n{top7}\n{top8}\n{top9}\n{top10}");
                    embed.WithColor(new Discord.Color(0, 255, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else return;
            }
        }

    }
}
