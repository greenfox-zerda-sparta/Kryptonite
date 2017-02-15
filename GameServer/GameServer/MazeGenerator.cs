using System;
using System.Collections.Generic;

namespace GameServer {
  public class MazeGenerator {
    private List<byte> wallList;
    private byte[,] mazeArray = new byte[Utility.NUMBER_OF_ROWS, Utility.NUMBER_OF_COLOUMNS];
    private byte[] mazeMessageArray = new byte[Utility.SPACE_FOR_MESSAGEID + Utility.SPACE_FOR_TRANSFORMED_LIST];

    public List<byte> WallList
    {
      get
      {
        if (null == wallList)
        {
          GenerateMaze();
        }
        return wallList;
      }
    }

    public byte[] MazeMessageArray
    {
      get
      {
        return mazeMessageArray;
      }

      set
      {
        mazeMessageArray = value;
      }
    }

    public MazeGenerator()
    {
    }

    public List<byte> GenerateMaze()
    {
      for (int row = 0; row < Utility.NUMBER_OF_ROWS; row++)
      {
        for (int column = 0; column < Utility.NUMBER_OF_COLOUMNS; column++)
        {
          mazeArray[row, column] = Convert.ToByte(Utility.ran.Next(Utility.RAND_MINIMUM, 2));
        }
      }

      return wallList = Utility.TransformTwoDimensionalByteArrayToList(mazeArray);
    }

    public byte[] CreateMessage()
    {
      MazeMessageArray[0] = Convert.ToByte(TCPMessageID.Maze);
      Console.WriteLine(MazeMessageArray[0]);
      string str = Utility.CreateStringFromList(WallList);
      for (int i = 0; i < Utility.SPACE_FOR_TRANSFORMED_LIST; i++)
      {
        MazeMessageArray[i + 1] = Convert.ToByte(Utility.SplitStringToEightBits(str, i), 2);
        Console.WriteLine(MazeMessageArray[i + 1]);
      }
      return MazeMessageArray;
    }
  }
}