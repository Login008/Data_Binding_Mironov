using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace Data_Binding_Mironov.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public static ObservableCollection<Pacient> pacientList { get; set; } = new ObservableCollection<Pacient>();
        public Pacient? SelectedPacient { get; set; }
        public Doctor Doctor { get; set; }

        public MainPage(Doctor _doctor)
        {
            InitializeComponent();
            DataContext = this;
            Doctor = _doctor;
            UpdateList();
        }

        public static void UpdateList()
        {
            List<string> allPacients = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.json", SearchOption.TopDirectoryOnly)
                            .Where(file => Path.GetFileName(file).Contains("P_")).ToList();

            pacientList.Clear();

            foreach (string fileName in allPacients)
            {
                string jsonString = File.ReadAllText(fileName);
                Pacient patient = JsonSerializer.Deserialize<Pacient>(jsonString);

                pacientList.Add(patient);
            }
        }

        private void CreatePacient(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new CreatePage());
        }

        private void ChangePacient(object sender, RoutedEventArgs e)
        {
            if (SelectedPacient != null)
            {
                NavigationService.Navigate(new ChangePatientPage(SelectedPacient));
            }
            else
                MessageBox.Show("Выберите пациента");
        }

        private void Accepting(object sender, RoutedEventArgs e)
        {
            if (SelectedPacient != null)
            {
                NavigationService.Navigate(new AcceptingPatientPage(SelectedPacient, Doctor));
            }
            else
                MessageBox.Show("Выберите пациента");
        }
    }
}
