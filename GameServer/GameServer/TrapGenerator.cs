using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameServer {
  class TrapGenerator {
    //a mazeArrayt és a meretet majd bekerjuk persze a mazegeneratortol, addig is: :)
    private const int NUMBER_OF_ROWS = 8;
    private const int NUMBER_OF_COLOUMNS = 8;
    private const int ROAD_ID = 0;
    private const int TRAP_ID = 2;
    private const int RAND_MINIMUM = 0;
    private const int RAND_MAXIMUM_FOR_ROWS = NUMBER_OF_ROWS;
    private const int RAND_MAXIMUM_FOR_COLOUMNS = NUMBER_OF_COLOUMNS;
    private const double PERCENIGE_FOR_NUMBER_OF_TRAPS = 0.05;
    private const int ONE_BYTE = 8;
    private byte[,] mazeArray = new byte[NUMBER_OF_ROWS, NUMBER_OF_COLOUMNS];
    private byte[,] trapArray = new byte[NUMBER_OF_ROWS, NUMBER_OF_COLOUMNS];
    private List<byte> trapList = new List<byte>();
    private byte[] byteArrayOFConvertedTrapList;
    private Random ran = new Random();

    public byte[] ByteArrayOfConvertedTrapList
    {
      get
      {
        return byteArrayOFConvertedTrapList;
      }

      set
      {
        byteArrayOFConvertedTrapList = value;
      }
    }

    public TrapGenerator()
    {
      GenerateTraps();
    }

    //amig nincs meg Danitol a palya :)
    private void FillMazeArray()
    {
      for (int row = 0; row < NUMBER_OF_ROWS; row++)
      {
        for (int column = 0; column < NUMBER_OF_COLOUMNS; column++)
        {
          mazeArray[row, column] = Convert.ToByte(ran.Next(RAND_MINIMUM, 2));
        }
      }
    }

    private void GenerateTraps()
    {
      FillMazeArray();
      int number_of_traps = CountNumberOfTraps();
      int created_traps = 0;
      {
        do
        {
          int i = ran.Next(RAND_MINIMUM, RAND_MAXIMUM_FOR_ROWS);
          int j = ran.Next(RAND_MINIMUM, RAND_MAXIMUM_FOR_COLOUMNS);
          if (mazeArray[i, j] == ROAD_ID)
          {
            mazeArray[i, j] = TRAP_ID;
            created_traps++;
          }
        } while (created_traps != number_of_traps);
      }
      CreateTrapArray();
    }

    private int CountNumberOfTraps()
    {
      return (int)(NUMBER_OF_ROWS * NUMBER_OF_COLOUMNS * PERCENIGE_FOR_NUMBER_OF_TRAPS);
    }

    private void CreateTrapArray()
    {
      for (int i = 0; i < NUMBER_OF_ROWS; i++)
      {
        for (int j = 0; j < NUMBER_OF_COLOUMNS; j++)
        {
          if (mazeArray[i, j] == 2)
          {
            trapArray[i, j] = 1;
          }
          else
          {
            trapArray[i, j] = 0;
          }
        }
      }
      TransformTrapArrayToList();
    }

    private void TransformTrapArrayToList()
    {
      trapList = trapArray.Cast<byte>().ToList();
      CreateTrapMessage();
    }

    private string CreateStringFromTrapList()
    {
      StringBuilder strBuilder = new StringBuilder();
      foreach (int item in trapList)
      {
        strBuilder.Append(item);
      }
      return strBuilder.ToString();
    }

    public string SplitString(string str, int index)
    {
      return str.Substring((index * ONE_BYTE), ONE_BYTE);
    }

    private void CreateTrapMessage()
    {
      ByteArrayOfConvertedTrapList = new byte[(trapList.Count / ONE_BYTE) + 1];
      ByteArrayOfConvertedTrapList[0] = Convert.ToByte(TCPMessageID.TrapPosition);
      string str = CreateStringFromTrapList();
      for (int i = 0; i < ByteArrayOfConvertedTrapList.Length - 1; i++)
      {
        ByteArrayOfConvertedTrapList[i + 1] = Convert.ToByte(SplitString(str, i), 2);
      }
    }
  }
}