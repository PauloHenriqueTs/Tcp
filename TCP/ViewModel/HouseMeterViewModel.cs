using System;
using System.Collections.Generic;
using System.Text;
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
            houseMeter = new HouseMeter();
            StopCommand = new RelayCommand(Stop, param => true);
            client = new Client();
            Task.Run(() =>
            {
                while (true)
                {
                    var command = client.Read();
                    if (command.type == MeterCommandType.Switch)
                        Stop(this);
                }
            });
        }

        private void Stop(object obj)
        {
            client.send(new MeterCommand { type = MeterCommandType.Count, serialId = houseMeter.serialId, value = houseMeter.count });
            houseMeter.Switch = !houseMeter.Switch;
        }
    }
}