using System;
using SplashKitSDK;
using static SplashKitSDK.SplashKit;
using System.Collections.Generic;

namespace CustomProgram
{
    public class Order
    {
        private int _orderID;
        private static int _incrementOrderID = 0;
        private List<Bitmap> _bitmaps;
        private double _objectDefaultX;
        private double _objectDefaultY;
        private double _totalWidth;
        private List<FoodName> _foodNames;
        private VIPDrink _drink;

        public Order(List<Bitmap> bitmaps, double x, double y, List<FoodName> foodNames)
        {
            _orderID = _incrementOrderID++;
            _bitmaps = bitmaps;
            _objectDefaultX = x;
            _objectDefaultY = y;
            _totalWidth = 75 * 2;
            _foodNames = foodNames;
        }

        /// <summary>
        /// Gets or sets the unique identifier for the order.
        /// </summary>
        public int OrderID
        {
            get { return _orderID; }
            set { _orderID = value; }
        }

        /// <summary>
        /// Gets or sets the list of bitmaps representing the order items.
        /// </summary>
        public List<Bitmap> Bitmaps
        {
            get { return _bitmaps; }
            set { _bitmaps = value; }
        }

        /// <summary>
        /// Gets or sets the default X position of the order's bitmap.
        /// </summary>
        public double ObjectDefaultX
        {
            get { return _objectDefaultX; }
            set { _objectDefaultX = value; }
        }

        /// <summary>
        /// Gets or sets the default Y position of the order's bitmap.
        /// </summary>
        public double ObjectDefaultY
        {
            get { return _objectDefaultY; }
            set { _objectDefaultY = value; }
        }

        /// <summary>
        /// Gets or sets the total width of the order's bitmap.
        /// </summary>
        public double TotalWidth
        {
            get { return _totalWidth; }
            set { _totalWidth = value; }
        }

        /// <summary>
        /// Gets or sets the list of food names in the order.
        /// </summary>
        public List<FoodName> FoodNames
        {
            get { return _foodNames; }
            set { _foodNames = value; }
        }

        /// <summary>
        /// Gets or sets the VIP drink enum value associated with the order.
        /// </summary>
        public VIPDrink Drink
        {
            get { return _drink; }
            set { _drink = value; }
        }

        /// <summary>
        /// Draws the order on the screen.
        /// </summary>
        public void DrawOrder()
        {
            if (_bitmaps.Count <= 2)
            {
                // draw white background rectangle
                FillRectangle(Color.White, _objectDefaultX, _objectDefaultY, _totalWidth, 75);
                // draw black border rectangle
                DrawRectangle(Color.Black, _objectDefaultX, _objectDefaultY, _totalWidth, 75);

                double currentBitmapX = _objectDefaultX;
                // draw each bitmap in the order
                for (int i = 0; i < _bitmaps.Count; i++)
                {
                    DrawBitmap(_bitmaps[i], currentBitmapX, _objectDefaultY);
                    currentBitmapX += _bitmaps[i].Width;
                }
            }
            else
            {
                // draw white background rectangle
                FillRectangle(Color.White, _objectDefaultX, _objectDefaultY - 75, _totalWidth, 75 * 2);
                // draw black border rectangle
                DrawRectangle(Color.Black, _objectDefaultX, _objectDefaultY - 75, _totalWidth, 75 * 2);

                double currentBitmapX2 = _objectDefaultX;
                // draw each bitmap in the order
                for (int i = 0; i < _bitmaps.Count; i++)
                {
                    if (i == 2)
                    {
                        currentBitmapX2 = _objectDefaultX;
                    }

                    double yPos;
                    if (i < 2)
                    {
                        // first row
                        yPos = _objectDefaultY - 75;
                    }
                    else
                    {
                        // second row
                        yPos = _objectDefaultY;
                    }

                    DrawBitmap(_bitmaps[i], currentBitmapX2, yPos);
                    currentBitmapX2 += _bitmaps[i].Width;
                }
            }
        }
    }
}