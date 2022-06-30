using Epam.Library.Dependencies;
using Epam.Library.Entities;
using Epam.Library.Entities.Exceptions;
using Epam.Library.LogicContracts;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace Epam.Library.ConsoleUI
{
    class Program
    {
        private static IBookLogic bookLogic = DependencyResolver.BookLogic;        
        private static IPersonLogic personLogic = DependencyResolver.PersonLogic;
        private static ILibraryObjectLogic libraryObjectLogic = DependencyResolver.LibraryObjectLogic;

        static void Main(string[] args)
        {
            var appSettings = ConfigurationManager.AppSettings;
            string role = appSettings["Role"];

            while (true)
            {
                Console.Write("\n" + @"Выберите действие:
0. Просмотр каталога. 
1. Поиск записей по названию. 
2. Сортировка по году выпуска в прямом и обратном порядке. 
3. Поиск всех книг данного автора (включая соавторство).  
4. Вывод всех книг, название издательства которых начинается с заданного набора символов, с группировкой по издательству.");
                if (role == "Librarian" || role == "Admin")
                {
                    Console.Write(@"
5. Отметить объект удаленным.
6. Добавить объект.");
                }
                if (role == "Admin")
                {
                    Console.Write(@"
A. Удалить объект.
B. Восстановить объект.");
                }
                Console.WriteLine();
                
                ConsoleKeyInfo action = Console.ReadKey();
                switch (action.Key)
                {

                    case ConsoleKey.D0:
                        GetAllObjects();
                        break;
                    case ConsoleKey.D1:
                        GetObjectByTitle();
                        break;
                    case ConsoleKey.D2:
                        SortObjects();
                        break;
                    case ConsoleKey.D3:
                        GetBooksByAuthor();
                        break;                    
                    case ConsoleKey.D4:
                        GetBooksByPublishingHouseFilter();
                        break;                    
                    case ConsoleKey.D5:
                        CheckObjectLikeDeletedById();
                        break;
                    case ConsoleKey.D6:
                        AddObject();
                        break;
                    case ConsoleKey.A:
                        DeleteObject();
                        break;
                    case ConsoleKey.B:
                        RestoreObject();
                        break;
                    default:
                        Console.WriteLine("\nВыберите нужное действие");
                        break;
                }
            }
        }

        private static void RestoreObject()
        {
            RestoreBook();       
        }

       

        private static void RestoreBook()
        {
            IEnumerable<Book> objs = bookLogic.GetAll();
            List<int> objsIds = new List<int>();

            if (objs.Any())
            {
                Console.WriteLine("\nВыберите нужный объект и введите его ID");
                foreach (Book obj in objs)
                {
                    Console.WriteLine($"\nКнига: {obj}");
                    objsIds.Add(obj.Id);
                }

                int id = 0;

                while (!objsIds.Any(o => o == id))
                {
                    Console.WriteLine("Введите верный ID");
                    id = ParseToInt();
                }

                bool deleteObj = libraryObjectLogic.RestoreObject(id);

                if (deleteObj)
                {
                    Console.WriteLine("Восстановление успешно");
                }
                else
                {
                    Console.WriteLine("Не удалось восстановить объект, повторите попытку");
                }
            }

            else
            {
                Console.WriteLine("\nЕще не было добавлено объектов");
            }
        }

        private static void CheckObjectLikeDeletedById()
        {
            CheckByDeletedBook();            
        }

       

        private static void CheckByDeletedBook()
        {
            IEnumerable<Book> objs = bookLogic.GetAll();
            List<int> objsIds = new List<int>();

            if (objs.Any())
            {
                Console.WriteLine("\nВыберите нужный объект и введите его ID");
                foreach (Book obj in objs)
                {
                    Console.WriteLine($"\nКнига: {obj}");
                    objsIds.Add(obj.Id);
                }

                int id = 0;

                while (!objsIds.Any(o => o == id))
                {
                    Console.WriteLine("Введите верный ID");
                    id = ParseToInt();
                }

                bool deleteObj = libraryObjectLogic.CheckObjectLikeDeletedById(id);

                if (deleteObj)
                {
                    Console.WriteLine("Удаление успешно");
                }
                else
                {
                    Console.WriteLine("Не удалось удалить объект, повторите попытку");
                }
            }

            else
            {
                Console.WriteLine("\nЕще не было добавлено объектов");
            }
        }

        private static int ParseToInt()
        {
            string readString = Console.ReadLine();
            bool parseToInt = int.TryParse(readString, out int result);

            while (!parseToInt)
            {
                Console.WriteLine("\nВведите число");
                parseToInt = int.TryParse(Console.ReadLine(), out result);
            }
            return result;
        }

        private static DateTime ParseToDateTime()
        {
            string readString = Console.ReadLine();
            bool parseToInt = DateTime.TryParse(readString, out DateTime result);

            while (!parseToInt)
            {
                Console.WriteLine("\nВведите дату в формате день/месяц/год");
                parseToInt = DateTime.TryParse(Console.ReadLine(), out result);
            }
            return result;
        }

        private static void GroupByYears()
        {

            Lookup<int, LibraryObject> grouping = (Lookup<int, LibraryObject>)libraryObjectLogic.GroupingByPublishingYear();

            if (grouping.Count > 0)
            {
                foreach (IGrouping<int, LibraryObject> group in grouping)
                {
                    Console.WriteLine($"\nГод {group.Key}");
                    foreach (LibraryObject libraryObject in group)
                    {
                        if (libraryObject is Book)
                        {
                            Console.WriteLine($"\nКнига: {libraryObject}");
                        }                        
                    }
                }

            }
            else
            {
                Console.WriteLine("\nЕще не добавлено ни одного объекта");
            }
        }

        private static void GetBooksByPublishingHouseFilter()
        {

            Console.WriteLine("\nВведите название издательства или его начало");
            string publishingHouseFilter = Console.ReadLine();

            Lookup<string, Book> groups = (Lookup<string, Book>)bookLogic.GetAndGroupByPublishingHouse(publishingHouseFilter);

            if (groups.Count > 0)
            {
                foreach (IGrouping<string, Book> group in groups)
                {
                    Console.WriteLine($"\nИздательство {group.Key}");
                    foreach (Book book in group)
                    {
                        Console.WriteLine($"{book}");
                    }
                }
            }
            else
            {
                Console.WriteLine("\nНе найдено ни одной книги");
            }
        }

        private static Person FindPerson()
        {
            IEnumerable<Person> persons = personLogic.GetAll();
            Console.WriteLine("\nВыберите нужного автора и введите его ID");

            if (persons.Count() > 0)
            {
                foreach (Person person in persons)
                {
                    Console.WriteLine($"{person}");
                }

                Person author = new Person();

                while (author.Id <= 0)
                {
                    Console.WriteLine("\nВведите верный ID");
                    int personId = ParseToInt();
                    author = personLogic.GetById(personId);
                }

                return author;
            }
            else
            {
                Console.WriteLine("\nЕще не добавлено ни одного автора, вернитесь и выполните добавление автора");
                return null;
            }
        }

        private static void GetBooksByPerson()
        {
            Person author = FindPerson();

            if (author != null)
            {
                IEnumerable<LibraryObject> group = libraryObjectLogic.GetBooksPatentsByPerson(author.Id);

                if (group.Count() > 0)
                {
                    Console.WriteLine($"\nАвтор {author}");
                    foreach (LibraryObject libraryObject in group)
                    {
                        if (libraryObject is Book)
                        {
                            Console.WriteLine($"\nКнига: {libraryObject}");
                        }                        
                    }
                }
                else
                {
                    Console.WriteLine("\nНе найдено ни одного объекта");
                }
            }
        }

        private static void GetBooksByAuthor()
        {

            Person author = FindPerson();

            if (author != null)
            {
                IEnumerable<Book> group = bookLogic.GetByAuthor(author.Id);

                if (group.Count() > 0)
                {
                    Console.WriteLine($"\nАвтор {author}");
                    foreach (Book book in group)
                    {
                        Console.WriteLine($"\nКнига: {book}");
                    }
                }
                else
                {
                    Console.WriteLine("\nНе найдено ни одного объекта");
                }
            }
        }

        private static void SortObjects()
        {
            Console.WriteLine("\n" + @"Выберите вариант сортировки:
1. В прямом порядке.
2. В обратном порядке. ");

            while (true)
            {
                ConsoleKeyInfo action = Console.ReadKey();
                switch (action.Key)
                {
                    case ConsoleKey.D1:
                        DirectionSort();
                        return;
                    case ConsoleKey.D2:
                        ReverseSort();
                        return;
                    default:
                        Console.WriteLine("\nВыберите нужное действие");
                        break;
                }
            }
        }

        private static void ReverseSort()
        {
            IEnumerable<LibraryObject> group = libraryObjectLogic.SortingByYearReverseOrder();
            if (group.Count() > 0)
            {
                foreach (LibraryObject obj in group)
                {
                    if (obj is Book)
                    {
                        Console.WriteLine($"\nКнига: {obj}");
                    }                   
                }
            }
            else
                Console.WriteLine("\nЕще не добавлено ни одного объекта");
        }

        private static void DirectionSort()
        {
            IEnumerable<LibraryObject> group = libraryObjectLogic.SortingByYearDirectOrder();
            if (group.Count() > 0)
            {
                foreach (LibraryObject obj in group)
                {
                    if (obj is Book)
                    {
                        Console.WriteLine($"\nКнига: {obj}");
                    }                    
                }
            }
            else
                Console.WriteLine("\nЕще не добавлено ни одного объекта");
        }

        private static void GetObjectByTitle()
        {
            Console.WriteLine("\nВведите название");
            string title = Console.ReadLine();

            IEnumerable<LibraryObject> group = libraryObjectLogic.GetByTitle(title);

            if (group.Count() > 0)
            {
                foreach (LibraryObject book in group)
                {
                    Console.WriteLine($"\n{book}");
                }
            }
            else
            {
                Console.WriteLine("\nНе найдено ни одной книги");
            }
        }

        private static void GetAllObjects()
        {
            IEnumerable<LibraryObject> group = libraryObjectLogic.GetAll();

            if (group.Count() > 0)
            {
                foreach (LibraryObject obj in group)
                {
                    if (obj is Book)
                    {
                        Console.WriteLine($"\nКнига: {obj}");
                    }                    
                }

            }
            else
            {
                Console.WriteLine("\nЕще не добавлено ни одного объекта");
            }
        }

        private static void DeleteObject()
        {
            Console.WriteLine("\n" + @"Выберите, что вы хотите удалить:
0. Удалить книгу.
1. Удалить объект, отмеченный удаленным.");

            while (true)
            {
                ConsoleKeyInfo action = Console.ReadKey();
                switch (action.Key)
                {
                    case ConsoleKey.D0:
                        DeleteBook();
                        return;                    
                    case ConsoleKey.D1:
                        DeleteDeletedObject();
                        return;                   
                    default:
                        Console.WriteLine("\nВыберите нужное действие");
                        break;
                }
            }
        }

      

        private static void DeleteDeletedObject()
        {
            IEnumerable<LibraryObject> objs = libraryObjectLogic.GetAllDeletedObjects();
            List<int> deletedIds = new List<int>();

            if (objs.Any())
            {
                Console.WriteLine("\nВыберите нужный объект и введите его ID");
                foreach (LibraryObject obj in objs)
                {
                    if (obj is Book)
                    {
                        Console.WriteLine($"\nКнига: {obj}");
                    }                  
                    
                    deletedIds.Add(obj.Id);
                }

                int id = 0;

                while (!deletedIds.Any(d => d == id))
                {
                    Console.WriteLine("Введите верный ID");
                    id = ParseToInt();
                }

                bool deleteObj = libraryObjectLogic.Delete(id);

                if (deleteObj)
                {
                    Console.WriteLine("Удаление успешно");
                }
                else
                {
                    Console.WriteLine("Не удалось удалить объект, повторите попытку");
                }
            }

            else
            {
                Console.WriteLine("\nЕще не было добавлено объектов");
            }

        }

        private static void AddObject()
        {
            Console.WriteLine("\n" + @"Выберите, что вы хотите добавить:
0. Добавить автора или изобретателя.
1. Добавить книгу.
Enter для выхода в главное меню");

            while (true)
            {
                ConsoleKeyInfo action = Console.ReadKey();
                switch (action.Key)
                {
                    case ConsoleKey.D0:
                        AddPerson();
                        return;
                    case ConsoleKey.D1:
                        AddBook();
                        return;                    
                    default:
                        Console.WriteLine("\nВыберите нужное действие");
                        break;
                }
            }
        }

        private static void AddPerson()
        {
            Console.WriteLine("\nВведите имя");
            string name = Console.ReadLine();

            Console.WriteLine("\nВведите фамилию");
            string surname = Console.ReadLine();

            Person newPerson = new Person
            {
                Name = name,
                Surname = surname
            };

            try
            {
                int personId = personLogic.Add(newPerson);
            }
            catch (ObjectNotValidateException e)
            {
                Console.WriteLine("\nДопущены ошибки в следующих полях:");
                foreach (string back in e.BackMessageValidate)
                {
                    Console.WriteLine(back);
                }
                return;
            }
            Console.WriteLine("\nДобавление прошло успешно");
        }

        

        private static void AddBook()
        {
            Book book = new Book();

            Console.WriteLine("\nВведите название");
            book.Title = Console.ReadLine();

            Console.WriteLine("Введите число страниц");
            book.NumberOfPages = ParseToInt();

            Console.WriteLine("Введите год публикации");
            book.PublishingYear = ParseToInt();

            Console.WriteLine("Введите заметку");
            book.Note = Console.ReadLine();

            Console.WriteLine("Введите город издания");
            book.PublishingCity = Console.ReadLine();

            Console.WriteLine("Введите название издательства");
            book.PublishingHouse = Console.ReadLine();

            Console.WriteLine("Для выбора автора введите 0, иначе - 1");
            int choice = ParseToInt();
            Person author = new Person();
            while (choice == 0)
            {
                switch (choice)
                {
                    case 0:
                        author = FindPerson();
                        if (author != null)
                        {
                            bool haveAuthor = book.Authors.Any(p => p.Id == author.Id);
                            if (haveAuthor)
                            {
                                Console.WriteLine("Изобретатель уже добавлен");
                            }
                            else
                            {
                                book.Authors.Add(author);
                            }
                            Console.WriteLine("\nДля выбора еще одного автора введите 0, иначе - 1");
                            choice = ParseToInt();
                        }
                        else
                            Console.WriteLine("\nНет авторов в списке");
                        break;
                    case 1:
                        break;
                    default:
                        Console.WriteLine("\nВыберите действие");
                        break;
                }
            }
            

            try
            {
                int bookId = bookLogic.Add(book);
            }
            catch (ObjectNotValidateException e)
            {
                Console.WriteLine("\nДопущены ошибки в следующих полях:");
                foreach (string back in e.BackMessageValidate)
                {
                    Console.WriteLine(back);
                }
                return;
            }
            catch (ObjectNotUniqueException)
            {
                Console.WriteLine("Объект уже существует");
                return;
            }
            catch (Exception)
            {
                Console.WriteLine("Произошла ошибка, повторите ввод данных");
                return;
            }

            Console.WriteLine("\nДобавление прошло успешно");

        }    
              

        private static void DeleteBook()
        {

            IEnumerable<Book> books = bookLogic.GetAll();

            if (books.Any())
            {
                Console.WriteLine("\nВыберите нужную книгу и введите её ID");
                foreach (Book book in books)
                {
                    Console.WriteLine($"{book}");
                }

                int bookId = 0;

                Book bookForDelete = new Book();

                while (bookForDelete == null)
                {
                    Console.WriteLine("Введите верный ID");
                    bookId = ParseToInt();
                    bookForDelete = bookLogic.GetById(bookId);
                }

                bool deleteObj = libraryObjectLogic.Delete(bookId);

                if (deleteObj)
                {
                    Console.WriteLine("Удаление успешно");
                }
                else
                {
                    Console.WriteLine("Не удалось удалить объект, повторите попытку");
                }
            }

            else
            {
                Console.WriteLine("\nЕще не было добавлено объектов");
            }
        }
    }
}