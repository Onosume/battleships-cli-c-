using System;
using battleships_cli.Application.GameProcessor;

namespace battleships_cli
{
    class Program
    {
        static void Main(string[] args)
        {
            GameProcessor gameProcessor = new GameProcessor();

            gameProcessor.WriteIntro();

            while (!gameProcessor.GameOver)
            {
                gameProcessor.DisplayCurrentGame();

                bool validInput = false;

                while(!validInput)
                {
                    gameProcessor.TakePlayerInput();
                    validInput = gameProcessor.Shoot();
                }

                gameProcessor.OutputPlayerScore();
                gameProcessor.AllShipsSunk();
            }

            string exitString;
            Console.WriteLine("That's the end of the game. Press any key and enter to exit.");
            exitString = Console.ReadLine();
        }
    }
}
