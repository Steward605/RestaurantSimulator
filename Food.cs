using System;
using SplashKitSDK;
using static SplashKitSDK.SplashKit;
using System.Collections.Generic;

namespace CustomProgram
{
    public class Food
    {
        private int _foodID;
        private static int _foodCount = 0;
        private FoodName _foodName;
        private Bitmap _objectBitmap;
        private double _objectDefaultX;
        private double _objectDefaultY;
        private bool _isDragging;
        private double _offsetX;
        private double _offsetY;
        private decimal _foodPrice;

        public Food(FoodName foodName, Bitmap objectBitmap, double x, double y)
        {
            _foodName = foodName;
            _foodID = _foodCount++;
            _objectDefaultX = x;
            _objectDefaultY = y;
            _objectBitmap = objectBitmap;
            _isDragging = false;
            _offsetX = 0;
            _offsetY = 0;
            _foodPrice = SetFoodPrices();
        }

        /// <summary>
        /// Gets the unique ID of the food item.
        /// </summary>
        public int FoodID
        {
            get { return _foodID; }
        }

        /// <summary>
        /// Gets or sets the Food's FoodName enum value.
        /// </summary>
        public FoodName FoodName
        {
            get { return _foodName; }
            set { _foodName = value; }
        }

        /// <summary>
        /// Gets the food's price.
        /// </summary>
        public decimal FoodPrice
        {
            get { return _foodPrice; }
        }

        /// <summary>
        /// Gets or sets the food's bitmap's default X position.
        /// </summary>
        public double ObjectDefaultX
        {
            get { return _objectDefaultX; }
            set { _objectDefaultX = value; }
        }

        /// <summary>
        /// Gets or sets the food's bitmap's default Y position.
        /// </summary>
        public double ObjectDefaultY
        {
            get { return _objectDefaultY; }
            set { _objectDefaultY = value; }
        }

        /// <summary>
        /// Gets or sets the food's bitmap.
        /// </summary>
        public Bitmap ObjectBitmap
        {
            get { return _objectBitmap; }
            set { _objectBitmap = value; }
        }

        /// <summary>
        /// Gets or sets the food's dragging state.
        /// </summary>
        public bool Dragging
        {
            get { return _isDragging; }
            set { _isDragging = value; }
        }

        /// <summary>
        /// Gets or sets the food's bitmap's offset X position.
        /// This property is used to adjust the position of the food item when it is being dragged.
        /// </summary>
        public double OffsetX
        {
            get { return _offsetX; }
            set { _offsetX = value; }
        }

        /// <summary>
        /// Gets or sets the food's bitmap's offset Y position.
        /// This property is used to adjust the position of the food item when it is being dragged.
        /// </summary>
        public double OffsetY
        {
            get { return _offsetY; }
            set { _offsetY = value; }
        }

        /// <summary>
        /// Sets the food prices based on the FoodName enum.
        /// </summary>
        /// <returns></returns>
        public decimal SetFoodPrices()
        {
            switch (_foodName)
            {
                case FoodName.Pizza:
                    return 30;
                case FoodName.Burger:
                    return 7;
                case FoodName.Salad:
                    return 5;
                case FoodName.Pasta:
                    return 17;
                case FoodName.Sushi:
                    return 12;
                case FoodName.Steak:
                    return 15;
                case FoodName.Sandwich:
                    return 10;
                case FoodName.Soup:
                    return 4;
                case FoodName.Fries:
                    return 6;
                case FoodName.Tacos:
                    return 8;
            }
            return 0;
        }
    }
}