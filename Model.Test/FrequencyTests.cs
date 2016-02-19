using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotesApp;

namespace Model.Test {

    [TestClass]
    public class FrequencyTests : ModelTestBase {

        private const int TestFrequency = 147;
        private const int KnownIndexForFrequency = 2;
        private readonly IToneProvider _toneProvider = new ToneProvider(1.0f);

        [TestMethod]
        public void FrequencyDictionaryHas2OctavesOfNotes() {
            TraceExecutingMethod();
            var frequencyCount = new NoteUtility(_toneProvider).GetAllNotes().Count;
            Assert.AreEqual((12 * 2), frequencyCount);
        }

        [TestMethod]
        public void RequestForFrequencyC3ReturnsExpectedElements() {
            TraceExecutingMethod();
            var toneSet = new NoteUtility(_toneProvider);
            Assert.AreEqual("D", toneSet.GetNoteElements(TestFrequency).Note);
            Assert.AreEqual(3, toneSet.GetNoteElements(TestFrequency).Octave);
        }

        [TestMethod]
        public void FrequencyCanBeGotViaIndex() {
            TraceExecutingMethod();
            var notes = new MusicalNotes();
            var frequency = notes.GetNoteFromIndex(KnownIndexForFrequency).Frequency;
            Assert.AreEqual(TestFrequency, frequency);

        }

        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void GettingFrequencyViaNonValidIndexThrowsException() {
            TraceExecutingMethod();
            var notes = new MusicalNotes();
            try {
                var dummy = notes.GetNoteFromIndex(999).Frequency;
            }
            catch (KeyNotFoundException e) {
                Assert.AreEqual("No MusicalNote found with an index of [999]", e.Message);
                throw;
            }
        }

        [TestMethod]
        public void EachFrequencyInTheCollectionsNaturalOrderIsHigherThanThePrevious() {
            var notes = new MusicalNotes();
            var previousFrequency = 0;
            foreach (var frequency in notes.GetAllFrequencies()) {
                Console.WriteLine(frequency);
                Assert.IsTrue(frequency > previousFrequency, 
                    $"The current frequency {frequency} is not higher than the previous frequency {previousFrequency}");
                previousFrequency = frequency;
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void RequestForNotFoundFrequencyThrowsException() {
            TraceExecutingMethod();
            var toneSet = new NoteUtility(_toneProvider);
            try {
                toneSet.GetNoteElements(9999);
            }
            catch (ArgumentException ex) {
                Assert.AreEqual("[The frequency [9999] was not found in the set of available notes]", ex.Message);
                throw;
            }
        }
    }
}
