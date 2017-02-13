using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createCubes : MonoBehaviour {
  private static List<int> map;
  private static int mapSize = 30;
  private static int mapMatrix = mapSize * mapSize;

  private static float xCoord = 0.5f;
  private static float zCoord = 0.5f;
  private static float yCoord = 1f;

  private static void fillTheMap()
  {
    map = new List<int>();
    for (int i = 0; i < mapMatrix; i++)
    {
      map.Add(Random.Range(0, 2));
    }
  }

  private static bool isWall(int i, int j)
  {
    return map[i * j] == 1;
  }

  private static bool isMapBoarder(int i, int j)
  {
    return (xCoord == 0.5 || zCoord == 0.5 || xCoord == mapSize - 0.5f || zCoord == mapSize - 0.5f);
  }

  void Start() {

    fillTheMap();

    GameObject mapBase = Resources.Load("Base") as GameObject;
    GameObject mapbasic = Instantiate(mapBase);
    mapbasic.transform.position = new Vector3(mapSize / 2, 0, mapSize / 2);
    mapbasic.transform.localScale = new Vector3(mapSize, 1, mapSize);

    GameObject cubeGreen = Resources.Load("CubeGreen") as GameObject;
    GameObject cubeRed = Resources.Load("CubeRed") as GameObject;

    for (int j = 0; j < mapSize; j++)
    {
      for (int i = 0; i < mapSize; i++)
      {
        if (isMapBoarder(i, j))
        {
          GameObject wall = Instantiate(cubeRed);
          wall.transform.position = new Vector3(xCoord, yCoord, zCoord);
        }
        else if (isWall(i, j))  {
          GameObject wall = Instantiate(cubeGreen);
          wall.transform.position = new Vector3(xCoord, yCoord, zCoord);
        }
        xCoord += 1;
      }
      xCoord = 0.5f;
      zCoord += 1;
    }
	}
}
