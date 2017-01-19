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
          Console.WriteLine("Wrong arguments!");
          break;
        case 11:
          Console.WriteLine("Wrong argument: After -r must be a greater number than zero");
          break;
      }
    }
  }
}
