using System;
using SplashKitSDK;
using static SplashKitSDK.SplashKit;
using System.Collections.Generic;

namespace CustomProgram
{
    public class KarenCustomer : Customer
    {
        private int _framesUntilOrderCheck;
        private int _framesToCheck;
        public KarenCustomer(CustomerType customerType, double x, double y, Bitmap bitmap, DateTime spawnTime, int patienceTimerSeconds, int waitingSeconds, int orderPromptTimerSeconds, int framesUntilOrderCheck) : base(customerType, x, y, bitmap, spawnTime, patienceTimerSeconds, waitingSeconds, orderPromptTimerSeconds)
        {
            PayMultiplier = 0.5m;
            _framesUntilOrderCheck = framesUntilOrderCheck;
            _framesToCheck = _framesUntilOrderCheck;
        }

        /// <summary>
        /// Places an order for the Karen customer.
        /// This method randomly selects food items from the FoodName enum and creates a new Order object.
        /// It also implements a mechanism to randomly change the order after a certain number of frames.
        /// If the order is changed, it resets the order creation process.
        /// </summary>
        public override void PlaceOrder()
        {
            // first order
            if (!OrderCreated)
            {
                int orderItems = 2;
                List<FoodName> randomFoodNames = new List<FoodName>();
                List<Bitmap> bitmaps = new List<Bitmap>();
                FoodName[] enumValues = (FoodName[])Enum.GetValues(typeof(FoodName));

                for (int i = 0; i < orderItems; i++)
                {
                    int randomIndex = Random.Next(enumValues.Length);
                    randomFoodNames.Add(enumValues[randomIndex]);
                    int bitmapID = IncrementCounter++;
                    string key = enumValues[randomIndex].ToString().ToLower();
                    bitmaps.Add(LoadBitmap($"{key}{bitmapID}", $"images/foods/{key}.png"));
                }

                Order = new Order(bitmaps, ObjectDefaultX + 75, ObjectDefaultY - 10, randomFoodNames);
                OrderPromptTimer.ResetTimer();
                OrderStartTime = DateTime.Now;
                ExpectedReceiveTime = DateTime.Now.AddSeconds(OrderPromptTimer.GetRemainingTime().TotalSeconds);
                OrderCreated = true;

                _framesUntilOrderCheck = _framesToCheck;
                return;
            }

            // decrement frame
            _framesUntilOrderCheck--;

            if (_framesUntilOrderCheck <= 0)
            {
                int changeRollDice = Random.Next(0, 100);
                if (changeRollDice < 30) // 30%
                {
                    FoodSlotIndices = null;
                    Order = null;
                    OrderCreated = false;

                    int orderItems = 2;
                    List<FoodName> randomFoodNames = new List<FoodName>();
                    List<Bitmap> bitmaps = new List<Bitmap>();
                    FoodName[] enumValues = (FoodName[])Enum.GetValues(typeof(FoodName));

                    for (int i = 0; i < orderItems; i++)
                    {
                        int randomIndex = Random.Next(enumValues.Length);
                        randomFoodNames.Add(enumValues[randomIndex]);
                        int bitmapID = IncrementCounter++;
                        string key = enumValues[randomIndex].ToString().ToLower();
                        bitmaps.Add(LoadBitmap($"{key}{bitmapID}", $"images/foods/{key}.png"));
                    }

                    Order = new Order(bitmaps, ObjectDefaultX + 75, ObjectDefaultY - 10, randomFoodNames);
                    OrderCreated = true;

                    _framesUntilOrderCheck = _framesToCheck;
                    return;
                }
                _framesUntilOrderCheck = _framesToCheck;
            }
        }
    }
}