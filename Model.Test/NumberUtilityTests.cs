﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotesApp;

namespace Model.Tests {

    [TestClass]
    public class NumberUtilityTests : ModelTestBase {

        const int MaxIterationsToTestForError = 1000;

        NumberUtilities util;
        Random rand;

        public NumberUtilityTests() {
            TestInitialize();
        }

        [TestMethod]
        public void UpperAndLowerNumbersAreNeverTheSame() {
            TraceExecutingMethod();

            for (int i = 0; i < MaxIterationsToTestForError; i++) {
                var randomBoundaries = util.GetRandomInterval(1, 30, 7, rand);
                Assert.AreNotEqual(randomBoundaries[0], randomBoundaries[1]);
            }
        }

        [TestMethod]
        public void UpperAndLowerNumbersAreAlwaysWithinRequestedDistance() {
            const int maxDistance = 7;

            TraceExecutingMethod();
            for (int i = 0; i < MaxIterationsToTestForError; i++) {
                var randomBoundaries = util.GetRandomInterval(1, 30, 7, rand);
                //    System.Console.WriteLine(randomBoundaries[0]);
                //    System.Console.WriteLine(randomBoundaries[1]);
                //    System.Console.WriteLine("-----------------");
                Assert.IsTrue(maxDistance >= (randomBoundaries[1] - randomBoundaries[0]));
            }
        }

        [TestMethod]
        public void ReturnInputIfRequestedUpperAndLowerLimitsMatch() {
            var upperLimit = 7;
            var lowerLimit = upperLimit;

            TraceExecutingMethod();
            var randomBoundaries = util.GetRandomInterval(lowerLimit, upperLimit, 70000, rand);
            Assert.AreEqual(randomBoundaries[0], randomBoundaries[1]);
        }

        [TestMethod]
        public void FrequencyC3ReturnsExpectedElements() {

        }

        private void TestInitialize() {
            util = new NumberUtilities();
            rand = new Random();
        }
    }
}

// Example execution:
//     PS C:\temp\ps\Tones\Tones> dnx -p .\Model.Tests\project.json test
// xUnit.net DNX Runner (64-bit DNXCore 5.0)
//   Discovering: Model.Tests
//   Discovered:  Model.Tests
//   Starting:    Model.Tests
//   Finished:    Model.Tests
// === TEST EXECUTION SUMMARY ===
//    Model.Tests  Total: 3, Errors: 0, Failed: 0, Skipped: 0, Time: 0.164s