using System;

namespace battleships_cli.Application.GameObjects.GameBoard
{
    public class GameBoard
    {
        public const int BOARD_WIDTH = 10;
        public const int BOARD_HEIGHT = 10;
        public const char HIT_MARKER = 'H';
        public const char MISS_MARKER = 'M';
        private char[,] board;

        // Accessor Methods
        public char HitMarker()
        {
            return HIT_MARKER;
        }

        public char MissMarker()
        {
            return MISS_MARKER;
        }

        private char[,] Board { get { return board; } }

        /// <summary>
        /// Constructor
        /// </summary>
        public GameBoard()
        {
            Init();
        }

        /// <summary>
        /// Sets up the game board
        /// </summary>
        public void Init()
        {
            board = new char[BOARD_WIDTH, BOARD_HEIGHT];

            for (int i = 0; i < BOARD_WIDTH; i++)
            {
                for (int j = 0; j < BOARD_HEIGHT; j++)
                {
                    board[i, j] = 'O';
                }
            }
        }

        /// <summary>
        /// Set a cell as H (hit) or M (miss)
        /// </summary>
        /// <param name="posX">Column</param>
        /// <param name="posY">Row</param>
        /// <param name="status">M or H</param>
        public void SetCell(int posX, int posY, char status)
        {
            board[posX, posY] = status;
        }

        /// <summary>
        /// Output the game board to the console
        /// </summary>
        public void Print()
        {
            for (int i = 0; i < BOARD_WIDTH; i++)
            {
                for (int j = 0; j < BOARD_HEIGHT; j++)
                {
                    Console.Write(board[i, j]);
                }

                Console.Write("\n");
            }
        }
    }
}
