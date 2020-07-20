using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace Amr.Model
{
    public class HouseMeter : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string nomePropriedade)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nomePropriedade));
        }

        private string _serialId;
        private string _count;

        public bool Switch { get; set; } = true;

        public string serialId
        {
            get { return _serialId; }
            set
            {
                _serialId = value;
                OnPropertyChanged(nameof(serialId));
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