using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using System.Net;
using System.Net.Sockets;

namespace GameServer
{
  class ServerSelector
  {
    private string arg;
    Thread threadUDP;
    Thread threadTCP;
    private const int LISTENPORT = 7777;
    //private const string IPADDRESS = "";
    private static UDPServer udpserver;
    private static TCPServer tcpserver;

    public ServerSelector()
    {
      arg = "";
    }

    private void readArg()
    {
      Console.WriteLine("Write -u- for UDP Server start\nWrite -t- for TCP Server start\nWrite -all- to start both\nType -exit- for quit");
      arg = Console.ReadLine();
    }

    private void startThreadUDP()
    {
      udpserver = new UDPServer();
      threadUDP = new Thread(udpserver.StartUdpServer);
      threadUDP.Start();
    }

    private void startThreadTCP()
    {
      tcpserver = new TCPServer();
      threadTCP = new Thread(tcpserver.StartTcpServer);
      threadTCP.Start();
    }

    public void select()
    {
      while (true)
      {
        readArg();
        switch (arg)
        {
          case "all":
            Console.WriteLine("Both starting...");
            startThreadUDP();
            startThreadTCP();
            break;
          case "u":
            Console.WriteLine("UDP starting...");
            startThreadUDP();
            break;
          case "t":
            Console.WriteLine("TCP starting..." );
            startThreadTCP();
            break;
          case "exit":
            Console.WriteLine("Bye!  ^.^");
            System.Environment.Exit(1);
            break;
          default:
            Console.WriteLine("Wrong arguments");
            select();
            break;
        }
      }
    }
  }
}
