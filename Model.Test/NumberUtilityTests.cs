#define DEBUG

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotesApp;
using System;
using System.Collections.Generic;

namespace Model.Test {

    [TestClass]
    public class NumberUtilityTests : ModelTestBase {
        private const int LowerLimit = 1;
        private const int UpperLimit = 30;
        private const int MaxDistance = 13;
        private const int MaxIterationsToTestForError = 10000;

        public NumberUtilityTests() {
            TestInitialize();
        }

        [TestMethod]
        public void UpperAndLowerNumbersAreAlwaysWithinRequestedDistance() {
            const int maxDistance = 7;

            TraceExecutingMethod();
            for (var i = 0; i < MaxIterationsToTestForError; i++) {
                var randomBoundaries = NumberUtilities.GetRandomInterval(1, 30, 7, Rand);
                Assert.IsTrue(maxDistance >= (randomBoundaries[1] - randomBoundaries[0]));
            }
        }

        [TestMethod]
        public void ReturnInputIfRequestedUpperAndLowerLimitsMatch() {
            const int upperLimit = 7;
            const int lowerLimit = upperLimit;

            TraceExecutingMethod();
            var randomBoundaries = NumberUtilities.GetRandomInterval(lowerLimit, upperLimit, 70000, Rand);
            Assert.AreEqual(randomBoundaries[0], randomBoundaries[1]);
        }

        [TestMethod]
        public void WithinALargeNumberOfIterationsEachNumberInTheRangeAppearsAMinimumNumberOfTimes() {
            var tallyOfFoundNumbers = new SortedDictionary<int, int>();

            const int minimumCountForANumberInTheRange = 50;
            for (var i = LowerLimit; i <= UpperLimit; i++) {
                tallyOfFoundNumbers.Add(i, 0);
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
    }
}
