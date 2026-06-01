using System;

namespace eCommercePlatform
{
    // ==========================================
    // ЛАБОРАТОРНА РОБОТА №6: СТРАТЕГІЯ (STRATEGY)
    // ==========================================

    // 1. Спільний інтерфейс для всіх алгоритмів доставки (Strategy)
    public interface IDeliveryStrategy
    {
        decimal CalculateShippingCost(decimal orderPrice, double packageWeightKg);
        string GetDeliveryMethodName();
    }

    // 2. Стратегія А: Самовивіз (Concrete Strategy 1)
    public class SelfPickupStrategy : IDeliveryStrategy
    {
        public string GetDeliveryMethodName() => "Самовивіз з магазину";

        public decimal CalculateShippingCost(decimal orderPrice, double packageWeightKg)
        {
            // Самовивіз завжди безкоштовний
            return 0.00m;
        }
    }

    // 3. Стратегія Б: Кур'єрська доставка (Concrete Strategy 2)
    public class CourierDeliveryStrategy : IDeliveryStrategy
    {
        public string GetDeliveryMethodName() => "Доставка кур'єром до дверей";

        public decimal CalculateShippingCost(decimal orderPrice, double packageWeightKg)
        {
            // Фіксована вартість, але якщо замовлення дорожче 5000 грн — доставка безкоштовна
            return orderPrice > 5000.00m ? 0.00m : 150.00m;
        }
    }

    // 4. Стратегія В: Поштова служба (Concrete Strategy 3)
    public class PostalDeliveryStrategy : IDeliveryStrategy
    {
        public string GetDeliveryMethodName() => "Доставка через поштову службу (Нова Пошта / Укрпошта)";

        public decimal CalculateShippingCost(decimal orderPrice, double packageWeightKg)
        {
            // Базовий тариф 60 грн + 15 грн за кожен кілограм ваги
            decimal baseTariff = 60.00m;
            decimal weightCost = (decimal)packageWeightKg * 15.00m;
            return baseTariff + weightCost;
        }
    }

    // 5. Контекст (Context), який використовує обрану стратегію
    public class ShippingCalculator
    {
        private IDeliveryStrategy _strategy;

        // Дозволяє динамічно встановлювати або змінювати стратегію «на льоту»
        public void SetStrategy(IDeliveryStrategy strategy)
        {
            _strategy = strategy;
            Console.WriteLine($"[Система]: Змінено спосіб доставки на -> {_strategy.GetDeliveryMethodName()}");
        }

        // Виконання розрахунку (калькулятор не знає деталей розрахунку, він просто делегує це стратегії)
        public decimal Calculate(decimal orderPrice, double packageWeightKg)
        {
            if (_strategy == null)
            {
                throw new InvalidOperationException("Будь ласка, оберіть спосіб доставки!");
            }
            return _strategy.CalculateShippingCost(orderPrice, packageWeightKg);
        }
    }

    // ==========================================
    // ГОЛОВНА ПРОГРАМА ДЛЯ ЛР №6
    // ==========================================
    class Lab6Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Лабораторна робота №6: Strategy ===\n");

            // Дані поточного кошика
            decimal orderTotal = 3200.00m; // Сума товарів
            double totalWeight = 4.5;      // Вага посилки в кг

            Console.WriteLine($"Замовлення на суму: {orderTotal} грн. Вага: {totalWeight} кг.\n");

            // Створюємо контекст розрахунку
            ShippingCalculator calculator = new ShippingCalculator();

            // --- Клієнт обирає Самовивіз ---
            calculator.SetStrategy(new SelfPickupStrategy());
            decimal cost1 = calculator.Calculate(orderTotal, totalWeight);
            Console.WriteLine($"Вартість доставки: {cost1} грн.\n");

            // --- Клієнт передумав і обрав Пошту ---
            calculator.SetStrategy(new PostalDeliveryStrategy());
            decimal cost2 = calculator.Calculate(orderTotal, totalWeight);
            Console.WriteLine($"Вартість доставки: {cost2} грн.\n");

            // --- Клієнт вирішив викликати Кур'єра ---
            calculator.SetStrategy(new CourierDeliveryStrategy());
            decimal cost3 = calculator.Calculate(orderTotal, totalWeight);
            Console.WriteLine($"Вартість доставки: {cost3} грн.\n");
        }
    }
}
