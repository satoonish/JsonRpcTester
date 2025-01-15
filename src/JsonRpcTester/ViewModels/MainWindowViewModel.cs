using JsonRpcTester.Core;
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

        public ReactiveProperty<string> Log { get; }
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
                var result = await JsonRpcClient.ExecuteAsync(JsonRpcMethod.Value, SendMessage.Value);
                Console.WriteLine($"Result: {result}");
                Log.Value = $"Result: {result}";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Log.Value = "Failed";
            }
        }
    }
}
