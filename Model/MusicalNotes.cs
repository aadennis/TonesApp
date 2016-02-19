using System.Collections.Generic;
using System.Linq;

namespace NotesApp {

    /// <summary>
    /// Defines a collection of notes. In practice and currently, these are the 2 chromatic octaves
    /// starting with C at frequency 131Hz.
    /// Amplitude/volume is not an attribute of this collection.
    /// </summary>
    public class MusicalNotes {



        //http://www.phy.mtu.edu/~suits/notefreqs.html
        private static readonly List<MusicalNote> _notes = new List<MusicalNote> {
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

        /// <summary>
        /// Gets a collection of all frequencies supported by MusicalNotes
        /// </summary>
        /// <returns>a collection of frequencies</returns>
        public List<int> GetAllFrequencies() {
            return _notes.Select(note => note.Frequency).ToList();
        }

        /// <summary>
        /// Gets a collection of all Musical Notes  
        /// </summary>
        /// <returns>a collection of Musical Notes</returns>
        public List<MusicalNote> GetAllNotes() {
            return _notes;
        }

        /// <summary>
        /// Given a frequency, return a note. An unhandled exception will be
        /// thrown if the frequency is not found.
        /// todo [Handle and test the exception]
        /// </summary>
        /// <param name="frequency"></param>
        /// <returns>the Note matching the passed frequency</returns>
        public MusicalNote GetNote(int frequency) {
            return _notes.Single(note => note.Frequency.Equals(frequency));
        }

        /// <summary>
        ///  Get the note represented by the passed index, zero-based.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>the Note matching the passed index</returns>
        public MusicalNote GetNoteFromIndex(int index) {
            if (_notes.ElementAtOrDefault(index) == null) {
                throw new KeyNotFoundException($"No MusicalNote found with an index of [{index}]");
            }
            return _notes[index];
        }
    }


}