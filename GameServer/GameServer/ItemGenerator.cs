using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer {
  class ItemGenerator {
    private byte[,] itemArray = new byte[Utility.NUMBER_OF_ROWS, Utility.NUMBER_OF_COLOUMNS];
    private List<byte> itemList;
    private byte[] itemMessageArray;
    private List<byte> wallList;

    public byte[] ItemMessageArray
    {
      get
      {
        return itemMessageArray;
      }

      set
      {
        itemMessageArray = value;
      }
    }

    public List<byte> ItemList
    {
      get
      {
        return itemList;
      }

      set
      {
        itemList = value;
      }
    }

    public ItemGenerator(byte[,] wallArrayWithPath)
    {
      ItemList = new List<byte>();
      wallList = Utility.TransformTwoDimensionalByteArrayToList(wallArrayWithPath);
      GenerateItemsFromMazeList();
    }


    private void GenerateItemsFromMazeList()
    {
      int number_of_items_outside_path = CountNumberOfItemsOutsidePath();
      int created_items_outside_path = 0;
      {
        do
        {
          int i = Utility.ran.Next(Utility.RAND_MINIMUM, Utility.RAND_MAXIMUM_FOR_ROWS * Utility.RAND_MAXIMUM_FOR_COLOUMNS);
          if (wallList[i] == Utility.ROAD_ID && i != Utility.BEGINNING_POINT)
          {
            wallList[i] = Utility.ITEM_ID;
            created_items_outside_path++;
          }
        } while (created_items_outside_path != number_of_items_outside_path);
      }

      int number_of_items_on_path = CountNumberOfItemsOnPath();
      int created_items_on_path = 0;
      {
        do
        {
          int i = Utility.ran.Next(Utility.RAND_MINIMUM, Utility.RAND_MAXIMUM_FOR_ROWS * Utility.RAND_MAXIMUM_FOR_COLOUMNS);
          if (wallList[i] == Utility.PATH_ID && i != Utility.BEGINNING_POINT)
          {
            wallList[i] = Utility.ITEM_ID;
            created_items_on_path++;
          }
        } while (created_items_on_path != number_of_items_on_path);
      }
      CreateItemList();
    }

    private int CountNumberOfItemsOutsidePath()
    {
      return (int)(Utility.NUMBER_OF_ROWS * Utility.NUMBER_OF_COLOUMNS * Utility.PERCENIGE_FOR_NUMBER_OF_ITEMS_OUTSIDE_PATH);
    }

    private int CountNumberOfItemsOnPath()
    {
      return (int)(Utility.NUMBER_OF_ROWS * Utility.NUMBER_OF_COLOUMNS * Utility.PERCENIGE_FOR_NUMBER_OF_ITEMS_ON_PATH);
    }

    private void CreateItemList()
    {
      foreach (var item in wallList)
      {
        if (item == 4)
        {
          ItemList.Add(1);
        }
        else
        {
          ItemList.Add(0);
        }
      }
    }

    public byte[] CreateMessage()
    {
      ItemMessageArray = new byte[Utility.MESSAGE_ARRAY_SIZE];
      ItemMessageArray[0] = Convert.ToByte(TCPMessageID.ItemPosition);
      string str = "";

      try
      {
        str = Utility.CreateStringFromList(ItemList);
      }
      catch (NullReferenceException e)
      {
        Console.WriteLine(e);
      }

      for (int i = 0; i < ItemMessageArray.Length - 1; i++)
      {
        ItemMessageArray[i + 1] = Convert.ToByte(Utility.SplitStringToEightChar(str, i), 2);
      }
      return ItemMessageArray;
    }
  }
}
