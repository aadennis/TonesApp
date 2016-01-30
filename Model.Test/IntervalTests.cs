using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotesApp;

namespace Model.Test {
    [TestClass]
    public class IntervalTests {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RequestedIntervalOutsideTheKnownRangeThrowsException() {
            const int badInterval = -1;
            try {
                Intervals.GetInterval(badInterval);
            }
            catch (ArgumentException e) {
                Assert.AreEqual("Requested semi-tone count of [-1] is outside the allowed range", e.Message);
                throw;
            }
        }

        [TestMethod]
        public void ValidSemitoneCountReturnsExpectedIntervalDescription() {
            const int interval = 5;
            var description = Intervals.GetInterval(interval);
            Assert.AreEqual("Perfect fourth", description);
        }

        [TestMethod]
        public void CountOfAllIntervalsIsCorrect() {
            Assert.AreEqual(13, Intervals.GetAllIntervals().Count);
        }
    }
}
