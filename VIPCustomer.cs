using System;
using SplashKitSDK;
using static SplashKitSDK.SplashKit;
using System.Collections.Generic;

namespace CustomProgram
{
    public class VIPCustomer : Customer
    {
        #nullable disable
        private bool _shouldLeave;

        public VIPCustomer(CustomerType customerType, double x, double y, Bitmap bitmap, DateTime spawnTime, int patienceTimerSeconds, int waitingSeconds, int orderPromptTimerSeconds) : base(customerType, x, y, bitmap, spawnTime, patienceTimerSeconds, waitingSeconds, orderPromptTimerSeconds)
        {
            PayMultiplier = 2;
            PatienceTimer = new Timer(patienceTimerSeconds / 2);
            WaitingPromptTimer = new Timer(waitingSeconds / 2);
            OrderPromptTimer = new Timer(orderPromptTimerSeconds / 2);
            _shouldLeave = false;
        }

        /// <summary>
        /// Places an order for the VIP customer.
        /// This method randomly selects food items from the FoodName enum and creates a new Order object.
        /// </summary>
        public bool ShouldLeave
        {
            get { return _shouldLeave; }
        }

        /// <summary>
        /// Places an order for the VIP customer.
        /// This method randomly selects food items from the FoodName enum and creates a new Order object.
        /// It also randomly selects a drink from the VIPDrink enum and adds it to the order.
        /// </summary>
        public override void PlaceOrder()
        {
            int foodCount = 3;
            List<FoodName> randomFoodNames = new List<FoodName>();
            List<Bitmap> bitmaps = new List<Bitmap>();
            FoodName[] allFoodNames = (FoodName[])Enum.GetValues(typeof(FoodName));
            VIPDrink[] allDrinkNames = (VIPDrink[])Enum.GetValues(typeof(VIPDrink));

            for (int i = 0; i < foodCount; i++)
            {
                int randomFoodIndex = Random.Next(allFoodNames.Length);
                randomFoodNames.Add(allFoodNames[randomFoodIndex]);
                int bitmapID = IncrementCounter++;
                bitmaps.Add(LoadBitmap(allFoodNames[randomFoodIndex].ToString().ToLower() + bitmapID, "images/foods/" + allFoodNames[randomFoodIndex].ToString().ToLower() + ".png"));
            }

            int randomIndex2 = Random.Next(allDrinkNames.Length);
            VIPDrink choice = allDrinkNames[randomIndex2];
            Bitmap drinkBmp = LoadBitmap(choice.ToString().ToLower() + "_order", "images/drinks/" + choice.ToString().ToLower() + ".png");
            bitmaps.Add(LoadBitmap(choice.ToString().ToLower() + IncrementCounter++, "images/drinks/" + choice.ToString().ToLower() + ".png"));

            Order = new Order(bitmaps, ObjectDefaultX + 75, ObjectDefaultY - 10, randomFoodNames);
            Order.Drink = choice;
            OrderPromptTimer.ResetTimer();
            OrderStartTime = DateTime.Now;
            ExpectedReceiveTime = DateTime.Now.AddSeconds(OrderPromptTimer.GetRemainingTime().TotalSeconds);
            OrderCreated = true;
        }

        /// <summary>
        /// Checks if the dropped drink matches the customer's order.
        /// If it matches, it sets the DrinkReceived property to true and stores the dropped drink.
        /// </summary>
        /// <param name="droppedDrink"></param>
        /// <returns></returns>
        public bool CheckReceivedDrink(Drink droppedDrink)
        {
            if (Order != null && !FullOrderReceived && droppedDrink.DrinkType == Order.Drink)
            {
                if (FoodSlotIndices == null)
                {
                    int numberOfSlots = Order.FoodNames.Count;
                    FoodSlotIndices = new List<int>(numberOfSlots);
                    for (int slot = 0; slot < numberOfSlots; slot++)
                    {
                        FoodSlotIndices.Add(slot);
                    }
                }
                Console.WriteLine($"VIPCustomer {CustomerID} got correct drink: {droppedDrink.DrinkType}");
                DrinkReceived = true;
                DroppedDrink = droppedDrink;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if the VIP customer should leave.
        /// </summary>
        public void CheckIfLeave()
        {
            _shouldLeave = true;
        }
    }
}