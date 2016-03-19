using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using battleships_cli.Application.GameObjects.Player;
using battleships_cli.Application.GameObjects.Battleship;
using battleships_cli.Application.GameObjects.GameBoard;

namespace battleships_cli.Application.GameProcessor
{
    public class GameProcessor
    {
        public const int NUM_PLAYER_CANNONBALLS = 24;
        public const int NUM_BATTLESHIPS = 3;
        public const int MAX_INPUT = 3;

        private Player player;
        private GameBoard gameBoard;
        private Battleship[] battleships;
        private char[] coordinateInput = new char[MAX_INPUT];
        private bool gameOver;

        // Accessor Methods
        public bool GameOver { get; }


        public GameProcessor()
        {
            CreateGame();
        }

        public void CreateGame()
        {
            gameOver = false;
            CreateGameBoard();
            CreatePlayer();
            CreateBattleships();
        }

        public void CreateGameBoard()
        {
            gameBoard = new GameBoard();
        }

        public void CreatePlayer()
        {
            player = new Player(NUM_PLAYER_CANNONBALLS);
        }

        public void CreateBattleships()
        {
            battleships = new Battleship[NUM_BATTLESHIPS];

            battleships[0].Init(GeneratePosition(5));

            for (int i = 1; i < NUM_BATTLESHIPS; i++)
            {
                battleships[1].Init(GeneratePosition(4));
            }
        }

        public List<string> GeneratePosition(int battleshipSize)
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
            Random rand1 = new Random();
            char randChar = (char)rand1.Next(min, max);

            // Generate a rand number 1 - 10 as an ASCII character
            min = 48;
            max = 57;
            Random rand2 = new Random();
            char randNum = (char)rand2.Next(min, max);

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

        public void DisplayCurrentGame()
        {
            Console.WriteLine("Cannonballs remaining: " + player.Cannonballs);

            Console.WriteLine("\nCurrent board:");
            gameBoard.Print();
        }

        public void TakePlayerInput()
        {
            Console.WriteLine("Enter a coordinate:");
            coordinateInput = Console.ReadLine().ToCharArray();
        }

        public bool Shoot()
        {
            bool validAction = true;

            player.TakeShot();

            int positionIndexX = 0;
            int positionIndexY = 0;

            try
            {
                positionIndexX = ConvertCharacterToIndex(coordinateInput[0]);

                if (coordinateInput[1] == '1' && coordinateInput[2] == '0')
                {
                    positionIndexY = 9;
                }
                else
                {
                    positionIndexY = ConvertCharacterToIndex(coordinateInput[1]);
                }
            }
            catch
            {   
                validAction = false;
            }

            if (!validAction)
            {
                Console.WriteLine("You entered an invalid character.");
                return false;
            }

            // If the characters are valid, check the position markers of the ships
            string hitPosition = coordinateInput.ToString();
            bool hit = false;

            for (int i = 0; i < NUM_BATTLESHIPS; i++)
            {
                string checkPosition;

                List<string> positionList = battleships[i].Position;

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

        public void OutputPlayerScore()
        {
            Console.WriteLine("\nYour current score is: \n" + player.Score + "\n");
        }

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
