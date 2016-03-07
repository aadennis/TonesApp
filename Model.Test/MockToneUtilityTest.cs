using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotesApp;
using System;
using System.Diagnostics;
using System.Speech.Synthesis;
using Speaking;
using Moq;

namespace Model.Test {

    /// <summary>
    /// Integration tests... which use your ears to determine whether something is right. So a bit hard to automate.
    /// But I should make a better attempt at Separation of Concerns on this.
    /// </summary>
    [TestClass]
    public class MockToneUtilityTest {

        private readonly Random _random = new Random();
        private readonly Speech _synth = new Speech();
        private readonly IToneProvider _toneProvider = new ToneProvider(1.0f);
        private readonly IToneProvider _nAudioToneProvider = new NAudioSineWave(1.0f);


        public MockToneUtilityTest() {

        }


        [TestMethod]
        [TestCategory("MockSoundTest")]
        public void PlayAllNotesWithMockToneProvider() {
            var toneProvider = new Mock<IToneProvider>(MockBehavior.Strict);
            toneProvider.Setup(x => x.PlayTone(It.IsAny<int>(), It.IsAny<double>()));

            var setOfTones = new NoteUtility(toneProvider.Object);
            foreach (var note in setOfTones.GetAllNotes()) {
                setOfTones.PlayNote(note);
            }
        }

    }
}
