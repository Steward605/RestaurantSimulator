using System;
using SplashKitSDK;
using static SplashKitSDK.SplashKit;
using System.Collections.Generic;

namespace CustomProgram
{
    public class Drink
    {
        private int _drinkID;
        private static int _incrementCount = 0;
        private VIPDrink _vipDrink;
        private decimal _drinkPrice;
        private Bitmap _objectBitmap;
        private double _objectDefaultX;
        private double _objectDefaultY;
        private bool _isDragging;
        private double _offsetX;
        private double _offsetY;

        public Drink(VIPDrink vipDrink, Bitmap objectBitmap, double x, double y)
        {
            _drinkID = _incrementCount++;
            _vipDrink = vipDrink;
            _objectDefaultX = x;
            _objectDefaultY = y;
            _objectBitmap = objectBitmap;
            _isDragging = false;
            _offsetX = 0;
            _offsetY = 0;
            _drinkPrice = SetDrinkPrices();
        }

        /// <summary>
        /// Gets the unique ID of the drink.
        /// </summary>
        public int DrinkID
        {
            get { return _drinkID; }
        }

        /// <summary>
        /// Gets or sets the drink's VIP drink type.
        /// This property represents the type of drink that the VIP customer has ordered.
        /// </summary>
        public VIPDrink DrinkType
        {
            get { return _vipDrink; }
            set { _vipDrink = value; }
        }

        /// <summary>
        /// Gets the drink's price.
        /// This property returns the price of the drink based on its type.
        /// </summary>
        public decimal DrinkPrice
        {
            get { return _drinkPrice; }
        }

        /// <summary>
        /// Gets or sets the drink's bitmap.
        /// This bitmap represents the visual icon of the drink in the game.
        /// </summary>
        public Bitmap ObjectBitmap
        {
            get { return _objectBitmap; }
            set { _objectBitmap = value; }
        }

        /// <summary>
        /// Gets or sets the drink's bitmap's default X position.
        /// </summary>
        public double ObjectDefaultX
        {
            get { return _objectDefaultX; }
            set { _objectDefaultX = value; }
        }

        /// <summary>
        /// Gets or sets the drink's bitmap's default Y position.
        /// </summary>
        public double ObjectDefaultY
        {
            get { return _objectDefaultY; }
            set { _objectDefaultY = value; }
        }

        /// <summary>
        /// Gets or sets the drink's dragging state.
        /// This property indicates whether the drink is currently being dragged by the player.
        /// </summary>
        public bool Dragging
        {
            get { return _isDragging; }
            set { _isDragging = value; }
        }

        /// <summary>
        /// Gets or sets the offset X position of the drink.
        /// This offset is used to adjust the position of the drink when it is being dragged.
        /// </summary>
        public double OffsetX
        {
            get { return _offsetX; }
            set { _offsetX = value; }
        }

        /// <summary>
        /// Gets or sets the offset Y position of the drink.
        /// This offset is used to adjust the position of the drink when it is being dragged.
        /// </summary>
        public double OffsetY
        {
            get { return _offsetY; }
            set { _offsetY = value; }
        }

        /// <summary>
        /// Sets the drink prices based on the VIP drink type.
        /// This method returns the price of the drink based on its type.
        /// </summary>
        /// <returns></returns>
        public decimal SetDrinkPrices()
        {
            switch (_vipDrink)
            {
                case VIPDrink.Wine:
                    return 100;
                case VIPDrink.Tequila:
                    return 80;
                case VIPDrink.Bourbon:
                    return 110;
                case VIPDrink.Champagne:
                    return 130;
                case VIPDrink.Whiskey:
                    return 90;
                case VIPDrink.Beer:
                    return 40;
            }
            return 0;
        }
    }
}