﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotesApp;
using System;
using System.Diagnostics;
using System.Speech.Synthesis;

namespace Model.Test {

    /// <summary>
    /// Integration tests... which use your ears to determine whether something is right. So a bit hard to automate.
    /// But I should make a better attempt at Separation of Concerns on this.
    /// </summary>
    [TestClass]
    public class ToneUtilityTest {

        private readonly Random _random = new Random();
        private readonly SpeechSynthesizer _synth = new SpeechSynthesizer();

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

            const int totalIterations = 1000;
            var notes = new MusicalNotes();
            var setOfTones = new ToneUtility();
            var upperLimit = notes.GetAllNotes().Count - 1;

            for (var i = 0; i < totalIterations; i++) {
                var intervalBoundaries = NumberUtilities.GetRandomInterval(0, upperLimit, 12, _random, NumberUtilities.Direction.Random);
                setOfTones.PlayIntervalWithCommentary(intervalBoundaries, 5);
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
