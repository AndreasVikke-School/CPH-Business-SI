using System;
using System.Threading.Tasks;
using MiniProject1.ClassLib;

namespace MiniProject1.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // GrpcClient.Run2().Wait();
            // Run().Wait();
        }

        static async Task Run() {
            System.Console.WriteLine(await SoapConnector.ISBN10Validator("9999999999"));
            System.Console.WriteLine(await SoapConnector.ISBN13Validator("9780007103072"));
        }
    }
}