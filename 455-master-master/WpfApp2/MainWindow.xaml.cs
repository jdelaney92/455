using System.Windows;
using TVA_CCU.Excel1;


namespace TVA_CCU
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
            var form = new Excel1InputWindow();
            form.Show();
            this.Close();
        }
    }
}
