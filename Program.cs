using System.Collections.Concurrent;

namespace Librarian
{
    internal class Program
    {
        private static ConcurrentDictionary<string, int> _dictionary = new ConcurrentDictionary<string, int>();
        static void Main(string[] args)
        {
            int percent = 0;
            var bookName = "";
            while (true)
            {
                Console.WriteLine("1 - добавить книгу\n2 - вывести список непрочитанного\n3 - выйти");
                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine("Введите название книги:");
                        bookName = Console.ReadLine();
                        if (!_dictionary.TryAdd(bookName, percent))
                            break;
                        break;
                    case "2":
                        GetAllBooks();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                }
            }
        }
        static void GetAllBooks()
        {
            foreach (var book in _dictionary)
            {
                Parallel.
                Console.WriteLine($"{book.Key} - {book.Value}%");
            }
        }

    }
}