using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Koopakiller.Portable.UnitTests
{
    [TestClass]
    public class ConsistentDayOfWeekRangeTests
    {
        #region GetDays

        [TestMethod]
        public void GetDays_MonToFri()
        {
            var cdowr = new ConsistentDayOfWeekRange(DayOfWeek.Monday, DayOfWeek.Friday);
            Assert.IsTrue(cdowr.GetDays().SequenceEqual(new[]
            {
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday,
            }));
        }

        [TestMethod]
        public void GetDays_FriToMon()
        {
            var cdowr = new ConsistentDayOfWeekRange(DayOfWeek.Friday, DayOfWeek.Monday);
            Assert.IsTrue(cdowr.GetDays().SequenceEqual(new[]
            {
                DayOfWeek.Friday,
                DayOfWeek.Saturday,
                DayOfWeek.Sunday,
                DayOfWeek.Monday,
            }));
        }

        [TestMethod]
        public void GetDays_Weekend()
        {
            var cdowr = new ConsistentDayOfWeekRange(DayOfWeek.Saturday, DayOfWeek.Sunday);
            Assert.IsTrue(cdowr.GetDays().SequenceEqual(new[]
            {
                DayOfWeek.Saturday,
                DayOfWeek.Sunday,
            }));
        }

        [TestMethod]
        public void GetDays_CompleteWeek()
        {
            var cdowr = new ConsistentDayOfWeekRange(DayOfWeek.Monday, DayOfWeek.Sunday);
            Assert.IsTrue(cdowr.GetDays().SequenceEqual(new[]
            {
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
                DayOfWeek.Friday,
                DayOfWeek.Saturday,
                DayOfWeek.Sunday,
            }));
        }

        [TestMethod]
        public void GetDays_CompleteWeekInnerStart()
        {
            var cdowr = new ConsistentDayOfWeekRange(DayOfWeek.Friday, DayOfWeek.Thursday);
            Assert.IsTrue(cdowr.GetDays().SequenceEqual(new[]
            {
                DayOfWeek.Friday,
                DayOfWeek.Saturday,
                DayOfWeek.Sunday,
                DayOfWeek.Monday,
                DayOfWeek.Tuesday,
                DayOfWeek.Wednesday,
                DayOfWeek.Thursday,
            }));
        }

        [TestMethod]
        public void GetDays_SingleDay()
        {
            var cdowr = new ConsistentDayOfWeekRange(DayOfWeek.Wednesday);
            Assert.IsTrue(cdowr.GetDays().SequenceEqual(new[]
            {
                DayOfWeek.Wednesday,
            }));
        }

        #endregion
    }
}
