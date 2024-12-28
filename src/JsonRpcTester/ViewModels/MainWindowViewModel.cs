using Reactive.Bindings;
using StreamJsonRpc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JsonRpcTester.ViewModels
{
    public class MainWindowViewModel
    {
        public ReactiveProperty<string> JsonRpcMethod { get; }
            = new ReactiveProperty<string>();

        public ReactiveProperty<string> SendMessage { get; }
            = new ReactiveProperty<string>();

        public AsyncReactiveCommand SendCommand { get; }
            = new AsyncReactiveCommand();

        public MainWindowViewModel()
        {
            SendCommand.Subscribe(ExeSendCommandAsync);
        }

        ~MainWindowViewModel()
        {
            SendCommand.Dispose();
        }

        private async Task ExeSendCommandAsync()
        {
            try
            {
                using (var client = new TcpClient())
                {
                    await client.ConnectAsync("localhost", 5000);
                    using (var stream = client.GetStream())
                    {
                        var jsonRpc = new JsonRpc(stream, stream);
                        jsonRpc.StartListening();
                        
                        var result = await jsonRpc.InvokeAsync<string>(JsonRpcMethod.Value, SendMessage.Value);
                        Console.WriteLine($"Result: {result}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
