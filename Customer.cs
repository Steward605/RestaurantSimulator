using System;
using SplashKitSDK;
using static SplashKitSDK.SplashKit;
using System.Collections.Generic;

namespace CustomProgram
{
    public abstract class Customer
    {
        #nullable disable
        private int _customerID;
        private static int _incrementCounter = 0;
        private CustomerType _customerType;
        private decimal _payMultiplier;
        private Bitmap _objectBitmap;
        private double _objectDefaultX;
        private double _objectDefaultY;
        private bool _isDragging;
        private double _offsetX;
        private double _offsetY;
        private DateTime _spawnTime;
        private Timer _patienceTimer;
        private Table _table;
        private StainLevel _currentCleanliness;
        private bool _isSeated;
        private Timer _waitingPromptTimer;
        private bool _waitingToBePrompted;
        private Timer _orderPromptTimer;
        private bool _prompted;
        private bool _orderCreated;
        private Order _order;
        private List<Food> _totalReceivedFoods;
        private DateTime _orderStartTime;
        private DateTime _orderFulfilledTime;
        private DateTime _expectedReceiveTime;
        private bool _fullOrderReceived;
        private Satisfaction _satisfaction;
        private List<int> _foodSlotIndices;
        private Drink _droppedDrink;
        private bool _drinkReceived;
        private Random _random;

        public Customer(CustomerType customerType, double x, double y, Bitmap bitmap, DateTime spawnTime, int patienceTimerSeconds, int waitingSeconds, int orderPromptTimerSeconds)
        {
            _customerID = _incrementCounter++;
            _customerType = customerType;
            _objectDefaultX = x;
            _objectDefaultY = y;
            _objectBitmap = bitmap;
            _isDragging = false;
            _spawnTime = spawnTime;
            _isSeated = false;
            _totalReceivedFoods = new List<Food>();
            _fullOrderReceived = false;
            _offsetX = 0;
            _offsetY = 0;
            _patienceTimer = new Timer(patienceTimerSeconds);
            _waitingPromptTimer = new Timer(waitingSeconds);
            _orderPromptTimer = new Timer(orderPromptTimerSeconds);
            _waitingToBePrompted = false;
            _prompted = false;
            _orderCreated = false;
            _payMultiplier = 1;
            _drinkReceived = false;
            _random = new Random();
        }

        /// <summary>
        /// Gets the unique identifier for the customer.
        /// This ID is used to differentiate between different customers in the system.
        /// </summary>
        public int CustomerID
        {
            get { return _customerID; }
        }

        /// <summary>
        /// Gets the increment counter for generating unique identifiers.
        /// This counter is incremented each time a new customer is created,
        /// ensuring that each customer has a unique ID.
        /// </summary>
        public int IncrementCounter
        {
            get { return _incrementCounter; }
            set { _incrementCounter = value; }
        }

        /// <summary>
        /// Gets or sets the pay multiplier for the customer.
        /// The pay multiplier affects the amount of money the customer pays for their order.
        /// </summary>
        public decimal PayMultiplier
        {
            get { return _payMultiplier; }
            set { _payMultiplier = value; }
        }

        /// <summary>
        /// Gets or sets the bitmap representing the customer object.
        /// This bitmap is used to visually represent the customer in the game.
        /// </summary>
        public Bitmap ObjectBitmap
        {
            get { return _objectBitmap; }
            set { _objectBitmap = value; }
        }

        /// <summary>
        /// Gets or sets the default X position of the customer object.
        /// This position is used to draw the customer on the screen.
        /// </summary>
        public double ObjectDefaultX
        {
            get { return _objectDefaultX; }
            set { _objectDefaultX = value; }
        }

