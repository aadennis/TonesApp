using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotesApp;
using System.Speech.Synthesis;
using System.Threading;

namespace Model.Test {

    /// <summary>
    /// Integration tests... which use your ears. So a bit hard to automate.
    /// But I should make a better attempt at Separation of Concerns on this.
    /// </summary>
    [TestClass]
    public class ToneUtilityTest {

        private readonly Random _random = new Random();
        private readonly SpeechSynthesizer _synth = new SpeechSynthesizer();

        [TestMethod]
        public void PlayAllNotes() {
            var setOfTones = new ToneUtility(); ;
            foreach (var note in setOfTones.GetAllNotes()) {
                setOfTones.PlayNote(note);
            }
        }

        [TestMethod]
        public void PlayIntervalsAndConfirmTheirName() {

            const int totalIterations = 5;
            const int delayInSecondsBetweenAudioSnippets = 3;
            var notes = new MusicalNotes();   
            var setOfTones = new ToneUtility();

            for (var i = 0; i < totalIterations; i++) {
                var intervalBoundaries = NumberUtilities.GetRandomInterval(1, 24, 12, _random);
                setOfTones.PlayNote(notes.GetNoteFromIndex(intervalBoundaries[0]));
                setOfTones.PlayNote(notes.GetNoteFromIndex(intervalBoundaries[1]));
                Console.WriteLine(intervalBoundaries[0]);
                Console.WriteLine(intervalBoundaries[1]);
                var semitoneCount = intervalBoundaries[1] - intervalBoundaries[0];
                var spokenInterval = Intervals.GetInterval(semitoneCount);
                Thread.Sleep(delayInSecondsBetweenAudioSnippets * 1000);
                _synth.Speak(spokenInterval);
                Console.WriteLine(spokenInterval);
                Thread.Sleep(delayInSecondsBetweenAudioSnippets * 1000);
            }

        }

        [TestMethod]
        public void PlayAPerfectFourthInterval() {
            var interval = Intervals.GetInterval(5);
            Console.WriteLine(interval);
            _synth.Speak(interval);
        }
    }


}
