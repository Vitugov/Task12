﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using Task12.Model.Accounts;
using Task12.Model.Clients;

namespace Task12.Model
{
    internal class DataStorage
    {
        static internal DataStorage Current { get; set;}
        internal Dictionary<Client, List<Account>> Accounts { get; set; }
        
        static DataStorage()
        {
            Current = new DataStorage();
        }
        internal DataStorage()
        {
            Accounts = [];
        }
        internal void AddClient(Client client)
        {
            if (Accounts.ContainsKey(client))
                throw new InvalidOperationException("Client already is in the DataStorage");
            
            Accounts.Add(client, []);
        }

        internal void AddAccount(Account account)
        {
            var accountsList = GetAccounts(account.Client);
            if (accountsList.Contains(account))
                throw new InvalidOperationException("Account already is in the DataStorage");
            
            Accounts[account.Client].Add(account);
        }       

        internal void RemoveAccount(Account account)
        {
            var accountsList = GetAccounts(account.Client);
            if (!accountsList.Contains(account))
                throw new NullReferenceException("There is no Account in the DataStorage");
            
            Accounts[account.Client].Remove(account);
        }

        internal List<Account> GetAccounts(Client client)
        {
            if (!Accounts.TryGetValue(client, out var accountsList))
                throw new NullReferenceException("There is no Client in the DataStorage");
            return accountsList;
        }

        internal List<Client> GetClients()
        {
            return [.. Accounts.Keys];
        }
    }
}