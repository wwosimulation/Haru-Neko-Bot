using System;
using System.Collections.Generic;
using System.Text;

namespace Neko_Test.Modules
{
    public class GlobalFunction
    {
        internal static object gamemodes;
        private static string gamecode;
        public static string gamecodes
        {
            get { return gamecode; }
            set { gamecode = value; }
        }
        private static string won;
        public static string wons
        {
            get { return won; }
            set { won = value; }
        }
        private static string gamestatuss;
        public static string gamestatus
        {
            get { return gamestatuss; }
            set { gamestatuss = value; }
        }
    }
}
