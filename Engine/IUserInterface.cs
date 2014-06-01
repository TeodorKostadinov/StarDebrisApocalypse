using System;

namespace StarDebrisApocalypse
{
    public interface IUserInterface
    {
        // All events
        event EventHandler OnUpPressed;
        event EventHandler OnDownPressed;
        event EventHandler OnActionPressed;
        event EventHandler OnMinePressed;

        void ProcessInput();
    }
}