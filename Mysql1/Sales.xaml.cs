using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;
using System.Windows.Controls;

namespace Mysql1
{
    /// <summary>
    /// Sales.xaml 的交互逻辑
    /// </summary>
    public partial class Sales : Window
    {
        static MySqlConnection conn = new MySqlConnection("Database=shop;Data Source=localhost;User Id=root;Password=*********;SslMode=none");
        int judge = 9999;
        DataTable dtcloth = new DataTable();
        DataTable dtfood = new DataTable();
        DataTable resultCloth = new DataTable();
        DataTable resultFood = new DataTable();

        public Sales()
        {
            conn.Open();
            InitializeComponent();
        }

        private void salesChoose_Click(object sender, RoutedEventArgs e)
        {
            if(salesComboBox.SelectedItem == null)
            {
                MessageBox.Show("请选择一个品类！");
            } else
            {
                ComboBoxItem comboBoxItem = salesComboBox.SelectedItem as ComboBoxItem;
                string flag = comboBoxItem.Content.ToString();
                if(flag == "服装类查询")
                {
                    judge = 0;
                } else
                {
                    judge = 1;
                }
                showContent();
            }
        }
        private void showContent()
        {
            if(judge == 0)
            {
                string sql = "SELECT cid AS 商品ID, name AS 商品名称, brand AS 品牌, color AS 颜色,size AS 尺寸,crowd AS 人群, prize AS 价格, number AS 库存 from clothGoods WHERE number = -1;";
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
                mySqlDataAdapter.SelectCommand = new MySqlCommand(sql, conn);
                mySqlDataAdapter.Fill(dtcloth);
                setDataGrid.ItemsSource = dtcloth.DefaultView;
                setDataGrid.MinRowHeight = 30;
            }
            if(judge == 1)
            {
                string sql = "SELECT fid AS 商品ID, name AS 商品名称, brand AS 品牌, exDate AS 过期时间,place AS 产地, prize AS 价格, number AS 库存 from foodGoods WHERE number = -1;";
                MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
                mySqlDataAdapter.SelectCommand = new MySqlCommand(sql, conn);
                mySqlDataAdapter.Fill(dtfood);
                setDataGrid.ItemsSource = dtfood.DefaultView;
                setDataGrid.MinRowHeight = 30;
            }
        }

