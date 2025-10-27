using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Example_Data_Binding_Collections_Mironov.Pages
{
    /// <summary>
    /// Логика взаимодействия для UserFormPage.xaml
    /// </summary>
    public partial class UserFormPage : Page
    {
        private User _user;
        private ObservableCollection<User> Users;
        private User? _originalUser;
        public UserFormPage(ObservableCollection<User> _users, User? user = null)
        {
            _originalUser = user;

            _user = user == null ? new User(): new User
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                Age = user.Age
            };

            Users = _users;
            InitializeComponent();
            DataContext = _user;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_originalUser == null)
            {
                Users.Add(_user);
                MessageBox.Show("Запись успешно добавлена");
                BackButton_Click(sender, e);
                return;
            }
            _originalUser.Id = _user.Id;
            _originalUser.Name = _user.Name;
            _originalUser.Email = _user.Email;
            _originalUser.Age = _user.Age;
            BackButton_Click(sender, e);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
