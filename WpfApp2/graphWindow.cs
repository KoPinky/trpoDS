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
        }

        TRPOPREntities1 db = new TRPOPREntities1();

        private void chart1_Click(object sender, EventArgs e)
        {
            chart.ChartAreas.Add(new ChartArea("Default"));
            chart.ChartAreas["Default"].AxisX.MajorGrid.Interval = 1;
            chart.ChartAreas["Default"].AxisX.Minimum = 0;
            chart.ChartAreas["Default"].AxisY.MajorGrid.Interval = 1;
            chart.ChartAreas["Default"].AxisY.Minimum = 0;



            foreach (var segment in db.segments)
            {
                chart.Series.Add(new Series(segment.Name));
                chart.Series[segment.Name].ChartArea = "Default";
                chart.Series[segment.Name].ChartType = SeriesChartType.Line;

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
                double x, y;

                


                //    double length = Math.Sqrt(Math.Pow((Math.Abs(seg.x1 - seg.x2)), 2) + Math.Pow((Math.Abs(seg.y1 - seg.y2)), 2));
                //int sumRel = rel.relation1 + rel.relation2;
                //double LeftRel = length / sumRel * rel.relation1;
                //double xRel =  (Math.Abs(seg.x1 - seg.x2)) / sumRel * rel.relation1;
                //double yRel =  Math.Sqrt(Math.Pow(LeftRel, 2) - Math.Pow(xRel, 2));
                double relation = Convert.ToDouble(Convert.ToDouble(rel.relation1) / Convert.ToDouble(rel.relation2));
                double XM = Convert.ToDouble(Convert.ToDouble(seg.x1) + relation * Convert.ToDouble(seg.x2))/(1 + relation);
                double YM = Convert.ToDouble(Convert.ToDouble(seg.y1) + relation * Convert.ToDouble(seg.y2)) / (1 + relation);

                MessageBox.Show("Rel = " + relation + "xM = " + Convert.ToString(XM)+ "\nyM = " + Convert.ToString(YM));

                chart.Series["Points"].ChartArea = "Default";
                chart.Series["Points"].ChartType = SeriesChartType.Point;
                XData.Add(XM);
                YData.Add(YM);
                chart.Series["Points"].Points.DataBindXY(XData, YData);
            }



            foreach(var ang in db.angles)
            {
                //                dx1 = x1 - x2
                //dy1 = y1 - y2
                //dx2 = x3 - x2
                //dy2 = y3 - y2

                //a = dx1 * dy2 - dy1 * dx2
                //b = dx1 * dx2 + dy1 * dy2

                //θ = arctan(a / b)
                //или лучше(при наличии нужной функции)
                //θ = atan2(a, b)
                //Второй вариант дает угол в диапазоне ±π
                var seg1 = db.segments.Where(w => w.id == ang.segment1).FirstOrDefault<segments>();
                var seg2 = db.segments.Where(w => w.id == ang.segment2).FirstOrDefault<segments>();

                double dx1 = seg1.x1 - seg1.x2;
                double dy1 = y1 - y2
                double dx2 = x3 - x2
                double dy2 = y3 - ;
                FindEquation(seg1);
            }
            
            
        }
        public double[] FindEquation(segments seg)
        {
            double[] k = new double[3];
            k[0] = seg.y1 - seg.y2;
            k[1] = seg.x2 - seg.x1;
            k[2] = (seg.x1 * seg.y2) - (seg.x2 * seg.y1);
            return k;
        }
    }
}
