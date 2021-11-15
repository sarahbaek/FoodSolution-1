using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using FoodClassLib;

namespace FoodTcpServer
{
    public class FoodServer
    {
        string serverCertificateFile = @"D:/Certificates/ServerSSL.pfx";
        bool clientCertificateRequired = false;
        bool checkCertificateRevocation = true;
        SslProtocols enabledSSLProtocols = SslProtocols.Tls;
        bool leaveInnerStreamOpen = false;
        X509Certificate serverCertificate;

        // Fake data
        private static List<Food> _food = new List<Food>()
        {
            new Food() {Id = 1, Name = "Cornflakes", InStock = 5, LowLevel = 5},
            new Food() {Id = 2, Name = "Cola", InStock = 15, LowLevel = 10},
            new Food() {Id = 3, Name = "Milk, low fat", InStock = 12, LowLevel = 15},
            new Food() {Id = 4, Name = "Chocolate", InStock = 7, LowLevel = 5},
            new Food() {Id = 5, Name = "Cookie", InStock = 5, LowLevel = 10}
        };

        public void StartServer()
        {
            serverCertificate = new X509Certificate(serverCertificateFile, "password");

            TcpListener listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 10100);
            listener.Start();
            Console.WriteLine("Server started.");

            while (true)
            {
                TcpClient socket = listener.AcceptTcpClient();
                Task.Run(() => HandleClient(socket));
            }
        }

        private void HandleClient(TcpClient socket)
        {
            NetworkStream unsecureStream = socket.GetStream();

            SslStream sslStream = new SslStream(unsecureStream, leaveInnerStreamOpen);

            sslStream.AuthenticateAsServer(serverCertificate, clientCertificateRequired, enabledSSLProtocols, checkCertificateRevocation);


            StreamReader reader = new StreamReader(sslStream);
            StreamWriter writer = new StreamWriter(sslStream);

            var request = reader.ReadLine();
            var response = HandleRequest(request);

            writer.WriteLine(response);
            writer.Flush();
            socket.Close();
        }

        private string HandleRequest(string request)
        {
            if (request.StartsWith("Show"))
            {
                return ShowAll();
            }
            else if (request.StartsWith("Buy"))
            {
                return Buy(request);
            }
            else
            {
                return $"Request method was not recognized. Your request was: {request}";
            }
        }

        private string ShowAll()
        {
            string json = JsonSerializer.Serialize(_food);
            return json;
        }

        private string Buy(string request)
        {
            var requestArray = request.Split(" ");
            int foodId = 0;
            int amount = 0;
            bool idParsed = int.TryParse(requestArray[1], out foodId);
            bool amountParsed = int.TryParse(requestArray[2], out amount);

            if (!idParsed) return $"Id must be a number. Your id was: {requestArray[1]}";
            if (!amountParsed) return $"Amount must be a number. Your amount was: {requestArray[2]}";
            
            // TryParse can be used in one step with the if statement

            Food foodItem = _food.Find(f => f.Id == foodId);

            if (foodItem == null) return $"No item was found for id: {foodId}";

            if (foodItem.InStock < amount)
            {
                amount = foodItem.InStock;
                foodItem.InStock = 0;
            }
            else
            {
                foodItem.InStock -= amount;
            }

            return $"{amount} {foodItem.Name}";
        }
    }
}
