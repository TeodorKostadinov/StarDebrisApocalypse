using System.Collections.Generic;

namespace StarDebrisApocalypse
{
    public static class CollisionDispatcher
    {
        public static void HandleCollisions(List<MovingObject> movingObjects, List<GameObject> staticObjects)
        {
            HandleMovingWithStaticCollisions(movingObjects, staticObjects);
        }

        //Counters
        private static int rocksDestroyed = 0;

        public static byte ShipLifesRemaining { get; set; }
        public static byte EarthLifesRemaining { get; set; }

        public static int RocksDestroyed
        {
            get { return CollisionDispatcher.rocksDestroyed; }
            set { CollisionDispatcher.rocksDestroyed = value; }
        }

        private static void HandleMovingWithStaticCollisions(List<MovingObject> movingObjects, List<GameObject> staticObjects)
        {
            foreach (var rock in movingObjects)
            {
                foreach (var objectSt in staticObjects)
                {
                    if (objectSt is Ship)
                    {
                        if (rock.TopLeft.Col == objectSt.TopLeft.Col + 4 && rock.TopLeft.Row == objectSt.TopLeft.Row)
                        {
                            CollisionDispatcher.ShipLifesRemaining--;
                            rock.RespondToCollision();
                            if (CollisionDispatcher.ShipLifesRemaining == 0)
                            {
                                throw new GameOverException("Game Over");
                            }
                        }
                    }
                    else
                    {
                        if (rock.TopLeft.Col == objectSt.TopLeft.Col && rock.TopLeft.Row == objectSt.TopLeft.Row)
                        {
                            if (objectSt is BorderBlock)
                            {
                                CollisionDispatcher.EarthLifesRemaining--;
                                rock.RespondToCollision();
                                if (CollisionDispatcher.EarthLifesRemaining == 0)
                                {
                                    throw new GameOverException("Game Over");
                                }
                            }
                            else
                            {
                                CollisionDispatcher.RocksDestroyed++;
                                rock.RespondToCollision();
                                objectSt.RespondToCollision();
                            }
                        }
                    }
                }
            }
        }
    }
}
