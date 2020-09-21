using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace Kursach.Windows
{
    /// <summary>
    /// Логика взаимодействия для DriverWindow.xaml
    /// </summary>
    public partial class DriverWindow : Window
    {
        public MainWindow mainWindow { get; set; }
        public int DriverID {get;set;}

        public SqlConnection connect = new SqlConnection();
        SqlCommand cmnd, cmndDrop,cmndFind;
        SqlDataReader reader;
        Driver driver;
        DataTable DT;
        List<string> DrTableStrings = new List<string> { "Adress1", "Adress2", "Adress3", "C_Full_name", "C_Phone_number", " Datatime", "Distance", "summary",  "PaymentOperator", "Servise_name", "Servise_surcharge" };

        public DriverWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mainWindow.Visibility = Visibility.Collapsed;
            SqlCommand DriverView =new SqlCommand("create view DriverView as select adress1, adress2, adress3, C_Full_name, C_Phone_number," +
               " Datatime, Distance, summary,  PaymentOperator, Servise_name, " +
             "Servise_surcharge from Rides r JOIN Driver d ON r.DriverID = d.Driver_ID JOIN Client c ON r.ClientID = c.Client_ID join Payment p on " +
             "r.PaymentOperator = p.Pay_operator join ExtraServises e on r.ExServise_ID = e.servise_ID where Driver_ID = " + DriverID.ToString(),connect);

           
            cmndDrop = new SqlCommand("drop view DriverView",connect);
            cmndDrop.ExecuteNonQuery();

            DriverView.ExecuteNonQuery();

            string cmndLine = "select * from DriverView";
            cmnd =new SqlCommand(cmndLine, connect);

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
            string CmndLine = "select * from DriverView";
            cmnd = new SqlCommand(CmndLine, connect);
            SqlDataReader reader = cmnd.ExecuteReader();
            DT = new DataTable();
            DT.Load(reader);
            DDataGrid.ItemsSource = DT.DefaultView;
        }

        private void Btn_Find_Click(object sender, RoutedEventArgs e) 
        {
            if (Column_Selection_CB.SelectedItem != null)
            {

                if (TB_Search.Text.Length > 0)
                {
                    switch (Column_Selection_CB.SelectedItem.ToString())
                    {
                        case "Adress1":
                            cmndFind = new SqlCommand($"select * from DriverView where Adress1 like '%{TB_Search.Text.ToString()}%'");
                            break;

                        case "Addres2":
                            cmndFind = new SqlCommand($"select * from DriverView where Adress2 like '%{TB_Search.Text.ToString()}%'");
                            break;

                        case "Adress3":
                            cmndFind = new SqlCommand($"select * from DriverView where Adress3 like '%{TB_Search.Text.ToString()}%'");
                            break;

                        case "Distance":
                            cmndFind = new SqlCommand($"select * from DriverView where Distance like '%{TB_Search.Text.ToString()}%'");
                            break;

                        case "Summary":
                            cmndFind = new SqlCommand($"select * from DriverView where Summary like '%{TB_Search.Text.ToString()}%'");
                            break;

                        case "DataTime":
                            cmndFind = new SqlCommand($"select * from DriverView where DataTime like '%{TB_Search.Text.ToString()}%'");
                            break;

                        case "C_Full_name":
                            cmndFind = new SqlCommand($"select * from DriverView where D_Full_name like '%{TB_Search.Text.ToString()}%'");
                            break;

                        case "C_Phone_number":
                            cmndFind = new SqlCommand($"select * from DriverView where D_Phone_number like '%{TB_Search.Text.ToString()}%'");
                            break;

                        case "PaymentOperator":
                            cmndFind = new SqlCommand($"select * from DriverView where PaymentOperator like '%{TB_Search.Text.ToString()}%'");
                            break;

                        case "Servise_name":
                            cmndFind = new SqlCommand($"select * from DriverView where Servise_name like '%{TB_Search.Text.ToString()}%' ");
                            break;

                        case "Servise_surcharge":
                            cmndFind = new SqlCommand($"select * from DriverView where Servise_surcharge like '%{TB_Search.Text.ToString()}%' ");
                            break;
                    }
                    cmndFind.Connection = connect;
                    SqlDataReader reader = cmndFind.ExecuteReader();
                    DataTable FDT = new DataTable();
                    FDT.Load(reader);
                    DDataGrid.ItemsSource = FDT.DefaultView;

                }
            }

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
                RW.RidesCount = int.Parse(num.ToString());

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
                RW.DistanceSum = double.Parse(num.ToString());

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

            RW.Show();

        }
    }
}
