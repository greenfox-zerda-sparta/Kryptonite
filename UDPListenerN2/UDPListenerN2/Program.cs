
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Collections.Generic;

namespace UDPListener2 {
  class UDPListener {
    private const int listenPort = 7777;

    private static void StartListener()
    {
      bool done = false;

      UdpClient listener = new UdpClient(listenPort);
      List<IPEndPoint> epList = new List<IPEndPoint>();
      IPEndPoint groupEP = new IPEndPoint(IPAddress.Any, listenPort);

      try
      {
        while (!done)
        {

          if (!epList.Contains(groupEP))
          {
            epList.Add(groupEP);
          }
 
          byte[] bytes = listener.Receive(ref groupEP);

          Console.WriteLine("Received broadcast from {0} :\n {1}\n",
              groupEP.ToString(),
              Encoding.ASCII.GetString(bytes, 0, bytes.Length));
         
          //bytes = Encoding.ASCII.GetBytes("7.7777;8");
          listener.Send(bytes, bytes.Length, groupEP);

          //if (Console.KeyAvailable)
          //{
          //  bytes = Encoding.ASCII.GetBytes(Console.ReadLine());
          //  listener.Send(bytes, bytes.Length, groupEP);
          //  foreach (var item in epList)
          //{
          // // Console.WriteLine("epList: " + item);
          // // listener.Send(bytes, bytes.Length, item);
          //}
        //}
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
      StartListener();

      return 0;
    }
  }
}