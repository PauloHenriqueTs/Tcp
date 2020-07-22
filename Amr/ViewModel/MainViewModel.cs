using Amr.Model;
using Amr.Utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public HouseMeter Selected { get; set; }

        public ICommand AddCommand { get; set; }

        public MainViewModel()
        {
            AddCommand = new RelayCommand(Add, e => true);
            server = new Server();
            meters = new ObservableCollection<HouseMeter> { new HouseMeter { serialId = "1", Switch = false, count = "35" } };
            BindingOperations.EnableCollectionSynchronization(meters, new object());
            Task.Run(GetMessages);
        }

        private void Add(object o)
        {
            var s = Selected;

            server.WriteTCP(new MeterCommand { value = s, type = MeterCommandType.Switch });
        }

        private void GetMessages()
        {
            while (true)
            {
                var data = server.ReadTCP();
                if(data != null) {
                    if (!meters.Any(m => m.serialId == data.value.serialId))
                    {
                        meters.Insert(0, data.value);
                    }
                    else
                    {
                        var m = meters.FirstOrDefault(m => m.serialId == data.value.serialId).count = data.value.count;
                    }
                }
               

                // var meter = new HouseMeter { serialId = data.value.serialId, count = data.value.count, Switch = data.value.Switch };
                // server.WriteTCP(data);
            }
        }
    }
}