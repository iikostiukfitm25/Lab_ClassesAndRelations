using Task1_2.StudentValidation;

Console.OutputEncoding = System.Text.Encoding.UTF8;

// 1. Успішне створення студентів
var s1 = new Student("Костюк Ілля", 19, 4.85, "ST-2026-01");
var s2 = new Student("Петренко Олена", 20, 4.10, "ST-2026-02");

Console.WriteLine("=== Успішно створені студенти ===");
Console.WriteLine(s1);
Console.WriteLine(s2);

// 2. Спроби передати некоректні значення (перехоплення винятків)
Console.WriteLine("\n=== Перевірка валідації (try-catch) ===");

try
{
    var badName = new Student("   ", 18, 4.0, "ST-03");
}
catch (ArgumentException ex)
{
    Console.WriteLine($"[Перехоплено {ex.GetType().Name}] {ex.Message}");
}

try
{
    var badAge = new Student("Іванов Іван", 14, 3.8, "ST-04");
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"[Перехоплено {ex.GetType().Name}] {ex.Message}");
}

try
{
    s2.GPA = 5.7;
}
catch (ArgumentOutOfRangeException ex)
{
    Console.WriteLine($"[Перехоплено {ex.GetType().Name}] {ex.Message}");
}

namespace Task1_2.StudentValidation
{
    public class Student
    {
        private string _name = string.Empty;
        private int _age;
        private double _gpa;

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Ім'я студента не може бути порожнім.", nameof(value));
                _name = value.Trim();
            }
        }

        public int Age
        {
            get => _age;
            set
            {
                if (value < 16 || value > 60)
                    throw new ArgumentOutOfRangeException(nameof(value), "Вік має бути в межах від 16 до 60 років.");
                _age = value;
            }
        }

        public double GPA
        {
            get => _gpa;
            set
            {
                if (value < 0.0 || value > 5.0)
                    throw new ArgumentOutOfRangeException(nameof(value), "Середній бал GPA має бути від 0.0 до 5.0.");
                _gpa = value;
            }
        }

        public string StudentId { get; private set; }

        public bool IsHonorStudent => _gpa >= 4.5;

        public Student(string name, int age, double gpa, string studentId)
        {
            if (string.IsNullOrWhiteSpace(studentId))
                throw new ArgumentException("ID студента не може бути порожнім.", nameof(studentId));

            Name = name;
            Age = age;
            GPA = gpa;
            StudentId = studentId;
        }

        public override string ToString() =>
            $"Студент: {Name}, Вік: {Age}, GPA: {GPA:F2}, ID: {StudentId}, " +
            $"Відмінник: {(IsHonorStudent ? "Так" : "Ні")}";
    }
}