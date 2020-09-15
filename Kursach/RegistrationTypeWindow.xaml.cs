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

namespace Kursach
{
    /// <summary>
    /// Логика взаимодействия для RegistrationTypeWindow.xaml
    /// </summary>
    public partial class RegistrationTypeWindow : Window
    {
        public RegistrationTypeWindow()
        {
            InitializeComponent();
        }

      

        private void RTW_Back_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void RB_Client_Checked(object sender, RoutedEventArgs e)
        {
            RegistrationWindow RW = new RegistrationWindow();
            RW.Driver_Grid.Visibility = Visibility.Hidden;
            RW.Client_Grid.Visibility = Visibility.Visible;
            RW.Show();
        }

        private void RB_Driver_Checked(object sender, RoutedEventArgs e)
        {
            RegistrationWindow RW = new RegistrationWindow();
            RW.Client_Grid.Visibility = Visibility.Hidden;
            RW.Driver_Grid.Visibility = Visibility.Visible;
            RW.Show();
        }
    }
}
