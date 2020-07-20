using Amr.Model;
using Amr.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Amr.ViewModel
{
    public class MainViewModel
    {
        private Server server;
        public ObservableCollection<HouseMeter> meters { get; set; }

        public MainViewModel()
        {
            server = new Server();
            meters = new ObservableCollection<HouseMeter> { new HouseMeter { serialId = "1", Switch = false, count = "35" } };
            Task.Run(GetMessages);
        }

        private void GetMessages()
        {
            while (true)
            {
                var data = server.ReadTCP();
                var meter = new HouseMeter { serialId = data.value.serialId, count = data.value.count, Switch = data.value.Switch };
                meters.Insert(0, meter);
            }
        }
    }
}