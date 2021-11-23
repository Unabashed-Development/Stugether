using System;
using Gateway;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            SetupSQLConnection connection = new SetupSQLConnection();
            Console.WriteLine($"MySQL version : {connection.Connection.ServerVersion}");

        }
    }
}
