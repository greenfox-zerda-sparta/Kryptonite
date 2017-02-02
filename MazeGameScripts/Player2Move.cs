using UnityEngine;

// uses methods from PlayerController: UDPconnection

public class Player2Move : MonoBehaviour {
  private float speed;
  private Rigidbody rd;
  private Vector3 spawnPosition = new Vector3(2, 1, 2);
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
    spawnPosition.x = float.Parse(uc.SplitData(str)[0]);
    spawnPosition.z = float.Parse(uc.SplitData(str)[1]);
    rd.position = spawnPosition;
  }
}
