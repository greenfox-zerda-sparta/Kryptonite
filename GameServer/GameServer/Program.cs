using System.Collections.Generic;
namespace GameServer
{
  class Program
  {
    static void Main(string[] args)
    {
      ServerSelector serverselector = new ServerSelector();
      serverselector.select();
    }
  }
}
