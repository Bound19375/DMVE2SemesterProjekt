
using _2EksamensProjekt.DAL;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MySql.Data.MySqlClient;
using System.Threading.Tasks;
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
                test.SpecialCollectionList(gv, API.SPECIALCOLLECTION.SortByAll);
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
            bool result = false;
            try
            {
                switch (test.Login("A", "1").Result)
                {
                    case "secretary":
                        result = true;
                        break;
                    case "admin":
                        result = true;
                        break;
                    case "youth":
                        result = true;
                        break;
                    case "senior":
                        result = true;
                        break;
                    case "normal":
                        result = true;
                        break;
                }
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
                ComboBox user = new ComboBox();
                min.Text = "100";
                max.Text = "0";
                user.Text = "BoundSoul";
                test.TextboxReader(min, API.SetReaderField.Min);
                test.TextboxReader(max, API.SetReaderField.Max);
                test.ComboBoxReader(user, API.SetReaderField.SortUsername);
                test.AdminStatisticsPrint(API.ResourceSort.AllPerUnit);
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