using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;
using System.IO;
using System.Globalization;
using System.Text.RegularExpressions;

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
            if (copyHistory.Recomendations != string.Empty && copyHistory.Diagnosis != string.Empty)
            {
                if (DateP.SelectedDate != null)
                {
                    DateTime.TryParse(copyHistory.Date, out DateTime result);
                    if (result.Date <= DateTime.Today && copyHistory.Date != "")
                    {
                        copyHistory.DoctorId = doctor.Id;
                        pacient.LastAppointment.Add(copyHistory);

                        string jsonString = JsonSerializer.Serialize(pacient);
                        File.WriteAllText($"P_{pacient.Id}.json", jsonString);

                        copyHistory = new HistoryAppointment();
                    }
                    else
                        MessageBox.Show("Дата не может быть будущей");
                }
                else
                    MessageBox.Show("Выберите дату");
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
