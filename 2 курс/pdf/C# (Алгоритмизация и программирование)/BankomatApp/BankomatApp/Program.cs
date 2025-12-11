using System;
using System.Collections.Generic;

namespace BankomatApp
{
    //Класс Банкнота
    class Banknote
    {
        public int Denomination { get; set; } // Номинал купюры
        public int Count { get; set; }        // Количество

        //Конструктор
        public Banknote(int denomination, int count)
        {
            Denomination = denomination;
            Count = count;
        }
    }
    //Класс Банкномата
    class Bankomat
    {
        //List всех банкнот
        private List<Banknote> _banknots = new List<Banknote>();

        // Инициализация банкомата
        public void Initialize(int count5000, int count2000, int count1000,
                               int count500, int count200, int count100)
        {
            _banknots.Clear();
            _banknots.Add(new Banknote(5000, count5000));
            _banknots.Add(new Banknote(2000, count2000));
            _banknots.Add(new Banknote(1000, count1000));
            _banknots.Add(new Banknote(500, count500));
            _banknots.Add(new Banknote(200, count200));
            _banknots.Add(new Banknote(100, count100));
        }

        // Метод выдачи денег
        public Dictionary<int, int> TryWithdraw(int amount)
        {
            //Условие на проверку суммы > 0 и сумма кратна 100р
            if (amount <= 0 || amount % 100 != 0)
                return null;


            //Хранение количества купюр, которые будут выданы
            var result = new Dictionary<int, int>();
            foreach (var note in _banknots)
                result[note.Denomination] = 0;

            //Банкомат пытается выдать как можно больше крупных купюр (начиная с самых больших номиналов)
            foreach (var note in _banknots)
            {
                int maxPossible = Math.Min(note.Count, amount / note.Denomination);
                result[note.Denomination] = maxPossible;
                amount -= maxPossible * note.Denomination;

                if (amount == 0)
                    break;
            }

            if (amount != 0)
                return null;

            // Обновляем количество купюр в банкомате
            foreach (var note in _banknots)
            {
                note.Count -= result[note.Denomination];
            }

            return result;
        }

        // Получить текущее состояние банкомата
        public Dictionary<int, int> GetState()
        {
            var state = new Dictionary<int, int>();
            foreach (var b in _banknots)
                state[b.Denomination] = b.Count;
            return state;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Bankomat bankomat = new Bankomat();

            Console.WriteLine("Введите начальное состояние банкомата:");

            int fiveThousand = ReadInt("Количество купюр номиналом 5000 рублей: ");
            int twoThousand = ReadInt("Количество купюр номиналом 2000 рублей: ");
            int oneThousand = ReadInt("Количество купюр номиналом 1000 рублей: ");
            int fiveHundred = ReadInt("Количество купюр номиналом 500 рублей: ");
            int twoHundred = ReadInt("Количество купюр номиналом 200 рублей: ");
            int oneHundred = ReadInt("Количество купюр номиналом 100 рублей: ");

            bankomat.Initialize(fiveThousand, twoThousand, oneThousand, fiveHundred, twoHundred, oneHundred);

            int amount = ReadInt("Введите сумму для выдачи: ");

            var withdrawn = bankomat.TryWithdraw(amount);

            if (withdrawn == null)
            {
                Console.WriteLine("Невозможно выдать указанную сумму");
            }
            else
            {
                Console.WriteLine("\nВыдано:");
                int total = 0;
                foreach (var pair in withdrawn)
                {
                    int value = pair.Key * pair.Value;
                    if (pair.Value > 0)
                        Console.WriteLine($"Количество купюр номиналом {pair.Key} рублей – {pair.Value} шт.");
                    total += value;
                }
                Console.WriteLine($"Всего выдано – {total} рублей.");

                Console.WriteLine("\nОстаток купюр в банкомате:");
                var state = bankomat.GetState();
                foreach (var pair in state)
                {
                    Console.WriteLine($"Количество купюр номиналом {pair.Key} рублей – {pair.Value} шт.");
                }
            }
        }

        // Утилитный метод для ввода целых чисел
        static int ReadInt(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine();
                if (int.TryParse(input, out int result) && result >= 0)
                    return result;
                Console.WriteLine("Ошибка ввода. Введите корректное неотрицательное число.");
            }
        }
    }
}