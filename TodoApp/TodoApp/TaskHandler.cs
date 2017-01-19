using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp {
  class TaskHandler {
    public TaskHandler () {
       
    }
    
    public void FileList(List<string> buff) {
      int i = 1;
      foreach (string value in buff) {
        if (value.First() == '0') {
          Console.WriteLine(i + " [ ] " + value.Substring(1));
        } else {
          Console.WriteLine(i + " [X] " + value.Substring(1));
        }
        i++;
      }  
    }

    public List<string> AddToList(List<string> buff, string TaskToBeAdded) {
      buff.Add('0' + TaskToBeAdded);
      return buff;
    }

    public List<string> CheckItOnList(List<string> buff, string index) {
      buff[int.Parse(index) - 1].Replace('0', '1');
      return buff;
    }
  }
}
