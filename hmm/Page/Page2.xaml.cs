using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
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

namespace hmm.Page
{
    /// <summary>
    /// Логика взаимодействия для Page2.xaml
    /// </summary>
    public partial class Page2
    {
        private DataTable dt = new DataTable();
        public DataTable items
        {
            get => dt;
            set
            {
                dt = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("items"));
            }
        }
        private SqlDataAdapter adapter;
        public Page2()
        {
            InitializeComponent();
            this.DataContext = this;
            items = new DataTable();
            adapter = new SqlDataAdapter("SELECT * FROM PERSON", "Server=SITHOND\\SQLEXPRESS;Database=tested_bd;Trusted_Connection=True;");
            adapter.Fill(items);

            items.PrimaryKey = new DataColumn[1] { items.Columns["ID"] };
            items.Columns["ID"].AutoIncrement = true;
            items.Columns["ID"].AutoIncrementSeed = int.Parse(items.Select().Max(x => x["ID"]).ToString()) + 1;

            SqlCommandBuilder builder = new SqlCommandBuilder(adapter);

            adapter.DeleteCommand = builder.GetDeleteCommand();
            adapter.InsertCommand = builder.GetInsertCommand();
            adapter.UpdateCommand = builder.GetUpdateCommand();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataRow newrow = items.NewRow();

            newrow["PERSONS"] = "2323";
            newrow["COMM"] = ">.<";

            items.Rows.Add(newrow);
            adapter.Update(items);
        }
        private void Table_RowEditEnding(object sender, DataGridRowEditEndingEventArgs e) =>
           new Thread(() => update()).Start();

        private void update()
        {
            Thread.Sleep(30);
            adapter.Update(items);
        }

        private void Table_PreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
                new Thread(() => update()).Start();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page1());
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Page3());
        }
    }
}
