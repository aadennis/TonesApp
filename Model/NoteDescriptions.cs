using System;
using System.Collections.Generic;
using System.Linq;

namespace NotesApp {

    /// <summary>
    /// Metadata for the 12 chromatic notes C to B. Currently limited to the spoken English for each
    /// note, with a usecase of an application which speaks the names of notes aloud.
    /// </summary>
    public static class NoteDescriptions {

        /// <summary>
        /// The set of notes C to B
        /// </summary>
        private static readonly List<NoteDescription> NoteDescriptionSet = new List<NoteDescription> {
            new NoteDescription {Note = "C", NameAsSpoken = "C"},
            new NoteDescription {Note = "C#", NameAsSpoken = "C sharp"},
            new NoteDescription {Note = "D", NameAsSpoken = "D"},
            new NoteDescription {Note = "D#", NameAsSpoken = "D  sharp"},
            new NoteDescription {Note = "E", NameAsSpoken = "E"},
            new NoteDescription {Note = "F", NameAsSpoken = "F"},
            new NoteDescription {Note = "F#", NameAsSpoken = "F sharp"},
            new NoteDescription {Note = "G", NameAsSpoken = "G"},
            new NoteDescription {Note = "G#", NameAsSpoken = "G sharp"},
            new NoteDescription {Note = "A", NameAsSpoken = "A"},
            new NoteDescription {Note = "A#", NameAsSpoken = "A sharp"},
            new NoteDescription {Note = "B", NameAsSpoken = "B"}
        };

        /// <summary>
        /// Return the full set of notes C to B
        /// </summary>
        /// <returns>the set of note descriptions</returns>
        public static List<NoteDescription> GetAllNoteDescriptions() {
            return NoteDescriptionSet;
        }

        /// <summary>
        /// Given a single note identifier as key, return the details matching that indentifier.
        /// If the key is note found, an exception is thrown.
        /// </summary>
        /// <param name="note">the key of the note to find in the set of notes</param>
        /// <returns>the details of the found note</returns>
        public static string GetSpokenNameForNote(string note) {
            var found = NoteDescriptionSet.Any(noted => noted.Note.Equals(note));
            if (!found) {
                throw new KeyNotFoundException($"No note found for [{note}]");
            }
            return NoteDescriptionSet.Single(noted => noted.Note.Equals(note)).NameAsSpoken;

        }
    }
}
