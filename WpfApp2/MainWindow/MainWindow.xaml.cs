using System.Windows;
using WpfApp2.LoanEstimate;

namespace WpfApp2
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
            var form = new LoanEstimateInputWindow();
            form.Show();
            this.Close();
        }
    }
}
