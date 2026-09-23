using System;
using System.Collections.Generic;
using System.Text;

namespace RefactoringWPF.Models
{
    public enum OrderStatus
    {
        New,        // Новый
        Paid,       // Оплачен
        Shipped,    // Отправлен
        Cancelled   // Отменён
    }
}