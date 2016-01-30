using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotesApp;

namespace Model.Test {

    [TestClass]
    public class NumberUtilityTests : ModelTestBase {

        const int MaxIterationsToTestForError = 1000;

        Random rand;

        public NumberUtilityTests() {
            TestInitialize();
        }

        [TestMethod]
        public void UpperAndLowerNumbersAreNeverTheSame() {
            TraceExecutingMethod();

            for (var i = 0; i < MaxIterationsToTestForError; i++) {
                var randomBoundaries = NumberUtilities.GetRandomInterval(1, 30, 7, rand);
                Assert.AreNotEqual(randomBoundaries[0], randomBoundaries[1]);
            }
        }

        [TestMethod]
        public void UpperAndLowerNumbersAreAlwaysWithinRequestedDistance() {
            const int maxDistance = 7;

            TraceExecutingMethod();
            for (var i = 0; i < MaxIterationsToTestForError; i++) {
                var randomBoundaries = NumberUtilities.GetRandomInterval(1, 30, 7, rand);
                Assert.IsTrue(maxDistance >= (randomBoundaries[1] - randomBoundaries[0]));
            }
        }

        [TestMethod]
        public void ReturnInputIfRequestedUpperAndLowerLimitsMatch() {
            const int upperLimit = 7;
            const int lowerLimit = upperLimit;

            TraceExecutingMethod();
            var randomBoundaries = NumberUtilities.GetRandomInterval(lowerLimit, upperLimit, 70000, rand);
            Assert.AreEqual(randomBoundaries[0], randomBoundaries[1]);
        }

        private void TestInitialize() {
            rand = new Random();
        }
    }
}
