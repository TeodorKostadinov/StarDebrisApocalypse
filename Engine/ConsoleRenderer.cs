using System;
using System.Media;
using System.Text;

namespace StarDebrisApocalypse
{
    public class ConsoleRenderer : IRenderer
    {
        // Fields
        private int renderContextMatrixRows;
        private int renderContextMatrixCols;

        // Field of the matrix that we will be printed
        private char[,] renderContextMatrix;

        // Construcotr
        public ConsoleRenderer(int visibleConsoleRows, int visibleConsoleCols)
        {
            this.renderContextMatrix = new char[visibleConsoleRows, visibleConsoleCols];
            this.renderContextMatrixRows = this.renderContextMatrix.GetLength(0);
            this.renderContextMatrixCols = this.renderContextMatrix.GetLength(1);
            this.ClearQueue();
        }

        // Add an object to the rendering queue
        public void EnqueueForRendering(GameObject obj)
        {
            char[,] objImage = obj.Print();

            int imageRows = objImage.GetLength(0);
            int imageCols = objImage.GetLength(1);

            MatrixCoords objTopLeft = obj.TopLeft;

            int lastRow = Math.Min(objTopLeft.Row + imageRows, this.renderContextMatrixRows);
            int lastCol = Math.Min(objTopLeft.Col + imageCols, this.renderContextMatrixCols);

            for (int row = obj.GetTopLeft().Row; row < lastRow; row++)
            {
                for (int col = obj.GetTopLeft().Col; col < lastCol; col++)
                {
                    if (row >= 0 && row < renderContextMatrixRows &&
                        col >= 0 && col < renderContextMatrixCols)
                    {
                        renderContextMatrix[row, col] = objImage[row - obj.GetTopLeft().Row, col - obj.GetTopLeft().Col];
                    }
                }
            }
        }

        // Print the matrix
        public void RenderAll()
        {
            Console.SetCursorPosition(0, 0);
            StringBuilder scene = new StringBuilder();

            for (int row = 0; row < this.renderContextMatrixRows; row++)
            {
                for (int col = 0; col < this.renderContextMatrixCols; col++)
                {
                    scene.Append(this.renderContextMatrix[row, col]);
                }

                scene.Append(Environment.NewLine);
            }

            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write(scene.ToString().Trim());
            Console.ForegroundColor = ConsoleColor.White;
        }

        // Fill the matrix with empty cells (spaces)
        public void ClearQueue()
        {
            for (int row = 0; row < this.renderContextMatrixRows; row++)
            {
                for (int col = 0; col < this.renderContextMatrixCols; col++)
                {
                    this.renderContextMatrix[row, col] = ' ';
                }
            }
        }

        public static void PrintGameOver()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            SoundPlayer myPlayer = new SoundPlayer();
            myPlayer.SoundLocation =  @"smb_gameover.wav";
            myPlayer.Play();
            Console.WriteLine("\n\n");
            Console.WriteLine("{0,45}", "Game Over!");
        }

        public static void PrintRules()
        {
            Console.WriteLine("Use the UP arrow/W and DOWN arrow/S to move your vessel, \nthe SPACEBAR to shoot and \nthe D key to plant mines. \nDon't let the rocks hit you and more importantly -- the Earth.");
        }

        public static void PrintBackStory()
        {
            Console.WriteLine("\n\tAfter an unexpected impact from a meteorite on russian soil the russian government launched a secret program, which lone purpose was to defend Earth in the forthcoming inevetiable apocalypse. Now, when the time has come, you, as an experienced russian pilot, are the only hope for the human race. \n\tGet into the ship and kick some rock ass!");
        }

        public static void PrintAbout()
        {
            Console.WriteLine("\t\t\tStar Debris Apocalypse is property of \n\t\t\t\tTeam Yogi Bear \n\t\t\t     featuring as follows:\n\t\t\t\t Qna Mihaylova\n\t\t\t\t Iliyan Iliev\n\t\t\t      Teodor Kostadinov.");
        }
    }
}