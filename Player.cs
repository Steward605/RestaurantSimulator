using System;
using SplashKitSDK;
using static SplashKitSDK.SplashKit;
using System.Collections.Generic;

namespace CustomProgram
{
    public class Player
    {
        #nullable disable
        private Customer _currentCustomerDragging;
        private Food _currentFoodDragging;
        private Action _currentActionDragging;
        private Drink _currentDrinkDragging;
        private Restaurant _restaurant;

        public Player(Restaurant restaurant)
        {
            _currentCustomerDragging = null;
            _currentFoodDragging = null;
            _currentActionDragging = null;
            _currentDrinkDragging = null;
            _restaurant = restaurant;
        }

        public Restaurant Restaurant
        {
            get { return _restaurant; }
            set { _restaurant = value; }
        }

        public void CustomerObjectState()
        {
            Point2D mousePos = MousePosition();
            List<Customer> customersToRemove = new List<Customer>();

            // Tracks when Customer object starts to get dragged
            if (_currentCustomerDragging == null && MouseDown(MouseButton.LeftButton) && _currentActionDragging == null && _currentFoodDragging == null && _currentDrinkDragging == null)
            {
                // find the top‐most customer under the mouse and walk the list backwards
                for (int i = _restaurant.Customers.Count - 1; i >= 0; i--)
                {
                    Customer customer = _restaurant.Customers[i];
                    Rectangle hit = RectangleFrom(customer.ObjectDefaultX, customer.ObjectDefaultY, customer.ObjectBitmap.Width, customer.ObjectBitmap.Height);
                    if (PointInRectangle(mousePos, hit))
                    {
                        _currentCustomerDragging = customer;
                        customer.Dragging = true;
                        customer.OffsetX = mousePos.X - customer.ObjectDefaultX;
                        customer.OffsetY = mousePos.Y - customer.ObjectDefaultY;

                        // bring to front visually
                        _restaurant.Customers.RemoveAt(i);
                        _restaurant.Customers.Add(customer);
                        break;
                    }
                }
            }

            // Tracks when Customer object is dropped
            if (_currentCustomerDragging != null && !MouseDown(MouseButton.LeftButton))
            {
                // Remove customer if dropped on back door
                int textWidth = TextWidth("Back Door", "VCR_OSD_MONO_1.001.ttf", 36);
                int textHeight = TextHeight("Back Door", "VCR_OSD_MONO_1.001.ttf", 36);
                Rectangle backDoor = RectangleFrom(1300, 10, textWidth + 5, textHeight + 3);
                if (PointInRectangle(mousePos, backDoor))
                {
                    customersToRemove.Add(_currentCustomerDragging);
                }
                // Snaps the customer to the table if dropped over it
                foreach (Table table in _restaurant.Tables)
                {
                    Rectangle tableHit = RectangleFrom(table.ObjectDefaultX, table.ObjectDefaultY, table.TableBitmap.Width, table.TableBitmap.Height);
                    if (PointInRectangle(mousePos, tableHit) && !table.IsOccupied)
                    {
                        // compute top‐left of table for the customer:
                        double snapX = table.ObjectDefaultX + (table.TableBitmap.Width - _currentCustomerDragging.ObjectBitmap.Width) / 2;
                        double snapY = table.ObjectDefaultY - _currentCustomerDragging.ObjectBitmap.Height;

                        _currentCustomerDragging.ObjectDefaultX = snapX - 90;
                        _currentCustomerDragging.ObjectDefaultY = snapY;

                        table.Customer = _currentCustomerDragging;
                        table.IsOccupied = true;
                        _currentCustomerDragging.IsSeated = true;
                        _currentCustomerDragging.Table = table;
                        _currentCustomerDragging.TableCleanliness = table.Cleanliness;
                        _currentCustomerDragging.PatienceTimer.ResetTimer();
                        _currentCustomerDragging.WaitingPromptTimer.ResetTimer();
                        _currentCustomerDragging.OrderPromptTimer.ResetTimer();
                        _currentCustomerDragging.WaitingToBePrompted = true;
                        break;
                    }
                }
                _currentCustomerDragging.Dragging = false;
                _currentCustomerDragging = null;
            }

            // Tracks Customer object getting dragged
            if (_currentCustomerDragging != null && _currentCustomerDragging.Dragging)
            {
                _currentCustomerDragging.ObjectDefaultX = mousePos.X - _currentCustomerDragging.OffsetX;
                _currentCustomerDragging.ObjectDefaultY = mousePos.Y - _currentCustomerDragging.OffsetY;
                // Highlight tables when a customer is dragged over them
                foreach (Table table in _restaurant.Tables)
                {
                    Rectangle tableHitBox = RectangleFrom(table.ObjectDefaultX, table.ObjectDefaultY, table.TableBitmap.Width, table.TableBitmap.Height);
                    if (PointInRectangle(mousePos, tableHitBox) && !table.IsOccupied)
                    {
                        // hover effect
                        DrawRectangle(ColorGreen(), tableHitBox);
                    }
                }
            }

            foreach (Customer cust in _restaurant.Customers)
            {
                cust.DrawCustomer();

                // waiting to be seated
                if (!cust.IsSeated)
                {
                    TimeSpan remainingTime = cust.PatienceTimer.GetRemainingTime();
                    double remainingSeconds = remainingTime.TotalSeconds;
                    double patienceWindowWidth = 70;
                    double patienceWindowHeight = cust.ObjectBitmap.Height / 3;
                    double patienceWindowX = cust.ObjectDefaultX + cust.ObjectBitmap.Width + 5;
                    double patienceWindowY = cust.ObjectDefaultY + patienceWindowHeight;

                    double fractionRemaining = remainingSeconds / cust.PatienceTimer.InitialSeconds;
                    double currentFilledWidth = patienceWindowWidth * fractionRemaining;

                    // background
                    FillRectangle(ColorBlack(), patienceWindowX, patienceWindowY, patienceWindowWidth, patienceWindowHeight);
                    // remaining time draw
                    FillRectangle(ColorGreen(), patienceWindowX, patienceWindowY, currentFilledWidth, patienceWindowHeight);
                    // border
                    DrawRectangle(ColorBlack(), patienceWindowX, patienceWindowY, patienceWindowWidth, patienceWindowHeight);
                }

                // waiting to be prompted for order
                if (cust.IsSeated && !cust.Prompted)
                {
                    cust.WaitingToBePrompted = true;
                    TimeSpan remainingTime = cust.WaitingPromptTimer.GetRemainingTime();
                    double remainingSeconds = remainingTime.TotalSeconds;
                    double patienceWindowWidth = 70;
                    double patienceWindowHeight = cust.ObjectBitmap.Height / 3;
                    double patienceWindowX = cust.ObjectDefaultX + cust.ObjectBitmap.Width + 5;
                    double patienceWindowY = cust.ObjectDefaultY + patienceWindowHeight;

                    double fractionRemaining = remainingSeconds / cust.WaitingPromptTimer.InitialSeconds;
                    double currentFilledWidth = patienceWindowWidth * fractionRemaining;

                    // background
                    FillRectangle(ColorBlack(), patienceWindowX, patienceWindowY, patienceWindowWidth, patienceWindowHeight);
                    // remaining time draw
                    FillRectangle(ColorYellow(), patienceWindowX, patienceWindowY, currentFilledWidth, patienceWindowHeight);
                    // border
                    DrawRectangle(ColorBlack(), patienceWindowX, patienceWindowY, patienceWindowWidth, patienceWindowHeight);
                }

                // Order prompted. Waiting for order to be fulfilled
                if (cust.Prompted == true && cust.WaitingToBePrompted == true)
                {
                    if (cust.OrderCreated == false)
                    {
                        cust.PlaceOrder();
                    }
                    else if (cust is KarenCustomer karenCustomer)
                    {
                        // only after the initial order exists do we let Karen “re‐consider” it
                        karenCustomer.PlaceOrder();
                    }
                    TimeSpan remainingTime = cust.OrderPromptTimer.GetRemainingTime();
                    double remainingSeconds = remainingTime.TotalSeconds;
                    double patienceWindowWidth = cust.Order.TotalWidth; // +10 for border
                    double patienceWindowHeight = cust.ObjectBitmap.Height / 5; // = 15
                    double patienceWindowX = cust.ObjectDefaultX + cust.ObjectBitmap.Width;
                    double patienceWindowY = cust.Order.ObjectDefaultY + 75;

                    double fractionRemaining = remainingSeconds / cust.OrderPromptTimer.InitialSeconds;
                    double currentFilledWidth = patienceWindowWidth * fractionRemaining;
                    // background
                    FillRectangle(ColorBlack(), patienceWindowX, patienceWindowY, patienceWindowWidth, patienceWindowHeight);
                    // remaining time draw
                    FillRectangle(ColorRed(), patienceWindowX, patienceWindowY, currentFilledWidth, patienceWindowHeight);
                    // border
                    DrawRectangle(ColorBlack(), patienceWindowX, patienceWindowY, patienceWindowWidth, patienceWindowHeight);
                }
            }

            // Tracks timers and order status
            foreach (Customer cust in _restaurant.Customers)
            {
                if (cust is VIPCustomer vipCust && vipCust.ShouldLeave)
                {
                    customersToRemove.Add(cust);
                    continue;
                }
                
                if (!cust.IsSeated)
                {
                    if (cust.PatienceTimer.HasEnded())
                        customersToRemove.Add(cust);
                }
                else if (!cust.Prompted)
                {
                    if (cust.WaitingPromptTimer.HasEnded())
                        customersToRemove.Add(cust);
                }
                else if (cust.OrderCreated)
                {
                    if (cust.OrderPromptTimer.HasEnded())
                        customersToRemove.Add(cust);
                }

                if (cust.CheckOrderFulfilled())
                {
                    cust.Table.StainDegree++;
                    customersToRemove.Add(cust);
                }
            }

            if (customersToRemove.Count > 0)
            {
                _restaurant.CalculateTimerAndMoney();
            }

            foreach (Customer cust in customersToRemove)
            {
                _restaurant.RemoveCustomer(cust);
            }
        }

