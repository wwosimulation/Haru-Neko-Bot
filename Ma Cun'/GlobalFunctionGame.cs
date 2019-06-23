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

        public static async Task showgameroles()
        {
            string roleselect = null;
            if (GlobalFunctionMaCun.plr1 != 0)
            {
                await GlobalFunctionMaCun.rolestring("1", "ten");
                roleselect = "" + roleselect + "\n" + GlobalFunctionMaCun.nameroles + "";
            }
            if (GlobalFunctionMaCun.plr2 != 0)
            {
                await GlobalFunctionMaCun.rolestring("2", "ten");
                roleselect = "" + roleselect + "\n" + GlobalFunctionMaCun.nameroles + "";
            }
            if (GlobalFunctionMaCun.plr3 != 0)
            {
                await GlobalFunctionMaCun.rolestring("3", "ten");
                roleselect = "" + roleselect + "\n" + GlobalFunctionMaCun.nameroles + "";
            }
            if (GlobalFunctionMaCun.plr4 != 0)
            {
                await GlobalFunctionMaCun.rolestring("4", "ten");
                roleselect = "" + roleselect + "\n" + GlobalFunctionMaCun.nameroles + "";
            }
            if (GlobalFunctionMaCun.plr5 != 0)
            {
                await GlobalFunctionMaCun.rolestring("5", "ten");
                roleselect = "" + roleselect + "\n" + GlobalFunctionMaCun.nameroles + "";
            }
            if (GlobalFunctionMaCun.plr6 != 0)
            {
                await GlobalFunctionMaCun.rolestring("6", "ten");
                roleselect = "" + roleselect + "\n" + GlobalFunctionMaCun.nameroles + "";
            }
            if (GlobalFunctionMaCun.plr7 != 0)
            {
                await GlobalFunctionMaCun.rolestring("7", "ten");
                roleselect = "" + roleselect + "\n" + GlobalFunctionMaCun.nameroles + "";
            }
            if (GlobalFunctionMaCun.plr8 != 0)
            {
                await GlobalFunctionMaCun.rolestring("8", "ten");
                roleselect = "" + roleselect + "\n" + GlobalFunctionMaCun.nameroles + "";
            }
            if (GlobalFunctionMaCun.plr9 != 0)
            {
                await GlobalFunctionMaCun.rolestring("9", "ten");
                roleselect = "" + roleselect + "\n" + GlobalFunctionMaCun.nameroles + "";
            }
            if (GlobalFunctionMaCun.plr10 != 0)
            {
                await GlobalFunctionMaCun.rolestring("10", "ten");
                roleselect = "" + roleselect + "\n" + GlobalFunctionMaCun.nameroles + "";
            }
            if (GlobalFunctionMaCun.plr11 != 0)
            {
                await GlobalFunctionMaCun.rolestring("11", "ten");
                roleselect = "" + roleselect + "\n" + GlobalFunctionMaCun.nameroles + "";
            }
            if (GlobalFunctionMaCun.plr12 != 0)
            {
                await GlobalFunctionMaCun.rolestring("12", "ten");
                roleselect = "" + roleselect + "\n" + GlobalFunctionMaCun.nameroles + "";
            }
            if (GlobalFunctionMaCun.plr13 != 0)
            {
                await GlobalFunctionMaCun.rolestring("13", "ten");
                roleselect = "" + roleselect + "\n" + GlobalFunctionMaCun.nameroles + "";
            }
            if (GlobalFunctionMaCun.plr14 != 0)
            {
                await GlobalFunctionMaCun.rolestring("14", "ten");
                roleselect = "" + roleselect + "\n" + GlobalFunctionMaCun.nameroles + "";
            }
            if (GlobalFunctionMaCun.plr15 != 0)
            {
                await GlobalFunctionMaCun.rolestring("15", "ten");
                roleselect = "" + roleselect + "\n" + GlobalFunctionMaCun.nameroles + "";
            }
            if (GlobalFunctionMaCun.plr16 != 0)
            {
                await GlobalFunctionMaCun.rolestring("16", "ten");
                roleselect = "" + roleselect + "\n" + GlobalFunctionMaCun.nameroles + "";
            }
            if (GlobalFunctionMaCun.plr17 != 0)
            {
                await GlobalFunctionMaCun.rolestring("17", "ten");
                roleselect = "" + roleselect + "\n" + GlobalFunctionMaCun.nameroles + "";
            }
            if (GlobalFunctionMaCun.plr18 != 0)
            {
                await GlobalFunctionMaCun.rolestring("18", "ten");
                roleselect = "" + roleselect + "\n" + GlobalFunctionMaCun.nameroles + "";
            }
            if (GlobalFunctionMaCun.plr19 != 0)
            {
                await GlobalFunctionMaCun.rolestring("19", "ten");
                roleselect = "" + roleselect + "\n" + GlobalFunctionMaCun.nameroles + "";
            }
            if (GlobalFunctionMaCun.plr20 != 0)
            {
                await GlobalFunctionMaCun.rolestring("20", "ten");
                roleselect = "" + roleselect + "\n" + GlobalFunctionMaCun.nameroles + "";
            }
            if (GlobalFunctionMaCun.plr21 != 0)
            {
                await GlobalFunctionMaCun.rolestring("21", "ten");
                roleselect = "" + roleselect + "\n" + GlobalFunctionMaCun.nameroles + "";
            }

            GlobalFunctionMaCun.nameroles = roleselect;
        }

        public static async Task voting(ulong user, int target)
        {
            if (user != GlobalFunctionMaCun.pha)
            {
                if (user == GlobalFunctionMaCun.plr1) { GlobalFunctionMaCun.plr1p = target; }
                else if (user == GlobalFunctionMaCun.plr2) { GlobalFunctionMaCun.plr2p = target; }
                else if (user == GlobalFunctionMaCun.plr3) { GlobalFunctionMaCun.plr3p = target; }
                else if (user == GlobalFunctionMaCun.plr4) { GlobalFunctionMaCun.plr4p = target; }
                else if (user == GlobalFunctionMaCun.plr5) { GlobalFunctionMaCun.plr5p = target; }
                else if (user == GlobalFunctionMaCun.plr6) { GlobalFunctionMaCun.plr6p = target; }
                else if (user == GlobalFunctionMaCun.plr7) { GlobalFunctionMaCun.plr7p = target; }
                else if (user == GlobalFunctionMaCun.plr8) { GlobalFunctionMaCun.plr8p = target; }
                else if (user == GlobalFunctionMaCun.plr9) { GlobalFunctionMaCun.plr9p = target; }
                else if (user == GlobalFunctionMaCun.plr10) { GlobalFunctionMaCun.plr10p = target; }
                else if (user == GlobalFunctionMaCun.plr11) { GlobalFunctionMaCun.plr11p = target; }
                else if (user == GlobalFunctionMaCun.plr12) { GlobalFunctionMaCun.plr12p = target; }
                else if (user == GlobalFunctionMaCun.plr13) { GlobalFunctionMaCun.plr13p = target; }
                else if (user == GlobalFunctionMaCun.plr14) { GlobalFunctionMaCun.plr14p = target; }
                else if (user == GlobalFunctionMaCun.plr15) { GlobalFunctionMaCun.plr15p = target; }
                else if (user == GlobalFunctionMaCun.plr16) { GlobalFunctionMaCun.plr16p = target; }
                else if (user == GlobalFunctionMaCun.plr17) { GlobalFunctionMaCun.plr17p = target; }
                else if (user == GlobalFunctionMaCun.plr18) { GlobalFunctionMaCun.plr18p = target; }
                else if (user == GlobalFunctionMaCun.plr19) { GlobalFunctionMaCun.plr19p = target; }
                else if (user == GlobalFunctionMaCun.plr20) { GlobalFunctionMaCun.plr20p = target; }
                else if (user == GlobalFunctionMaCun.plr21) { GlobalFunctionMaCun.plr21p = target; }
                else return;
            }
            else return;
        }

        public static async Task vedem()
        {
            GlobalFunctionMaCun.channel1 = 1;
            GlobalFunctionMaCun.channel2 = 1;
            GlobalFunctionMaCun.channel3 = 1;
            GlobalFunctionMaCun.channel4 = 1;
            GlobalFunctionMaCun.channel5 = 1;
            GlobalFunctionMaCun.channel6 = 1;
            GlobalFunctionMaCun.channel7 = 1;
            GlobalFunctionMaCun.channel8 = 1;
            GlobalFunctionMaCun.channel9 = 1;
            GlobalFunctionMaCun.channel10 = 1;
            GlobalFunctionMaCun.channel11 = 1;
            GlobalFunctionMaCun.channel12 = 1;
            GlobalFunctionMaCun.channel13 = 1;
            GlobalFunctionMaCun.channel14 = 1;
            GlobalFunctionMaCun.channel15 = 1;
            GlobalFunctionMaCun.channel16 = 1;
            GlobalFunctionMaCun.channel17 = 1;
            GlobalFunctionMaCun.channel18 = 1;
            GlobalFunctionMaCun.channel19 = 1;
            GlobalFunctionMaCun.channel20 = 1;
            GlobalFunctionMaCun.channel21 = 1;
            GlobalFunctionMaCun.plr1p = 0;
            GlobalFunctionMaCun.plr2p = 0;
            GlobalFunctionMaCun.plr3p = 0;
            GlobalFunctionMaCun.plr4p = 0;
            GlobalFunctionMaCun.plr5p = 0;
            GlobalFunctionMaCun.plr6p = 0;
            GlobalFunctionMaCun.plr7p = 0;
            GlobalFunctionMaCun.plr8p = 0;
            GlobalFunctionMaCun.plr9p = 0;
            GlobalFunctionMaCun.plr10p = 0;
            GlobalFunctionMaCun.plr11p = 0;
            GlobalFunctionMaCun.plr12p = 0;
            GlobalFunctionMaCun.plr13p = 0;
            GlobalFunctionMaCun.plr14p = 0;
            GlobalFunctionMaCun.plr15p = 0;
            GlobalFunctionMaCun.plr16p = 0;
            GlobalFunctionMaCun.plr17p = 0;
            GlobalFunctionMaCun.plr18p = 0;
            GlobalFunctionMaCun.plr19p = 0;
            GlobalFunctionMaCun.plr20p = 0;
            GlobalFunctionMaCun.plr21p = 0;
            GlobalFunctionMaCun.gamestatus = 1;
            GlobalFunctionMaCun.thayboi = 1;
            GlobalFunctionMaCun.tientri = 1;
            GlobalFunctionMaCun.soitri = 1;
            GlobalFunctionMaCun.votesong = 0;
            GlobalFunctionMaCun.votechet = 0;
            GlobalFunctionMaCun.pha = 1;
            GlobalFunctionMaCun.daycount++;
            GlobalFunctionMaCun.treo = 0;
        }

    }
}
