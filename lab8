using System;

namespace eCommercePlatform
{
    // ==========================================
    // ЛАБОРАТОРНА РОБОТА №8: СТАН (STATE)
    // ==========================================

    // 1. Інтерфейс Стан (State) - визначає поведінку, що залежить від стану замовлення
    public interface IOrderState
    {
        void CancelOrder(OrderContext context);
        void ShipOrder(OrderContext context);
        string GetStateName();
    }

    // 2. Конкретний стан 1: Нове замовлення (Concrete State 1)
    public class NewOrderState : IOrderState
    {
        public string GetStateName() => "Нове (Очікує оплати)";

        public void CancelOrder(OrderContext context)
        {
            Console.WriteLine("[Стан - Нове]: Замовлення успішно скасовано. Повертаємо товари на склад.");
            // Тут логіка скасування
        }

        public void ShipOrder(OrderContext context)
        {
            Console.WriteLine("[Стан - Нове]: Помилка! Не можна відправити неоплачене замовлення.");
        }
    }

    // 3. Конкретний стан 2: Оплачене замовлення (Concrete State 2)
    public class PaidOrderState : IOrderState
    {
        public string GetStateName() => "Оплачено";

        public void CancelOrder(OrderContext context)
        {
            Console.WriteLine("[Стан - Оплачено]: Замовлення скасовано. Робимо повернення коштів на картку клієнта.");
        }

        public void ShipOrder(OrderContext context)
        {
            Console.WriteLine("[Стан - Оплачено]: Товар передано службі доставки. Змінюємо стан замовлення...");
            // Зміна стану на "Відправлено"
            context.SetState(new ShippedOrderState());
        }
    }

    // 4. Конкретний стан 3: Відправлене замовлення (Concrete State 3)
    public class ShippedOrderState : IOrderState
    {
        public string GetStateName() => "Відправлено клієнту";

        public void CancelOrder(OrderContext context)
        {
            Console.WriteLine("[Стан - Відправлено]: Помилка! Кур'єр уже в дорозі, скасувати замовлення неможливо.");
        }

        public void ShipOrder(OrderContext context)
        {
            Console.WriteLine("[Стан - Відправлено]: Помилка! Замовлення вже було відправлене раніше.");
        }
    }

    // 5. Контекст (Context) - клас замовлення, який зберігає поточний стан
    public class OrderContext
    {
        private IOrderState _currentState;

        public OrderContext()
        {
            // Початковий стан будь-якого замовлення — Нове
            _currentState = new NewOrderState();
            Console.WriteLine($"[Замовлення]: Створено. Поточний статус: {_currentState.GetStateName()}");
        }

        // Метод для зміни стану (викликається самими станами)
        public void SetState(IOrderState state)
        {
            _currentState = state;
            Console.WriteLine($"[Замовлення]: Статус змінено на -> {_currentState.GetStateName()}");
        }

        // Клієнтські методи, які просто делегують роботу поточному стану
        public void Cancel()
        {
            _currentState.CancelOrder(this);
        }

        public void Ship()
        {
            _currentState.Ship();
        }
        
        // Додатковий метод імітації оплати, який переводить в стан Paid
        public void Pay()
        {
            if (_currentState is NewOrderState)
            {
                Console.WriteLine("[Замовлення]: Оплата пройшла успішно.");
                SetState(new PaidOrderState());
            }
            else
            {
                Console.WriteLine("[Замовлення]: Оплата не потрібна або замовлення вже оброблене.");
            }
        }
    }

    // ==========================================
    // ГОЛОВНА ПРОГРАМА ДЛЯ ЛР №8
    // ==========================================
    class Lab8Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Лабораторна робота №8: State ===\n");

            // Створюємо нове замовлення
            OrderContext myOrder = new OrderContext();

            Console.WriteLine("\n--- Спроба 1: Спробуємо відправити без оплати ---");
            myOrder.Ship(); // Має видати помилку

            Console.WriteLine("\n--- Спроба 2: Оплачуємо замовлення ---");
            myOrder.Pay(); // Змінить стан на PaidOrderState

            Console.WriteLine("\n--- Спроба 3: Відправляємо замовлення ---");
            myOrder.Ship(); // Переведе в стан ShippedOrderState

            Console.WriteLine("\n--- Спроба 4: Клієнт раптово хоче скасувати замовлення ---");
            myOrder.Cancel(); // Має видати помилку, бо товар уже в дорозі
        }
    }
}
