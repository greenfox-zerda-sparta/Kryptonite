using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;

namespace GameServer
{
  class ServerSelector
  {
    private string arg;

    public ServerSelector()
    {
      arg = "";
    }

    private void inputArg()
    {
      Console.WriteLine("Write -u- for UDP Server start\nWrite -t- for TCP Server start\nWrite -all- to start both\nType -exit- for quit");
      arg = Console.ReadLine();
    }

    public void select()
    {
      inputArg();
      switch (arg)
      {
        case "all":
          Console.WriteLine("Both starting...");
          break;
        case "u":
          Console.WriteLine("UDP starting...");
          break;
        case "t":
          Console.WriteLine("TCP starting...");
          break;
        case "exit":
          Console.WriteLine("Bye!  ^.^");
          System.Environment.Exit(1);
          break;
        default:
          Console.WriteLine("Wrong arguments");
          select();
          break;
      }
    }
  }
}
