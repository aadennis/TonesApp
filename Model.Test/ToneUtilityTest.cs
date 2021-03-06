﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotesApp;
using System;
using System.Diagnostics;
using System.Speech.Synthesis;
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
        private readonly IToneProvider _toneProvider = new ToneProvider(1.0f);
        private readonly IToneProvider _nAudioToneProvider = new NAudioSineWave(1.0f);


        public ToneUtilityTest() {

        }


        [TestMethod]
        [TestCategory("SoundTest")]
        public void PlayAllNotesWithMyToneProvider() {
            var setOfTones = new NoteUtility(_toneProvider); ;
            foreach (var note in setOfTones.GetAllNotes()) {
                setOfTones.PlayNote(note);
            }
        }

        [TestMethod]
        [TestCategory("SoundTest")]
        public void PlayAllNotesWithNAudioToneProvider() {

            var setOfTones = new NoteUtility(_nAudioToneProvider);
            foreach (var note in setOfTones.GetAllNotes()) {
                setOfTones.PlayNote(note);
            }
        }

        [TestMethod]
        [TestCategory("SoundTest")]
        public void VoiceCallingNoteUtilitySoundsAsExpected() {
            var setOfTones = new NoteUtility(_nAudioToneProvider);
            setOfTones.Speak("what voice am I?");
        }

        [TestMethod]
        [TestCategory("SoundTestVeryLongDuration")]
        public void PlayIntervalsAndConfirmTheirName() {

            const int totalIterations = 10;
            const int sectionCount = 2;
            const int iterationsPerSection = totalIterations / sectionCount;
            const int secondsToSleep = 5;
            var notes = new MusicalNotes();
            var setOfTones = new NoteUtility(_toneProvider);
            var upperLimit = notes.GetAllNotes().Count - 1;

            SpeakIntroduction();
            for (var i = 0; i < totalIterations; i++) {
                // do a break so that the listener has time to consolidate a bit
                if (i % iterationsPerSection == 0) {
                    _synth.Speak($"This is; Musical Intervals: Section {i / iterationsPerSection + 1}");
                }
                //we want substantially more ascending than descending intervals - this spread gives about 80%:
                var currentDirectionType = i % 3 == 0 ? NumberUtilities.Direction.Random : NumberUtilities.Direction.Ascending;
                var interval = NumberUtilities.GetRandomInterval(0, upperLimit, 12, _random, currentDirectionType);

                if (i < (totalIterations/2)) {
                    // play the voice/piano versions...
                    setOfTones.PlayIntervalWithCommentary(interval, secondsToSleep, "Emm", true);
                }
                else {
                    setOfTones.PlayIntervalWithCommentary(interval, secondsToSleep);
                }
            }


        }

        private void SpeakIntroduction() {
            return;
            var wordsToSpeak = "Thanks for choosing to listen to ear-yer Training in Musical Intervals";
            _synth.Speak(wordsToSpeak);
            wordsToSpeak =
                "This track has a LOT of intervals, spread over more than 40 minutes. Half the intervals are played using a piano, then half using er synther-sizer";
            _synth.Speak(wordsToSpeak);
            wordsToSpeak =
                "Practice makes perfect, so to get the BEST! out of this, listen to the track LOTS of times.";
            _synth.Speak(wordsToSpeak);
            wordsToSpeak =
                "OK, let’s start!";
            _synth.Speak(wordsToSpeak);
        }

        [TestMethod]
        [TestCategory("SoundTestVeryLongDuration")]
        public void PlayIntervalsAsSineAndConfirmTheirNameAsAudio() {

            const int totalIterations = 300;
            const int sectionCount = 6;
            const int iterationsPerSection = totalIterations / sectionCount;
            const int secondsToSleep = 5;
            var notes = new MusicalNotes();
            var setOfTones = new NoteUtility(_toneProvider);
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
                setOfTones.PlayIntervalWithCommentary(interval, secondsToSleep, "", false);
            }
        }


        [TestMethod]
        [TestCategory("SoundTest")]
        public void SpeakAllTheIntervalsUsingSynthesis() {
            var intervals = Intervals.GetAllIntervals();
            foreach (var interval in intervals) {
                string wordsToSpeak = $"SemitoneCount: {interval.SemiToneCount};{interval.Description}";
                Debug.WriteLine(wordsToSpeak);
                _synth.Speak(wordsToSpeak);
            }
        }

        [TestMethod]
        [TestCategory("SoundTest")]
        public void SpeakAllTheIntervalsUsingStoredAudioWithFilePrefix() {
            var s = new Speech();
            var intervals = Intervals.GetAudioIntervals("Emm");
            foreach (var interval in intervals) {
                s.SpeakAudio(interval);
            }
        }

        [TestMethod]
        [TestCategory("SoundTest")]
        public void SpeakAllTheIntervalsUsingStoredAudioNoFilePrefix() {
            var s = new Speech();
            var intervals = Intervals.GetAudioIntervals();
            foreach (var interval in intervals) {
                s.SpeakAudio(interval);
            }
        }

        [TestMethod]
        [TestCategory("SoundTest")]
        public void SpeakAnIntervalUsingStoredAudio() {
            var s = new Speech();
            var intervals = Intervals.GetAudioIntervals();
            foreach (var interval in intervals) {
                s.SpeakAudio(interval);
            }
        }


        [TestMethod]
        [TestCategory("SoundTest")]
        public void PromptBuilderBasicTest() {  
            _synth.SpeakWithPromptBuilder();
        }
    }
}
