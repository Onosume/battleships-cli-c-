using System;

namespace battleships_cli.Application.GameObjects.GameBoard
{
    public class GameBoard
    {
        private const int BOARD_WIDTH = 10;
        private const int BOARD_HEIGHT = 10;
        private const char HIT_MARKER = 'H';
        private const char MISS_MARKER = 'M';
        private char[,] board;

        public GameBoard()
        {
            Init();
        }

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

        public void SetCell(int posX, int posY, char status)
        {
            board[posX, posY] = status;
        }

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
