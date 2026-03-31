using System;
using SplashKitSDK;
using static SplashKitSDK.SplashKit;
using System.Collections.Generic;

namespace CustomProgram
{
    public class PowerOutage : Event
    {
        private Timer _powerOutageTimer;

        public PowerOutage(int powerOutageTimerSeconds)
        {
            _powerOutageTimer = new Timer(powerOutageTimerSeconds);
        }

        /// <summary>
        /// Gets or sets the power outage timer for the power outage event.
        /// </summary>
        public Timer PowerOutageTimer
        {
            get { return _powerOutageTimer; }
            set { _powerOutageTimer = value; }
        }

        /// <summary>
        /// Applies the power outage event effect on the restaurant.
        /// </summary>
        /// <param name="restaurant"></param>
        public override void EventEffect(Restaurant restaurant)
        {
            if (!Activated)
            {
                PowerOutageTimer.ResetTimer();
                Activated = true;
            }

            if (!PowerOutageTimer.HasEnded())
            {
                Point2D mousePosition = MousePosition();
                int visibleRadius = 100;
                double rectangleLeft = mousePosition.X - visibleRadius;
                double rectangleRight = mousePosition.X + visibleRadius;
                double rectangleTop = mousePosition.Y - visibleRadius;
                double rectangleBottom = mousePosition.Y + visibleRadius;
                if (rectangleLeft < 0)
                {
                    rectangleLeft = 0;
                }
                if (rectangleRight > CurrentWindowWidth())
                {
                    rectangleRight = CurrentWindowWidth();
                }
                if (rectangleTop < 0)
                {
                    rectangleTop = 0;
                }
                if (rectangleBottom > CurrentWindowHeight())
                {
                    rectangleBottom = CurrentWindowHeight();
                }
                FillRectangle(ColorBlack(), 0, 0, CurrentWindowWidth(), rectangleTop);
                FillRectangle(ColorBlack(), 0, rectangleBottom, CurrentWindowWidth(), CurrentWindowHeight() - rectangleBottom);
                FillRectangle(ColorBlack(), 0, rectangleTop, rectangleLeft, rectangleBottom - rectangleTop);
                FillRectangle(ColorBlack(), rectangleRight, rectangleTop, CurrentWindowWidth() - rectangleRight, rectangleBottom - rectangleTop);
            }
            else
            {
                if (Activated)
                {
                    Activated = false;
                }
            }
        }
    }
}