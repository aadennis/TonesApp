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
        public void ValidSemitoneCountReturnsExpectedIntervalDescriptionForAudio() {
            var interval = 5;
            var description = Intervals.GetInterval(interval, true);
            Assert.AreEqual(@"c:\temp\PerfectFourth.wav", description);

            interval = 0;
            description = Intervals.GetInterval(interval, true);
            Assert.AreEqual(@"c:\temp\Unison.wav", description);

            interval = -1;
            description = Intervals.GetInterval(interval, true);
            Assert.AreEqual(@"c:\temp\MinorSecond.wav", description);

            interval = 5;
            description = Intervals.GetInterval(interval, true, "TestPrefix");
            Assert.AreEqual(@"c:\temp\TestPrefixPerfectFourth.wav", description);
        }

        [TestMethod]
        public void ValidDescendingSemitoneCountReturnsExpectedIntervalDescriptionForAudio() {
            var interval = 5;
            var description = Intervals.GetInterval(interval, true, "TestPrefix", true);
            Assert.AreEqual(@"c:\temp\TestPrefixPerfectFourthDesc.wav", description);
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
        public void WithinALargeNumberOfIterationsAllTheIntervalsExceptUnisonAppearAtLeastOnce() {
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

            // 0 is unison and not supported, so test starts from 1
            for (var i = 1; i <= Intervals.GetAllIntervals().Count; i++) {
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
            const int sectionCount = 5;
            var iterationsPerSection = totalIterations/sectionCount;

            var notes = new MusicalNotes();
            var upperLimit = notes.GetAllNotes().Count - 1;
            var directionTally = new Dictionary<int, int> {
                {(int) NumberUtilities.Direction.Ascending, 0},
                {(int) NumberUtilities.Direction.Descending, 0}
            };

            for (var i = 0; i < totalIterations; i++) {
                // do a break so that the listener has time to consolidate a bit
                if (i == 0) {
                    Debug.WriteLine($"Section 1");
                } else 
                if (i%iterationsPerSection == 0) {
                    Debug.WriteLine($"Section {i/iterationsPerSection + 1}");
                }
                //we want substantially more ascending than descending:
                var currentDirectionType = i%3 == 0 ? NumberUtilities.Direction.Random : NumberUtilities.Direction.Ascending;

                var interval = NumberUtilities.GetRandomInterval(0, upperLimit, 12, RandomInterval, currentDirectionType);
                notes.GetNoteFromIndex(interval[0]);
                notes.GetNoteFromIndex(interval[1]);
                directionTally[(int) NumberUtilities.GetDirection(interval)]++;
            }

            Debug.WriteLine(directionTally[0]);
            Debug.WriteLine(directionTally[1]);
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
