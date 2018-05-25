using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.IO;
using System.Reflection;
using Microsoft.Office.Interop.Excel;

namespace Mysql1
{
    /// <summary>
    /// Data.xaml 的交互逻辑
    /// </summary>
    public partial class Data : System.Windows.Window
    {
        static MySqlConnection conn = new MySqlConnection("Database=shop;Data Source=localhost;User Id=root;Password=********;SslMode=none");
        string sql;
        const string pass = "123456";
        Microsoft.Office.Interop.Excel.Application ac = new Microsoft.Office.Interop.Excel.Application();
        System.Data.DataTable dataTable = new System.Data.DataTable();

        public Data()
        {
            conn.Close();
            InitializeComponent();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            if (beginDate.Text == string.Empty || endDate.Text == string.Empty)
            {
                System.Windows.MessageBox.Show("信息填写不足！请重填。");
            } else
            {
                dataTable.Clear();
                string begin = beginDate.Text;
                string end = endDate.Text;
                if(id.Text == string.Empty)
                {
                    sql = "SELECT gid AS 商品ID, name AS 商品名称, brand AS 商品品牌, SUM(number) AS 销售数量, SUM(total_prize) AS 销售总额 FROM salesRecord WHERE sales_date >= '" + begin + "' AND sales_date <= '" + end + "' GROUP BY gid;";
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
                    mySqlDataAdapter.SelectCommand = new MySqlCommand(sql, conn);
                    mySqlDataAdapter.Fill(dataTable);
                    dataGrid.ItemsSource = dataTable.DefaultView;
                } else
                {
                    sql = "SELECT gid AS 商品ID, name AS 商品名称, brand AS 商品品牌, SUM(number) AS 销售数量, SUM(total_prize) AS 销售总额 FROM salesRecord WHERE sales_date >= '" + begin + "' AND sales_date <= '" + end + "' AND gid = '" + id.Text + "' GROUP BY gid;";
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
                    mySqlDataAdapter.SelectCommand = new MySqlCommand(sql, conn);
                    mySqlDataAdapter.Fill(dataTable);
                    dataGrid.ItemsSource = dataTable.DefaultView;
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            conn.Close();
        }

        private void dayButton_Click(object sender, RoutedEventArgs e)
        {
            string sql1 = "SELECT gid AS 商品ID, name AS 商品名称, brand AS 商品品牌, SUM(number) AS 销售数量, SUM(total_prize) AS 销售总额 FROM salesRecord WHERE to_days(sales_date) = to_days(NOW()) GROUP BY gid;";
            string sql2 = "SELECT brand AS 品牌名称,SUM(total_prize) AS 销售总金额 FROM salesRecord WHERE to_days(sales_date) = to_days(NOW()) GROUP BY brand;";
            exportExcel(sql1, sql2);
            password.Password = null;
        }

        private void exportExcel(string sql1, string sql2 = null)
        {

            System.Data.DataTable dt1 = new System.Data.DataTable();
            System.Data.DataTable dt2 = new System.Data.DataTable();
            if (password.Password != pass)
            {
                MessageBox.Show("验证管理员密码错误！请重新输入。");
            }
            else
            {
                if (sql2 == null)
                {
                    dt1.Clear();
                    MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter();
                    mySqlDataAdapter.SelectCommand = new MySqlCommand(sql1, conn);
                    mySqlDataAdapter.Fill(dt1);
                    Workbook wb1 = ac.Workbooks.Add(true);
                    if (ac == null)
                    {
                        System.Windows.MessageBox.Show("无法创建excel，请检查excel的安装情况");
                        return;
                    }
                    else
                    {
                        SaveFileDialog save1 = new SaveFileDialog();
                        save1.Title = "请选择要保存的路径";
                        if (save1.ShowDialog() == true)
                        {
                            Worksheet sheet = new Worksheet();
                            sheet = wb1.Worksheets.Add();
                            int rowIndex = 1;
                            int colIndex = 0;
                            //为各个sheet添加列名
                            foreach (DataColumn col in dt1.Columns)
                            {
                                colIndex++;
                                sheet.Cells[1, colIndex] = col.ColumnName;
                            }
                            //开始添加数据
                            foreach (DataRow row in dt1.Rows)
                            {
                                rowIndex++;
                                colIndex = 0;
                                foreach (DataColumn col in dt1.Columns)
                                {
                                    colIndex++;
                                    sheet.Cells[rowIndex, colIndex] = row[col.ColumnName];
                                }
                            }
                            sheet.Name = "统计明细";
                            wb1.SaveAs(save1.FileName);
                            wb1.Close();
                        }
                    }
                }
                else
                {
                    dt1.Clear();
                    dt2.Clear();
                    MySqlDataAdapter mySqlDataAdapter1 = new MySqlDataAdapter();
                    MySqlDataAdapter mySqlDataAdapter2 = new MySqlDataAdapter();
                    mySqlDataAdapter1.SelectCommand = new MySqlCommand(sql1, conn);
                    mySqlDataAdapter1.Fill(dt1);
                    mySqlDataAdapter1.SelectCommand = new MySqlCommand(sql2, conn);
                    mySqlDataAdapter1.Fill(dt2);
                    Workbook wb = ac.Workbooks.Add(true);
                    if (ac == null)
                    {
                        System.Windows.MessageBox.Show("无法创建excel，请检查excel的安装情况");
                        return;
                    }
                    else
                    {
                        SaveFileDialog save = new SaveFileDialog();
                        save.Title = "请选择要保存的路径";
                        if (save.ShowDialog() == true)
                        {
                            Worksheet sheet = new Worksheet();
                            sheet = wb.Worksheets.Add();
                            int rowIndex = 1;
                            int colIndex = 0;
                            //为各个sheet添加列名
                            foreach (DataColumn col in dt1.Columns)
                            {
                                colIndex++;
                                sheet.Cells[1, colIndex] = col.ColumnName;
                            }
                            //开始添加数据
                            foreach (DataRow row in dt1.Rows)
                            {
                                rowIndex++;
                                colIndex = 0;
                                foreach (DataColumn col in dt1.Columns)
                                {
                                    colIndex++;
                                    sheet.Cells[rowIndex, colIndex] = row[col.ColumnName];
                                }
                            }
                            sheet.Name = "每种商品销售统计";
                            Worksheet sheet2 = new Worksheet();
                            sheet2 = wb.Worksheets.Add();
                            rowIndex = 1;
                            colIndex = 0;
                            //为各个sheet添加列名
                            foreach (DataColumn col in dt2.Columns)
                            {
                                colIndex++;
                                sheet2.Cells[1, colIndex] = col.ColumnName;
                            }
                            //开始添加数据
                            foreach (DataRow row in dt2.Rows)
                            {
                                rowIndex++;
                                colIndex = 0;
                                foreach (DataColumn col in dt2.Columns)
                                {
                                    colIndex++;
                                    sheet2.Cells[rowIndex, colIndex] = row[col.ColumnName];
                                }
                            }
                            sheet2.Name = "每种品牌销售统计";
                            wb.SaveAs(save.FileName);
                            wb.Close();
                        }
                    }
                }
            }   
        }

        private void weekButton_Click(object sender, RoutedEventArgs e)
        {
            string sql1 = "SELECT gid AS 商品ID, name AS 商品名称, brand AS 商品品牌, SUM(number) AS 销售数量, SUM(total_prize) AS 销售总额 FROM salesRecord WHERE month(sales_date) = month(curdate()) and week(sales_date) = week(curdate()) GROUP BY gid;";
            string sql2 = "SELECT brand AS 品牌名称,SUM(total_prize) AS 销售总金额 FROM salesRecord WHERE month(sales_date) = month(curdate()) and week(sales_date) = week(curdate()) GROUP BY brand;";
            exportExcel(sql1, sql2);
            password.Password = null;
        }

        private void monthButton_Click(object sender, RoutedEventArgs e)
        {
            string sql1 = "SELECT gid AS 商品ID, name AS 商品名称, brand AS 商品品牌, SUM(number) AS 销售数量, SUM(total_prize) AS 销售总额 FROM salesRecord WHERE month(sales_date) = month(curdate()) and year(sales_date) = year(curdate()) GROUP BY gid;";
            string sql2 = "SELECT brand AS 品牌名称,SUM(total_prize) AS 销售总金额 FROM salesRecord WHERE month(sales_date) = month(curdate()) and year(sales_date) = year(curdate()) GROUP BY brand;";
            exportExcel(sql1, sql2);
            password.Password = null;
        }

        private void seasonButton_Click(object sender, RoutedEventArgs e)
        {
            string sql1 = "SELECT gid AS 商品ID, name AS 商品名称, brand AS 商品品牌, SUM(number) AS 销售数量, SUM(total_prize) AS 销售总额 FROM salesRecord WHERE quarter(sales_date) = quarter(curdate()) GROUP BY gid;";
            string sql2 = "SELECT brand AS 品牌名称,SUM(total_prize) AS 销售总金额 FROM salesRecord WHERE quarter(sales_date) = quarter(curdate()) GROUP BY brand;";
            exportExcel(sql1, sql2);
            password.Password = null;
        }

        private void yearutton_Click(object sender, RoutedEventArgs e)
        {
            string sql1 = "SELECT gid AS 商品ID, name AS 商品名称, brand AS 商品品牌, SUM(number) AS 销售数量, SUM(total_prize) AS 销售总额 FROM salesRecord WHERE year(sales_date) = year(curdate()) GROUP BY gid;";
            string sql2 = "SELECT brand AS 品牌名称,SUM(total_prize) AS 销售总金额 FROM salesRecord WHERE year(sales_date) = year(curdate()) GROUP BY brand;";
            exportExcel(sql1, sql2);
            password.Password = null;
        }

        private void salesButton_Click(object sender, RoutedEventArgs e)
        {
            string sql1 = "SELECT sales_date AS 销售日期,gid AS 商品ID,name AS 商品名称,brand AS 品牌,number AS 销售数量,prize AS 价格,total_prize AS 销售总额 FROM salesRecord;";
            exportExcel(sql1);
            password.Password = null;
        }

        private void stockButton_Click(object sender, RoutedEventArgs e)
        {
            string sql1 = "SELECT stock_date AS 进货日期,gid AS 商品ID,number AS 进货数量,prize AS 价格,total_prize AS 进货总价 FROM stockRecord;";
            exportExcel(sql1);
            password.Password = null;
        }
    } 
}
