namespace StarDebrisApocalypse
{
    public class Ship : GameObject
    {
        public Ship(MatrixCoords topLeft, char[,] body)
            : base(topLeft, body)
        {
        }

        // Method for moving up 
        public void MoveUp(int topBorder)
        {
            if (this.topLeft.Row > topBorder)
                this.topLeft.Row--;
        }

        // Method for moving down
        public void MoveDown(int bottomBorder)
        {
            if (this.topLeft.Row < bottomBorder - 1)
                this.topLeft.Row++;
        }

        // Method for firing
        public void Fire(Engine engine)
        {
            Bullet bullet = new Bullet(new MatrixCoords(this.TopLeft.Row, this.TopLeft.Col + 5), new char[,] { { '●' } });
            engine.AddObject(bullet);
        }

        public void Mine(Engine engine)
        {
            Mine aMine = new Mine(new MatrixCoords(this.TopLeft.Row, this.TopLeft.Col + 5), new char[,] { { '*' } });
            engine.AddObject(aMine);
        }

        public override void Update() { }

        public override void RespondToCollision()
        {
        }
    }
}