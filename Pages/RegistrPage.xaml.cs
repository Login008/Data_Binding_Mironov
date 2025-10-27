using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.IO;

namespace Data_Binding_Mironov.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegistrPage.xaml
    /// </summary>
    public partial class RegistrPage : Page
    {
        public RegistrPage()
        {
            DataContext = doctorOnline;
            InitializeComponent();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        Random rand = new Random();
        Doctor doctorOnline = new Doctor();

        private void Registration(object sender, RoutedEventArgs e)
        {
            if (doctorOnline.LastName != "" && doctorOnline.Name != "" && doctorOnline.MiddleName != "" && doctorOnline.Specialisation != "" && doctorOnline.Password != "")
                if (DoubleCheckingTextBox.Text == doctorOnline.Password)
                {
                    int min = 10000;
                    int max = 99999;

                    string randId = "99999";
                    string fName = "null";

                    List<string> Excludes = new List<string>();

                    for (int i = min; i < max; i++)
                    {
                        do
                        {
                            randId = rand.Next(min, max).ToString();
                        } 
                        while (Excludes.Contains(randId));

                        if (File.Exists(fName))
                        {
                            Excludes.Add(randId);
                        }
                        else
                        {
                            fName = $"D_{randId}.json";
                            break;
                        }

                    }
                    doctorOnline.Id = int.Parse(randId);
                    string jsonString = JsonSerializer.Serialize(doctorOnline);
                    File.WriteAllText(fName, jsonString);

                    DoubleCheckingTextBox.Text = "";
                    MessageBox.Show($"Пользователь с Id {doctorOnline.Id}\nуспешно зарегистрирован");

                    NavigationService.Navigate(new MainPage(doctorOnline));
                }
                else
                    MessageBox.Show("Пароли не совпадают");
            else
                MessageBox.Show("Заполните все пустые поля");
        }
    }
}
