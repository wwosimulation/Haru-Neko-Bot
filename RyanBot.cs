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

            <:TohruWeary:585492969025568799>
            <:remsleepy:585492968182644758>
            <:remBlush:585492968228519937>
            <:LoveHeart:585492967880523821>
            <:Kya:585507397993234556>
            <:kannaWave:585492969008791580>
            <:kannaPeek:585492968807464984>
            <:kannanom:585492968962523160>
            <:pillowYes:585492967649837197>
            <:pillowNo:585492968274657300>

            <:GWpdnlaugh:587152172161040396>
            <:GWpdnXD:587152173922648070>
            <:ReimuFacePalm:587152173595623424>
            <:Naisu:587265138843844608>

            <:SenkoThinking:589049035055169536>
            <:SenkoPlease:589049212239347753>
            <:SenkoListening:589049338622246922>
            <:SenkoHi:589049277754507265>
            <:SenkoBlush:589049141183905797>

            
            <:VampySmug:590121887707693058>
            <:RaphiOhM:590121887963676673>
            <:RaphiWink:590121888043237390>
            <:WhoDesu:590121888655605762>
            <:WOW:590149641241231384>
            <:WannaSee:590149643376001024>
            <:owoAwoo:590149640356233220>

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
#cậu-bé-hoang-dã : 589462982275235860
#giáo-xứ : 589463015212974080
#mẹ-đơn-thân : 591478522111983616
#kẻ-tài-lanh : 591478642966528020
#sói-đào-hoa : 591478711774085143

#khu-đăng-ký : 580555887324954635
#sảnh-đợi : 580557883931099138
#buổi-sáng-thảo-luận : 580563096544739331
#khu-vực-xem-vote : 580564164687298609
#đàn-sói-tung-tăng : 580564753982816256
#gia-đình : 591517434540982272


// Quản Trò.

Ma Cun' : 530689610313891840
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
