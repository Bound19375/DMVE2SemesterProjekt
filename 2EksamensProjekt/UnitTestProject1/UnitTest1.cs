using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // arrange
            List<string> test = DAL.Getinstance().WaitlistUsernames();
            // act
            test
            // assert
            mock.Received.SomeMethod();
        }
    }
}
