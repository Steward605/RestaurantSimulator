using System;
using SplashKitSDK;
using static SplashKitSDK.SplashKit;
using System.Collections.Generic;

namespace CustomProgram
{
    public class Table
    {
#nullable disable
        private int _tableID;
        private static int _incrementCount = 0;
        private Bitmap _objectBitmap;
        private double _objectDefaultX;
        private double _objectDefaultY;
        private Customer _customer;
        private StainLevel _cleanliness;
        private int _stainDegree;
        private bool _isOccupied;

        public Table(double x, double y, Bitmap tableBitmap)
        {
            _tableID = _incrementCount++;
            _objectDefaultX = x;
            _objectDefaultY = y;
            _objectBitmap = tableBitmap;
            _customer = null;
            _cleanliness = StainLevel.Clean;
            _stainDegree = 0;
            _isOccupied = false;
        }

        /// <summary>
        /// Gets or sets the table's ID.
        /// </summary>
        public int TableID
        {
            get { return _tableID; }
            set { _tableID = value; }
        }

        /// <summary>
        /// Gets or sets the table's bitmap.
        /// </summary>
        public Bitmap TableBitmap
        {
            get { return _objectBitmap; }
            set { _objectBitmap = value; }
        }

        /// <summary>
        /// Gets or sets the table's bitmap's default X position.
        /// </summary>
        public double ObjectDefaultX
        {
            get { return _objectDefaultX; }
            set { _objectDefaultX = value; }
        }

        /// <summary>
        /// Gets or sets the table's bitmap's default Y position.
        /// </summary>
        public double ObjectDefaultY
        {
            get { return _objectDefaultY; }
            set { _objectDefaultY = value; }
        }

        /// <summary>
        /// Gets or sets the customer assigned to this table.
        /// </summary>
        public Customer Customer
        {
            get { return _customer; }
            set { _customer = value; }
        }

        /// <summary>
        /// Gets or sets the cleanliness level of the table.
        /// </summary>
        public StainLevel Cleanliness
        {
            get { return _cleanliness; }
            set { _cleanliness = value; }
        }

        /// <summary>
        /// Gets or sets the degree of stain on the table.
        /// </summary>
        public int StainDegree
        {
            get { return _stainDegree; }
            set { _stainDegree = value; }
        }

        /// <summary>
        /// Gets or sets the boolean indicating if the table is occupied.
        /// </summary>
        public bool IsOccupied
        {
            get { return _isOccupied; }
            set { _isOccupied = value; }
        }

        /// <summary>
        /// Draws the table and any food served at this table.
        /// </summary>
        public void DrawTable()
        {
            DrawBitmap(_objectBitmap, _objectDefaultX, _objectDefaultY);
            // then draw any foods served at this table
            if (_customer != null)
            {
                foreach (Food food in _customer.TotalReceivedFoods)
                {
                    DrawBitmap(food.ObjectBitmap, food.ObjectDefaultX, food.ObjectDefaultY);
                }
                if (_customer.DrinkReceived && _customer.DroppedDrink != null)
                {
                    DrawBitmap(_customer.DroppedDrink.ObjectBitmap, _customer.DroppedDrink.ObjectDefaultX, _customer.DroppedDrink.ObjectDefaultY);
                }
            }
        }

        /// <summary>
        /// Draws the stain on the table based on its cleanliness level.
        /// </summary>
        public void DrawStain()
        {
            Bitmap stainBitmap = null;
            if (_cleanliness == StainLevel.Dirty)
            {
                stainBitmap = LoadBitmap("dirty", "images/stainlevel/dirty-stain.png");
                double stainX = _objectDefaultX + (_objectBitmap.Width - stainBitmap.Width) / 2;
                double stainY = _objectDefaultY + (_objectBitmap.Height - stainBitmap.Height) / 2;
                DrawBitmap(stainBitmap, stainX, stainY - 15);
            }
            else if (_cleanliness == StainLevel.SemiDirty)
            {
                stainBitmap = LoadBitmap("semi-dirty", "images/stainlevel/semidirty-stain.png");
                double stainX = _objectDefaultX + (_objectBitmap.Width - stainBitmap.Width) / 2;
                double stainY = _objectDefaultY + (_objectBitmap.Height - stainBitmap.Height) / 2;
                DrawBitmap(stainBitmap, stainX, stainY - 15);
            }
            else if (_cleanliness == StainLevel.Clean)
            {
                stainBitmap = LoadBitmap("clean", "images/stainlevel/clean-stain.png");
                double stainX = _objectDefaultX + (_objectBitmap.Width - stainBitmap.Width) / 2;
                double stainY = _objectDefaultY + (_objectBitmap.Height - stainBitmap.Height) / 2;
                DrawBitmap(stainBitmap, stainX, stainY - 15);
            }
        }

        /// <summary>
        /// Checks the stain level of the table based on the degree of stain.
        /// </summary>
        public void CheckStain()
        {
            switch (_stainDegree)
            {
                case 0:
                    _cleanliness = StainLevel.Clean;
                    break;
                case 1:
                    _cleanliness = StainLevel.SemiDirty;
                    break;
                case 2:
                    _cleanliness = StainLevel.Dirty;
                    break;
            }
        }
    }
}