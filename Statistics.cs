using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace Data_Binding_Mironov
{
    public class Statistics : INotifyPropertyChanged
    {
        //поля для хранения количества докторов и пациентов
        private int _doctorCount = 0;
        private int _patientCount = 0;

        //свойство с уведомлением об изменении для количества докторов
        public int DoctorCount
        {
            get => _doctorCount;
            set
            {
                if (_doctorCount != value)
                {
                    _doctorCount = value;
                    NotifyPropertyChanged(); //уведомление об изменении
                }
            }
        }

        //свойство с уведомлением об изменении для количества пациентов
        public int PatientCount
        {
            get => _patientCount;
            set
            {
                if (_patientCount != value)
                {
                    _patientCount = value;
                    NotifyPropertyChanged(); //уведомление об изменении
                }
            }
        }

        //метод для обновления статистики из файловой системы
        public void UpdateCounts()
        {
            //считаем JSON-файлы докторов
            DoctorCount = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.json", SearchOption.TopDirectoryOnly)
                            .Where(file => Path.GetFileName(file).StartsWith("D_")).Count();

            //считаем JSON-файлы пациентов
            PatientCount = Directory.GetFiles(Directory.GetCurrentDirectory(), "*.json", SearchOption.TopDirectoryOnly)
                            .Where(file => Path.GetFileName(file).StartsWith("P_")).Count();
        }

        //событие для уведомления об изменении свойств
        public event PropertyChangedEventHandler? PropertyChanged;

        //метод для вызова события PropertyChanged
        protected void NotifyPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}