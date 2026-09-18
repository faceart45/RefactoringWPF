using System;
using System.Collections.Generic;
using System.Text;

namespace RefactoringWPF.Utils
{
    public static class Helper
    {
        public static string FormatMoney(decimal amount)
        {
            return amount.ToString("0.00") + " руб.";
        }

        public static string FormatMoneyShort(decimal amount)
        {
            return amount.ToString("0.0") + " руб.";
        }

        public static bool IsValidEmail(string email)
        {
            if (email.Contains("@") && email.Contains("."))
                return true;
            else
                return false;
        }

        public static string StatusToRussian(string status)
        {
            if (status == "new") return "Новый";
            if (status == "paid") return "Оплачен";
            if (status == "shipped") return "Отправлен";
            if (status == "cancelled") return "Отменён";
            return "Неизвестно";
        }

        // НАРУШЕНИЕ: dead code (не используется)
        public static int CalculateSomething(int a, int b)
        {
            return a + b * 2 - 1;
        }
    }
}
