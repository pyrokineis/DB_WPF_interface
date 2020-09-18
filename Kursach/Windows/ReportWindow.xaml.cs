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
        List<string> infoList = new List<string>();
     
        public int Sum { get { return summary; } set { this.summary = value; } }
        public double DistanceSum { get { return dist; } set { this.dist = value; } }
        public int RidesCount { get { return Ridescount; } set { this.Ridescount = value; } }
        public List<string> InfoList { get { return infoList; } set { infoList = value; } }
        public ReportWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        List<string> list = new List<string>();

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            list.AddRange(infoList);
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
