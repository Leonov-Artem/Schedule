using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using static Schedule.ScheduleType;

namespace Schedule
{
    public class Schedule
    {
        string[] scheduleTypes = new string[]
        {
            DAY,
            NIGHT,
            AFTER_NIGHT,
            DAY_OFF
        };

        DateTime _currentDate;
        RingList<string> _ringList;

        public Schedule()
        {
            _currentDate = DateTime.Now;
            _ringList = new RingList<string>(scheduleTypes);
        }

        public string Calculate(string currentType, DateTime newDate)
        {
            SwitchRingListToCurrentType(currentType);
            string newType = currentType;

            while (CurrentDate() <= newDate.Date)
            {
                SwitchToNextDay();
                newType = NextType();
            }

            return newType;
        }

        private void SwitchRingListToCurrentType(string type)
        {
            while (CurrentType() != type)
                _ = NextType();
        }

        private string NextType()
            => _ringList.Next;

        private string CurrentType()
            => _ringList.Current;

        private DateTime CurrentDate()
            => _currentDate.Date;

        private void SwitchToNextDay()
            => _currentDate = _currentDate.AddDays(1);
    }
}
