using System;

namespace StarDebrisApocalypse
{
    class GameInfoPrinter : GameObject, IPrintable
    {
        // Fields
        private byte shipLife;
        private byte earthLife;
        private int playerScore;
        private byte playerLevel;

        // Constructor
        public GameInfoPrinter(GameInfo playerDetails, MatrixCoords topLeft, char[,] body, Engine engine)
            : base(topLeft, body)
        {
            this.shipLife = playerDetails.shipLifes;
            this.earthLife = playerDetails.earthLifes;
            this.playerScore = playerDetails.score;
            this.playerLevel = playerDetails.level;
            CollisionDispatcher.ShipLifesRemaining = playerDetails.shipLifes;
            CollisionDispatcher.EarthLifesRemaining = playerDetails.earthLifes;
        }

        public override void Update()
        {
            string life = "Remaining Lifes:";
            AddToBody(this.body, life, 1);

            string lifeInfo;
            lifeInfo = String.Format("{0:00} Ships", this.shipLife);
            AddToBody(this.body, lifeInfo, 2);
            lifeInfo = String.Format("{0:00} Earth Lifes", this.earthLife);
            AddToBody(this.body, lifeInfo, 3);

            string player;
            player = String.Format("Score: {0:0000}", this.playerScore);
            AddToBody(this.body, player, 5);
            player = String.Format("Level: {0:0000}", this.playerLevel);
            AddToBody(this.body, player, 6);

            this.shipLife = CollisionDispatcher.ShipLifesRemaining;
            this.earthLife = CollisionDispatcher.EarthLifesRemaining;
            this.playerScore = CollisionDispatcher.RocksDestroyed;
        }

        private void AddToBody(char[,] array, string str, int row)
        {
            int i = 0;
            foreach (var character in str)
            {
                array[row, i] = character;
                i++;
            }
        }
    }
}