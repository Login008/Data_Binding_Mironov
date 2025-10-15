using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Data_Binding_Mironov
{
    public class Doctor : INotifyPropertyChanged
    {
        //приватные поля для хранения данных
        private string _firstName = "";
        private string _surname = "";
        private string _patronymic = "";
        private string _specialty = "";
        private string _secretCode = "";

        //свойства с уведомлением об изменении
        public string FirstName
        {
            get => _firstName;
            set
            {
                if (_firstName != value)
                {
                    _firstName = value;
                    NotifyPropertyChanged(); //уведомляем систему об изменении
                }
            }
        }

        public string Surname
        {
            get => _surname;
            set
            {
                if (_surname != value)
                {
                    _surname = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Patronymic
        {
            get => _patronymic;
            set
            {
                if (_patronymic != value)
                {
                    _patronymic = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Specialty
        {
            get => _specialty;
            set
            {
                if (_specialty != value)
                {
                    _specialty = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string SecretCode
        {
            get => _secretCode;
            set
            {
                if (_secretCode != value)
                {
                    _secretCode = value;
                    NotifyPropertyChanged();
                }
            }
        }

        //событие для реализации INotifyPropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        //метод для уведомления об изменении свойства
        protected void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //метод для очистки всех полей
        public void ResetFields()
        {
            FirstName = string.Empty;
            Surname = string.Empty;
            Patronymic = string.Empty;
            Specialty = string.Empty;
            SecretCode = string.Empty;
        }
    }
}
