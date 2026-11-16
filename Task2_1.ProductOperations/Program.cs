using Task2_1.ProductOperations;

Console.OutputEncoding = System.Text.Encoding.UTF8;

// TODO 1: Масив із 7 продуктів 4 категорій
Product[] products =
[
    new Product("Ноутбук Lenovo", "Електроніка", 28500m, 4),
    new Product("Миша Logitech", "Периферія", 1250m, 15),
    new Product("Монітор Dell 27\"", "Електроніка", 11200m, 6),
    new Product("Крісло офісне", "Меблі", 6400m, 3),
    new Product("Клавіатура механічна", "Периферія", 2900m, 10),
    new Product("Смартфон Samsung", "Електроніка", 19800m, 5),
    new Product("Рюкзак для ноутбука", "Аксесуари", 1600m, 12)
];

// TODO 2: Вивести всі продукти
Console.WriteLine("=== Усі продукти ===");
foreach (var p in products)
    Console.WriteLine($"  {p}");

// TODO 3–5: Найдорожчий, найдешевший та загальна вартість через foreach
Product mostExpensive = products[0];
Product cheapest = products[0];
decimal totalValue = 0m;

foreach (var p in products)
{
    if (p.Price > mostExpensive.Price) mostExpensive = p;
    if (p.Price < cheapest.Price) cheapest = p;
    totalValue += p.GetTotalValue();
}

Console.WriteLine($"\nНайдорожчий: {mostExpensive.Name} ({mostExpensive.Price:F2} грн)");
Console.WriteLine($"Найдешевший: {cheapest.Name} ({cheapest.Price:F2} грн)");
Console.WriteLine($"Загальна вартість усіх товарів на складі: {totalValue:F2} грн");

// TODO 6: Фільтрація за категорією "Електроніка"
Console.WriteLine("\n=== Категорія: Електроніка ===");
foreach (var p in products)
{
    if (p.Category == "Електроніка")
        Console.WriteLine($"  {p}");
}

// TODO 7: Бульбашкове сортування за зростанням ціни
for (int i = 0; i < products.Length - 1; i++)
{
    for (int j = 0; j < products.Length - i - 1; j++)
    {
        if (products[j].Price > products[j + 1].Price)
        {
            (products[j], products[j + 1]) = (products[j + 1], products[j]);
        }
    }
}

Console.WriteLine("\n=== Після бульбашкового сортування за ціною ===");
foreach (var p in products)
    Console.WriteLine($"  {p}");

// TODO 8: List<Product> та LINQ (Where, OrderBy, Average)
List<Product> list = new(products);
var filteredSorted = list.Where(p => p.Price > 2000m).OrderBy(p => p.Name);
decimal avgPrice = list.Average(p => p.Price);

Console.WriteLine($"\n=== LINQ: дорожчі за 2000 грн (за алфавітом), середня ціна = {avgPrice:F2} грн ===");
foreach (var p in filteredSorted)
    Console.WriteLine($"  {p}");

namespace Task2_1.ProductOperations
{
    public class Product
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Product(string name, string category, decimal price, int quantity)
        {
            Name = name;
            Category = category;
            Price = price;
            Quantity = quantity;
        }

        public decimal GetTotalValue() => Price * Quantity;

        public override string ToString() =>
            $"{Name} [{Category}]: {Price:F2} грн x {Quantity} = {GetTotalValue():F2} грн";
    }
}