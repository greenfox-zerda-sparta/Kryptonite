using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp {
  class TaskHandler {
    private FileHandler filehandler;

    public TaskHandler() {
      filehandler = new FileHandler();
    }

    public void FileList() {
      int i = 1;
      if (!filehandler.TodoList.Any()) {
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("No TODO\'s for today!:)");
        Console.ResetColor();
      } else {
        foreach (string value in filehandler.TodoList) {
        
          if (value.First() == '0') {
            Console.WriteLine(i + " [ ] " + value.Substring(1));
          } else {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(i + " [X] " + value.Substring(1));
            Console.ResetColor();
          }
          i++;
        }
      }
    }

    public void AddToList(string TaskToBeAdded) {
      filehandler.TodoList.Add('0' + TaskToBeAdded);
      filehandler.WriteToFile();
    }

    public void CheckItOnList(string index) {
      filehandler.ReadFromFile();
      int counter = Int32.Parse(index) - 1; 
      filehandler.TodoList[counter] = "1" + filehandler.TodoList[counter].Substring(1);
      filehandler.WriteToFile();
    }

    public void RemoveFromList(string index) {
      int counter = Int32.Parse(index) - 1;
      int capacity = filehandler.TodoList.Capacity;
        filehandler.TodoList.RemoveAt(counter);

      filehandler.ReadFromFile();
      int counter = Int32.Parse(index) -1;
      filehandler.TodoList[counter] = "1" + filehandler.TodoList[counter].Substring(1);
      filehandler.WriteToFile();
    }
  }
}
