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
  class UDPServer
  {
    private const int LISTENPORT = 7777;
    //private const string IPADDRESS = "";

    public UDPServer()
    {

    }

    public void StartUdpServer()
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
  }
}
