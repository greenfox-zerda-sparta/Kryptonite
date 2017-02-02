using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

public class UDPconnection
{

  private const int PORTNUMBER = 7777;
  private const string  IPADDRESS = "10.27.99.28";
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

public class PlayerController : MonoBehaviour {
  public float speed;
  public Rigidbody rd;
  Vector3 spawnPosition = new Vector3(2, 1, 2);

  public UDPconnection uc;

  private void Start()
  {
    uc = new UDPconnection();
    uc.SendData("Player - Driver connected");
    rd.position = spawnPosition;
  }

  private void FixedUpdate()
  {
    float moveHorizontal = Input.GetAxis("Horizontal");
    float moveVertical = Input.GetAxis("Vertical");
    Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
    rd.AddForce(movement * speed * Time.deltaTime);

    uc.SendData(rd.position.x.ToString() +";"+ rd.position.z.ToString());
  }
}
