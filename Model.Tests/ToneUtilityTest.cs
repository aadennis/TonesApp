// <copyright file="ToneUtilityTest.cs">Copyright ©  2016</copyright>

using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotesApp;

namespace Model.Tests {

    /// <summary>
    /// Integration tests... which use your ears. So a bit hard to automate.
    /// But I should make a better attempt at Separation of Concerns on this.
    /// </summary>
    [TestClass]
    public class ToneUtilityTest {
        /// <summary>Test stub for PlayNote(Int32, String)</summary>
        [TestMethod]
        public void PlayAllNotes() {
            var setOfTones = new ToneUtility(); ;
            foreach (var note in setOfTones.GetAllNotes()) {
                setOfTones.PlayNote(note.Key, note.Value);
            }
        }
    }
}