        private void setButton_Click(object sender, RoutedEventArgs e)
        {
            string sql = "";
            resultCloth.Clear();
            resultFood.Clear();

            if (endDate.Text == string.Empty || beginDate.Text == string.Empty)
            {
                if (judge == 0)
                {
                    sql = "SELECT c.cid AS 商品ID, c.name AS 商品名称,c.brand AS 品牌,c.color AS 颜色,c.size AS 尺寸,c.crowd AS 适合人群,c.prize AS 价格,c.number AS 库存,s.number AS 销售数量, s.sales_date AS 销售日期,s.total_prize AS 销售总额 FROM salesRecord s INNER JOIN clothGoods c WHERE s.gid = c.cid";
                    DataRowView dataRowView = (DataRowView)setDataGrid.SelectedItem;
                    if (dataRowView.Row[0].ToString() != string.Empty)
                    {
                        sql += " AND c.cid = '" + dataRowView.Row[0].ToString() + "'";
                    }
                    if (dataRowView.Row[1].ToString() != string.Empty)
                    {
                        sql += " AND c.name = '" + dataRowView.Row[1].ToString() + "'";
                    }
                    if (dataRowView.Row[2].ToString() != string.Empty)
                    {
                        sql += " AND c.brand = '" + dataRowView.Row[2].ToString() + "'";
                    }
                    if (dataRowView.Row[3].ToString() != string.Empty)
                    {
                        sql += " AND c.color = '" + dataRowView.Row[3].ToString() + "'";
                    }
                    if (dataRowView.Row[4].ToString() != string.Empty)
                    {
                        sql += " AND c.size = '" + dataRowView.Row[4].ToString() + "'";
                    }
                    if (dataRowView.Row[5].ToString() != string.Empty)
                    {
                        sql += " AND c.crowd = '" + dataRowView.Row[5].ToString() + "'";
                    }
                    if (dataRowView.Row[6].ToString() != string.Empty)
                    {
                        sql += " AND c.prize = '" + dataRowView.Row[6].ToString() + "'";
                    }
                    if (dataRowView.Row[7].ToString() != string.Empty)
                    {
                        sql += " AND c.number = '" + dataRowView.Row[7].ToString() + "'";
                    }
                    sql += ";";
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
                    mySqlDataAdapter.SelectCommand = new MySqlCommand(sql, conn);
                    mySqlDataAdapter.Fill(resultCloth);
                    resultDataGrid.ItemsSource = resultCloth.DefaultView;
                }
                else if (judge == 1)
                {
                    sql = "SELECT f.fid AS 商品ID, f.name AS 商品名称,f.brand AS 品牌,f.exDate AS 过期时间,f.place AS 产地,f.prize AS 价格,f.number AS 库存,s.number AS 销售数量, s.sales_date AS 销售日期,s.total_prize AS 销售总额 FROM salesRecord s INNER JOIN foodGoods f WHERE s.gid = f.fid";
                    DataRowView dataRowView = (DataRowView)setDataGrid.SelectedItem;
                    if (dataRowView.Row[0].ToString() != string.Empty)
                    {
                        sql += " AND f.fid = '" + dataRowView.Row[0].ToString() + "'";
                    }
                    if (dataRowView.Row[1].ToString() != string.Empty)
                    {
                        sql += " AND f.name = '" + dataRowView.Row[1].ToString() + "'";
                    }
                    if (dataRowView.Row[2].ToString() != string.Empty)
                    {
                        sql += " AND f.brand = '" + dataRowView.Row[2].ToString() + "'";
                    }
                    if (dataRowView.Row[3].ToString() != string.Empty)
                    {
                        sql += " AND f.exDate = '" + dataRowView.Row[3].ToString() + "'";
                    }
                    if (dataRowView.Row[4].ToString() != string.Empty)
                    {
                        sql += " AND f.place = '" + dataRowView.Row[4].ToString() + "'";
                    }
                    if (dataRowView.Row[5].ToString() != string.Empty)
                    {
                        sql += " AND f.prize = '" + dataRowView.Row[5].ToString() + "'";
                    }
                    if (dataRowView.Row[6].ToString() != string.Empty)
                    {
                        sql += " AND f.number = '" + dataRowView.Row[6].ToString() + "'";
                    }
                    sql += ";";
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
                    mySqlDataAdapter.SelectCommand = new MySqlCommand(sql, conn);
                    mySqlDataAdapter.Fill(resultFood);
                    resultDataGrid.ItemsSource = resultFood.DefaultView;
                }
            } else
            {
                string begin = beginDate.Text;
                string end = endDate.Text;
                if(setDataGrid.SelectedIndex == -1)
                {
                    if(judge == 0)
                    {
                        sql = "SELECT c.cid AS 商品ID, c.name AS 商品名称,c.brand AS 品牌,c.color AS 颜色,c.size AS 尺寸,c.crowd AS 适合人群,c.prize AS 价格,c.number AS 库存,s.number AS 销售数量, s.sales_date AS 销售日期,s.total_prize AS 销售总额 FROM salesRecord s INNER JOIN clothGoods c WHERE s.gid = c.cid AND s.sales_date >= '" + begin + "' AND s.sales_date <= '" + end + "';";
                        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
                        mySqlDataAdapter.SelectCommand = new MySqlCommand(sql, conn);
                        mySqlDataAdapter.Fill(resultCloth);
                        resultDataGrid.ItemsSource = resultCloth.DefaultView;
                    } else if(judge == 1)
                    {
                        sql = "SELECT f.fid AS 商品ID, f.name AS 商品名称,f.brand AS 品牌,f.exDate AS 过期时间,f.place AS 产地,f.prize AS 价格,f.number AS 库存,s.number AS 销售数量, s.sales_date AS 销售日期,s.total_prize AS 销售总额 FROM salesRecord s INNER JOIN foodGoods f WHERE s.gid = f.fid AND s.sales_date >= '" + begin + "' AND s.sales_date <= '" + end + "';";
                        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
                        mySqlDataAdapter.SelectCommand = new MySqlCommand(sql, conn);
                        mySqlDataAdapter.Fill(resultFood);
                        resultDataGrid.ItemsSource = resultFood.DefaultView;
                    }
                } else
                {
                    if (judge == 0)
                    {
                        sql = "SELECT c.cid AS 商品ID, c.name AS 商品名称,c.brand AS 品牌,c.color AS 颜色,c.size AS 尺寸,c.crowd AS 适合人群,c.prize AS 价格,c.number AS 库存,s.number AS 销售数量, s.sales_date AS 销售日期,s.total_prize AS 销售总额 FROM salesRecord s INNER JOIN clothGoods c WHERE s.gid = c.cid AND s.sales_date >= '" + begin + "' AND s.sales_date <= '" + end + "'";
                        DataRowView dataRowView = (DataRowView)setDataGrid.SelectedItem;
                        if (dataRowView.Row[0].ToString() != string.Empty)
                        {
                            sql += " AND c.cid = '" + dataRowView.Row[0].ToString() + "'";
                        }
                        if (dataRowView.Row[1].ToString() != string.Empty)
                        {
                            sql += " AND c.name = '" + dataRowView.Row[1].ToString() + "'";
                        }
                        if (dataRowView.Row[2].ToString() != string.Empty)
                        {
                            sql += " AND c.brand = '" + dataRowView.Row[2].ToString() + "'";
                        }
                        if (dataRowView.Row[3].ToString() != string.Empty)
                        {
                            sql += " AND c.color = '" + dataRowView.Row[3].ToString() + "'";
                        }
                        if (dataRowView.Row[4].ToString() != string.Empty)
                        {
                            sql += " AND c.size = '" + dataRowView.Row[4].ToString() + "'";
                        }
                        if (dataRowView.Row[5].ToString() != string.Empty)
                        {
                            sql += " AND c.crowd = '" + dataRowView.Row[5].ToString() + "'";
                        }
                        if (dataRowView.Row[6].ToString() != string.Empty)
                        {
                            sql += " AND c.prize = '" + dataRowView.Row[6].ToString() + "'";
                        }
                        if (dataRowView.Row[7].ToString() != string.Empty)
                        {
                            sql += " AND c.number = '" + dataRowView.Row[7].ToString() + "'";
                        }
                        sql += ";";
                        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
                        mySqlDataAdapter.SelectCommand = new MySqlCommand(sql, conn);
                        mySqlDataAdapter.Fill(resultCloth);
                        resultDataGrid.ItemsSource = resultCloth.DefaultView;
                    }
                    else if (judge == 1)
                    {
                        sql = "SELECT f.fid AS 商品ID, f.name AS 商品名称,f.brand AS 品牌,f.exDate AS 过期时间,f.place AS 产地,f.prize AS 价格,f.number AS 库存,s.number AS 销售数量, s.sales_date AS 销售日期,s.total_prize AS 销售总额 FROM salesRecord s INNER JOIN foodGoods f WHERE s.gid = f.fid AND s.sales_date >= '" + begin + "' AND s.sales_date <= '" + end + "'";
                        DataRowView dataRowView = (DataRowView)setDataGrid.SelectedItem;
                        if (dataRowView.Row[0].ToString() != string.Empty)
                        {
                            sql += " AND f.fid = '" + dataRowView.Row[0].ToString() + "'";
                        }
                        if (dataRowView.Row[1].ToString() != string.Empty)
                        {
                            sql += " AND f.name = '" + dataRowView.Row[1].ToString() + "'";
                        }
                        if (dataRowView.Row[2].ToString() != string.Empty)
                        {
                            sql += " AND f.brand = '" + dataRowView.Row[2].ToString() + "'";
                        }
                        if (dataRowView.Row[3].ToString() != string.Empty)
                        {
                            sql += " AND f.exDate = '" + dataRowView.Row[3].ToString() + "'";
                        }
                        if (dataRowView.Row[4].ToString() != string.Empty)
                        {
                            sql += " AND f.place = '" + dataRowView.Row[4].ToString() + "'";
                        }
                        if (dataRowView.Row[5].ToString() != string.Empty)
                        {
                            sql += " AND f.prize = '" + dataRowView.Row[5].ToString() + "'";
                        }
                        if (dataRowView.Row[6].ToString() != string.Empty)
                        {
                            sql += " AND f.number = '" + dataRowView.Row[6].ToString() + "'";
                        }
                        sql += ";";
                        MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
                        mySqlDataAdapter.SelectCommand = new MySqlCommand(sql, conn);
                        mySqlDataAdapter.Fill(resultFood);
                        resultDataGrid.ItemsSource = resultFood.DefaultView;
                    }
                }
                
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            conn.Close();
        }
    }
}
