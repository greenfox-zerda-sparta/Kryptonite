using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;

namespace UDPServer {
  class UDPServer
  {
    private const int LISTENPORT = 7777;

    private static UdpClient listener;
    private static List<IPEndPoint> endPointList;

    private static void startServer()
    {
      bool isQuit = false;
      IPEndPoint clientEndPoint = new IPEndPoint(IPAddress.Any, LISTENPORT);
      try
      {
        while (!isQuit)
        {
          byte[] bytes = listener.Receive(ref clientEndPoint);
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

    public static int Main()
    {
      UdpClient listener = new UdpClient(LISTENPORT);
      List<IPEndPoint> endPointList = new List<IPEndPoint>();
      startServer();
      return 0;
    }
  }
}