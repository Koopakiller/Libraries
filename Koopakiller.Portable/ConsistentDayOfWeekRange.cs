using System;
using System.Collections;
using System.Collections.Generic;

namespace Koopakiller
{
    public struct ConsistentDayOfWeekRange
    {
        #region .ctor

        public ConsistentDayOfWeekRange(DayOfWeek singleDay) : this(singleDay, singleDay) { }

        public ConsistentDayOfWeekRange(DayOfWeek startDay, DayOfWeek endDay)
        {
            this.StartDay = startDay;
            this.EndDay = endDay;
        }

        #endregion

        #region Properties

        public DayOfWeek StartDay { get; }
        public DayOfWeek EndDay { get; }

        #endregion

        #region override

        public override string ToString()
        {
            if (this.StartDay == this.EndDay)
            {
                return this.StartDay.ToString();
            }
            if (this.StartDay == DayOfWeek.Saturday && this.EndDay == DayOfWeek.Sunday)
            {
                return "Weekend";
            }
            //Sunday = 0, ..., Saturday = 6
            if ((int)this.StartDay - 1 == (int)this.EndDay
                || (int)this.StartDay + 1 == (int)this.EndDay
                || (this.StartDay == DayOfWeek.Saturday && this.EndDay == DayOfWeek.Sunday)
                || (this.StartDay == DayOfWeek.Sunday && this.EndDay == DayOfWeek.Saturday))
            {
                return "Complete Week";
            }
            return $"{this.StartDay} - {this.EndDay}";
        }

        public override int GetHashCode()
        {
            return (int)this.StartDay + (int)this.EndDay * 1024;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is ConsistentDayOfWeekRange)) return false;
            var x = (ConsistentDayOfWeekRange)obj;
            return x.StartDay == this.StartDay && x.EndDay == this.EndDay;
        }

        #endregion

        public IEnumerable<DayOfWeek> GetDays()
        {
            if ((int)this.StartDay <= (int)this.EndDay)
            {
                for (var i = (int)this.StartDay; i <= (int)this.EndDay; ++i)
                {
                    yield return (DayOfWeek)i;
                }
            }
            else
            {
                for (var i = (int)this.StartDay; i <= 6; ++i)
                {
                    yield return (DayOfWeek)i;
                }

                for (var i = 0; i <= (int)this.EndDay; ++i)
                {
                    yield return (DayOfWeek)i;
                }
            }
        }

        #region Static Properties

        public static ConsistentDayOfWeekRange Weekend { get; } = new ConsistentDayOfWeekRange(DayOfWeek.Saturday, DayOfWeek.Sunday);
        public static ConsistentDayOfWeekRange BusinessDays { get; } = new ConsistentDayOfWeekRange(DayOfWeek.Monday, DayOfWeek.Friday);
        public static ConsistentDayOfWeekRange BusinessDaysWithSaturday { get; } = new ConsistentDayOfWeekRange(DayOfWeek.Monday, DayOfWeek.Saturday);
        public static ConsistentDayOfWeekRange CompleteWeek { get; } = new ConsistentDayOfWeekRange(DayOfWeek.Monday, DayOfWeek.Sunday);

        #endregion
    }
}