using UnityEngine;

// uses methods from PlayerController: UDPconnection

public class Player2Move : MonoBehaviour {
  private float speed;
  public Rigidbody rd;
  public Vector3 spawnPosition = new Vector3(2, 1, 2);
  private UDPconnection uc;

  private void Start()
  {
    uc = new UDPconnection();
    uc.SendData("Player - Navigator Connected");
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
}
