using System;
using System.Collections.Generic;

namespace CustomProgram
{
    public class Satisfaction
    {
        private int _satisfactionID;
        private static int _incrementSatisfactionID = 0;
        private List<Food> _correctFoods;
        private decimal _accuracy;
        private decimal _speed;
        private decimal _cleanliness;
        private decimal _overallSatisfaction;
        private bool _hasCalculated;

        public Satisfaction()
        {
            _satisfactionID = _incrementSatisfactionID++;
            _correctFoods = new List<Food>();
            _accuracy = 0;
            _speed = 0;
            _cleanliness = 0;
            _overallSatisfaction = 0;
            _hasCalculated = false;
        }

        /// <summary>
        /// Gets or sets the satisfaction ID.
        /// </summary>
        public int SatisfactionID
        {
            get { return _satisfactionID; }
            set { _satisfactionID = value; }
        }

        /// <summary>
        /// Gets or sets the list of correct foods.
        /// </summary>
        public List<Food> CorrectFoods
        {
            get { return _correctFoods; }
            set { _correctFoods = value; }
        }

        /// <summary>
        /// Gets or sets the overall satisfaction.
        /// </summary>
        public decimal OverallSatisfaction
        {
            get { return _overallSatisfaction; }
            set { _overallSatisfaction = value; }
        }

        /// <summary>
        /// Gets or sets the boolean indicating if the satisfaction has been calculated.
        /// </summary>
        public bool HasCalculated
        {
            get { return _hasCalculated; }
            set { _hasCalculated = value; }
        }

        /// <summary>
        /// Calculates the accuracy of the customer's order.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public decimal CalculateAccuracy(Customer customer)
        {
            decimal correctFoodCount = 0m;
            List<FoodName> _pending = new List<FoodName>(customer.Order.FoodNames); // copy to pending
            foreach (Food receivedFood in customer.TotalReceivedFoods)
            {
                if (_pending.Remove(receivedFood.FoodName))
                {
                    correctFoodCount++;
                    _correctFoods.Add(receivedFood);
                }
            }
            _accuracy = correctFoodCount / customer.TotalReceivedFoods.Count;
            return _accuracy;
        }

        /// <summary>
        /// Calculates the speed of the order fulfillment.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public decimal CalculateSpeed(Customer customer)
        {
            TimeSpan expected = customer.ExpectedReceiveTime - customer.OrderStartTime;
            TimeSpan actual = customer.OrderFulfilledTime - customer.OrderStartTime;
            TimeSpan speed = expected - actual;
            decimal timeRemainingFraction = (decimal)speed.TotalSeconds / (decimal)expected.TotalSeconds; // ratio of remaining time vs total expected time
            if (timeRemainingFraction >= (decimal)2.0 / (decimal)3.0)
            {
                _speed = timeRemainingFraction * (decimal)1.3;
                return _speed;
            }
            else if (timeRemainingFraction >= (decimal)1.0 / (decimal)3.0 && timeRemainingFraction < (decimal)2.0 / (decimal)3.0)
            {
                _speed = timeRemainingFraction;
                return _speed;
            }
            else
            {
                _speed = timeRemainingFraction * (decimal)0.7;
                return _speed;
            }
        }

        /// <summary>
        /// Calculates the cleanliness of the table based on the customer's feedback.
        /// </summary>
        /// <param name="customer"></param>
        /// <returns></returns>
        public decimal CalculateCleanliness(Customer customer)
        {
            if (customer.TableCleanliness == StainLevel.Clean)
            {
                _cleanliness = (decimal)1.0;
                return _cleanliness;
            }
            else if (customer.TableCleanliness == StainLevel.SemiDirty)
            {
                _cleanliness = (decimal)0.5;
                return _cleanliness;
            }
            else
            {
                _cleanliness = (decimal)0.2;
                return _cleanliness;
            }
        }
        
        /// <summary>
        /// Calculates the overall satisfaction based on accuracy, speed, and cleanliness.
        /// </summary>
        /// <returns></returns>
        public decimal CalculateOverallSatisfaction()
        {
            _overallSatisfaction = (_accuracy + _speed + _cleanliness) / 3;
            return _overallSatisfaction;
        }
    }
}