using System;

namespace eCommercePlatform
{
    // ==========================================
    // ЛАБОРАТОРНА РОБОТА №3: АДАПТЕР (ADAPTER)
    // ==========================================

    // 1. Стандартний інтерфейс оплати, який використовує наш магазин
    public interface IPaymentProcessor
    {
        void ProcessPayment(string orderId, decimal amountUah);
    }

    // Наша рідна система оплати, яка працює за стандартом
    public class LocalPaymentProcessor : IPaymentProcessor
    {
        public void ProcessPayment(string orderId, decimal amountUah)
        {
            Console.WriteLine($"[Внутрішня оплата] Замовлення {orderId} успішно оплачено на суму {amountUah} грн.");
        }
    }

    // 2. Стороння (Legacy або закордонна) платіжна система, яку треба інтегрувати.
    // Вона взагалі не знає про наш інтерфейс IPaymentProcessor і приймає лише USD.
    public class ExternalStripeSystem
    {
        public void AuthorizeCard(string cardNumber)
        {
            Console.WriteLine($"[Stripe] Картку {cardNumber} верифіковано.");
        }

        public void ChargeDollars(double dollarsAmount)
        {
            Console.WriteLine($"[Stripe] Знято кошти: ${dollarsAmount:F2}");
        }
    }

    // 3. КЛАС-АДАПТЕР: він робить так, щоб ExternalStripeSystem працювала як IPaymentProcessor
    public class StripeAdapter : IPaymentProcessor
    {
        private readonly ExternalStripeSystem _stripeSystem;
        private readonly string _userCardNumber;
        private const decimal UsdRate = 40.0m; // Фіксований курс для симуляції конвертації

        public StripeAdapter(ExternalStripeSystem stripeSystem, string userCardNumber)
        {
            _stripeSystem = stripeSystem;
            _userCardNumber = userCardNumber;
        }

        public void ProcessPayment(string orderId, decimal amountUah)
        {
            Console.WriteLine($"[Адаптер] Конвертуємо {amountUah} грн в USD за курсом {UsdRate}...");
            
            // Перераховуємо гривні в долари для сторонньої системи
            double amountInUsd = (double)(amountUah / UsdRate);

            // Викликаємо специфічні методи сторонньої системи, адаптуючи логіку
            _stripeSystem.AuthorizeCard(_userCardNumber);
            _stripeSystem.ChargeDollars(amountInUsd);
            
            Console.WriteLine($"[Адаптер] Оплата через Stripe для замовлення {orderId} завершена.");
        }
    }

    // ==========================================
    // ГОЛОВНА ПРОГРАМА ДЛЯ ЛР №3
    // ==========================================
    class Lab3Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Лабораторна робота №3: Adapter ===\n");

            string currentOrderId = "ORD-2026-99X";
            decimal cartTotalUah = 2000.0m; // Сума до оплати в гривнях

            // --- Варіант А: Оплата через нашу стандартну систему ---
            Console.WriteLine("Клієнт обирає локальну оплату:");
            IPaymentProcessor localProcessor = new LocalPaymentProcessor();
            localProcessor.ProcessPayment(currentOrderId, cartTotalUah);

            Console.WriteLine("\n---------------------------------------\n");

            // --- Варіант Б: Клієнт хоче оплатити через закордонний Stripe ---
            Console.WriteLine("Клієнт обирає оплату через Stripe:");
            
            // Створюємо об'єкт сторонньої системи
            ExternalStripeSystem stripeService = new ExternalStripeSystem();
            
            // Огортаємо її в наш адаптер, передаючи туди дані картки
            IPaymentProcessor stripeAdapter = new StripeAdapter(stripeService, "4444-5555-6666-7777");

            // Магазин викликає той самий стандартний метод ProcessPayment, 
            // навіть не підозрюючи, що всередині працює зовсім інша система в доларах!
            stripeAdapter.ProcessPayment(currentOrderId, cartTotalUah);
        }
    }
}
