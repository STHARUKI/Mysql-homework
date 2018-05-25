using System.Windows;

namespace Mysql1
{
    /// <summary>
    /// Statistics.xaml 的交互逻辑
    /// </summary>
    public partial class Statistics : Window
    {
        public Statistics()
        {
            InitializeComponent();
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            Search search = new Search();
            search.Show();
        }

        private void salesButton_Click(object sender, RoutedEventArgs e)
        {
            Sales sales = new Sales();
            sales.Show();
        }

        private void dataButton_Click(object sender, RoutedEventArgs e)
        {
            Data data = new Data();
            data.Show();
        }
    }
}
