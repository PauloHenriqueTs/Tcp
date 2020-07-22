using SimpleTcp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Amr.Utils
{
    public class Server
    {
        public TcpServer server;

        public Server()
        {
            server = new TcpServer(GetLocalIPAddress().ToString(), 23, false, null, null);
            server.Start();
        }

        public MeterCommand ReadTCP(DataReceivedFromClientEventArgs message)
        {
            var received = Encoding.UTF8.GetString(message.Data, 0, message.Data.Length);
            return JsonSerializer.Deserialize<MeterCommand>(received);
        }

        public void WriteTCP(MeterCommand command)
        {
            var sender = JsonSerializer.Serialize(command);

            foreach (var item in server.GetClients())
            {
                server.Send(item, Encoding.UTF8.GetBytes(sender));
            }
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