using System;

namespace eCommercePlatform
{
    // ==========================================
    // ЛАБОРАТОРНА РОБОТА №4: ДЕКОРАТОР (DECORATOR)
    // ==========================================

    // 1. Базовий інтерфейс замовлення (Component)
    public interface IOrder
    {
        string GetDescription();
        decimal GetTotalCost();
    }

    // 2. Конкретний об'єкт замовлення — просто товари в кошику (Concrete Component)
    public class StandardOrder : IOrder
    {
        private readonly decimal _itemsPrice;

        public StandardOrder(decimal itemsPrice)
        {
            _itemsPrice = itemsPrice;
        }

        public string GetDescription() => "Товари в кошику";
        public decimal GetTotalCost() => _itemsPrice;
    }

    // 3. Базовий клас декоратора (Decorator). 
    // Він реалізує той самий інтерфейс і зберігає посилання на загорнутий об'єкт.
    public abstract class OrderDecorator : IOrder
    {
        protected readonly IOrder _wrappedOrder;

        protected OrderDecorator(IOrder order)
        {
            _wrappedOrder = order;
        }

        // Перенаправляємо виклики за замовчуванням до загорнутого об'єкта
        public virtual string GetDescription() => _wrappedOrder.GetDescription();
        public virtual decimal GetTotalCost() => _wrappedOrder.GetTotalCost();
    }

    // 4. Конкретний декоратор А: Додає святкове пакування (Concrete Decorator)
    public class GiftWrappingDecorator : OrderDecorator
    {
        public GiftWrappingDecorator(IOrder order) : base(order) { }

        public override string GetDescription()
        {
            // Додаємо новий текст до опису
            return base.GetDescription() + " + святкове пакування (🎁)";
        }

        public override decimal GetTotalCost()
        {
            // Додаємо вартість пакування до загальної суми
            return base.GetTotalCost() + 150.00m;
        }
    }

    // 5. Конкретний декоратор Б: Додає швидку доставку (Concrete Decorator)
    public class ExpressDeliveryDecorator : OrderDecorator
    {
        public ExpressDeliveryDecorator(IOrder order) : base(order) { }

        public override string GetDescription()
        {
            return base.GetDescription() + " + експрес-доставка (⚡)";
        }

        public override decimal GetTotalCost()
        {
            return base.GetTotalCost() + 300.00m;
        }
    }

    // ==========================================
    // ГОЛОВНА ПРОГРАМА ДЛЯ ЛР №4
    // ==========================================
    class Lab4Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Лабораторна робота №4: Decorator ===\n");

            // Клієнт набрав товарів на 1500 грн
            IOrder order = new StandardOrder(1500.00m);
            Console.WriteLine("1. Базове замовлення:");
            PrintOrder(order);

            Console.WriteLine("\n---------------------------------------\n");

            // Клієнт вирішив додати святкове пакування (загортаємо базове замовлення)
            order = new GiftWrappingDecorator(order);
            Console.WriteLine("2. Замовлення після додавання пакування:");
            PrintOrder(order);

            Console.WriteLine("\n---------------------------------------\n");

            // Клієнт також додав експрес-доставку (загортаємо вже декороване замовлення ще раз!)
            order = new ExpressDeliveryDecorator(order);
            Console.WriteLine("3. Фінальне комбіноване замовлення:");
            PrintOrder(order);
        }

        static void PrintOrder(IOrder order)
        {
            Console.WriteLine($"Склад: {order.GetDescription()}");
            Console.WriteLine($"Фінальна вартість: {order.GetTotalCost()} грн.");
        }
    }
}
