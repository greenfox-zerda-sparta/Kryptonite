using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp {
  class ArgumentHandler {
    private string[] arguments;
    private string firstarg = "";
    private string task = "";

    private ErrorHandler errorhandler;

    public ArgumentHandler(string[] args) {
      errorhandler = new ErrorHandler();
      this.arguments = args;
      SetArguments();
    }

    public void SetArguments() {
      if (ExistArgs()) {
        firstarg = arguments.First();
        if (ExistTwoArgs()) {
          task = arguments[1];
        }
        else {
          task = "-1";
        }
      } 
      else {
        firstarg = "-1";
        task = "-1";
      }
    }

    private bool ExistArgs() {
      return arguments.Count() > 0;
    }

    private bool ExistTwoArgs() {
      return arguments.Count() > 1;
    }

    public string GetFirstArg() {
      return firstarg;
    }

    public string GetTask() {
      return task;
    }

    private bool IsTaskBiggerNumberThanZero() {
      return Int32.Parse(task) > 0;
    } 

    public void RunbyArg() {
      TaskHandler taskhandler = new TaskHandler();
      if (!ExistArgs()) {
        //taskHandler.WriteManual();
      }
      else {
        switch (firstarg) {
          case "-l":
            taskhandler.FileList();
            break;
          case "-a":
            taskhandler.AddToList(task);
            break;
          case "-r":
            if (IsTaskBiggerNumberThanZero()) {
              try {
                taskhandler.RemoveFromList(task);
              } catch (Exception e) {
                errorhandler.WriteError(21);
              }
            }
            break;
          case "-c":
            if (IsTaskBiggerNumberThanZero()) {
              try {
                taskhandler.CheckItOnList(task);
              } catch (Exception e) {
                errorhandler.WriteError(21);
              }
            }
            break;
          default:
            errorhandler.WriteError(10);
            break;
        }
      }
    }
  }
}
