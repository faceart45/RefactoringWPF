using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using RefactoringWPF.Models;
using RefactoringWPF.Services.Discounts;

namespace RefactoringWPF.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly IDiscountStrategy _discountStrategy;
        private ObservableCollection<Order> _orders = new();
        private Order? _selectedOrder;
        private string _customerName = string.Empty;
        private string _productName = string.Empty;
        private string _quantityText = "1";
        private string _priceText = "100";
        private string _statusMessage = string.Empty;
        private int _nextId = 1;

        public ObservableCollection<Order> Orders
        {
            get => _orders;
            set { _orders = value; OnPropertyChanged(); }
        }

        public Order? SelectedOrder
        {
            get => _selectedOrder;
            set
            {
                _selectedOrder = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanDeleteOrder));
                OnPropertyChanged(nameof(CanShowDiscount));
            }
        }

        public string CustomerName
        {
            get => _customerName;
            set { _customerName = value; OnPropertyChanged(); }
        }

        public string ProductName
        {
            get => _productName;
            set { _productName = value; OnPropertyChanged(); }
        }

        public string QuantityText
        {
            get => _quantityText;
            set { _quantityText = value; OnPropertyChanged(); }
        }

        public string PriceText
        {
            get => _priceText;
            set { _priceText = value; OnPropertyChanged(); }
        }

        public string StatusMessage
        {
            get => _statusMessage;
            set { _statusMessage = value; OnPropertyChanged(); }
        }

        public bool CanDeleteOrder => SelectedOrder != null;
        public bool CanShowDiscount => SelectedOrder != null;

        public decimal TotalRevenue
        {
            get
            {
                decimal total = 0;
                foreach (var order in Orders) total += order.Total;
                return total;
            }
        }

        public RelayCommand AddOrderCommand { get; }
        public RelayCommand DeleteOrderCommand { get; }
        public RelayCommand ShowDiscountCommand { get; }
        public RelayCommand RefreshCommand { get; }

        public MainViewModel()
        {
            _discountStrategy = new QuantityDiscountStrategy();

            AddOrderCommand = new RelayCommand(_ => AddOrder(), _ => CanAddOrder());
            DeleteOrderCommand = new RelayCommand(_ => DeleteOrder(), _ => CanDeleteOrder);
            ShowDiscountCommand = new RelayCommand(_ => ShowDiscount(), _ => CanShowDiscount);
            RefreshCommand = new RelayCommand(_ => RefreshData());

            AddTestData();
        }

        private bool CanAddOrder()
        {
            return !string.IsNullOrWhiteSpace(CustomerName) &&
                   !string.IsNullOrWhiteSpace(ProductName) &&
                   int.TryParse(QuantityText, out var qty) && qty > 0 &&
                   decimal.TryParse(PriceText, out var price) && price > 0;
        }

        private void AddOrder()
        {
            if (!int.TryParse(QuantityText, out int quantity) || quantity <= 0)
            {
                StatusMessage = "Ошибка: количество должно быть больше 0";
                return;
            }

            if (!decimal.TryParse(PriceText, out decimal price) || price <= 0)
            {
                StatusMessage = "Ошибка: цена должна быть больше 0";
                return;
            }

            if (!CustomerName.Contains("@") || !CustomerName.Contains("."))
            {
                StatusMessage = "Ошибка: введите корректный email";
                return;
            }

            var order = new Order
            {
                Id = _nextId++,
                CustomerName = CustomerName.Trim(),
                ProductName = ProductName.Trim(),
                Quantity = quantity,
                Price = price,
                Status = OrderStatus.New,
                CreatedDate = DateTime.Now
            };

            Orders.Add(order);
            StatusMessage = $"Заказ #{order.Id} успешно добавлен";

            QuantityText = "1";
            PriceText = "100";

            OnPropertyChanged(nameof(TotalRevenue));
        }

        private void DeleteOrder()
        {
            if (SelectedOrder == null)
            {
                StatusMessage = "Ошибка: выберите заказ для удаления";
                return;
            }

            var result = MessageBox.Show(
                $"Удалить заказ #{SelectedOrder.Id}?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                Orders.Remove(SelectedOrder);
                StatusMessage = $"Заказ #{SelectedOrder.Id} удалён";
                SelectedOrder = null;
                OnPropertyChanged(nameof(TotalRevenue));
            }
        }

        private void ShowDiscount()
        {
            if (SelectedOrder == null)
            {
                StatusMessage = "Ошибка: выберите заказ";
                return;
            }

            var discountInfo = _discountStrategy.GetDiscountInfo(SelectedOrder);
            var discountPercent = _discountStrategy.CalculateDiscount(SelectedOrder);
            var discountAmount = SelectedOrder.Total * discountPercent;

            MessageBox.Show(
                $"Заказ #{SelectedOrder.Id}\n" +
                $"Сумма: {SelectedOrder.Total:N2} ₽\n" +
                $"{discountInfo}\n" +
                $"Скидка: {discountAmount:N2} ₽",
                "Информация о скидке",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
        }

        private void RefreshData()
        {
            OnPropertyChanged(nameof(TotalRevenue));
            StatusMessage = "Данные обновлены";
        }

        private void AddTestData()
        {
            Orders.Add(new Order
            {
                Id = _nextId++,
                CustomerName = "ivanov@mail.ru",
                ProductName = "Ноутбук",
                Quantity = 2,
                Price = 75000,
                Status = OrderStatus.New,
                CreatedDate = new DateTime(2024, 1, 15)
            });

            Orders.Add(new Order
            {
                Id = _nextId++,
                CustomerName = "petrova@mail.ru",
                ProductName = "Мышь",
                Quantity = 150,
                Price = 500,
                Status = OrderStatus.Paid,
                CreatedDate = new DateTime(2024, 1, 16)
            });
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}