using UnityEngine;

public class PlayerController : MonoBehaviour {
  public float speed = 4;
  public Rigidbody rd;
  Vector3 spawnPosition = new Vector3(2.5f, 1, 2.5f);

  private static int maxItems = 10;
  private static int itemCounter;
  private static bool isItemsCollected = false;

  public string lastPosition;
  public Rect labelPosition;
  string labelText;
  public GUIStyle labelStyle;

  public UDPClientConnection uc;
  public TCPClientConnection tc;

  private void Awake()
  {
    lastPosition = "";
    uc = new UDPClientConnection();
    tc = new TCPClientConnection();
    tc.TCPConnectRun();
    uc.SendData("Player - Driver connected(UDP)");
    tc.Send(TCPMessageID.Message, tc.client, "Player - Driver connected(TCP)");
    tc.Receive(tc.client);
    tc.Receive(tc.client);

  }

  private void Start()
  {
    labelText = "Items: " + itemCounter.ToString();
    labelStyle.fontSize = 24;
    
  }

  private void FixedUpdate()
  {
    float moveHorizontal = Input.GetAxis("Horizontal");
    float moveVertical = Input.GetAxis("Vertical");
    Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
    rd.AddForce(movement * speed * Time.deltaTime);
    /*
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
    */
    //if (!lastPosition.Equals((rd.position.x.ToString() + ";" + rd.position.z.ToString()))) {
    //  uc.SendData(rd.position.x.ToString() + ";" + rd.position.z.ToString());
    //  lastPosition = (rd.position.x.ToString() + ";" + rd.position.z.ToString());
    uc.SendData(rd.position.x.ToString() + ";" + rd.position.z.ToString());
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Pick Up"))
    {
      other.gameObject.SetActive(false);
      itemCounter++;
      labelText = "Items: " + itemCounter.ToString()  + " | " +  maxItems.ToString();
      if (itemCounter == maxItems)
      {
        isItemsCollected = true;
      }
    }
  }

    void OnGUI() {
    labelStyle.normal.textColor = Color.white;
    GUI.Label(labelPosition, labelText, labelStyle);
  }
}
