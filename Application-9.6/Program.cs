namespace ExceptionCustom
{
    public class OneException : Exception
    {
        public OneException(string message) : base(message)
        {
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Exception[] exceptions = new Exception[]
            {
                new OneException("Исключение"),
                new ArgumentOutOfRangeException("Исключение ArgumentOutOfRangeException"),
                new ArgumentNullException("Исключение ArgumentNullException"),
                new DivideByZeroException("Исключение DivideByZeroException"),
                new DirectoryNotFoundException("Исключение DirectoryNotFoundException")
            };

            foreach (var ex in exceptions)
            {
                try
                {
                    throw ex;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Выполнено исключение: {e.Message}");
                }
                finally
                {
                    Console.WriteLine("Блок finally выполнен");
                }
            }

            Console.ReadLine();
        }
    }
}

namespace Surname
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message) { }
    }

    public class SortingEventArgs : EventArgs
    {
        public int SortOrder { get; set; }
    }

    public class Sorter
    {
        public event EventHandler<SortingEventArgs> SortEvent;

        public void OnSortEvent(int sortOrder)
        {
            SortEvent?.Invoke(this, new SortingEventArgs { SortOrder = sortOrder });
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<string> surnames = new List<string> { "Иванов", "Смирнов", "Кузнецов", "Гагарин", "Крылов" };
            Sorter sorter = new Sorter();
            sorter.SortEvent += (sender, e) =>
            {
                if (e.SortOrder == 1)
                {
                    surnames.Sort();
                }
                else if (e.SortOrder == 2)
                {
                    surnames.Sort();
                    surnames.Reverse();
                }
            };

            Console.WriteLine("Нажмите 1 для сортировки от А-Я, нажмите 2 для сортировки от Я-А.");

            try
            {
                int sortOrder = int.Parse(Console.ReadLine());

                if (sortOrder != 1 && sortOrder != 2)
                {
                    throw new CustomException("Повторите ввод, число должно быть 1/2");
                }

                sorter.OnSortEvent(sortOrder);

                Console.WriteLine("Сортировка фамилий:");
                foreach (string surname in surnames)
                {
                    Console.WriteLine(surname);
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Неправильный ввод, введите число");
            }
            catch (CustomException ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Нажмите Enter для выхода.");
                Console.ReadKey();
            }
        }
    }
}