using RefactoringWPF.Models;

namespace RefactoringWPF.Services.Discounts
{
    public class QuantityDiscountStrategy : IDiscountStrategy
    {
        public string GetDiscountInfo(Order order)
        {
            if (order.Quantity > 100) return "Скидка 20% (опт)";
            if (order.Quantity > 50) return "Скидка 10%";
            if (order.Quantity > 10) return "Скидка 5%";
            if (order.Total > 10000) return "Скидка 25% (сумма)";
            return "Без скидки";
        }

        public decimal CalculateDiscount(Order order)
        {
            if (order.Quantity > 100) return 0.20m;
            if (order.Quantity > 50) return 0.10m;
            if (order.Quantity > 10) return 0.05m;
            if (order.Total > 10000) return 0.25m;
            return 0m;
        }
    }
}