using System.Windows;
using Data_Binding_Mironov.Pages;

namespace Data_Binding_Mironov
{
    public partial class MainWindow : Window
    {
        public static Statistics stat = new Statistics();
        public MainWindow()
        {
            InitializeComponent();
            DataContext = stat;
            MainFrame.Navigate(new SignInPage());
            stat.UpdateCounts();
        }
    }
}