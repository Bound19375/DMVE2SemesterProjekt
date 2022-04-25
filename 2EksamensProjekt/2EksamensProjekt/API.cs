using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _2EksamensProjekt;
using DAL;

namespace UnikAPI
{
    public class API : Form
    {
        static API singleton = new API();
        private API() //Private Due to Singleton ^^
        {
        }
        //Singleton
        public static API Getinstance()
        {
            return singleton;
        }

        public async Task<string> SloganT()
        {
           try
           {
                do
                {
                    List<string> list = new List<string>() { "Bo godt – bo hos Sønderbo", "test2", "test3", "test4", "test5", "test6", "test7" };

                    Random r = new Random();
                    int slogan = r.Next(0, list.Count);

                    return await Task.FromResult(list[slogan]);
                }
                while (true);
           }
           catch (Exception ex)
           {
                MessageBox.Show(ex.Message);
           }
           return await Task.FromResult("Slogan Error");
        }
    }


}
