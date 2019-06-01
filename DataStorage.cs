using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Neko_Test
{
    class DataStorage
    {
        private static Dictionary<string, string> pairs = new Dictionary<string, string>();
        public static void addpairs(string key, string value)
        {
            pairs.Add(key, value);
            SaveData();
        }
        public static bool paircount(string key)
        {
            if (pairs.ContainsKey(key))
            {
                return false;
            }
            else
            {
                return true;
            }

        }
        static DataStorage()
        {
            if (!getorcreatefile("Data.json")) return;
            string json = File.ReadAllText("Data.json");

            pairs = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
        }

        public static void SaveData()
        {
            string json = JsonConvert.SerializeObject(pairs, Formatting.Indented);
            File.WriteAllText("Data.json", json);
        }
        private static bool getorcreatefile(string file)
        {
            if (!File.Exists(file))
            {
                File.WriteAllText(file, "");
                SaveData();
                return false;
            }
            return true;
        }

    }
}

