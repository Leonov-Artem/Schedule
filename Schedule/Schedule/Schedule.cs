using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Schedule
{
    public class Schedule
    {
        ScheduleType[] scheduleTypes = new ScheduleType[]
        {
            ScheduleType.Day,
            ScheduleType.Night,
            ScheduleType.AfterNight,
            ScheduleType.DayOff
        };

        DateTime _currentDate;
        RingList<ScheduleType> _ringList;

        public Schedule()
        {
            _currentDate = DateTime.Now;
            _ringList = new RingList<ScheduleType>(scheduleTypes);
        }

        public ScheduleType Calculate(ScheduleType currentType, DateTime newDate)
        {
            SwitchRingListToCurrentType(currentType);
            ScheduleType newType = currentType;

            while (CurrentDate() <= newDate.Date)
            {
                SwitchToNextDay();
                newType = NextType();
            }

            return newType;
        }

        private void SwitchRingListToCurrentType(ScheduleType type)
        {
            while (CurrentType() != type)
                _ = NextType();
        }

        private ScheduleType NextType()
            => _ringList.Next;

        private ScheduleType CurrentType()
            => _ringList.Current;

        private void ResetRingList()
            => _ringList.Reset();

        private DateTime CurrentDate()
            => _currentDate.Date;

        private void SwitchToNextDay()
            => _currentDate = _currentDate.AddDays(1);
    }
}
