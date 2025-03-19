using Kursach.Database;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace Kursach.main_windows.admin
{
    /// <summary>
    /// Логика взаимодействия для AddProductWindow.xaml
    /// </summary>
    public partial class AddProductWindow : Window
    {
        private string createdBy;

        public AddProductWindow(string adminUsername)
        {
            InitializeComponent();
            this.createdBy = adminUsername;
            LoadCategories();
        }

        private void LoadCategories()
        {
            DataTable categoriesTable = Queries.GetCategories(createdBy);
            CategoryComboBox.ItemsSource = categoriesTable.DefaultView;
            CategoryComboBox.DisplayMemberPath = "CategoryName";
            CategoryComboBox.SelectedValuePath = "CategoryID";
        }

        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            string name = NameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Введите название товара.", "Ошибка");
                return;
            }

            if (CategoryComboBox.SelectedValue == null)
            {
                MessageBox.Show("Выберите категорию.", "Ошибка");
                return;
            }
            int categoryID = (int)CategoryComboBox.SelectedValue;

            if (!decimal.TryParse(PriceTextBox.Text, out decimal price) || price <= 0)
            {
                MessageBox.Show("Введите корректную цену.", "Ошибка");
                return;
            }

            if (!int.TryParse(QuantityTextBox.Text, out int quantity) || quantity < 0)
            {
                MessageBox.Show("Введите корректное количество.", "Ошибка");
                return;
            }

            try
            {
                Queries.AddProduct(name, categoryID, price, quantity, createdBy);
                MessageBox.Show("Товар успешно добавлен.", "Успех");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при добавлении товара: {ex.Message}", "Ошибка");
            }
        }
    }
}
