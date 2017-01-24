using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace CSSynchronousServerListener {
  class Program {
    public static int Main(String[] args)
    {
      SynchronousSocketListener.StartListening();
      return 0;
    }
  }
}


public class SynchronousSocketListener {

  // Incoming data from the client.
  public static string data = null;

  public static void StartListening()
  {
    // Data buffer for incoming data.
    byte[] bytes = new Byte[1024];

    // Establish the local endpoint for the socket.
    // Dns.GetHostName returns the name of the 
    // host running the application.
    IPHostEntry ipHostInfo = Dns.Resolve(Dns.GetHostName());
    IPAddress ipAddress = ipHostInfo.AddressList[0];
    IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

    // Create a TCP/IP socket.
    Socket listener = new Socket(AddressFamily.InterNetwork,
        SocketType.Stream, ProtocolType.Tcp);

    // Bind the socket to the local endpoint and 
    // listen for incoming connections.
    try
    {
      listener.Bind(localEndPoint);
      listener.Listen(10);

    // Start listening for connections.
      
      while (true)
      {
        Console.WriteLine("Waiting for a connection...");
        // Program is suspended while waiting for an incoming connection.
        Socket handler = listener.Accept();
       // Socket handler2 = listener.Accept();
      // An incoming connection needs to be processed.
        while (true)
        {
          data = null;
          bytes = new byte[1024];
          int bytesRec = handler.Receive(bytes);
          data += Encoding.ASCII.GetString(bytes, 0, bytesRec);
            
          // Show the data on the console.
          Console.WriteLine("Text received : {0}", data);
          
          //write back a message
          string str = Console.ReadLine();
          byte[] msg = Encoding.ASCII.GetBytes(str);
          handler.Send(msg);
          if (data == "q")
          {
            break;
          }
        }
        handler.Shutdown(SocketShutdown.Both);
        handler.Close();
       }
    }
    catch (Exception e)
    {
      Console.WriteLine(e.ToString());
    }

    Console.WriteLine("\nPress ENTER to continue...");
    Console.Read();
  }
}