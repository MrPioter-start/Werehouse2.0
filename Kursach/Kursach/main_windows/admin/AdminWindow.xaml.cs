using Kursach.main_windows;
using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace Kursach
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        private string adminUsername;

        public AdminWindow(string username)
        {
            InitializeComponent();
            this.adminUsername = username;

            AdminLabel.Content = $"Добро пожаловать, {adminUsername}";
        }
        private void UserManagement(object sender, RoutedEventArgs e)
        {
            var userManagement = new UserManagementWindow(adminUsername);
            userManagement.Show();
        }

        private void AdminCode(object sender, RoutedEventArgs e)
        {
            var codeManagementWindow = new AdminCodeManagementWindow(adminUsername);
            codeManagementWindow.Show();
        }
    }
}
