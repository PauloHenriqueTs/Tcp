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
            var meter = new HouseMeter { serialId = "344", count = "1115555", Switch = true };
            meters.Insert(0, meter);
        }

        private void GetMessages()
        {
            while (true)
            {
                var data = server.ReadTCP();
                var meter = new HouseMeter { serialId = data.value.serialId, count = data.value.count, Switch = data.value.Switch };
                // server.WriteTCP(data);
                meters.Insert(0, meter);
            }
        }
    }
}