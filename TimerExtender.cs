using System;
using SplashKitSDK;
using static SplashKitSDK.SplashKit;
using System.Collections.Generic;

namespace CustomProgram
{
    public class TimerExtender : ShopItem
    {
        private int _addSeconds;

        public TimerExtender(string itemName, decimal itemPrice, Bitmap objectBitmap, double x, double y, int addSeconds) : base(itemName, itemPrice, objectBitmap, x, y)
        {
            _addSeconds = addSeconds;
        }

        /// <summary>
        /// Adds a fixed amount of seconds to the game timer when the item is used.
        /// </summary>
        /// <param name="r"></param>
        public override void UseItem(Restaurant r)
        {
            if (Activated)
            {
                if (r.MoneyEarned > ItemPrice)
                {
                    r.MoneyEarned -= ItemPrice;
                    r.GameTimer.AddOrRemoveTime(_addSeconds);
                }
            }
        }
    }
}