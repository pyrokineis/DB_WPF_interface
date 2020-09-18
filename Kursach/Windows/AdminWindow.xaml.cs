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
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public MainWindow mainWindow {get; set;}
        public SqlConnection connect { get; set; }

        SqlCommand  cmnd, cmndFind, cmndReport;
        SqlDataAdapter adapter=new SqlDataAdapter();
        DataTable DT;
        ComboBoxItem CBi;

        string rbText, genCmndLine, drCmndLine, cltCmndLine, payCmndLine, clssCmndLine, servCmndLine;
        List <string> GenTableStrings = new List<string> { "Rides_ID", "Adress1", "Adress2", "Adress3","Distance", "Summary", "DataTime", "DriverID", "ClientID", "PaymentOperator", "autoClass_ID", "ExServise_ID" };
        List<string> CtTableStrings = new List<string> { "Client_ID", "C_Full_name", "C_Age", "C_Phone_Number" };
        List<string> DrTableStrings = new List<string> { "Driver_ID", "D_Full_name", "Auto_model", "Auto_plate", "D_Phone_Number", "Licence_number" };
        List<string> PayTableStrings = new List<string> { "Pay_Type", "Pay_Operator" };
        List<string> ServTableStrings = new List<string> { "Servise_ID", "Servise_Name", "Servise_Surcharge", };
        List<string> ClTableStrings = new List<string> { "Class_ID", "Class_Name", "Coef" };

        public AdminWindow()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

     

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mainWindow.Visibility = Visibility.Collapsed;

            drCmndLine = "select * from Driver";
            cltCmndLine = "select * from Client";
            payCmndLine = "select * from Payment";
            servCmndLine = "select * from ExtraServises";
            clssCmndLine = "select * from AutoClass";
            genCmndLine = "select * from Rides";
            
           
        }

       
        private void Btn_Update_Click(object sender, RoutedEventArgs e)
        {
            DB_Updater();
        }

        private void DB_Updater()
        {
            try 
            {
                adapter.Update(DT);
                ADataGrid.ItemsSource = DT.DefaultView;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public SqlDataAdapter AdapterSettingsSet(ComboBoxItem CBI, SqlConnection connect)
        {
            switch (CBI.Content.ToString())
            {
                case "General Table":
                    {
                        //select
                        adapter.SelectCommand = new SqlCommand("Select * from Rides (Rides_ID ,Adress1 ,Adress2 ,Adress3 ,Distance ,Summary ,DataTime ,DriverID ,ClientID ,PaymentOperator,autoClass_ID ,ExServise_ID) ", connect);
                        //insert
                        cmnd = new SqlCommand("Insert into Rides (Rides_ID ,Adress1 ,Adress2 ,Adress3 ,Distance ,Summary ,DataTime ,DriverID ,ClientID ,PaymentOperator,autoClass_ID ,ExServise_ID) " +
                    "values (@Rides_ID, @Adress1, @Adress2, @Adress3, @Distance, @Summary, @DataTime, @DriverID, @ClientID, @PaymentOperator, @autoClass_ID, @ExServise_ID)", connect);
                        cmnd.Parameters.Add("@Rides_ID", SqlDbType.Int, 6, "Rides_ID");
                        cmnd.Parameters.Add("@Adress1", SqlDbType.NVarChar, 100, "Adress1");
                        cmnd.Parameters.Add("@Adress2", SqlDbType.NVarChar, 100, "Adress2");
                        cmnd.Parameters.Add("@Adress3", SqlDbType.NVarChar, 100, "Adress3");
                        cmnd.Parameters.Add("@Distance", SqlDbType.Decimal, 6, "Distance");
                        cmnd.Parameters.Add("@Summary", SqlDbType.Int, 6, "Summary");
                        cmnd.Parameters.Add("@DataTime", SqlDbType.DateTime, 0, "DataTime");
                        cmnd.Parameters.Add("@DriverID", SqlDbType.Int, 6, "DriverID");
                        cmnd.Parameters.Add("@ClientID", SqlDbType.Int, 6, "ClientID");
                        cmnd.Parameters.Add("@PaymentOperator", SqlDbType.NVarChar, 100, "PaymentOperator");
                        cmnd.Parameters.Add("@autoClass_ID", SqlDbType.Int, 6, "autoClass_ID");
                        cmnd.Parameters.Add("@ExServise_ID", SqlDbType.Int, 6, "ExServise_ID");
                        adapter.InsertCommand = cmnd;
                        //update
                        cmnd = new SqlCommand("update Rides set Rides_ID=@Rides_ID, Adress1=@Adress1 ,Adress2=@Adress2 ,Adress3=@Adress3 ,Distance=@Distance ,Summary=@Summary ,DataTime=@DataTime ,DriverID=@DriverID ,ClientID=@ClientID, " +
                            "PaymentOperator=@PaymentOperator,autoClass_ID=@autoClass_ID ,ExServise_ID=@ExServise_ID where Rides_ID=@prevRides_ID", connect);
                        cmnd.Parameters.Add("@Rides_ID", SqlDbType.Int, 6, "Rides_ID");
                        cmnd.Parameters.Add("@Adress1", SqlDbType.NVarChar, 100, "Adress1");
                        cmnd.Parameters.Add("@Adress2", SqlDbType.NVarChar, 100, "Adress2");
                        cmnd.Parameters.Add("@Adress3", SqlDbType.NVarChar, 100, "Adress3");
                        cmnd.Parameters.Add("@Distance", SqlDbType.Decimal, 6, "Distance");
                        cmnd.Parameters.Add("@Summary", SqlDbType.Int, 6, "Summary");
                        cmnd.Parameters.Add("@DataTime", SqlDbType.DateTime, 0, "DataTime");
                        cmnd.Parameters.Add("@DriverID", SqlDbType.Int, 6, "DriverID");
                        cmnd.Parameters.Add("@ClientID", SqlDbType.Int, 6, "ClientID");
                        cmnd.Parameters.Add("@PaymentOperator", SqlDbType.NVarChar, 100, "PaymentOperator");
                        cmnd.Parameters.Add("@autoClass_ID", SqlDbType.Int, 6, "autoClass_ID");
                        cmnd.Parameters.Add("@ExServise_ID", SqlDbType.Int, 6, "ExServise_ID");
                        SqlParameter parameter = cmnd.Parameters.Add("@prevRides_ID", SqlDbType.Int, 6, "Rides_ID");
                        parameter.SourceVersion = DataRowVersion.Original;
                        adapter.UpdateCommand = cmnd;

                        //delete 
                        cmnd = new SqlCommand("delete from Rides where Rides_ID=@Rides_ID", connect);
                        parameter = cmnd.Parameters.Add("@Rides_ID", SqlDbType.Int, 6, "Rides_ID");
                        parameter.SourceVersion = DataRowVersion.Original;
                        adapter.DeleteCommand = cmnd;

                    }
                    break;

                case "Drivers Table":
                    {
                        //select
                        adapter.SelectCommand = new SqlCommand("select * from Driver (Driver_ID, D_Full_name, Auto_model, Auto_plate, D_Phone_number,Licence_number)", connect);

                        //ins
                        cmnd = new SqlCommand("insert into Driver (D_Full_name, Auto_model, Auto_plate, D_Phone_number,Licence_number) values " +
                            "(@D_Full_name, @Auto_model, @Auto_plate, @D_Phone_number, @Licence_number)", connect);
                      //  cmnd.Parameters.Add("@Driver_ID",SqlDbType.Int,6,"Driver_ID");
                        cmnd.Parameters.Add("@D_Full_name",SqlDbType.NVarChar,100, "D_Full_name");
                        cmnd.Parameters.Add("@Auto_model",SqlDbType.NVarChar,100, "Auto_model");
                        cmnd.Parameters.Add("@Auto_plate",SqlDbType.NVarChar,10, "Auto_plate");
                        cmnd.Parameters.Add("@D_Phone_number", SqlDbType.NVarChar, 20, "D_Phone_number");
                        cmnd.Parameters.Add("@Licence_number", SqlDbType.NVarChar, 20, "Licence_number");
                        adapter.InsertCommand = cmnd;

                        //upd
                        cmnd = new SqlCommand("update Driver set D_Full_name=@D_Full_name, Auto_model=@Auto_model, Auto_plate=@Auto_plate, D_Phone_number=@D_Phone_number, Licence_number=@Licence_number where Driver_ID=@prevDriver_ID",connect);

                        //cmnd.Parameters.Add("@Driver_ID", SqlDbType.Int, 6, "Driver_ID");
                        cmnd.Parameters.Add("@D_Full_name", SqlDbType.NVarChar, 100, "D_Full_name");
                        cmnd.Parameters.Add("@Auto_model", SqlDbType.NVarChar, 100, "Auto_model");
                        cmnd.Parameters.Add("@Auto_plate", SqlDbType.NVarChar, 10, "Auto_plate");
                        cmnd.Parameters.Add("@D_Phone_number", SqlDbType.NVarChar, 20, "D_Phone_number");
                        cmnd.Parameters.Add("@Licence_number", SqlDbType.NVarChar, 20, "Licence_number");
                        SqlParameter parameter = cmnd.Parameters.Add("@prevDriver_ID", SqlDbType.Int, 6, "Driver_ID");
                        parameter.SourceVersion = DataRowVersion.Original;
                        adapter.UpdateCommand = cmnd;

                        //del
                        cmnd = new SqlCommand("delete from Driver_ID where Driver_ID=@Driver_ID", connect);
                        cmnd.Parameters.Add("@Driver_ID", SqlDbType.Int, 6, "Driver_ID");
                        parameter.SourceVersion = DataRowVersion.Original;
                        adapter.DeleteCommand = cmnd;
                    }
                    break;

                case "Clients Table":
                    {
                        //select
                        adapter.SelectCommand = new SqlCommand("select * from Client (Client_ID, C_Full_name, C_Age, C_Phone_number)", connect);

                        //ins
                        cmnd = new SqlCommand("insert into Client (C_Full_name, C_Age, C_Phone_number) values " +
                            "(@C_Full_name, @C_Age, @C_Phone_number)", connect);
                        //cmnd.Parameters.Add("@Client_ID", SqlDbType.Int, 6, "Client_ID");
                        cmnd.Parameters.Add("@C_Full_name", SqlDbType.NVarChar, 70, "C_Full_name");
                        cmnd.Parameters.Add("@C_Age",SqlDbType.Int,6,"C_Age");
                        cmnd.Parameters.Add("@C_Phone_number", SqlDbType.NVarChar, 20, "C_Phone_number");
                       
                        adapter.InsertCommand = cmnd;

                        //upd
                        cmnd = new SqlCommand("update Client set C_Full_name=@C_Full_name, C_Age=@C_Age, C_Phone_number=@C_Phone_number where Client_ID=@prevClient_ID", connect);
                        //cmnd.Parameters.Add("@Client_ID", SqlDbType.Int, 6, "Client_ID");
                        cmnd.Parameters.Add("@C_Full_name", SqlDbType.NVarChar, 70, "C_Full_name");
                        cmnd.Parameters.Add("@C_Age", SqlDbType.Int, 6, "C_Age");
                        cmnd.Parameters.Add("@C_Phone_number", SqlDbType.NVarChar, 20, "C_Phone_number");
                        SqlParameter parameter = cmnd.Parameters.Add("@prevClient_ID", SqlDbType.Int, 6, "Client_ID");
                        parameter.SourceVersion = DataRowVersion.Original;
                        adapter.UpdateCommand = cmnd;

                        //del
                        cmnd = new SqlCommand("delete from Client where Client_ID=@Client_ID", connect);
                        cmnd.Parameters.Add("@Client_ID", SqlDbType.Int, 6, "Client_ID");
                        parameter.SourceVersion = DataRowVersion.Original;
                        adapter.DeleteCommand = cmnd;
                    }
                    break;

                case "Extra Servises Table":
                    {
                        //sel
                        adapter.SelectCommand = new SqlCommand("select * from ExrtaServises (Servise_ID,Servise_Name,Servise_Surcharge)", connect);

                        //ins
                        cmnd = new SqlCommand("insert into ExtraServises (Servise_Name, Servise_Surcharge) values (@Servise_Name, @Servise_Surcharge)",connect);
                        //cmnd.Parameters.Add("@Servise_ID",SqlDbType.Int,6, "Servise_ID");
                        cmnd.Parameters.Add("@Servise_Name",SqlDbType.NVarChar,50, "Servise_Name");
                        cmnd.Parameters.Add("@Servise_Surcharge",SqlDbType.Int,6, "Servise_Surcharge");
                        adapter.InsertCommand = cmnd;


                        //update
                        cmnd = new SqlCommand("update ExtraServises set Servise_ID=@Servise_ID, Servise_Name=@Servise_Name, Servise_Surcharge=@Servise_Surcharge where @prevServise_ID=Servise_ID",connect);
                        cmnd.Parameters.Add("@Servise_ID", SqlDbType.Int, 6, "Servise_ID");
                        cmnd.Parameters.Add("@Servise_Name", SqlDbType.NVarChar, 50, "Servise_Name");
                        cmnd.Parameters.Add("@Servise_Surcharge", SqlDbType.Int, 6, "Servise_Surcharge");
                        SqlParameter parameter = cmnd.Parameters.Add("prevServise_ID", SqlDbType.Int,6,"Servise_ID");
                        parameter.SourceVersion = DataRowVersion.Original;
                        adapter.UpdateCommand = cmnd;

                        //del
                        cmnd = new SqlCommand("delete from ExtraServises where Servise_ID=@Servise_ID",connect);
                        cmnd.Parameters.Add("@Servise_ID", SqlDbType.Int, 6, "Servise_ID");
                        parameter.SourceVersion = DataRowVersion.Original;
                        adapter.DeleteCommand = cmnd;



                    }
                    break;

                case "Payment Table":
                    {
                        //select
                        adapter.SelectCommand = new SqlCommand("select * from Payment(Pay_Type, Pay_Operator)", connect);
                        //ins
                        cmnd = new SqlCommand("Insert into Payment (Pay_Type, Pay_Operator) values (@Pay_Type, @Pay_Operator)", connect);
                        cmnd.Parameters.Add("@Pay_Type",SqlDbType.NVarChar,20, "Pay_Type");
                        cmnd.Parameters.Add("@Pay_Operator",SqlDbType.NVarChar,100, "Pay_Operator");
                        adapter.InsertCommand = cmnd;
                        //upd
                        cmnd = new SqlCommand("update Payment set Pay_Type=@Pay_Type, Pay_Operator=@Pay_Operator where @prevPay_Operator=Pay_Operator",connect);
                        cmnd.Parameters.Add("@Pay_Type", SqlDbType.NVarChar, 20, "Pay_Type");
                        cmnd.Parameters.Add("@Pay_Operator", SqlDbType.NVarChar, 100, "Pay_Operator");
                        SqlParameter parameter = cmnd.Parameters.Add("@prevPay_Operator",SqlDbType.NVarChar,100,"Pay_Operator");
                        parameter.SourceVersion = DataRowVersion.Original;
                        adapter.UpdateCommand = cmnd;
                        //del
                        cmnd = new SqlCommand("delete from Payment where Pay_Operator=@Pay_Oerator",connect);
                        parameter = cmnd.Parameters.Add("@Pay_Operator",SqlDbType.NVarChar,100,"Pay_Operator");
                        parameter.SourceVersion = DataRowVersion.Original;
                        adapter.DeleteCommand = cmnd;
                        
                    }
                    break;

                case "Auto Class Table":
                    {
                        //select
                        adapter.SelectCommand = new SqlCommand("seleсt * from AutoClass(Class_ID,Class_Name,Coef)",connect);
                        //ins
                        cmnd = new SqlCommand("insert into AutoClass (Class_Name,Coef) values (@Class_name, @Coef)", connect);
                        //cmnd.Parameters.Add("@Class_ID",SqlDbType.Int,6, "Class_ID");
                        cmnd.Parameters.Add("@Class_Name",SqlDbType.NVarChar,20, "Class_Name");
                        cmnd.Parameters.Add("@Coef",SqlDbType.Int,6, "Coef");
                        adapter.InsertCommand = cmnd;
                        //upd
                        cmnd = new SqlCommand("update AutoClass set Class_Name=@Class_Name, Coef=@Coef where @prevClass_ID=Class_ID", connect);
                       // cmnd.Parameters.Add("@Class_ID", SqlDbType.Int, 6, "Class_ID");
                        cmnd.Parameters.Add("@Class_Name", SqlDbType.NVarChar, 20, "Class_Name");
                        cmnd.Parameters.Add("@Coef", SqlDbType.Int, 6, "Coef");
                        SqlParameter parameter = cmnd.Parameters.Add("@prevClass_ID",SqlDbType.Int,6, "Class_ID");
                        parameter.SourceVersion = DataRowVersion.Original;
                        adapter.UpdateCommand = cmnd;


                        //del
                        cmnd = new SqlCommand("delete from AutoClass where Class_ID=@Class_ID", connect);
                        parameter = cmnd.Parameters.Add("@ClassID", SqlDbType.Int, 6, "Class_ID");
                        parameter.SourceVersion = DataRowVersion.Original;
                        adapter.DeleteCommand = cmnd;
                    }
                    break;
            }
            return adapter;
        }
        private void CDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
         
            Btn_DeleteRow.IsEnabled = true;
        }

      

        private void Table_Selection_CB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            CBi = ((sender as ComboBox).SelectedItem as ComboBoxItem);
            Column_Selection_CB.ItemsSource = null;
            switch (CBi.Content.ToString())
            {

                case "General Table":
                    {
                        Column_Selection_CB.ItemsSource = GenTableStrings;
                        DT = new DataTable();
                        adapter.SelectCommand = new SqlCommand(genCmndLine, connect);
                        adapter.Fill(DT);
                        ADataGrid.ItemsSource = DT.DefaultView;
                        AdapterSettingsSet(CBi, connect);

                    }
                    break;


                case "Drivers Table":
                    {
                        Column_Selection_CB.ItemsSource = DrTableStrings;
                        DT = new DataTable();
                        adapter.SelectCommand = new SqlCommand(drCmndLine, connect);
                        adapter.Fill(DT);
                        ADataGrid.ItemsSource = DT.DefaultView;
                        AdapterSettingsSet(CBi, connect);
                    }
                    break;

                case "Clients Table":
                    {
                        Column_Selection_CB.ItemsSource = CtTableStrings;
                        DT = new DataTable();
                        adapter.SelectCommand = new SqlCommand(cltCmndLine, connect);
                        adapter.Fill(DT);
                        ADataGrid.ItemsSource = DT.DefaultView;
                        AdapterSettingsSet(CBi, connect);

                    }
                    break;

                case "Extra Servises Table":
                    {
                        Column_Selection_CB.ItemsSource = ServTableStrings;
                        DT = new DataTable();
                        adapter.SelectCommand = new SqlCommand(servCmndLine, connect);
                        adapter.Fill(DT);
                        ADataGrid.ItemsSource = DT.DefaultView;
                        AdapterSettingsSet(CBi, connect);
                    }

                    break;

                case "Payment Table":
                    {
                        Column_Selection_CB.ItemsSource = PayTableStrings;
                        DT = new DataTable();
                        adapter.SelectCommand = new SqlCommand(payCmndLine, connect);
                        adapter.Fill(DT);
                        ADataGrid.ItemsSource = DT.DefaultView;
                        AdapterSettingsSet(CBi, connect);
                    }
                    break;

                case "Auto Class Table":
                    {
                        Column_Selection_CB.ItemsSource = ClTableStrings;
                        DT = new DataTable();
                        adapter.SelectCommand = new SqlCommand(clssCmndLine, connect);
                        adapter.Fill(DT);
                        ADataGrid.ItemsSource = DT.DefaultView;
                        AdapterSettingsSet(CBi, connect);
                    }
                    break;
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            RadioButton RBChecked = (RadioButton)sender;
            rbText = RBChecked.Content.ToString();
        }

        private void RadioButton_Checked_1(object sender, RoutedEventArgs e)
        {
            RadioButton RBChecked = (RadioButton)sender;
            rbText = RBChecked.Content.ToString();
        }

        private void Btn_Find_Click(object sender, RoutedEventArgs e)
        {
            if (Column_Selection_CB.SelectedIndex > -1)
            {
                switch (CBi.Content.ToString())
                {
                    case "General Table":
                        switch (Column_Selection_CB.SelectedItem.ToString())
                        {
                            case "Rides_ID":
                                cmndFind = new SqlCommand($"select * from Rides where Rides_ID like '%{TB_Search.Text.ToString()}%' ");
                                break;
                            case "Adress1":
                                cmndFind = new SqlCommand($"select * from Rides where Adress1 like '%{TB_Search.Text.ToString()}%' ");
                                break;
                            case "Addres2":
                                cmndFind = new SqlCommand($"select * from Rides where Adress2 like '%{TB_Search.Text.ToString()}%' ");
                                break;
                            case "Adress3":
                                cmndFind = new SqlCommand($"select * from Rides where Adress3 like '%{TB_Search.Text.ToString()}%' ");
                                break;
                            case "Distance":
                                cmndFind = new SqlCommand($"select * from Rides where Distance like '%{TB_Search.Text.ToString()}%' ");
                                break;
                            case "Summary":
                                cmndFind = new SqlCommand($"select * from Rides where Summary like '%{TB_Search.Text.ToString()}%' ");
                                break;
                            case "DataTime":
                                cmndFind = new SqlCommand($"select * from Rides where DataTime like '%{TB_Search.Text.ToString()}%' ");
                                break;
                            case "DriverID":
                                cmndFind = new SqlCommand($"select * from Rides where DriverID like '%{TB_Search.Text.ToString()}%' ");
                                break;
                            case "ClientID":
                                cmndFind = new SqlCommand($"select * from Rides where ClientID like '%{TB_Search.Text.ToString()}%' ");
                                break;
                            case "PaymentOperator":
                                cmndFind = new SqlCommand($"select * from Rides where PaymentOperator like '%{TB_Search.Text.ToString()}%' ");
                                break;
                            case "autoClass_ID":
                                cmndFind = new SqlCommand($"select * from Rides where autoClass_ID like '%{TB_Search.Text.ToString()}%' ");
                                break;
                            case "ExServise_ID":
                                cmndFind = new SqlCommand($"select * from Rides where ExServise_ID like '%{TB_Search.Text.ToString()}%' ");
                                break;
                        }

                        break;

                    case "Drivers Table":
                        switch (Column_Selection_CB.SelectedItem.ToString())
                        {
                            case "Driver_ID":
                                cmndFind = new SqlCommand($"select * from Driver where Driver_ID like '%{TB_Search.Text.ToString()}%'");
                                break;
                            case "D_Full_name":
                                cmndFind = new SqlCommand($"select * from Driver where D_Full_name like '%{TB_Search.Text.ToString()}%'");
                                break;
                            case "Auto_model":
                                cmndFind = new SqlCommand($"select * from Driver where Auto_model like '%{TB_Search.Text.ToString()}%'");
                                break;
                            case "Auto_plate":
                                cmndFind = new SqlCommand($"select * from Driver where Auto_plate like '%{TB_Search.Text.ToString()}%'");
                                break;
                            case "D_Phone_number":
                                cmndFind = new SqlCommand($"select * from Driver where D_Phone_number like '%{TB_Search.Text.ToString()}%'");
                                break;
                            case "Licence_number":
                                cmndFind = new SqlCommand($"select * from Driver where Licence_number like '%{TB_Search.Text.ToString()}%'");
                                break;
                        }
                        break;
                    case "Clients Table":
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
                        break;
                    case "Extra Servises Table":
                        switch (Column_Selection_CB.SelectedItem.ToString())
                        {
                            case "Servise_ID":
                                cmndFind = new SqlCommand($"select * from ExtraServises where Servise_ID like '%{TB_Search.Text.ToString()}%'");
                                break;
                            case "Servise_Name":
                                cmndFind = new SqlCommand($"select * from ExtraServises where Servise_Name like '%{TB_Search.Text.ToString()}%'");
                                break;
                            case "Servise_Surcharge":
                                cmndFind = new SqlCommand($"select * from ExtraServises where Servise_Surcharge like '%{TB_Search.Text.ToString()}%'");
                                break;
                        }
                        break;
                    case "Payment Table":
                        switch (Column_Selection_CB.SelectedItem.ToString())
                        {
                            case "Pay_Type":
                                cmndFind = new SqlCommand($"select * from Payment where Pay_Type like '%{TB_Search.Text.ToString()}%'");
                                break;
                            case "Pay_Operator":
                                cmndFind = new SqlCommand($"select * from Payment where Pay_Operator like '%{TB_Search.Text.ToString()}%'");
                                break;
                        }
                        break;

                    case "Auto Class Table":
                        switch (Column_Selection_CB.SelectedItem.ToString())
                        {
                            case "Class_ID":
                                cmndFind = new SqlCommand($"select * from AutoClass where Class_ID like '%{TB_Search.Text.ToString()}%'");
                                break;
                            case "Class_Name":
                                cmndFind = new SqlCommand($"select * from AutoClass where Class_Name like '%{TB_Search.Text.ToString()}%'");
                                break;
                            case "Coef":
                                cmndFind = new SqlCommand($"select * from AutoClass where Coef like '%{TB_Search.Text.ToString()}%'");
                                break;
                        }
                        break;

                }
            }
            if (Table_Selection_CB.SelectedItem != null)
            {
                if (Column_Selection_CB.SelectedItem != null)
                {
                    if (TB_Search.Text.Length > 0)
                    {
                        cmndFind.Connection = connect;
                        SqlDataReader reader = cmndFind.ExecuteReader();
                        DataTable FDT = new DataTable();
                        FDT.Load(reader);
                        ADataGrid.ItemsSource = FDT.DefaultView;

                    }
                }
            }
        }
        private void Btn_cancel_Click(object sender, RoutedEventArgs e)
        {

          
            switch (CBi.Content.ToString())
            {

                case "General Table":
                    {
                        Column_Selection_CB.ItemsSource = GenTableStrings;
                        DT = new DataTable();
                        adapter.SelectCommand = new SqlCommand(genCmndLine, connect);
                        adapter.Fill(DT);
                        ADataGrid.ItemsSource = DT.DefaultView;


                    }
                    break;

                case "Drivers Table":
                    {
                        Column_Selection_CB.ItemsSource = DrTableStrings;
                        DT = new DataTable();
                        adapter.SelectCommand = new SqlCommand(drCmndLine, connect);
                        adapter.Fill(DT);
                        ADataGrid.ItemsSource = DT.DefaultView;
                    }
                    break;

                case "Clients Table":
                    {
                        Column_Selection_CB.ItemsSource = CtTableStrings;
                        DT = new DataTable();
                        adapter.SelectCommand = new SqlCommand(cltCmndLine, connect);
                        adapter.Fill(DT);
                        ADataGrid.ItemsSource = DT.DefaultView;

                    }
                    break;

                case "Extra Servises Table":
                    {
                        Column_Selection_CB.ItemsSource = ServTableStrings;
                        DT = new DataTable();
                        adapter.SelectCommand = new SqlCommand(servCmndLine, connect);
                        adapter.Fill(DT);
                        ADataGrid.ItemsSource = DT.DefaultView;
                    }
                    break;

                case "Payment Table":
                    {
                        Column_Selection_CB.ItemsSource = PayTableStrings;
                        DT = new DataTable();
                        adapter.SelectCommand = new SqlCommand(payCmndLine, connect);
                        adapter.Fill(DT);
                        ADataGrid.ItemsSource = DT.DefaultView;
                    }
                    break;

                case "Auto Class Table":
                    {
                        Column_Selection_CB.ItemsSource = ClTableStrings;
                        DT = new DataTable();
                        adapter.SelectCommand = new SqlCommand(clssCmndLine, connect);
                        adapter.Fill(DT);
                        ADataGrid.ItemsSource = DT.DefaultView;
                    }
                    break;
                default:
                    {
                        MessageBox.Show("FAIL");
                    }
                    break;



            }
            TB_Search.Text = null;
        }
     

        private void Btn_DeleteRow_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (ADataGrid.SelectedItems != null)
                {
                    DataRowView datarowView = ADataGrid.SelectedItems[0] as DataRowView;
                    if (datarowView != null)
                    {
                        datarowView.Row.Delete();
                    }
                    DB_Updater();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                ADataGrid.ItemsSource = DT.DefaultView;
            }
        }
        private List<string> LoadClientInfo(int id)
        {
            List<string> info = new List<string>();
            cmnd = new SqlCommand("select * from Client where Client_ID =" + id,connect);
            SqlDataReader reader = cmnd.ExecuteReader();
            if (reader.Read())
            {
                info.Add("ID - "+reader[0].ToString());
                info.Add("ФИО - "+reader[1].ToString());
                info.Add("Возраст - "+reader[2].ToString());
                info.Add("Номер телефона - "+reader[3].ToString());
            }

            else
            {
                MessageBox.Show("ID не найден");
                Close();
            }
            reader.Close();
            return info;

        }

        private List<string> LoadDriverInfo(int id)
        {
            List<string> info = new List<string>();
            cmnd = new SqlCommand("select * from Driver where Driver_ID =" + id, connect);
            SqlDataReader reader = cmnd.ExecuteReader();
            if (reader.Read())
            {
                info.Add("ID - " + reader[0].ToString());
                info.Add("ФИО - " + reader[1].ToString());
                info.Add("Автомобиль - " + reader[2].ToString());
                info.Add("Номерной знак автомобиля - " + reader[3].ToString());
                info.Add("Номер телефона - " + reader[4].ToString());
                info.Add("Номер водительских прав - " + reader[5].ToString());
            }

            else
            {
                MessageBox.Show("ID не найден");
                Close();
            }
            reader.Close();
            return info;

        }
        private void Btn_Report_Click(object sender, RoutedEventArgs e)
        {
           if(rbText!=null)
           {
                if (TB_Report.Text!=null)
                {
                    try
                    {
                        int ReportID = int.Parse(TB_Report.Text.ToString());
                    }

                    catch
                    {
                        MessageBox.Show("Введите целое число");
                        return;
                    }

                    ReportWindow RW = new ReportWindow();
                   
                    
                    switch (rbText)
                    {
                        case "Client":
                            {
                                RW.InfoList = LoadClientInfo(int.Parse(TB_Report.Text.ToString()));

                                cmndReport = new SqlCommand("select count(Rides_ID) from Rides where ClientID=@ClientID",connect);
                                SqlParameter param = new SqlParameter();
                                param.ParameterName = "@ClientID";
                                param.Value = int.Parse(TB_Report.Text.ToString());
                                cmndReport.Parameters.Add(param);
                                SqlDataReader reader = cmndReport.ExecuteReader();
                                while(reader.Read())
                                {
                                    object num = reader.GetValue(0);
                                    RW.RidesCount = int.Parse(num.ToString());
                                    
                                }
                                reader.Close();

                                cmndReport = new SqlCommand("select sum(Distance) from Rides where ClientID=@ClientID",connect);
                                param = new SqlParameter();
                                param.ParameterName = "@ClientID";
                                param.Value = int.Parse(TB_Report.Text.ToString());
                                cmndReport.Parameters.Add(param);
                                reader = cmndReport.ExecuteReader();
                                while (reader.Read())
                                {
                                    object num = reader.GetValue(0);
                                    RW.DistanceSum = double.Parse(num.ToString());
                           

                                }
                                reader.Close();


                                cmndReport = new SqlCommand("select sum(Summary) from Rides where ClientID=@ClientID",connect);
                                param = new SqlParameter();
                                param.ParameterName = "@ClientID";
                                param.Value = int.Parse(TB_Report.Text.ToString());
                                cmndReport.Parameters.Add(param);
                                reader = cmndReport.ExecuteReader();
                                while (reader.Read())
                                {
                                    object num = reader.GetValue(0);
                                    RW.Sum = int.Parse(num.ToString());

                                }
                                reader.Close();

                            }
                            break;


                        case "Driver":
                            {
                                 RW.InfoList = LoadDriverInfo(int.Parse(TB_Report.Text.ToString()));
                                cmndReport = new SqlCommand("select count(Rides_ID) from Rides where DriverID=@DriverID",connect);
                                SqlParameter param = new SqlParameter();
                                param.ParameterName = "@DriverID";
                                param.Value = int.Parse(TB_Report.Text.ToString());
                                cmndReport.Parameters.Add(param);
                                SqlDataReader reader = cmndReport.ExecuteReader();
                                while (reader.Read())
                                {
                                    object num = reader.GetValue(0);
                                    RW.RidesCount = int.Parse(num.ToString());

                                }
                                reader.Close();

                                cmndReport = new SqlCommand("select sum(Distance) from Rides where DriverID=@DriverID",connect);
                                param = new SqlParameter();
                                param.ParameterName = "@DriverID";
                                param.Value = int.Parse(TB_Report.Text.ToString());
                                cmndReport.Parameters.Add(param);
                                reader = cmndReport.ExecuteReader();
                                while (reader.Read())
                                {
                                    object num = reader.GetValue(0);
                                    RW.DistanceSum = double.Parse(num.ToString());
                                }
                                reader.Close();


                                cmndReport = new SqlCommand("select sum(Summary) from Rides where DriverID=@DriverID",connect);
                                param = new SqlParameter();
                                param.ParameterName = "@DriverID";
                                param.Value = int.Parse(TB_Report.Text.ToString());
                                cmndReport.Parameters.Add(param);
                                reader = cmndReport.ExecuteReader();
                                while (reader.Read())
                                {
                                    object num = reader.GetValue(0);
                                    RW.Sum = int.Parse(num.ToString());

                                }
                                reader.Close();
                            }
                            break;
                            
                    }
                    RW.ShowDialog();

                }
           }
           
        }

        private void Btn_back_Click(object sender, RoutedEventArgs e)
        {
            Close();
            mainWindow.Visibility = Visibility.Visible;

        }


    }
}
