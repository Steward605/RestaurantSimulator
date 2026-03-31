using System;
using SplashKitSDK;
using static SplashKitSDK.SplashKit;
using System.Collections.Generic;

namespace CustomProgram
{
    public abstract class Event
    {
        private bool _activated;

        public Event()
        {
            _activated = false;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the event is activated.
        /// </summary>
        public bool Activated
        {
            get { return _activated; }
            set { _activated = value; }
        }

        /// <summary>
        /// Abstract method to apply the event effect on the game.
        /// </summary>
        /// <param name="restaurant"></param>
        public abstract void EventEffect(Restaurant restaurant);
    }
}