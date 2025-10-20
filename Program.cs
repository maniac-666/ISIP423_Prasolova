using System;
using System.Collections.Generic;
using System.Linq;

public abstract class Person
{
    private string_name;
    private int_age;
    private string_contactInfo;

    public string Name
    {
        get => _name;
        set => _name = !
string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("Имя не может быть пустым. Напишите банан.")
    }
}
  public int Age
    {
        get => _age;
        set => _age = value >= 16 && value <= 100 ? value : throw new ArgumentException("Возраст должен быть от 16 до 100");
    }

    public string ContactInfo
    {
        get => _contactInfo;
        set => _contactInfo = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("Контактная информация не может быть пустой");
    }
    public int Id { get; protected set; }

    protected Person(int id, string name, int age, string contactInfo)
{
    Id = id;
    Name = name;
    Age = age;
    ContactInfo = contactInfo;
}

public virtual void DisplayInfo()
{
    Console.WriteLine($"ID: {Id}, Имя: {Name}, Возраст: {Age}, Контакты: {ContactInfo}");
}
}

public class Student : Person
{
    private List<Course> _courses;

    public IReadOnlyList<Course> Courses => _courses.AsReadOnly();

    public Student(int id, string name, int age, string contactInfo)
        : base(id, name, age, contactInfo)
    {
        _courses = new List<Course>();
    }

    public void EnrollInCourse(Course course)
    {
        if (!_courses.Contains(course))
        {
            _courses.Add(course);
            course.AddStudent(this);
        }
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"=== СТУДЕНТ ===");
        base.DisplayInfo();
        Console.WriteLine($"Курсы ({_courses.Count}):");
        foreach (var course in _courses)
        {
            Console.WriteLine($"  - {course.Name}");
        }
        Console.WriteLine();
    }
}

public class Teacher : Person
{
    private List<Course> _courses;

    public IReadOnlyList<Course> Courses => _courses.AsReadOnly();

    public Teacher(int id, string name, int age, string contactInfo)
        : base(id, name, age, contactInfo)
    {
        _courses = new List<Course>();
    }

    public void AssignToCourse(Course course)
    {
        if (!_courses.Contains(course))
        {
            _courses.Add(course);
            course.AssignTeacher(this);
        }
    }

    public override void DisplayInfo()
    {
        Console.WriteLine($"=== ПРЕПОДАВАТЕЛЬ ===");
        base.DisplayInfo();
        Console.WriteLine($"Ведет курсы ({_courses.Count}):");
        foreach (var course in _courses)
        {
            Console.WriteLine($"  - {course.Name}");
        }
        Console.WriteLine();
    }
}

public class Course
{
    private string _name;
    private string _description;
    private List<Student> _students;
    private Teacher _teacher;

    public string Name
    {
        get => _name;
        set => _name = !string.IsNullOrWhiteSpace(value) ? value : throw new ArgumentException("Название курса не может быть пустым");
    }

    public string Description
    {
        get => _description;
        set => _description = value ?? string.Empty;
    }

    public int Id { get; private set; }
    public IReadOnlyList<Student> Students => _students.AsReadOnly();
    public Teacher Teacher => _teacher;

    public Course(int id, string name, string description = "")
    {
        Id = id;
        Name = name;
        Description = description;
        _students = new List<Student>();
        _teacher = null;
    }
    public void AddStudent(Student student)
    {
        if (!_students.Contains(student))
        {
            _students.Add(student);
        }
    }

    public void AssignTeacher(Teacher teacher)
    {
        _teacher = teacher;
    }

    public void DisplayInfo()
    {
        Console.WriteLine($"=== КУРС ===");
        Console.WriteLine($"ID: {Id}, Название: {Name}");
        Console.WriteLine($"Описание: {Description}");
        Console.WriteLine($"Преподаватель: {_teacher?.Name ?? "Не назначен"}");
        Console.WriteLine($"Студентов: {_students.Count}");
        Console.WriteLine("Список студентов:");
        foreach (var student in _students)
        {
            Console.WriteLine($"  - {student.Name}");
        }
        Console.WriteLine();
    }
}

public class UniversityManager
{
    private List<Student> _students;
    private List<Teacher> _teachers;
    private List<Course> _courses;
    private int _nextStudentId = 1;
    private int _nextTeacherId = 1;
    private int _nextCourseId = 1;

    public UniversityManager()
    {
        _students = new List<Student>();
        _teachers = new List<Teacher>();
        _courses = new List<Course>();
    }

    public void AddStudent(string name, int age, string contactInfo)
    {
        var student = new Student(_nextStudentId++, name, age, contactInfo);
        _students.Add(student);
        Console.WriteLine($"Студент {name} успешно добавлен!");
    }

    public void AddTeacher(string name, int age, string contactInfo)
    {
        var teacher = new Teacher(_nextTeacherId++, name, age, contactInfo);
        _teachers.Add(teacher);
        Console.WriteLine($"Преподаватель {name} успешно добавлен!");
    }

    public void AddCourse(string name, string description = "")
    {
        var course = new Course(_nextCourseId++, name, description);
        _courses.Add(course);
        Console.WriteLine($"Курс {name} успешно создан!");
    }

