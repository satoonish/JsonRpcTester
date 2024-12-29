using StreamJsonRpc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using JsonRpcTester.Core;

namespace JsonRpcTester.Server
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("JSON-RPC サーバーを起動しました");
            var listener = new TcpListener(IPAddress.Any, 5000);
            listener.Start();
            while (true)
            {
                var client = await listener.AcceptTcpClientAsync();
                _ = Task.Run(() => HandleClientAsync(client));
            }
        }

        private static async Task HandleClientAsync(TcpClient client)
        {
            using (var stream = client.GetStream())
            {
                var jsonRpc = new JsonRpc(stream, stream, new Program());
                jsonRpc.StartListening();
                await jsonRpc.Completion;
            }
        }

        [JsonRpcMethod("Message")]
        public string Message(string message)
        {
            Console.WriteLine($"ClientMessage: {message}");
            return "Success";
        }

        [JsonRpcMethod("GetWeathers")]
        public Weathers GetWeathers(string city)
        {
            if (city == "Tokyo")
            {
                return new Weathers(city, 22, 30, "曇り");
            }
            return null;
        }
    }
}
