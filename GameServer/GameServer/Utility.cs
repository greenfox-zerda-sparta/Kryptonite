using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameServer {
  public static class Utility {
    public const int NUMBER_OF_ROWS = 21;
    public const int NUMBER_OF_COLOUMNS = 21;
    public const byte SPACE_FOR_MESSAGEID = 1;
    public const byte ROAD_ID = 0;
    public const byte TRAP_ID = 2;
    public const byte PATH_ID = 3;
    public const byte ITEM_ID = 4;
    public const byte ONE_BYTE = 8;
    public const int RAND_MINIMUM = 0;
    public const int RAND_MAXIMUM_FOR_ROWS = NUMBER_OF_ROWS;
    public const int RAND_MAXIMUM_FOR_COLOUMNS = NUMBER_OF_COLOUMNS;
    public const double PERCENIGE_FOR_NUMBER_OF_TRAPS_OUTSIDE_PATH = 0.01;
    public const double PERCENIGE_FOR_NUMBER_OF_TRAPS_ON_PATH = 0.005;
    public const double PERCENIGE_FOR_NUMBER_OF_ITEMS_OUTSIDE_PATH = 0.02;
    public const double PERCENIGE_FOR_NUMBER_OF_ITEMS_ON_PATH = 0.006;
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

    public static List<byte> CreateListForMessage(List<byte> list, int id)
    {
      List<byte> listForMessage = new List<byte>();
      foreach (var item in list)
      {
        if (item == id)
        {
          listForMessage.Add(1);
        }
        else
        {
          listForMessage.Add(0);
        }
      }
      return listForMessage;
    }

    public static byte[] CreateMessage(TCPMessageID id, List<byte> list)
    {
      byte[] messageArray = new byte[MESSAGE_ARRAY_SIZE];
      messageArray[0] = Convert.ToByte(id);
      string str = "";

      try
      {
        str = CreateStringFromList(list);
      }
      catch (NullReferenceException e)
      {
        Console.WriteLine(e);
      }

      for (int i = 0; i < messageArray.Length - 1; i++)
      {
        messageArray[i + 1] = Convert.ToByte(SplitStringToEightChar(str, i), 2);
      }
      return messageArray;
    }

    public static int counterForGenerators(double percentige)
    {
      return (int)(NUMBER_OF_ROWS * NUMBER_OF_COLOUMNS * percentige);
    }

    public static List<byte> GenerateThings(double percentige, List<byte> listWithData, byte idForCompare, byte thingId)
    {
      List<byte> list = listWithData;
      int number_of_things = counterForGenerators(percentige);
      int created_things = 0;
      {
        do
        {
          int i = ran.Next(RAND_MINIMUM, RAND_MAXIMUM_FOR_ROWS * RAND_MAXIMUM_FOR_COLOUMNS);
          if (list[i] == idForCompare && i != BEGINNING_POINT)
          {
            list[i] = thingId;
            created_things++;
          }
        } while (created_things != number_of_things);
      }
      return list;
    }
  }
}
