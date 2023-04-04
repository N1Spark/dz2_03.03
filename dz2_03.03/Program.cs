using System;


namespace dz2_03._03
{
    class Travel
    {
        public IStrategy Strategy;
        public DateTime YourTime { get; set; }
        public int Money { get; set; }
        public Travel(IStrategy strategy)
        {
            Strategy = strategy;
        }
        public void SetStrategy(IStrategy strategy)
        {
            Strategy = strategy;
        }
        public void BusinessLogic()
        {
            Console.WriteLine("Сколько времени? (Формат записи: чч:мм:сс)");
            string input = Console.ReadLine();
            bool isValidTime = DateTime.TryParse(input, out DateTime time);

            while (!isValidTime)
            {
                Console.WriteLine("Некорректный ввод");
                input = Console.ReadLine();
                isValidTime = DateTime.TryParse(input, out time);
            }

            YourTime = time;

            Console.WriteLine("Сколько денег?");
            Money = Convert.ToInt32(Console.ReadLine());

            if (Money < Strategy.Money && YourTime < Strategy.Time)
            {
                Console.WriteLine("У вас недостаточно денег и времени для этой поездки");
                return;
            }
            if (Money < Strategy.Money)
            {
                Console.WriteLine("У вас недостаточно денег");
                return;
            }
            if (YourTime < Strategy.Time)
            {
                Console.WriteLine("Вы не успеете до аэропорта");
                return;
            }
            Console.WriteLine($"Вы доберетесь до аэропорта: {Strategy.ChoiceTransport()}");


        }
    }
    public interface IStrategy
    {
        DateTime Time { get; set; }
        int Money { get; set; }
        string ChoiceTransport();
    }
    class Bicycle : IStrategy
    {
        public DateTime Time { get; set; }
        public int Money { get; set; }
        public Bicycle()
        {
            DateTime now = DateTime.Now;
            DateTime timeOnly = new DateTime(now.Year, now.Month, now.Day, 2, 30, 0);
            Time = timeOnly;
            Money = 0;
        }
        public string ChoiceTransport()
        {
            return "Bicycle";
        }
    }
    class Bus : IStrategy
    {
        public DateTime Time { get; set; }
        public int Money { get; set; }
        public Bus()
        {
            DateTime now = DateTime.Now;
            DateTime timeOnly = new DateTime(now.Year, now.Month, now.Day, 1, 30, 0);
            Time = timeOnly;
            Money = 50;
        }
        public string ChoiceTransport()
        {
            return "Bus";
        }
    }
    class Taxi : IStrategy
    {
        public DateTime Time { get; set; }
        public int Money { get; set; }
        public Taxi()
        {
            DateTime now = DateTime.Now;
            DateTime timeOnly = new DateTime(now.Year, now.Month, now.Day, 0, 40, 0);
            Time = timeOnly;
            Money = 260;
        }
        public string ChoiceTransport()
        {
            return "Taxi";
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            IStrategy strategy = new Taxi();
            Travel travel = new Travel(strategy);
            travel.BusinessLogic();
        }
    }
}
