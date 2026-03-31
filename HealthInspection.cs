using System;
using SplashKitSDK;
using static SplashKitSDK.SplashKit;
using System.Collections.Generic;

namespace CustomProgram
{
    public class HealthInspection : Event
    {
        private Timer _gracePeriodTimer;

        public HealthInspection(int gracePeriodSeconds)
        {
            _gracePeriodTimer = new Timer(gracePeriodSeconds);
        }

        /// <summary>
        /// Gets or sets the grace period timer for the health inspection event.
        /// </summary>
        public Timer GracePeriod
        {
            get { return _gracePeriodTimer; }
            set { _gracePeriodTimer = value; }
        }

        /// <summary>
        /// Applies the health inspection event effect on the restaurant.
        /// </summary>
        /// <param name="restaurant"></param>
        public override void EventEffect(Restaurant restaurant)
        {
            if (!Activated)
            {
                GracePeriod.ResetTimer();
                Activated = true;
            }

            if (!GracePeriod.HasEnded())
            {
                double elapsedTotalSeconds = GracePeriod.GetElapsedTime().TotalSeconds;
                // determine 3 secs interval
                int wholeSecond = (int)elapsedTotalSeconds;

                if (wholeSecond % 2 == 0)
                {
                    Color semiTransparentRed = RGBAColor(255, 0, 0, 100);
                    FillRectangle(semiTransparentRed, 0, 0, CurrentWindowWidth(), CurrentWindowHeight());
                }
            }
            else
            {
                if (Activated)
                {
                    bool foundAnyDirtyTable = false;

                    foreach (Table table in restaurant.Tables)
                    {
                        if (table.Cleanliness != StainLevel.Clean)
                        {
                            foundAnyDirtyTable = true;
                            break;
                        }
                    }

                    if (foundAnyDirtyTable)
                    {
                        if (restaurant.MoneyEarned < 50)
                        {
                            restaurant.MoneyEarned = 0;
                        }
                        else
                        {
                            restaurant.MoneyEarned -= 50;
                        }
                    }

                    Activated = false;
                }
            }
        }
    }
}