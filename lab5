using System;
using System.Collections.Generic;

namespace eCommercePlatform
{
    // ==========================================
    // ЛАБОРАТОРНА РОБОТА №5: СПОСТЕРІГАЧ (OBSERVER)
    // ==========================================

    // 1. Інтерфейс підписника (Observer)
    public interface ISubscriber
    {
        void Update(string productName, bool isAvailable);
    }

    // 2. Конкретний підписник 1: Покупець (Concrete Observer)
    public class CustomerSubscriber : ISubscriber
    {
        private readonly string _customerName;

        public CustomerSubscriber(string name)
        {
            _customerName = name;
        }

        public void Update(string productName, bool isAvailable)
        {
            if (isAvailable)
            {
                Console.WriteLine($"[SMS для {_customerName}]: Чудова новина! Товар '{productName}' з'явився в наявності. Поспішайте купити!");
            }
        }
    }

    // 3. Конкретний підписник 2: Менеджер складу (Concrete Observer)
    public class StockManagerSubscriber : ISubscriber
    {
        public void Update(string productName, bool isAvailable)
        {
            string status = isAvailable ? "ОНОВЛЕНО НА СТАДІЇ: В НАЯВНОСТІ" : "ЗАКІНЧИВСЯ";
            Console.WriteLine($"[Система Складу]: Статус товару '{productName}' змінено на [{status}]. Оновити внутрішню базу даних.");
        }
    }

    // 4. Суб'єкт (Subject) — Товар, за статусом якого всі стежать
    public class ProductStock
    {
        private readonly List<ISubscriber> _subscribers = new List<ISubscriber>();
        public string ProductName { get; private set; }
        private bool _isAvailable;

        public ProductStock(string name)
        {
            ProductName = name;
            _isAvailable = false; // Спочатку товару немає
        }

        // Методи підписки / відписки
        public void Subscribe(ISubscriber subscriber)
        {
            _subscribers.Add(subscriber);
            Console.WriteLine($"[Система] Додано нового підписника на товар '{ProductName}'.");
        }

        public void Unsubscribe(ISubscriber subscriber)
        {
            _subscribers.Remove(subscriber);
            Console.WriteLine($"[Система] Підписника видалено з черги очікування '{ProductName}'.");
        }

        // Зміна стану товару, яка тригерить сповіщення
        public void ChangeAvailability(bool isAvailable)
        {
            _isAvailable = isAvailable;
            Console.WriteLine($"\n[Зміна на складі]: Статус товару '{ProductName}' змінився. Наявність: {isAvailable}");
            NotifySubscribers();
        }

        // Розсилка повідомлень усім підписникам
        private void NotifySubscribers()
        {
            foreach (var subscriber in _subscribers)
            {
                subscriber.Update(ProductName, _isAvailable);
            }
        }
    }

    // ==========================================
    // ГОЛОВНА ПРОГРАМА ДЛЯ ЛР №5
    // ==========================================
    class Lab5Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Лабораторна робота №5: Observer ===\n");

            // Створюємо товар, якого зараз немає в наявності
            ProductStock playStationStock = new ProductStock("PlayStation 5 Pro");

            // Створюємо різних підписників
            var customer1 = new CustomerSubscriber("Макс");
            var customer2 = new CustomerSubscriber("Дмитро");
            var stockManager = new StockManagerSubscriber();

            // Оформлюємо підписки
            playStationStock.Subscribe(customer1);
            playStationStock.Subscribe(customer2);
            playStationStock.Subscribe(stockManager); // Склад теж хоче знати про зміни

            // Симулюємо ситуацію: товар приїхав на склад
            playStationStock.ChangeAvailability(true);

            Console.WriteLine("\n---------------------------------------\n");

            // Один клієнт купив товар і вирішив скасувати підписку
            playStationStock.Unsubscribe(customer1);

            // Симулюємо ще одну зміну (наприклад, товар знову розкупили)
            playStationStock.ChangeAvailability(false);
        }
    }
}
