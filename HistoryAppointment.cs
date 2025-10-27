using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.Json;
using System.IO;

namespace Data_Binding_Mironov
{
    public class HistoryAppointment : INotifyPropertyChanged
    {
        public HistoryAppointment(string _date, int _doctorId, string _diagnosis, string _recomendations)
        {
            Date = _date;
            DoctorId = _doctorId;
            Diagnosis = _diagnosis;
            Recomendations = _recomendations;
        }

        public HistoryAppointment()
        {
        }

        private string _date = "";
        public string Date
        {
            get => _date;
            set
            {
                if (_date != value)
                {
                    _date = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private int _doctorId;
        public int DoctorId
        {
            get => _doctorId;
            set
            {
                if (_doctorId != value)
                {
                    _doctorId = value;
                    NotifyPropertyChanged();
                }
            }
        }

        private string _diagnosis = "";
        public string Diagnosis
        {
            get => _diagnosis;
            set
            {
                if (_diagnosis != value)
                {
                    _diagnosis = value;
                    NotifyPropertyChanged();
                }
            }
        }
        private string _recomendation = "";
        public string Recomendations
        {
            get => _recomendation;
            set
            {
                if (_recomendation != value)
                {
                    _recomendation = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string GetName
        {
            get
            {
                string jsonString = File.ReadAllText("D_" + DoctorId.ToString() + ".json");
                Doctor doc = JsonSerializer.Deserialize<Doctor>(jsonString);
                string name = $"{doc.LastName} {doc.Name} {doc.MiddleName}";
                return name;
            }
            set { }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
