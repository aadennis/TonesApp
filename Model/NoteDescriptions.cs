using System;
using System.Collections.Generic;
using System.Linq;

namespace NotesApp {
    public static class NoteDescriptions {
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

        public static List<NoteDescription> GetAllNoteDescriptions() {
            return NoteDescriptionSet;
        }

        public static string GetSpokenNameForNote(string note) {
            var found = NoteDescriptionSet.Any(noted => noted.Note.Equals(note));
            if (!found) {
                throw new KeyNotFoundException($"No note found for [{note}]");
            }
            return NoteDescriptionSet.Single(noted => noted.Note.Equals(note)).NameAsSpoken;

        }
    }
}
