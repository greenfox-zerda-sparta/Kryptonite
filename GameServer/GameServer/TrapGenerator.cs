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

    public TrapGenerator(List<byte> wallList)
    {
      trapList = new List<byte>();
      this.wallList = wallList;
      GenerateTrapsFromMazeList();
    }

   
    private void GenerateTrapsFromMazeList()
    {
      int number_of_traps = CountNumberOfTraps();
      int created_traps = 0;
      {
        do
        {
          int i = Utility.ran.Next(Utility.RAND_MINIMUM, Utility.RAND_MAXIMUM_FOR_ROWS * Utility.RAND_MAXIMUM_FOR_COLOUMNS);
          if (wallList[i] == Utility.ROAD_ID)
          {
            wallList[i] = Utility.TRAP_ID;
            created_traps++;
          }
        } while (created_traps != number_of_traps);
      }
      CreateTrapList();
    }

    private int CountNumberOfTraps()
    {
      return (int)(Utility.NUMBER_OF_ROWS * Utility.NUMBER_OF_COLOUMNS * Utility.PERCENIGE_FOR_NUMBER_OF_TRAPS);
    }

    private void CreateTrapList()
    {
      foreach (var item in wallList)
      {
        if (item == 2)
        {
          trapList.Add(1);
        }
        else
        {
          trapList.Add(0);
        }
      }
    }

    public byte[] CreateMessage()
    {
      TrapMessageArray = new byte[(trapList.Count / Utility.ONE_BYTE) + 1];
      TrapMessageArray[0] = Convert.ToByte(TCPMessageID.TrapPosition);
      string str = "";

      try
      {
        str = Utility.CreateStringFromList(trapList);
      }
      catch (NullReferenceException e)
      {
        Console.WriteLine(e);
      }

      for (int i = 0; i < TrapMessageArray.Length - 1; i++)
      {
        TrapMessageArray[i + 1] = Convert.ToByte(Utility.SplitStringToEightBits(str, i), 2);
      }
      return TrapMessageArray;
    }
  }
}