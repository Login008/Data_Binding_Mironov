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
    /// Логика взаимодействия для ListPage.xaml
    /// </summary>
    public partial class ListPage : Page
    {
        public ObservableCollection<User> Users { get; set; } = new();
        public User? SelectedUser { get; set; }
        public ListPage()
        {
            Users.Add(new User() { Id = 1, Name = "Иван", Email = "ivan@mail.ru", Age = 17 });
            Users.Add(new User() { Id = 2, Name = "Пётр", Email = "petr@mail.ru", Age = 18 });
            Users.Add(new User() { Id = 3, Name = "Дмитрий", Email = "dmitriy@mail.ru", Age = 16 });
            Users.Add(new User() { Id = 4, Name = "Фёдор", Email = "fedor@mail.ru", Age = 20 });
            Users.Add(new User() { Id = 5, Name = "Максим", Email = "maksim@mail.ru", Age = 19 });
            Users.Add(new User() { Id = 6, Name = "Александр", Email = "alexander@mail.ru", Age = 17 });
            InitializeComponent();
            DataContext = this;
        }

        private void DeleteItem_Click(object senver, RoutedEventArgs e)
        {
            if (SelectedUser == null)
                MessageBox.Show("Пользователь не выбран");
            else
            {
                bool confirm = MessageBox.Show(
                    "Вы действительно хотите удалить запись?",
                    "Подтверждение удаления",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question
                    ) == MessageBoxResult.Yes;

                if (confirm)
                {
                    Users.Remove(SelectedUser);
                }
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new UserFormPage(Users, null));
        }

        private void ListView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (SelectedUser == null)
            {
                MessageBox.Show("Не выбран элемент списка");
                return;
            }
            NavigationService.Navigate(new UserFormPage(Users, SelectedUser));
        }
    }
}
