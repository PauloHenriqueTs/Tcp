﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace Amr.Utils
{
    public class Server
    {
        private TcpListener server;
        private NetworkStream stream;

        public Server()
        {
            server = new TcpListener(GetLocalIPAddress(), 23);

            server.Start();
        }

        public MeterCommand ReadTCP()
        {
            TcpClient client = server.AcceptTcpClient();
            stream = client.GetStream();

            byte[] myReadBuffer = new byte[1024];
            var numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);
            var receive = Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead);
            return JsonSerializer.Deserialize<MeterCommand>(receive);
        }

        public void WriteTCP(MeterCommand command)
        {
            TcpClient client = server.AcceptTcpClient();
            stream = client.GetStream();
            if (stream.CanWrite)
            {
                var sender = JsonSerializer.Serialize(command);
                byte[] message = Encoding.ASCII.GetBytes(sender);
                stream.Write(message, 0, message.Length);
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