using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.IO;

namespace Data_Binding_Mironov.Pages
{
    /// <summary>
    /// Логика взаимодействия для AcceptingPatientPage.xaml
    /// </summary>
    public partial class AcceptingPatientPage : Page
    {
        Doctor doctor;
        public Pacient pacient { get; set; }
        public HistoryAppointment? copyHistory { get; set; } = new HistoryAppointment();

        public AcceptingPatientPage(Pacient _pacient, Doctor _doctor)
        {
            InitializeComponent();
            doctor = _doctor;
            pacient = _pacient;
            DataContext = this;
        }

        private void Addiction(object sender, RoutedEventArgs e)
        {
            if (copyHistory.Recomendations != "" && copyHistory.Diagnosis != "")
            {
                copyHistory.DoctorId = doctor.Id;
                pacient.LastAppointment.Add(copyHistory);

                string jsonString = JsonSerializer.Serialize(pacient);
                File.WriteAllText($"P_{pacient.Id}.json", jsonString);

                copyHistory = new HistoryAppointment();
            }
            else
                MessageBox.Show("Заполните пустые поля");
        }

        private void Back(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void ListView_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
