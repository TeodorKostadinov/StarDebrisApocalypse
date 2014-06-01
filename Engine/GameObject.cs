using System.Collections.Generic;

namespace StarDebrisApocalypse
{
    public abstract class GameObject : IPrintable, ICollidable
    {
        // Fields
        protected MatrixCoords topLeft;
        protected char[,] body;

        // Properties
        public MatrixCoords TopLeft
        {
            get
            {
                return new MatrixCoords(this.topLeft.Row, this.topLeft.Col);
            }
            set
            {
                this.topLeft = new MatrixCoords(value.Row, value.Col);
            }
        }

        // this property enable us to get the image of the body
        public char[,] Body
        {
            get
            {
                return this.body;
            }
            protected set
            {
                this.body = value;
            }
        }

        public bool IsDestroyed { get; protected set; }

        // Costructor
        protected GameObject(MatrixCoords topLeft, char[,] body)
        {
            // making a deep copy of the input parameters
            this.topLeft = new MatrixCoords(topLeft.Row, topLeft.Col);
            this.body = this.CopyMatrix(body);

            this.IsDestroyed = false;
        }

        // Virtual method which every derived class should implement in order to show us the collision responce
        public virtual void RespondToCollision()
        {
        }

        // Method for making copy of a matrix
        char[,] CopyMatrix(char[,] matrixToCopy)
        {
            int rows = matrixToCopy.GetLength(0);
            int cols = matrixToCopy.GetLength(1);

            char[,] result = new char[rows, cols];

            for (int row = 0; row < rows; row++)
            {
                for (int col = 0; col < cols; col++)
                {
                    result[row, col] = matrixToCopy[row, col];
                }
            }

            return result;
        }

        // Get coordinates of the matrix
        public virtual MatrixCoords GetTopLeft()
        {
            return this.TopLeft;
        }

        public char[,] Print()
        {
            return this.CopyMatrix(this.body);
        }

        public virtual IEnumerable<GameObject> ProduceObjects()
        {
            return new List<GameObject>();
        }

        public abstract void Update();
    }
}