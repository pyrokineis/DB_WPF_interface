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
        SqlCommand cmnd = new SqlCommand();
        SqlDataReader reader;
        Driver driver;
        DataTable DT;
        List<string> DrTableStrings = new List<string> { "Driver_ID", "D_Full_name", "Auto_model", "Auto_plate", "D_Phone_Number", "Licence_number" };

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
            cmnd = new SqlCommand(CmndLine, connect);
            SqlDataReader reader = cmnd.ExecuteReader();
            DT = new DataTable();
            DT.Load(reader);
            DDataGrid.ItemsSource = DT.DefaultView;
            Column_Selection_CB.ItemsSource = DrTableStrings;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            mainWindow.Visibility = Visibility.Visible;
        }

        private void Btn_back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Btn_cancel_Click(object sender, RoutedEventArgs e)
        {
            string CmndLine = "select adress1, adress2, adress3, C_Full_name, C_Phone_number," +
             " Datatime, Distance, summary,  PaymentOperator, Servise_name, " +
           "Servise_surcharge from Rides r JOIN Driver d ON r.DriverID = d.Driver_ID JOIN Client c ON r.ClientID = c.Client_ID join Payment p on " +
           "r.PaymentOperator = p.Pay_operator join ExtraServises e on r.ExServise_ID = e.servise_ID where Driver_ID = " + DriverID.ToString();
            cmnd = new SqlCommand(CmndLine, connect);
            SqlDataReader reader = cmnd.ExecuteReader();
            DT = new DataTable();
            DT.Load(reader);
            DDataGrid.ItemsSource = DT.DefaultView;
        }

        private void Btn_Find_Click(object sender, RoutedEventArgs e)  ///yyyyyyy blya
        {
            if (Column_Selection_CB.SelectedItem != null)
            {

                if (TB_Search.Text.Length > 0)
                {
                    switch (Column_Selection_CB.SelectedItem.ToString())
                    {
                        case "Driver_ID":
                            cmnd = new SqlCommand($"select * from Client where Client_ID like '%{TB_Search.Text.ToString()}%'");
                            break;
                        case "D_Full_name":
                            cmnd = new SqlCommand($"select * from Client where C_Full_name like '%{TB_Search.Text.ToString()}%'");
                            break;
                        case "C_":
                            cmnd = new SqlCommand($"select * from Client where C_Age like '%{TB_Search.Text.ToString()}%'");
                            break;
                        case "D_Phone_Number":
                            cmnd = new SqlCommand($"select * from Client where C_Phone_number like '%{TB_Search.Text.ToString()}%'");
                            break;
                    }
                    cmnd.Connection = connect;
                    SqlDataReader reader = cmnd.ExecuteReader();
                    DataTable FDT = new DataTable();
                    FDT.Load(reader);
                    DDataGrid.ItemsSource = FDT.DefaultView;

                }
            }

        }

        private void Btn_History_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Report_Click(object sender, RoutedEventArgs e)
        {
            ReportWindow RW = new ReportWindow();

            cmnd = new SqlCommand("select count(Rides_ID) from Rides where DriverID=@DriverID", connect);
            SqlParameter param = new SqlParameter();
            param.ParameterName = "@DriverID";
            param.Value = DriverID;
            cmnd.Parameters.Add(param);
            reader = cmnd.ExecuteReader();
            while (reader.Read())
            {
                object num = reader.GetValue(0);
                RW.Sum = int.Parse(num.ToString());

            }
            reader.Close();

            cmnd = new SqlCommand("select sum(Distance) from Rides where DriverID=@DriverID", connect);
            param = new SqlParameter();
            param.ParameterName = "@DriverID";
            param.Value = DriverID;
            cmnd.Parameters.Add(param);
            reader = cmnd.ExecuteReader();
            while (reader.Read())
            {
                object num = reader.GetValue(0);
                RW.Sum = int.Parse(num.ToString());

            }
            reader.Close();


            cmnd = new SqlCommand("select sum(Summary) from Rides where DriverID=@DriverID", connect);
            param = new SqlParameter();
            param.ParameterName = "@DriverID";
            param.Value = DriverID;
            cmnd.Parameters.Add(param);
            reader = cmnd.ExecuteReader();
            while (reader.Read())
            {
                object num = reader.GetValue(0);
                RW.Sum = int.Parse(num.ToString());
            }
            reader.Close();

            RW.ShowDialog();

        }
    }
}
