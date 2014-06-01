namespace StarDebrisApocalypse
{
    public struct GameInfo
    {
        // Fields
        public byte shipLifes;
        public byte level;
        public int score;
        public byte earthLifes;

        // Constructor
        public GameInfo(byte shipLifes, byte level, int score, byte earthLifes)
        {
            this.shipLifes = shipLifes;
            this.level = level;
            this.score = score;
            this.earthLifes = earthLifes;
        }
    }
}