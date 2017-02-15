using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace GameServer.Tests {
  [TestClass()]
  public class UtilityTests {

    [TestMethod()]
    public void TransformTwoDimensionalByteArrayToListTest()
    {
      byte[,] testArray = new byte[2, 2] { { 1, 2 }, { 3, 4 } };
      List<byte> expected = new List<byte>() { 1, 2, 3, 4 };
      List<byte> actual = Utility.TransformTwoDimensionalByteArrayToList(testArray);
      for (int i = 0; i < expected.Count; i++)
      {
        Assert.AreEqual(expected[i], actual[i]);
      }
    }

    [TestMethod()]
    public void SplitStringToEightBitsTest_WithGoodStringlength()
    {
      string teststr = "almaalmaalmaalma";
      string expected = "almaalma";
      string actual = Utility.SplitStringToEightBits(teststr, 0);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    public void SplitStringToEightBitsTest_WithShorterString()
    {
      string teststr = "alma";
      string expected = "alma0000";
      string actual = Utility.SplitStringToEightBits(teststr, 0);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    [ExpectedException(typeof(Exception))]
    public void TransformTwoDimensionalByteArrayToListTest_WithException()
    {
      byte[,] arr = null;
      Utility.TransformTwoDimensionalByteArrayToList(arr);
    }

    [TestMethod()]
    public void CreateStringFromListTest_Equal()
    {
      List<byte> byteList = new List<byte>() { 1, 2, 3 };
      string expected = "123";
      string actual = Utility.CreateStringFromList(byteList);
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    public void CreateStringFromListTest_NotEqual()
    {
      List<byte> byteList = new List<byte>() { 1, 2, 3 };
      string expected = "12";
      string actual = Utility.CreateStringFromList(byteList);
      Assert.AreNotEqual(expected, actual);
    }
  }
}