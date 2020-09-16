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
using Kursach.Windows;

namespace Kursach
{
    /// <summary>
    /// Логика взаимодействия для RegistrationWindow.xaml
    /// </summary>
    public partial class RegistrationWindow : Window
    {

        public SqlConnection connect { get; set; }
        public MainWindow mainWindow { get; set; }

        SqlCommand cmmnd = new SqlCommand();

        public RegistrationWindow()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
               // connect.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Client_Register_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (Client_Grid.Visibility == Visibility.Visible)
                {
                    if (ClientFIO_TB.Text != null & ClientPhone_TB.Text.Length == 11 & ClientAge_TB.Text!=null)
                    {
                        try
                        {
                            int age = int.Parse(ClientAge_TB.Text);
                        }

                        catch
                        {
                            MessageBox.Show("Введите целое число");
                            ClientAge_TB.Text = null;
                        }

                        cmmnd = new SqlCommand("insert into Client (C_Full_name, C_Age, C_Phone_number) values " +
                            "(@C_Full_name, @C_Age, @C_Phone_number)", connect);
                        SqlParameter param = new SqlParameter();
                        param.ParameterName = "@C_Full_name";
                        param.Value = ClientFIO_TB.Text;
                        cmmnd.Parameters.Add(param);
                        param = new SqlParameter();
                        param.ParameterName = "@C_Age";
                        param.Value = ClientAge_TB.Text;
                        cmmnd.Parameters.Add(param);
                        param = new SqlParameter();
                        param.ParameterName = "@C_Phone_number";
                        param.Value = ClientPhone_TB.Text;
                        cmmnd.Parameters.Add(param);
                        cmmnd.ExecuteNonQuery();
                  
                    }
                }


                else if (Driver_Grid.Visibility == Visibility.Visible)
                {

                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Driver_Register_Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}

