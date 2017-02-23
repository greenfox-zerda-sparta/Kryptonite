using System.Collections.Generic;

namespace GameServer {
  class TrapGenerator {
    private List<byte> trapList = new List<byte>();
    private List<byte> wallListWithPathAndItems = new List<byte>();
    private List<byte> wallListWithPathAndItemsAndTrapsOutsidePath = new List<byte>();
    private List<byte> wallListWithPathAndItemsAndTrapsOnAndOutsidePath = new List<byte>();
    private byte[] trapMessageArray;
   
    public byte[] TrapMessageArray
    {
      get
      {
        return trapMessageArray;
      }
    }

    public TrapGenerator(List<byte> wallListWithPathAndItems)
    {
      this.wallListWithPathAndItems = wallListWithPathAndItems;
      wallListWithPathAndItemsAndTrapsOutsidePath = Utility.GenerateThings(Utility.PERCENIGE_FOR_NUMBER_OF_TRAPS_OUTSIDE_PATH, wallListWithPathAndItems, Utility.ROAD_ID, Utility.TRAP_ID);
      wallListWithPathAndItemsAndTrapsOnAndOutsidePath = Utility.GenerateThings(Utility.PERCENIGE_FOR_NUMBER_OF_TRAPS_ON_PATH, wallListWithPathAndItemsAndTrapsOutsidePath, Utility.PATH_ID, Utility.TRAP_ID);
      trapList = Utility.CreateListForMessage(wallListWithPathAndItemsAndTrapsOnAndOutsidePath, Utility.TRAP_ID);
      trapMessageArray = Utility.CreateMessage(TCPMessageID.TrapPosition, trapList);
    }
  }
}