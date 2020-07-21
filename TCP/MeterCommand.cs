using System;
using System.Collections.Generic;
using System.Text;
using TCP.Model;

namespace TCP
{
    public class MeterCommand
    {
        public HouseMeter value { get; set; }
        public MeterCommandType type { get; set; }
    }

    public enum MeterCommandType
    {
        Switch,
        Count
    }
}