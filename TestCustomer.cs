using System;
using System.Collections.Generic;
using NUnit.Framework;
using SplashKitSDK;
using static SplashKitSDK.SplashKit;

namespace CustomProgram
{
    [TestFixture]
    public class TestCustomer
    {
        [Test]
        public void TestPlaceOrder_NormalCustomer()
        {
            // Setup
            Bitmap customerBitmap = LoadBitmap("customer", "images/customer/customer1.png");
            NormalCustomer normalCustomer = new NormalCustomer(CustomerType.Normal, 0, 0, customerBitmap, DateTime.Now, 90, 90, 90);
            normalCustomer.Random = new Random(1234);

            // Check
            Assert.AreEqual(normalCustomer.Order, null);
            Assert.AreEqual(normalCustomer.OrderCreated, false);
            Assert.AreEqual(normalCustomer.FoodSlotIndices, null);

            // Execute
            normalCustomer.PlaceOrder();

            // Check
            Assert.AreEqual(normalCustomer.OrderCreated, true);
            Assert.IsNotNull(normalCustomer.Order);
            Assert.AreEqual(2, normalCustomer.Order.FoodNames.Count);
            Assert.AreEqual(default(VIPDrink), normalCustomer.Order.Drink);
        }

        [Test]
        public void TestPlaceOrder_KarenCustomer()
        {
            // Setup
            Bitmap customerBitmap = LoadBitmap("customer", "images/customer/customer1.png");
            KarenCustomer karenCustomer = new KarenCustomer(CustomerType.Karen, 0, 0, customerBitmap, DateTime.Now, 90, 90, 90, 120);
            karenCustomer.Random = new Random(1234);

            // Check
            Assert.AreEqual(karenCustomer.Order, null);
            Assert.AreEqual(karenCustomer.OrderCreated, false);
            Assert.AreEqual(karenCustomer.FoodSlotIndices, null);

            // Execute
            karenCustomer.PlaceOrder();

            // Check
            Assert.AreEqual(karenCustomer.OrderCreated, true);
            Assert.IsNotNull(karenCustomer.Order);
            Assert.AreEqual(2, karenCustomer.Order.FoodNames.Count);
            Assert.AreEqual(default(VIPDrink), karenCustomer.Order.Drink);
        }

        [Test]
        public void TestPlaceOrder_VIPCustomer()
        {
            // Setup
            Bitmap customerBitmap = LoadBitmap("customer", "images/customer/customer1.png");
            VIPCustomer vipCustomer = new VIPCustomer(CustomerType.VIP, 0, 0, customerBitmap, DateTime.Now, 90, 90, 90);
            vipCustomer.Random = new Random(1234);

            // Check
            Assert.AreEqual(vipCustomer.Order, null);
            Assert.AreEqual(vipCustomer.OrderCreated, false);
            Assert.AreEqual(vipCustomer.FoodSlotIndices, null);

            // Execute
            vipCustomer.PlaceOrder();

            // Check
            Assert.AreEqual(vipCustomer.OrderCreated, true);
            Assert.IsNotNull(vipCustomer.Order);
            Assert.AreEqual(3, vipCustomer.Order.FoodNames.Count);
            Assert.IsTrue(Enum.IsDefined(typeof(VIPDrink), vipCustomer.Order.Drink));
            Assert.AreEqual(4, vipCustomer.Order.Bitmaps.Count);
        }
    }
}