namespace StarDebrisApocalypse
{
    public abstract class MovingObject : GameObject
    {
        // Property
        public MatrixCoords Speed { get; set; }

        // Constructor
        protected MovingObject(MatrixCoords topLeft, char[,] body, MatrixCoords speed)
            : base(topLeft, body)
        {
            this.Speed = speed;
        }

        protected virtual void UpdatePosition()
        {
            base.TopLeft += this.Speed;
        }

        public override void Update()
        {
            this.UpdatePosition();
        }
    }
}