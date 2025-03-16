using Kursach.Database;
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

namespace Kursach.login_registration
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = UsernameTextBox.Text.Trim();
            string password = PasswordBox.Password;

            string storedHashedPassword = Queries.GetUserPasswordHash(username);

            if (string.IsNullOrEmpty(storedHashedPassword))
            {
                MessageBox.Show("Пользователь не найден.", "Ошибка");
                return;
            }

            bool isPasswordValid = PasswordHelper.VerifyPassword(password, storedHashedPassword);

            if (isPasswordValid)
            {
                MessageBox.Show("Авторизация успешна!", "Успех");

                string userRole = Queries.GetUserRole(username);

                switch (userRole)
                {
                    case "Администратор":
                        var adminDashboard = new AdminWindow(username);
                        adminDashboard.Show();
                        this.Close();
                        break;
                    case "Менеджер":
                        var managerDashboard = new ManagerWindow();
                        managerDashboard.Show();
                        this.Close();
                        break;
                    case "Пользователь":
                        var userDashboard = new UserWindow();
                        userDashboard.Show();
                        this.Close();
                        break;
                    default:
                        MessageBox.Show("Роль пользователя не определена.", "Ошибка");
                        break;
                }
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль.", "Ошибка");
            }
        }

        private void GoToRegister_Click(object sender, RoutedEventArgs e)
        {
            var registerWindow = new RegisterWindow();
            registerWindow.Show();
            this.Hide();
        }

        private string HashPassword(string password)
        {
            return PasswordHelper.HashPassword(password);
        }
    }
}
