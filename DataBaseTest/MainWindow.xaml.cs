using System.Windows;
using MysqlTemplate2_Mysql_data_dll;

namespace DataBaseTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MysqlTemplate2_Mysql_data_dll.MysqlHelper mysqlHelper = new MysqlHelper();

            mysqlHelper.GetConnectionString("test");
            mysqlHelper.CreateDatabase("test1");

        }
    }
}