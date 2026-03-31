using System;
using SplashKitSDK;
using static SplashKitSDK.SplashKit;
using System.Collections.Generic;

namespace CustomProgram
{
    public class Restaurant
    {
        #nullable disable
        private List<Table> _tables;
        private List<Customer> _customers;
        private List<Action> _actions;
        private List<ShopItem> _shopItems;
        private Kitchen _kitchen;
        private Timer _gameTimer;
        private Event _event;
        public bool _eventActivated;
        private int _eventFrameCounter;
        private decimal _moneyEarned;
        private decimal _lastMoneyAdded;
        private bool _isPaused;
        private double _windowWidth;
        private double _windowHeight;
        private int _moneyDisplayFramesLeft;
        private Random _random;
        public Restaurant(double width, double height, Kitchen kitchen, Timer gameTimer)
        {
            _windowWidth = width;
            _windowHeight = height;
            _customers = new List<Customer>();
            _tables = new List<Table>();
            _actions = new List<Action>();
            _shopItems = new List<ShopItem>();
            _random = new Random();
            _kitchen = kitchen;
            _gameTimer = gameTimer;
            _moneyEarned = 0;
            _moneyDisplayFramesLeft = 0;
            _lastMoneyAdded = 0;
            _isPaused = false;
            _event = null;
            _eventActivated = false;
            _eventFrameCounter = 0;
        }

        /// <summary>
        /// Gets the list of customers in the restaurant.
        /// This property returns the _customers list, which contains all the Customer objects currently in the restaurant.
        /// </summary>
        public List<Customer> Customers
        {
            get { return _customers; }
        }

        /// <summary>
        /// Gets the list of tables in the restaurant.
        /// This property returns the _tables list, which contains all the Table objects representing the tables in the restaurant.
        /// </summary>
        public List<Table> Tables
        {
            get { return _tables; }
        }

        /// <summary>
        /// Gets the list of actions available in the restaurant.
        /// This property returns the _actions list, which contains all the Action objects that can be performed in the restaurant.
        /// </summary>
        public List<Action> Actions
        {
            get { return _actions; }
        }

        /// <summary>
        /// Gets or sets the list of shop items available in the restaurant.
        /// This property returns the _shopItems list, which contains all the ShopItem objects that can be purchased in the restaurant.
        /// </summary>
        public List<ShopItem> ShopItems
        {
            get { return _shopItems; }
            set { _shopItems = value; }
        }

        /// <summary>
        /// Gets the kitchen of the restaurant.
        /// This property returns the Kitchen object that represents the kitchen of the restaurant.
        /// </summary>
        public Kitchen Kitchen
        {
            get { return _kitchen; }
        }

        /// <summary>
        /// Gets or sets the game timer for the restaurant.
        /// This property returns the Timer object that tracks the game time for the restaurant.
        /// </summary>
        public Timer GameTimer
        {
            get { return _gameTimer; }
            set { _gameTimer = value; }
        }

        /// <summary>
        /// Gets or sets the total money earned in the restaurant.
        /// This property returns the total amount of money earned, which is represented as a decimal value.
        /// </summary>
        public decimal MoneyEarned
        {
            get { return _moneyEarned; }
            set { _moneyEarned = value; }
        }

        /// <summary>
        /// Gets or sets the frames left for displaying money.
        /// This property returns the number of frames left for displaying the last added money amount.
        /// </summary>
        public bool IsPaused
        {
            get { return _isPaused; }
            set { _isPaused = value; }
        }

        /// <summary>
        /// Draws the front and back doors of the restaurant.
        /// </summary>
        public void DrawDoors()
        {
            // Draw front door
            int textWidth = TextWidth("Front Door", "fonts/VCR_OSD_MONO_1.001.ttf", 36);
            int textHeight = TextHeight("Front Door", "fonts/VCR_OSD_MONO_1.001.ttf", 36);
            FillRectangle(ColorBurlyWood(), 120, 10, textWidth + 5, textHeight + 3);
            // Thick borders
            DrawRectangle(ColorBlack(), 120, 10, textWidth + 5, textHeight + 3);
            DrawRectangle(ColorBlack(), 121, 11, textWidth + 3, textHeight + 1);
            DrawRectangle(ColorBlack(), 122, 12, textWidth + 1, textHeight - 1);
            DrawText("Front Door", ColorBlack(), "VCR_OSD_MONO_1.001.ttf", 36, 122.5, 10);

            // Draw Back Door
            textWidth = TextWidth("Back Door", "fonts/VCR_OSD_MONO_1.001.ttf", 36);
            textHeight = TextHeight("Back Door", "fonts/VCR_OSD_MONO_1.001.ttf", 36);
            FillRectangle(ColorBurlyWood(), 1300, 10, textWidth + 5, textHeight + 3);
            // Thick borders
            DrawRectangle(ColorBlack(), 1300, 10, textWidth + 5, textHeight + 3);
            DrawRectangle(ColorBlack(), 1301, 11, textWidth + 3, textHeight + 1);
            DrawRectangle(ColorBlack(), 1302, 12, textWidth + 1, textHeight - 1);
            DrawText("Back Door", ColorBlack(), "fonts/VCR_OSD_MONO_1.001.ttf", 36, 1302.5, 10);
        }

