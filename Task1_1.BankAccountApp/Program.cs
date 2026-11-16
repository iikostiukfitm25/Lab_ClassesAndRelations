using Task1_1.BankAccountApp;

Console.OutputEncoding = System.Text.Encoding.UTF8;

// 1. Створення рахунків трьома різними конструкторами
var acc1 = new BankAccount(); // За замовчуванням
var acc2 = new BankAccount("UA-7788", "Костюк Ілля", 2500.00m); // Параметризований
var acc3 = new BankAccount(acc2); // Копіювання

Console.WriteLine("=== Створені рахунки ===");
Console.WriteLine(acc1);
Console.WriteLine(acc2);
Console.WriteLine(acc3);
Console.WriteLine($"Загальна кількість створених рахунків: {BankAccount.GetTotalAccounts()}\n");

// 2. Демонстрація Deposit та Withdraw (успішних і помилкових)
Console.WriteLine("=== Операції з рахунками ===");
acc1.Deposit(1000m);
acc1.Deposit(-50m); // Перевірка помилки
acc2.Withdraw(800m);
acc2.Withdraw(5000m); // Перевірка нестачі коштів

Console.WriteLine("\n=== Підсумковий стан ===");
Console.WriteLine(acc1);
Console.WriteLine(acc2);
Console.WriteLine(acc3);

namespace Task1_1.BankAccountApp
{
    public class BankAccount
    {
        private string _accountNumber;
        private string _ownerName;
        private decimal _balance;
        private static int _totalAccounts = 0;

        // Конструктор за замовчуванням
        public BankAccount() : this("0000", "Невідомо", 0m) { }

        // Параметризований конструктор
        public BankAccount(string accountNumber, string ownerName, decimal initialBalance)
        {
            _accountNumber = string.IsNullOrWhiteSpace(accountNumber) ? "0000" : accountNumber;
            _ownerName = string.IsNullOrWhiteSpace(ownerName) ? "Невідомо" : ownerName;
            _balance = initialBalance >= 0 ? initialBalance : 0m;
            _totalAccounts++;
        }

        // Конструктор копіювання
        public BankAccount(BankAccount other)
        {
            ArgumentNullException.ThrowIfNull(other);
            _accountNumber = other._accountNumber + "-COPY";
            _ownerName = other._ownerName;
            _balance = other._balance;
            _totalAccounts++;
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0)
            {
                Console.WriteLine($"[Помилка] Сума поповнення ({amount} грн) має бути більшою за 0.");
                return;
            }
            _balance += amount;
            Console.WriteLine($"[OK] Рахунок {_accountNumber} поповнено на {amount:F2} грн.");
        }

        public bool Withdraw(decimal amount)
        {
            if (amount <= 0 || amount > _balance)
            {
                Console.WriteLine($"[Помилка] Неможливо зняти {amount:F2} грн з рахунку {_accountNumber} (баланс: {_balance:F2} грн).");
                return false;
            }
            _balance -= amount;
            Console.WriteLine($"[OK] З рахунку {_accountNumber} знято {amount:F2} грн.");
            return true;
        }

        public static int GetTotalAccounts() => _totalAccounts;

        public override string ToString() =>
            $"Рахунок: {_accountNumber}, Власник: {_ownerName}, Баланс: {_balance:F2} грн";
    }
}