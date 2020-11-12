using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WpfApp2;
using System.Linq;
namespace geomTest
{
    [TestClass]
    public class MainWindowTests
    {

        [TestMethod]
        public void ClearDBTest()
        {
            var mw = new MainWindow();
            int LengthDB = 0;
            mw.AddVector(5, 6, 8, 9);
            foreach (var p in mw.db.segments)
            {
                LengthDB++;
            }
            Assert.IsFalse(LengthDB == 0);
            mw.ClearDB();
            LengthDB = 0;
            foreach (var p in mw.db.segments)
            {
                LengthDB++;
            }
            Assert.IsTrue(LengthDB == 0);
        }

        [TestMethod]
        public void AddVectorTest()
        {
            var mw = new MainWindow();
            int LengthDB = 0;
            int x1 = 6, x2 = 9, y1 = 9, y2 = 1,
                x3 = 6, x4 = 9, y3 = 1, y4 = 9;
            mw.ClearDB();
            LengthDB = 0;
            foreach (var p in mw.db.segments)
            {
                LengthDB++;
            }
            Assert.IsTrue(LengthDB == 0);
            LengthDB = 0;
            mw.AddVector(x1, x2, y1, y2);

            var ResDBVector = mw.db.segments.Where(s => s.x1 == x1 && s.x2 == x2 && s.y1 == y1 && s.y2 == y2).First<segments>();

            Assert.IsTrue(ResDBVector.x1 == x1);
            Assert.IsTrue(ResDBVector.x2 == x2);
            Assert.IsTrue(ResDBVector.y1 == y1);
            Assert.IsTrue(ResDBVector.y2 == y2);
        }

        [TestMethod]
        public void AddAngleTest()
        {
            var mw = new MainWindow();
            int LengthDB = 0;
            int x1 = 6, x2 = 9, y1 = 9, y2 = 1,
                x3 = 6, x4 = 9, y3 = 1, y4 = 9;
            mw.ClearDB();
            LengthDB = 0;
            foreach (var p in mw.db.segments)
            {
                LengthDB++;
            }
            Assert.IsTrue(LengthDB == 0);
            LengthDB = 0;
            mw.AddVector(x1, x2, y1, y2);
            mw.AddVector(x3, x4, y3, y4);
            var seg1 = mw.db.segments.Where(s => s.x1 == 6 && s.x2 == 9 && s.y1 == 9 && s.y2 == 1).First<segments>();
            var seg2 = mw.db.segments.Where(s => s.x1 == 6 && s.x2 == 9 && s.y1 == 1 && s.y2 == 9).First<segments>();

            mw.AddAngle(seg1.Name, seg2.Name);
            var ang = mw.db.angles.Where(w => w.segment1 == seg1.id && w.segment2 == seg2.id).First<angles>();

            Assert.IsTrue(ang.segment1 == seg1.id);
            Assert.IsTrue(ang.segment2 == seg2.id);


        }

