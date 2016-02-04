using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotesApp;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Model.Test {
    [TestClass]
    public class IntervalTests : ModelTestBase {

        private const int LowerLimit = 1;
        private const int UpperLimit = 30;
        private const int MaxDistance = 13;
        private const int MaxIterationsToTestForError = 10000;

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
            var interval = 5;
            var description = Intervals.GetInterval(interval);
            Assert.AreEqual("Perfect fourth", description);

            interval = 0;
            description = Intervals.GetInterval(interval);
            Assert.AreEqual("Unison", description);
        }

        [TestMethod]
        public void CountOfAllIntervalsIsCorrect() {
            Assert.AreEqual(13, Intervals.GetAllIntervals().Count);
        }

        [TestMethod, Ignore]
        public void WithinALargeNumberOfIterationsEachDescriptionInTheRangeAppearsAMinimumNumberOfTimes() {
            var tallyOfFoundNumbers = new SortedDictionary<object, int>();

            const int minimumCountForANumberInTheRange = 50;
            for (var i = LowerLimit; i <= UpperLimit; i++) {
                tallyOfFoundNumbers.Add(i.ToString(), 0);
            }

            ShowDictionary(LowerLimit, tallyOfFoundNumbers, "Initializing");

            TraceExecutingMethod();
            for (var i = 0; i < MaxIterationsToTestForError; i++) {
                var randomBoundaries = NumberUtilities.GetRandomInterval(LowerLimit, UpperLimit, MaxDistance, Rand);

                for (var x = randomBoundaries[0]; x <= randomBoundaries[1]; x++) {
                    tallyOfFoundNumbers[x]++;
                }
            }

            ShowDictionary(LowerLimit, tallyOfFoundNumbers, "Post population");


            foreach (var number in tallyOfFoundNumbers) {
                if (number.Value < minimumCountForANumberInTheRange) {
                    throw new Exception($"expected a count of at least [{minimumCountForANumberInTheRange}] for [{number}] but got only [{number.Value}]");
                }

            }
        }

        [TestMethod]
        public void WithinALargeNumberOfIterationsAllTheIntervalsAppearAtLeastOnce() {
            var dictionaryOfSemiToneCounts = new SortedDictionary<int, int>();

            TraceExecutingMethod();
            for (var i = 0; i < MaxIterationsToTestForError; i++) {
                var randomBoundaries = NumberUtilities.GetRandomInterval(LowerLimit, UpperLimit, MaxDistance, Rand);
                var semitoneDistance = randomBoundaries[1] - randomBoundaries[0];
                if (!dictionaryOfSemiToneCounts.ContainsKey(semitoneDistance)) {
                    dictionaryOfSemiToneCounts.Add(semitoneDistance, 0);
                }
                dictionaryOfSemiToneCounts[semitoneDistance]++;

            }

            for (var i = 0; i <= Intervals.GetAllIntervals().Count; i++) {
                Assert.IsTrue(dictionaryOfSemiToneCounts.ContainsKey(i), $"could not find key for {i}");
                Debug.WriteLine($"{i}: count: {dictionaryOfSemiToneCounts[i]}");
            }

        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void PassingInNullRandomToGetRandomIntervalThrowsException() {
            Random rand = null;
            try {
                // ReSharper disable once ExpressionIsAlwaysNull
                NumberUtilities.GetRandomInterval(LowerLimit, UpperLimit, MaxDistance, rand);
            }
            catch (ArgumentNullException e) {
                Assert.AreEqual("Value cannot be null.\r\nParameter name: rand", e.Message);
                throw;
            }
        }
    }
}
