using System;
using ViewModel;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            SetupMySQLConnection connection = new SetupMySQLConnection();
            Console.WriteLine($"MySQL version : {connection.Connection.ServerVersion}");
        }
    }
}
