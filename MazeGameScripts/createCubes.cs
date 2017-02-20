using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createCubes : MonoBehaviour {
  private static int mapSize = 10;
  private static int mapMatrix = (mapSize +2) * (mapSize + 2);
  public static int[] map = new int[200];

  private static float xCoord = 1.5f;
  private static float zCoord = 1.5f;
  private static float yCoord = 1f;

  private static GameObject cubeGreen;
  private static GameObject cubeRed;

  public Rect labelPosition;
  string labelText;
  public GUIStyle labelStyle;

  public createCubes()
  {
    cubeRed = Resources.Load("CubeRed") as GameObject;
    cubeGreen = Resources.Load("CubeGreen") as GameObject;
  }

  private static bool isWall(int i)
  {
    return map[i] == 1;
  }

  private static void makeBoarder()
    {
    xCoord = 0.5f;
    zCoord = 0.5f;
    for (int i = 0; i < (mapSize + 2); i++)
    {
      GameObject border = Instantiate(cubeRed); 
      border.transform.position = new Vector3(xCoord, yCoord, zCoord);
      zCoord += 1;
    }

    zCoord = 0.5f;
    xCoord = 0.5f + mapSize + 1;
    for (int i = 0; i < (mapSize + 2); i++)
    {
      GameObject border = Instantiate(cubeRed);
      border.transform.position = new Vector3(xCoord, yCoord, zCoord);
      zCoord += 1;
    }

    zCoord = 0.5f;
    xCoord = 1.5f;
    for (int i = 0; i < mapSize; i++)
    {
      GameObject border = Instantiate(cubeRed);
      border.transform.position = new Vector3(xCoord, yCoord, zCoord);
      xCoord += 1;
    }

    zCoord = 0.5f + mapSize + 1;
    xCoord = 1.5f;
    for (int i = 0; i < mapSize; i++)
    {
      GameObject border = Instantiate(cubeRed);
      border.transform.position = new Vector3(xCoord, yCoord, zCoord);
      xCoord += 1;
    }
  }

  void Start() {
    GameObject mapBase = Resources.Load("Base") as GameObject;
    GameObject mapbasic = Instantiate(mapBase);
    mapbasic.transform.position = new Vector3(6, 0, 6);
    mapbasic.transform.localScale = new Vector3(12, 1, 12);
  }

  public static void makeMap() { 
    makeBoarder();
    xCoord = 1.5f;
    zCoord = 1.5f;

    for (int i = 0; i < mapSize * mapSize; i++)
    {   
      if (i % mapSize == 0 && i != 0)
      {
        xCoord = 1.5f;
        zCoord += 1;
      }
      if (isWall(i))
      {
        GameObject wall = Instantiate(cubeGreen);
        wall.transform.position = new Vector3(xCoord, yCoord, zCoord);
      }
      xCoord += 1;
    }
  }
  void OnGUI()
  {
    GUI.Label(labelPosition, labelText, labelStyle);
  }
}
