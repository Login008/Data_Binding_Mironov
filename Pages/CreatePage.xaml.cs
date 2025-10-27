using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.IO;

namespace Data_Binding_Mironov.Pages
{
    /// <summary>
    /// Логика взаимодействия для CreatePage.xaml
    /// </summary>
    public partial class CreatePage : Page
    {
        Random rand = new Random();
        Pacient chosenPacient = new Pacient();
        public CreatePage()
        {
            InitializeComponent();
            DataContext = chosenPacient;
        }

        private void Register(object sender, RoutedEventArgs e)
        {
            if (chosenPacient.Name != "" && chosenPacient.LastName != "" && chosenPacient.MiddleName != "" && chosenPacient.Birthday != "")
            {
                int min = 1000000;
                int max = 9999999;

                string randId = "999999";
                string fName = "null";

                List<string> Excludes = new List<string>();
                for (int i = min; i < max; i++)
                {
                    do
                    {
                        randId = rand.Next(min, max).ToString();
                    } while (Excludes.Contains(randId));

                    if (File.Exists(fName))
                    {
                        Excludes.Add(randId);
                    }
                    else
                    {
                        fName = "P_" + randId + ".json";
                        break;
                    }
                }
                chosenPacient.Id = randId;

                string jsonString = JsonSerializer.Serialize(chosenPacient);
                File.WriteAllText(fName, jsonString);

                MainPage.pacientList.Add(chosenPacient);
                MainWindow.stat.UpdateCounts();
                MainPage.UpdateList();

                NavigationService.GoBack();
            }
            else
                MessageBox.Show("Заполните пустые поля");
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