        public Food FoodObjectState()
        {
            Point2D mousePos = MousePosition();
            Food servedFood = null;

            // start dragging
            if (_currentFoodDragging == null && MouseDown(MouseButton.LeftButton) && _currentDrinkDragging == null && _currentCustomerDragging == null && _currentActionDragging == null)
            {
                int previewFoodIndex = _restaurant.Kitchen.Foods.Count - 1;
                for (int i = previewFoodIndex - 1; i >= 0; i--)
                {
                    Food food = _restaurant.Kitchen.Foods[i];
                    Rectangle hit = RectangleFrom(food.ObjectDefaultX, food.ObjectDefaultY, food.ObjectBitmap.Width, food.ObjectBitmap.Height);
                    if (PointInRectangle(mousePos, hit))
                    {
                        _currentFoodDragging = food;
                        food.Dragging = true;
                        food.OffsetX = mousePos.X - food.ObjectDefaultX;
                        food.OffsetY = mousePos.Y - food.ObjectDefaultY;
                        break;
                    }
                }
            }

            // Highlight tables when a food is dragged over them
            if (_currentFoodDragging != null && _currentFoodDragging.Dragging)
            {
                _currentFoodDragging.ObjectDefaultX = mousePos.X - _currentFoodDragging.OffsetX;
                _currentFoodDragging.ObjectDefaultY = mousePos.Y - _currentFoodDragging.OffsetY;
                foreach (Table table in _restaurant.Tables)
                {
                    Rectangle tableHitBox = RectangleFrom(table.ObjectDefaultX, table.ObjectDefaultY, table.TableBitmap.Width, table.TableBitmap.Height);
                    if (PointInRectangle(mousePos, tableHitBox))
                    {
                        // hover effect
                        DrawRectangle(ColorGreen(), tableHitBox);
                    }
                }
            }

            if (_currentFoodDragging != null && !MouseDown(MouseButton.LeftButton))
            {
                // Check if the food is moved over the rubbish bin
                Rectangle rubbishBin = RectangleFrom(200, 680, 100, 100);
                if (PointInRectangle(mousePos, rubbishBin))
                {
                    // Remove food from the kitchen
                    servedFood = _currentFoodDragging;
                }
                else
                {
                    // Check if the food is moved over a table
                    foreach (Table table in _restaurant.Tables)
                    {
                        Rectangle tableHit = RectangleFrom(table.ObjectDefaultX, table.ObjectDefaultY, table.TableBitmap.Width, table.TableBitmap.Height);
                        if (PointInRectangle(mousePos, tableHit) && table.IsOccupied)
                        {
                            // Snap food to the center of the occupied table
                            double snapX = table.ObjectDefaultX + (table.TableBitmap.Width - _currentFoodDragging.ObjectBitmap.Width) / 2;
                            double snapY = table.ObjectDefaultY + (table.TableBitmap.Height - _currentFoodDragging.ObjectBitmap.Height) / 2;

                            _currentFoodDragging.ObjectDefaultX = snapX;
                            _currentFoodDragging.ObjectDefaultY = snapY;
                            // Add food to customer's order
                            if (table.Customer != null && table.IsOccupied)
                            {
                                table.Customer.TotalReceivedFoods.Add(_currentFoodDragging);
                                // table.Customer.CheckReceivedFood(_currentFoodDragging);
                                bool correctFood = table.Customer.CheckReceivedFood(_currentFoodDragging);
                                if (!correctFood && table.Customer is VIPCustomer vipCustomer)
                                {
                                    vipCustomer.CheckIfLeave();
                                }
                            }
                            servedFood = _currentFoodDragging;
                            break;
                        }
                    }
                }
                _currentFoodDragging.Dragging = false;
                _currentFoodDragging = null;
            }
            return servedFood;
        }