        /// <summary>
        /// Spawns a new customer of random type in the restaurant.
        /// If there are already 8 customers, it does nothing.
        /// </summary>
        public void SpawnCustomer()
        {
            int patienceTimerSeconds = 9;
            int waitingSeconds = 9;
            int orderPromptTimerSeconds = 30;
            int randomInteger = _random.Next(1, 11);
            if (_customers.Count >= 8)
            {
                return;
            }
            for (int i = _customers.Count - 1; i >= 0; i--)
            {
                Customer existingCustomer = _customers[i];
                // Only move customers that haven’t been dragged off yet
                if (!existingCustomer.Dragging && existingCustomer.ObjectDefaultX == 180 - 20)
                {
                    // Shift them down one “cell”
                    existingCustomer.ObjectDefaultY += 75;
                }
            }

            int randomChance = _random.Next(100);
            if (randomChance < 60) // 60%
            {
                _customers.Add(new NormalCustomer(CustomerType.Normal, 180 - 20, 75, LoadBitmap($"c{randomInteger}", $"images/customers/customer{randomInteger}.png"), DateTime.Now, patienceTimerSeconds, waitingSeconds, orderPromptTimerSeconds));
            }
            else if (randomChance < 80) // 20%
            {
                _customers.Add(new KarenCustomer(CustomerType.Karen, 180 - 20, 75, LoadBitmap($"c{randomInteger}", $"images/customers/customer{randomInteger}.png"), DateTime.Now, patienceTimerSeconds, waitingSeconds, orderPromptTimerSeconds, 60));
            }
            else // 20%
            {
                _customers.Add(new VIPCustomer(CustomerType.VIP, 180 - 20, 75, LoadBitmap($"c{randomInteger}", $"images/customers/customer{randomInteger}.png"), DateTime.Now, patienceTimerSeconds, waitingSeconds, orderPromptTimerSeconds));
            }
        }

        /// <summary>
        /// Removes a customer from the restaurant.
        /// If the customer is sitting at a table, it clears out any foods on that table and sets the table to unoccupied.
        /// </summary>
        /// <param name="customer"></param>
        public void RemoveCustomer(Customer customer)
        {
            if (_customers.Contains(customer))
            {
                if (customer.Table != null)
                {
                    // clear out any foods on that table
                    customer.Table.Customer.TotalReceivedFoods.Clear();
                    customer.Table.IsOccupied = false;
                    customer.Table.Customer = null;
                    customer.Table = null;
                }
                _customers.Remove(customer);
            }
        }

        /// <summary>
        /// Initializes the tables in the restaurant.
        /// Each table is represented by a Table object with a specific position and a bitmap for the table image.
        /// </summary>
        public void InitializeTables()
        {
            _tables.Clear();
            Bitmap tableBitmap = LoadBitmap("TableImage", "images/table.png");
            _tables.Add(new Table(350, 180, tableBitmap)); // Table 1
            _tables.Add(new Table(350, 430, tableBitmap)); // Table 2
            _tables.Add(new Table(600, 180, tableBitmap)); // Table 3
            _tables.Add(new Table(600, 430, tableBitmap)); // Table 4
            _tables.Add(new Table(850, 180, tableBitmap)); // Table 5
            _tables.Add(new Table(850, 430, tableBitmap)); // Table 6
            _tables.Add(new Table(1100, 180, tableBitmap)); // Table 7
            _tables.Add(new Table(1100, 430, tableBitmap)); // Table 8
        }

        /// <summary>
        /// Draws all the tables in the restaurant.
        /// It iterates through each table in the _tables list, draws the table, checks for any stains, and draws them if present.
        /// </summary>
        public void DrawTables()
        {
            foreach (Table table in _tables)
            {
                table.DrawTable();
                table.CheckStain();
                table.DrawStain();
            }
        }

        /// <summary>
        /// Initializes the actions available in the restaurant.
        /// It creates instances of MenuAction and CleanAction, each with a specific position and bitmap.
        /// </summary>
        public void InitializeActions()
        {
            // add menu action
            Bitmap menuActionBitmap = LoadBitmap("MenuAction", "images/actions/menu.png");
            _actions.Add(new MenuAction(0, "MenuAction", menuActionBitmap, 20, 212.5));
            // add clean action
            Bitmap cleanActionBitmap = LoadBitmap("CleanAction", "images/actions/clean.png");
            _actions.Add(new CleanAction(1, "CleanAction", cleanActionBitmap, 20, 487.5));
        }

