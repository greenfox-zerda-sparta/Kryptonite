using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp {
  class FileHandler {
    
    private const string _FILENAME = "todoList.txt";
    private List<string> _todoList;

    public FileHandler() {
      _todoList = new List<string>();
      ReadFromFile();
    }

    public List<string> TodoList {
      get {
        return _todoList;
      }

      set {
        _todoList = value;
      }
    }

    public List<string> ReadFromFile() {
      try { 
        using (System.IO.StreamReader reader = new System.IO.StreamReader(_FILENAME)) {
          string line;
          while ((line = reader.ReadLine()) != null) {
            _todoList.Add(line);
          }
        }
      }
      catch (Exception e) {
        Console.WriteLine("The file could not be read:");
        Console.WriteLine(e.Message);
      }
      return _todoList;
    }

    public void WriteToFile(/*List<string> todoList*/) {
      //_todoList = todoList;
      using (System.IO.StreamWriter writer = new System.IO.StreamWriter(_FILENAME)) {
        foreach (var list in _todoList) {
          writer.WriteLine(list);
        }
      }
    }
  }
}
