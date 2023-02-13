using System;

namespace Stone_Scabbard_Paper
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Игра камень ножны  бумага");

                Gamer gamer = new Gamer("Иван"); // подготовка  
                Gamer gamer2 = new Gamer("Петя"); // подготовка  

                Console.WriteLine($"Играют {gamer.Name} - vs {gamer2.Name}");
                Game game = new Game(gamer, gamer2); // подготовка  
                int x = 0;
                while (true) // повторение  игры 
                {
                    x++; // ход 
                    Console.WriteLine($"Раунд {x}:"); 
                    StartGame(game); // передаем  стартовые  параметры
                    Console.WriteLine("Что  бы  продолжить  битву нажмите  ENTER");
                    Console.WriteLine("Для выхода введите *");
                    if (Console.ReadLine() == "*")
                    {
                        return;
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// одна игра
        /// </summary>
        /// <param name="game"></param>
        private static void StartGame(Game game)
        {
            Console.WriteLine(game.StepGame()); // получили результат  игры
            Console.WriteLine(game.StatusGame()); // Запросили статус игроков 
        }
    }
    /// <summary>
    /// Игра
    /// </summary>
    public class Game
    {
        public Gamer Gamer1 { get; set; } // игрок 1 
        public Gamer Gamer2 { get; set; } // игрок 2

        private  string[] variants = new string[] // варианты  
        {
            "Камень" , "Ножницы" , "Бумага"
        };

        public Game(Gamer gamer1, Gamer gamer2)  // конструктор 
        {
            try
            {
                Gamer1 = gamer1 ?? throw new ArgumentNullException("Пустой объект");
                Gamer2 = gamer2 ?? throw new ArgumentNullException("Пустой объект");
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public string StepGame() // ход игроков 
        {
            Gamer1.Option = GetVariant(); // Вариант  первого игрока 
            Gamer2.Option = GetVariant(); // Вариант  второго игрока 
            string rez = GameLogik(Gamer1, Gamer2); // логика  игры 
            return $"Игрок {Gamer1.Name} - { Gamer1.Option}, {Gamer2.Name} - {Gamer2.Option}\n" + rez; // результат  ишры
        }

        /// <summary>
        /// Логика  игры
        /// </summary>
        /// <param name="gamer1"></param>
        /// <param name="gamer2"></param>
        /// <returns></returns>
        private string GameLogik(Gamer gamer1, Gamer gamer2)
        {
            int i = GetCodeStep(Gamer1.Option); // получаем  индекс маски 
            int j = GetCodeStep(gamer2.Option); // получаем  индекс маски 

            int[,] vs = new int[,]  // маска  побед
            {
                { 0 ,1, -1},
                { -1 ,0, 1},
                { 1, -1, 0},
            };

            if ( vs[i,j] == 0)  // если ничья 
            {
                Gamer1.Friendship++;
                Gamer2.Friendship++;
                return "Ничья";
            }

            if (vs[i, j] == 1) // если победа  1 игрока 
            {
                Gamer1.Victiry++;
                Gamer2.Defeat++;
                return $"{ Gamer1.Name} победил";
            }

            if (vs[i, j] == -1) // если  проигрыш   1 игрока 
            {
                Gamer1.Defeat++;
                Gamer2.Victiry++;
                return $"{ Gamer2.Name} победил";
            }

            return string.Empty;
        }


        /// <summary>
        /// индекс  варианта по  строке 
        /// </summary>
        /// <param name="option"></param>
        /// <returns></returns>
        private int GetCodeStep(string option) 
        {
            switch (option)
            {
                case "Камень": return  0; break;
                case "Ножницы": return 1; break;
                case "Бумага": return 2; break;
                default: throw new Exception("Eror");
            }
        }

        /// <summary>
        /// Вариант  в  текстовом  представлении 
        /// </summary>
        /// <returns></returns>
        private string GetVariant()  
        {
            Random random = new Random();
            return  variants[random.Next(0, variants.Length)];
        }

        /// <summary>
        /// Статус  игры
        /// </summary>
        /// <returns>Общий  счет игры</returns>
        public string StatusGame ()
        {
            return Gamer1.PrintInfo() + "\n" + Gamer2.PrintInfo();
        }
    }
      
    public class Gamer
    {
        public string Name { get; set; }
        public string Option { get; set; }

        /// <summary>
        /// победы
        /// </summary>
        public int Victiry { get; set; }

        /// <summary>
        /// поражения
        /// </summary>
        public int Defeat { get; set; }
        /// <summary>
        /// ничьи
        /// </summary>
        public int Friendship { get; set; }

        public Gamer(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentException($"Имя  не может быть пустым или содержать только пробелы");
            }
            Name = name;
            Option = string.Empty;
            Victiry = 0;
            Defeat = 0;
            Friendship = 0;
        }

        public string PrintInfo()
        {
            return $"Игрок {Name} имеет  побед:{Victiry} проигрышей:{Defeat}  ничейных результатов:{Friendship}";
        }
    }
}
