﻿using Discord.WebSocket;

using System;

using System.Collections.Generic;

using System.Linq;

using System.Text;

using System.Threading.Tasks;



namespace Neko_Test.Core.UserAccounts

{

    public static class UserAccounts

    {

        private static List<UserAccount> accounts;



        private static string accountsFile = "UserAccounts.json";



        static UserAccounts()

        {

            if (DataStorage.SaveExists(accountsFile))

            {

                accounts = DataStorage.LoadUserAccounts(accountsFile).ToList();

            }

            else

            {

                accounts = new List<UserAccount>();

                SaveAccounts();

            }

        }



        public static void SaveAccounts()

        {

            DataStorage.SaveUserAccounts(accounts, accountsFile);

        }



        public static UserAccount GetAccount(SocketUser user)

        {

            return GetOrCreateAccount(user.Id);

        }



        private static UserAccount GetOrCreateAccount(ulong id)

        {

            var result = from a in accounts

                         where a.ID == id

                         select a;



            var account = result.FirstOrDefault();

            if (account == null) account = CreateUserAccount(id);

            return account;

        }



        private static UserAccount CreateUserAccount(ulong id)

        {

            var newAccount = new UserAccount()

            {
                ID = id,
                points = 0,
                roses = 0,
                plrroses = 0
            };

            accounts.Add(newAccount);

            SaveAccounts();

            return newAccount;

        }

    }

}