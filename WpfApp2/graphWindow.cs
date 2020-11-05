using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace WpfApp2
{
    public partial class graphWindow : Form
    {
        public graphWindow()
        {
            InitializeComponent();
            OutPutList();
        }

       public void OutPutList()
        {
            var vector = db.segments.ToList();
            foreach(var p in vector)
            {
                VectorsBox.Items.Add(p.Name);
            }
        }


        TRPOPREntities1 db = new TRPOPREntities1();

        private void Start(object sender, EventArgs e)
        {
            
            chart.ChartAreas.Add(new ChartArea("Default"));

           
            chart.ChartAreas["Default"].AxisX.MajorGrid.Interval = 5;
            chart.ChartAreas["Default"].AxisX.Minimum = 0;
            
            chart.ChartAreas["Default"].AxisY.MajorGrid.Interval = 5;
            chart.ChartAreas["Default"].AxisY.Minimum = 0;
            

            Drowing();



        }

        public void Rotate(double angle, segments seg1, double X, double Y)
        {
            /*
             * X = x * cos(alpha) — y * sin(alpha);
             * Y = x * sin(alpha) + y * cos(alpha);
            */
            
                double angleRadian =angle * Math.PI / 180;
            double OX1 = seg1.x1, OY1 = seg1.y1, OX2 = seg1.x2, OY2 = seg1.y2 ;
            seg1.x1 = (OX1 - X )* Math.Cos(angleRadian) - (OY1 - Y) * Math.Sin(angleRadian) + X;
            seg1.y1 = (OX1 - X) * Math.Sin(angleRadian) + (OY1 - Y) * Math.Cos(angleRadian) + Y;
            seg1.x2 = (OX2 - X) * Math.Cos(angleRadian) - (OY2 - Y) * Math.Sin(angleRadian) + X;
            seg1.y2 = (OX2 - X) * Math.Sin(angleRadian) + (OY2 - Y) * Math.Cos(angleRadian) + Y;

            
            
        }

        public void Drowing()
        {
            string ResAngleList = "";
            foreach (var segment in db.segments)
            {
                chart.Series.Add(new Series(segment.Name));
                chart.Series[segment.Name].ChartArea = "Default";
                chart.Series[segment.Name].ChartType = SeriesChartType.Line;
                chart.Series[segment.Name].BorderWidth = 3;
                

                // добавим данные линии
                double[] axisXData = new double[] { segment.x1, segment.x2 };
                double[] axisYData = new double[] { segment.y1, segment.y2 };
                chart.Series[segment.Name].Points.DataBindXY(axisXData, axisYData);
            }

            List<double> XData = new List<double> { };
            List<double> YData = new List<double> { };
            chart.Series.Add(new Series("Points"));
            foreach (var rel in db.Relations)
            {
                var seg = db.segments.Where(w => w.id == rel.segment_ID).FirstOrDefault<segments>();





                //    double length = Math.Sqrt(Math.Pow((Math.Abs(seg.x1 - seg.x2)), 2) + Math.Pow((Math.Abs(seg.y1 - seg.y2)), 2));
                //int sumRel = rel.relation1 + rel.relation2;
                //double LeftRel = length / sumRel * rel.relation1;
                //double xRel =  (Math.Abs(seg.x1 - seg.x2)) / sumRel * rel.relation1;
                //double yRel =  Math.Sqrt(Math.Pow(LeftRel, 2) - Math.Pow(xRel, 2));
                double relation = Convert.ToDouble(Convert.ToDouble(rel.relation1) / Convert.ToDouble(rel.relation2));
                double XM = Convert.ToDouble(Convert.ToDouble(seg.x1) + relation * Convert.ToDouble(seg.x2)) / (1 + relation);
                double YM = Convert.ToDouble(Convert.ToDouble(seg.y1) + relation * Convert.ToDouble(seg.y2)) / (1 + relation);



                chart.Series["Points"].ChartArea = "Default";
                chart.Series["Points"].ChartType = SeriesChartType.Point;
                XData.Add(XM);
                YData.Add(YM);
                chart.Series["Points"].Points.DataBindXY(XData, YData);
            }



            foreach (var ang in db.angles)
            {

                var seg1 = db.segments.Where(w => w.id == ang.segment1).FirstOrDefault<segments>();
                var seg2 = db.segments.Where(w => w.id == ang.segment2).FirstOrDefault<segments>();
                if (Intersection(seg1, seg2))
                {
                    ResAngleList = ResAngleList + "(" + seg1.x1 + ";" + seg1.y1 + ")" + "(" + seg1.x2 + ";" + seg1.y2 + ")" + "\u2220" + "(" + seg2.x1 + ";" + seg2.y1 + ")" + "(" + seg2.x2 + ";" + seg2.y2 + ")"
                        + " = " + (VectorAng(seg1, seg2) * 180 / Math.PI) + "\r\n";
                    AngleList.Text = ResAngleList;
                }
                else
                {
                    ResAngleList = ResAngleList +  "(" + seg1.x1 + ";" + seg1.y1 + ")" + "(" + seg1.x2 + ";" + seg1.y2 + ")" + "\u2220" + "(" + seg2.x1 + ";" + seg2.y1 + ")" + "(" + seg2.x2 + ";" + seg2.y2 + ")"
                        + " не пересекаются \r\n";
                }

            }


        }

        //расчет угла между отрезками
        public double VectorAng(segments seg1, segments seg2)
        {
            
                double x1 = Math.Abs(seg1.x1 - seg1.x2);
                double x2 = Math.Abs(seg2.x1 - seg2.x2);
                double y1 = Math.Abs(seg1.y1 - seg1.y2);
                double y2 = Math.Abs(seg2.y1 - seg2.y2);
                double CosA = (x1 * x2 + y1 * y2) / (Math.Sqrt(Math.Pow(x1, 2) + Math.Pow(y1, 2)) * Math.Sqrt(Math.Pow(x2, 2) + Math.Pow(y2, 2)));
                double ACosA = Math.Acos(CosA);
                return ACosA;
            
        }


        public bool Intersection(segments seg1, segments seg2)
        {

            double x1, x2, x3, x4, y1, y2, y3, y4;
            double Ua, Ub, numerator_a, numerator_b, denominator;
            if (seg1.x2 <= seg1.x1)
            {
                x1 = seg1.x2;
                x2 = seg1.x1;
                y1 = seg1.y2;
                y2 = seg1.y1;
            }
            else
            {
                x1 = seg1.x1;
                x2 = seg1.x2;
                y1 = seg1.y1;
                y2 = seg1.y2;
            }
            if (seg2.x2 <= seg2.x1)
            {
                x3 = seg2.x2;
                x4 = seg2.x1;
                y3 = seg2.y2;
                y4 = seg2.y1;
            }
            else
            {
                x3 = seg2.x1;
                x4 = seg2.x2;
                y3 = seg2.y1;
                y4 = seg2.y2;
            }
            denominator = (y4 - y3) * (x1 - x2) - (x4 - x3) * (y1 - y2);
            if (denominator == 0)
            {
                if ((x1 * y2 - x2 * y1) * (x4 - x3) - (x3 * y4 - x4 * y3) * (x2 - x1) == 0 && (x1 * y2 - x2 * y1) * (y4 - y3) - (x3 * y4 - x4 * y3) * (y2 - y1) == 0)
                    return true;
                else return false;
            }
            else
            {
                numerator_a = (x4 - x2) * (y4 - y3) - (x4 - x3) * (y4 - y2);
                numerator_b = (x1 - x2) * (y4 - y2) - (x4 - x2) * (y1 - y2);
                Ua = numerator_a / denominator;
                Ub = numerator_b / denominator;
                if (Ua >= 0 && Ua <= 1 && Ub >= 0 && Ub <= 1) return true;
                else return false;
                

            }
           
        }

        private void Rebuilding_Click(object sender, EventArgs e)
        {
            chart.Series.Clear();
            var seg1 = db.segments.Where(w => w.Name == VectorsBox.Text).FirstOrDefault<segments>();
            try
            {
                Rotate(Convert.ToDouble(AngleBox.Text), seg1, Convert.ToDouble(XPoint.Text), Convert.ToDouble(YPoint.Text));
            }
            catch
            {
                MessageBox.Show("Не все поля заполненны корректно!");
            }
            Drowing();
        }

       
    }
}
