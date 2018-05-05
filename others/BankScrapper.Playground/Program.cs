using BankScrapper.BB;
using BankScrapper.Utils;
using System;
using System.Configuration;

namespace BankScrapper.Playground
{
    class Program
    {
        static void Main(string[] args)
        {
            LoginBB();
            Console.ReadKey();
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

                Console.WriteLine();
                Console.WriteLine("Dados coletados a partir do Banco do Brasil");
                Console.WriteLine($"Ag: {account.Agency}");
                Console.WriteLine($"Cc: {account.Number}");
                Console.WriteLine($"Tipo: {account.PersonType.GetDescription()}");
                Console.WriteLine($"Titularidade: {account.HoldershipLevel}º titular");
                Console.WriteLine($"Saldo atual: {account.CurrentBalance.ToBrazillianCurrency()}");
                Console.WriteLine($"Nome do cliente: {account.CustomerName}");

                var transactions = await bbProvider.GetTransactionsAsync();
            }
        }
    }
}