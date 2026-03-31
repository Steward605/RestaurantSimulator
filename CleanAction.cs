using System;
using SplashKitSDK;
using static SplashKitSDK.SplashKit;

namespace CustomProgram
{
    public class CleanAction : Action
    {
        #nullable disable
        private Table _table;

        public CleanAction(int actionID, string actionName, Bitmap objectBitmap, double objectDefaultX, double objectDefaultY) : base(actionID, actionName, objectBitmap, objectDefaultX, objectDefaultY)
        {
            _table = null;
        }

        /// <summary>
        /// Gets or sets the table associated with this clean action.
        /// This property allows the action to be linked to a specific table that needs cleaning.
        /// </summary>
        public Table Table
        {
            get { return _table; }
            set { _table = value; }
        }

        /// <summary>
        /// Performs the clean action on the associated table.
        /// This method sets the stain degree of the table to 0, effectively cleaning it.
        /// </summary>
        public override void PerformAction()
        {
            _table.StainDegree = 0;
        }
    }
}