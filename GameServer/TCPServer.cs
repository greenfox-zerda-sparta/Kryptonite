using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Collections.Generic;

namespace GameServer
{

  public class TCPServer
  {
    public static ManualResetEvent allDone = new ManualResetEvent(false);
    public static List<Socket> socketList;
    private const string IPADDRESS = "10.27.99.226";
    private const int PORT = 5555;

    public TCPServer()
    {

    }

    public void StartTcpServer()
    {
      IPAddress ipAddress = IPAddress.Parse(IPADDRESS);
      IPEndPoint localEndPoint = new IPEndPoint(ipAddress, PORT);
      Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      socketList = new List<Socket>();
      try
      {
        listener.Bind(localEndPoint);
        listener.Listen(100);
        while (true)
        {
          allDone.Reset();
          Console.WriteLine("Waiting for a connection...");
          listener.BeginAccept(new AsyncCallback(AcceptCallback), listener);
          allDone.WaitOne();
        }
      }
      catch (Exception e)
      {
        Console.WriteLine(e.ToString());
      }
      Console.WriteLine("\nPress ENTER to continue...");
      Console.Read();
    }
    public static void AcceptCallback(IAsyncResult ar)
    {
      allDone.Set();
      Socket listener = (Socket)ar.AsyncState;
      Socket handler = listener.EndAccept(ar);
      socketList.Add(handler);
      BeginRecieve(handler);
    }
    public static void BeginRecieve(Socket handler)
    {
      StateObject state = new StateObject();
      state.workSocket = handler;
      handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
    }
    public static void ReadCallback(IAsyncResult ar)
    {
      string content = String.Empty;
      StateObject state = (StateObject)ar.AsyncState;
      Socket handler = state.workSocket;
      int bytesRead = 0;
      try
      {
        bytesRead = handler.EndReceive(ar);
      }
      catch
      {
        socketList.Remove(handler);
      }
      if (bytesRead > 0)
      {
        state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
        content = state.sb.ToString();
        if (content.IndexOf("<EOF>") > -1)
        {
          foreach (var item in socketList)
          {
            if (!item.Equals(handler))
            {
              Send(item, content);
            }
          }
          Console.WriteLine(content);
        }
        else
        {
          handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
        }
        state.sb.Clear();
        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
      }
    }
    private static void Send(Socket handler, string data)
    {
      byte[] byteData = Encoding.ASCII.GetBytes(data);
      handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);
    }
    private static void SendCallback(IAsyncResult ar)
    {
      try
      {
        Socket handler = (Socket)ar.AsyncState;
        int bytesSent = handler.EndSend(ar);
      }
      catch (Exception e)
      {
        Console.WriteLine(e.ToString());
      }
    }
  }
}