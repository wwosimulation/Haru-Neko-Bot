using Neko_Test.Core.Scores;

using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;

using Newtonsoft.Json;

using System.IO;

namespace Neko_Test.Core
{

    public static class DataStorageClass

    {

        public static void SaveScores(IEnumerable<Score> accounts, string filePath)

        {

            string json = JsonConvert.SerializeObject(accounts, Formatting.Indented);

            File.WriteAllText(filePath, json);

        }



        public static IEnumerable<Score> LoadScores(string filePath)

        {

            if (!File.Exists(filePath)) return null;

            string json = File.ReadAllText(filePath);

            return JsonConvert.DeserializeObject<List<Score>>(json);

        }


        public static bool SaveExists(string filePath)

        {

            return File.Exists(filePath);

        }


    }

}