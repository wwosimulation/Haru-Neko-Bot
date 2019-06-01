using Discord;
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
using System.Globalization;
using System.Windows.Forms;

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
                    Context.Client.GetGuild(530689610313891840).GetTextChannel(546314357991145502).SendMessageAsync(Context.User.Username + "#" + Context.User.Discriminator + " - " + Context.User.Id + ": -themtien " + user.Username + " " + number + ".");
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
                    embed.AddField($"Lỗi!", "Bạn không thể đưa xóa dưới 0 xu.");
                    embed.WithColor(new Discord.Color(255, 0, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else//546314357991145502
                {
                    var accounts = UserAccounts.GetAccount(user);
                    accounts.points = accounts.points - number;
                    embed.AddField($"Hệ Thống!", $"Đã xóa {number}{Emote.Parse("<:coin:584231931835580419>")} cho {user.Username}, giờ {user.Username} có {accounts.points}{Emote.Parse("<:coin:584231931835580419>")}.");
                    embed.WithColor(new Discord.Color(0, 255, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                    Context.Client.GetGuild(530689610313891840).GetTextChannel(546314357991145502).SendMessageAsync(Context.User.Username + "#" + Context.User.Discriminator + " - " + Context.User.Id + ": -xoatien " + user.Username + " " + number + ".");
                    UserAccounts.SaveAccounts();
                }
            }
            else return;
        }
        [Command("tien")]
        [Alias("tuido", "bal")]
        public async Task inventory([Optional]SocketUser user)
        {
            if (Context.Guild.Id == 530689610313891840)
            {
                var embed = new EmbedBuilder();
                if (user == null || user == (Context.User as SocketUser))
                {
                    var accounts = UserAccounts.GetAccount(Context.User);
                    embed.AddField($"Túi đồ của bạn!", $"Tổng xu có: {accounts.points}{Emote.Parse("<:coin:584231931835580419>")}.\nHoa có: {accounts.roses}{Emote.Parse("<:rose:584250710284304384>")}.\nHoa được tặng: {accounts.roses}{Emote.Parse("<:rose:584250710284304384>")}.");
                    embed.WithColor(new Discord.Color(0, 255, 255));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    var accounts = UserAccounts.GetAccount(user);
                    embed.AddField($"Túi đồ của {user.Username}!", $"Tổng xu có: {accounts.points}{Emote.Parse("<:coin:584231931835580419>")}.\nHoa có: {accounts.roses}{Emote.Parse("<:rose:584250710284304384>")}.\nHoa được tặng: {accounts.roses}{Emote.Parse("<:rose:584250710284304384>")}.");
                    embed.WithColor(new Discord.Color(0, 255, 255));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                UserAccounts.SaveAccounts();
            }
            else return;
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
                    embed.WithAuthor($"Chào mừng {Context.User.Username} đã quay trở lại, bạn được thưởng {daily}{Emote.Parse("<:coin:584231931835580419>")}.");
                    embed.WithColor(new Discord.Color(0, 255, 0));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                else
                {
                    if (24 - DateTime.Now.Hour != 0 || 24 - DateTime.Now.Hour != 24 || 60 - DateTime.Now.Minute != 60 || 60 - DateTime.Now.Minute != 0)
                    {
                        embed.AddField($"Lỗi!", "Bạn đã nhận thưởng ngày hôm nay, hãy quay lại sau {23 - DateTime.Now.Hour} giờ {60 - DateTime.Now.Minute} phút.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                    if (60 - DateTime.Now.Minute == 60 || 60 - DateTime.Now.Minute == 0)
                    {
                        embed.AddField($"Lỗi!", "Bạn đã nhận thưởng ngày hôm nay, hãy quay lại sau {23 - DateTime.Now.Hour} giờ.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }

                }
                UserAccounts.SaveAccounts();
            }
            else return;
        }
    }
}
