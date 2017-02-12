using UnityEngine;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

public class StateObject : MonoBehaviour {

    public Socket workSocket = null;
    public const int BufferSize = 1024;
    public byte[] buffer = new byte[BufferSize];
    public StringBuilder sb = new StringBuilder();

}

public enum TCPMessageID : byte {
  Message,
  Maze,
  Trap,
}