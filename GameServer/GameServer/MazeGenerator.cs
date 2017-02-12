using System;
using System.Collections.Generic;
using System.Text;

namespace GameServer {
  class MazeGenerator {

    private const int SIZE_OF_WALLLIST = 104;
    //private double size = Math.Ceiling(Convert.ToDouble(SIZE_OF_WALLLIST) / 8);
    private const int SIZE_OF_BYTEARR_FOR_WALLLIST = SIZE_OF_WALLLIST / 8;

    private Random ran = new Random();
    private StringBuilder strBuilder = new StringBuilder();

    public static List<int> wallList = new List<int>();
    public static byte[] byteArrOfWallList = new byte[SIZE_OF_BYTEARR_FOR_WALLLIST + 1];

    public MazeGenerator()
    {
      FillWallListRandomlyWith0or1();
      ConvertWallListToBytes();
    }

    private void FillWallListRandomlyWith0or1() {
      for (int i = 0; i < SIZE_OF_WALLLIST; i++)
      {
        int randomWall = ran.Next(0, 2);
        wallList.Add(randomWall);
      }
    }

    private void ConvertWallListToBytes() {
      byteArrOfWallList[0] = Convert.ToByte(TCPMessageID.Maze); //mazeflag
      int j = 1;
      for (int i = 1; i <= SIZE_OF_WALLLIST; i++)
      {
        strBuilder.Append(wallList[i - 1].ToString());
        if (i % 8 == 0)
        {
          byteArrOfWallList[j] = Convert.ToByte(strBuilder.ToString(), 2);
          strBuilder.Clear();
          j++;
        }
      }
    }
  }
}
