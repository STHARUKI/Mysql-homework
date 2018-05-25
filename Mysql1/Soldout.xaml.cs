using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;

namespace Mysql1
{
    /// <summary>
    /// Soldout.xaml 的交互逻辑
    /// </summary>
    public partial class Soldout : Window
    {
        static MySqlConnection conn = new MySqlConnection("Database=shop;Data Source=localhost;User Id=root;Password=*********!;SslMode=none");
        string sql = "SELECT * FROM soldout";
        DataTable dt = new DataTable();
        const string rootPass = "123456";

        public Soldout()
        {
            conn.Open();
            InitializeComponent();
            updateMysql();
        }

        private void updateMysql()
        {
            dt.Clear();
            MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
            mySqlDataAdapter.SelectCommand = new MySqlCommand(sql, conn);
            mySqlDataAdapter.Fill(dt);
            soldoutdatagrid.ItemsSource = dt.DefaultView;
        }

        private void soldoutbutton_Click(object sender, RoutedEventArgs e)
        {
            if(auth.Password != rootPass)
            {
                MessageBox.Show("管理员密码错误！请重新输入。");
            } else
            {
                if(goodid.Text == string.Empty || goodreason.Text == string.Empty)
                {
                    MessageBox.Show("缺少信息！请重新输入。");
                } else
                {
                    try
                    {
                        string sql = "INSERT INTO soldout(soldoutData,soldoutReason,gid,name,brand,prize) SELECT NOW(),'" + goodreason.Text
                            + "',gid,name,brand,prize FROM goods WHERE gid = '" + goodid.Text + "';";
                        string sql2 = "DELETE FROM goods WHERE gid = '" + goodid.Text + "';";
                        MySqlCommand cmd = new MySqlCommand(sql, conn);
                        MySqlCommand cmd2 = new MySqlCommand(sql2, conn);
                        cmd.ExecuteNonQuery();
                        cmd2.ExecuteNonQuery();
                        updateMysql();
                    }
                    catch
                    {
                        MessageBox.Show("信息填写错误，可能有两种原因：该商品已下架或该商品不存在，请重新核对！");
                    }
                }
                auth.Password = null; 
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            conn.Close();
        }
    }
}
