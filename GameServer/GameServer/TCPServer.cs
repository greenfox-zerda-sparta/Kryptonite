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
    private const int PORT = 5555;
    private static ManualResetEvent allDone;
    private static List<Socket> socketList;
    private static MazeGenerator mazeGen;
    private static TrapGenerator trapGen;
    private static ItemGenerator itemGen;
    private static PathFinder pathFinder;
    private static IPEndPoint localEndPoint;
    private static Socket listener;
    private static string content;
    private static int bytesRead;

    public TCPServer()
    {
      allDone = new ManualResetEvent(false);
      localEndPoint = new IPEndPoint(IPAddress.Any, PORT);
      listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
      socketList = new List<Socket>();
      content = String.Empty;
      mazeGen = new MazeGenerator(10,10);
      mazeGen.GenerateTWMaze_GrowingTree();
      mazeGen.LineToBlock();
      mazeGen.CreateMessage();
      pathFinder = new PathFinder(mazeGen.MazeArray);
      itemGen = new ItemGenerator(pathFinder.MazePath);
      //trapGen = new TrapGenerator(pathFinder.MazePath);
    }

    public void Start()
    {
      try
      {
        Listening();
      }
      catch (Exception e)
      {
        Console.WriteLine(e.ToString());
      }
      Console.WriteLine("\nPress ENTER to continue...");
    }

    private static void Listening()
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

    public static void AcceptCallback(IAsyncResult ar)
    {
      allDone.Set();
      Socket listener = (Socket)ar.AsyncState;
      Socket handler = listener.EndAccept(ar);
      socketList.Add(handler);
      BeginRecieve(handler);
      Send(handler, mazeGen.MazeMessageArray);
    }

    public static void BeginRecieve(Socket handler)
    {
      StateObject state = new StateObject();
      state.workSocket = handler;
      handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
    }

    public static void ReadCallback(IAsyncResult ar)
    {
      StateObject state = (StateObject)ar.AsyncState;
      Socket handler = state.workSocket;
      bytesRead = 0;
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
      TransformIncomeData(state, handler);
      state.sb.Clear();
      handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
      }
    }


    private static void TransformIncomeData(StateObject state, Socket handler)
    {
      state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
      content = state.sb.ToString();
      if (content.IndexOf("<EOF>") > -1)
      {
        CheckMessageID(state, handler);
      }
      else
      {
        handler.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), state);
      }
    }

    private static void CheckMessageID(StateObject state, Socket handler)
    {
      TCPMessageID ID = (TCPMessageID)state.buffer[0];
      switch (ID)
      {
        case TCPMessageID.Message:
          PrintMessageToConsol();
          break;
        case TCPMessageID.MazeIsReceived:
          Send(handler, itemGen.CreateMessage());
          break;
        //case TCPMessageID.ItemPositionIsReceived:
        //  Send(handler, trapGen.CreateMessage());
        //  break;
        case TCPMessageID.Item:
          SendMessageToTheOtherClients(handler);
          break;
        //case TCPMessageID.Trap:
        //  SendMessageToTheOtherClients(handler);
        //  break;
      }
    }

    public static void PrintMessageToConsol()
    {
      string message = content.Substring(0, content.Length - 5);
      Console.WriteLine(message);
    }

    private static void SendMessageToTheOtherClients(Socket handler) { 
          foreach (var item in socketList)
          {
        if (!item.Equals(handler))
        {
          Send(item, content);
        }
  }
}

private static void Send(Socket handler, string data)
    {
      byte[] byteData = Encoding.ASCII.GetBytes(data);
      handler.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), handler);
    }

    private static void Send(Socket handler, byte[] byteArr)
    {
      byte[] byteData = byteArr;
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