using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        TRPOPREntities1 db = new TRPOPREntities1();
        public MainWindow()
        {
            InitializeComponent();
            ClearDB();
            OutPutList();
        }

        private void AddVector_Click(object sender, RoutedEventArgs e)
        {

            segments NewSeg = new segments()
            {
                x1 = Convert.ToInt32(x1.Text),
                x2 = Convert.ToInt32(x2.Text),
                y1 = Convert.ToInt32(y1.Text),
                y2 = Convert.ToInt32(y2.Text),
                Name = ("("+x1.Text + ";"+y1.Text + ")("+x2.Text + ";"+y2.Text + ")")
            };
            db.segments.Add(NewSeg);
            db.SaveChanges();   
            OutPutList();

            x1.Clear(); x2.Clear(); y1.Clear(); y2.Clear();



        }

        private void AddAngle_Click(object sender, RoutedEventArgs e)
        {
            var SegmentF = db.segments.Where(s => s.Name == RelCB1.Text).FirstOrDefault<segments>();
            angles NewAng = new angles()
            {
                segment1 = SegmentF.id,
                segment2 = SegmentF.id
            };
            db.angles.Add(NewAng);
            db.SaveChanges();
            OutPutList();

            
        }

        private void AddRelation_Click(object sender, RoutedEventArgs e)
        {
            var SegmentF = db.segments.Where(s => s.Name == RelCB1.Text).First<segments>();
            Relations NewRel = new Relations()
            {
                relation1 = Convert.ToInt32(RelTB_1.Text),
                relation2 = Convert.ToInt32(RelTB_2.Text),
                segment_ID = Convert.ToInt32(SegmentF.id)
            };
            db.Relations.Add(NewRel);
            db.SaveChanges();
            OutPutList();

            RelTB_1.Clear(); RelTB_2.Clear();
        }

        private void OutPutList()
        {
            vectorsList.ItemsSource = db.segments.Select(s => s.Name).ToList();
            RelationList.ItemsSource = db.Relations.Select(s => db.segments.Where(w => w.id == s.segment_ID).FirstOrDefault().Name
            + " : " + s.relation1 + ":" + s.relation2).ToList();
            AngleList.ItemsSource = db.angles.Select(s => db.segments.Where(w => w.id == s.segment1).FirstOrDefault().Name + " : " + db.segments.Where(w => w.id == s.segment2).FirstOrDefault().Name).ToList();
            RelCB1.ItemsSource = db.segments.Select(s => s.Name).ToList();
            AngCB1.ItemsSource = db.segments.Select(s => s.Name).ToList();
            AngCB2.ItemsSource = db.segments.Select(s => s.Name).ToList();

        }

        private void ClearDB()
        {
            db.Database.ExecuteSqlCommand("TRUNCATE TABLE [Relations]");
            db.Database.ExecuteSqlCommand("TRUNCATE TABLE [angles]");
            db.Database.ExecuteSqlCommand("delete [segments]");
            db.Database.ExecuteSqlCommand("DBCC CHECKIDENT('segments', RESEED, 0)");
        }

        private void ClearDate_Click(object sender, RoutedEventArgs e)
        {
            ClearDB();
            OutPutList();
        }

        private void PlotGraph_Click(object sender, RoutedEventArgs e)
        {

            graphWindow gw = new graphWindow();
            gw.Show();

        }
    }

}
