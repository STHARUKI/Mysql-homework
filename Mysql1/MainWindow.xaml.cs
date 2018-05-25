using System.Windows;

namespace Mysql1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();
        }

        private void clothButton_Click(object sender, RoutedEventArgs e)
        {
            Cloth cloth = new Cloth();
            cloth.Show();
        }

        private void foodButton_Click(object sender, RoutedEventArgs e)
        {
            Food food = new Food();
            food.Show();
        }

        private void soldoutButton_Click(object sender, RoutedEventArgs e)
        {
            Soldout soldout = new Soldout();
            soldout.Show();
        }

        private void statisticsButton_Click(object sender, RoutedEventArgs e)
        {
            Statistics statistics = new Statistics();
            statistics.Show();
        }
    }
}
