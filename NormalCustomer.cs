using System;
using SplashKitSDK;
using static SplashKitSDK.SplashKit;
using System.Collections.Generic;

namespace CustomProgram
{
    public class NormalCustomer : Customer
    {
        public NormalCustomer(CustomerType customerType, double x, double y, Bitmap bitmap, DateTime spawnTime, int patienceTimerSeconds, int waitingSeconds, int orderPromptTimerSeconds) : base(customerType, x, y, bitmap, spawnTime, patienceTimerSeconds, waitingSeconds, orderPromptTimerSeconds) { }

        /// <summary>
        /// Places an order for the normal customer.
        /// This method randomly selects food items from the FoodName enum and creates a new Order object.
        /// </summary>
        public override void PlaceOrder()
        {
            int orderItems = 2;
            List<FoodName> randomFoodNames = new List<FoodName>();
            List<Bitmap> bitmaps = new List<Bitmap>();
            FoodName[] enumValues = (FoodName[])Enum.GetValues(typeof(FoodName));
            for (int i = 0; i < orderItems; i++)
            {
                // Randomly select a food item from the enum
                int randomIndex = Random.Next(enumValues.Length);
                randomFoodNames.Add(enumValues[randomIndex]);
                // Create a bitmap for the selected food item
                int bitmapID = IncrementCounter++;
                bitmaps.Add(LoadBitmap(enumValues[randomIndex].ToString().ToLower() + bitmapID.ToString(), "images/foods/" + enumValues[randomIndex].ToString().ToLower() + ".png"));
            }
            Order = new Order(bitmaps, ObjectDefaultX + 75, ObjectDefaultY - 10, randomFoodNames);
            OrderPromptTimer.ResetTimer();
            OrderStartTime = DateTime.Now;
            ExpectedReceiveTime = DateTime.Now.AddSeconds(OrderPromptTimer.GetRemainingTime().TotalSeconds);
            OrderCreated = true;
        }
    }
}