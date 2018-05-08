using BankScrapper.BB;
using BankScrapper.Models;
using BankScrapper.Nubank;
using BankScrapper.Utils;
using System;
using System.Configuration;

namespace BankScrapper.Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            LoginNubank();
            //LoginBB();
            Console.ReadKey();
        }

        async static void LoginNubank()
        {
            var connectionData = new NubankConnectionData
            {
                CPF = ConfigurationManager.AppSettings["CPF"].Replace("-", string.Empty).Replace(".", string.Empty),
                Password = ConfigurationManager.AppSettings["NubankPassword"]
            };

            var api = new NubankApi();

            await api.LoginAsync(connectionData.CPF, connectionData.Password);
        }

        async static void LoginBB()
        {
            var connectionData = new BancoDoBrasilConnectionData
            {
                Account = ConfigurationManager.AppSettings["ContaBB"],
                Agency = ConfigurationManager.AppSettings["AgenciaBB"],
                ElectronicPassword = ConfigurationManager.AppSettings["SenhaEletronicaBB"],
                HoldershipLevel = 1
            };

            Console.WriteLine("Realizando conexão com o Banco do Brasil. Por favor, aguarde...");
            

            using (var bbProvider = new BancoDoBrasilProvider(connectionData))
            {
                var account = await bbProvider.GetAccountAsync();
                var transactions = await bbProvider.GetTransactionsAsync();

                Console.WriteLine("Dados coletados a partir do Banco do Brasil");

                PrintAccountData(account);
            }
        }

        static void PrintAccountData(Account account)
        {
            Console.WriteLine();
            Console.WriteLine("Dados coletados da conta");
            Console.WriteLine($"Agencia: {account.Agency}");
            Console.WriteLine($"Número: {account.Number}");
            Console.WriteLine($"Tipo: {account.Type.GetDescription()}");
            Console.WriteLine($"Saldo atual: {account.CurrentBalance.ToBrazillianCurrency()}");

            if (account.Customer != null)
                Console.WriteLine($"Nome do cliente: {account.Customer.Name}");

            foreach (var extraInformation in account.ExtraInformation)
            {
                Console.WriteLine($"{extraInformation.Key}: {extraInformation.Value}");
            }
        }
    }
}