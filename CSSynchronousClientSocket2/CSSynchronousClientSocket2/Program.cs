using System;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSSynchronousClientSocket2 {
  class Program {
    public static int Main(String[] args)
    {
      SynchronousSocketClient.StartClient();
      return 0;
    }
  }
}
