using System.Collections.Generic;

namespace GameServer {
  class ItemGenerator {
    private List<byte> itemList;
    private byte[] itemMessageArray;
    private List<byte> wallListWithPath = new List<byte>();
    private List<byte> wallListWithPathAndItemsOutsidePath = new List<byte>();
    private List<byte> wallListWithPathAndItemsOnAndOutsidePath = new List<byte>();

    public byte[] ItemMessageArray
    {
      get
      {
        return itemMessageArray;
      }
    }

    public List<byte> WallListWithPathAndItemsOnAndOutsidePath
    {
      get
      {
        return wallListWithPathAndItemsOnAndOutsidePath;
      }
    }

    public ItemGenerator(byte[,] wallArrayWithPath)
    {
      wallListWithPath = Utility.TransformTwoDimensionalByteArrayToList(wallArrayWithPath);
      wallListWithPathAndItemsOutsidePath = Utility.GenerateThings(Utility.PERCENIGE_FOR_NUMBER_OF_ITEMS_OUTSIDE_PATH, wallListWithPath, Utility.ROAD_ID, Utility.ITEM_ID);
      wallListWithPathAndItemsOnAndOutsidePath = Utility.GenerateThings(Utility.PERCENIGE_FOR_NUMBER_OF_ITEMS_ON_PATH, wallListWithPathAndItemsOutsidePath, Utility.PATH_ID, Utility.ITEM_ID);
      itemList = Utility.CreateListForMessage(WallListWithPathAndItemsOnAndOutsidePath, Utility.ITEM_ID);
      itemMessageArray = Utility.CreateMessage(TCPMessageID.ItemPosition, itemList);
    }
  }
}
