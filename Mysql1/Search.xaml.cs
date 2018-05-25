using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;

namespace Mysql1
{
    /// <summary>
    /// Search.xaml 的交互逻辑
    /// </summary>
    public partial class Search : Window
    {
        static MySqlConnection conn = new MySqlConnection("Database=shop;Data Source=localhost;User Id=root;Password=*********;SslMode=none");
        DataTable dtcloth = new DataTable();
        DataTable dtfood = new DataTable();

        public Search()
        {
            conn.Open();
            InitializeComponent();
        }

        private void clothsearchbutton_Click(object sender, RoutedEventArgs e)
        {
            dtcloth.Clear();
            int count = 0;
            string[] goodinfo = new string[8];
            if(searchclothid.Text != string.Empty)
            {
                goodinfo[0] = "cid = '" + searchclothid.Text + "'";
                ++count;
            }
            if (searchclothname.Text != string.Empty)
            {
                goodinfo[1] = "name = '" + searchclothname.Text + "'";
                ++count;
            }
            if (searchclothbrand.Text != string.Empty)
            {
                goodinfo[2] = "brand = '" + searchclothbrand.Text + "'";
                ++count;
            }
            if (searchclothcolor.Text != string.Empty)
            {
                goodinfo[3] = "color = '" + searchclothcolor.Text + "'";
                ++count;
            }
            if (searchclothsize.Text != string.Empty)
            {
                goodinfo[4] = "size = '" + searchclothsize.Text + "'";
                ++count;
            }
            if (searchclothcrowd.Text != string.Empty)
            {
                goodinfo[5] = "crowd = '" + searchclothcrowd.Text + "'";
                ++count;
            }
            if (searchclothprize.Text != string.Empty)
            {
                goodinfo[6] = "prize = '" + searchclothprize.Text + "'";
                ++count;
            }
            if (searchclothnumber.Text != string.Empty)
            {
                goodinfo[7] = "number = '" + searchclothnumber.Text + "'";
                ++count;
            }
            string sql = "SELECT * FROM clothGoods WHERE ";
            for(int i = 0; i < 8; ++i)
            {
                if(count == 1)
                {
                    break;
                }
                if (goodinfo[i] != null)
                {
                    sql += goodinfo[i] + " AND ";
                    count--;
                    goodinfo[i] = null;
                }
            }
            for(int i = 0; i < 8; ++i)
            {
                if(goodinfo[i] != null)
                {
                    sql += goodinfo[i] + ";";
                }
            }
            MySqlDataAdapter mySqlDataAdapter1 = new MySqlDataAdapter();
            mySqlDataAdapter1.SelectCommand = new MySqlCommand(sql,conn);
            mySqlDataAdapter1.Fill(dtcloth);
            searchCloth.ItemsSource = dtcloth.DefaultView;

        }

        private void foodsearchbutton_Click(object sender, RoutedEventArgs e)
        {
            dtfood.Clear();
            int count = 0;
            string[] goodinfo = new string[7];
            if (searchfoodid.Text != string.Empty)
            {
                goodinfo[0] = "fid = '" + searchfoodid.Text + "'";
                ++count;
            }
            if (searchfoodname.Text != string.Empty)
            {
                goodinfo[1] = "name = '" + searchfoodname.Text + "'";
                ++count;
            }
            if (searchfoodbrand.Text != string.Empty)
            {
                goodinfo[2] = "brand = '" + searchfoodbrand.Text + "'";
                ++count;
            }
            if (searchfooddate.Text != string.Empty)
            {
                goodinfo[3] = "exDate = '" + searchfooddate.Text + "'";
                ++count;
            }
            if (searchfoodplace.Text != string.Empty)
            {
                goodinfo[4] = "place = '" + searchfoodplace.Text + "'";
                ++count;
            }
            if (searchfoodprize.Text != string.Empty)
            {
                goodinfo[5] = "prize = '" + searchfoodprize.Text + "'";
                ++count;
            }
            if (searchfoodnumber.Text != string.Empty)
            {
                goodinfo[6] = "number = '" + searchfoodnumber.Text + "'";
                ++count;
            }
            string sql = "SELECT * FROM foodGoods WHERE ";
            for (int i = 0; i < 7; ++i)
            {
                if (count == 1)
                {
                    break;
                }
                if (goodinfo[i] != null)
                {
                    sql += goodinfo[i] + " AND ";
                    count--;
                    goodinfo[i] = null;
                }
            }
            for (int i = 0; i < 7; ++i)
            {
                if (goodinfo[i] != null)
                {
                    sql += goodinfo[i] + ";";
                }
            }
            MySqlDataAdapter mySqlDataAdapter2 = new MySqlDataAdapter();
            mySqlDataAdapter2.SelectCommand = new MySqlCommand(sql, conn);
            mySqlDataAdapter2.Fill(dtfood);
            searchFood.ItemsSource = dtfood.DefaultView;

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            conn.Close();
        }
    }
}
