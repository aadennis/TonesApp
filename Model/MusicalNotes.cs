using System.Collections.Generic;
using System.Linq;

namespace NotesApp {

    public class MusicalNotes {
        private readonly List<MusicalNote> _notes = new List<MusicalNote> {
            new MusicalNote {Frequency = 131, Note = "C", Octave = 3},
            new MusicalNote {Frequency = 139, Note = "C#", Octave = 3},
            new MusicalNote {Frequency = 147, Note = "D", Octave = 3},
            new MusicalNote {Frequency = 156, Note = "D#", Octave = 3},
            new MusicalNote {Frequency = 165, Note = "E", Octave = 3},
            new MusicalNote {Frequency = 175, Note = "F", Octave = 3},
            new MusicalNote {Frequency = 185, Note = "F#", Octave = 3},
            new MusicalNote {Frequency = 196, Note = "G", Octave = 3},
            new MusicalNote {Frequency = 208, Note = "G#", Octave = 3},
            new MusicalNote {Frequency = 220, Note = "A", Octave = 3},
            new MusicalNote {Frequency = 233, Note = "A#", Octave = 3},
            new MusicalNote {Frequency = 247, Note = "B", Octave = 3},
            new MusicalNote {Frequency = 262, Note = "C", Octave = 4},
            new MusicalNote {Frequency = 277, Note = "C#", Octave = 4},
            new MusicalNote {Frequency = 294, Note = "D", Octave = 4},
            new MusicalNote {Frequency = 311, Note = "D#", Octave = 4},
            new MusicalNote {Frequency = 330, Note = "E", Octave = 4},
            new MusicalNote {Frequency = 349, Note = "F", Octave = 4},
            new MusicalNote {Frequency = 370, Note = "F#", Octave = 4},
            new MusicalNote {Frequency = 392, Note = "G", Octave = 4},
            new MusicalNote {Frequency = 415, Note = "G#", Octave = 4},
            new MusicalNote {Frequency = 440, Note = "A", Octave = 4},
            new MusicalNote {Frequency = 466, Note = "A#", Octave = 4},
            new MusicalNote {Frequency = 494, Note = "B", Octave = 4}
        };

        public List<int> GetAllFrequencies() {
            return _notes.Select(note => note.Frequency).ToList();
        }

        public List<MusicalNote> GetAllNotes() {
            return _notes;
        }

        public MusicalNote GetNote(int frequency) {
            return _notes.Single(note => note.Frequency.Equals(frequency));
        }

        public MusicalNote GetNoteFromIndex(int index) {
            return _notes[index];
        }
    }


}