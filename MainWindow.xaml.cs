using System.Windows;
using System.Text.Json;
using System.IO;

namespace Data_Binding_Mironov
{
    public partial class MainWindow : Window
    {
        //генератор случайных чисел для создания ID
        Random _idGenerator = new Random();

        //экземпляры классов для работы с данными
        Doctor _loggedInDoctor = new Doctor();                                  //текущий авторизованный доктор
        Doctor _newDoctorRegistration = new Doctor();                           //новый доктор для регистрации
        PatientRecord _currentPatient = new PatientRecord();                    //текущий пациент
        PatientRecord _newPatientRegistration = new PatientRecord();            //новый пациент для регистрации
        PatientRecord _patientBeingEdited = new PatientRecord();                //пациент для редактирования
        Statistics _systemStats = new Statistics();                             //статистика системы

        public MainWindow()
        {
            InitializeComponent();

            //установка DataContext для привязки данных
            DataContext = new
            {
                Doctor = _loggedInDoctor,
                NewDoctor = _newDoctorRegistration,
                Patient = _currentPatient,
                NewPatient = _newPatientRegistration,
                EditedPatient = _patientBeingEdited,
                Stats = _systemStats
            };

            //первоначальное обновление статистики
            _systemStats.UpdateCounts();
        }

        //обработчик регистрации нового доктора
        private void RegisterNewDoctor(object sender, RoutedEventArgs e)
        {
            //проверка заполнения всех полей и совпадения паролей
            if (_newDoctorRegistration.FirstName != "" && _newDoctorRegistration.Surname != "" &&
                _newDoctorRegistration.Patronymic != "" && _newDoctorRegistration.Specialty != "" &&
                _newDoctorRegistration.SecretCode != "" && (PasswordConfirmField.Text == _newDoctorRegistration.SecretCode))
            {
                string generatedId = "99999";
                string filePath = "null";
                int minValue = 10000;
                int maxValue = 99999;

                //генерация уникального ID для доктора
                List<string> usedIds = new List<string>();
                for (int i = minValue; i < maxValue; i++)
                {
                    do
                    {
                        generatedId = _idGenerator.Next(minValue, maxValue).ToString();
                    }
                    while (usedIds.Contains(generatedId));

                    //проверка существования файла с таким ID
                    if (File.Exists(filePath))
                    {
                        usedIds.Add(generatedId);
                    }
                    else
                    {
                        filePath = "D_" + generatedId + ".json";
                        break;
                    }
                }

                //сохранение данных доктора в JSON
                string jsonData = JsonSerializer.Serialize(_newDoctorRegistration);
                File.WriteAllText(filePath, jsonData);

                //очистка полей после регистрации
                _newDoctorRegistration.ResetFields();
                PasswordConfirmField.Text = string.Empty;
                _systemStats.UpdateCounts();

                MessageBox.Show($"Доктор {_newDoctorRegistration.FirstName} успешно зарегистрирован с ID: {generatedId}");
            }
        }

        //обработчик авторизации пользователя
        private void AuthenticateUser(object sender, RoutedEventArgs e)
        {
            //проверка заполнения полей логина и пароля
            if (UserIdField.Text != "" && UserPasswordField.Password != "")
            {
                string targetFile = "D_" + UserIdField.Text + ".json";

                //проверка существования файла пользователя
                if (File.Exists(targetFile))
                {
                    //чтение и получение данных пользователя
                    string fileContent = File.ReadAllText(targetFile);
                    Doctor fileData = JsonSerializer.Deserialize<Doctor>(fileContent);

                    //проверка пароля
                    if (fileData.SecretCode == UserPasswordField.Password)
                    {
                        //установка данных текущего пользователя
                        _loggedInDoctor.FirstName = fileData.FirstName;
                        _loggedInDoctor.Patronymic = fileData.Patronymic;
                        _loggedInDoctor.Surname = fileData.Surname;
                        _loggedInDoctor.Specialty = fileData.Specialty;
                        _loggedInDoctor.SecretCode = fileData.SecretCode;

                        MessageBox.Show($"Добро пожаловать, {_loggedInDoctor.FirstName}!");

                        //очистка полей ввода
                        UserIdField.Text = string.Empty;
                        UserPasswordField.Password = string.Empty;
                    }
                }
                else
                    MessageBox.Show("Пользователь не найден");
            }
        }

        //обработчик регистрации нового пациента
        private void RegisterNewPatient(object sender, RoutedEventArgs e)
        {
            //проверка заполнения обязательных полей
            if (_newPatientRegistration.GivenName != "" && _newPatientRegistration.FamilyName != "" &&
                _newPatientRegistration.FathersName != "" && _newPatientRegistration.DateOfBirth != null)
            {
                string patientId = "999999";
                string patientFile = "null";

                int lowerBound = 1000000;
                int upperBound = 9999999;

                //генерация уникального ID для пациента
                List<string> existingIds = new List<string>();
                for (int i = lowerBound; i < upperBound; i++)
                {
                    do
                    {
                        patientId = _idGenerator.Next(lowerBound, upperBound).ToString();
                    }
                    while (existingIds.Contains(patientId));

                    //проверка существования файла с таким ID
                    if (File.Exists(patientFile))
                    {
                        existingIds.Add(patientId);
                    }
                    else
                    {
                        patientFile = "P_" + patientId + ".json";
                        break;
                    }
                }

                //установка ID и сохранение данных пациента
                _newPatientRegistration.RecordId = patientId;
                string patientJson = JsonSerializer.Serialize(_newPatientRegistration);
                File.WriteAllText(patientFile, patientJson);

                //очистка полей и обновление статистики
                _newPatientRegistration.ClearData();
                _systemStats.UpdateCounts();

                MessageBox.Show($"Пациент {_newPatientRegistration.GivenName} зарегистрирован с ID: {_newPatientRegistration.RecordId}");
            }
            else
                MessageBox.Show("Заполните все обязательные поля");
        }

