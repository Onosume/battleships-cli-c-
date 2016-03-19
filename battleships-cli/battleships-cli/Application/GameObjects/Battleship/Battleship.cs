using System;
using System.Collections.Generic;
using System.Linq;

namespace battleships_cli.Application.GameObjects.Battleship
{
    /// <summary>
    /// A class to represent a battleship
    /// </summary>
    public class Battleship
    {
        public const int MAX_BATTLESHIP_LENGTH = 5;

        private bool sunk;
        private List<string> position;
        private int health;
        private int[] positionsHit = new int[MAX_BATTLESHIP_LENGTH];

        // Accessor Methods
        public bool Sunk { get; }
        public List<string> Position { get; }
        public int Health { get; }

        public Battleship(List<string> position)
        {
            Init(position);
        }

        /// <summary>
        /// Initialises the battleship's position and other attributes
        /// </summary>
        /// <param name="position">List of cells for ship placement</param>
        public void Init(List<string> position)
        {
            sunk = false;
            health = position.Count();
            this.position = position;

            for (int i = 0; i < MAX_BATTLESHIP_LENGTH; i++)
            {
                positionsHit[i] = 0;
            }
        }

        /// <summary>
        /// Damage the ship
        /// </summary>
        /// <param name="positionIndex">Cell hit by the player</param>
        public void TakeDamageOn(int positionIndex)
        {
            if (positionsHit[positionIndex] != 1)
            {
                positionsHit[positionIndex] = 1;
                health--;

                if (health == 0)
                {
                    sunk = true;
                    Console.WriteLine("\nYou sunk a ship!\n");
                }
            }
        }
    }
}
