using System;
using SplashKitSDK;
using static SplashKitSDK.SplashKit;
using System.Collections.Generic;

namespace CustomProgram
{
    public abstract class ShopItem
    {
        private int _itemID;
        private static int _incrementCount = 0;
        private string _itemName;
        private decimal _itemPrice;
        private bool _activated;
        private Bitmap _objectBitmap;
        private double _objectDefaultX;
        private double _objectDefaultY;

        public ShopItem(string itemName, decimal itemPrice, Bitmap objectBitmap, double x, double y)
        {
            _itemID = _incrementCount++;
            _itemName = itemName;
            _itemPrice = itemPrice;
            _objectDefaultX = x;
            _objectDefaultY = y;
            _objectBitmap = objectBitmap;
            _activated = false;
        }

        /// <summary>
        /// Gets or sets the unique identifier for the item.
        /// </summary>
        public int ItemID
        {
            get { return _itemID; }
            set { _itemID = value; }
        }

        /// <summary>
        /// Gets or sets the name of the item.
        /// </summary>
        public string ItemName
        {
            get { return _itemName; }
            set { _itemName = value; }
        }

        /// <summary>
        /// Gets or sets the price of the item.
        /// </summary>
        public decimal ItemPrice
        {
            get { return _itemPrice; }
            set { _itemPrice = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the item is activated.
        /// </summary>
        public bool Activated
        {
            get { return _activated; }
            set { _activated = value; }
        }

        /// <summary>
        /// Gets or sets the bitmap representing the item.
        /// </summary>
        public Bitmap ObjectBitmap
        {
            get { return _objectBitmap; }
            set { _objectBitmap = value; }
        }

        /// <summary>
        /// Gets or sets the default X position of the item's bitmap.
        /// </summary>
        public double ObjectDefaultX
        {
            get { return _objectDefaultX; }
            set { _objectDefaultX = value; }
        }

        /// <summary>
        /// Gets or sets the default Y position of the item's bitmap.
        /// </summary>
        public double ObjectDefaultY
        {
            get { return _objectDefaultY; }
            set { _objectDefaultY = value; }
        }

        /// <summary>
        /// Toggles the activation state of the item.
        /// </summary>
        public void ToggleItem()
        {
            _activated = !_activated;
        }

        /// <summary>
        /// Draws the item on the screen.
        /// </summary>
        public void DrawItem()
        {
            string price = ((int)_itemPrice).ToString();
            int textWidth = TextWidth(price, "fonts/VCR_OSD_MONO_1.001.ttf", 36);
            int textHeight = TextHeight(price, "fonts/VCR_OSD_MONO_1.001.ttf", 36);
            DrawBitmap(_objectBitmap, _objectDefaultX, _objectDefaultY);
            DrawText(price, ColorBlack(), "fonts/VCR_OSD_MONO_1.001.ttf", 36, _objectDefaultX + ((100 - textWidth) / 2), _objectDefaultY - textHeight);
        }

        /// <summary>
        /// Abstract method to be implemented by derived classes TimerExtender and MassCleaner.
        /// Uses the item in the context of a restaurant.
        /// </summary>
        /// <param name="r"></param>
        public abstract void UseItem(Restaurant r);
    }
}