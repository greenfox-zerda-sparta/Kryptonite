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

      try
      {
        wallList = Utility.TransformTwoDimensionalByteArrayToList(mazeArray);
      }
      catch (NullReferenceException e)
      {
        Console.WriteLine(e);
      }

      return wallList;
    }

    public byte[] CreateMessage()
    {
      MazeMessageArray[0] = Convert.ToByte(TCPMessageID.Maze);
      string str = "";

      try
      {
        str = Utility.CreateStringFromList(WallList);
      }
      catch (NullReferenceException e)
      {
        Console.WriteLine(e);
      }

      for (int i = 0; i < Utility.SPACE_FOR_TRANSFORMED_LIST; i++)
      {
        MazeMessageArray[i + 1] = Convert.ToByte(Utility.SplitStringToEightChar(str, i), 2);
      }

      return MazeMessageArray;
    }
  }
}