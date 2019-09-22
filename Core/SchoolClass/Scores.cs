using Discord.WebSocket;

using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;



namespace Neko_Test.Core.Scores

{

    public static class Scores

    {

        public static List<Score> accounts;



        public static string accountsFile = "Scores.json";



        static Scores()

        {

            if (DataStorageClass.SaveExists(accountsFile))

            {

                accounts = DataStorageClass.LoadScores(accountsFile).ToList();

            }

            else

            {

                accounts = new List<Score>();

                SaveAccounts();

            }

        }



        public static void SaveAccounts()

        {

            DataStorageClass.SaveScores(accounts, accountsFile);

        }
  



        public static Score GetAccount(ulong id)

        {

            return GetOrCreateAccount(id);

        }



        public static Score GetAccountUlong(ulong id)

        {

            return GetOrCreateAccount(id);

        }



        private static Score GetOrCreateAccount(ulong id)

        {

            var result = from a in accounts

                         where a.ID == id

                         select a;



            var account = result.FirstOrDefault();

            if (account == null) account = CreateScore(id);

            return account;

        }



        private static Score CreateScore(ulong id)

        {

            var newAccount = new Score()

            {
                ID = id,
                diem = 0
            };

            accounts.Add(newAccount);

            SaveAccounts();

            return newAccount;

        }

    }

}