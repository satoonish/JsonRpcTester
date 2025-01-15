using StreamJsonRpc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace JsonRpcTester.Core
{
    public class JsonRpcClient
    {
        public static async Task<string> ExecuteAsync(string targetName, string argument)
        {
            using (var client = new TcpClient())
            {
                await client.ConnectAsync("localhost", 5000);
                using (var stream = client.GetStream())
                {
                    var jsonRpc = new JsonRpc(stream, stream);
                    jsonRpc.StartListening();

                    var result = await jsonRpc.InvokeAsync<object>(targetName, argument);
                    jsonRpc.Dispose();
                    return result.ToString();
                }
            }
        }
    }
}
