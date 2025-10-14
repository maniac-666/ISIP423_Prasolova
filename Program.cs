using System;
using System.Collections.Generic;
using System.Linq;

namespace LibraryApp
{
    class Program
    {
        static void Main()
        {
            var library = new List<Book>
            {
                new Book("Война и Мир", "Кто воевал и мрился", "Любовь", 1869, 500),
                new Book("Преступление и наказание", "Федор Достоевский", "Дед и зайцы", 1866, 450),
                new Book("Робинзон крузо", "Михаил ", "Золушка", 1967, 600),
                new Book("Три мушкетера", "Александр", "Малахитовая шкатулкак", 1844, 300),
                new Book("Отцы и дети", "Иван Тургеев", "Доктор кто", 1862, 400)
            };
            while (true)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1 - Добавить книгу");
                Console.WriteLine("2 - Удалить книгу по Id");
                Console.WriteLine("3 - Найти книги");
                Console.WriteLine("4 - Отсортировать книги по названию");
                Console.WriteLine("5 - Отсортировать книги по году");
                Console.WriteLine("6 - Вывести самую дорогую и самую дешёвую книгу");
                Console.WriteLine("7 - Сгруппировать книги по авторам");
                Console.WriteLine("0 - Выход");
                Console.Write("Выберите команду: ");

                if (!int.TryParse(Console.ReadLine(), out int cmd)) continue;
                if (cmd == 0) break;

                switch (cmd)
                {
                    case 1:
                        AddBook(library);
                        break;
                    case 2:
                        RemoveBook(library);
                        break;
                    case 3:
                        SearchBooks(library);
                        break;
                    case 4:
                        var byTitle = library.OrderBy(b => b.Title);
                        PrintBooks(byTitle);
                        break;
                    case 5:
                        var byYear = library.OrderBy(b => b.Year);
                        PrintBooks(byYear);
                        break;
                    case 6:
                        var max = library.OrderByDescending(b => b.Price).First();
                        var min = library.OrderBy(b => b.Price).First();
                        Console.WriteLine($"Дорогая: {max}");
                        Console.WriteLine($"Дешёвая: {min}");
                        break;
                    case 7:
                        var grouped = library.GroupBy(b => b.Author);
                        foreach (var g in grouped)
                            Console.WriteLine($"{g.Key} -> {g.Count()} книг");
                        break;
                }
            }
        }




            