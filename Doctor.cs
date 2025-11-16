using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Data_Binding_Mironov
{
    public class Doctor : INotifyPropertyChanged
    {
        public Doctor(int id, string name, string lastname, string middlename, string specialisation, string password)
        {
            Id = id;
            Name = name;
            LastName = lastname;
            MiddleName = middlename;
            Specialisation = specialisation;
            Password = password;
        }

        public Doctor()
        {
        }

        private int _id = 0;
        public int Id
        {
            get => _id;
            set
            {
                if (_id != value)
                {
                    _id = value;
                    NotifyPropertyChanged();
                }
            }
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
        private string _specialisation = "";
        public string Specialisation
        {
            get => _specialisation;
            set
            {
                if (_specialisation != value)
                {
                    _specialisation = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _password = "";
        public string Password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _doubleCheck = "";
        public string DoubleCheck
        {
            get => _doubleCheck;
            set
            {
                if (_doubleCheck != value)
                {
                    _doubleCheck = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