        public Drink DrinkObjectState()
        {
            Point2D mousePos = MousePosition();
            Drink servedDrink = null;

            // start dragging drink
            if (_currentDrinkDragging == null && MouseDown(MouseButton.LeftButton) && _currentFoodDragging == null && _currentCustomerDragging == null && _currentActionDragging == null)
            {
                Drink drink = _restaurant.Kitchen.Drink;
                if (drink != null)
                {
                    Rectangle hit = RectangleFrom(drink.ObjectDefaultX, drink.ObjectDefaultY, drink.ObjectBitmap.Width, drink.ObjectBitmap.Height);
                    if (PointInRectangle(mousePos, hit))
                    {
                        _currentDrinkDragging = drink;
                        drink.Dragging = true;
                        drink.OffsetX = mousePos.X - drink.ObjectDefaultX;
                        drink.OffsetY = mousePos.Y - drink.ObjectDefaultY;
                    }
                }
            }

            // dragging drink
            if (_currentDrinkDragging != null && _currentDrinkDragging.Dragging)
            {
                _currentDrinkDragging.ObjectDefaultX = mousePos.X - _currentDrinkDragging.OffsetX;
                _currentDrinkDragging.ObjectDefaultY = mousePos.Y - _currentDrinkDragging.OffsetY;
                foreach (Table table in _restaurant.Tables)
                {
                    // highlight table when dragged over
                    Rectangle tableHitBox = RectangleFrom(table.ObjectDefaultX, table.ObjectDefaultY, table.TableBitmap.Width, table.TableBitmap.Height);
                    if (PointInRectangle(mousePos, tableHitBox) && table.IsOccupied && table.Customer is VIPCustomer)
                    {
                        // hover effect
                        DrawRectangle(ColorGreen(), tableHitBox);
                    }
                }
            }

            // drop drink
            if (_currentDrinkDragging != null && !MouseDown(MouseButton.LeftButton))
            {
                // remove drink
                Rectangle rubbishBin = RectangleFrom(200, 680, 100, 100);
                if (PointInRectangle(mousePos, rubbishBin))
                {
                    servedDrink = _currentDrinkDragging;
                }
                // snap to table center and give drink
                else
                {
                    foreach (Table table in _restaurant.Tables)
                    {
                        Rectangle tableHit = RectangleFrom(table.ObjectDefaultX, table.ObjectDefaultY, table.TableBitmap.Width, table.TableBitmap.Height);
                        if (PointInRectangle(mousePos, tableHit) && table.IsOccupied && table.Customer is VIPCustomer vipCust)
                        {
                            double snapX = table.ObjectDefaultX + (table.TableBitmap.Width - _currentDrinkDragging.ObjectBitmap.Width) / 2;
                            double snapY = table.ObjectDefaultY + (table.TableBitmap.Height - _currentDrinkDragging.ObjectBitmap.Height) / 2;

                            _currentDrinkDragging.ObjectDefaultX = snapX;
                            _currentDrinkDragging.ObjectDefaultY = snapY;

                            if (table.Customer != null && table.IsOccupied)
                            {
                                bool correctDrink = vipCust.CheckReceivedDrink(_currentDrinkDragging);
                                if (!correctDrink)
                                {
                                    vipCust.CheckIfLeave();
                                }
                            }
                            servedDrink = _currentDrinkDragging;
                            break;
                        }
                    }
                }
                _currentDrinkDragging.Dragging = false;
                _currentDrinkDragging = null;
            }
            return servedDrink;
        }

