using RefactoringWPF.Models;
using RefactoringWPF.Services;
using RefactoringWPF.Utils;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace RefactoringWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        OrderService service = new OrderService();
        public MainWindow()
        {
            InitializeComponent();
            RefreshGrid();
        }

        private void RefreshGrid()
        {
            dgOrders.ItemsSource = null;
            dgOrders.ItemsSource = OrderService.orders;
            lblTotal.Text = "Итого " + Helper.FormatMoney(service.GetTotalRevenue()); 
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int qty = int.Parse(txtQty.Text);
            decimal price = decimal.Parse(txtPrice.Text);

            if (Helper.IsValidEmail(txtCustomer.Text))
            {
                service.AddOrder(txtCustomer.Text, txtProduct.Text, qty, price);
            }
            else
            {
                MessageBox.Show("Email неверный!");
            }

            RefreshGrid();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            RefreshGrid();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var selected = dgOrders.SelectedItems as Order;
            service.DeleteOrder(selected.Id);
            RefreshGrid();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            var selected = dgOrders.SelectedItems as Order;
            string discount = service.GetDiscount(selected.Quantity, selected.GetTotal());
            MessageBox.Show("Скидка " + discount);
        }
    }
}