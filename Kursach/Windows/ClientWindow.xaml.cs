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
    /// Логика взаимодействия для ClientWindow.xaml
    /// </summary>
    public partial class ClientWindow : Window
    {
        class Client
        {
            public string ID { get; }
            public string FIO { get; }
            public string Age { get; }
            public string Number { get; }

            public Client(string id, string fio, string age, string number)
            {
                ID = id;
                FIO = fio;
                Age = age;
                Number = number;
            }
            public Client(string id)
            {
                ID = id;
            }
        }
        public MainWindow mainWindow { get; set; }
        public int ClientID { get; set; }
        public SqlConnection connect { get; set; }
        SqlCommand cmnd, cmndFind;
        SqlDataReader reader;
        Client client;
        DataTable DT;
        int rDriver, driverCount, DriverID, serviseID,classID;
        string PayOperator,payType;

        List<string> CtTableStrings = new List<string> { "Client_ID", "C_Full_name", "C_Age", "C_Phone_Number" };
        List<string> DriversIDs = new List<string> { };
        List<string> PayTypeString = new List<string> { };
        List<string> PayOpsString = new List<string> { };
        List<string> AutoClassString = new List<string> { };
        List<string> ExtraServString = new List<string> { };
        public ClientWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mainWindow.Visibility = Visibility.Collapsed;
            //сдеать представленние
            string CmndLine = "Select * from Client";
            cmnd = new SqlCommand(CmndLine, connect);
            SqlDataReader reader = cmnd.ExecuteReader();
            DT = new DataTable();
            DT.Load(reader);
            CDataGrid.ItemsSource = DT.DefaultView;
            Column_Selection_CB.ItemsSource = CtTableStrings;
        }
        private void LoadClientInfo(int id)
        {
            cmnd.CommandText = "select * from Client where Client_ID =" + id;
            reader = cmnd.ExecuteReader();
            if (reader.Read())
            {
                client = new Client(
                    (reader[0].ToString()),
                    reader[1].ToString(),
                    reader[2].ToString(),
                    reader[3].ToString()
                    );
            }

            else
            {
                MessageBox.Show("нет таких");
                Close();

            }
            reader.Close();


        }

        private void Window_Closed(object sender, EventArgs e)
        {
            mainWindow.Visibility = Visibility.Visible;
        }

        private void Btn_Serve_Click(object sender, RoutedEventArgs e)
        {
            OrderGrid.Visibility = Visibility.Visible;
            TableGrid.Visibility = Visibility.Hidden;
            top_SP.Visibility = Visibility.Hidden;
            top_SP2.Visibility = Visibility.Hidden;

            cmnd = new SqlCommand("select Driver_ID from Driver",connect);
            reader = cmnd.ExecuteReader();
            while (reader.Read())
            {
                DriversIDs.Add(reader.GetValue(0).ToString());
            }
            reader.Close();
            cmnd = new SqlCommand("select Pay_Type from Payment", connect);
            reader = cmnd.ExecuteReader();
            while (reader.Read())
            {
                PayTypeString.Add(reader.GetValue(0).ToString());
            }

            reader.Close();

            foreach (string i in PayTypeString)
            {
                if (i == "None")
                    PayTypeString[PayTypeString.IndexOf(i)] = "Безналичный";

            }

            cmnd = new SqlCommand("select Class_Name from AutoClass", connect);
            reader = cmnd.ExecuteReader();
            while (reader.Read())
            {
                AutoClassString.Add(reader.GetValue(0).ToString());
            }
            reader.Close();

            cmnd = new SqlCommand("select Servise_Name from ExtraServises", connect);
            reader = cmnd.ExecuteReader();
            while (reader.Read())
            {
                ExtraServString.Add(reader.GetValue(0).ToString());
            }
            reader.Close();

            AutoClass_TB.ItemsSource = AutoClassString;
            PayType_CB.ItemsSource = PayTypeString;
            ExtraServ_CB.ItemsSource = PayTypeString;
        }

        private void Btn_History_Click(object sender, RoutedEventArgs e)
        {
            TableGrid.Visibility = Visibility.Visible;
            top_SP.Visibility = Visibility.Visible;
            top_SP2.Visibility = Visibility.Visible;
        }
        private void Btn_Find_Click(object sender, RoutedEventArgs e) //dodelat
        {
        


            switch (Column_Selection_CB.SelectedItem.ToString())
            {
                            case "Client_ID":
                                cmndFind = new SqlCommand($"select * from Client where Client_ID like '%{TB_Search.Text.ToString()}%'");
                                break;
                            case "C_Full_name":
                                cmndFind = new SqlCommand($"select * from Client where C_Full_name like '%{TB_Search.Text.ToString()}%'");
                                break;
                            case "C_Age":
                                cmndFind = new SqlCommand($"select * from Client where C_Age like '%{TB_Search.Text.ToString()}%'");
                                break;
                            case "C_Phone_Number":
                                cmndFind = new SqlCommand($"select * from Client where C_Phone_number like '%{TB_Search.Text.ToString()}%'");
                                break;
            }
               
                    
                
           if (TB_Search.Text.Length > 0)
           {
                cmndFind.Connection = connect;
                SqlDataReader reader = cmndFind.ExecuteReader();
                DataTable FDT = new DataTable();
                FDT.Load(reader);
                CDataGrid.ItemsSource = FDT.DefaultView;

           }
         
            
        }

        private void Btn_cancel_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Btn_Report_Click(object sender, RoutedEventArgs e)
        {

        }

        private void PlaceOrder_Click(object sender, RoutedEventArgs e)
        {
            //адр123, дист, сумма, дата, айдди вод, айди кл, оп опл, класс авто, доп усл

            //обн цены
            if (OrderGrid.Visibility == Visibility.Visible)
            {
                driverCount = DriversIDs.Count;
                Random r = new Random();
                DriverID = r.Next(driverCount);

              

                if (Adress1_TB.Text != null & Adress3_TB.Text != null & PayType_CB.SelectedItem != null & AutoClass_TB.SelectedItem != null)
                {

                    if (PayType_CB.SelectedItem.ToString() == "Безналичный")
                        payType = "None";

                    cmnd = new SqlCommand("select Class_ID from AutoClass where Class_Name="+"'"+AutoClass_TB.SelectedItem.ToString()+"'",connect);
                    reader = cmnd.ExecuteReader();
                    while (reader.Read())
                    {
                        classID = int.Parse(reader.GetValue(0).ToString());
                    }
                    reader.Close();

                    cmnd = new SqlCommand("select Servise_ID from ExtraServises where Servise_Name="+ "'"+ExtraServ_CB.SelectedItem.ToString()+"'",connect);
                    reader = cmnd.ExecuteReader();
                    while(reader.Read())
                    {
                        serviseID = int.Parse(reader.GetValue(0).ToString());
                    }
                    reader.Close();
                  
                    try
                    {
                        cmnd = new SqlCommand("Insert into Rides (Rides_ID ,Adress1 ,Adress2 ,Adress3 ,Distance ,Summary ,DataTime ,DriverID ,ClientID ,PaymentOperator,autoClass_ID ,ExServise_ID) " +
                    "values (@Rides_ID, @Adress1, @Adress2, @Adress3, @Distance, @Summary, @DataTime, @DriverID, @ClientID, @PaymentOperator, @autoClass_ID, @ExServise_ID)", connect);

                        //получчть айдишник
                        //SqlParameter param = new SqlParameter();
                        //param.ParameterName = "";
                        //param.Value = ;
                        //cmmnd.Parameters.Add(param);

                        //SqlParameter param = new SqlParameter();
                        //param.ParameterName = "";
                        //param.Value = ;
                        //cmmnd.Parameters.Add(param);
                        //SqlParameter param = new SqlParameter();
                        //param.ParameterName = "";
                        //param.Value = ;
                        //cmmnd.Parameters.Add(param);
                        //SqlParameter param = new SqlParameter();
                        //param.ParameterName = "";
                        //param.Value = ;
                        //cmmnd.Parameters.Add(param);
                        //SqlParameter param = new SqlParameter();
                        //param.ParameterName = "";
                        //param.Value = ;
                        //cmmnd.Parameters.Add(param);
                        //SqlParameter param = new SqlParameter();
                        //param.ParameterName = "";
                        //param.Value = ;
                        //cmmnd.Parameters.Add(param);
                        //SqlParameter param = new SqlParameter();
                        //param.ParameterName = "";
                        //param.Value = ;
                        //cmmnd.Parameters.Add(param);
                        //SqlParameter param = new SqlParameter();
                        //param.ParameterName = "";
                        //param.Value = ;
                        //cmmnd.Parameters.Add(param);
                    }
                    catch
                    {

                    }
                }
            }
        }

        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
