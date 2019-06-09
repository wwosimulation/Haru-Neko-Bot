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
                            if (Context.User.Id == GlobalFunctionMaCun.dongbang & Context.User.Id == GlobalFunctionMaCun.moi)
                            {
                                embed.AddField($"Lỗi!", "Bạn không thể Soi khi bị Đóng Băng.");
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
                                if (Context.Channel.Id == 580574739391578112 & Context.User.Id != GlobalFunctionMaCun.dongbang & Context.User.Id != GlobalFunctionMaCun.moi || Context.Channel.Id == 580574739391578112 & Context.User.Id != GlobalFunctionMaCun.moi)
                                {
                                    if (GlobalFunctionMaCun.thayboi == 1)
                                    {
                                        if (user.Id == GlobalFunctionMaCun.plr1 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Người chơi số " + user.Nickname + " là Thiện.");
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
                                        else if (user.Id == GlobalFunctionMaCun.plr14 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Người chơi số " + user.Nickname + " là Không Rõ.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.thayboi--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr15 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Người chơi số " + user.Nickname + " là Không Rõ.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.thayboi--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr16 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Người chơi số " + user.Nickname + " là Ác.");
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
                                else if (Context.Channel.Id == 580574414660435982 & Context.User.Id != GlobalFunctionMaCun.dongbang & Context.User.Id != GlobalFunctionMaCun.moi || Context.Channel.Id == 580574414660435982 & Context.User.Id != GlobalFunctionMaCun.moi)
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
                                            embed.AddField($"Hệ Thống!", "Vai Trò của người chơi số " + user.Nickname + " là Thầy Bói.");
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
                                        else if (user.Id == GlobalFunctionMaCun.plr14 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Người chơi số " + user.Nickname + " là Gái Điếm.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.tientri--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr15 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Người chơi số " + user.Nickname + " là Thầy Đồng.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.tientri--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr16 & user.Id != GlobalFunctionMaCun.phuphep)
                                        {
                                            embed.AddField($"Hệ Thống!", "Người chơi số " + user.Nickname + " là Sói Tri.");
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
                                        if (user.Id == GlobalFunctionMaCun.plr4 || user.Id == GlobalFunctionMaCun.plr6 || user.Id == GlobalFunctionMaCun.plr11)
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
                                        else if (user.Id == GlobalFunctionMaCun.plr1)
                                        {
                                            embed.AddField($"Hệ Thống!", "Vai Trò của người chơi số " + user.Nickname + " là Bảo Vệ.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.soitri--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr2)
                                        {
                                            embed.AddField($"Hệ Thống!", "Vai Trò của người chơi số " + user.Nickname + " là Thầy Bói.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.soitri--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr3)
                                        {
                                            embed.AddField($"Hệ Thống!", "Vai Trò của người chơi số " + user.Nickname + " là Dân Làng.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.soitri--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr5)
                                        {
                                            embed.AddField($"Hệ Thống!", "Vai Trò của người chơi số " + user.Nickname + " là Già Làng.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.soitri--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr7)
                                        {
                                            embed.AddField($"Hệ Thống!", "Vai Trò của người chơi số " + user.Nickname + " là Thợ Săn.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.soitri--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr8)
                                        {
                                            embed.AddField($"Hệ Thống!", "Vai Trò của người chơi số " + user.Nickname + " là Thằng Ngố.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.soitri--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr9)
                                        {
                                            embed.AddField($"Hệ Thống!", "Vai Trò của người chơi số " + user.Nickname + " là Phù Thủy.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.soitri--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr10)
                                        {
                                            embed.AddField($"Hệ Thống!", "Vai Trò của người chơi số " + user.Nickname + " là Xạ Thủ.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.soitri--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr12)
                                        {
                                            embed.AddField($"Hệ Thống!", "Người chơi số " + user.Nickname + " là Tiên Tri.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.soitri--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr13)
                                        {
                                            embed.AddField($"Hệ Thống!", "Người chơi số " + user.Nickname + " là Sát Nhân.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.soitri--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr14)
                                        {
                                            embed.AddField($"Hệ Thống!", "Người chơi số " + user.Nickname + " là Gái Điếm.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.soitri--;
                                        }
                                        else if (user.Id == GlobalFunctionMaCun.plr15)
                                        {
                                            embed.AddField($"Hệ Thống!", "Người chơi số " + user.Nickname + " là Thầy Đồng.");
                                            embed.WithColor(new Discord.Color(0, 255, 0));
                                            await Context.Channel.SendMessageAsync("", false, embed.Build());
                                            GlobalFunctionMaCun.soitri--;
                                        }
                                        else return;
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
                                    embed.AddField($"Lỗi!", "Bạn không thể soi khi bị Đóng Băng hoặc bị Gái Điếm mời.");
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
            if (Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 580574451834290176 || Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 580574522361774081 || Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 580574634811064342 || Context.Guild.Id == 580555457983152149 & Context.Channel.Id == 583828385659355147)
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
                                    if (user.Id == GlobalFunctionMaCun.plr6 || user.Id == GlobalFunctionMaCun.plr11 || user.Id == GlobalFunctionMaCun.plr16)
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
                                    if (user.Id == GlobalFunctionMaCun.plr4 || user.Id == GlobalFunctionMaCun.plr11 || user.Id == GlobalFunctionMaCun.plr16)
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
                                    if (user.Id == GlobalFunctionMaCun.plr4 || user.Id == GlobalFunctionMaCun.plr6 || user.Id == GlobalFunctionMaCun.plr16)
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
                                    if (user.Id == GlobalFunctionMaCun.plr4 || user.Id == GlobalFunctionMaCun.plr6 || user.Id == GlobalFunctionMaCun.plr11)
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
                            else if (user.Id == GlobalFunctionMaCun.plr4 || user.Id == GlobalFunctionMaCun.plr11 || user.Id == GlobalFunctionMaCun.plr16)
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
                                    else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr14)
                                    {
                                        await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.moi = 0;
                                    }
                                    else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr16)
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
                            else if (user.Id == GlobalFunctionMaCun.plr14)
                            {
                                if (GlobalFunctionMaCun.moi != 0)
                                {
                                    if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr1)
                                    {
                                        await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + Context.Guild.GetUser(GlobalFunctionMaCun.moi).Nickname + " chết chung.");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.baoveplr = 0;
                                        GlobalFunctionMaCun.phedan--;
                                    }
                                    else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr4)
                                    {
                                        await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + Context.Guild.GetUser(GlobalFunctionMaCun.moi).Nickname + " chết chung.");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phesoi--;
                                    }
                                    else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr6)
                                    {
                                        await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + Context.Guild.GetUser(GlobalFunctionMaCun.moi).Nickname + " chết chung.");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phesoi--;
                                    }
                                    else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr9)
                                    {
                                        await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + Context.Guild.GetUser(GlobalFunctionMaCun.moi).Nickname + " chết chung.");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phedan--;
                                        GlobalFunctionMaCun.cuu = 0;
                                    }
                                    else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr11)
                                    {
                                        await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + Context.Guild.GetUser(GlobalFunctionMaCun.moi).Nickname + " chết chung.");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phesoi--;
                                    }
                                    else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr13)
                                    {
                                        await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + Context.Guild.GetUser(GlobalFunctionMaCun.moi).Nickname + " chết chung.");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phethu3--;
                                        GlobalFunctionMaCun.dam = 0;
                                    }
                                    else if (GlobalFunctionMaCun.moi == GlobalFunctionMaCun.plr16)
                                    {
                                        await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + Context.Guild.GetUser(GlobalFunctionMaCun.moi).Nickname + " chết chung.");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phesoi--;
                                    }
                                    else
                                    {
                                        await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + Context.Guild.GetUser(GlobalFunctionMaCun.moi).Nickname + " chết chung.");
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                        await Context.Guild.GetUser(GlobalFunctionMaCun.moi).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                        GlobalFunctionMaCun.phedan--;
                                    }
                                }
                                else
                                {
                                    await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + user.Nickname + ".");
                                    await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                    await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                    GlobalFunctionMaCun.phedan--;
                                }
                            }
                            else if (user.Id == GlobalFunctionMaCun.plr16)
                            {
                                await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Phù Thủy đã đầu độc và giết " + user.Nickname + ".");
                                await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                GlobalFunctionMaCun.phesoi--;
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
                                        else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr14)
                                        {
                                            await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Thợ Săn chết và kéo " + Context.Guild.GetUser(GlobalFunctionMaCun.keo).Nickname + " chết chung.");
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                            await Context.Guild.GetUser(GlobalFunctionMaCun.keo).AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                            GlobalFunctionMaCun.phedan--;
                                            GlobalFunctionMaCun.moi = 0;
                                        }
                                        else if (GlobalFunctionMaCun.keo == GlobalFunctionMaCun.plr16)
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
                                else if (GlobalFunctionMaCun.treo == GlobalFunctionMaCun.plr14)
                                {
                                    await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + user.Nickname + ".");
                                    await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                    await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                    GlobalFunctionMaCun.phedan--;
                                    GlobalFunctionMaCun.moi = 0;
                                }
                                else if (user.Id == GlobalFunctionMaCun.plr16)
                                {
                                    await Context.Guild.GetTextChannel(580563096544739331).SendMessageAsync("Xạ Thủ đã bắn chết " + user.Nickname + ".");
                                    await user.RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Sống"));
                                    await user.AddRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Chết"));
                                    GlobalFunctionMaCun.phesoi--;
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
                            else if (user.Id == GlobalFunctionMaCun.plr4 || user.Id == GlobalFunctionMaCun.plr6 || user.Id == GlobalFunctionMaCun.plr16)
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
                            await Context.Client.GetGuild(580555457983152149).GetTextChannel(580564753982816256).SendMessageAsync(""+Context.Guild.GetUser(Context.User.Id).Nickname+" đã từ bỏ chức năng của mình để có thể cắn người chơi.");
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
