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
    /// Логика взаимодействия для RegistrationTypeWindow.xaml
    /// </summary>
    public partial class RegistrationTypeWindow : Window
    {
        public MainWindow mainWindow { get; set; }
        public RegistrationTypeWindow()
        {
            InitializeComponent();
        }
        public SqlConnection connect { get; set; }


        private void RTW_Back_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RB_Client_Checked(object sender, RoutedEventArgs e)
        {
            RegistrationWindow RgW = new RegistrationWindow();
            RgW.connect = connect;
            RgW.Driver_Grid.Visibility = Visibility.Hidden;
            RgW.Client_Grid.Visibility = Visibility.Visible;
            RgW.Name = "ClientRegisrationWindow";
            RgW.ShowDialog();
        }

        private void RB_Driver_Checked(object sender, RoutedEventArgs e)
        {
            RegistrationWindow RgW = new RegistrationWindow();
            RgW.connect = connect;
            RgW.Client_Grid.Visibility = Visibility.Hidden;
            RgW.Driver_Grid.Visibility = Visibility.Visible;
            RgW.Name = "DriverRegisrationWindow";
            RgW.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
         
        }
    }
}
