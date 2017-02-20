using GameServer;
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
        Assert.AreEqual(expected[i], actual[i], 0, "Some elements of the byte array were not correctly transformed to list");
      }
    }

    [TestMethod()]
    public void SplitStringToEightCharTest_WithGoodStringlength()
    {
      string teststr = "almaalmaalmaalma";
      string expected = "almaalma";
      string actual = Utility.SplitStringToEightChar(teststr, 0);
      Assert.AreEqual(expected, actual, "The string was not correctly splitted");
    }

    [TestMethod()]
    public void SplitStringToEightCharTest_WithShorterString()
    {
      string teststr = "alma";
      string expected = "alma0000";
      string actual = Utility.SplitStringToEightChar(teststr, 0);
      Assert.AreEqual(expected, actual, "The string was not correctly filled");
    }

    [TestMethod()]
    [ExpectedException(typeof(NullReferenceException))]
    public void TransformTwoDimensionalByteArrayToListTest_WithException()
    {
      byte[,] arr = null;
      Utility.TransformTwoDimensionalByteArrayToList(arr);
    }

    [TestMethod()]
    public void CreateStringFromListTest_IsEqual()
    {
      List<byte> byteList = new List<byte>() { 1, 2, 3 };
      string expected = "123";
      string actual = Utility.CreateStringFromList(byteList);
      Assert.AreEqual(expected, actual, "Some elements of the byte list were not correctly converted to string");
    }

    [TestMethod()]
    public void CreateStringFromListTest_IsNotEqual()
    {
      List<byte> byteList = new List<byte>() { 1, 2, 3 };
      string expected = "12";
      string actual = Utility.CreateStringFromList(byteList);
      Assert.AreNotEqual(expected, actual);
    }

    [TestMethod()]
    [ExpectedException(typeof(NullReferenceException))]
    public void CreateStringFromListTest_WithException()
    {
      List<byte> list = null;
      Utility.CreateStringFromList(list);
    }
  }
}