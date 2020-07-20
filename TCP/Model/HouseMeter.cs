using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace TCP.Model
{
    internal class HouseMeter : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string nomePropriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
        }

        private string _serialId;
        private string _count;

        public bool Switch { get; set; } = true;

        public HouseMeter()
        {
            Task.Run(async () =>
            {
                int i = 0;
                while (true)
                {
                    await Task.Delay(200);
                    if (Switch)
                    {
                        count = (i++).ToString();
                    }
                }
            });
        }

        public string serialId
        {
            get { return _serialId; }
            set
            {
                _serialId = value;
                2OnPropertyChanged(nameof(serialId));
            }
        }

        public string count
        {
            get { return _count; }
            set
            {
                _count = value;
                OnPropertyChanged(nameof(count));
            }
        }
    }
}