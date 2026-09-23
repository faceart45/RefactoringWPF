using RefactoringWPF.Models;

namespace RefactoringWPF.Services.Discounts
{
    public interface IDiscountStrategy
    {
        string GetDiscountInfo(Order order);
        decimal CalculateDiscount(Order order);
    }
}