        /// <summary>
        /// Draws the actions panel on the screen.
        /// It fills a rectangle with a specific color, draws thick borders around it, and then iterates through each action in the _actions list to draw them.
        /// </summary>
        public void DrawActions()
        {
            // Draw Actions Panel
            FillRectangle(ColorBurlyWood(), 20, 125, 100, 550);
            // Thick borders
            DrawRectangle(ColorBlack(), 20, 125, 100, 550);
            DrawRectangle(ColorBlack(), 21, 126, 98, 548);
            DrawRectangle(ColorBlack(), 22, 127, 96, 546);
            foreach (Action action in _actions)
            {
                action.DrawAction();
            }
        }

        /// <summary>
        /// Initializes the shop items available in the restaurant.
        /// It creates instances of TimerExtender and MassCleaner, each with a specific position and bitmap.
        /// </summary>
        public void InitializeShopItems()
        {
            double panelX = 1380;
            double panelY = (_windowHeight - 400) / 2;
            // Timer Extender Item
            Bitmap timerExtenderBitmap = LoadBitmap("TimerExtender", "images/shopitems/timer-extender.png");
            _shopItems.Add(new TimerExtender("TimerExtender", 50, timerExtenderBitmap, panelX, panelY + 75, 30)); // x = 550 + 75 = 625
            // Mass Cleaner
            Bitmap massCleanerBitmap = LoadBitmap("Mass Cleaner", "images/shopitems/mass-cleaner.png");
            _shopItems.Add(new MassCleaner("MassCleaner", 30, massCleanerBitmap, panelX, panelY + 75 + 100 + 50)); // x = 725
        }

        /// <summary>
        /// Draws the shop items panel on the screen.
        /// It fills a rectangle with a specific color, draws thick borders around it, and then iterates through each shop item in the _shopItems list to draw them.
        /// </summary>
        public void DrawShopItems()
        {
            double panelY = (_windowHeight - 400) / 2;
            // Draw Shop Panel
            FillRectangle(ColorBurlyWood(), 1380, panelY, 100, 400);
            // Thick borders
            DrawRectangle(ColorBlack(), 1380, panelY, 100, 400);
            DrawRectangle(ColorBlack(), 1381, panelY + 1, 98, 398);
            DrawRectangle(ColorBlack(), 1382, panelY + 2, 96, 396);
            foreach (ShopItem item in _shopItems)
            {
                item.DrawItem();
            }
        }

        /// <summary>
        /// Draws the orders of all customers in the restaurant.
        /// It iterates through each customer in the _customers list and calls the DrawOrder method on their Order object if it exists.
        /// </summary>
        public void DrawOrders()
        {
            foreach (Customer customer in _customers)
            {
                if (customer.Order != null)
                {
                    customer.Order.DrawOrder();
                }
            }
        }

        /// <summary>
        /// Draws the game timer on the screen.
        /// It formats the remaining time from the _gameTimer and draws it in a rectangle with a thick border.
        /// </summary>
        public void DrawTimer()
        {
            string timerText = _gameTimer.GetFormattedRemainingTime();
            // Draw Timer
            FillRectangle(ColorWhite(), 1000, 10, 200, 50);
            // Thick borders
            DrawRectangle(ColorBlack(), 1000, 10, 200, 50);
            DrawRectangle(ColorBlack(), 1001, 11, 198, 48);
            DrawRectangle(ColorBlack(), 1002, 12, 196, 46);
            // (1000 - textWidth)/2 + 1000
            int textWidth = TextWidth(timerText, "fonts/VCR_OSD_MONO_1.001.ttf", 36);
            int textHeight = TextHeight(timerText, "fonts/VCR_OSD_MONO_1.001.ttf", 36);
            DrawText(timerText, ColorBlack(), "fonts/VCR_OSD_MONO_1.001.ttf", 36, 1000 + (200 - textWidth) / 2, 10 + (50 - textHeight) / 2);
        }

