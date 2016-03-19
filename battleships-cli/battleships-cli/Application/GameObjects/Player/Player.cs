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
        public int Score { get; }
        public int Cannonballs { get; }

        public Player(int numCannonballs)
        {
            Init(numCannonballs);
        }
        
        private void Init(int numCannonballs)
        {
            score = 0;
            cannonballs = numCannonballs;
        }

        public void TakeShot()
        {
            cannonballs--;
        }

        public void AddToScore(int points)
        {
            score += points;
        }
    }
}
