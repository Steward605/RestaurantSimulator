using System;
using SplashKitSDK;
using static SplashKitSDK.SplashKit;

namespace CustomProgram
{
    public class MenuAction : Action
    {
        #nullable disable
        private Customer _customer;
        public MenuAction(int actionID, string actionName, Bitmap objectBitmap, double objectDefaultX, double objectDefaultY) : base(actionID, actionName, objectBitmap, objectDefaultX, objectDefaultY)
        {
            _customer = null;
        }

        /// <summary>
        /// Gets or sets the customer the menu action dropped on.
        /// </summary>
        public Customer Customer
        {
            get { return _customer; }
            set { _customer = value; }
        }

        /// <summary>
        /// Performs the action of prompting the customer to order from the menu.
        /// </summary>
        public override void PerformAction()
        {
            if (_customer.WaitingToBePrompted == true)
            {
                _customer.Prompted = true;
            }
        }
    }
}