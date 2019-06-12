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
using Neko_Test.ModulesMaCun;

namespace Neko_Test.Ma_Cun_
{
    public class Minigame : ModuleBase<SocketCommandContext>
    {

        [Command("RussianRoulette")]
        [Alias("rr", "coquaynga")]
        private async Task pewoof([Optional]string oof, [Optional]ulong test)
        {
            if (Context.Guild.Id == 530689610313891840)
            {
                var embed = new EmbedBuilder();
                if (oof == null)
                {
                    embed.WithTitle($"Ma Sói Online - Ma Cún - Cò Quay Nga \n \n ");
                    embed.AddField($"-rr cuoc (bet)", $"Để bắt đầu trận đấu mới.");
                    embed.AddField($"-rr thamgia", $"Tham gia trận đấu đã có người mở.");
                    embed.AddField($"-rr batdau", $"Bắt đầu trận.");
                    embed.AddField($"-rr huy", $"Hủy trận.");

                    embed.WithColor(new Discord.Color(0, 255, 255));
                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                }
                if (oof == "bet" || oof == "Bet" || oof == "cuoc" || oof == "Cuoc")
                {
                    if (GlobalFunctionMaCun.RRbetp == 0)
                    {
                        var acccounts = UserAccounts.GetAccount(Context.User);
                        if (acccounts.points >= test)
                        {
                            if (test >= 5)
                            {

                                GlobalFunctionMaCun.RRplrstartedIDp = Context.User.Id;
                                GlobalFunctionMaCun.RRbetp = test;

                                acccounts.points = acccounts.points - GlobalFunctionMaCun.RRbetp;
                                embed.WithTitle($"Ma Cún - Cò Quay Nga - Đã Mở! \n \n ");
                                embed.WithDescription($"Cò Quay Nga bắt đầu với {test}{Emote.Parse("<:coin:584231931835580419>")}! Sử dụng `-rr thamgia` để tham gia!");
                                embed.WithColor(new Discord.Color(0, 255, 255));

                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                            else
                            {
                                embed.AddField($"Lỗi!", $"Tiền đặt cược không thể thấp hơn 5 Xu.");
                                embed.WithColor(new Discord.Color(255, 0, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                        }
                        else
                        {
                            embed.AddField($"Lỗi!", $"Tiền đặt cược không thể cao hơn số tiền hiện tại của bạn.");
                            embed.WithColor(new Discord.Color(255, 0, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                    }
                    else
                    {
                        embed.AddField($"Lỗi!", $"{Context.Guild.Users.FirstOrDefault(x => x.Id == GlobalFunctionMaCun.RRplrstartedIDp).Username} đặt cược để mở trận đấu nên dùng `-rr thamgia` để tham gia!");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                }
                if (oof == "Join" || oof == "join" || oof == "Thamgia" || oof == "thamgia")
                {
                    if (GlobalFunctionMaCun.RRplr6IDp == 0)
                    {
                        if (Context.User.Id != GlobalFunctionMaCun.RRplrstartedIDp && Context.User.Id != GlobalFunctionMaCun.RRplr2IDp && Context.User.Id != GlobalFunctionMaCun.RRplr3IDp && Context.User.Id != GlobalFunctionMaCun.RRplr4IDp && Context.User.Id != GlobalFunctionMaCun.RRplr5IDp && Context.User.Id != GlobalFunctionMaCun.RRplr6IDp)
                        {
                            var acccounts = UserAccounts.GetAccount(Context.User);
                            if (acccounts.points >= GlobalFunctionMaCun.RRbetp)
                            {
                                if (GlobalFunctionMaCun.RRbetp != 0)
                                {
                                    if (GlobalFunctionMaCun.RRplr5IDp != 0)
                                    {
                                        GlobalFunctionMaCun.RRplr6IDp = Context.User.Id;
                                    }
                                    if (GlobalFunctionMaCun.RRplr4IDp != 0)
                                    {
                                        if (GlobalFunctionMaCun.RRplr5IDp == 0)
                                        {
                                            GlobalFunctionMaCun.RRplr5IDp = Context.User.Id;
                                        }
                                    }
                                    if (GlobalFunctionMaCun.RRplr3IDp != 0)
                                    {
                                        if (GlobalFunctionMaCun.RRplr4IDp == 0)
                                        {
                                            GlobalFunctionMaCun.RRplr4IDp = Context.User.Id;
                                        }
                                    }
                                    if (GlobalFunctionMaCun.RRplr2IDp != 0)
                                    {
                                        if (GlobalFunctionMaCun.RRplr3IDp == 0)
                                        {
                                            GlobalFunctionMaCun.RRplr3IDp = Context.User.Id;
                                        }
                                    }

                                    if (GlobalFunctionMaCun.RRplr2IDp == 0)
                                    {
                                        GlobalFunctionMaCun.RRplr2IDp = Context.User.Id;
                                    }


                                    acccounts.points = acccounts.points - GlobalFunctionMaCun.RRbetp;
                                    embed.WithTitle($"Ma Cún - Cò Quay Nga - Tham Gia! \n \n ");
                                    embed.WithDescription($"Đã tham gia trận đấu với {GlobalFunctionMaCun.RRbetp}{Emote.Parse("<:coin:584231931835580419>")}!");
                                    embed.WithColor(new Discord.Color(0, 255, 255));

                                    await Context.Channel.SendMessageAsync("", false, embed.Build());

                                }
                                else
                                {
                                    embed.AddField($"Lỗi!", $"Không có trận đấu nào được mở nên dùng `-rr cuoc (Số tiền cược)` để mở trận đấu.");
                                    embed.WithColor(new Discord.Color(255, 0, 0));
                                    await Context.Channel.SendMessageAsync("", false, embed.Build());
                                }
                            }
                            else
                            {
                                embed.AddField($"Lỗi!", $"Bạn không đủ tiền để tham gia.");
                                embed.WithColor(new Discord.Color(255, 0, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                        }
                        else
                        {
                            embed.AddField($"Lỗi!", $"Bạn đã tham gia trận đấu rồi.");
                            embed.WithColor(new Discord.Color(255, 0, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                    }
                    else
                    {
                        embed.AddField($"Lỗi!", $"Tối đa là 6 người chơi.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                }
                if (oof == "Start" || oof == "start" || oof == "batdau" || oof == "Batdau")
                {
                    if (GlobalFunctionMaCun.RRplrstartedIDp != 0)
                    {
                        if (GlobalFunctionMaCun.RRplrstartedIDp == Context.User.Id)
                        {
                            if (GlobalFunctionMaCun.RRplr2IDp != 0)
                            {
                                int bombs = 1;

                                GlobalFunctionMaCun.RRplrsp = 1;
                                List<ulong> RRlist = new List<ulong>();
                                List<int> oofbigoof = new List<int>();
                                RRlist.Add(GlobalFunctionMaCun.RRplrstartedIDp);
                                RRlist.Add(GlobalFunctionMaCun.RRplr2IDp);
                                if (GlobalFunctionMaCun.RRplr3IDp != 0)
                                {
                                    bombs++;
                                    GlobalFunctionMaCun.RRplrsp = 2;
                                    RRlist.Add(GlobalFunctionMaCun.RRplr3IDp);
                                }
                                if (GlobalFunctionMaCun.RRplr4IDp != 0)
                                {
                                    bombs++;
                                    GlobalFunctionMaCun.RRplrsp = 3;
                                    RRlist.Add(GlobalFunctionMaCun.RRplr4IDp);
                                }
                                if (GlobalFunctionMaCun.RRplr5IDp != 0)
                                {
                                    bombs++;
                                    GlobalFunctionMaCun.RRplrsp = 4;
                                    RRlist.Add(GlobalFunctionMaCun.RRplr5IDp);
                                }
                                if (GlobalFunctionMaCun.RRplr6IDp != 0)
                                {
                                    bombs++;
                                    GlobalFunctionMaCun.RRplrsp = 5;
                                    RRlist.Add(GlobalFunctionMaCun.RRplr6IDp);
                                }
                                RRlist.Add(GlobalFunctionMaCun.RRplrstartedIDp);
                                RRlist.Add(GlobalFunctionMaCun.RRplr2IDp);
                                bombs = 3;
                                if (GlobalFunctionMaCun.RRplr3IDp != 0)
                                {
                                    bombs++;
                                    GlobalFunctionMaCun.RRplrsp = 2;
                                    RRlist.Add(GlobalFunctionMaCun.RRplr3IDp);
                                }
                                if (GlobalFunctionMaCun.RRplr4IDp != 0)
                                {
                                    bombs++;
                                    GlobalFunctionMaCun.RRplrsp = 3;
                                    RRlist.Add(GlobalFunctionMaCun.RRplr4IDp);
                                }
                                if (GlobalFunctionMaCun.RRplr5IDp != 0)
                                {
                                    bombs++;
                                    GlobalFunctionMaCun.RRplrsp = 4;
                                    RRlist.Add(GlobalFunctionMaCun.RRplr5IDp);
                                }
                                if (GlobalFunctionMaCun.RRplr6IDp != 0)
                                {
                                    bombs++;
                                    GlobalFunctionMaCun.RRplrsp = 5;
                                    RRlist.Add(GlobalFunctionMaCun.RRplr6IDp);
                                }
                                GlobalFunctionMaCun.RRplrsp++;
                                Random a = new Random();
                                bombs++;
                                int n = a.Next(0, bombs);
                                int dude = 0;
                                int s = 0;
                                while (dude == 0)
                                {

                                    if (n != s)
                                    {
                                        var winlose = new EmbedBuilder();



                                        winlose.WithTitle($"{Context.Guild.GetUser(RRlist[s]).Username} đã bóp cò súng và may mắn sống sót!");
                                        winlose.WithColor(new Discord.Color(0, 255, 255));
                                        await Context.Channel.SendMessageAsync("", false, winlose.Build());
                                    }
                                    else
                                    {
                                        var winlose = new EmbedBuilder();
                                        winlose.WithTitle($"{Context.Guild.GetUser(RRlist[s]).Username} đã bóp có súng và chết...");
                                        winlose.WithDescription($"{Emote.Parse("<:normal_gravestone:475777194606460929>")}");
                                        dude = 1;
                                        winlose.WithColor(new Discord.Color(0, 255, 255));
                                        await Context.Channel.SendMessageAsync("", false, winlose.Build());
                                    }
                                    s++;
                                    await Task.Delay(1500);


                                }
                                var winners = new EmbedBuilder();
                                winners.WithTitle("Người thắng:");
                                if (RRlist[n] != GlobalFunctionMaCun.RRplrstartedIDp)
                                {
                                    var accounts = UserAccounts.GetAccount(Context.Guild.GetUser(GlobalFunctionMaCun.RRplrstartedIDp));
                                    accounts.points = accounts.points + GlobalFunctionMaCun.RRbetp + (GlobalFunctionMaCun.RRbetp / (GlobalFunctionMaCun.RRplrsp - 1));

                                    winners.AddField($"{Context.Guild.GetUser(GlobalFunctionMaCun.RRplrstartedIDp)} thắng và nhận được {  (GlobalFunctionMaCun.RRbetp / (GlobalFunctionMaCun.RRplrsp - 1))}{Emote.Parse("<:coin:584231931835580419>")}", "Woohoo coins!\n \n");
                                }
                                if (RRlist[n] != GlobalFunctionMaCun.RRplr2IDp)
                                {
                                    var accounts = UserAccounts.GetAccount(Context.Guild.GetUser(GlobalFunctionMaCun.RRplr2IDp));
                                    accounts.points = accounts.points + GlobalFunctionMaCun.RRbetp + (GlobalFunctionMaCun.RRbetp / (GlobalFunctionMaCun.RRplrsp - 1));

                                    winners.AddField($"{Context.Guild.GetUser(GlobalFunctionMaCun.RRplr2IDp)} thắng và nhận được { (GlobalFunctionMaCun.RRbetp / (GlobalFunctionMaCun.RRplrsp - 1))}{Emote.Parse("<:coin:584231931835580419>")}", "Woohoo coins!\n \n");
                                }
                                if (RRlist[n] != GlobalFunctionMaCun.RRplr3IDp && GlobalFunctionMaCun.RRplr3IDp != 0)
                                {
                                    var accounts = UserAccounts.GetAccount(Context.Guild.GetUser(GlobalFunctionMaCun.RRplr3IDp));
                                    accounts.points = accounts.points + GlobalFunctionMaCun.RRbetp + (GlobalFunctionMaCun.RRbetp / (GlobalFunctionMaCun.RRplrsp - 1));

                                    winners.AddField($"{Context.Guild.GetUser(GlobalFunctionMaCun.RRplr3IDp)} thắng và nhận được { (GlobalFunctionMaCun.RRbetp / (GlobalFunctionMaCun.RRplrsp - 1))}{Emote.Parse("<:coin:584231931835580419>")}", "Woohoo coins!\n \n");
                                }
                                if (RRlist[n] != GlobalFunctionMaCun.RRplr4IDp && GlobalFunctionMaCun.RRplr4IDp != 0)
                                {
                                    var accounts = UserAccounts.GetAccount(Context.Guild.GetUser(GlobalFunctionMaCun.RRplr4IDp));
                                    accounts.points = accounts.points + GlobalFunctionMaCun.RRbetp + (GlobalFunctionMaCun.RRbetp / (GlobalFunctionMaCun.RRplrsp - 1));

                                    winners.AddField($"{Context.Guild.GetUser(GlobalFunctionMaCun.RRplr4IDp)} thắng và nhận được {(GlobalFunctionMaCun.RRbetp / (GlobalFunctionMaCun.RRplrsp - 1))}{Emote.Parse("<:coin:584231931835580419>")}", "Woohoo coins!\n \n");
                                }
                                if (RRlist[n] != GlobalFunctionMaCun.RRplr5IDp && GlobalFunctionMaCun.RRplr5IDp != 0)
                                {
                                    var accounts = UserAccounts.GetAccount(Context.Guild.GetUser(GlobalFunctionMaCun.RRplr5IDp));
                                    accounts.points = accounts.points + GlobalFunctionMaCun.RRbetp + (GlobalFunctionMaCun.RRbetp / (GlobalFunctionMaCun.RRplrsp - 1));

                                    winners.AddField($"{Context.Guild.GetUser(GlobalFunctionMaCun.RRplr5IDp)} thắng và nhận được {(GlobalFunctionMaCun.RRbetp / (GlobalFunctionMaCun.RRplrsp - 1))}{Emote.Parse("<:coin:584231931835580419>")}", "Woohoo coins!\n \n");
                                }
                                if (RRlist[n] != GlobalFunctionMaCun.RRplr6IDp && GlobalFunctionMaCun.RRplr6IDp != 0)
                                {
                                    var accounts = UserAccounts.GetAccount(Context.Guild.GetUser(GlobalFunctionMaCun.RRplr6IDp));
                                    accounts.points = accounts.points + GlobalFunctionMaCun.RRbetp + (GlobalFunctionMaCun.RRbetp / (GlobalFunctionMaCun.RRplrsp - 1));

                                    winners.AddField($"{Context.Guild.GetUser(GlobalFunctionMaCun.RRplr6IDp)} thắng và nhận được {(GlobalFunctionMaCun.RRbetp / (GlobalFunctionMaCun.RRplrsp - 1))}{Emote.Parse("<:coin:584231931835580419>")}", "Woohoo coins!\n \n");
                                }
                                winners.WithColor(new Discord.Color(0, 255, 255));
                                await Context.Channel.SendMessageAsync("", false, winners.Build());

                                GlobalFunctionMaCun.RRbetp = 0;
                                GlobalFunctionMaCun.RRplr2IDp = 0;
                                GlobalFunctionMaCun.RRplr3IDp = 0;
                                GlobalFunctionMaCun.RRplr4IDp = 0;
                                GlobalFunctionMaCun.RRplr5IDp = 0;
                                GlobalFunctionMaCun.RRplr6IDp = 0;
                                GlobalFunctionMaCun.RRplrsp = 0;
                                GlobalFunctionMaCun.RRplrstartedIDp = 0;
                            }
                            else
                            {
                                embed.AddField($"Lỗi!", $"Tối thiểu là 2 người chơi.");
                                embed.WithColor(new Discord.Color(255, 0, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());
                            }
                        }
                        else
                        {
                            embed.AddField($"Lỗi!", $"Bạn không phải là người cược cho trận đấu nên không thể bắt đầu.");
                            embed.WithColor(new Discord.Color(255, 0, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                    }
                    else
                    {
                        embed.AddField($"Lỗi!", $"Không có trận đấu nào được mở nên dùng `-rr cuoc (Số tiền cược)` để mở trận đấu.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                }
                if (oof == "cancel" || oof == "Cancel" || oof == "Huy" || oof == "huy")
                {
                    if (GlobalFunctionMaCun.RRplrstartedIDp != 0 || Context.User.Id == 454492255932252160)
                    {
                        if (Context.User.Id == GlobalFunctionMaCun.RRplrstartedIDp || Context.User.Id == 215498607703490560)
                        {
                            var hostacc = UserAccounts.GetAccount(Context.Guild.GetUser(GlobalFunctionMaCun.RRplrstartedIDp));
                            hostacc.points = hostacc.points + GlobalFunctionMaCun.RRbetp;
                            if (GlobalFunctionMaCun.RRplr2IDp != 0)
                            {
                                var accounts = UserAccounts.GetAccount(Context.Guild.GetUser(GlobalFunctionMaCun.RRplr2IDp));
                                accounts.points = accounts.points + GlobalFunctionMaCun.RRbetp;
                            }
                            if (GlobalFunctionMaCun.RRplr3IDp != 0)
                            {
                                var accounts = UserAccounts.GetAccount(Context.Guild.GetUser(GlobalFunctionMaCun.RRplr3IDp));
                                accounts.points = accounts.points + GlobalFunctionMaCun.RRbetp;
                            }
                            if (GlobalFunctionMaCun.RRplr4IDp != 0)
                            {
                                var accounts = UserAccounts.GetAccount(Context.Guild.GetUser(GlobalFunctionMaCun.RRplr4IDp));
                                accounts.points = accounts.points + GlobalFunctionMaCun.RRbetp;
                            }
                            if (GlobalFunctionMaCun.RRplr5IDp != 0)
                            {
                                var accounts = UserAccounts.GetAccount(Context.Guild.GetUser(GlobalFunctionMaCun.RRplr5IDp));
                                accounts.points = accounts.points + GlobalFunctionMaCun.RRbetp;
                            }
                            if (GlobalFunctionMaCun.RRplr6IDp != 0)
                            {
                                var accounts = UserAccounts.GetAccount(Context.Guild.GetUser(GlobalFunctionMaCun.RRplr6IDp));
                                accounts.points = accounts.points + GlobalFunctionMaCun.RRbetp;
                            }
                            embed.AddField($"Ma Cún - Cò Quay Nga - Hủy!", $"Đã hủy trận đấu Cò Quay Nga!");
                            embed.WithColor(new Discord.Color(0, 255, 255));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                            GlobalFunctionMaCun.RRbetp = 0;
                            GlobalFunctionMaCun.RRplr2IDp = 0;
                            GlobalFunctionMaCun.RRplr3IDp = 0;
                            GlobalFunctionMaCun.RRplr4IDp = 0;
                            GlobalFunctionMaCun.RRplr5IDp = 0;
                            GlobalFunctionMaCun.RRplr6IDp = 0;
                            GlobalFunctionMaCun.RRplrsp = 0;
                            GlobalFunctionMaCun.RRplrstartedIDp = 0;
                        }
                        else
                        {
                            embed.AddField($"Lỗi!", $"Bạn không phải là người cược cho trận đấu nên không thể hủy.");
                            embed.WithColor(new Discord.Color(255, 0, 0));
                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                        }
                    }
                    else
                    {
                        embed.AddField($"Lỗi!", $"Không có trận đấu nào được mở nên dùng `-rr cuoc (Số tiền cược)` để mở trận đấu.");
                        embed.WithColor(new Discord.Color(255, 0, 0));
                        await Context.Channel.SendMessageAsync("", false, embed.Build());
                    }
                }
                UserAccounts.SaveAccounts();
            }
        }

    }
}
