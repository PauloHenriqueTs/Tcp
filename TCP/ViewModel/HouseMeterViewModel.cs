using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using TCP.Model;
using TCP.Utils;

namespace TCP.ViewModel
{
    internal class HouseMeterViewModel
    {
        public HouseMeter houseMeter { get; set; }
        public ICommand StopCommand { get; set; }

        private Client client;

        public HouseMeterViewModel()
        {
            try
            {
                houseMeter = new HouseMeter();

                StopCommand = new RelayCommand(Stop, param => true);
                client = new Client();
                Task.Run(GetCommand);
                Task.Run(SendCountCommand);
            }
            catch (Exception)
            {
            }
        }

        private void Stop(object obj)
        {
            //  client.send(new MeterCommand { type = MeterCommandType.Count, serialId = houseMeter.serialId, value = houseMeter.count });
            houseMeter.Switch = !houseMeter.Switch;
        }

        private void GetCommand()
        {
            while (true)
            {
                var command = client.Read();
                if (command.type == MeterCommandType.Switch && command.serialId == houseMeter.serialId)
                    Stop(this);
            }
        }

        private async void SendCountCommand()
        {
            while (true)
            {
                if (!String.IsNullOrEmpty(houseMeter.serialId))
                {
                    await Task.Delay(500);
                    client.send(new MeterCommand { type = MeterCommandType.Count, serialId = houseMeter.serialId, value = houseMeter.count });
                }
            }
        }
    }
}