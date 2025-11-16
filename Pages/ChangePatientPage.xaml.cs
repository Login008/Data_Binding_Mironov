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
            copy = new Pacient(_pacient.Id, _pacient.Name, _pacient.LastName, _pacient.MiddleName, _pacient.Birthday, _pacient.LastAppointment, _pacient.NumberPhone);
            DataContext = copy;
            InitializeComponent();
        }

        private void Change(object sender, RoutedEventArgs e)
        {
            if (copy.LastName != "" && copy.Name != "" && copy.MiddleName != "" && copy.NumberPhone != "")
            {
                if (DateP1.SelectedDate != null && DateP1.SelectedDate < DateTime.Today)
                {
                    DateTime.TryParse(copy.Birthday, out DateTime result);
                    if (result.Date <= DateTime.Today && copy.Birthday != "")
                    {
                        if (NumberBox.Text.Count() == 10 && NumberBox.Text.All(char.IsDigit))
                        {
                            string jsonString = JsonSerializer.Serialize(copy);
                            File.WriteAllText($"P_{copy.Id}.json", jsonString);

                            _pacient.LastName = copy.LastName;
                            _pacient.Name = copy.Name;
                            _pacient.MiddleName = copy.MiddleName;
                            _pacient.Birthday = copy.Birthday;
                            _pacient.LastAppointment = copy.LastAppointment;
                            _pacient.NumberPhone = copy.NumberPhone;

                            NavigationService.GoBack();
                        }
                        else
                            MessageBox.Show("В номере телефона должно быть только 10 цифр");
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
    }
}
