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
        private int score { get; set; }
        private int cannonballs { get; set; }

        public Player(int cannonballs)
        {
            Init(cannonballs);
        }
        
        private void Init(int cannonballs)
        {
            score = 0;
            this.cannonballs = cannonballs;
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