    public Student FindStudent(int id) => _students.FirstOrDefault(s => s.Id == id);
    public Teacher FindTeacher(int id) => _teachers.FirstOrDefault(t => t.Id == id);
    public Course FindCourse(int id) => _courses.FirstOrDefault(c => c.Id == id);

    public void DisplayAllStudents()
    {
        Console.WriteLine("\n=== ВСЕ СТУДЕНТЫ ===");
        foreach (var student in _students)
        {
            student.DisplayInfo();
        }
    }

    public void DisplayAllTeachers()
    {
        Console.WriteLine("\n=== ВСЕ ПРЕПОДАВАТЕЛИ ===");
        foreach (var teacher in _teachers)
        {
            teacher.DisplayInfo();
        }
    }

    public void DisplayAllCourses()
    {
        Console.WriteLine("\n=== ВСЕ КУРСЫ ===");
        foreach (var course in _courses)
        {
            course.DisplayInfo();
        }
    }

    public void EnrollStudentInCourse(int studentId, int courseId)
    {
        var student = FindStudent(studentId);
        var course = FindCourse(courseId);

        if (student == null || course == null)
        {
            Console.WriteLine("Ошибка: Студент или курс не найден!");
            return;
        }

        student.EnrollInCourse(course);
        Console.WriteLine($"Студент {student.Name} записан на курс {course.Name}");
    }

    public void AssignTeacherToCourse(int teacherId, int courseId)
    {
        var teacher = FindTeacher(teacherId);
        var course = FindCourse(courseId);

        if (teacher == null || course == null)
        {
            Console.WriteLine("Ошибка: Преподаватель или курс не найден!");
            return;
        }

        teacher.AssignToCourse(course);
        Console.WriteLine($"Преподаватель {teacher.Name} назначен на курс {course.Name}");
    }
}

public class ConsoleMenu
{
    private UniversityManager _university;

    public ConsoleMenu()
    {
        _university = new UniversityManager();
    }

    public void Run()
    {
        while (true)
        {
            DisplayMainMenu();
            var choice = GetUserInput("Выберите действие: ");

            switch (choice)
            {
                case "1": AddStudentMenu(); break;
                case "2": AddTeacherMenu(); break;
                case "3": AddCourseMenu(); break;
                case "4": EnrollStudentMenu(); break;
                case "5": AssignTeacherMenu(); break;
                case "6": _university.DisplayAllStudents(); break;
                case "7": _university.DisplayAllTeachers(); break;
                case "8": _university.DisplayAllCourses(); break;
                case "0": return;
                default: Console.WriteLine("Неверный выбор!"); break;
            }

            Console.WriteLine("\nНажмите любую клавишу для продолжения...");
            Console.ReadKey();
        }
    }

    private void DisplayMainMenu()
    {
        Console.Clear();
        Console.WriteLine("=== СИСТЕМА УПРАВЛЕНИЯ УНИВЕРСИТЕТОМ ===");
        Console.WriteLine("1. Добавить студента");
        Console.WriteLine("2. Добавить преподавателя");
        Console.WriteLine("3. Создать курс");
        Console.WriteLine("4. Записать студента на курс");
        Console.WriteLine("5. Назначить преподавателя на курс");
        Console.WriteLine("6. Показать всех студентов");
        Console.WriteLine("7. Показать всех преподавателей");
        Console.WriteLine("8. Показать все курсы");
        Console.WriteLine("0. Выход");
    }

    private void AddStudentMenu()
    {
        Console.WriteLine("\n--- Добавление студента ---");
        var name = GetUserInput("Введите имя: ");
        var age = int.Parse(GetUserInput("Введите возраст: "));
        var contact = GetUserInput("Введите контактную информацию: ");

        _university.AddStudent(name, age, contact);
    }

    private void AddTeacherMenu()
    {
        Console.WriteLine("\n--- Добавление преподавателя ---");
        var name = GetUserInput("Введите имя: ");
        var age = int.Parse(GetUserInput("Введите возраст: "));
        var contact = GetUserInput("Введите контактную информацию: ");

        _university.AddTeacher(name, age, contact);
    }

    private void AddCourseMenu()
    {
        Console.WriteLine("\n--- Создание курса ---");
        var name = GetUserInput("Введите название курса: ");
        var description = GetUserInput("Введите описание курса: ");

        _university.AddCourse(name, description);
    }

    private void EnrollStudentMenu()
    {
        Console.WriteLine("\n--- Запись студента на курс ---");
        var studentId = int.Parse(GetUserInput("Введите ID студента: "));
        var courseId = int.Parse(GetUserInput("Введите ID курса: "));

        _university.EnrollStudentInCourse(studentId, courseId);
    }

    private void AssignTeacherMenu()
    {
        Console.WriteLine("\n--- Назначение преподавателя на курс ---");
        var teacherId = int.Parse(GetUserInput("Введите ID преподавателя: "));
        var courseId = int.Parse(GetUserInput("Введите ID курса: "));

        _university.AssignTeacherToCourse(teacherId, courseId);
    }

    private string GetUserInput(string prompt)
    {
        Console.Write(prompt);
        return Console.ReadLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        var menu = new ConsoleMenu();
        menu.Run();
    }
}