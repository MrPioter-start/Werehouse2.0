using Kursach.Database;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
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
using System.Xml.Linq;

namespace Kursach.main_windows.admin
{
    /// <summary>
    /// Логика взаимодействия для EditProductWindow.xaml
    /// </summary>
    public partial class EditProductWindow : Window
    {
        private string originalName;
        private string createdBy;

        public EditProductWindow(string name, string category, decimal price, int quantity, string adminUsername)
        {
            InitializeComponent();
            this.originalName = name;
            this.createdBy = adminUsername;

            NameTextBox.Text = name;
            PriceTextBox.Text = price.ToString();
            QuantityTextBox.Text = quantity.ToString();

            LoadCategories(category);
        }
        private void LoadCategories(string selectedCategory)
        {
            DataTable categoriesTable = Queries.GetCategories(createdBy);
            CategoryComboBox.ItemsSource = categoriesTable.DefaultView;
            CategoryComboBox.DisplayMemberPath = "CategoryName";
            CategoryComboBox.SelectedValuePath = "CategoryID";

            // Выбор текущей категории
            if (!string.IsNullOrEmpty(selectedCategory))
            {
                foreach (DataRowView row in categoriesTable.DefaultView)
                {
                    if (row["CategoryName"].ToString() == selectedCategory)
                    {
                        CategoryComboBox.SelectedItem = row;
                        break;
                    }
                }
            }
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            string newName = NameTextBox.Text.Trim();
            int categoryID = (int)CategoryComboBox.SelectedValue;
            if (!decimal.TryParse(PriceTextBox.Text, out decimal newPrice) || newPrice <= 0)
            {
                MessageBox.Show("Введите корректную цену.", "Ошибка");
                return;
            }
            if (!int.TryParse(QuantityTextBox.Text, out int newQuantity) || newQuantity < 0)
            {
                MessageBox.Show("Введите корректное количество.", "Ошибка");
                return;
            }

            try
            {
                Queries.UpdateProduct(originalName, newName, categoryID, newPrice, newQuantity, createdBy);
                MessageBox.Show("Товар успешно обновлен.", "Успех");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при обновлении товара: {ex.Message}", "Ошибка");
            }
        }
    }
}
