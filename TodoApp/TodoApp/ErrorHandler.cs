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
    private int errorcode;

    public ErrorHandler(){
      this.errorcode = -1;
    }

    public void WriteError(int _errorcode) {
      switch (errorcode) {
        case 0:
          Console.WriteLine("No error!");
          break;
        case 30:
          Console.WriteLine("File-hiba.");
          break;
      }
    }
  }
}
