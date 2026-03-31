using System;
using SplashKitSDK;
using static SplashKitSDK.SplashKit;

namespace CustomProgram
{
    public abstract class Action
    {
        private int _actionID;
        private string _actionName;
        private Bitmap _objectBitmap;
        private double _objectDefaultX;
        private double _objectDefaultY;
        private bool _dragging;
        private double _offsetX;
        private double _offsetY;
        private double _dragStartPositionX;
        private double _dragStartPositionY;

        public Action(int actionID, string actionName, Bitmap objectBitmap, double objectDefaultX, double objectDefaultY)
        {
            _actionID = actionID;
            _actionName = actionName;
            _objectBitmap = objectBitmap;
            _objectDefaultX = objectDefaultX;
            _objectDefaultY = objectDefaultY;
            _dragStartPositionX = _objectDefaultX;
            _dragStartPositionY = _objectDefaultY;
            _dragging = false;
            _offsetX = 0;
            _offsetY = 0;
        }

        /// <summary>
        /// Gets the unique ID of the action.
        /// </summary>
        public int ActionID
        {
            get { return _actionID; }
        }

        /// <summary>
        /// Gets the action's name.
        /// </summary>
        public string ActionName
        {
            get { return _actionName; }
        }

        /// <summary>
        /// Gets or sets the action's bitmap.
        /// This bitmap represents the visual icon of the action in the game.
        /// </summary>
        public Bitmap ObjectBitmap
        {
            get { return _objectBitmap; }
            set { _objectBitmap = value; }
        }

        /// <summary>
        /// Gets or sets the action's bitmap's default X position.
        /// </summary>
        public double ObjectDefaultX
        {
            get { return _objectDefaultX; }
            set { _objectDefaultX = value; }
        }

        /// <summary>
        /// Gets or sets the action's bitmap's default Y position.
        /// </summary>
        public double ObjectDefaultY
        {
            get { return _objectDefaultY; }
            set { _objectDefaultY = value; }
        }

        /// <summary>
        /// Gets or sets the action's dragging state.
        /// This property indicates whether the action is currently being dragged by the player.
        /// </summary>
        public bool Dragging
        {
            get { return _dragging; }
            set { _dragging = value; }
        }

        /// <summary>
        /// Gets or sets the offset X position of the action.
        /// This offset is used to adjust the position of the action when it is being dragged.
        /// </summary>
        public double OffsetX
        {
            get { return _offsetX; }
            set { _offsetX = value; }
        }

        /// <summary>
        /// Gets or sets the offset Y position of the action.
        /// This offset is used to adjust the position of the action when it is being dragged.
        /// </summary>
        public double OffsetY
        {
            get { return _offsetY; }
            set { _offsetY = value; }
        }

        /// <summary>
        /// Gets or sets the starting position X of the drag.
        /// This property is used to store the initial X position of the action when dragging starts.
        /// </summary>
        public double DragStartPositionX
        {
            get { return _dragStartPositionX; }
            set { _dragStartPositionX = value; }
        }

        /// <summary>
        /// Gets or sets the starting position Y of the drag.
        /// This property is used to store the initial Y position of the action when dragging starts.
        /// </summary>
        public double DragStartPositionY
        {
            get { return _dragStartPositionY; }
            set { _dragStartPositionY = value; }
        }

        /// <summary>
        /// Draws the action's bitmap at its default position.
        /// </summary>
        public void DrawAction()
        {
            DrawBitmap(_objectBitmap, ObjectDefaultX, ObjectDefaultY);
        }

        /// <summary>
        /// Performs the action associated with this Action class.
        /// This method is overridden in MenuAction and CleanAction to implement specific actions.
        /// </summary>
        public abstract void PerformAction();
    }
}