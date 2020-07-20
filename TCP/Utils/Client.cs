﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace TCP.Utils
{
    public class Client
    {
        private TcpClient client;
        private NetworkStream stream;

        public Client()
        {
            client = new TcpClient();
            var s = Dns.GetHostEntry(Dns.GetHostName());
            client.Connect(s.AddressList[1], 23);
            stream = client.GetStream();
        }

        public void send(MeterCommand command)
        {
            if (stream.CanWrite)
            {
                var sender = JsonSerializer.Serialize(command);
                byte[] message = Encoding.ASCII.GetBytes(sender);
                stream.Write(message, 0, message.Length);
            }
        }

        public MeterCommand Read()
        {
            byte[] myReadBuffer = new byte[1024];
            var numberOfBytesRead = stream.Read(myReadBuffer, 0, myReadBuffer.Length);
            var receive = Encoding.ASCII.GetString(myReadBuffer, 0, numberOfBytesRead);
            return JsonSerializer.Deserialize<MeterCommand>(receive);
        }
    }
}