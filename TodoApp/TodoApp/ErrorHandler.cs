using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
      }
    }
  }
}
