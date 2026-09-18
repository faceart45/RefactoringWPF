using System;
using System.Collections.Generic;
using System.Text;

namespace RefactoringWPF.Models
{
    public class Order
    {
        public int Id;
        public string CustomerName { get; set; }
        public string ProductName;
        public int Quantity;
        public decimal Price;
        public string Status; // "new", "paid", "shipped", "cancelled"
        public DateTime CreatedDate;

        public decimal GetTotal()
        {
            return Quantity * Price;
        }
    }
}
