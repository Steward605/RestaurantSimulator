using System;
using SplashKitSDK;
using static SplashKitSDK.SplashKit;

namespace CustomProgram
{
    public class Program
    {
        private static void GameTitle(Window window)
        {
            DrawText("Restaurant Simulator", ColorBlack(), "VCR_OSD_MONO_1.001.ttf", 72, /*window.Width*/CurrentWindowWidth() / 2 - TextWidth("Restaurant Simulator", "VCR_OSD_MONO_1.001.ttf", 72) / 2, /*window.Height*/CurrentWindowHeight() / 2 - TextHeight("Restaurant Simulator", "VCR_OSD_MONO_1.001.ttf", 72) / 2 - 150);
        }
        private static bool StartButton(Window window)
        {
            int textWidth = TextWidth("Start", "VCR_OSD_MONO_1.001.ttf", 48);
            int textHeight = TextHeight("Start", "VCR_OSD_MONO_1.001.ttf", 48);
            int padding = 10; // space around the text
            int borderPadding = 2; // how much bigger the border is

            int innerWidth = textWidth + padding;
            int innerHeight = textHeight + padding;
            int innerX = /*window.Width*/CurrentWindowWidth() / 2 - innerWidth / 2;
            int innerY = /*window.Height*/CurrentWindowHeight() / 2 - innerHeight / 2 - 50;

            Rectangle innerRect = new Rectangle
            {
                X = innerX,
                Y = innerY,
                Width = innerWidth,
                Height = innerHeight
            };
            Rectangle borderRect = new Rectangle
            {
                X = innerX - borderPadding,
                Y = innerY - borderPadding,
                Width = innerWidth + borderPadding * 2,
                Height = innerHeight + borderPadding * 2
            };

            FillRectangle(ColorGreen(), innerRect);
            DrawRectangle(ColorBlack(), borderRect.X, borderRect.Y, borderRect.Width, borderRect.Height);
            DrawText("Start", ColorWhite(), "VCR_OSD_MONO_1.001.ttf", 48, innerX + padding / 2, innerY + padding / 2);
            if (MouseClicked(MouseButton.LeftButton))
            {
                Point2D mousePosition = MousePosition();
                if (mousePosition.X >= innerRect.X && mousePosition.X <= innerRect.X + innerRect.Width && mousePosition.Y >= innerRect.Y && mousePosition.Y <= innerRect.Y + innerRect.Height)
                {
                    return true; // Start the game
                }
            }
            return false;
        }
        private static bool QuitButton(Window window)
        {
            int textWidth = TextWidth("Quit", "VCR_OSD_MONO_1.001.ttf", 48);
            int textHeight = TextHeight("Quit", "VCR_OSD_MONO_1.001.ttf", 48);
            int padding = 10; // space around the text
            int borderPadding = 2; // how much bigger the border is

            int innerWidth = textWidth + padding;
            int innerHeight = textHeight + padding;
            int innerX = /*window.Width*/CurrentWindowWidth() / 2 - innerWidth / 2;
            int innerY = /*window.Height*/CurrentWindowHeight() / 2 - innerHeight / 2 + 40;

            Rectangle innerRect = new Rectangle
            {
                X = innerX,
                Y = innerY,
                Width = innerWidth,
                Height = innerHeight
            };
            Rectangle borderRect = new Rectangle
            {
                X = innerX - borderPadding,
                Y = innerY - borderPadding,
                Width = innerWidth + borderPadding * 2,
                Height = innerHeight + borderPadding * 2
            };

            FillRectangle(ColorRed(), innerRect);
            DrawRectangle(ColorBlack(), borderRect.X, borderRect.Y, borderRect.Width, borderRect.Height);
            DrawText("Quit", ColorWhite(), "VCR_OSD_MONO_1.001.ttf", 48, innerX + padding / 2, innerY + padding / 2);

            if (MouseClicked(MouseButton.LeftButton))
            {
                Point2D mousePosition = MousePosition();
                if (mousePosition.X >= innerRect.X && mousePosition.X <= innerRect.X + innerRect.Width && mousePosition.Y >= innerRect.Y && mousePosition.Y <= innerRect.Y + innerRect.Height)
                {
                    window.Close();
                    Console.WriteLine("Quit button clicked");
                    return true; // Quit the game
                }
            }
            return false;
        }
        private static void PauseMenu(Window window)
        {
            Color tint = RGBAColor(128, 128, 128, 128);
            FillRectangle(
                tint,
                0, 0,
                CurrentWindowWidth(), CurrentWindowHeight()
            );
            Rectangle pauseMenuRect = new Rectangle
            {
                X = /*window.Width*/CurrentWindowWidth() / 2 - 200,
                Y = /*window.Height*/CurrentWindowHeight() / 2 - 100,
                Width = 400,
                Height = 200
            };
            Rectangle borderRect = new Rectangle
            {
                X = pauseMenuRect.X - 2,
                Y = pauseMenuRect.Y - 2,
                Width = pauseMenuRect.Width + 4,
                Height = pauseMenuRect.Height + 4
            };
            FillRectangle(ColorCadetBlue(), pauseMenuRect);
            DrawRectangle(ColorBlack(), borderRect.X, borderRect.Y, borderRect.Width, borderRect.Height);
            DrawText("Game Paused", ColorBlack(), "VCR_OSD_MONO_1.001.ttf", 48, window.Width / 2 - TextWidth("Game Paused", "VCR_OSD_MONO_1.001.ttf", 48) / 2, window.Height / 2 - TextHeight("Game Paused", "VCR_OSD_MONO_1.001.ttf", 48) / 2 - 50);
        }
        private static void GameOverMenu(Window window)
        {
            Color tint = RGBAColor(128, 128, 128, 128);
            FillRectangle(
                tint,
                0, 0,
                CurrentWindowWidth(), CurrentWindowHeight()
            );
            Rectangle gameOverRect = new Rectangle
            {
                X = /*window.Width*/CurrentWindowWidth() / 2 - 200,
                Y = /*window.Height*/CurrentWindowHeight() / 2 - 100,
                Width = 400,
                Height = 200
            };
            Rectangle borderRect = new Rectangle
            {
                X = gameOverRect.X - 2,
                Y = gameOverRect.Y - 2,
                Width = gameOverRect.Width + 4,
                Height = gameOverRect.Height + 4
            };
            FillRectangle(ColorCadetBlue(), gameOverRect);
            DrawRectangle(ColorBlack(), borderRect.X, borderRect.Y, borderRect.Width, borderRect.Height);
            DrawText("Game Over", ColorBlack(), "VCR_OSD_MONO_1.001.ttf", 48, window.Width / 2 - TextWidth("Game Over", "VCR_OSD_MONO_1.001.ttf", 48) / 2, window.Height / 2 - TextHeight("Game Over", "VCR_OSD_MONO_1.001.ttf", 48) / 2 - 50);
        }
        public static void Main()
        {
            bool gameOver = false;
            Random random = new Random();
            Window mainWindow = OpenWindow("Main Window", 1500, 800);
            //CurrentWindowToggleFullscreen();
            bool launchGame = false;
            // Main loop
            while (!mainWindow.CloseRequested && !launchGame)
            {
                ProcessEvents();
                mainWindow.Clear(ColorWhite());

                GameTitle(mainWindow);
                if (StartButton(mainWindow) == true)
                {
                    launchGame = true;
                }
                if (QuitButton(mainWindow))
                {
                    break;
                }

                mainWindow.Refresh(60);
                // Delay(16);
            }
            if (launchGame)
            {
                CloseWindow(mainWindow);
                Kitchen kitchen = new Kitchen();
                Timer timer = new Timer(120); // 1 minutes timer
                Restaurant restaurant = new Restaurant(1500, 800, kitchen, timer);
                Player player = new Player(restaurant);
                Window gameWindow = OpenWindow("Game Window", 1500, 800);
                bool paused = false;
                Bitmap floorBitmap = LoadBitmap("FloorImage", "images/floor.png");
                player.Restaurant.InitializeTables();
                player.Restaurant.InitializeActions();
                player.Restaurant.InitializeShopItems();
                while (!gameWindow.CloseRequested)
                {
                    ProcessEvents();
                    gameWindow.Clear(ColorWhite());

                    // Draw Floor
                    DrawBitmap(floorBitmap, 0, 0);
                    // Draw Rubbish Bin
                    player.Restaurant.Kitchen.DrawRubbishBin();
                    // Draw Doors
                    player.Restaurant.DrawDoors();
                    // Draw Money Counter
                    player.Restaurant.DrawMoney();
                    // Draw Game Timer
                    player.Restaurant.DrawTimer();
                    // Draw Shop Items
                    player.Restaurant.DrawShopItems();
                    // Draw Tables
                    player.Restaurant.DrawTables();
                    // Draw Kitchen Counter
                    player.Restaurant.Kitchen.DrawKitchenCounter();
                    // Draw Orders
                    player.Restaurant.DrawOrders();
                    // Draw Usable Action Icons
                    player.Restaurant.DrawActions();
                    // Draw food
                    Food servedFood = player.FoodObjectState();
                    player.Restaurant.Kitchen.ManageFood(servedFood);

                    // Pause the game
                    if (KeyReleased(KeyCode.EscapeKey))
                    {
                        paused = !paused;
                        player.Restaurant.IsPaused = paused;
                    }

                    // Game logic
                    if (!paused)
                    {
                        player.Restaurant.GameTimer.ResumeTimer();
                        Drink servedDrink = player.DrinkObjectState();
                        player.Restaurant.Kitchen.ManageDrink(servedDrink);
                        foreach (Customer c in player.Restaurant.Customers)
                        {
                            c.PatienceTimer.ResumeTimer();
                            c.WaitingPromptTimer.ResumeTimer();
                            c.OrderPromptTimer.ResumeTimer();
                        }
                        if (random.Next(100) < 1) // 10% chance to spawn a customer
                        {
                            player.Restaurant.SpawnCustomer();
                        }
                        player.CustomerObjectState();
                        player.ActionObjectState();
                        player.ShopItemState();
                        player.Restaurant.DrawEvent();
                    }
                    else
                    {
                        PauseMenu(gameWindow);
                        player.Restaurant.GameTimer.PauseTimer();
                        foreach (Customer c in player.Restaurant.Customers)
                        {
                            c.PatienceTimer.PauseTimer();
                            c.WaitingPromptTimer.PauseTimer();
                            c.OrderPromptTimer.PauseTimer();
                        }
                        if (QuitButton(gameWindow))
                        {
                            break;
                        }
                    }

                    if (player.Restaurant.GameOverCheck())
                    {
                        gameOver = true;
                        break;
                    }
                    gameWindow.Refresh(60);
                    // Delay(16);
                }
                CloseWindow(gameWindow);
            }
            if (gameOver)
            {
                Window gameOverWindow = OpenWindow("Game Over", 1500, 800);
                while (!gameOverWindow.CloseRequested)
                {
                    ProcessEvents();
                    gameOverWindow.Clear(ColorWhite());
                    GameOverMenu(gameOverWindow);
                    if (QuitButton(gameOverWindow))
                    {
                        break;
                    }
                    gameOverWindow.Refresh(60);
                }
                CloseWindow(gameOverWindow);
            }
            CloseAllWindows();
            // test execution time
            TestPerformance testPerformance = new TestPerformance();
            testPerformance.TestExecutionTime();
        }
    }
}
