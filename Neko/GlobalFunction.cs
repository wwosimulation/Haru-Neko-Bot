using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Neko_Test.Modules
{
    public class GlobalFunction
    {
        public static string filelocal = @"C:\Users\ADMIN\Desktop\Neko's Discord Bot\Neko's Test\Neko's Test\";

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






        private static string emotes;
        public static string emote
        {
            get { return emotes; }
            set { emotes = value; }
        }

        public static int MaxEmotes = 26;

        public static async Task emotesstring(string e)
        {
            if (e == "1")
            {
                GlobalFunction.emote = "<:TohruWeary:585492969025568799>";
            }
            else if (e == "2")
            {
                GlobalFunction.emote = "<:remsleepy:585492968182644758>";
            }
            else if (e == "3")
            {
                GlobalFunction.emote = "<:remBlush:585492968228519937>";
            }
            else if (e == "4")
            {
                GlobalFunction.emote = "<:LoveHeart:585492967880523821>";
            }
            else if (e == "5")
            {
                GlobalFunction.emote = "<:Kya:585507397993234556>";
            }
            else if (e == "6")
            {
                GlobalFunction.emote = "<:kannaWave:585492969008791580>";
            }
            else if (e == "7")
            {
                GlobalFunction.emote = "<:kannaPeek:585492968807464984>";
            }
            else if (e == "8")
            {
                GlobalFunction.emote = "<:kannanom:585492968962523160>";
            }
            else if (e == "9")
            {
                GlobalFunction.emote = "<:pillowYes:585492967649837197>";
            }
            else if (e == "10")
            {
                GlobalFunction.emote = "<:pillowNo:585492968274657300>";
            }
            else if (e == "11")
            {
                GlobalFunction.emote = "<:GWpdnlaugh:587152172161040396>";
            }
            else if (e == "12")
            {
                GlobalFunction.emote = "<:GWpdnXD:587152173922648070>";
            }
            else if (e == "13")
            {
                GlobalFunction.emote = "<:ReimuFacePalm:587152173595623424>";
            }
            else if (e == "14")
            {
                GlobalFunction.emote = "<:Naisu:587265138843844608>";
            }
            else if (e == "15")
            {
                GlobalFunction.emote = "<:SenkoThinking:589049035055169536>";
            }
            else if (e == "16")
            {
                GlobalFunction.emote = "<:SenkoPlease:589049212239347753>";
            }
            else if (e == "17")
            {
                GlobalFunction.emote = "<:SenkoListening:589049338622246922>";
            }
            else if (e == "18")
            {
                GlobalFunction.emote = "<:SenkoHi:589049277754507265>";
            }
            else if (e == "19")
            {
                GlobalFunction.emote = "<:SenkoBlush:589049141183905797>";
            }
            else if (e == "20")
            {
                GlobalFunction.emote = "<:VampySmug:590121887707693058>";
            }
            else if (e == "21")
            {
                GlobalFunction.emote = "<:RaphiOhM:590121887963676673>";
            }
            else if (e == "22")
            {
                GlobalFunction.emote = "<:RaphiWink:590121888043237390>";
            }
            else if (e == "23")
            {
                GlobalFunction.emote = "<:WhoDesu:590121888655605762>";
            }
            else if (e == "24")
            {
                GlobalFunction.emote = "<:WOW:590149641241231384>";
            }
            else if (e == "25")
            {
                GlobalFunction.emote = "<:WannaSee:590149643376001024>";
            }
            else if (e == "26")
            {
                GlobalFunction.emote = "<:owoAwoo:590149640356233220>";
            }
            else return;

        }
    }
}
