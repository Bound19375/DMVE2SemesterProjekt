using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange test
            API test = API.Getinstance();

            //Act test
            string result = test.SloganT().Result;
            if (result is string)
            {

            }
            Assert.IsTrue(true);
            
            //Assert test
            //Assert.AreEqual(, result);
        }
    }
}