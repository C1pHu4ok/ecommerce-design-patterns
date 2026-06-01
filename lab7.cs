using System;

namespace eCommercePlatform
{
    // ==========================================
    // СЛУЖБОВІ ПІДСИСТЕМИ (Subsystems)
    // Вони складні й виконують окремі завдання магазину.
    // ==========================================

    public class InventorySystem
    {
        public bool IsInStock(string itemId)
        {
            Console.WriteLine($"[Склад]: Перевірка наявності товару {itemId}... Є в наявності.");
            return true;
        }

        public void ReserveItem(string itemId)
        {
            Console.WriteLine($"[Склад]: Товар {itemId} успішно зарезервовано під замовлення.");
        }
    }

    public class BillingSystem
    {
        public bool ChargeUser(string username, decimal amount)
        {
            Console.WriteLine($"[Оплата]: Спроба списання {amount} грн з рахунку користувача '{username}'...");
            Console.WriteLine($"[Оплата]: Транзакція успішна. Кошти знято.");
            return true;
        }
    }

    public class LogisticsSystem
    {
        public void CreateShippingOrder(string itemId, string address)
        {
            Console.WriteLine($"[Доставка]: Формування ТТН для товару {itemId}.");
            Console.WriteLine($"[Доставка]: Маршрут побудовано за адресою: {address}. Передано кур'єру.");
        }
    }

    // ==========================================
    // ЛАБОРАТОРНА РОБОТА №7: ФАСАД (FACADE)
    // ==========================================
    
    // Цей клас об'єднує роботу трьох складних систем в один простий виклик
    public class OrderProcessingFacade
    {
        private readonly InventorySystem _inventory;
        private readonly BillingSystem _billing;
        private readonly LogisticsSystem _logistics;

        public OrderProcessingFacade()
        {
            _inventory = new InventorySystem();
            _billing = new BillingSystem();
            _logistics = new LogisticsSystem();
        }

        // Один простий метод для клієнта, який керує всім процесом
        public bool PlaceOrder(string itemId, string username, decimal price, string deliveryAddress)
        {
            Console.WriteLine("[ФАСАД]: Початок обробки комплексного замовлення...");

            // Крок 1: Перевірка складу
            if (!_inventory.IsInStock(itemId))
            {
                Console.WriteLine("[ФАСАД]: Помилка оформлення — товару немає на складі!");
                return false;
            }

            // Крок 2: Резервування
            _inventory.ReserveItem(itemId);

            // Крок 3: Оплата
            if (!_billing.ChargeUser(username, price))
            {
                Console.WriteLine("[ФАСАД]: Помилка оформлення — відхилено платіж!");
                return false;
            }

            // Крок 4: Доставка
            _logistics.CreateShippingOrder(itemId, deliveryAddress);

            Console.WriteLine("[ФАСАД]: Замовлення успішно оформлено «під ключ»!");
            return true;
        }
    }

    // ==========================================
    // ГОЛОВНА ПРОГРАМА ДЛЯ ЛР №7
    // ==========================================
    class Lab7Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Лабораторна робота №7: Facade ===\n");

            // Створюємо фасад
            OrderProcessingFacade checkoutWindow = new OrderProcessingFacade();

            // Клієнт купує товар. Йому не треба створювати 3 різні системи та зв'язувати їх вручну.
            // Він робить лише ОДИН виклик:
            bool success = checkoutWindow.PlaceOrder(
                itemId: "IPHONE-17-PRO",
                username: "max_godunko",
                price: 48000.00m,
                deliveryAddress: "м. Хмельницький, Відділення Нової Пошти №1"
            );

            if (success)
            {
                Console.WriteLine("\nПрограма: Покупка завершена, статус замовлення: 'В дорозі'.");
            }
        }
    }
}
