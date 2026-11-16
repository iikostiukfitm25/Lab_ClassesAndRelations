using Task3_1.UniversityRelations;

Console.OutputEncoding = System.Text.Encoding.UTF8;

// TODO 1: Створити факультет
Faculty? fitFaculty = new Faculty("Факультет інформаційних технологій");

// TODO 2: Створити 2 кафедри (КОМПОЗИЦІЯ — створюються всередині факультету)
fitFaculty.CreateDepartment("Кафедра програмної інженерії", "д.т.н. Коваленко О.П.");
fitFaculty.CreateDepartment("Кафедра кібербезпеки", "к.т.н. Мельник І.В.");

// TODO 3: Створити 3 професорів окремо (існують незалежно від факультету)
var prof1 = new Professor("Сидоренко В.М.", "Доктор технічних наук", "Кафедра програмної інженерії");
var prof2 = new Professor("Бондаренко Т.С.", "Кандидат фізико-математичних наук", "Кафедра програмної інженерії");
var prof3 = new Professor("Кравчук А.Ю.", "Доктор технічних наук", "Кафедра кібербезпеки");

// TODO 4: Додати професорів до факультету (АГРЕГАЦІЯ — передаються ззовні)
fitFaculty.AddProfessor(prof1);
fitFaculty.AddProfessor(prof2);
fitFaculty.AddProfessor(prof3);

// TODO 5: Створити будівлю та додати 4 аудиторії (КОМПОЗИЦІЯ)
var mainBuilding = new Building("Навчальний корпус №1");
mainBuilding.AddRoom("101", "Лекційна зала", 120);
mainBuilding.AddRoom("204", "Комп'ютерний клас", 30);
mainBuilding.AddRoom("305", "Лабораторія мереж", 24);
mainBuilding.AddRoom("410", "Семінарська", 35);

// TODO 6: Вивести інформацію
fitFaculty.DisplayInfo();
Console.WriteLine();
mainBuilding.DisplayInfo();

var found = mainBuilding.FindRoom("204");
Console.WriteLine($"\nПошук аудиторії 204: {found}");

// TODO 7: Демонстрація незалежності професорів при знищенні факультету (агрегація)
fitFaculty = null;
Console.WriteLine("\n=== Після знищення об'єкта факультету ===");
Console.WriteLine($"Професор продовжує існувати незалежно: {prof1}");

namespace Task3_1.UniversityRelations
{
    // Частина будівлі (Композиція)
    public class Room
    {
        public string Number { get; set; }
        public string Type { get; set; }
        public int Capacity { get; set; }

        public Room(string number, string type, int capacity)
        {
            Number = number;
            Type = type;
            Capacity = capacity;
        }

        public override string ToString() => $"Аудиторія {Number} ({Type}, {Capacity} місць)";
    }

    // Частина факультету (Композиція — життєвий цикл залежить від Faculty)
    public class Department
    {
        public string Name { get; set; }
        public string HeadName { get; set; }

        public Department(string name, string headName)
        {
            Name = name;
            HeadName = headName;
        }

        public override string ToString() => $"Кафедра: {Name} (зав. кафедри: {HeadName})";
    }

    // Існує незалежно від факультету (Агрегація з Faculty, Асоціація з Department)
    public class Professor
    {
        public string Name { get; set; }
        public string Degree { get; set; }
        public string Department { get; set; } // Асоціація

        public Professor(string name, string degree, string department)
        {
            Name = name;
            Degree = degree;
            Department = department;
        }

        public override string ToString() => $"{Name} ({Degree}) — {Department}";
    }

    public class Faculty
    {
        private string _name;
        private List<Department> _departments; // Композиція
        private List<Professor> _professors;   // Агрегація

        public Faculty(string name)
        {
            _name = name;
            _departments = new List<Department>();
            _professors = new List<Professor>();
        }

        // Композиція: об'єкт створюється всередині контейнера
        public void CreateDepartment(string name, string headName)
        {
            _departments.Add(new Department(name, headName));
        }

        // Агрегація: готовий зовнішній об'єкт додається до списку
        public void AddProfessor(Professor professor)
        {
            ArgumentNullException.ThrowIfNull(professor);
            _professors.Add(professor);
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"=== {_name} ===");
            Console.WriteLine("Кафедри (Композиція):");
            foreach (var dept in _departments)
                Console.WriteLine($"  - {dept}");

            Console.WriteLine("Викладацький склад (Агрегація):");
            foreach (var prof in _professors)
                Console.WriteLine($"  - {prof}");
        }
    }

    public class Building
    {
        private string _name;
        private List<Room> _rooms; // Композиція

        public Building(string name)
        {
            _name = name;
            _rooms = new List<Room>();
        }

        // Композиція: аудиторія створюється всередині будівлі
        public void AddRoom(string number, string type, int capacity)
        {
            _rooms.Add(new Room(number, type, capacity));
        }

        public Room? FindRoom(string number)
        {
            foreach (var room in _rooms)
            {
                if (room.Number == number)
                    return room;
            }
            return null;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Будівля: {_name}");
            foreach (var room in _rooms)
                Console.WriteLine($"  {room}");
        }
    }
}