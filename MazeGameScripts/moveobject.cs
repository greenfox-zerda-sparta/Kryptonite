using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveobject : MonoBehaviour {
  public static bool myBool = false;
  private static float x = 0.5f;
  private static float z = 0.5f;

  public void moveTheCube()
  {
    transform.position = Vector3.MoveTowards(transform.position, new Vector3(x + 1, 1, z + 1), 20);
  }

  void Update () {
  }
}
