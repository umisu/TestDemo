using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyMath;
using System;

namespace MathTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            MyMathFunctions myMathFunctions = new MyMathFunctions();
            int result = myMathFunctions.sum("1", "4");
            int expectedResult = 5;
            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        public void TestMethod2()
        {
            MyMathFunctions myMathFunctions = new MyMathFunctions();
            int result = myMathFunctions.sum("3", "7");
            int expectedResult = 10;
            Assert.AreEqual(result, expectedResult);
        }

        [TestMethod]

        public void TestMethod3()
        {
            MyMathFunctions myMathFunctions = new MyMathFunctions();
            var ex = Assert.ThrowsException<Exception>(() => myMathFunctions.sum("r", "7"));
            Assert.AreEqual(ex.Message, "The given value is not a number: r");
        }

        [TestMethod]
        public void TestMethod4()
        {
            MyMathFunctions myMathFunctions = new MyMathFunctions();
            long result = myMathFunctions.sum("2147483647", "1");
            long expectedResult = 2147483648;
            Assert.AreEqual(result, expectedResult);
        }
    }

}
