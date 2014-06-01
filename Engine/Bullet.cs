using System;

namespace StarDebrisApocalypse
{
    public class Bullet : GameObject
    {
        public Bullet(MatrixCoords topLeft, char[,] body)
            : base(topLeft, body)
        {
            //Here we can change the beep melody of the fire
            Console.Beep(800, 100);
        }

        public override void RespondToCollision()
        {
            this.IsDestroyed = true;
        }

        public override void Update()
        {
            base.topLeft.Col++;
            if (base.TopLeft.Col > 56)
            {
                this.RespondToCollision();
            }
        }
    }
}