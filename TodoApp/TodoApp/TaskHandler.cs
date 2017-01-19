using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp {
  class TaskHandler {
    private FileHandler filehandler;

    public TaskHandler () {
      filehandler = new FileHandler();
    }
    
    public void FileList() {
      int i = 1;
      foreach (string value in filehandler.TodoList) {
        if (value.First() == '0') {
          Console.WriteLine(i + " [ ] " + value.Substring(1));
        } else {
          Console.WriteLine(i + " [X] " + value.Substring(1));
        }
        i++;
      }  
    }

    public void AddToList(string TaskToBeAdded) {
      filehandler.TodoList.Add('0' + TaskToBeAdded);
      filehandler.WriteToFile(filehandler.TodoList);
    }

    public void CheckItOnList(string index) {
      //IsIndexInTheList
      int counter = Int32.Parse(index) -1;
      filehandler.TodoList[counter] = "1" + filehandler.TodoList[counter].Substring(1);
      filehandler.WriteToFile(filehandler.TodoList);
    }
  }
}
