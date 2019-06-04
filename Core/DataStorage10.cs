using Neko_Test.Core.UserAccounts10;

using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

using Newtonsoft.Json;

using System.IO;

namespace Neko_Test.Core
{

    public static class DataStorage10

    {

        public static void SaveUserAccounts(IEnumerable<UserAccount10> accounts, string filePath)

        {

            string json = JsonConvert.SerializeObject(accounts, Formatting.Indented);

            File.WriteAllText(filePath, json);

        }



        public static IEnumerable<UserAccount10> LoadUserAccounts(string filePath)

        {

            if (!File.Exists(filePath)) return null;

            string json = File.ReadAllText(filePath);

            return JsonConvert.DeserializeObject<List<UserAccount10>>(json);

        }


        public static bool SaveExists(string filePath)

        {

            return File.Exists(filePath);

        }


    }

}