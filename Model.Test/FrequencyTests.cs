using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotesApp;

namespace Model.Test {

    [TestClass]
    public class FrequencyTests : ModelTestBase {

        private const int TestFrequency = 147;
        private const int KnownIndexForFrequency = 2;

        [TestMethod]
        public void FrequencyDictionaryHas2OctavesOfNotes() {
            TraceExecutingMethod();
            var frequencyCount = new ToneUtility().GetAllNotes().Count;
            Assert.AreEqual((12 * 2), frequencyCount);
        }

        [TestMethod]
        public void RequestForFrequencyC3ReturnsExpectedElements() {
            TraceExecutingMethod();
            var toneSet = new ToneUtility();
            Assert.AreEqual("C", toneSet.GetNoteElements(TestFrequency).Note);
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
        [ExpectedException(typeof(ArgumentException))]
        public void RequestForNotFoundFrequencyThrowsException() {


            TraceExecutingMethod();
            var toneSet = new ToneUtility();

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
