using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameServer {
  public static class Utility {
    public const int NUMBER_OF_ROWS = 21;
    public const int NUMBER_OF_COLOUMNS = 21;
   // public const int SPACE_FOR_TRANSFORMED_LIST = NUMBER_OF_ROWS * NUMBER_OF_COLOUMNS / ONE_BYTE;
    public const int SPACE_FOR_MESSAGEID = 1;
    public const int ROAD_ID = 0;
    public const int TRAP_ID = 2;
    public const int PATH_ID = 3;
    public const int ITEM_ID = 4;
    public const int ONE_BYTE = 8;
    public const int RAND_MINIMUM = 0;
    public const int RAND_MAXIMUM_FOR_ROWS = NUMBER_OF_ROWS;
    public const int RAND_MAXIMUM_FOR_COLOUMNS = NUMBER_OF_COLOUMNS;
    public const double PERCENIGE_FOR_NUMBER_OF_TRAPS_OUTSIDE_PATH = 0.01;
    public const double PERCENIGE_FOR_NUMBER_OF_TRAPS_ON_PATH = 0.005;
    public const double PERCENIGE_FOR_NUMBER_OF_ITEMS_OUTSIDE_PATH = 0.01;
    public const double PERCENIGE_FOR_NUMBER_OF_ITEMS_ON_PATH = 0.005;
    public const int BEGINNING_POINT = 418;
    public const int MESSAGE_ARRAY_SIZE = 57;
    public static Random ran = new Random();

    public static string CreateStringFromList(List<byte> list)
    {
      if (list == null)
      {
        throw new NullReferenceException("List is null");
      }
      StringBuilder strBuilder = new StringBuilder();
      foreach (int item in list)
      {
        strBuilder.Append(item);
      }
      return strBuilder.ToString();
    }

    public static string SplitStringToEightChar(string str, int number)
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
        throw new NullReferenceException("Array is null");
      }
      List<byte> byteList = arr.Cast<byte>().ToList();
      return byteList;
    }
  }
}
