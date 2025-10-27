using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.IO;

namespace Data_Binding_Mironov.Pages
{
    /// <summary>
    /// Логика взаимодействия для ChangePatientPage.xaml
    /// </summary>
    public partial class ChangePatientPage : Page
    {
        Pacient _pacient;
        Pacient copy;

        public ChangePatientPage(Pacient pacient)
        {
            _pacient = pacient;
            copy = new Pacient(_pacient.Name, _pacient.LastName, _pacient.MiddleName, _pacient.Birthday, _pacient.LastAppointment);
            DataContext = copy;
            InitializeComponent();
        }

        private void Change(object sender, RoutedEventArgs e)
        {
            string jsonString = JsonSerializer.Serialize(copy);
            File.WriteAllText($"P_{_pacient.Id}.json", jsonString);

            _pacient.LastName = copy.LastName;
            _pacient.Name = copy.Name;
            _pacient.MiddleName = copy.MiddleName;
            _pacient.Birthday = copy.Birthday;
            _pacient.LastAppointment = copy.LastAppointment;

            NavigationService.GoBack();
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
