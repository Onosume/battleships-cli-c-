// battleships-cli
// GameProcessor.cs
// Author: Matthew Tinn

using System;
using System.Collections.Generic;
using System.Threading;
using battleships_cli.Application.GameObjects.Player;
using battleships_cli.Application.GameObjects.Battleship;
using battleships_cli.Application.GameObjects.GameBoard;

namespace battleships_cli.Application.GameProcessor
{
    /// <summary>
    /// Class that contains the game logic
    /// </summary>
    public class GameProcessor
    {
        public const int NUM_PLAYER_CANNONBALLS = 24;
        public const int NUM_BATTLESHIPS = 3;
        public const int MAX_INPUT = 3;

        private Player player;
        private GameBoard gameBoard;
        private Battleship[] battleships; // Array of battleships
        private char[] coordinateInput = new char[MAX_INPUT]; // Array for characters from input stream
        private bool gameOver;

        private Random[] rands = new Random[NUM_BATTLESHIPS*2];

        // Accessor Methods
        public bool GameOver { get { return gameOver; } }

        /// <summary>
        /// Constructor
        /// </summary>
        public GameProcessor()
        {
            CreateGame();
        }

        /// <summary>
        /// Initialises the game
        /// </summary>
        public void CreateGame()
        {
            gameOver = false;
            CreateGameBoard();
            CreatePlayer();
            SeedRands();
            CreateBattleships();
        }

        /// <summary>
        /// Initialises the game board
        /// </summary>
        public void CreateGameBoard()
        {
            gameBoard = new GameBoard();
        }

        /// <summary>
        /// Initialises the player
        /// </summary>
        public void CreatePlayer()
        {
            player = new Player(NUM_PLAYER_CANNONBALLS);
        }

        /// <summary>
        /// Seeds the Random objects
        /// </summary>
        public void SeedRands()
        {
            for (int i = 0; i < NUM_BATTLESHIPS*2; i++)
            {
                Thread.Sleep(100);
                rands[i] = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            }
        }

        /// <summary>
        /// Initialise the battleships
        /// </summary>
        public void CreateBattleships()
        {
            battleships = new Battleship[NUM_BATTLESHIPS];

            int currentRandIndex1 = 0;
            int currentRandIndex2 = 1;

            battleships[0] = new Battleship(GeneratePosition(5, currentRandIndex1, currentRandIndex2));

            for (int i = 1; i < NUM_BATTLESHIPS; i++)
            {
                currentRandIndex1 += 2;
                currentRandIndex2 += 2;

                battleships[i] = new Battleship(GeneratePosition(4, currentRandIndex1, currentRandIndex2));
            }
        }

        /// <summary>
        /// Generates the cell positions for a battleship
        /// </summary>
        /// <param name="battleshipSize">The size of the battleship</param>
        /// <returns>List of positions</returns>
        public List<string> GeneratePosition(int battleshipSize, int randIndex1, int randIndex2)
        {
            List<string> cells = new List<string>();
            string[] usedPositions = new string[battleshipSize];

            string startColumn = "";
            char direction = 'a';

            string cell = "";
            int min = 0;
            int max = 0;

            // Generate a random starting point
            // Generate a rand 65 - 74
            min = 65;
            max = 74;
            Random rand1 = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            char randChar = (char)rands[randIndex1].Next(min, max);

            // Generate a rand number 1 - 10 as an ASCII character
            min = 48;
            max = 57;
            Random rand2 = new Random((int)DateTime.Now.Ticks & 0x0000FFFF);
            char randNum = (char)rands[randIndex2].Next(min, max);

            if (randNum == '0')
            {
                // If we get 0 we convert it to 10
                cell += randChar;
                cell += "10";
                startColumn += "10";
            }
            else
            {
                cell += randChar;
                cell += randNum;
                startColumn += randNum;
            }

            cells.Add(cell);
            usedPositions[0] = cell;

            // Choose whether to go down, left, right or up

            char downCell = (char)(randChar + battleshipSize);
            char upCell = (char)(randChar - battleshipSize);
            char leftCell, rightCell;

            if (randNum == '0')
            {
                leftCell = (char)((char)('9' + 1) - battleshipSize);
                rightCell = (char)((char)('9' + 1) + battleshipSize);
            }
            else
            {
                leftCell = (char)(randNum - battleshipSize);
                rightCell = (char)(randNum + battleshipSize);
            }

            if (downCell <= 'J')
            {
                direction = 'd';
            }
            else if (leftCell >= '1')
            {
                direction = 'l';
            }
            else if (rightCell <= (char)('9' + 1))
            {
                direction = 'r';
            }
            else if (upCell >= 'A')
            {
                direction = 'u';
            }

            for (int cellCount = 1; cellCount < battleshipSize; cellCount++)
            {
                string nextCell = "";

                if (direction == 'd')
                {
                    char nextRow = (char)(randChar + cellCount);
                    nextCell += nextRow;
                    nextCell += startColumn;
                }
                else if (direction == 'l')
                {
                    char nextColumn;

                    if (randNum == '0')
                    {
                        nextColumn = (char)((char)('9' + 1) - cellCount);
                    }
                    else
                    {
                        nextColumn = (char)(randNum - cellCount);
                    }

                    nextCell += randChar;
                    nextCell += nextColumn;
                }
                else if (direction == 'r')
                {
                    char nextColumn = (char)(randNum + cellCount);
                    nextCell += randChar;
                    if (nextColumn == (char)('9' + 1))
                    {
                        nextCell += "10";
                    }
                    else
                    {
                        nextCell += nextColumn;
                    }
                }
                else if (direction == 'u')
                {
                    char nextRow = (char)(randChar - cellCount);
                    nextCell += nextRow;
                    nextCell += startColumn;
                }

                cells.Add(nextCell);
                usedPositions[cellCount] = nextCell;
            }

            return cells;
        }

