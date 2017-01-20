using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TodoApp {
  class Program {
    static void Main(string[] args) {
      ArgumentHandler arghandler = new ArgumentHandler(args);
      arghandler.RunbyArg();
    }
  }
}
