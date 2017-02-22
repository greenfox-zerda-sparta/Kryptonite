using UnityEngine;
using System;
using System.Linq;
using System.Collections;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;


public class TCPClientConnection : MonoBehaviour
{

  private const int PORT = 5555;
  private const string IPADDRESS = "10.27.99.186";

  public static BitArray mapBitArray;
  public static BitArray itemBitArray;

  private static ManualResetEvent connectDone = new ManualResetEvent(false);
  private static ManualResetEvent sendDone = new ManualResetEvent(false);
  private static ManualResetEvent receiveDone = new ManualResetEvent(false);

  private static string response = "";
  public Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
  
  public TCPClientConnection()
  {

  }

  public void TCPConnectRun() { 
  try
    {
      IPAddress serverIP = IPAddress.Parse(IPADDRESS);
      IPEndPoint serverEP = new IPEndPoint(serverIP, PORT);
      client.BeginConnect(serverEP, new AsyncCallback(ConnectCallback), client);
      connectDone.WaitOne();
    }
    catch (Exception e)
    {
      Console.WriteLine(e.ToString());
    }
  }

  private static void ConnectCallback(IAsyncResult ar)
  {
    try
    {
      Socket client = (Socket)ar.AsyncState;
      client.EndConnect(ar);
      Console.WriteLine("Socket connected to {0}", client.RemoteEndPoint.ToString());
      connectDone.Set();
    }
    catch (Exception e)
    {
      Console.WriteLine(e.ToString());
    }
  }

  public void Send(TCPMessageID messageID, Socket client, string data)
  {
    byte[] byteForMessageID = new byte[1];
    byteForMessageID[0] = Convert.ToByte(messageID);
    byte[] byteData = Encoding.ASCII.GetBytes(data + "<EOF>");
    byte[] byteForSend = byteForMessageID.Concat(byteData).ToArray();
    client.BeginSend(byteForSend, 0, byteForSend.Length, 0, new AsyncCallback(SendCallback), client);
  }

  private static void SendCallback(IAsyncResult ar)
  {
    try
    {
      Socket client = (Socket)ar.AsyncState;
      int bytesSent = client.EndSend(ar);
      sendDone.Set();
    }
    catch (Exception e)
    {
      // Console.WriteLine(e.ToString());
    }
  }

  public string Receive(Socket client)
  {
    try
    {
      StateObject state = new StateObject();
      state.workSocket = client;
      client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
      return response;
    }
    catch (Exception e)
    {
      Console.WriteLine(e.ToString());
      return "";
    }
  }

  private void ReceiveCallback(IAsyncResult ar)
  {
    try
    {
      StateObject state = (StateObject)ar.AsyncState;
      Socket client = state.workSocket;
      int bytesRead = client.EndReceive(ar);

      if (bytesRead > 0)
      {
        TCPMessageID ID = (TCPMessageID)state.buffer[0];
        switch (ID)
        {
          case TCPMessageID.Maze:
            Send(TCPMessageID.Message, client, ("I've received the mazeWalls! " + bytesRead.ToString()));
            Send(TCPMessageID.MazeIsReceived, client, "I've received the mazeWalls!");
            //remove the ID from the array
            state.buffer = state.buffer.Where((source, index) => index != 0).ToArray();
            ConvertTheMessageToMap(state, bytesRead);
            break;
          case TCPMessageID.Trap:
            //remove the ID from the array
            state.buffer = state.buffer.Where((source, index) => index != 0).ToArray();
            //write the healthpoint to the label
            state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
            response = state.sb.ToString(0, state.sb.Length - 6);
            break;
          case TCPMessageID.TrapPosition:
            Send(TCPMessageID.Message, client, ("I've received the traps! " + bytesRead.ToString()));
            //remove the ID from the array
            state.buffer = state.buffer.Where((source, index) => index != 0).ToArray();
            ConvertTheMessageToItem(state, bytesRead);
            break;
        }
        client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
      }
      if (state.sb.Length > 0)
      {
        state.sb = null;
      }
      receiveDone.Set();
    }
    catch (Exception e)
    {
      // Console.WriteLine(e.ToString());
    }
  }

  private void ConvertTheMessageToMap(StateObject state, int bytesRead)
  {
    Array.Resize<byte>(ref state.buffer, bytesRead);
    if (BitConverter.IsLittleEndian)
    {
      Array.Reverse(state.buffer);
    }
    mapBitArray = new BitArray(state.buffer);
    Array.Resize<byte>(ref state.buffer, StateObject.BufferSize);
  }

  private void ConvertTheMessageToItem(StateObject state, int bytesRead)
  {
    Array.Resize<byte>(ref state.buffer, bytesRead);
    if (BitConverter.IsLittleEndian)
    {
      Array.Reverse(state.buffer);
    }
    itemBitArray = new BitArray(state.buffer);
    Array.Resize<byte>(ref state.buffer, StateObject.BufferSize);
  }
}
