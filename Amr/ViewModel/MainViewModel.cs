using Amr.Model;
using Amr.Utils;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Threading;

namespace Amr.ViewModel
{
    public class MainViewModel
    {
        private Server server;
        public ObservableCollection<HouseMeter> meters { get; set; }

        private HubConnection connection;
        public HouseMeter Selected { get; set; }
        public ICommand AddCommand { get; set; }

        public MainViewModel()
        {
            AddCommand = new RelayCommand(Add, e => true);
            server = new Server();
            meters = new ObservableCollection<HouseMeter> { new HouseMeter { serialId = "1", Switch = false, count = "35" } };
            BindingOperations.EnableCollectionSynchronization(meters, new object());
            connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5001/ChatHub")
                .Build();
            Task.Run(GetMessages);
        }

        private void Add(object o)
        {
            connection.SendAsync("ReceiveMessage", "Hello Workd");
            server.WriteTCP(new MeterCommand { value = Selected, type = MeterCommandType.Switch });
        }

        private async Task GetMessages()
        {
            await connection.StartAsync();
            server.server.DataReceived += (sender, message) =>
            {
                var data = server.ReadTCP(message);

                if (!meters.Any(m => m.serialId == data.value.serialId))
                {
                    meters.Add(data.value);
                }
                else
                {
                    var meter = meters.FirstOrDefault(m => m.serialId == data.value.serialId);
                    meters[meters.IndexOf(meter)].count = data.value.count;
                    //meters.Remove(meter);
                }
            };
        }
    }
}