        /// <summary>
        /// Gets or sets the default Y position of the customer object.
        /// This position is used to draw the customer on the screen.
        /// </summary>
        public double ObjectDefaultY
        {
            get { return _objectDefaultY; }
            set { _objectDefaultY = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the customer is currently being dragged.
        /// This property is used to track if the customer is being moved by the player.
        /// </summary>
        public bool Dragging
        {
            get { return _isDragging; }
            set { _isDragging = value; }
        }

        /// <summary>
        /// Gets or sets the X offset for the customer object.
        /// This offset is used to adjust the position of the customer when dragging.
        /// </summary>
        public double OffsetX
        {
            get { return _offsetX; }
            set { _offsetX = value; }
        }

        /// <summary>
        /// Gets or sets the Y offset for the customer object.
        /// This offset is used to adjust the position of the customer when dragging.
        /// </summary>
        public double OffsetY
        {
            get { return _offsetY; }
            set { _offsetY = value; }
        }

        /// <summary>
        /// Gets or sets the type of customer.
        /// This property indicates whether the customer is a normal customer, a VIP customer, or a Karen customer.
        /// </summary>
        public bool IsSeated
        {
            get { return _isSeated; }
            set { _isSeated = value; }
        }

        /// <summary>
        /// Gets or sets the spawn time of the customer.
        /// This property indicates when the customer was spawned in the game.
        /// </summary>
        public DateTime SpawnTime
        {
            get { return _spawnTime; }
            set { _spawnTime = value; }
        }

        /// <summary>
        /// Gets or sets the patience timer for the customer.
        /// This timer is used to track how long the customer is willing to wait before leaving.
        /// </summary>
        public Timer PatienceTimer
        {
            get { return _patienceTimer; }
            set { _patienceTimer = value; }
        }

        /// <summary>
        /// Gets or sets the waiting prompt timer for the customer.
        /// This timer is used to track how long the customer has been waiting to be prompted for their order.
        /// </summary>
        public bool WaitingToBePrompted
        {
            get { return _waitingToBePrompted; }
            set { _waitingToBePrompted = value; }
        }

        /// <summary>
        /// Gets or sets the waiting prompt timer for the customer.
        /// This timer is used to track how long the customer has been waiting before they are prompted for their order.
        /// </summary>
        public Timer WaitingPromptTimer
        {
            get { return _waitingPromptTimer; }
            set { _waitingPromptTimer = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the customer has been prompted for their order.
        /// This property is used to track if the customer has already been prompted to place an order.
        /// </summary>
        public bool Prompted
        {
            get { return _prompted; }
            set { _prompted = value; }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether an order has been created for the customer.
        /// This property is used to track if the customer has placed an order.
        /// </summary>
        public bool OrderCreated
        {
            get { return _orderCreated; }
            set { _orderCreated = value; }
        }
        
        /// <summary>
        /// Gets or sets the order prompt timer for the customer.
        /// This timer is used to track how long the customer has been waiting for their order to be fulfilled.
        /// </summary>
        public Timer OrderPromptTimer
        {
            get { return _orderPromptTimer; }
            set { _orderPromptTimer = value; }
        }

        /// <summary>
        /// Gets or sets the type of customer.
        /// This property indicates whether the customer is a normal customer, a VIP customer, or a Karen customer.
        /// </summary>
        public Table Table
        {
            get { return _table; }
            set { _table = value; }
        }

        /// <summary>
        /// Gets or sets the cleanliness level of the table where the customer is seated.
        /// This property indicates how clean the table is, which can affect the customer's satisfaction.
        /// </summary>
        public StainLevel TableCleanliness
        {
            get { return _currentCleanliness; }
            set { _currentCleanliness = value; }
        }

        /// <summary>
        /// Gets or sets the order placed by the customer.
        /// This property contains the details of the food and drink items ordered by the customer.
        /// </summary>
        public Order Order
        {
            get { return _order; }
            set { _order = value; }
        }

        /// <summary>
        /// Gets or sets the list of total received foods by the customer.
        /// This property contains all the food items that the customer has received so far.
        /// </summary>
        public List<Food> TotalReceivedFoods
        {
            get { return _totalReceivedFoods; }
            set { _totalReceivedFoods = value; }
        }

        /// <summary>
        /// Gets or sets the start time of the customer's order.
        /// This property indicates when the customer started their order, which can be used to track how long they have been waiting.
        /// </summary>
        public DateTime OrderStartTime
        {
            get { return _orderStartTime; }
            set { _orderStartTime = value; }
        }

        /// <summary>
        /// Gets or sets the time when the customer's order was fulfilled.
        /// This property indicates when the customer received their order, which is used to calculate satisfaction.
        /// </summary>
        public DateTime OrderFulfilledTime
        {
            get { return _orderFulfilledTime; }
            set { _orderFulfilledTime = value; }
        }

        /// <summary>
        /// Gets or sets the expected time when the customer should receive their order.
        /// This property is calculated based on the order prompt timer and indicates when the customer expects to receive their food and drink.
        /// </summary>
        public DateTime ExpectedReceiveTime
        {
            get { return _expectedReceiveTime; }
            set { _expectedReceiveTime = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the customer has received their full order.
        /// This property is used to track if the customer has received all the food and drink items they ordered.
        /// </summary>
        public bool FullOrderReceived
        {
            get { return _fullOrderReceived; }
            set { _fullOrderReceived = value; }
        }

        /// <summary>
        /// Gets or sets the satisfaction level of the customer.
        /// This property contains the satisfaction details of the customer based on their order fulfillment and waiting time.
        /// </summary>
        public Satisfaction Satisfaction
        {
            get { return _satisfaction; }
            set { _satisfaction = value; }
        }

        /// <summary>
        /// Gets or sets the list of indices of food slots that have been filled.
        /// This property is used to track which food items have been received by the customer.
        /// </summary>
        public List<int> FoodSlotIndices
        {
            get { return _foodSlotIndices; }
            set { _foodSlotIndices = value; }
        }

        /// <summary>
        /// Gets or sets the drink that has been dropped by the customer.
        /// This property is used to track the drink that the customer has received, especially for VIP customers.
        /// </summary>
        public Drink DroppedDrink
        {
            get { return _droppedDrink; }
            set { _droppedDrink = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether the customer has received their drink.
        /// This property is specifically used for VIP customers to track if they have received the drink they ordered.
        /// </summary>
        public bool DrinkReceived
        {
            get { return _drinkReceived; }
            set { _drinkReceived = value; }
        }
        
        /// <summary>
        /// Gets or sets the random number generator used for various random operations in the customer class.
        /// This property allows for consistent random behavior across different instances of the customer.
        /// </summary>
        public Random Random
        {
            get { return _random; }
            set { _random = value; }
        }

        /// <summary>
        /// Draws the customer on the screen.
        /// This method draws the customer's bitmap at the assigned position.
        /// If the customer is a Karen or VIP, it also draws an additional icon to indicate their type.
        /// The method also checks if the customer has an order and draws the correct received food items with a green tick mark.
        /// </summary>
        public void DrawCustomer()
        {
            if (_customerType == CustomerType.Normal)
            {
                DrawBitmap(_objectBitmap, _objectDefaultX, _objectDefaultY);
            }
            else if (_customerType == CustomerType.Karen)
            {
                DrawBitmap(_objectBitmap, _objectDefaultX, _objectDefaultY);
                DrawBitmap(LoadBitmap("karen-icon", "images/customertypes/karen-icon.png"), _objectDefaultX, _objectDefaultY);
            }
            else
            {
                DrawBitmap(_objectBitmap, _objectDefaultX, _objectDefaultY);
                DrawBitmap(LoadBitmap("vip-icon", "images/customertypes/vip-icon.png"), _objectDefaultX, _objectDefaultY);
            }

            if (_order != null && _order.FoodNames != null)
            {
                List<Bitmap> bitmaps = _order.Bitmaps;
                double objectX = _order.ObjectDefaultX;
                double objectY = _order.ObjectDefaultY;
                Bitmap greenTickBitmap = LoadBitmap("GreenTick", "images/checkmarks/correct-mark.png");

                if (bitmaps.Count <= 2)
                {
                    if (_foodSlotIndices != null)
                    {
                        double currentX = objectX;
                        double iconY = objectY;

                        for (int i = 0; i < bitmaps.Count; i++)
                        {
                            if (!_foodSlotIndices.Contains(i))
                            {
                                Bitmap iconBmp = bitmaps[i];
                                double iconW = iconBmp.Width;
                                double iconH = iconBmp.Height;

                                double tickX = currentX + (iconW - greenTickBitmap.Width) / 2;
                                double tickY = iconY + (iconH - greenTickBitmap.Height) / 2;
                                DrawBitmap(greenTickBitmap, tickX, tickY);  // centered on icon i
                            }

                            currentX += bitmaps[i].Width;
                        }
                    }

                    return;
                }

                if (_foodSlotIndices != null)
                {
                    double currentX = objectX;
                    double currentY;

                    for (int i = 0; i < bitmaps.Count; i++)
                    {
                        if (i < 2)
                        {
                            currentY = objectY - 75;
                        }
                        else
                        {
                            if (i == 2)
                            {
                                currentX = objectX;
                            }
                            currentY = objectY;
                        }

                        if (i < 3 && !_foodSlotIndices.Contains(i))
                        {
                            Bitmap slotBmp = bitmaps[i];
                            double slotW = slotBmp.Width;
                            double slotH = slotBmp.Height;

                            double markX = currentX + (slotW - greenTickBitmap.Width) / 2;
                            double markY = currentY + (slotH - greenTickBitmap.Height) / 2;
                            DrawBitmap(greenTickBitmap, markX, markY);
                        }

                        if (i == 3
                            && _droppedDrink != null
                            && DrinkReceived
                            && _droppedDrink.DrinkType == _order.Drink)
                        {
                            Bitmap slotBmp = bitmaps[i];
                            double slotW = slotBmp.Width;
                            double slotH = slotBmp.Height;

                            double markX = currentX + (slotW - greenTickBitmap.Width) / 2;
                            double markY = currentY + (slotH - greenTickBitmap.Height) / 2;
                            DrawBitmap(greenTickBitmap, markX, markY);
                        }

                        currentX += bitmaps[i].Width;
                    }
                }
            }
        }

        /// <summary>
        /// Checks if the dropped food matches any of the food items in the customer's order.
        /// This method iterates through the food slot indices and compares the dropped food's name with the names in the order.
        /// </summary>
        /// <param name="droppedFood"></param>
        /// <returns></returns>
        public bool CheckReceivedFood(Food droppedFood)
        {
            if (_order == null || _order.FoodNames == null)
            {
                return false;
            }

            if (_foodSlotIndices == null)
            {
                int numberOfSlots = _order.FoodNames.Count;
                _foodSlotIndices = new List<int>(numberOfSlots);
                for (int slot = 0; slot < numberOfSlots; slot++)
                {
                    _foodSlotIndices.Add(slot);
                }
            }

            for (int i = 0; i < _foodSlotIndices.Count; i++)
            {
                int slotIndex = _foodSlotIndices[i];
                FoodName slotName = _order.FoodNames[slotIndex];

                if (slotName == droppedFood.FoodName)
                {
                    _foodSlotIndices.RemoveAt(i);
                    _totalReceivedFoods.Add(droppedFood);
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Checks if the customer's order has been fulfilled.
        /// This method checks if the customer is a VIP or not, and verifies if all food items and drinks have been received.
        /// </summary>
        /// <returns></returns>
        public bool CheckOrderFulfilled()
        {
            if (_customerType != CustomerType.VIP)
            {
                if (_order == null || _foodSlotIndices == null)
                {
                    return false;
                }
                if (_foodSlotIndices.Count == 0)
                {
                    _fullOrderReceived = true;
                    _orderFulfilledTime = DateTime.Now;
                    _satisfaction = new Satisfaction();
                    return true;
                }
                return false;
            }
            else
            {

                if (_order == null || _foodSlotIndices == null || _drinkReceived == false)
                {
                    return false;
                }
                if (_foodSlotIndices.Count == 0 && _drinkReceived == true)
                {
                    _fullOrderReceived = true;
                    _orderFulfilledTime = DateTime.Now;
                    _satisfaction = new Satisfaction();
                    return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Places an order for the customer.
        /// This method is abstract and must be implemented by NormalCustomer, VIPCustomer, and KarenCustomer classes.
        /// </summary>
        public abstract void PlaceOrder();
        // {
        //     int orderItems = 2;
        //     List<FoodName> randomFoodNames = new List<FoodName>();
        //     List<Bitmap> bitmaps = new List<Bitmap>();
        //     FoodName[] enumValues = (FoodName[])Enum.GetValues(typeof(FoodName));
        //     for (int i = 0; i < orderItems; i++)
        //     {
        //         // Randomly select a food item from the enum
        //         int randomIndex = _rnd.Next(enumValues.Length);
        //         randomFoodNames.Add(enumValues[randomIndex]);
        //         // Create a bitmap for the selected food item
        //         int bitmapID = _incrementCounter++;
        //         bitmaps.Add(LoadBitmap(enumValues[randomIndex].ToString().ToLower() + bitmapID.ToString(), "images/foods/" + enumValues[randomIndex].ToString().ToLower() + ".png"));
        //     }
        //     _order = new Order(bitmaps, _objectDefaultX + 75, _objectDefaultY - 10, randomFoodNames);
        //     _orderPromptTimer.ResetTimer();
        //     _orderStartTime = DateTime.Now;
        //     _expectedReceiveTime = DateTime.Now.AddSeconds(_orderPromptTimer.GetRemainingTime().TotalSeconds);
        //     _orderCreated = true;
        // }
    }
}