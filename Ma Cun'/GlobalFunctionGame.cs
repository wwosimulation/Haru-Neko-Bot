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
using Neko_Test.Core.UserAccounts10;
using Neko_Test.ModulesMaCun;

namespace Neko_Test.ModulesMaCunGame
{
    public class GlobalFunctionGame
    {

        public static async Task RoleShowWhenHide(ulong id, string role)
        {
            if (role.ToLower() == "sát" || role.ToLower() == "sát-nhân" || role.ToLower() == "13")
            {
                if (id == GlobalFunctionMaCun.plr1)
                {
                    GlobalFunctionMaCun.nameroles = "Bảo Vệ";
                }
                else if (id == GlobalFunctionMaCun.plr2)
                {
                    GlobalFunctionMaCun.nameroles = "Thầy Bói";
                }
                else if (id == GlobalFunctionMaCun.plr3)
                {
                    GlobalFunctionMaCun.nameroles = "Dân Làng";
                }
                else if (id == GlobalFunctionMaCun.plr4)
                {
                    GlobalFunctionMaCun.nameroles = "Sói Thường";
                }
                else if (id == GlobalFunctionMaCun.plr5)
                {
                    GlobalFunctionMaCun.nameroles = "Già Làng";
                }
                else if (id == GlobalFunctionMaCun.plr6)
                {
                    GlobalFunctionMaCun.nameroles = "Sói Phù Thủy";
                }
                else if (id == GlobalFunctionMaCun.plr7)
                {
                    GlobalFunctionMaCun.nameroles = "Thợ Săn";
                }
                else if (id == GlobalFunctionMaCun.plr8)
                {
                    GlobalFunctionMaCun.nameroles = "Thằng Ngố";
                }
                else if (id == GlobalFunctionMaCun.plr9)
                {
                    GlobalFunctionMaCun.nameroles = "Phù Thủy";
                }
                else if (id == GlobalFunctionMaCun.plr10)
                {
                    GlobalFunctionMaCun.nameroles = "Xạ Thủ";
                }
                else if (id == GlobalFunctionMaCun.plr11)
                {
                    GlobalFunctionMaCun.nameroles = "Sói Băng";
                }
                else if (id == GlobalFunctionMaCun.plr12)
                {
                    GlobalFunctionMaCun.nameroles = "Tiên Tri";
                }
                else if (id == GlobalFunctionMaCun.plr13)
                {
                    GlobalFunctionMaCun.nameroles = "Sát Nhân";
                }
                else if (id == GlobalFunctionMaCun.plr14)
                {
                    GlobalFunctionMaCun.nameroles = "Gái Điếm";
                }
                else if (id == GlobalFunctionMaCun.plr15)
                {
                    GlobalFunctionMaCun.nameroles = "Thầy Đồng";
                }
                else if (id == GlobalFunctionMaCun.plr16)
                {
                    GlobalFunctionMaCun.nameroles = "Sói Tri";
                }
                else return;
            }
        }

        public static async Task rolegiaoxu(ulong id)
        {
            if (id == GlobalFunctionMaCun.giaoxu1)
            {
                GlobalFunctionMaCun.checkgiaoxu = 1;
            }
            else if (id == GlobalFunctionMaCun.giaoxu2)
            {
                GlobalFunctionMaCun.checkgiaoxu = 1;
            }
            else if (id == GlobalFunctionMaCun.giaoxu3)
            {
                GlobalFunctionMaCun.checkgiaoxu = 1;
            }
            else if (id == GlobalFunctionMaCun.giaoxu4)
            {
                GlobalFunctionMaCun.checkgiaoxu = 1;
            }
            else if (id == GlobalFunctionMaCun.giaoxu5)
            {
                GlobalFunctionMaCun.checkgiaoxu = 1;
            }
            else if (id == GlobalFunctionMaCun.giaoxu6)
            {
                GlobalFunctionMaCun.checkgiaoxu = 1;
            }
            else if (id == GlobalFunctionMaCun.giaoxu7)
            {
                GlobalFunctionMaCun.checkgiaoxu = 1;
            }
            else if (id == GlobalFunctionMaCun.giaoxu8)
            {
                GlobalFunctionMaCun.checkgiaoxu = 1;
            }
            else if (id == GlobalFunctionMaCun.giaoxu9)
            {
                GlobalFunctionMaCun.checkgiaoxu = 1;
            }
            else if (id == GlobalFunctionMaCun.giaoxu10)
            {
                GlobalFunctionMaCun.checkgiaoxu = 1;
            }
            else if (id == GlobalFunctionMaCun.giaoxu11)
            {
                GlobalFunctionMaCun.checkgiaoxu = 1;
            }
            else if (id == GlobalFunctionMaCun.giaoxu12)
            {
                GlobalFunctionMaCun.checkgiaoxu = 1;
            }
            else if (id == GlobalFunctionMaCun.giaoxu13)
            {
                GlobalFunctionMaCun.checkgiaoxu = 1;
            }
            else if (id == GlobalFunctionMaCun.giaoxu14)
            {
                GlobalFunctionMaCun.checkgiaoxu = 1;
            }
            else if (id == GlobalFunctionMaCun.giaoxu15)
            {
                GlobalFunctionMaCun.checkgiaoxu = 1;
            }
            else if (id == GlobalFunctionMaCun.giaoxu16)
            {
                GlobalFunctionMaCun.checkgiaoxu = 1;
            }
            else if (id == GlobalFunctionMaCun.giaoxu17)
            {
                GlobalFunctionMaCun.checkgiaoxu = 1;
            }
            else GlobalFunctionMaCun.checkgiaoxu = 0;
        }

    }
}
