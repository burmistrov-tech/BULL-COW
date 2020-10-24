using System;
using System.Linq;

namespace BULL_COW
{
    class Game
    {
        public Game(int numberOfLives, int numberOfDigits)
        {
            NumberOfLives = numberOfLives;
            NumberOfDigits = numberOfDigits;

            Console.CancelKeyPress += delegate (object sender, ConsoleCancelEventArgs e)
            {
                e.Cancel = true;
            };
        }

        public int NumberOfLives { get; }
        public int NumberOfDigits { get; }
        private int Lives { get; set; }
        private RandomNumber Unknown { get; set; }
        private GameStatus Status { get; set; }

        public void Start()
        {            
            while (true)
            {
                ResetToDefault();
                try
                {
                    Run();
                    Over();
                }
                catch (ArgumentException e)
                {
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.WriteLine($"\n{e.ParamName}");
                    Console.WriteLine("The game will start again");
                    Console.ResetColor();
                }
                finally
                {
                    TryToExit();
                }
            }
        }

        private void Run()
        {
            Console.WriteLine($"The system made a {Unknown.Length}-digit number.");
            while (Status == GameStatus.InProcess)
            {
                Console.Write("Try to guess: ");
                int[] answer = PrepareAnswer(Console.ReadLine() ??
                    throw new ArgumentNullException("You interrupted the input"));

                (BULL_COW, int)[] result = Unknown.CompareTo(answer);
                if (result.Length == NumberOfDigits && result.All(x => x.Item1 == BULL_COW.BULL))
                    Status = GameStatus.Won;
                else
                    Lives--;

                Array.ForEach(result, x =>
                    Console.Write($"[{x.Item1} - {x.Item2}]"));

                if (Lives <= 0)
                    Status = GameStatus.Lost;

                Console.WriteLine($"\nYou have {Lives} lives left");
            }
        }
        private void Over()
        {
            Console.BackgroundColor = Status == GameStatus.Won ?
                ConsoleColor.Green :
                ConsoleColor.Red;
            Console.WriteLine("Game Over!");
            Console.ResetColor();
            Console.ReadKey();
        }

        private void TryToExit()
        {
            Console.WriteLine("If you want to exit press any button, but if want to play again press \"Y\"");
            var key = Console.ReadKey().Key;

            if (key != ConsoleKey.Y)
                Environment.Exit(0);

            Console.WriteLine();
        }

        private int[] PrepareAnswer(string answer)
        {
            return Array.ConvertAll(answer.Trim()
                        .Where(x => char.IsDigit(x))
                        .Select(x => x.ToString())
                        .ToArray(), int.Parse);
        }

        private void ResetToDefault()
        {
            Status = GameStatus.InProcess;
            Unknown = new RandomNumber(NumberOfDigits);
            Lives = NumberOfLives;
        }

        private enum GameStatus
        {
            Won,
            Lost,
            InProcess
        }
    }
}
