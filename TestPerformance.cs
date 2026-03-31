using System;
using System.Diagnostics;
using SplashKitSDK;
using static SplashKitSDK.SplashKit;

namespace CustomProgram
{
    public class TestPerformance
    {
        public TestPerformance()
        { }

        public void TestExecutionTime()
        {
            NormalCustomer normalCustomer = new NormalCustomer(CustomerType.Normal, 0, 0, null, DateTime.Now, 1, 1, 1);
            VIPCustomer vipCustomer = new VIPCustomer(CustomerType.VIP, 0.0, 0.0, null, DateTime.Now, 1, 1, 1);
            KarenCustomer karenCustomer = new KarenCustomer(CustomerType.Karen, 0, 0, null, DateTime.Now, 1, 1, 1, 60);

            Stopwatch stopwatch = new Stopwatch();

            long normalCustomerTotalTicks = 0;
            long vipCustomerTotalTicks = 0;
            long karenCustomerTotalTicks = 0;

            // Simulate a performance test by creating a large number of customers
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"Test iteration no. {i + 1}");

                stopwatch.Start();
                normalCustomer.PlaceOrder();
                normalCustomer.OrderCreated = false; // Reset for the next customer
                stopwatch.Stop();
                normalCustomerTotalTicks += stopwatch.ElapsedTicks;
                Console.WriteLine($"Normal customer order created in {stopwatch.ElapsedTicks} ticks");
                stopwatch.Reset();

                stopwatch.Start();
                vipCustomer.PlaceOrder();
                vipCustomer.OrderCreated = false; // Reset for the next customer
                stopwatch.Stop();
                vipCustomerTotalTicks += stopwatch.ElapsedTicks;
                Console.WriteLine($"VIP customer order created in {stopwatch.ElapsedTicks} ticks");
                stopwatch.Reset();

                stopwatch.Start();
                karenCustomer.PlaceOrder();
                karenCustomer.OrderCreated = false; // Reset for the next customer
                stopwatch.Stop();
                karenCustomerTotalTicks += stopwatch.ElapsedTicks;
                Console.WriteLine($"Karen customer order created in {stopwatch.ElapsedTicks} ticks");
                stopwatch.Reset();

                Console.WriteLine("--------------------------------------------------");
            }

            // Display average execution time
            Console.WriteLine("Performance test completed.");
            Console.WriteLine("Average execution time for each customer type:");
            Console.WriteLine($"Normal Customer: {normalCustomerTotalTicks / 10} ticks");
            Console.WriteLine($"VIP Customer: {karenCustomerTotalTicks / 10} ticks");
            Console.WriteLine($"Karen Customer: {vipCustomerTotalTicks / 10} ticks");
        }
    }
}