using _2EksamensProjekt.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using System.Windows.Forms;

namespace UnitTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestSlogan()
        {
            //Arrange test
            API test = API.GetInstance();

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
        public void TestConn()
        {
            //Arrange test
            API test = API.GetInstance();
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
        public void TestSpecialCollectionList()
        {
            //Arrange test
            API test = API.GetInstance();

            //Act test
            bool result;
            try
            {
                DataGridView gv = new DataGridView();
                string sql = "SELECT h.id, h.`type`, h.rental_price, h.m2 FROM housing h WHERE h.id NOT IN(SELECT hr2.housing_id FROM housing_residents hr2) GROUP BY h.id ORDER BY h.id;";
                test.GridviewCollection(gv,sql);
                result = true;
            }
            catch
            {
                result = false;
            }
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestLogin()
        {
            //Arrange test
            API test = API.GetInstance();

            //Act test
            bool result;
            try
            {
                test.Login("A", "1");
                result = true;
            }
            catch
            {
                result = false;
            }
            //Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void TestAdminPrint()
        {
            //Arrange test
            API test = API.GetInstance();

            //Act test
            bool result;
            try
            {
                TextBox min = new TextBox();
                TextBox max = new TextBox();
                min.Text = "100";
                max.Text = "0";
                test.TextboxReader(min, API.SetReaderField.Min);
                test.TextboxReader(max, API.SetReaderField.Max);
                test.AdminStatisticsPrint(API.ADMINPRINTSQL.PerUser);
                result = true;
            }
            catch
            {
                result = false;
            }
            //Assert
            Assert.IsTrue(result);
        }
    }
}