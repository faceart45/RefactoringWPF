using RefactoringWPF.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace RefactoringWPF.Services
{
    public class OrderService
    {
        public static List<Order> orders = new List<Order>();
        public static int nextId = 1;

        public void AddOrder(string customer, string product, int qty, decimal price)
        {
            Order o = new Order();
            o.Id = nextId;
            nextId = nextId + 1;
            o.CustomerName = customer;
            o.ProductName = product;
            o.Quantity = qty;
            o.Price = price;
            o.Status = "new";
            o.CreatedDate = DateTime.Now;
            orders.Add(o);

            Console.WriteLine("Order added: " + o.Id);
        }

        public decimal GetTotalRevenue()
        {
            decimal total = 0;
            for (int i = 0; i < orders.Count; i++)
            {
                total = total + orders[i].Quantity * orders[i].Price;
            }
            return total;
        }

        public decimal GetTotalRevenueByStatus(string status)
        {
            decimal total = 0;
            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i].Status == status)
                {
                    total = total + orders[i].Quantity * orders[i].Price;
                }
            }
            return total;
        }

        public List<Order> GetOrdersByStatus(string status)
        {
            List<Order> result = new List<Order>();
            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i].Status == status)
                {
                    result.Add(orders[i]);
                }
            }
            return result;
        }

        public void UpdateStatus(int id, string newStatus, bool writeLog)
        {
            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i].Id == id)
                {
                    orders[i].Status = newStatus;
                    if (writeLog == true)
                    {
                        Console.WriteLine("Status updated for order " + id + " to " + newStatus);
                    }
                }
            }
        }

        public Order FindOrder(int id)
        {
            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i].Id == id)
                    return orders[i];
            }
            return null;
        }

        public string GetDiscount(int qty, decimal total)
        {
            string result = "";
            if (qty > 100)
            {
                result = "20%";
            }
            else if (qty > 50)
            {
                result = "10%";
            }
            else if (qty > 10)
            {
                result = "5%";
            }
            else
            {
                result = "0%";
            }

            if (total > 10000)
            {
                result = "25%";
            }

            return result;
        }

        public void DeleteOrder(int id)
        {
            for (int i = 0; i < orders.Count; i++)
            {
                if (orders[i].Id == id)
                {
                    orders.RemoveAt(i);
                    break;
                }
            }
        }
    }
}
