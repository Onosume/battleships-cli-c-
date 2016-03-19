// battleships-cli
// Program.cs
// Author: Matthew Tinn

using System;
using battleships_cli.Application.GameProcessor;

namespace battleships_cli
{
    /// <summary>
    /// The main application class
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main game loop
        /// </summary>
        /// <param name="args"></param>
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

            // Holds the console and stops it from exiting automatically
            string exitString;
            Console.WriteLine("That's the end of the game. Press any key and enter to exit.");
            exitString = Console.ReadLine();
        }
    }
}
