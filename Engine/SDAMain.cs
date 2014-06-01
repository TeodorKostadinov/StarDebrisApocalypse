using System;
using System.Media;
using System.Text;

namespace StarDebrisApocalypse
{
    public class SDAMain
    {
        // Dimentions of the game field
        const int WorldRows = 23;
        const int WorldCols = 79;

        // Constructor - add all objects
        static void Initialize(Engine engine, char[,] body)
        {
            int startRow = 0;
            int startCol = 0;
            int endCol = 5;
            int endRow = WorldRows;

            // Create the "earth" and after collision with rocks => GAME OVER
            for (int i = startCol; i < endCol; i++)
            {
                for (int j = startRow; j < endRow; j++)
                {
                    BorderBlock currBlock = new BorderBlock(new MatrixCoords(j, i), new char[,] { { '▓' } });
                    engine.AddObject(currBlock);
                }
                startRow = startRow + 2;
                endRow = endRow - 2;
            }

            //Player Info
            GameInfo playerInfo = new GameInfo(3, 70, 0, 3);
            char[,] menuBox = new char[12, 18];
            GameInfoPrinter infoObject = new GameInfoPrinter(playerInfo, new MatrixCoords(5, 61), menuBox, engine);
            engine.AddObject(infoObject);

            // Walls
            for (int i = 0; i < WorldRows; i++)
            {
                BorderBlock currentBlock = new BorderBlock(new MatrixCoords(i, WorldCols - 21), new char[,] { { '▒' } });
                engine.AddObject(currentBlock);
            }

            //Ship
            Ship theShip = new Ship(new MatrixCoords(WorldRows / 2, 10), body);//na4alni koordinati, bez skorost
            engine.AddObject(theShip);
        }

        public static void PrintMenu()
        {
            char choice = '0';
            do
            {
                Console.WriteLine("\n\n\n\n");
                Console.WriteLine("{0,49}", "___________________");
                Console.WriteLine("{0,50}", "|------ Menu -------|");
                Console.WriteLine("{0,50}", "|                   |");
                Console.WriteLine("{0,50}", "|   1. Start Game.  |");
                Console.WriteLine("{0,50}", "|   2. Rules.       |");
                Console.WriteLine("{0,50}", "|   3. Back Story.  |");
                Console.WriteLine("{0,50}", "|   4. About.       |");
                Console.WriteLine("{0,50}", "|   5. Exit.        |");
                Console.WriteLine("{0,50}", "|___________________|");
                Console.WriteLine();
                Console.Write("{0,54}", "Please, enter your choice: ");

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                choice = keyInfo.KeyChar;
                Console.ForegroundColor = ConsoleColor.DarkMagenta;

                switch (choice)
                {
                    case '1': Console.Clear(); PrintSubMenu(); break;
                    case '2': Console.Clear(); ConsoleRenderer.PrintRules(); break;
                    case '3': Console.Clear(); ConsoleRenderer.PrintBackStory(); break;
                    case '4': Console.Clear(); ConsoleRenderer.PrintAbout(); break;
                    case '5': Console.Clear(); Console.WriteLine("Goodbye!"); break;
                    default: Console.Clear(); Console.WriteLine("\nPlease, enter a number between 1 and 5!"); break;
                }
            } while (choice != '5');
        }

        public static void PrintSubMenu()
        {
            char choice = '0';
            do
            {
                Console.WriteLine("\n\n\n\n");
                Console.WriteLine("{0,49}", "___________________");
                Console.WriteLine("{0,50}", "|---- Pick Ship ----|");
                Console.WriteLine("{0,50}", "|                   |");
                Console.WriteLine("{0,50}", "|   1. Happy.       |");
                Console.WriteLine("{0,50}", "|   2. Pointy.      |");
                Console.WriteLine("{0,50}", "|   3. Perky.       |");
                Console.WriteLine("{0,50}", "|   4. Back.        |");
                Console.WriteLine("{0,50}", "|                   |");
                Console.WriteLine("{0,50}", "|___________________|");
                Console.WriteLine();
                Console.Write("{0,54}", "Please, enter your choice: ");

                ConsoleKeyInfo keyInfo = Console.ReadKey();
                choice = keyInfo.KeyChar;
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                switch (choice)
                {
                    case '1':
                        StartGame(GetShipBody(ShipName.Happy));
                        break;
                    case '2':
                        StartGame(GetShipBody(ShipName.Pointy));
                        break;
                    case '3':
                        StartGame(GetShipBody(ShipName.Perky));
                        break;
                    case '4':
                        Console.Clear();
                        PrintMenu();
                        break;
                    default:
                        Console.Clear();
                        Console.WriteLine("\nPlease, enter a number between 1 and 5!");
                        break;
                }
            } while (choice != '4');
        }

        private static void ConfigureConsole()
        {
            Console.OutputEncoding = Encoding.Unicode;
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Title = "Star Debris Apocalypse";
            Console.BufferHeight = Console.WindowHeight = WorldRows;
        }

        private static void StartGame(char[,] body)
        {
            SoundPlayer myPlayer = new SoundPlayer();
            myPlayer.SoundLocation = @"Clip - Star Trek Insurrection OST 4 Not Functioning - Segment1(00_00_01.999-00_00_11.814).wav";
            myPlayer.PlayLooping();

            IRenderer renderer = new ConsoleRenderer(WorldRows, WorldCols);
            IUserInterface keyboard = Keyboard.Instance();

            Engine gameEngine = new Engine(renderer, keyboard);

            // Events for moving
            keyboard.OnUpPressed += (sender, eventInfo) =>
            {
                gameEngine.MovePlayerShipUp(0);
            };

            keyboard.OnDownPressed += (sender, eventInfo) =>
            {
                gameEngine.MovePlayerShipDown(WorldRows);
            };

            keyboard.OnActionPressed += (sender, eventInfo) =>
            {
                gameEngine.MakePlayerShipFire(gameEngine);
            };

            keyboard.OnMinePressed += (sender, eventInfo) =>
            {
                gameEngine.MakePlayerShipMine(gameEngine);
            };

            Initialize(gameEngine, body);
            gameEngine.Run(WorldCols - 20, WorldRows);
        }

        public static char[,] GetShipBody(ShipName shipName)
        {
            switch (shipName)
            {
                case ShipName.Happy:
                    return new char[,] { { '-', '=', '[', '☺', ']', '=', '-' } };
                case ShipName.Pointy:
                    return new char[,] { { '-', '<', '[', 'O', '|', '>', '-' } };
                case ShipName.Perky:
                    return new char[,] { { '-', '=', '[', ']', '|', 'O', '=' } };
                default:
                    throw new ArgumentException("Unkown ship name.");
            }
        }

        static void Main()
        {
            ConfigureConsole();
            PrintMenu();
        }
    }
}