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
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace Kursach.Windows
{
    /// <summary>
    /// Логика взаимодействия для DriverWindow.xaml
    /// </summary>
    public partial class DriverWindow : Window
    {
        public MainWindow mainWindow { get; set; }
        class Driver
        {
            public string ID { get; set; }
            public string FIO { get; set; }
            public string Car { get; set; }
            public string CarPlate { get; set; }
            public string Licence { get; set; }

            public Driver(string id, string fio, string car, string carplate, string licence)
            {
                ID = id; FIO = fio; Car = car; CarPlate = carplate; Licence = licence;
            }
            public Driver(string id)
            {
                ID = id;
            }
        }
        public int DriverID {get;set;}

        public SqlConnection connect = new SqlConnection();
        SqlCommand cmmnd = new SqlCommand();
        SqlDataReader Reader;
        Driver driver;
        SqlDataAdapter adapter;
        DataTable DT;
        

        public DriverWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mainWindow.Visibility = Visibility.Collapsed;
            string CmndLine = "select adress1, adress2, adress3, C_Full_name, C_Phone_number," +
               " Datatime, Distance, summary,  PaymentOperator, Servise_name, " +
             "Servise_surcharge from Rides r JOIN Driver d ON r.DriverID = d.Driver_ID JOIN Client c ON r.ClientID = c.Client_ID join Payment p on " +
             "r.PaymentOperator = p.Pay_operator join ExtraServises e on r.ExServise_ID = e.servise_ID where Driver_ID = " + DriverID.ToString();
            cmmnd = new SqlCommand(CmndLine, connect);
            SqlDataReader reader = cmmnd.ExecuteReader();
            DT = new DataTable();
            DT.Load(reader);
            DDataGrid.ItemsSource = DT.DefaultView;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            mainWindow.Visibility = Visibility.Visible;
        }

        private void Btn_back_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_cancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Find_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_History_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Report_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
