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
            string ResAngleList = "";


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
                

                


                //    double length = Math.Sqrt(Math.Pow((Math.Abs(seg.x1 - seg.x2)), 2) + Math.Pow((Math.Abs(seg.y1 - seg.y2)), 2));
                //int sumRel = rel.relation1 + rel.relation2;
                //double LeftRel = length / sumRel * rel.relation1;
                //double xRel =  (Math.Abs(seg.x1 - seg.x2)) / sumRel * rel.relation1;
                //double yRel =  Math.Sqrt(Math.Pow(LeftRel, 2) - Math.Pow(xRel, 2));
                double relation = Convert.ToDouble(Convert.ToDouble(rel.relation1) / Convert.ToDouble(rel.relation2));
                double XM = Convert.ToDouble(Convert.ToDouble(seg.x1) + relation * Convert.ToDouble(seg.x2))/(1 + relation);
                double YM = Convert.ToDouble(Convert.ToDouble(seg.y1) + relation * Convert.ToDouble(seg.y2)) / (1 + relation);

                

                chart.Series["Points"].ChartArea = "Default";
                chart.Series["Points"].ChartType = SeriesChartType.Point;
                XData.Add(XM);
                YData.Add(YM);
                chart.Series["Points"].Points.DataBindXY(XData, YData);
            }



            foreach(var ang in db.angles)
            {

                var seg1 = db.segments.Where(w => w.id == ang.segment1).FirstOrDefault<segments>();
                var seg2 = db.segments.Where(w => w.id == ang.segment2).FirstOrDefault<segments>();
                if (intersection(seg1, seg2))
                {
                    ResAngleList = ResAngleList + "(" + seg1.x1 + ";" + seg1.y1 + ")" + "(" + seg1.x2 + ";" + seg1.y2 + ")" + "\u2220" + "(" + seg2.x1 + ";" + seg2.y1 + ")" + "(" + seg2.x2 + ";" + seg2.y2 + ")"
                        + " = " + (VectorAng(seg1, seg2) * 180 / Math.PI);
                    AngleList.Text = ResAngleList;
                }
                else
                {
                    ResAngleList = ResAngleList + "(" + seg1.x1 + ";" + seg1.y1 + ")" + "(" + seg1.x2 + ";" + seg1.y2 + ")" + "\u2220" + "(" + seg2.x1 + ";" + seg2.y1 + ")" + "(" + seg2.x2 + ";" + seg2.y2 + ")"
                        + " не пересекаются " ;
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
        public double[] FindEquation(segments seg)
        {
            double[] k = new double[3];
            k[0] = seg.y1 - seg.y2;
            k[1] = seg.x2 - seg.x1;
            k[2] = (seg.x1 * seg.y2) - (seg.x2 * seg.y1);
            return k;
        }

        public bool intersection(segments seg1, segments seg2)
        {
            //Шаг 2.Если x1 ≥ x2 то меняем между собой значения x1 и x2 и y1 и y2
            //Шаг 3.Если x3 ≥ x4 то меняем между собой значения x3 и x4 и y3 и y4;
            double x1, x2, x3, x4, y1, y2, y3, y4, k1, k2;
            if(seg1.x2 <= seg1.x1)
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
            /*Шаг 4.Проверяем, равны ли между собой у2 и у1,
            если у2 = у1 да, то принимаем k1 = 0 иначе
            Определяем угловой коэффициент в уравнении прямой:
            k1 = (у2 - у1) / (x2 - x1) */
            if (y2 == y1) k1 = 0;
            else
            {
                k1 = (y2 - y1) / (x2 - x1);
            }
            /*Шаг 5.Проверяем, равны ли между собой у3 и у4,
            если у3 = у4 да, то принимаем k2 = 0 иначе
            Определяем угловой коэффициент в уравнении прямой:
            k2 = (у4 - у3) / (x4 - x3)*/
            if (y3 == y4) k2 = 0;
            else
            {
                k2 = (y4 - y3) / (x4 - x3);
            }

            /*Шаг 6. Проверим отрезки на параллельность.
            Если k1 = k2 , то прямые параллельны и отрезки пересекаться не могут. Решение задачи прекращаем.*/
            if (k1 == k2) return false;
            /*
            Шаг 7. Вычисляем значения свободных переменных
            Определяем свободные члены в уравнении прямой:
            */
            else
            {
                return true;
            }
            

        }
    }
}
