using System;
using System.Collections.Generic;

namespace StarDebrisApocalypse
{
    public class Engine
    {
        // Fields
        private const int defaultSleepTime = 300;
        private IRenderer renderer;
        private IUserInterface keyboard;
        private List<GameObject> allObjects;
        private List<MovingObject> movingObjects;
        private List<GameObject> staticObjects;
        private int sleepTime;
        private int difficulty = 40;
        private Ship playerShip;

        // Constructors
        public Engine(IRenderer renderer, IUserInterface userInterface)
            : this(renderer, userInterface, defaultSleepTime)
        { }
        public Engine(IRenderer renderer, IUserInterface userInterface, int sleepTime)
        {
            this.renderer = renderer;
            this.keyboard = userInterface;
            this.allObjects = new List<GameObject>();
            this.movingObjects = new List<MovingObject>();
            this.staticObjects = new List<GameObject>();
            this.sleepTime = sleepTime;
        }

        private void AddStaticObject(GameObject obj)
        {
            this.staticObjects.Add(obj);
            this.allObjects.Add(obj);
        }

        private void AddMovingObject(MovingObject obj)
        {
            this.movingObjects.Add(obj);
            this.allObjects.Add(obj);
        }

        private void AddShip(GameObject obj)
        {
            this.playerShip = obj as Ship;
            this.AddStaticObject(obj);
        }

        public virtual void AddObject(GameObject obj)
        {
            if (obj is MovingObject)
            {
                this.AddMovingObject(obj as MovingObject);
            }
            else
            {
                if (obj is Ship)
                {
                    AddShip(obj);
                }
                else
                {
                    this.AddStaticObject(obj);
                }
            }
        }

        // Methods for moving the ship
        public virtual void MovePlayerShipUp(int topBorder)
        {
            this.playerShip.MoveUp(topBorder);
        }

        public virtual void MovePlayerShipDown(int bottomBorder)
        {
            this.playerShip.MoveDown(bottomBorder);
        }

        public virtual void MakePlayerShipFire(Engine engine)
        {
            this.playerShip.Fire(engine);
        }

        public virtual void MakePlayerShipMine(Engine engine)
        {
            this.playerShip.Mine(engine);
        }

        // Method for executing the game
        public virtual void Run(int rightBorder, int bottomBorder)
        {
            Random rand = new Random();

            while (true)
            {
                try
                {
                    // Print on the console
                    this.renderer.RenderAll();
                    System.Threading.Thread.Sleep(this.sleepTime);

                    // Read a key
                    this.keyboard.ProcessInput();

                    // Clear the matrix
                    this.renderer.ClearQueue();

                    if (rand.Next(0, this.difficulty) < 2)
                    {
                        for (int i = 0; i < rand.Next(1, 4); i++)
                        {
                            Rocks rock = new Rocks(new MatrixCoords(rand.Next(3, bottomBorder - 1), rightBorder - 1), new char[,] { { 'Ҩ' } }, new MatrixCoords(0, -1));
                            this.AddObject(rock);
                        }
                    }

                    // Move all objects and put them into the matrix
                    foreach (var obj in this.allObjects)
                    {
                        obj.Update();
                        this.renderer.EnqueueForRendering(obj);
                    }

                    CollisionDispatcher.HandleCollisions(this.movingObjects, this.staticObjects);
                    List<GameObject> producedObjects = new List<GameObject>();

                    foreach (var obj in this.allObjects)
                    {
                        producedObjects.AddRange(obj.ProduceObjects());
                    }

                    this.allObjects.RemoveAll(obj => obj.IsDestroyed);
                    this.movingObjects.RemoveAll(obj => obj.IsDestroyed);
                    this.staticObjects.RemoveAll(obj => obj.IsDestroyed);

                    foreach (var obj in producedObjects)
                    {
                        this.AddObject(obj);
                    }
                }
                catch (GameOverException)
                {
                    ConsoleRenderer.PrintGameOver();
                    break;
                }
            }
        }
    }
}