using Discord;
using Discord.Commands;

using System;
using System.Collections.Generic;
using System.Text;

namespace Neko_Test
{
    class RyanBot
    {
        public RyanBot()
        {
            //ID WWO Sim: 465795320526274561
            //ID GameServer: 472261911526768642
            //Neko Cyan ID: 454492255932252160
            // await Context.Client.GetGuild(465795320526274561).GetUser(GlobalFunction.jailed).RemoveRoleAsync(Context.Guild.Roles.FirstOrDefault(x => x.Name == "Jailed"));


            //https://docs.stillu.cc/guides/emoji/emoji.html
            //https://docs.stillu.cc/guides/commands/typereaders.html

            //Mau Do: embed.WithColor(new Discord.Color(255, 0, 0));
            //Mau Xanh La: embed.WithColor(new Discord.Color(0, 255, 0));
            //Server ID: 580555457983152149
            /*
             
                                embed.AddField($"Lỗi!", "Bạn không thể bảo vệ khi bị Đóng Băng.");
                                embed.WithColor(new Discord.Color(255, 0, 0));
                                await Context.Channel.SendMessageAsync("", false, embed.Build());

            Xu: {Emote.Parse("<:coin:584231931835580419>")}
            Hoa: {Emote.Parse("<:rose:584250710284304384>")}

#bảo-vệ : 580574363930198021
#tiên-tri : 580574414660435982
#dân : 580574427712847872
#sói-thường : 580574451834290176
#già-làng : 580574497514586137
#sói-phù-thủy : 580574522361774081
#thợ-săn : 580574545606475782
#thằng-ngố : 580574572483706891
#phù-thủy : 580574598677135390
#xạ-thủ : 580574616645402656
#sói-băng : 580574634811064342
#thầy-bói : 580574739391578112
#sát-nhân : 580574812662136836
#gái-điếm : 583828253681254400
#thầy-đồng : 583828359394492427
#sói-tri : 583828385659355147

#khu-đăng-ký : 580555887324954635
#sảnh-đợi : 580557883931099138
#buổi-sáng-thảo-luận : 580563096544739331
#khu-vực-xem-vote : 580564164687298609
#đàn-sói-tung-tăng : 580564753982816256

// Quản Trò.

Ma Cún - Game Server ID: 580555457983152149
#vai-trò : 580699915156455424
#Quản Trò - Lệnh : 580558295023222784
#Quản Trò - Trò Chuyện : 580558465081147392

-baove (Đã Xong) - 2 Mục - Đêm
-soi (Đã Xong) - 1 Mục - Đêm
-can (Đã Xong) - 1 Mục - Đêm
-phuphep (Đã Xong) - 1 Mục - Sáng
-keo (Đã Xong) - 1 Mục - All
-daudoc (Đã Xong) - 1 Mục - Đêm
-cuu (Đã Xong) - 2 Mục - Đêm
-ban (Đã Xong) - 1 Mục - Sáng
-dongbang (Đã Xong) - 1 Mục - Sáng
-dam (Đã Xong) - 1 Mục - Đêm

//                      await Context.Guild.GetTextChannel(580564164687298609).SendMessageAsync("" + Context.Guild.GetUser(Context.User.Id).Nickname + " đã bỏ phiếu **__Sống__**.");
                        return;


                        if (GlobalFunctionMaCun.danxathu >= 1 )
                        {
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
             
             */
        }
    }
}