        //обработчик поиска пациента по ID
        private void FindPatientRecord(object sender, RoutedEventArgs e)
        {
            //проверка наличия ID и авторизации доктора
            if (SearchIdField.Text != "" && _loggedInDoctor.FirstName != "")
            {
                string searchFile = "P_" + SearchIdField.Text + ".json";

                //проверка существования файла пациента
                if (File.Exists(searchFile))
                {
                    //чтение и получение данных пациента
                    string patientData = File.ReadAllText(searchFile);
                    PatientRecord foundPatient = JsonSerializer.Deserialize<PatientRecord>(patientData);

                    //копирование данных в текущего пациента
                    _currentPatient.RecordId = foundPatient.RecordId;
                    _currentPatient.GivenName = foundPatient.GivenName;
                    _currentPatient.FathersName = foundPatient.FathersName;
                    _currentPatient.FamilyName = foundPatient.FamilyName;
                    _currentPatient.DateOfBirth = foundPatient.DateOfBirth;
                    _currentPatient.LastVisitDate = foundPatient.LastVisitDate;
                    _currentPatient.AttendingDoctor = foundPatient.AttendingDoctor;
                    _currentPatient.MedicalCondition = foundPatient.MedicalCondition;
                    _currentPatient.TreatmentPlan = foundPatient.TreatmentPlan;

                    //копирование данных в пациента для редактирования
                    _patientBeingEdited.RecordId = foundPatient.RecordId;
                    _patientBeingEdited.GivenName = foundPatient.GivenName;
                    _patientBeingEdited.FathersName = foundPatient.FathersName;
                    _patientBeingEdited.FamilyName = foundPatient.FamilyName;
                    _patientBeingEdited.DateOfBirth = foundPatient.DateOfBirth;
                    _patientBeingEdited.LastVisitDate = foundPatient.LastVisitDate;
                    _patientBeingEdited.AttendingDoctor = foundPatient.AttendingDoctor;
                    _patientBeingEdited.MedicalCondition = foundPatient.MedicalCondition;
                    _patientBeingEdited.TreatmentPlan = foundPatient.TreatmentPlan;

                    MessageBox.Show($"Найден пациент: {_currentPatient.GivenName}");
                }
                else
                    MessageBox.Show("Пациент с указанным ID не найден");
            }
            else
                MessageBox.Show("Введите ID пациента и авторизуйтесь");
        }

        //обработчик завершения приема пациента
        private void CompleteAppointment(object sender, RoutedEventArgs e)
        {
            //проверка заполнения всех полей приема
            if (_currentPatient.LastVisitDate != "" && _currentPatient.MedicalCondition != "" && _currentPatient.TreatmentPlan != "")
            {
                //установка текущего доктора как лечащего врача
                _currentPatient.AttendingDoctor = _loggedInDoctor;

                //сохранение обновленных данных пациента
                string appointmentData = JsonSerializer.Serialize(_currentPatient);
                File.WriteAllText("P_" + _currentPatient.RecordId + ".json", appointmentData);

                //очистка данных пациентов
                _currentPatient.ClearData();
                _patientBeingEdited.ClearData();

                MessageBox.Show($"Приём пациента {_currentPatient.GivenName} завершён");
            }
            else
                MessageBox.Show("Заполните все поля приёма");
        }

        //обработчик сохранения изменений данных пациента
        private void SavePatientChanges(object sender, RoutedEventArgs e)
        {
            //копирование данных из редактируемого пациента в текущего
            _currentPatient.RecordId = _patientBeingEdited.RecordId;
            _currentPatient.GivenName = _patientBeingEdited.GivenName;
            _currentPatient.FathersName = _patientBeingEdited.FathersName;
            _currentPatient.FamilyName = _patientBeingEdited.FamilyName;
            _currentPatient.DateOfBirth = _patientBeingEdited.DateOfBirth;
            _currentPatient.LastVisitDate = _patientBeingEdited.LastVisitDate;
            _currentPatient.AttendingDoctor = _patientBeingEdited.AttendingDoctor;
            _currentPatient.MedicalCondition = _patientBeingEdited.MedicalCondition;
            _currentPatient.TreatmentPlan = _patientBeingEdited.TreatmentPlan;

            //сохранение изменений в файл
            string updatedData = JsonSerializer.Serialize(_currentPatient);
            File.WriteAllText("P_" + _currentPatient.RecordId + ".json", updatedData);

            MessageBox.Show($"Данные пациента {_currentPatient.GivenName} обновлены");
        }
    }
}