        public void ActionObjectState()
        {
            Point2D mousePos = MousePosition();

            // find action under the mouse
            if (_currentActionDragging == null && MouseDown(MouseButton.LeftButton) && _currentCustomerDragging == null && _currentFoodDragging == null && _currentDrinkDragging == null)
            {
                for (int i = _restaurant.Actions.Count - 1; i >= 0; i--)
                {
                    Action action = _restaurant.Actions[i];
                    Rectangle hit = RectangleFrom(action.ObjectDefaultX, action.ObjectDefaultY, action.ObjectBitmap.Width, action.ObjectBitmap.Height);
                    if (PointInRectangle(mousePos, hit))
                    {
                        _currentActionDragging = action;
                        action.Dragging = true;
                        action.OffsetX = mousePos.X - action.ObjectDefaultX;
                        action.OffsetY = mousePos.Y - action.ObjectDefaultY;

                        _restaurant.Actions.RemoveAt(i);
                        _restaurant.Actions.Add(action);
                        break;
                    }
                }
            }

            if (_currentActionDragging != null && _currentActionDragging.Dragging)
            {
                _currentActionDragging.ObjectDefaultX = mousePos.X - _currentActionDragging.OffsetX;
                _currentActionDragging.ObjectDefaultY = mousePos.Y - _currentActionDragging.OffsetY;
            }

            // Highlight tables when a customer is dragged over them
            if (_currentActionDragging != null && _currentActionDragging.Dragging)
            {
                if (_currentActionDragging is CleanAction)
                {
                    foreach (Table table in _restaurant.Tables)
                    {
                        Rectangle tableHitBox = RectangleFrom(table.ObjectDefaultX, table.ObjectDefaultY, table.TableBitmap.Width, table.TableBitmap.Height);
                        if (PointInRectangle(mousePos, tableHitBox) && !table.IsOccupied)
                        {
                            // hover effect
                            DrawRectangle(ColorGreen(), tableHitBox);
                        }
                    }
                }
            }

            if (_currentActionDragging != null && !MouseDown(MouseButton.LeftButton))
            {
                bool validDrop = false;

                // check if customer is below menu action object
                if (_currentActionDragging is MenuAction menuAction)
                {
                    foreach (Customer c in _restaurant.Customers)
                    {
                        Rectangle customerHitbox = RectangleFrom(c.ObjectDefaultX, c.ObjectDefaultY, c.ObjectBitmap.Width, c.ObjectBitmap.Height);
                        if (PointInRectangle(mousePos, customerHitbox))
                        {
                            menuAction.Customer = c;
                            menuAction.PerformAction();
                            validDrop = true;
                            _currentActionDragging.ObjectDefaultX = _currentActionDragging.DragStartPositionX;
                            _currentActionDragging.ObjectDefaultY = _currentActionDragging.DragStartPositionY;
                            break;
                        }
                    }
                }
                // Check if table is below clean action object
                else if (_currentActionDragging is CleanAction cleanAction)
                {
                    foreach (Table table in _restaurant.Tables)
                    {
                        Rectangle tableHitBox = RectangleFrom(table.ObjectDefaultX, table.ObjectDefaultY, table.TableBitmap.Width, table.TableBitmap.Height);
                        if (PointInRectangle(mousePos, tableHitBox))
                        {
                            cleanAction.Table = table;
                            cleanAction.PerformAction();
                            validDrop = true;
                            _currentActionDragging.ObjectDefaultX = _currentActionDragging.DragStartPositionX;
                            _currentActionDragging.ObjectDefaultY = _currentActionDragging.DragStartPositionY;
                            break;
                        }
                    }
                }

                if (!validDrop)
                {
                    _currentActionDragging.ObjectDefaultX = _currentActionDragging.DragStartPositionX;
                    _currentActionDragging.ObjectDefaultY = _currentActionDragging.DragStartPositionY;
                }

                _currentActionDragging.Dragging = false;
                _currentActionDragging = null;
            }
        }

        public void ShopItemState()
        {
            Point2D mousePos = MousePosition();
            if (MouseClicked(MouseButton.LeftButton))
            {
                foreach (ShopItem item in _restaurant.ShopItems)
                {
                    Rectangle hitbox = RectangleFrom(item.ObjectDefaultX, item.ObjectDefaultY, item.ObjectBitmap.Width, item.ObjectBitmap.Height);
                    if (PointInRectangle(mousePos, hitbox))
                    {
                        item.ToggleItem();
                        item.UseItem(_restaurant);
                        item.ToggleItem();
                    }
                }
            }
        }
    }
}