using UnityEngine;

public class navigatorController : MonoBehaviour {
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
    uc.SendData("Player - Navigator connected(UDP)");
    tc.Send(TCPMessageID.Message, tc.client, "Navigator - Driver connected(TCP)");
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
    string str = uc.incomeData();
    float coordX = float.Parse(uc.SplitData(str)[0]);
    float coordZ = float.Parse(uc.SplitData(str)[1]);

    spawnPosition.x = coordX;
    spawnPosition.z = coordZ;
    rd.position = spawnPosition;
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.gameObject.CompareTag("Pick Up"))
    {
      other.gameObject.SetActive(false);
      itemCounter++;
      labelText = "Items: " + itemCounter.ToString() + " | " + maxItems.ToString();
      if (itemCounter == maxItems)
      {
        isItemsCollected = true;
      }
    }
  }

  void OnGUI()
  {
    labelStyle.normal.textColor = Color.white;
    GUI.Label(labelPosition, labelText, labelStyle);
  }
}