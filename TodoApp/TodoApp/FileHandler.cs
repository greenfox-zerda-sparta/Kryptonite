using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TodoApp {
  class FileHandler {

    private const string _FILENAME = "todoList.txt";
    private List<string> _todoList;

    public FileHandler()
    {
      _todoList = new List<string>();
      ReadFromFile();
    }

    public List<string> TodoList
    {
      get
      {
        return _todoList;
      }
      set
      {
        _todoList = value;
      }
    }

    public string[] textToByteAndToStringArr(FileStream stream) {
      byte[] fileBytes = new byte[stream.Length];
      stream.Read(fileBytes, 0, fileBytes.Length);
      string[] arr = (Encoding.Default.GetString(
                 fileBytes,
                 0,
                 fileBytes.Length - 1)).Split(new string[] { "\r\n" },
                                             StringSplitOptions.None);
      return arr;
    }

    public List<string> ReadFromFile() {
      if (!File.Exists(_FILENAME)) {
        using (FileStream fs = new FileStream(_FILENAME, FileMode.CreateNew)) ;
      }
      else {
        using (FileStream stream = File.OpenRead(_FILENAME)) {
          if (stream.Length > 0) {
            _todoList = textToByteAndToStringArr(stream).ToList();
          }
        }
      }
      return _todoList;
    }

    public byte[] stringListToByte() {
      byte[] buffer = new byte[1024];
      string str = "";
      foreach (string item in _todoList) {
        str += item + "\r\n";
      }
      buffer = Encoding.UTF8.GetBytes(str);
      return buffer;
    }

    public void WriteToFile() {
      ErrorHandler err = new ErrorHandler();
      try
      {
        using (FileStream stream = File.OpenWrite(_FILENAME))
        {
          if (stream.Length >= 0)
          {
            byte[] buffer = new byte[1024];
            buffer = stringListToByte();
            stream.Write(buffer, 0, buffer.Length);
          }
        }
      }
      catch (FileNotFoundException ioEx)
      {
        err.WriteError(30);
        //Console.WriteLine(ioEx.Message);
      }
    }
  }
}