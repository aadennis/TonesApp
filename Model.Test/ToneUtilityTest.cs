using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotesApp;
using System;
using System.Diagnostics;
using Speaking;

namespace Model.Test {

    /// <summary>
    /// Integration tests... which use your ears to determine whether something is right. So a bit hard to automate.
    /// But I should make a better attempt at Separation of Concerns on this.
    /// </summary>
    [TestClass]
    public class ToneUtilityTest {

        private readonly Random _random = new Random();
        private readonly Speech _synth = new Speech();

        [TestMethod]
        [TestCategory("SoundTest")]
        public void PlayAllNotes() {
            var setOfTones = new ToneUtility(); ;
            foreach (var note in setOfTones.GetAllNotes()) {
                setOfTones.PlayNote(note);
            }
        }

        [TestMethod]
        [TestCategory("SoundTestVeryLongDuration")]
        public void PlayIntervalsAndConfirmTheirName() {

            const int totalIterations = 300;
            const int sectionCount = 6;
            const int iterationsPerSection = totalIterations / sectionCount;
            const int secondsToSleep = 5;
            var notes = new MusicalNotes();
            var setOfTones = new ToneUtility();
            var upperLimit = notes.GetAllNotes().Count - 1;

            for (var i = 0; i < totalIterations; i++) {
                // do a break so that the listener has time to consolidate a bit
                if (i == 0) {
                    _synth.Speak("This is; Musical Intervals: Section 1");
                    
                }
                else
                if (i % iterationsPerSection == 0) {
                    _synth.Speak($"This is; Musical Intervals: Section {i / iterationsPerSection + 1}");
                }
                //we want substantially more ascending than descending intervals - this spread gives about 80%:
                var currentDirectionType = i % 3 == 0 ? NumberUtilities.Direction.Random : NumberUtilities.Direction.Ascending;
                var interval = NumberUtilities.GetRandomInterval(0, upperLimit, 12, _random, currentDirectionType);
                setOfTones.PlayIntervalWithCommentary(interval, secondsToSleep);
            }
        }


        [TestMethod]
        [TestCategory("SoundTest")]
        public void SpeakAllTheIntervals() {
            var intervals = Intervals.GetAllIntervals();
            foreach (var interval in intervals) {
                string wordsToSpeak = $"SemitoneCount: {interval.SemiToneCount};{interval.Description}";
                Debug.WriteLine(wordsToSpeak);
                _synth.Speak(wordsToSpeak);
            }
        }
    }
}
