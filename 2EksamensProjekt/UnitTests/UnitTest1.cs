using DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSlogan()
        {
            //Arrange test
            API test = API.Getinstance();

            //Act test
            string result = test.SloganT().Result;

            //Assert
            if (result is string)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void TestOpenConn()
        {
            //Arrange test
            API test = API.Getinstance();
            string ConnStr = "server=62.61.157.3;port=80;database=2SemesterEksamen;user=plebs;password=1234;SslMode=none;";
            MySqlConnection conn = new MySqlConnection(ConnStr);

            //Act test
            MySqlConnection resultOpen = test.OpenConn(conn);
            MySqlConnection resultClose = test.CloseConn(conn);

            //Assert
            if (resultOpen is MySqlConnection && resultClose is MySqlConnection)
            {
                Assert.IsTrue(true);
            }
            else
            {
                Assert.IsTrue(false);
            }
        }

        [TestMethod]
        public void TestUserCreateWaitlist()
        {
            //Arrange test
            API test = API.Getinstance();

            //Act test
            bool result;
            try
            {
                test.CreateUser_Waitlist();
                result = true;
            }
            catch
            {
                result = false;
            }

            //Assert
            Assert.IsTrue(result);
        }

        //Assert test
        //Assert.AreEqual(, result);
    }
}