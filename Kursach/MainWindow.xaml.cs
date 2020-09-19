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
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using Kursach.Windows;

namespace Kursach
{
    public partial class MainWindow : Window
    {
        const string password = "main";
        const string num = "", Lic = "";
        public SqlConnection mainConnect { get; set; }
        SqlCommand cmmnd = new SqlCommand();

        public MainWindow()
        {
            InitializeComponent();

            MessageBox.Show("Чтобы войти как Администратор, введите логин и пароль\n" +
                "Чтобы войти как Клиент, введите номер телефона\n" + "Чтобы войти как Водитель, введите номер прав (с пробелами)");
            WindowStartupLocation = WindowStartupLocation.CenterScreen; 
        }

        private void btnEnter_Click(object sender, RoutedEventArgs e)
        {
            if ( TBoxLog.Text==null)
            {
                MessageBox.Show("Введите данные");
            }
            else if (TBoxLog.Text == "admin" & TBoxPass.Password == password)
            {
                AdminWindow AW = new AdminWindow();
                AW.mainWindow = this;
                AW.connect = mainConnect;
                TBoxPass.Password = TBoxLog.Text = "";
                AW.ShowDialog();
            }
            else try
            {
                    if (TBoxLog.Text.Length == 11)
                    {
                        cmmnd.Connection = mainConnect;
                        cmmnd.CommandText = "select Client_ID from Client where C_Phone_number = " + TBoxLog.Text.ToString();
                        SqlDataReader Reader = cmmnd.ExecuteReader();
                        if (!Reader.Read())
                            throw new Exception();
                        ClientWindow CW = new ClientWindow() { ClientID = int.Parse(Reader[0].ToString() )};
                        CW.mainWindow = this;
                        TBoxLog.Text = TBoxPass.Password = "";
                        CW.connect = mainConnect;
                        Reader.Close();
                        CW.ShowDialog();
                    }

                    else if (TBoxLog.Text.Length == 12)
                    {
                        cmmnd.Connection = mainConnect;
                        cmmnd.CommandText = "select Driver_ID from Driver where Licence_number = " + "'" + TBoxLog.Text.ToString()+"'";
                        SqlDataReader Reader = cmmnd.ExecuteReader();
                        if (!Reader.Read())
                            throw new Exception();
                        DriverWindow DW = new DriverWindow() { DriverID = int.Parse(Reader[0].ToString() )};
                        DW.mainWindow = this;
                        TBoxLog.Text = TBoxPass.Password = "";
                        DW.connect = mainConnect;
                        Reader.Close();
                        DW.ShowDialog();

                    }
                    else throw new Exception();
                }

            catch
            {
                    MessageBox.Show("Проверьте введенные данные");
                    TBoxLog.Text = TBoxPass.Password = "";

            }

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

           
            string dataSource = @"Data Source=49B2\SQLEXPRESS; Initial Catalog = Taxi_agregator_DB; Integrated Security=True";
            mainConnect = new SqlConnection(dataSource);

            try
            {
                mainConnect.Open();
            }
            catch
            {
                MessageBox.Show("Connection FAILURE");
                Close();
            }
        }

        private void btnRegisterClient_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow RgW = new RegistrationWindow();
            RgW.connect = mainConnect;
            RgW.Driver_Grid.Visibility = Visibility.Hidden;
            RgW.Client_Grid.Visibility = Visibility.Visible;
            RgW.Name = "ClientRegisrationWindow";
            RgW.ShowDialog();
        }

        private void btnRegisterDriver_Click(object sender, RoutedEventArgs e)
        {
            RegistrationWindow RgW = new RegistrationWindow();
            RgW.connect = mainConnect;
            RgW.Client_Grid.Visibility = Visibility.Hidden;
            RgW.Driver_Grid.Visibility = Visibility.Visible;
            RgW.Name = "DriverRegisrationWindow";
            RgW.ShowDialog();
        }

      

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            mainConnect.Close();
            Close();
            Application.Current.Shutdown();

        }


    }
}