        [TestMethod]
        public void AddRelationTest()
        {
            var mw = new MainWindow();
            int LengthDB = 0;
            int x1 = 6, x2 = 9, y1 = 9, y2 = 1,
                x3 = 6, x4 = 9, y3 = 1, y4 = 9;
            mw.ClearDB();
            LengthDB = 0;
            foreach (var p in mw.db.segments)
            {
                LengthDB++;
            }
            Assert.IsTrue(LengthDB == 0);
            LengthDB = 0;
            mw.AddVector(x1, x2, y1, y2);
            int rel1 = 3, rel2 = 7;

            var seg1 = mw.db.segments.Where(s => s.x1 == 6 && s.x2 == 9 && s.y1 == 9 && s.y2 == 1).First<segments>();
            mw.AddRelation(seg1.Name, rel1, rel2);
            var rel = mw.db.Relations.Where(w => w.segment_ID == seg1.id && w.relation1 == rel1 && w.relation2 == rel2).First<Relations>();

            Assert.IsTrue(rel.segment_ID == seg1.id);
            Assert.IsTrue(rel.relation1 == rel1);
            Assert.IsTrue(rel.relation2 == rel2);
        }


    }
    [TestClass]
    public class graphWindowTests
    {
        [TestMethod]
        public void IntersectionTest()
        {
            var gw = new graphWindow();
            var mw = new MainWindow();
            int LengthDB = 0;
            int x1 = 6, x2 = 9, y1 = 9, y2 = 1,
                x3 = 6, x4 = 9, y3 = 1, y4 = 9;
            mw.ClearDB();
            LengthDB = 0;
            foreach (var p in mw.db.segments)
            {
                LengthDB++;
            }
            Assert.IsTrue(LengthDB == 0);
            LengthDB = 0;
            mw.AddVector(x1, x2, y1, y2);
            mw.AddVector(x3, x4, y3, y4);
            var seg1 = mw.db.segments.Where(s => s.x1 == 6 && s.x2 == 9 && s.y1 == 9 && s.y2 == 1).First<segments>();
            var seg2 = mw.db.segments.Where(s => s.x1 == 6 && s.x2 == 9 && s.y1 == 1 && s.y2 == 9).First<segments>();

            Assert.IsTrue(gw.Intersection(seg1, seg2));




            LengthDB = 0;
            mw.AddVector(1, 9, 1, 1);
            mw.AddVector(1, 9, 2, 2);
            seg1 = mw.db.segments.Where(s => s.x1 == 1 && s.x2 == 9 && s.y1 == 1 && s.y2 == 1).First<segments>();
            seg2 = mw.db.segments.Where(s => s.x1 == 1 && s.x2 == 9 && s.y1 == 2 && s.y2 == 2).First<segments>();

            Assert.IsFalse(gw.Intersection(seg1, seg2));
        }


        [TestMethod]
        public void VectorAngTest()
        {
            var gw = new graphWindow();
            var mw = new MainWindow();
            int LengthDB = 0;
            int x1 = 6, x2 = 9, y1 = 9, y2 = 1,
                x3 = 6, x4 = 9, y3 = 1, y4 = 9;
            mw.ClearDB();
            LengthDB = 0;
            foreach (var p in mw.db.segments)
            {
                LengthDB++;
            }
            Assert.IsTrue(LengthDB == 0);
            LengthDB = 0;

            mw.AddVector(x1, x2, y1, y2);
            mw.AddVector(x3, x4, y3, y4);
            var seg1 = mw.db.segments.Where(s => s.x1 == 6 && s.x2 == 9 && s.y1 == 9 && s.y2 == 1).First<segments>();
            var seg2 = mw.db.segments.Where(s => s.x1 == 6 && s.x2 == 9 && s.y1 == 1 && s.y2 == 9).First<segments>();
            Assert.IsNotNull(gw.VectorAng(seg1, seg2));
        }

        [TestMethod]
        public void ReletionTest()
        {
            var gw = new graphWindow();
            var mw = new MainWindow();
            int LengthDB = 0;
            int x1 = 6, x2 = 9, y1 = 9, y2 = 1,
                x3 = 6, x4 = 9, y3 = 1, y4 = 9;
            mw.ClearDB();
            LengthDB = 0;
            foreach (var p in mw.db.segments)
            {
                LengthDB++;
            }
            Assert.IsTrue(LengthDB == 0);
            LengthDB = 0;

            mw.AddVector(x1, x2, y1, y2);

            var seg1 = mw.db.segments.Where(s => s.x1 == 6 && s.x2 == 9 && s.y1 == 9 && s.y2 == 1).First<segments>();

            int rel1 = 3, rel2 = 7;

            mw.AddRelation(seg1.Name, rel1, rel2);
            var rel = mw.db.Relations.Where(w => w.segment_ID == seg1.id && w.relation1 == rel1 && w.relation2 == rel2).First<Relations>();

            Assert.IsFalse(gw.Reletion(rel, "") != 0);
            Assert.IsTrue(gw.Reletion(rel, "x") != 0);
            Assert.IsTrue(gw.Reletion(rel, "y") != 0);


        }


       

    }
}
