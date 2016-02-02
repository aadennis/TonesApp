using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotesApp;
using System;
using System.Collections.Generic;

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
        public void WithinALargeNumberOfIterationsEachNumberInTheRangeAppearsAtLeastOnce() {
            var tallyOfFoundNumbers = new Dictionary<int, int>();
            const int lowerLimit = 1;
            const int upperLimit = 30;
            const int maxDistance = 13;
            for (var i = lowerLimit; i <= upperLimit; i++) {
                tallyOfFoundNumbers.Add(i, 0);
            }

            TraceExecutingMethod();
            for (var i = 0; i < MaxIterationsToTestForError; i++) {
                var randomBoundaries = NumberUtilities.GetRandomInterval(lowerLimit, upperLimit, maxDistance, _rand);
                for (var x = randomBoundaries[0]; x <= randomBoundaries[1]; x++) {
                    tallyOfFoundNumbers[x] = x;
                }
            }

            foreach (var number in tallyOfFoundNumbers.Keys) {
                if (tallyOfFoundNumbers[number] != number) {
                    throw new Exception($"expected an index for {number}");
                }
            }


        }

        private void TestInitialize() {
            _rand = new Random();
        }
    }
}
