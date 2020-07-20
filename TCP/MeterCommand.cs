using System;
using System.Collections.Generic;
using System.Text;

namespace TCP
{
    public class MeterCommand
    {
        public string serialId { get; set; }
        public string value { get; set; }
        public MeterCommandType type { get; set; }
    }

    public enum MeterCommandType
    {
        Switch,
        Count
    }
}