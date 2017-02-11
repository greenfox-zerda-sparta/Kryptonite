using UnityEngine;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class UDPconnection : MonoBehaviour {

  private const int PORTNUMBER = 7777;
  private const string IPADDRESS = "192.168.1.37";
  public static Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
  public static IPAddress serverIPAddress = IPAddress.Parse(IPADDRESS);
  public static EndPoint serverEndPoint = new IPEndPoint(serverIPAddress, PORTNUMBER);

  public UDPconnection()
  {
  }

  public string[] SplitData(string text)
  {
    char delimiter = ';';
    string[] words = text.Split(delimiter);
    return words;
  }

  public void SendData(string data)
  {
    string str = data;
    byte[] sendbuf = Encoding.ASCII.GetBytes(str);
    socket.SendTo(sendbuf, serverEndPoint);
  }

  public string incomeData()
  {
    byte[] bytes = new byte[1024];
    int bytesRec = socket.Receive(bytes);
    string temp = Encoding.ASCII.GetString(bytes, 0, bytesRec);
    return temp;
  }
}