using UnityEngine;

// uses methods from PlayerController: UDPconnection

public class Player2Move : MonoBehaviour {
  private float speed;
  public Rigidbody rd;
  public Vector3 spawnPosition = new Vector3(2, 1, 2);
  private UDPconnection uc;
  private TCPConnection tc;

  public string lastPosition;
  public Rect labelPosition;
  string labelText;
  public GUIStyle labelStyle;
  int healthPoint = 1000;

  private void Start()
  {
    uc = new UDPconnection();
    tc = new TCPConnection();
    uc.SendData("Player - Navigator Connected(UDP)");
    string strFromTCP = tc.Receive(tc.client);
    tc.Send(TCPMessageID.Message, tc.client, "Player - Navigator Connected(TCP)");
    tc.Receive(tc.client);
    rd.position = spawnPosition;
  }

  private void FixedUpdate()
  {
    string str = uc.incomeData();
    float coordX = float.Parse(uc.SplitData(str)[0]);
    float coordZ = float.Parse(uc.SplitData(str)[1]);

    spawnPosition.x = coordX;
    spawnPosition.z = coordZ;
    rd.position = spawnPosition;
  }

  void OnGUI()
  {
    GUI.Label(labelPosition, tc.Receive(tc.client), labelStyle);
  }
}