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
    Thread t1;
    public int sum;
    private const int LISTENPORT = 7777;
    //private const string IPADDRESS = "";

    private static void StartUdpServer()
    {
      bool isQuit = false;
      UdpClient listener = new UdpClient(LISTENPORT);
      List<IPEndPoint> endPointList = new List<IPEndPoint>();
      IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, LISTENPORT);
      Console.WriteLine("server started working");
      string message;
      try
      {
        while (!isQuit)
        {
          byte[] bytes = listener.Receive(ref clientEndPoint);
          message = Encoding.ASCII.GetString(bytes, 0, bytes.Length);
          if (message == "exit")
          {
            Console.WriteLine("Sever shutdown.");
            break;
          }
          if (!endPointList.Contains(clientEndPoint))
          {
            endPointList.Add(clientEndPoint);
          }
          Console.WriteLine("Received broadcast from {0} :\n {1}\n", clientEndPoint.ToString(), Encoding.ASCII.GetString(bytes, 0, bytes.Length));
          foreach (IPEndPoint item in endPointList)
          {
            if (!item.Equals(clientEndPoint))
            {
              listener.Send(bytes, bytes.Length, item);
            }
          }
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.ToString());
      }
      finally
      {
        listener.Close();
      }
    }

    public ServerSelector()
    {
      arg = "";
      sum = 1; 
    }

    private void readArg()
    {
      Console.WriteLine("Write -u- for UDP Server start\nWrite -t- for TCP Server start\nWrite -all- to start both\nType -exit- for quit");
      arg = Console.ReadLine();
    }

    private void t1start()
    {
      t1 = new Thread(StartUdpServer);
      t1.Start();
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
            break;
          case "u":
            Console.WriteLine("UDP starting...");
            t1start();
            Console.WriteLine(sum);
            break;
          case "t":
            Console.WriteLine("TCP starting...  - you haven't got tcp server yet");
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
