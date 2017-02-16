using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer {
  public static class Utility {
    public const int NUMBER_OF_ROWS = 11;
    public const int NUMBER_OF_COLOUMNS = 11;
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

    public static byte[] CreateMessage(int messageId, List<byte> list)
    {
      byte[] messageArr = new byte[SPACE_FOR_MESSAGEID + list.Count / ONE_BYTE];
      messageArr[0] = Convert.ToByte(messageId);
      string str = "";

      try
      {
        str = Utility.CreateStringFromList(list);
      }
      catch (NullReferenceException e)
      {
        Console.WriteLine(e);
      }
      // ArgumentOutOfRangeException
      for (int i = 0; i < messageArr.Length - 1; i++)
      {
        messageArr[i + 1] = Convert.ToByte(Utility.SplitStringToEightChar(str, i), 2);
      }

      return messageArr;
    }
  }
}
