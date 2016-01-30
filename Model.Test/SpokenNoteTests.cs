using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotesApp;

namespace Model.Test {
    [TestClass]
    public class SpokenNoteTests {
        [TestMethod]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void PassedNoteMustExist() {
            const string nonsenseNote = "X";

            try {
                NoteDescriptions.GetSpokenNameForNote(nonsenseNote);
            }
            catch (KeyNotFoundException e) {
                Assert.AreEqual("No note found for [X]", e.Message);
                throw;
            }

        }
    }
}