        /// <summary>
        /// Draws the money counter on the screen.
        /// It formats the money earned and displays it in a rectangle with a thick border.
        /// </summary>
        public void DrawMoney()
        {
            int textWidth = TextWidth($"RM{_moneyEarned.ToString("F2")}", "fonts/VCR_OSD_MONO_1.001.ttf", 36);
            int textHeight = TextHeight($"RM{_moneyEarned.ToString("F2")}", "fonts/VCR_OSD_MONO_1.001.ttf", 36);
            double boxWidth = textWidth + 40;
            double boxHeight = textHeight + 10;
            double boxX = (_windowWidth - boxWidth) / 2;
            double boxY = 10;
            // Draw money counter
            FillRectangle(ColorWhite(), boxX, boxY, boxWidth, boxHeight);
            // Thick borders
            DrawRectangle(ColorBlack(), boxX, boxY, boxWidth, boxHeight);
            DrawRectangle(ColorBlack(), boxX + 1, boxY + 1, boxWidth - 2, boxHeight - 2);
            DrawRectangle(ColorBlack(), boxX + 2, boxY + 2, boxWidth - 4, boxHeight - 4);
            DrawText($"RM{_moneyEarned.ToString("F2")}", ColorBlack(), "fonts/VCR_OSD_MONO_1.001.ttf", 36, boxX + (boxWidth - textWidth) / 2, boxY + (boxHeight - textHeight) / 2);

            if (_moneyDisplayFramesLeft > 0)
            {
                string tempText = $"+{_lastMoneyAdded.ToString("F2")}";
                int tempTextWidth = TextWidth(tempText, "fonts/VCR_OSD_MONO_1.001.ttf", 36);
                int tempTextHeight = TextHeight(tempText, "fonts/VCR_OSD_MONO_1.001.ttf", 36);
                double textX = boxX - 10 - tempTextWidth;
                double textY = boxY + (boxHeight - tempTextHeight) / 2;
                DrawText(tempText, RGBAColor(255, 255, 255, 200), "fonts/VCR_OSD_MONO_1.001.ttf", 36, textX, textY);
                if (!_isPaused)
                {
                    _moneyDisplayFramesLeft--;
                }
            }
        }

        /// <summary>
        /// Calculates the timer and money earned based on customer satisfaction.
        /// It iterates through each customer, checks if their full order has been received and satisfaction has not been calculated yet.
        /// </summary>
        public void CalculateTimerAndMoney()
        {
            decimal foodPrices = 0;
            foreach (Customer c in _customers)
            {
                if (c.FullOrderReceived == true && c.Satisfaction.HasCalculated == false)
                {
                    c.Satisfaction.CalculateAccuracy(c);
                    c.Satisfaction.CalculateSpeed(c);
                    c.Satisfaction.CalculateCleanliness(c);
                    foreach (Food f in c.Satisfaction.CorrectFoods)
                    {
                        foodPrices += f.FoodPrice;
                    }
                    c.Satisfaction.CalculateOverallSatisfaction();
                    decimal drinkPrice = 0;
                    if (c is VIPCustomer vip)
                    {
                        drinkPrice += vip.DroppedDrink.DrinkPrice;
                    }
                    decimal amountToAdd = (foodPrices + drinkPrice) * c.PayMultiplier * (1 + c.Satisfaction.OverallSatisfaction);
                    _moneyEarned += amountToAdd;
                    _lastMoneyAdded = amountToAdd;
                    _moneyDisplayFramesLeft = 60;
                    _gameTimer.AddOrRemoveTime(10 * (1 + (int)c.Satisfaction.OverallSatisfaction));
                    c.Satisfaction.HasCalculated = true;
                }
            }
        }

        /// <summary>
        /// Draws the current event if it is activated.
        /// It checks if the event is a HealthInspection or PowerOutage and applies the corresponding effects.
        /// </summary>
        public void DrawEvent()
        {
            if (_eventActivated && _event is HealthInspection currentInspection)
            {
                currentInspection.EventEffect(this);

                // reset event
                if (currentInspection.Activated == false)
                {
                    _eventActivated = false;
                    _event = null;
                    _eventFrameCounter = 0;
                }
                return;
            }

            if (_eventActivated && _event is PowerOutage currentOutage)
            {
                currentOutage.EventEffect(this);

                if (currentOutage.Activated == false)
                {
                    _eventActivated = false;
                    _event = null;
                    _eventFrameCounter = 0;
                }
                return;
            }

            _eventFrameCounter++;

            // 60 fps
            int framesPerSeconds = 60 * 10;

            if (_eventFrameCounter >= framesPerSeconds)
            {
                _eventFrameCounter = 0;
                double random = _random.Next(0, 100);

                if (random < 50) // 50%
                {
                    _event = new HealthInspection(4);
                    _eventActivated = true;
                }
                else if (random < 55)// next 5%
                {
                    _event = new PowerOutage(5);
                    _eventActivated = true;
                }
            }
        }

        /// <summary>
        /// Checks if the game is over by verifying if the game timer has run out.
        /// If the game is over, it clears all customers and their tables.
        /// </summary>
        /// <returns></returns>
        public bool GameOverCheck()
        {
            if (_gameTimer.GetRemainingTime().TotalSeconds <= 0)
            {
                foreach (Customer customer in _customers)
                {
                    if (customer.Table != null)
                    {
                        customer.Table.IsOccupied = false;
                        customer.Table.Customer = null;
                        customer.Table = null;
                    }
                }
                _customers.Clear();
                return true;
            }
            return false;
        }
    }
}