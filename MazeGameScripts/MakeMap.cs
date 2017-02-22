using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MakeMap : MonoBehaviour {
  public static int[] map;
  private static List<GameObject> wallcubes;

  private static int mapSize = 11;

  private static GameObject mapBase;
  private static GameObject cubeGreen;
  private static GameObject cubeRedKey;

  private static float xCoord = 1.5f;
  private static float zCoord = 1.5f;
  private static float yCoord = 1f;

  private static bool isWall(int i)
  {
    return map[i] == 1;
  }

  public static void generateTheMap()
  {
    wallcubes = new List<GameObject>();
    xCoord = 1.5f;
    zCoord = 1.5f;
    int counter = 0;
    for (int i = 15; i < TCPClientConnection.mapBitArray.Count; i++)
    {
      if (TCPClientConnection.mapBitArray[i] == true)
      {
        GameObject wall = Instantiate(cubeGreen);
        wall.transform.position = new Vector3(xCoord, yCoord, zCoord);
      }
      counter++;
      if (counter % 21 == 0)
      {
        xCoord = 0.5f;
        zCoord += 1.0f;
      }
      xCoord += 1.0f;
    }
  }

  public static void generateTheItem()
  {
    wallcubes = new List<GameObject>();
    xCoord = 1.5f;
    zCoord = 1.5f;
    int counter = 0;
    for (int i = 15; i < TCPClientConnection.itemBitArray.Count; i++)
    {
      if (TCPClientConnection.itemBitArray[i] == true)
      {
        GameObject wall = Instantiate(cubeRedKey);
        wall.transform.position = new Vector3(xCoord, yCoord, zCoord);
      }
      counter++;
      if (counter % 21 == 0)
      {
        xCoord = 0.5f;
        zCoord += 1.0f;
      }
      xCoord += 1.0f;
    }
  }

  public static void makeMapBase()
  {
    GameObject mapbasic = Instantiate(mapBase);
    mapbasic.transform.position = new Vector3(11.5f, 0, 11.5f);
    mapbasic.transform.localScale = new Vector3(23, 1, 23);
  }

  private void Awake()
  {
    map = new int[200];
    mapBase = Resources.Load("Base") as GameObject;
    cubeRedKey = Resources.Load("CubeRedKey") as GameObject;
    cubeGreen = Resources.Load("CubeGreen") as GameObject;
  }
  private void Start()
  {
    makeMapBase();
    generateTheMap();
    generateTheItem();
  }
}

