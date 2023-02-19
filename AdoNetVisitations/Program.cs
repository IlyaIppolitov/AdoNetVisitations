using AdoNetVisitations;
using System.Dynamic;

var dbService = new StudentsVisitationService();

dbService.CreateTableStudents();
dbService.CreateTableVisitations();
dbService.FillTestStudents();
dbService.FillTestVisitations();

while (true)
{
    Console.Clear();
    Console.WriteLine("Введите команду:\n" +
        "\"Students\" - для получения списка посещений;\n" +
        "\"AddVisit\" - для новой записи в список посещений;\n" +
        "\"AddStudent\" - для новой записи в список студентов;\n" +
        "Для выхода введите \"0\"."); ;
    var command = Console.ReadLine();

    if (command != null) command = command.ToLower();
    else
    {
        Console.WriteLine("Попробуйте ещё раз!");
        Console.ReadKey();
    }

    if (command == "0") break;

    string name = string.Empty;
    string surname = string.Empty;

    switch (command)
    {
        case "students":

            Visitation[] Visitations = dbService.GetVisitations();
            if (Visitations.Count() == 0 )
            {
                Console.WriteLine("Нет информации о посещениях!");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Список посещений:");
                foreach (var it in Visitations)
                {
                    Console.WriteLine(it.ToString());
                }
                Console.ReadKey();
            }
            break;

        case "addvisit":
            Console.WriteLine("Введите имя:");
            while (true)
            {
                name = Console.ReadLine();
                if (name == null) Console.WriteLine("Поле не может быть пустым! \nПопробуйте ещё раз!");
                else break;
            }
            Console.WriteLine("Введите фамилию:");
            while (true)
            {
                surname = Console.ReadLine();
                if (surname == null) Console.WriteLine("Поле не может быть пустым! \nПопробуйте ещё раз!");
                else break;
            }

            if (!dbService.CheckStudentByName(name, surname))
            {
                Console.WriteLine("Нет студента с таким имененм в базе!");
                Console.ReadKey();
                break;
            }

            DateOnly date;
            while (true)
            {
                Console.WriteLine("Введите дату в формате dd.MM.yyyy:");
                if (DateOnly.TryParseExact(Console.ReadLine(), "dd.MM.yyyy", out date))
                {
                    break;
                }
                else Console.WriteLine("Соблюдайте формат!");

            }
            dbService.AddVisitation(name,surname,date);
            Console.WriteLine("DONE!");
            Console.ReadKey();
            break;

        case "addstudent":
            Console.WriteLine("Введите имя:");
            while (true)
            {
                name = Console.ReadLine();
                if (name == null) Console.WriteLine("Поле не может быть пустым! \nПопробуйте ещё раз!");
                else break;
            }
            Console.WriteLine("Введите фамилию:");
            while (true)
            {
                surname = Console.ReadLine();
                if (surname == null) Console.WriteLine("Поле не может быть пустым! \nПопробуйте ещё раз!");
                else break;
            }
            
            dbService.AddStudent(name,surname);
            Console.WriteLine("DONE!");
            Console.ReadKey();
            break;

            default: break;
    }
}