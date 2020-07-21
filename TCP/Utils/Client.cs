using SimpleTcp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using TcpClient = SimpleTcp.TcpClient;

namespace TCP.Utils
{
    public class Client
    {
        public TcpClient listener;

        public Client()
        {
            listener = new TcpClient(GetLocalIPAddress().ToString(), 23, false, null, null);
            listener.Connect();
        }

        public void send(MeterCommand command)
        {
            var sender = JsonSerializer.Serialize(command);
            listener.Send(Encoding.UTF8.GetBytes(sender));
        }

        public MeterCommand Read(DataReceivedFromServerEventArgs message)
        {
            var received = Encoding.UTF8.GetString(message.Data, 0, message.Data.Length);
            return JsonSerializer.Deserialize<MeterCommand>(received);
        }

        public static IPAddress GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip;
                }
            }
            throw new Exception("No network adapters with an IPv4 address in the system!");
        }
    }
}