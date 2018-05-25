using System.Data;
using System.Windows;
using MySql.Data.MySqlClient;

namespace Mysql1
{
    /// <summary>
    /// Food.xaml 的交互逻辑
    /// </summary>
    public partial class Food : Window
    {
        static MySqlConnection conn = new MySqlConnection("Database=shop;Data Source=localhost;User Id=root;Password=*********;SslMode=none");
        string sql = "SELECT * FROM foodGoods";
        DataTable dt = new DataTable();

        public Food()
        {
            conn.Open();
            InitializeComponent();
            updatetMysql();
        }

        private void updatetMysql()
        {
            dt.Clear();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
            mySqlDataAdapter.SelectCommand = new MySqlCommand(sql, conn);
            mySqlDataAdapter.Fill(dt);
            clothDataGrid.ItemsSource = dt.DefaultView;
        }

        private void clothbutton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)clothDataGrid.SelectedItem;
            string number = dataRowView.Row[6].ToString();
            int buynumber = int.Parse(foodnumber.Text);
            if (buynumber < 0)
            {
                MessageBox.Show("请重填购买数量！");
            }
            else
            {
                if (buynumber > int.Parse(number))
                {
                    MessageBox.Show("库存不足！");
                }
                else
                {
                    string sqlcommand = "UPDATE foodGoods SET number = number - " + foodnumber.Text + " WHERE fid = '" + dataRowView.Row[0].ToString() + "';";
                    MySqlTransaction tx = conn.BeginTransaction();
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.Transaction = tx;
                    cmd.CommandText = sqlcommand;
                    try
                    {
                        cmd.ExecuteNonQuery();
                        tx.Commit();
                        updatetMysql();
                    }
                    catch
                    {
                        tx.Rollback();
                    }
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            conn.Clone();
        }
    }
}
