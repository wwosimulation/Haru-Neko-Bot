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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

using System.Diagnostics;

namespace Neko_Test.Ma_Cun_
{
    public class HuongDanLenh : ModuleBase<SocketCommandContext>
    {
        Random rnd = new Random();

        [Command("lenhquantro")]
        [RequireBotPermission(Discord.GuildPermission.EmbedLinks)]
        public async Task LenhChoQuanTro()
        {
            var embed = new EmbedBuilder();
            if (Context.Guild.Id != 580555457983152149)
            {
                return;
            }
            else if (!Context.Guild.Roles.FirstOrDefault(x => x.Name == "Quản Trò").Members.Contains(Context.User))
            {
                embed.AddField($"Lỗi!", "Người sử dụng cần có role Quản Trò.");
                embed.WithColor(new Discord.Color(255, 0, 0));
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
            else
            {
                embed.WithAuthor($"Lệnh Quản Trò. \n \n");
                embed.AddField($"-quantro", "Để đưa lên Quản Trò - Game hoặc bỏ đi.");
                embed.AddField($"-mogame", "Dùng để mở cho người chơi đăng ký tham gia game.");
                embed.AddField($"-donggame", "Dùng để khóa không cho người chơi đăng ký tham gia game.");
                embed.AddField($"-gamerole", "Để tự động đưa người chơi vào vai trò một cách ngẫu nhiên (Nhưng sẽ theo danh sách).");
                embed.AddField($"-themvaitro (Số Người Chơi) (Số Vai Trò)", "Để đưa vai trò cho người chơi (-danhsachvaitro để xem Số Vai Trò).");
                embed.AddField($"-batdau", "Dùng khi đã đưa vai trò cho người chơi xong - Sẽ bắt đầu thằng vào đêm 1.");
                embed.AddField($"-sang", "Dùng để bắt đầu buổi sáng cho Game - Dân Làng dậy.");
                embed.AddField($"-vote", "Để bắt đầu bỏ phiếu - Có 2 đợt.\nĐợt 1: Bắt đầu bỏ phiếu treo (Nếu không có ai bị treo thì vào thẳng -dem).\nĐợt 2: Sau khi có người bị treo thì sẽ bắt đầu vào bỏ phiếu sống chết (Đợi người chơi bỏ phiếu xong sẽ -dem).");
                embed.AddField($"-treo (Số Người Chơi Nhiếu Phiếu Nhất)", "Dùng để treo người chơi bị nhiều phiếu nhất sau khi có hiệu lệnh của đợt 1 (Cẩn thận với phiếu lặp của người chơi).");
                embed.AddField($"-dem","Dùng để bắt đầu vào ban đêm cho Game - Dân Làng ngủ.");
                embed.AddField($"-thang (Phe hoặc Người Thứ 3)", "Dùng khi game gần kết thúc và đã có phe hoặc người thắng.");
                embed.AddField($"-ketthuc", "Dùng để kết thúc 1 Game: Người chơi tự động kick sau 10 giây, tự động dọn dẹp khu vực Game.");


                //embed.AddField($"", "");
                int Re = rnd.Next(0, 255);
                int Ge = rnd.Next(0, 255);
                int Be = rnd.Next(0, 255);

                embed.WithColor(new Discord.Color(255, 50, 255));

                await Context.User.SendMessageAsync("", false, embed.Build());
            }
        }
        [Command("danhsachlenh")]
        [Alias("cmds", "lenh")]
        private async Task danhsachlenh()
        {
            if (Context.Guild.Id == 530689610313891840)
            {
                var embed = new EmbedBuilder();

                embed.WithAuthor($"Danh Sách Lệnh - Ma Sói Online - Ma Cún\n \n ");
                embed.WithDescription("[]-Tùy chọn ()-Yêu Cầu");
                embed.AddField($"-cuahang [Số trang]", $"Danh sách cửa hàng mua bằng xu và hoa.");
                embed.AddField($"-hangngay", $"Để nhận điểm danh nhận 10 xu hàng ngày!");
                embed.AddField($"-tien [Người Chơi]", $"Để kiểm tra túi đồ của người chơi!");
                embed.AddField($"-chotien (Người Chơi) (Số tiền)", $"Để cho người chơi xu!");
                embed.AddField($"-tanghoa (Người Chơi)", $"Để tặng một bông hoa cho người chơi (Chỉ dùng ở Trong khi chơi - Game Server)!");
                embed.AddField($"-trochoi", $"Để kiểm tra danh sách trò chơi của Ma Cún");
                embed.AddField($"-mua (Vật phẩm) [soluong] [Số lượng nếu là hoa]", $"Mua vật phẩm ở cửa hàng!");

                embed.WithColor(new Discord.Color(255, 255, 0));
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
            else return;
        }

        [Command("testcheck")]
        public async Task testcheckid()
        {
            var embed = new EmbedBuilder();
            {
                await Context.Channel.SendMessageAsync($"{Emote.Parse("<:coin:584231931835580419>")}");
                embed.AddField($"Lỗi!", $"{Emote.Parse("<:coin:584231931835580419>")} nè :)))");
                embed.WithColor(new Discord.Color(255, 0, 0));
                await Context.Channel.SendMessageAsync("", false, embed.Build());
            }
        }
        [Command("reboots")]
        public async Task rebootsomething()
        {
            Process.Start(Application.ExecutablePath);
            Environment.Exit(0);
        }

    }
}
