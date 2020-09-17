using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Kursach.Windows
{
    /// <summary>
    /// Логика взаимодействия для ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        int summary, Ridescount;
        double dist;
        string str;
     
        public int Sum { get { return summary; } set { this.summary = value; } }
        public double DistanceSum { get { return dist; } set { this.dist = value; } }
        public int RidesCount { get { return Ridescount; } set { this.Ridescount = value; } }
        public ReportWindow()
        {
            InitializeComponent();
        }
        List<string> list = new List<string>();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            str = "Всего поездок - " + RidesCount.ToString();
            list.Add(str);
            str = "Общая сумма поездок - " + Sum.ToString() + "р.";
            list.Add(str);
            str = "Общее расстояние - " + DistanceSum.ToString() + "км.";
            list.Add(str);
            Report_LB.ItemsSource = list;
 
        }
    }
}
