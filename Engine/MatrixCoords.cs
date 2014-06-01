namespace StarDebrisApocalypse
{
    public class MatrixCoords
    {
        // Fields
        public int Row { get; set; }
        public int Col { get; set; }

        // Constructor
        public MatrixCoords(int row, int col)
        {
            this.Row = row;
            this.Col = col;
        }

        // Predifine the operators
        public static MatrixCoords operator +(MatrixCoords firstMatrixCoord, MatrixCoords secondMatrixCoord)
        {
            return new MatrixCoords(firstMatrixCoord.Row + secondMatrixCoord.Row, firstMatrixCoord.Col + secondMatrixCoord.Col);
        }

        public static MatrixCoords operator -(MatrixCoords firstMatrixCoord, MatrixCoords secondMatrixCoord)
        {
            return new MatrixCoords(firstMatrixCoord.Row - secondMatrixCoord.Row, firstMatrixCoord.Col - secondMatrixCoord.Col);
        }
    }
}