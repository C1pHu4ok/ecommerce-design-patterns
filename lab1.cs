using System;
using System.Collections.Generic;

namespace eCommercePlatform
{
    // ==========================================
    // 1. ПРОДУКТИ (PRODUCTS)
    // ==========================================
    
    public interface IProduct
    {
        string GetName();
        decimal GetPrice();
        string GetDetails();
    }

    public class Electronics : IProduct
    {
        private string _name;
        private decimal _price;
        private int _warrantyMonths;

        public Electronics(string name, decimal price, int warrantyMonths)
        {
            _name = name;
            _price = price;
            _warrantyMonths = warrantyMonths;
        }

        public string GetName() => _name;
        public decimal GetPrice() => _price;
        public string GetDetails() => $"[Електроніка] Гарантія: {_warrantyMonths} міс.";
    }

    public class Clothing : IProduct
    {
        private string _name;
        private decimal _price;
        private string _size;

        public Clothing(string name, decimal price, string size)
        {
            _name = name;
            _price = price;
            _size = size;
        }

        public string GetName() => _name;
        public decimal GetPrice() => _price;
        public string GetDetails() => $"[Одяг] Розмір: {_size}";
    }

    // ==========================================
    // 2. ФАБРИКИ (FACTORIES)
    // ==========================================

    public abstract class ProductFactory
    {
        public abstract IProduct CreateProduct(string name, decimal price, string specificAttribute);

        public void ShipProduct(string name, decimal price, string specificAttribute)
        {
            var product = CreateProduct(name, price, specificAttribute);
            Console.WriteLine($"Товар '{product.GetName()}' відправлено на склад. Опис: {product.GetDetails()}");
        }
    }

    public class ElectronicsFactory : ProductFactory
    {
        public override IProduct CreateProduct(string name, decimal price, string specificAttribute)
        {
            int warranty = int.TryParse(specificAttribute, out var result) ? result : 12;
            return new Electronics(name, price, warranty);
        }
    }

    public class ClothingFactory : ProductFactory
    {
        public override IProduct CreateProduct(string name, decimal price, string specificAttribute)
        {
            return new Clothing(name, price, specificAttribute);
        }
    }

    // ==========================================
    // 3. ГОЛОВНА ПРОГРАМА (PROGRAM)
    // ==========================================
    
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Лабораторна робота №1: Factory Method ===");

            var factories = new List<ProductFactory>
            {
                new ElectronicsFactory(),
                new ClothingFactory()
            };

            IProduct laptop = factories[0].CreateProduct("Ноутбук", 25000, "24");
            IProduct tShirt = factories[1].CreateProduct("Футболка", 800, "XL");

            PrintProductInfo(laptop);
            PrintProductInfo(tShirt);

            Console.WriteLine("\nДемонстрація внутрішньої логіки фабрики:");
            factories[0].ShipProduct("Смартфон", 12000, "12");
        }

        static void PrintProductInfo(IProduct product)
        {
            Console.WriteLine($"Назва: {product.GetName()} | Ціна: {product.GetPrice()} грн | Деталі: {product.GetDetails()}");
        }
    }
}
