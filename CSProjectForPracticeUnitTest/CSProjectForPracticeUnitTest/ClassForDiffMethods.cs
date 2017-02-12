using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSProjectForPracticeUnitTest {
  public class ClassForDiffMethods {
    private int number1;
    private int number2;

    public int Number1
    {
      get
      {
        return number1;
      }

      set
      {
        number1 = value;
      }
    }

    public int Number2
    {
      get
      {
        return number2;
      }

      set
      {
        number2 = value;
      }
    }

    public ClassForDiffMethods() { }

    public int add(int n1, int n2)
    {
      int sum = 0;
      sum = n1 + n2;
      return sum;
    }

    public double divide(int n1, int n2)
    {
      if (n2 == 0)
      {
        throw new DivideByZeroException();
      }
      else
      {
        double result = n1 / n2;
        return result;
      }
    }
    
    public bool isBigger(int n1, int n2)
    {
      return n1 > n2;
    }
  }
}
