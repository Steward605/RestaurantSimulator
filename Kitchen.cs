using System;
using SplashKitSDK;
using static SplashKitSDK.SplashKit;
using System.Collections.Generic;

namespace CustomProgram
{
    public class Kitchen
    {
        #nullable disable
        private List<Food> _foods;
        private Drink _drink;
        private Random random;

        public Kitchen()
        {
            _foods = new List<Food>();
            random = new Random();
        }

        /// <summary>
        /// Gets the list of foods currently available in the kitchen.
        /// </summary>
        public List<Food> Foods
        {
            get { return _foods; }
        }

        /// <summary>
        /// Gets or sets the drink currently available in the kitchen.
        /// </summary>
        public Drink Drink
        {
            get { return _drink; }
        }

        /// <summary>
        /// Draws the kitchen counter where food and drinks are displayed.
        /// </summary>
        public void DrawKitchenCounter()
        {
            // Draw Food Counter
            FillRectangle(ColorBurlyWood(), 300, 680, 975, 100);
            // Thick borders
            DrawRectangle(ColorBlack(), 300, 680, 975, 100);
            DrawRectangle(ColorBlack(), 301, 681, 973, 98);
            DrawRectangle(ColorBlack(), 302, 682, 971, 96);
            // Next Food Indicator Grey Tint
            FillRectangle(RGBAColor(128, 128, 128, 128), 1275, 680, 100, 100);

            // Draw Drink counter
            FillRectangle(ColorBurlyWood(), 100, 680, 100, 100);
            // Thick borders
            DrawRectangle(ColorBlack(), 100, 680, 100, 100);
            DrawRectangle(ColorBlack(), 101, 681, 98, 98);
            DrawRectangle(ColorBlack(), 102, 682, 96, 96);

            if (_drink != null)
            {
                DrawBitmap(_drink.ObjectBitmap, _drink.ObjectDefaultX, _drink.ObjectDefaultY);
            }
        }
        
        /// <summary>
        /// Draws the rubbish bin where players can dispose of items.
        /// </summary>
        public void DrawRubbishBin()
        {
            Bitmap binBitmap = LoadBitmap("bin", "images/trashbin.png");
            double binX = 200;
            double binY = 680;
            DrawBitmap(binBitmap, binX, binY);
        }

        /// <summary>
        /// Manages the drink mechanism in the kitchen.
        /// </summary>
        /// <param name="servedDrink"></param>
        public void ManageDrink(Drink servedDrink)
        {
            double positionX = 100 + (100 - 75) / 2;
            double positionY = 692.5;

            if (_drink == null)
            {
                VIPDrink randomDrink = (VIPDrink)random.Next(Enum.GetValues(typeof(VIPDrink)).Length);
                Bitmap drinkBitmap = LoadBitmap(randomDrink.ToString().ToLower(), $"images/drinks/{randomDrink.ToString().ToLower()}.png");
                _drink = new Drink(randomDrink, drinkBitmap, positionX, positionY);
            }

            if (servedDrink != null)
            {
                _drink = null;
                VIPDrink randomDrink = (VIPDrink)random.Next(Enum.GetValues(typeof(VIPDrink)).Length);
                Bitmap drinkBitmap = LoadBitmap(randomDrink.ToString().ToLower(), $"images/drinks/{randomDrink.ToString().ToLower()}.png");
                _drink = new Drink(randomDrink, drinkBitmap, positionX, positionY);
            }
        }

        /// <summary>
        /// Manages the food mechanism in the kitchen.
        /// </summary>
        /// <param name="servedFood"></param>
        public void ManageFood(Food servedFood)
        {
            double baseX = 400.0;
            double offsetX = 175.0;
            double foodPositionY = 692.5;

            while (_foods.Count < 6)
            {
                FoodName randomFoodName = (FoodName)random.Next(Enum.GetValues(typeof(FoodName)).Length);
                Bitmap foodBitmap = LoadBitmap(randomFoodName.ToString().ToLower(), $"images/foods/{randomFoodName.ToString().ToLower()}.png");
                double spawnX = baseX + (_foods.Count * offsetX);
                _foods.Add(new Food(randomFoodName, foodBitmap, spawnX, foodPositionY));
            }

            if (servedFood != null)
            {
                int servedIndex = _foods.IndexOf(servedFood);
                if (servedIndex >= 0 && servedIndex < 5)
                {
                    // remove the served counter‐food
                    _foods.RemoveAt(servedIndex);

                    // slide everything right of it one slot to the left
                    for (int i = servedIndex; i < 5; i++)
                    {
                        _foods[i].ObjectDefaultX = baseX + (i * offsetX);
                    }

                    // spawn a new “next” preview at slot 5
                    FoodName nextFoodName = (FoodName)random.Next(Enum.GetValues(typeof(FoodName)).Length);
                    Bitmap nextFoodBitmap = LoadBitmap(nextFoodName.ToString().ToLower(), $"images/foods/{nextFoodName.ToString().ToLower()}.png");
                    _foods.Add(new Food(nextFoodName, nextFoodBitmap, baseX + (5 * offsetX), foodPositionY)); // last food (food preview) x should be 1325
                }
            }

            foreach (Food food in _foods)
            {
                DrawBitmap(food.ObjectBitmap, food.ObjectDefaultX, food.ObjectDefaultY);
            }
        }
    }
}