using System;
using SplashKitSDK;
using static SplashKitSDK.SplashKit;
using System.Collections.Generic;

namespace CustomProgram
{
    public class MassCleaner : ShopItem
    {
        public MassCleaner(string itemName, decimal itemPrice, Bitmap objectBitmap, double x, double y) : base(itemName, itemPrice, objectBitmap, x, y) { }

        /// <summary>
        /// Resets the stain degree of all tables in the restaurant to zero (clean) when the item is used.
        /// </summary>
        /// <param name="r"></param>
        public override void UseItem(Restaurant r)
        {
            if (Activated)
            {
                if (r.MoneyEarned > ItemPrice)
                {
                    r.MoneyEarned -= ItemPrice;
                    foreach (Table table in r.Tables)
                    {
                        table.StainDegree = 0;
                    }
                }
            }
        }
    }
}