// battleships-cli
// Battleship.cs
// Author: Matthew Tinn

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
        private List<string> position; // A list of cells that the ship is stationed on
        private int health;
        private int[] positionsHit = new int[MAX_BATTLESHIP_LENGTH]; // Specifies if a position has been hit. Indexes match with list.

        // Accessor Methods
        public bool Sunk { get { return sunk; } }
        public List<string> Position { get { return position; } }
        public int Health { get { return health; } }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="position"></param>
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
                // If position matches and has not taken damage before, take damage
                positionsHit[positionIndex] = 1;
                health--;

                // Sink if no health
                if (health == 0)
                {
                    sunk = true;
                    Console.WriteLine("\nYou sunk a ship!\n");
                }
            }
        }
    }
}
