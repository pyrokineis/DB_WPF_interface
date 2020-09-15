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
        public SqlConnection connect = new SqlConnection();
        SqlCommand cmnd, cmndFind;
        SqlDataReader Reader;
        Client client;
        SqlDataAdapter adapter;
        DataTable DT;

        List<string> CtTableStrings = new List<string> { "Client_ID", "C_Full_name", "C_Age", "C_Phone_Number" };
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
        }
        private void LoadClientInfo(int id)
        {
            cmnd.CommandText = "select * from Client where Client_ID =" + id;
            Reader = cmnd.ExecuteReader();
            if (Reader.Read())
            {
                client = new Client(
                    (Reader[0].ToString()),
                    Reader[1].ToString(),
                    Reader[2].ToString(),
                    Reader[3].ToString()
                    );
            }

            else
            {
                MessageBox.Show("нет таких");
                Close();

            }
            Reader.Close();


        }

        private void Window_Closed(object sender, EventArgs e)
        {
            mainWindow.Visibility = Visibility.Visible;
        }

        private void Btn_Serve_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Btn_History_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Btn_Find_Click(object sender, RoutedEventArgs e)
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
        private void Btn_hmm_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Btn_Back_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
