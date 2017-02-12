using Microsoft.VisualStudio.TestTools.UnitTesting;
using CSProjectForPracticeUnitTest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSProjectForPracticeUnitTest.Tests {
  [TestClass()]
  public class ClassForDiffMethodsTests {
    [TestMethod()]
    public void addTest_is_give_back_a_good_sum()
    {
      int n1 = 2;
      int n2 = 2;
      int expected = 4;
      var sg = new ClassForDiffMethods();
      int actual = sg.add(n1, n2);

      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    public void addTest_with_wrong_sum()
    {
      int n1 = 2;
      int n2 = 2;
      int notExpected = 3;
      var sg = new ClassForDiffMethods();
      int actual = sg.add(n1, n2);
      Assert.AreNotEqual(notExpected, actual);
    }

    [TestMethod()]
    [Timeout(1000)]
    public void divideTest()
    {
      //int n1 = 2;
      //int n2 = 2;
      //int expected = 1;
      var sg = new ClassForDiffMethods();
      //double actual = sg.divide(n1, n2);
      Assert.AreEqual(1, sg.divide(2, 2));
      Assert.AreEqual(1, sg.divide(-2, -2));
      Assert.AreEqual(5, sg.divide(25, 5));
    }

    [TestMethod()]
    [ExpectedException(typeof(DivideByZeroException))]
    public void divideTestWithExeption()
    {
      var sg = new ClassForDiffMethods();
      sg.divide(2, 0);
    }

    [TestMethod()]
    [Timeout(TestTimeout.Infinite)]
    public void isBiggerTest_true()
    {
      var sg = new ClassForDiffMethods();
      Assert.IsTrue(sg.isBigger(3, 2));
    }

    [TestMethod()]
    public void isBiggerTest_false()
    {
      var sg = new ClassForDiffMethods();
      Assert.IsFalse(sg.isBigger(2, 4));
    }
  }
}

