namespace StarDebrisApocalypse
{
    public class Mine : Bullet
    {
        // Constructor
        public Mine(MatrixCoords topLeft, char[,] body)
            : base(topLeft, body)
        {
        }

        public override void RespondToCollision()
        {
            this.IsDestroyed = true;
        }

        public override void Update()
        {
            if (base.topLeft.Col < 20)
            {
                base.topLeft.Col++;
            }
        }
    }
}