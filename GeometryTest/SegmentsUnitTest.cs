using System;
using System.Text;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfApp2;

namespace GeometryTest
{
    /// <summary>
    /// Сводное описание для SegmentsUnitTest
    /// </summary>
    [TestClass]
    public class SegmentsUnitTest
    {

        

        [TestMethod]
        public void AddVectorTest()
        {

            MainWindow mw = new MainWindow();
            int s = 0;
            foreach(var seg in mw.db.segments)
            {
                s++;
            }
            Assert.IsTrue(s == 0);

            
            //mw.AddVector("2", "4", "6", "2");
           
        }
    }
}
