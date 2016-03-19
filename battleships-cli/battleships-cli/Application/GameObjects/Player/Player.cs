// battlships-cli
// Player.cs
// Author: Matthew Tinn

namespace battleships_cli.Application.GameObjects.Player
{
    /// <summary>
    /// The player class
    /// Holds the player's score and number of cannonballs remaining
    /// </summary>
    public class Player
    {
        private int score;
        private int cannonballs;

        // Accessor Methods
        public int Score { get { return score; } }
        public int Cannonballs { get { return cannonballs; } }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="numCannonballs">Player's maximum number of cannonballs</param>
        public Player(int numCannonballs)
        {
            Init(numCannonballs);
        }

        /// <summary>
        /// Initialise the player
        /// </summary>
        /// <param name="numCannonballs">Player's maximum number of cannonballs</param>
        private void Init(int numCannonballs)
        {
            score = 0;
            cannonballs = numCannonballs;
        }

        /// <summary>
        /// Decrement player cannonballs
        /// </summary>
        public void TakeShot()
        {
            cannonballs--;
        }

        /// <summary>
        /// Add specific value to player score
        /// </summary>
        /// <param name="points">Value to add</param>
        public void AddToScore(int points)
        {
            score += points;
        }
    }
}
