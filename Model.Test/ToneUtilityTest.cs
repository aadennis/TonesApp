using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotesApp;

namespace Model.Test {

    /// <summary>
    /// Integration tests... which use your ears. So a bit hard to automate.
    /// But I should make a better attempt at Separation of Concerns on this.
    /// </summary>
    [TestClass]
    public class ToneUtilityTest {

        private Random _random = new Random();
        /// <summary>Test stub for PlayNote(Int32, String)</summary>
        [TestMethod]
        public void PlayAllNotes() {
            var setOfTones = new ToneUtility(); ;
            foreach (var note in setOfTones.GetAllNotes()) {
                setOfTones.PlayNote(note);
            }
        }

        [TestMethod]
        public void PlayNote() {
            var notes = new MusicalNotes();
            var interval = NumberUtilities.GetRandomInterval(1, 24, 12, _random);
            var setOfTones = new ToneUtility();
            setOfTones.PlayNote(notes.GetNoteFromIndex(interval[0]));
            setOfTones.PlayNote(notes.GetNoteFromIndex(interval[1]));
            Console.WriteLine(interval[0]);
            Console.WriteLine(interval[1]);
            var distance = interval[1] - interval[0];
            Console.WriteLine(distance);

        }
    }


}
