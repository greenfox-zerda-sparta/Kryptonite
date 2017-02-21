using System;
using System.Collections.Generic;

namespace GameServer {
  class TrapGenerator {
    private byte[,] trapArray = new byte[Utility.NUMBER_OF_ROWS, Utility.NUMBER_OF_COLOUMNS];
    private List<byte> trapList;
    private byte[] trapMessageArray;
    private List<byte> wallList;

    public byte[] TrapMessageArray
    {
      get
      {
        return trapMessageArray;
      }

      set
      {
        trapMessageArray = value;
      }
    }

    public List<byte> TrapList
    {
      get
      {
        return trapList;
      }

      set
      {
        trapList = value;
      }
    }

    public TrapGenerator(byte[,] wallArrayWithPath)
    {
      TrapList = new List<byte>();
      wallList = Utility.TransformTwoDimensionalByteArrayToList(wallArrayWithPath);
      GenerateTrapsFromMazeList();
    }

   
    private void GenerateTrapsFromMazeList()
    {
      int number_of_traps_outside_path = CountNumberOfTrapsOutsidePath();
      int created_traps_outside_path = 0;
      {
        do
        {
          int i = Utility.ran.Next(Utility.RAND_MINIMUM, Utility.RAND_MAXIMUM_FOR_ROWS * Utility.RAND_MAXIMUM_FOR_COLOUMNS);
          if (wallList[i] == Utility.ROAD_ID)
          {
            wallList[i] = Utility.TRAP_ID;
            created_traps_outside_path++;
          }
        } while (created_traps_outside_path != number_of_traps_outside_path);
      }

      int number_of_traps_on_path = CountNumberOfTrapsOnPath();
      int created_traps_on_path = 0;
      {
        do
        {
          int i = Utility.ran.Next(Utility.RAND_MINIMUM, Utility.RAND_MAXIMUM_FOR_ROWS * Utility.RAND_MAXIMUM_FOR_COLOUMNS);
          if (wallList[i] == Utility.ROAD_ID)
          {
            wallList[i] = Utility.PATH_ID;
            created_traps_on_path++;
          }
        } while (created_traps_on_path != number_of_traps_on_path);
      }
      CreateTrapList();
    }

    private int CountNumberOfTrapsOutsidePath()
    {
      return (int)(Utility.NUMBER_OF_ROWS * Utility.NUMBER_OF_COLOUMNS * Utility.PERCENIGE_FOR_NUMBER_OF_TRAPS_OUTSIDE_PATH);
    }

    private int CountNumberOfTrapsOnPath()
    {
      return (int)(Utility.NUMBER_OF_ROWS * Utility.NUMBER_OF_COLOUMNS * Utility.PERCENIGE_FOR_NUMBER_OF_TRAPS_ON_PATH);
    }

    private void CreateTrapList()
    {
      foreach (var item in wallList)
      {
        if (item == 2)
        {
          TrapList.Add(1);
        }
        else
        {
          TrapList.Add(0);
        }
      }
    }

    public byte[] CreateMessage()
    {
      TrapMessageArray = new byte[(TrapList.Count / Utility.ONE_BYTE) + 1];
      TrapMessageArray[0] = Convert.ToByte(TCPMessageID.TrapPosition);
      string str = "";

      try
      {
        str = Utility.CreateStringFromList(TrapList);
      }
      catch (NullReferenceException e)
      {
        Console.WriteLine(e);
      }

      for (int i = 0; i < TrapMessageArray.Length - 1; i++)
      {
        TrapMessageArray[i + 1] = Convert.ToByte(Utility.SplitStringToEightChar(str, i), 2);
      }
      return TrapMessageArray;
    }
  }
}