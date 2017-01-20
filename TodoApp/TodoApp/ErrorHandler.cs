using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// main 0- 9
// arguments 10-19
// taskhandler 20-29
// filehandler 30 - 39 

namespace TodoApp {
  class ErrorHandler {

    public void WriteError(int errorcode) {
      switch (errorcode) {
        case 10:
          UsageInfo();
          break;
        case 11:
          Console.WriteLine("Wrong argument: After -r must be a greater number than zero");
          break;
        case 21:
          Console.ForegroundColor = ConsoleColor.Magenta;
          Console.WriteLine("No Todo with this index in the list");
          Console.ResetColor();
          break;
        case 30:
          Console.WriteLine("File-hiba.");
          break;
      }
    }

    public void UsageInfo() {
      Console.ForegroundColor = ConsoleColor.Green;
      Console.Write("CLI Todo application:\n"+
                     "====================\r\n" +
                     "Command line arguments:\n" + 
                     "-l   Lists all the tasks\n" +
                     "-a   Adds a new task\n"+
                     "-r   Removes a task\n"+
                     "-c   Completes a task\n");
      Console.ResetColor();
    }
  }
}
