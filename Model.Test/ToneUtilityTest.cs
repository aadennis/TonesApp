using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotesApp;
using System.Speech.Synthesis;

namespace Model.Test {

    /// <summary>
    /// Integration tests... which use your ears. So a bit hard to automate.
    /// But I should make a better attempt at Separation of Concerns on this.
    /// </summary>
    [TestClass]
    public class ToneUtilityTest {

        private readonly Random _random = new Random();
        private SpeechSynthesizer synth = new SpeechSynthesizer();
        public ToneUtilityTest() {
            
        }


        [TestMethod]
        public void PlayAllNotes() {
            var setOfTones = new ToneUtility(); ;
            foreach (var note in setOfTones.GetAllNotes()) {
                setOfTones.PlayNote(note);
            }
        }

        [TestMethod]
        public void PlayIntervals() {

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
                System.Threading.Thread.Sleep(delayInSecondsBetweenAudioSnippets * 1000);
                synth.Speak(spokenInterval);
                Console.WriteLine(spokenInterval);
                System.Threading.Thread.Sleep(delayInSecondsBetweenAudioSnippets * 1000);
            }

        }

        [TestMethod]
        public void PlayAPerfectFourthInterval() {
            var interval = Intervals.GetInterval(5);
            Console.WriteLine(interval);
            synth.Speak(interval);

        }
    }


}
