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
            const int badInterval = -13;
            try {
                Intervals.GetInterval(badInterval);
            }
            catch (ArgumentException e) {
                Assert.AreEqual("Requested semi-tone count of [-13] is outside the allowed range", e.Message);
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

            interval = -1;
            description = Intervals.GetInterval(interval);
            Assert.AreEqual("Minor second", description);
        }

        [TestMethod]
        public void CountOfAllIntervalsIsCorrect() {
            Assert.AreEqual(13, Intervals.GetAllIntervals().Count);
        }

        [TestMethod]
        public void WithinALargeNumberOfIterationsEachDescriptionInTheRangeAppearsAMinimumNumberOfTimes() {
            const int minimumCountForANumberInTheRange = 50;

            var tallyOfFoundNumbers = new SortedDictionary<int, int>();
            for (var i = LowerLimit; i <= UpperLimit; i++) {
                tallyOfFoundNumbers.Add(i, 0);
            }

            ShowDictionary(LowerLimit, tallyOfFoundNumbers, "Initializing");

            TraceExecutingMethod();
            for (var i = 0; i < MaxIterationsToTestForError; i++) {
                var randomBoundaries = NumberUtilities.GetRandomInterval(LowerLimit, UpperLimit, MaxDistance, RandomInterval);

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
                var randomBoundaries = NumberUtilities.GetRandomInterval(LowerLimit, UpperLimit, MaxDistance, RandomInterval);
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
        public void WithinASmallNumberOfIterationsIntervalsAscendAndDescendOnRandomDirection() {
            var gotDescending = false;
            var gotAscending = false;
            TraceExecutingMethod();
            for (var i = 0; i < 10; i++) {
                var randomBoundaries =
                    NumberUtilities.GetRandomInterval(LowerLimit, UpperLimit, MaxDistance,
                        RandomInterval, NumberUtilities.Direction.Random);
                if (randomBoundaries[1] > randomBoundaries[0]) {
                    gotAscending = true;
                }
                if (randomBoundaries[0] > randomBoundaries[1]) {
                    gotDescending = true;
                }
            }
            Assert.IsTrue(gotAscending, "No ascending interval was found");
            Assert.IsTrue(gotDescending, "No descending interval was found");
        }

        [TestMethod]
        public void PlayIntervalsAndConfirmTheirNameNoSound() {

            const int totalIterations = 1000;
            var notes = new MusicalNotes();
            var upperLimit = notes.GetAllNotes().Count - 1;

            for (var i = 0; i < totalIterations; i++) {
                var intervalBoundaries = NumberUtilities.GetRandomInterval(0, upperLimit, 12, RandomInterval, NumberUtilities.Direction.Random);
                notes.GetNoteFromIndex(intervalBoundaries[0]);
                notes.GetNoteFromIndex(intervalBoundaries[1]);
                NumberUtilities.GetDirection(intervalBoundaries);
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
