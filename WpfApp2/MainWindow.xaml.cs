﻿using System;
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
        public TRPOPREntities1 db = new TRPOPREntities1();
        public MainWindow()
        {
            InitializeComponent();
            ClearDB();



        }
        //отроботка кнопки добавление векторов
        private void AddVector_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddVector(Convert.ToInt32((x1.Text)), Convert.ToInt32(x2.Text), Convert.ToInt32(y1.Text), Convert.ToInt32(y2.Text));
                OutPutList();

                x1.Clear(); x2.Clear(); y1.Clear(); y2.Clear();
            }

            
            catch
            {
                MessageBox.Show("Заполните поля ввода!!!");
            }


        }
        //Добавление вектора
        public void AddVector(int x1,int x2,int y1, int y2)
        {
          
                segments NewSeg = new segments()
                {
                    x1 = Convert.ToInt32(x1),
                    x2 = Convert.ToInt32(x2),
                    y1 = Convert.ToInt32(y1),
                    y2 = Convert.ToInt32(y2),
                    Name = ("(" + x1 + ";" + y1 + ")(" + x2 + ";" + y2 + ")")
                };
                db.segments.Add(NewSeg);
                db.SaveChanges();
      
         
            
        }
        //отроботка кнопки добавления угла
        private void AddAngle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddAngle(AngCB1.Text, AngCB2.Text);
                OutPutList();
            }
            catch
            {
                MessageBox.Show("Заполните поля ввода!!!");
            }

            
        }
        //добаление угла
        public void AddAngle(string AngCB1, string AngCB2)
        {
            var Segment1 = db.segments.Where(s => s.Name == AngCB1).First<segments>();
            var Segment2 = db.segments.Where(s => s.Name == AngCB2).First<segments>();
            angles NewAng = new angles()
            {
                segment1 = Segment1.id,
                segment2 = Segment2.id
            };
            db.angles.Add(NewAng);
            db.SaveChanges();
           
        }
        //отроботка кнопки добавления соотношения
        private void AddRelation_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                AddRelation(RelCB1.Text, Convert.ToInt32(RelTB_1.Text), Convert.ToInt32(RelTB_2.Text));
                OutPutList();

                RelTB_1.Clear(); RelTB_2.Clear();
            }
            catch
            {
                MessageBox.Show("Заполните поля ввода!!!");
            }
        }
        //добавление соотношения
        public void AddRelation(string RelCB1, int RelTB_1, int RelTB_2)
        {
            var SegmentF = db.segments.Where(s => s.Name == RelCB1).First<segments>();
            Relations NewRel = new Relations()
            {
                relation1 = Convert.ToInt32(RelTB_1),
                relation2 = Convert.ToInt32(RelTB_2),
                segment_ID = Convert.ToInt32(SegmentF.id)
            };
            db.Relations.Add(NewRel);
            db.SaveChanges();
        }
        //Обновление листов с данными
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
        //удаление всех данных из бд перед работой
        public void ClearDB()
        {
            try { db.Database.ExecuteSqlCommand("TRUNCATE TABLE [Relations]"); }
            catch
            {
                
            }
            try
            {
                db.Database.ExecuteSqlCommand("TRUNCATE TABLE [angles]");
            }
            catch
            {
                
            }
            try
            {
                db.Database.ExecuteSqlCommand("delete [segments]");
                db.Database.ExecuteSqlCommand("DBCC CHECKIDENT('segments', RESEED, 0)");
            }
            catch
            {
               
            }
            
           
        }


        //переход на страницу с графиками
        private void PlotGraph_Click(object sender, RoutedEventArgs e)
        {

            graphWindow gw = new graphWindow();
            gw.Show();

        }
    }

}
