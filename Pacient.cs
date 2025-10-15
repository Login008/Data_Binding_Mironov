using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace Data_Binding_Mironov
{
    public class PatientRecord : INotifyPropertyChanged
    {
        //приватные поля для хранения данных пациента
        private string _recordId = "";
        private string _givenName = "";
        private string _familyName = "";
        private string _fathersName = "";
        private string _dateOfBirth = "";
        private string _lastVisitDate = "";
        private Doctor _attendingDoctor = null;
        private string _medicalCondition = "";
        private string _treatmentPlan = "";

        //свойство ID пациента
        public string RecordId
        {
            get => _recordId;
            set => _recordId = value;
        }

        //свойства с уведомлением об изменении
        public string GivenName
        {
            get => _givenName;
            set
            {
                if (_givenName != value)
                {
                    _givenName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string FamilyName
        {
            get => _familyName;
            set
            {
                if (_familyName != value)
                {
                    _familyName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string FathersName
        {
            get => _fathersName;
            set
            {
                if (_fathersName != value)
                {
                    _fathersName = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string DateOfBirth
        {
            get => _dateOfBirth;
            set
            {
                if (_dateOfBirth != value)
                {
                    _dateOfBirth = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string LastVisitDate
        {
            get => _lastVisitDate;
            set
            {
                if (_lastVisitDate != value)
                {
                    _lastVisitDate = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public Doctor AttendingDoctor
        {
            get => _attendingDoctor;
            set
            {
                if (_attendingDoctor != value)
                {
                    _attendingDoctor = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string MedicalCondition
        {
            get => _medicalCondition;
            set
            {
                if (_medicalCondition != value)
                {
                    _medicalCondition = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string TreatmentPlan
        {
            get => _treatmentPlan;
            set
            {
                if (_treatmentPlan != value)
                {
                    _treatmentPlan = value;
                    NotifyPropertyChanged();
                }
            }
        }

        //cобытие для уведомления об изменении свойств
        public event PropertyChangedEventHandler? PropertyChanged;

        //vетод для вызова события PropertyChanged
        protected void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        //метод для очистки всех данных пациента
        public void ClearData()
        {
            GivenName = string.Empty;
            FamilyName = string.Empty;
            FathersName = string.Empty;
            DateOfBirth = string.Empty;
            AttendingDoctor = null;
            LastVisitDate = string.Empty;
            MedicalCondition = string.Empty;
            TreatmentPlan = string.Empty;
        }
    }
}
