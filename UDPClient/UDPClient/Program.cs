using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace UDPClient {
  class Program {
    static void Main(string[] args)
    {
      Socket s = new Socket(AddressFamily.InterNetwork, SocketType.Dgram,
            ProtocolType.Udp);
      IPAddress broadcast = IPAddress.Parse("127.0.0.1");
      EndPoint ep = new IPEndPoint(broadcast, 7777);
      byte[] bytes = new byte[1024];

      while (true)
      {
        while (s.Available > 0)
        {
          int bytesRec = s.Receive(bytes);
          string str = (Encoding.ASCII.GetString(bytes, 0, bytesRec));
          Console.WriteLine(str);
        }
        if (Console.KeyAvailable) {
        string str = Console.ReadLine();
        byte[] sendbuf = Encoding.ASCII.GetBytes(str);
        s.SendTo(sendbuf, ep);
        }
      }

      //byte[] bytes = new byte[100];
      //s.ReceiveFrom(bytes, ref ep);

      //Console.WriteLine("Received message from {0} :\n {1}\n",
      //        ep.ToString(),
      //        Encoding.ASCII.GetString(bytes, 0, bytes.Length));

      //Console.WriteLine("Message sent to the broadcast address");
    }
  }
}
