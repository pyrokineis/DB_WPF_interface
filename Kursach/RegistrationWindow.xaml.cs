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
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
           
        }
        private void Client_Register_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (Client_Grid.Visibility == Visibility.Visible)
                {
                    if (ClientFIO_TB.Text != null & ClientPhone_TB.Text.Length == 11 & ClientAge_TB.Text != null)
                    {
                        try
                        {
                            int.Parse(ClientAge_TB.Text);
                            int.Parse(ClientPhone_TB.Text);
                        }

                        catch
                        {
                            MessageBox.Show("Укажите корректные данные");
                            ClientAge_TB.Text = null;
                            ClientPhone_TB.Text = null;
                            return;
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

                        MessageBox.Show("Успешная регистрация");
                        Close();

                    }
                    else MessageBox.Show("Проверьте корректность данных");
                }
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Driver_Register_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Driver_Grid.Visibility == Visibility.Visible)
                {
                    if (DriverFIO_TB.Text != null & DriverPhone_TB.Text.Length == 11 & DriverAuto_TB.Text!=null & CarPlate_TB.Text.Length==6 & CarLicense_TB.Text.Length==12 )
                    {
                        try
                        {
                           // int.Parse(DriverPhone_TB.Text);
                        }

                        catch
                        {
                            MessageBox.Show("Укажите корректные данные");
                            DriverPhone_TB.Text = null;
                            return;
                        }

                        cmmnd = new SqlCommand("insert into Driver (D_Full_name, Auto_model, Auto_plate, D_Phone_number,Licence_number) values " +
                            "(@D_Full_name, @Auto_model, @Auto_plate, @D_Phone_number, @Licence_number)", connect);
                        SqlParameter param = new SqlParameter();
                        param.ParameterName = "@D_Full_name";
                        param.Value = DriverFIO_TB.Text;
                        cmmnd.Parameters.Add(param);

                        param = new SqlParameter();
                        param.ParameterName = "@Auto_model";
                        param.Value = DriverAuto_TB.Text;
                        cmmnd.Parameters.Add(param);

                        param = new SqlParameter();
                        param.ParameterName = "@Auto_plate";
                        param.Value = CarPlate_TB.Text;
                        cmmnd.Parameters.Add(param);

                        param = new SqlParameter();
                        param.ParameterName = "@D_Phone_number";
                        param.Value = DriverPhone_TB.Text;
                        cmmnd.Parameters.Add(param);

                        param = new SqlParameter();
                        param.ParameterName = "@Licence_number";
                        param.Value = CarLicense_TB.Text;
                        cmmnd.Parameters.Add(param);
                        cmmnd.ExecuteNonQuery();

                        MessageBox.Show("Успешная регистрация");
                        Close();

                    }
                    else MessageBox.Show("Проверьте корректность данных");
                }

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Driver_Back_Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

       
    }
}

