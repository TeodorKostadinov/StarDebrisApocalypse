namespace StarDebrisApocalypse
{
    public class Rocks : MovingObject
    {
        // Constructor
        public Rocks(MatrixCoords topLeft, char[,] body, MatrixCoords speed)
            : base(topLeft, body, speed)
        {
        }

        public override void RespondToCollision()
        {
            this.IsDestroyed = true;
        }

        // Method for moving right
        public override void Update()
        {
            base.topLeft += base.Speed;
        }
    }
}