using System;
using System.Text;
using System.Threading;
using System.Net.Sockets;
using System.Net;


namespace TCPasyncClient {

  public class AsynchronousClient {

      private const int PORT = 7777;
      private const string IPADDRESS = "127.0.0.1";

      private static ManualResetEvent connectDone = new ManualResetEvent(false);
      private static ManualResetEvent sendDone = new ManualResetEvent(false);
      private static ManualResetEvent receiveDone = new ManualResetEvent(false);

      private static string response = String.Empty;

      public static void StartClient() {

        try {
          IPAddress serverIP = IPAddress.Parse(IPADDRESS);
          IPEndPoint serverEP = new IPEndPoint(serverIP, PORT);
          Socket client = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

          client.BeginConnect(serverEP, new AsyncCallback(ConnectCallback), client);
          connectDone.WaitOne();

          while (true) {
            if (Console.KeyAvailable) {
              string str = Console.ReadLine();
              Send(client, (str + "<EOF>"));
              sendDone.WaitOne();
            }
            while (client.Available > 0) {//ha ez benne van, nem fagy le!!!!
              Receive(client);
              receiveDone.WaitOne();
            }
          }
        } catch (Exception e) {
          Console.WriteLine(e.ToString());
        }
      }

      private static void ConnectCallback(IAsyncResult ar) {
        try {
          Socket client = (Socket)ar.AsyncState;
          client.EndConnect(ar);
          Console.WriteLine("Socket connected to {0}", client.RemoteEndPoint.ToString());
          connectDone.Set();
        } catch (Exception e) {
          Console.WriteLine(e.ToString());
        }
      }

      private static void Receive(Socket client) {
        try {
          StateObject state = new StateObject();
          state.workSocket = client;
          client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
        } catch (Exception e) {
          Console.WriteLine(e.ToString());
        }
      }

      private static void ReceiveCallback(IAsyncResult ar) {
        try {
          StateObject state = (StateObject)ar.AsyncState;
          Socket client = state.workSocket;
          int bytesRead = client.EndReceive(ar);

          if (bytesRead > 0) {
            state.sb.Append(Encoding.ASCII.GetString(state.buffer, 0, bytesRead));
            response = state.sb.ToString();
            client.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReceiveCallback), state);
          }
          if (state.sb.Length > 0) {
            Console.WriteLine(response);
            state.sb.Clear();
          }
          receiveDone.Set();
        } catch (Exception e) {
          Console.WriteLine(e.ToString());
        }
      }

      private static void Send(Socket client, string data) {
        byte[] byteData = Encoding.ASCII.GetBytes(data);
        client.BeginSend(byteData, 0, byteData.Length, 0, new AsyncCallback(SendCallback), client);
      }

      private static void SendCallback(IAsyncResult ar) {
        try {
          Socket client = (Socket)ar.AsyncState;
          int bytesSent = client.EndSend(ar);
          sendDone.Set();
        } catch (Exception e) {
          Console.WriteLine(e.ToString());
        }
      }
    }
}
