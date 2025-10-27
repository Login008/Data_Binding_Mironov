using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Data_Binding_Mironov
{
    public class Pacient : INotifyPropertyChanged
    {
        //конструктор для более быстрого создания
        public Pacient(string name, string lastName, string middleName, string birthday, ObservableCollection<HistoryAppointment> lastAppointment)
        {
            Name = name;
            LastName = lastName;
            MiddleName = middleName;
            Birthday = birthday;
            LastAppointment = lastAppointment;
        }

        public Pacient()
        {
        }

        //приватные свойства для хранения данных пациента
        private string _id = "";
        public string Id
        {
            get => _id;
            set => _id = value;
        }
        private string _name = "";
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _lastname = "";
        public string LastName
        {
            get => _lastname;
            set
            {
                if (_lastname != value)
                {
                    _lastname = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _middlename = "";
        public string MiddleName
        {
            get => _middlename;
            set
            {
                if (_middlename != value)
                {
                    _middlename = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _birthday = "";
        public string Birthday
        {
            get => _birthday;
            set
            {
                if (_birthday != value)
                {
                    _birthday = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private ObservableCollection<HistoryAppointment> _lastAppointment = new ObservableCollection<HistoryAppointment>();
        public ObservableCollection<HistoryAppointment> LastAppointment
        {
            get => _lastAppointment;
            set
            {
                if (_lastAppointment != value)
                {
                    _lastAppointment = value;
                    NotifyPropertyChanged();
                }
            }
        }

        //cобытие для уведомления об изменении свойств
        public event PropertyChangedEventHandler? PropertyChanged;

        //метод для вызова события PropertyChanged
        protected void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
