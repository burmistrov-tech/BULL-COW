namespace BULL_COW
{
    class Program
    {
        static void Main(string[] args)
        {
            const int numberOfLives = 5;
            const int numberOfDigits = 4;

            var game = new Game(numberOfLives, numberOfDigits);

            game.Start();
        }
    }
}
