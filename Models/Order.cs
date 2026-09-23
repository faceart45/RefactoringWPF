using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace RefactoringWPF.Models
{
    public class Order : INotifyPropertyChanged
    {
        private int _id;
        private string _customerName = string.Empty;
        private string _productName = string.Empty;
        private int _quantity;
        private decimal _price;
        private OrderStatus _status;
        private DateTime _createdDate;

        public int Id
        {
            get => _id;
            set { _id = value; OnPropertyChanged(); }
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

        public int Quantity
        {
            get => _quantity;
            set
            {
                _quantity = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Total));
                OnPropertyChanged(nameof(DiscountInfo));
            }
        }

        public decimal Price
        {
            get => _price;
            set
            {
                _price = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(Total));
                OnPropertyChanged(nameof(DiscountInfo));
            }
        }

        public OrderStatus Status
        {
            get => _status;
            set { _status = value; OnPropertyChanged(); }
        }

        public DateTime CreatedDate
        {
            get => _createdDate;
            set { _createdDate = value; OnPropertyChanged(); }
        }

    
        public decimal Total => Quantity * Price;

        public string DiscountInfo
        {
            get
            {
                if (Quantity > 100) return "Скидка 20%";
                if (Quantity > 50) return "Скидка 10%";
                if (Quantity > 10) return "Скидка 5%";
                if (Total > 10000) return "Скидка 25%";
                return "Без скидки";
            }
        }

        public string StatusRussian => Status switch
        {
            OrderStatus.New => "Новый",
            OrderStatus.Paid => "Оплачен",
            OrderStatus.Shipped => "Отправлен",
            OrderStatus.Cancelled => "Отменён",
            _ => "Неизвестно"
        };

        public event PropertyChangedEventHandler? PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}