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
            //LoginNubank();
            LoginBB();
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

            var auth = await api.LoginAsync(connectionData.CPF, connectionData.Password);
            var customer = await api.GetCustomerAsync(auth);
            var account = await api.GetAccountAsync(auth);

            //var billsSummary = await api.GetToken(auth.Links.Events.Href, auth.AccessToken);




        }

        async static void LoginBB()
        {
            var connectionData = new BancoDoBrasilConnectionData
            {
                Account = ConfigurationManager.AppSettings["ContaBB"],
                Agency = ConfigurationManager.AppSettings["AgenciaBB"],
                ElectronicPassword = ConfigurationManager.AppSettings["SenhaEletronicaBB"]
            };

            Console.WriteLine("Realizando conexão com o Banco do Brasil. Por favor, aguarde...");

            using (var bbProvider = BancoDoBrasilProvider.New(connectionData))
            {
                var result = await bbProvider.GetResultAsync();

                Console.WriteLine("Dados coletados a partir do Banco do Brasil:");

                var json = result.ToString();

                Console.WriteLine();
                Console.WriteLine(json);
            }
        }
    }
}