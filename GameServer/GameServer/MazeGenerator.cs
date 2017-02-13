using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer {
  public class MazeGenerator {

    private const int SIZE_OF_LIST = 104;
    private const int ONE_BYTE = 8;
    private const int SPACE_FOR_TRANSFORMED_LIST = SIZE_OF_LIST / ONE_BYTE;
    private const int SPACE_FOR_MESSAGEID = 1;
    private Random ran = new Random();
    private List<int> wallList = new List<int>();
    private byte[] byteArrayOfConvertedWallList = new byte[SPACE_FOR_MESSAGEID + SPACE_FOR_TRANSFORMED_LIST];

    public byte[] ByteArrayOfConvertedWallList
    {
      get
      {
        return byteArrayOfConvertedWallList;
      }

      set
      {
        byteArrayOfConvertedWallList = value;
      }
    }

    public MazeGenerator()
    {
      FillWallListRandomlyWith0or1();
      CreateMazeMessage();
    }

    private void FillWallListRandomlyWith0or1() {
      for (int i = 0; i < SIZE_OF_LIST; i++)
      {
        int randomWall = ran.Next(0, 2);
        wallList.Add(randomWall);
      }
    }

    private string CreateStringFromWallList()
    {
      StringBuilder strBuilder = new StringBuilder();
      foreach (int item in wallList)
      {
        strBuilder.Append(item);
      }
      return strBuilder.ToString();
    }

    private string SplitString(string str, int index)
    {
      return str.Substring((index * 8), ONE_BYTE);
    }

    private void CreateMazeMessage()
    {
      ByteArrayOfConvertedWallList[0] = Convert.ToByte(TCPMessageID.Maze);
      string str = CreateStringFromWallList();
      for (int i = 0; i < SPACE_FOR_TRANSFORMED_LIST; i++)
      {
        ByteArrayOfConvertedWallList[i + 1] = Convert.ToByte(SplitString(str, i), 2);
      }
    }
  }
}