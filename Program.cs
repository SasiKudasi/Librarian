using System.Collections.Concurrent;

namespace Librarian
{
    internal class Program
    {
        private static ConcurrentDictionary<string, int> _dictionary = new ConcurrentDictionary<string, int>();
        static async Task Main(string[] args)
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
                        RecalculationPercent();
                        break;
                    case "2":

                        GetAllBook();
                        break;
                    case "3":
                        Environment.Exit(0);
                        break;
                }
            }
        }
        static void GetAllBook()
        {
            foreach (var book in _dictionary)
            {
                Console.WriteLine($"{book.Key} - {book.Value}%");
            }
        }

        static void RecalculationPercent()
        {
            new Thread(() =>
            {
                while (true)
                {
                    var full = 100;
                    Parallel.ForEach(_dictionary.Keys, (key) =>
                    {
                        _dictionary[key]++;
                        if (_dictionary[key] == full)
                        {
                            _dictionary.TryRemove(key, out full);
                        }

                    });
                    Thread.Sleep(1000);
                }
            }).Start();
        }
    }
}