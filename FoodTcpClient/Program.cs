using System;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;

namespace FoodTcpClient
{
    class Program
    {
        static bool leaveInnerStreamOpen = false;

        static void Main(string[] args)
        {
            Console.WriteLine("Food Server Console Application");

            Console.WriteLine("\n-----------------------------------\n");

            using (TcpClient socket = new TcpClient("localhost", 10100))
            {
                NetworkStream unsecureStream = socket.GetStream();

                SslStream sslStream = new SslStream(unsecureStream, leaveInnerStreamOpen);
                sslStream.AuthenticateAsClient("FakeServer");


                StreamWriter writer = new StreamWriter(sslStream);
                StreamReader reader = new StreamReader(sslStream);

                Console.WriteLine("Example 1: Show request");
                Console.WriteLine("Request text: Show");
                writer.WriteLine("Show");
                writer.Flush();
                string showResponse = reader.ReadLine();
                socket.Close();
                Console.WriteLine("Response text: " + showResponse);
            }
            
            //Console.WriteLine("\n-----------------------------------\n");

            //using (TcpClient socket = new TcpClient("localhost", 10100))
            //{
            //    NetworkStream ns = socket.GetStream();
            //    StreamWriter writer = new StreamWriter(ns);
            //    StreamReader reader = new StreamReader(ns);

            //    Console.WriteLine("Example 2: Buy request");
            //    Console.WriteLine("Request text: Buy 1 3");
            //    writer.WriteLine("Buy 1 3");
            //    writer.Flush();
            //    string buyResponse = reader.ReadLine();
            //    socket.Close();
            //    Console.WriteLine("Response text: " + buyResponse);
            //}
            
            //Console.WriteLine("\n-----------------------------------\n");

            Console.WriteLine("Press any key to close the console...");
            Console.ReadKey();
        }
    }
}
