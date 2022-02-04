using System;

namespace Stone_Scabbard_Paper
{
    class Program
    {
        static void Main(string[] args)
        {

            Gamer gamer = new Gamer("Иван");
            Gamer gamer2 = new Gamer("Петя");
            Game game = new Game(gamer, gamer2);

            while (true)
            {
                StartGame(game);

                Console.WriteLine("Для выхода введите *");
                if (Console.ReadLine() == "*")
                {
                    return;
                }
            }
        }

        private static void StartGame(Game game)
        {
            Console.WriteLine(game.StepGame());
            Console.WriteLine(game.StatusGame());
        }
    }

    public class Game
    {
        public Gamer Gamer1 { get; set; }
        public Gamer Gamer2 { get; set; }

        private  string[] variants = new string[]
        {
            "Камень" , "Ножницы" , "Бумага"
        };

        public Game(Gamer gamer1, Gamer gamer2)
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


        public string StepGame()
        {
            Gamer1.Option = GetVariant();
            Gamer2.Option = GetVariant();
            string rez = GameLogik(Gamer1, Gamer2);
            return $"Игрок {Gamer1.Name} - { Gamer1.Option}, {Gamer2.Name} - {Gamer2.Option}\n" + rez;
        }

        private string GameLogik(Gamer gamer1, Gamer gamer2)
        {
            int i = GetCodeStep(Gamer1.Option);
            int j = GetCodeStep(gamer2.Option);

            int[,] vs = new int[,]
            {
                { 0 ,1, -1},
                { -1 ,0, 1},
                { 1, -1, 0},
            };

            if ( vs[i,j] == 0)
            {
                Gamer1.Friendship++;
                Gamer2.Friendship++;
                return "Ничья";
            }

            if (vs[i, j] == 1)
            {
                Gamer1.Victiry++;
                Gamer2.Defeat++;
                return $"{ Gamer1.Name} победил";
            }

            if (vs[i, j] == -1)
            {
                Gamer1.Defeat++;
                Gamer2.Victiry++;
                return $"{ Gamer2.Name} победил";
            }

            return string.Empty;
        }

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

        private string GetVariant()
        {
            Random random = new Random();
            return  variants[random.Next(0, variants.Length)];
        }

        public string StatusGame ()
        {
            return Gamer1.PrintInfo() + "\n" + Gamer2.PrintInfo();
        }
    }

        public class Gamer
        {
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

       public string  Name { get; set; }
       public string  Option { get; set; }
       
       public int   Victiry { get; set; }

       public int Defeat { get; set; }

       public int Friendship { get; set; }


      public string PrintInfo()
      {
        return $"Игрок {Name} имеет  побед:{Victiry} проигрышей:{Defeat}  ничейных результатов:{Friendship}";
      }
    }
}
