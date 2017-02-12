using UnityEngine;

public class PlayerController : MonoBehaviour {
  public float speed;
  public Rigidbody rd;
  Vector3 spawnPosition = new Vector3(2, 1, 2);

  public UDPconnection uc;
  public TCPConnection tc;

  public string lastPosition;
  public Rect labelPosition;
  string labelText;
  public GUIStyle labelStyle;
  int healthPoint = 100;
  bool youAreInATrap = false;
  
  private void Start()
  {
    lastPosition = "";
    uc = new UDPconnection();
    tc = new TCPConnection();
    uc.SendData("Player - Driver connected(UDP)");
    tc.Send(TCPMessageID.Message, tc.client, "Player - Driver connected(TCP)");
    tc.Receive(tc.client);
    rd.position = spawnPosition;
  }

  private void FixedUpdate()
  {
    float moveHorizontal = Input.GetAxis("Horizontal");
    float moveVertical = Input.GetAxis("Vertical");
    Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
    rd.AddForce(movement * speed * Time.deltaTime);

    if (rd.position.x >= 8 && rd.position.z <= 2 && !youAreInATrap) {
      youAreInATrap = true;
      healthPoint -= 1;
      labelText = "HP - " + healthPoint.ToString();
      tc.Send(TCPMessageID.Trap, tc.client, labelText);
    }
    else if (rd.position.x < 8 || rd.position.z > 2)
    {
      youAreInATrap = false;
    }

    if (!lastPosition.Equals((rd.position.x.ToString() + ";" + rd.position.z.ToString()))) {
      uc.SendData(rd.position.x.ToString() + ";" + rd.position.z.ToString());
      lastPosition = (rd.position.x.ToString() + ";" + rd.position.z.ToString());
    }
  }

  void OnGUI() {
    GUI.Label(labelPosition, labelText, labelStyle);
  }
}