        /// <summary>
        /// Print the intro text to the console
        /// </summary>
        public void WriteIntro()
        {
            Console.WriteLine("***  **** *** *** *   *** *** *  * * *** ***");
            Console.WriteLine("*  * *  *  *   *  *   *   *   *  * * * * *");
            Console.WriteLine("* *  ****  *   *  *   *** *** **** * **  ***");
            Console.WriteLine("**   *  *  *   *  *   *     * *  * * *     *");
            Console.WriteLine("* *  *  *  *   *  *   *     * *  * * *     *");
            Console.WriteLine("*  * *  *  *   *  *   *     * *  * * *     *");
            Console.WriteLine("***  *  *  *   *  *** *** *** *  * * *   ***");

            Console.WriteLine("Sink all of the enemy ships!");
            Console.WriteLine("When prompted enter a coordinate (A-J, 1-10) e.g. A5.");
            Console.WriteLine("Hit all of the ships before your cannonballs run out!");
        }

        /// <summary>
        /// Display information about the current game
        /// </summary>
        public void DisplayCurrentGame()
        {
            Console.WriteLine("Cannonballs remaining: " + player.Cannonballs);

            Console.WriteLine("\nCurrent board:");
            gameBoard.Print();
        }

        /// <summary>
        /// Take coordinates from player
        /// </summary>
        public void TakePlayerInput()
        {
            Console.WriteLine("Enter a coordinate:");
            coordinateInput = Console.ReadLine().ToCharArray();
        }

        /// <summary>
        /// Determine if the player has hit a ship; perform game logic
        /// </summary>
        /// <returns></returns>
        public bool Shoot()
        {
            bool validAction = true;

            int positionIndexX = 0;
            int positionIndexY = 0;

            // Convert the input into coordinates
            positionIndexX = ConvertCharacterToIndex(coordinateInput[0]);

            if (coordinateInput.Length == 3 && coordinateInput[1] == '1' && coordinateInput[2] == '0')
            {
                positionIndexY = 9;
            }
            else
            {
                positionIndexY = ConvertCharacterToIndex(coordinateInput[1]);
            }

            if (positionIndexX == -1 || positionIndexY == -1)
            {
                validAction = false;
            }

            if (!validAction)
            {
                Console.WriteLine("You entered an invalid character.");
                return false;
            }

            player.TakeShot();

            // If the characters are valid, check the position markers of the ships
            // If the positions match it is a hit
            string hitPosition = new string(coordinateInput);

            bool hit = false;

            for (int i = 0; i < NUM_BATTLESHIPS; i++)
            {
                string checkPosition;

                List<string> positionList = new List<string>(battleships[i].Position);

                int index = 0;

                foreach(var position in positionList)
                {
                    if (position == hitPosition)
                    {
                        hit = true;

                        player.AddToScore(10);

                        if(!battleships[i].Sunk)
                        {
                            battleships[i].TakeDamageOn(index);
                        }
                    }

                    index++;
                }
            }

            // Update the game board
            if (hit)
            {
                gameBoard.SetCell(positionIndexX, positionIndexY, gameBoard.HitMarker());
                Console.WriteLine("\nIt's a hit!\n");
            }
            else
            {
                gameBoard.SetCell(positionIndexX, positionIndexY, gameBoard.MissMarker());
                Console.WriteLine("\nIt's a miss!\n");
            }

            if (player.Cannonballs == 0)
            {
                Console.WriteLine("\nYou ran out of cannonballs!\n");
                gameOver = true;
            }

            return true;
        }

        /// <summary>
        /// Converts char A-Z or a-z to an int
        /// </summary>
        /// <param name="character">Character to convert</param>
        /// <returns>Converted interger</returns>
        public int ConvertCharacterToIndex(char character)
        {
            if (character == 'A' || character == 'a' || character == '1')
            {
                return 0;
            }
            else if (character == 'B' || character == 'b' || character == '2')
            {
                return 1;
            }
            else if (character == 'C' || character == 'c' || character == '3')
            {
                return 2;
            }
            else if (character == 'D' || character == 'd' || character == '4')
            {
                return 3;
            }
            else if (character == 'E' || character == 'e' || character == '5')
            {
                return 4;
            }
            else if (character == 'F' || character == 'F' || character == '6')
            {
                return 5;
            }
            else if (character == 'G' || character == 'g' || character == '7')
            {
                return 6;
            }
            else if (character == 'H' || character == 'h' || character == '8')
            {
                return 7;
            }
            else if (character == 'I' || character == 'i' || character == '9')
            {
                return 8;
            }
            else if (character == 'J' || character == 'J')
            {
                return 9;
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Outputs the player score
        /// </summary>
        public void OutputPlayerScore()
        {
            Console.WriteLine("\nYour current score is: \n" + player.Score + "\n");
        }

        /// <summary>
        /// Checks if player has won the game
        /// </summary>
        /// <returns>True/false on game win state</returns>
        public bool AllShipsSunk()
        {
            if (battleships[0].Sunk && battleships[1].Sunk && battleships[2].Sunk)
            {
                Console.WriteLine("\nYou sunk all of the ships!\n");
                gameOver = true;
                return true;
            }

            return false;
        }
    }
}
