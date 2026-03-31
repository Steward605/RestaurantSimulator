using System;
using SplashKitSDK;
using static SplashKitSDK.SplashKit;
using System.Collections.Generic;

namespace CustomProgram
{
    public class Timer
    {
        private int _initialSeconds;
        private DateTime _startTime;
        private DateTime _endTime;
        private bool _isPaused;
        private DateTime _pauseStartTime;

        public Timer(int initialSeconds)
        {
            _initialSeconds = initialSeconds;
            ResetTimer();
        }
        
        /// <summary>
        /// Gets or sets the initial seconds for the timer.
        /// </summary>
        public int InitialSeconds
        {
            get { return _initialSeconds; }
            set { _initialSeconds = value; }
        }

        /// <summary>
        /// Resets the timer to the initial seconds and sets the start and end times.
        /// </summary>
        public void ResetTimer()
        {
            _isPaused = false;
            _startTime = DateTime.Now;
            _endTime = _startTime.AddSeconds(_initialSeconds);
        }

        /// <summary>
        /// Pauses the timer if it is not already paused.
        /// </summary>
        public void PauseTimer()
        {
            if (!_isPaused)
            {
                _pauseStartTime = DateTime.Now;
                _isPaused = true;
            }
        }

        /// <summary>
        /// Resumes the timer if it is paused.
        /// </summary>
        public void ResumeTimer()
        {
            if (_isPaused)
            {
                TimeSpan pauseDuration = DateTime.Now - _pauseStartTime;
                _endTime = _endTime.Add(pauseDuration);
                _startTime = _startTime.Add(pauseDuration);
                _isPaused = false;
            }
        }

        /// <summary>
        /// Adds or removes seconds from the timer's end time.
        /// </summary>
        /// <param name="seconds"></param>
        public void AddOrRemoveTime(int seconds)
        {
            _endTime = _endTime.AddSeconds(seconds);
        }

        /// <summary>
        /// Checks if the timer has ended.
        /// </summary>
        /// <returns></returns>
        public bool HasEnded()
        {
            return DateTime.Now >= _endTime;
        }

        /// <summary>
        /// Gets the remaining time until the timer ends.
        /// </summary>
        /// <returns></returns>
        public TimeSpan GetRemainingTime()
        {
            DateTime referenceTime;
            if (_isPaused == true)
            {
                referenceTime = _pauseStartTime;
            }
            else
            {
                referenceTime = DateTime.Now;
            }

            TimeSpan remainingTime = _endTime - referenceTime;
            if (remainingTime.TotalSeconds < 0)
            {
                return TimeSpan.Zero;
            }
            else
            {
                return remainingTime;
            }
        }

        /// <summary>
        /// Gets the elapsed time since the timer started.
        /// </summary>
        /// <returns></returns>
        public TimeSpan GetElapsedTime()
        {
            TimeSpan elapsedTime = DateTime.Now - _startTime;
            if (elapsedTime.TotalSeconds < 0)
            {
                return TimeSpan.Zero;
            }
            return elapsedTime;
        }

        /// <summary>
        /// Formats the remaining time as a string in MM:SS format.
        /// </summary>
        /// <returns></returns>
        public string GetFormattedRemainingTime()
        {
            TimeSpan timeLeft = GetRemainingTime();

            // extract minutes and seconds
            int minutesLeft = timeLeft.Minutes;
            int secondsLeft = timeLeft.Seconds;

            // in 2 digits format
            string minuteText = minutesLeft.ToString("D2");
            string secondText = secondsLeft.ToString("D2");

            return minuteText + ":" + secondText;
        }
    }
}