#define DEBUG

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotesApp;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Model.Test {

    [TestClass]
    public class NumberUtilityTests : ModelTestBase {

        const int MaxIterationsToTestForError = 10000;

        private Random _rand;

        public NumberUtilityTests() {
            TestInitialize();
        }

        [TestMethod]
        // There is an assumption in this that we are not interested in Unison... perhaps we should be?
        public void UpperAndLowerNumbersAreNeverTheSame() {
            TraceExecutingMethod();

            for (var i = 0; i < MaxIterationsToTestForError; i++) {
                var randomBoundaries = NumberUtilities.GetRandomInterval(1, 30, 7, _rand);
                Assert.AreNotEqual(randomBoundaries[0], randomBoundaries[1]);
            }
        }

        [TestMethod]
        public void UpperAndLowerNumbersAreAlwaysWithinRequestedDistance() {
            const int maxDistance = 7;

            TraceExecutingMethod();
            for (var i = 0; i < MaxIterationsToTestForError; i++) {
                var randomBoundaries = NumberUtilities.GetRandomInterval(1, 30, 7, _rand);
                Assert.IsTrue(maxDistance >= (randomBoundaries[1] - randomBoundaries[0]));
            }
        }

        [TestMethod]
        public void ReturnInputIfRequestedUpperAndLowerLimitsMatch() {
            const int upperLimit = 7;
            const int lowerLimit = upperLimit;

            TraceExecutingMethod();
            var randomBoundaries = NumberUtilities.GetRandomInterval(lowerLimit, upperLimit, 70000, _rand);
            Assert.AreEqual(randomBoundaries[0], randomBoundaries[1]);
        }

        [TestMethod]
        public void WithinALargeNumberOfIterationsEachNumberInTheRangeAppearsAMinimumNumberOfTimes() {
            var tallyOfFoundNumbers = new SortedDictionary<int, int>();


            const int lowerLimit = 1;
            const int upperLimit = 30;
            const int maxDistance = 13;
            const int minimumCountForANumberInTheRange = 50;
            for (var i = lowerLimit; i <= upperLimit; i++) {
                tallyOfFoundNumbers.Add(i, 0);
            }

            ShowDictionary(lowerLimit, tallyOfFoundNumbers, "Initializing");

            TraceExecutingMethod();
            for (var i = 0; i < MaxIterationsToTestForError; i++) {
                var randomBoundaries = NumberUtilities.GetRandomInterval(lowerLimit, upperLimit, maxDistance, _rand);

                for (var x = randomBoundaries[0]; x <= randomBoundaries[1]; x++) {
                    tallyOfFoundNumbers[x]++;
                }
            }

            ShowDictionary(lowerLimit, tallyOfFoundNumbers, "Post population");


            foreach (var number in tallyOfFoundNumbers) {
                if (number.Value < minimumCountForANumberInTheRange) {
                    throw new Exception($"expected a count of at least [{minimumCountForANumberInTheRange}] for [{number}] but got only [{number.Value}]");
                }

            }
        }

        private static void ShowDictionary(int lowerLimit, SortedDictionary<int, int> tallyOfFoundNumbers, string prefixMessage) {
            for (var i = lowerLimit; i <= tallyOfFoundNumbers.Count; i++) {
                Debug.WriteLine($"[{i}]{prefixMessage}: {tallyOfFoundNumbers[i]}");
            }
        }

        private void TestInitialize() {
            Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Debug.AutoFlush = true;
            _rand = new Random();
        }
    }
}
