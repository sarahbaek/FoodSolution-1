using System;

namespace FoodTcpServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Food Server Console Application");
            FoodServer server = new FoodServer();
            server.StartServer();

            Console.ReadKey();
        }
    }
}
