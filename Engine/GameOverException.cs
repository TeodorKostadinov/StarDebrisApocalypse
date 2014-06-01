using System;

namespace StarDebrisApocalypse
{
    class GameOverException : Exception
    {
        // Constructor
        public GameOverException(string message)
            : base(message)
        { }
    }
}