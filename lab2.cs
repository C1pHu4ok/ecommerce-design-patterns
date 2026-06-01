using System;

namespace eCommercePlatform
{
    // ==========================================
    // ЛАБОРАТОРНА РОБОТА №2: ОДИНАК (SINGLETON)
    // ==========================================
    
    public class StoreSettings
    {
        private static StoreSettings _instance;
        private static readonly object _lock = new object();

        // Налаштування магазину
        public string StoreName { get; set; } = "MyOnlineStore";
        public string Currency { get; set; } = "UAH";
        public string Language { get; set; } = "UA";

        // Приватний конструктор
        private StoreSettings() { }

        // Глобальна точка доступу
        public static StoreSettings Instance
        {
            get
            {
                if (_instance == null)
                {
                    lock (_lock)
                    {
                        if (_instance == null)
                        {
                            _instance = new StoreSettings();
                        }
                    }
                }
                return _instance;
            }
        }
    }

    class Lab2Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Лабораторна робота №2: Singleton ===");

            // Отримуємо доступ до налаштувань у першому модулі програми
            StoreSettings settings1 = StoreSettings.Instance;
            Console.WriteLine($"Початкова назва магазину: {settings1.StoreName}");

            // Змінюємо налаштування в іншому місці програми
            StoreSettings settings2 = StoreSettings.Instance;
            settings2.StoreName = "Super-Tech-Shop";
            settings2.Currency = "USD";

            // Перевіряємо, чи змінилися налаштування в першому посиланні
            Console.WriteLine($"Оновлена назва магазину (через settings1): {settings1.StoreName}");
            Console.WriteLine($"Валюта магазину (через settings1): {settings1.Currency}");

            // Доводимо, що це один і той самий об'єкт в пам'яті
            bool isSame = ReferenceEquals(settings1, settings2);
            Console.WriteLine($"Чи посилання settings1 та settings2 однакові? {isSame}");
        }
    }
}
