using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer {
  public static class Utility {
    public const int NUMBER_OF_ROWS = 12;
    public const int NUMBER_OF_COLOUMNS = 12;
    public const int SPACE_FOR_TRANSFORMED_LIST = NUMBER_OF_ROWS * NUMBER_OF_COLOUMNS / ONE_BYTE;
    public const int SPACE_FOR_MESSAGEID = 1;
    public const int ROAD_ID = 0;
    public const int TRAP_ID = 2;
    public const int ONE_BYTE = 8;
    public const int RAND_MINIMUM = 0;
    public const int RAND_MAXIMUM_FOR_ROWS = NUMBER_OF_ROWS;
    public const int RAND_MAXIMUM_FOR_COLOUMNS = NUMBER_OF_COLOUMNS;
    public const double PERCENIGE_FOR_NUMBER_OF_TRAPS = 0.05;
    public static Random ran = new Random();

    public static string CreateStringFromList(List<byte> list)
    {
      StringBuilder strBuilder = new StringBuilder();
      foreach (int item in list)
      {
        strBuilder.Append(item);
      }
      return strBuilder.ToString();
    }

    public static string SplitStringToEightBits(string str, int number)
    {
      while (str.Length % ONE_BYTE != 0)
      {
        str += "0";
      }
      return str.Substring((number * ONE_BYTE), ONE_BYTE);
    }

    public static List<byte> TransformTwoDimensionalByteArrayToList(byte[,] arr)
    {
      if (arr == null)
      {
        throw new Exception("The given array is not initialised.");
      }
      List<byte> byteList = arr.Cast<byte>().ToList();
      return byteList;
    }
  }
}
