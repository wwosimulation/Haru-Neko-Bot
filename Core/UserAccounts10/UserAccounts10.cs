using Discord.WebSocket;

using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;



namespace Neko_Test.Core.UserAccounts10

{

    public static class UserAccounts10

    {

        private static List<UserAccount10> accounts;



        private static string accountsFile = "UserAccounts10.json";



        static UserAccounts10()

        {

            if (DataStorage10.SaveExists(accountsFile))

            {

                accounts = DataStorage10.LoadUserAccounts(accountsFile).ToList();

            }

            else

            {

                accounts = new List<UserAccount10>();

                SaveAccounts();

            }

        }



        public static void SaveAccounts()

        {

            DataStorage10.SaveUserAccounts(accounts, accountsFile);

        }



        public static UserAccount10 GetAccount(SocketUser user)

        {

            return GetOrCreateAccount(user.Id);

        }



        private static UserAccount10 GetOrCreateAccount(ulong id)

        {

            var result = from a in accounts

                         where a.ID == id

                         select a;



            var account = result.FirstOrDefault();

            if (account == null) account = CreateUserAccount(id);

            return account;

        }



        private static UserAccount10 CreateUserAccount(ulong id)

        {

            var newAccount = new UserAccount10()

            {
                ID = id,
                emote = false,
                None1 = null,
                None2 = null,
                None3 = null,
                None4 = 0,
                None5 = 0,
                None6 = 0,
                None7 = 0,
                None8 = 0,
                None9 = 0
            };

            accounts.Add(newAccount);

            SaveAccounts();

            return newAccount;

        }

    }

}