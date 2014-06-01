using System;
using System.Linq;

namespace StarDebrisApocalypse
{
    public class Keyboard : IUserInterface
    {
        // the onlyone instance of the keyboard class
        private static Keyboard keyboard;
        // Fields
        private event EventHandler onUpPressed;
        private event EventHandler onDownPressed;
        private event EventHandler onActionPressed;
        private event EventHandler onMinePressed;

        // Properties
        public event EventHandler OnUpPressed
        {
            add
            {
                if (this.onUpPressed == null || !this.onUpPressed.GetInvocationList().Contains(value))
                {
                    this.onUpPressed += value;
                }
            }
            remove
            {
                this.onUpPressed -= value;
            }
        }

        public event EventHandler OnDownPressed
        {
            add
            {
                if (this.onDownPressed == null || !this.onDownPressed.GetInvocationList().Contains(value))
                {
                    this.onDownPressed += value;
                }
            }
            remove
            {
                this.onDownPressed -= value;
            }
        }

        public event EventHandler OnActionPressed
        {
            add
            {
                if (this.onActionPressed == null || !this.onActionPressed.GetInvocationList().Contains(value))
                {
                    this.onActionPressed += value;
                }
            }
            remove
            {
                this.onActionPressed -= value;
            }
        }

        public event EventHandler OnMinePressed
        {
            add
            {
                if (this.onMinePressed == null || !this.onMinePressed.GetInvocationList().Contains(value))
                {
                    this.onMinePressed += value;
                }
            }
            remove
            {
                this.onMinePressed -= value;
            }
        }

        // Constructor
        protected Keyboard()
        {
        }

        public void ProcessInput()
        {
            while (Console.KeyAvailable)
            {
                var keyInfo = Console.ReadKey();
                while (Console.KeyAvailable)
                {
                    Console.ReadKey();
                }
                if (keyInfo.Key.Equals(ConsoleKey.S) || keyInfo.Key.Equals(ConsoleKey.DownArrow))
                {
                    if (this.onDownPressed != null)
                    {
                        this.onDownPressed(this, new EventArgs());
                    }
                }

                if (keyInfo.Key.Equals(ConsoleKey.W) || keyInfo.Key.Equals(ConsoleKey.UpArrow))
                {
                    if (this.onUpPressed != null)
                    {
                        this.onUpPressed(this, new EventArgs());

                    }
                }

                if (keyInfo.Key.Equals(ConsoleKey.Spacebar))
                {
                    if (this.onActionPressed != null)
                    {
                        this.onActionPressed(this, new EventArgs());
                    }
                }
                if (keyInfo.Key.Equals(ConsoleKey.D))
                {
                    if (this.onActionPressed != null)
                    {
                        this.onMinePressed(this, new EventArgs());
                    }
                }
            }
        }

        internal static IUserInterface Instance()
        {
            if (keyboard == null)
            {
                keyboard = new Keyboard();
            }

            return keyboard;
        }
    }
}