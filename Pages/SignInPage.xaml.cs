using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.IO;

namespace Data_Binding_Mironov.Pages
{
    /// <summary>
    /// Логика взаимодействия для SignInPage.xaml
    /// </summary>
    public partial class SignInPage : Page
    {
        Doctor user;

        public SignInPage()
        {
            InitializeComponent();
        }

        private void SignIn(object sender, RoutedEventArgs e)
        {
            if (passwordLoginTextBox.Text != "" && idLoginTextBox.Text != "")
            {
                string fName = $"D_{idLoginTextBox.Text}.json";
                if (File.Exists(fName))
                {
                    string jsonString = File.ReadAllText(fName);
                    Doctor jsonAnswer = JsonSerializer.Deserialize<Doctor>(jsonString);

                    if (jsonAnswer.Password == passwordLoginTextBox.Text)
                    {
                        user = JsonSerializer.Deserialize<Doctor>(jsonString);

                        idLoginTextBox.Text = "";
                        passwordLoginTextBox.Text = "";

                        NavigationService.Navigate(new MainPage(user));
                    }
                    else
                        MessageBox.Show("Неверный пароль");
                }
                else
                    MessageBox.Show("Такого файла не существует");
            }
            else
                MessageBox.Show("Заполните пустые поля");
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new RegistrPage());
        }
    }
}
