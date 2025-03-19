using Kursach.Database;
using Kursach.main_windows.admin;
using System.Data;
using System.Windows;

namespace Kursach.main_windows
{
    public partial class ManagerWindow : Window
    {
        private string adminUsername;

        public ManagerWindow(string managerUsername)
        {
            InitializeComponent();
            this.adminUsername = Queries.GetAdminForManager(managerUsername); // Получаем администратора [[1]]
            LoadCashAmount();
            LoadSalesHistory();
        }

        private void LoadCashAmount()
        {
            decimal amount = Queries.GetCurrentCashAmount(adminUsername);
            CashAmountTextBlock.Text = $"{amount:F2} ₽";
        }

        private void LoadSalesHistory()
        {
            DataTable salesTable = Queries.GetSalesHistory(adminUsername);
            SalesHistoryDataGrid.ItemsSource = salesTable.DefaultView;
        }

        private void OpenSalesMenu_Click(object sender, RoutedEventArgs e)
        {
            var salesWindow = new SalesWindow(adminUsername); // Передаем администратора [[3]]
            salesWindow.ShowDialog();
        }

        private void OpenCashManagement_Click(object sender, RoutedEventArgs e)
        {
            var cashWindow = new CashManagementWindow(adminUsername); // Передаем администратора
            cashWindow.Closed += (s, ev) => LoadCashAmount();
            cashWindow.ShowDialog();
        }
    }
}