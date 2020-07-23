using Amr.Model;
using Amr.Utils;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
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
        private HubConnection connection;

        public ObservableCollection<HouseMeter> meters { get; set; }

        public HouseMeter Selected { get; set; }
        public ICommand AddCommand { get; set; }

        private string Jwt = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiIyNTgyMTk5OC03MzJjLTQxMDctYTExMi00NjA5MDcyMTk1NTkiLCJuYmYiOjE1OTU0NjI5MDgsImV4cCI6MTU5NTQ3MDEwOCwiaWF0IjoxNTk1NDYyOTA4LCJpc3MiOiJNZXVTaXN0ZW1hIiwiYXVkIjoiaHR0cHM6Ly9sb2NhbGhvc3QifQ.uaDKvlH2vWUnRWkwBgYicy7HM9l8wFH5os4BVXFfzMg";

        public MainViewModel()
        {
            AddCommand = new RelayCommand(Add, e => true);
            server = new Server();
            meters = new ObservableCollection<HouseMeter> { new HouseMeter { serialId = "1", Switch = false, count = "35" } };
            BindingOperations.EnableCollectionSynchronization(meters, new object());
            Task.Run(GetMessages);
            Task.Run(signalr);
        }

        private async Task signalr()
        {
            connection = new HubConnectionBuilder()
             .WithUrl("https://localhost:5001/chathub",
              options =>
              {
                  options.Headers.Add("Authorization", "Bearer " + Jwt);
              })
             .Build();
            connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                var meter = JsonSerializer.Deserialize<MeterCommand>(message);
                server.WriteTCP(meter);
            });
            await connection.StartAsync();
        }

        private void Add(object o)
        {
            connection.InvokeAsync("SendMessage", "Hello", "Hello");
            server.WriteTCP(new MeterCommand { value = Selected, type = MeterCommandType.Switch });
        }

        private async Task GetMessages()
        {
            server.server.DataReceived += async (sender, message) =>
            {
                var data = server.ReadTCP(message);
                if (connection != null)
                    await connection.InvokeAsync("SendMessage", "Hello", JsonSerializer.Serialize<MeterCommand>(data));
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