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
        private static ulong jaileds;
        public static ulong jailed
        {
            get { return jaileds; }
            set { jaileds = value; }
        }
        private static ulong jailers;
        public static ulong jailer
        {
            get { return jailers; }
            set { jailers = value; }
        }
        private static ulong jailerammos;
        public static ulong jailerammo
        {
            get { return jailerammos; }
            set { jailerammos = value; }
        }
        private static string gametimes;
        public static string gametime
        {
            get { return gametimes; }
            set { gametimes = value; }
        }
        private static ulong getalluserr;
        public static ulong getalluser
        {
            get { return getalluserr; }
            set { getalluserr = value; }
        }
        private static string matchresults;
        public static string matchresult
        {
            get { return matchresults; }
            set { matchresults = value; }
        }
        private static string matchmembers;
        public static string matchmember
        {
            get { return matchmembers; }
            set { matchmembers = value; }
        }
        private static ulong daycounts;
        public static ulong daycount
        {
            get { return daycounts; }
            set { daycounts = value; }
        }
        private static int playerjoineds;
        public static int playerjoin
        {
            get { return playerjoineds; }
            set { playerjoineds = value; }
        }
    }
}
