using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace NotesApp {

    /// <summary>
    /// Defines a collection of notes. In practice and currently, these are the 2 chromatic octaves
    /// starting with C at frequency 131Hz.
    /// Amplitude/volume is not an attribute of this collection.
    /// </summary>
    public class MusicalNotes {

        //http://www.phy.mtu.edu/~suits/notefreqs.html
        private static readonly List<MusicalNote> Notes = new List<MusicalNote> {
            new MusicalNote {Sequence = 1, Frequency = 131, Note = "C", Octave = 3},
            new MusicalNote {Sequence = 2, Frequency = 139, Note = "C#", Octave = 3},
            new MusicalNote {Sequence = 3, Frequency = 147, Note = "D", Octave = 3},
            new MusicalNote {Sequence = 4, Frequency = 156, Note = "D#", Octave = 3},
            new MusicalNote {Sequence = 5, Frequency = 165, Note = "E", Octave = 3},
            new MusicalNote {Sequence = 6, Frequency = 175, Note = "F", Octave = 3},
            new MusicalNote {Sequence = 7, Frequency = 185, Note = "F#", Octave = 3},
            new MusicalNote {Sequence = 8, Frequency = 196, Note = "G", Octave = 3},
            new MusicalNote {Sequence = 9, Frequency = 208, Note = "G#", Octave = 3},
            new MusicalNote {Sequence = 10, Frequency = 220, Note = "A", Octave = 3},
            new MusicalNote {Sequence = 11, Frequency = 233, Note = "A#", Octave = 3},
            new MusicalNote {Sequence = 12, Frequency = 247, Note = "B", Octave = 3},
            new MusicalNote {Sequence = 13, Frequency = 262, Note = "C", Octave = 4},
            new MusicalNote {Sequence = 14, Frequency = 277, Note = "C#", Octave = 4},
            new MusicalNote {Sequence = 15, Frequency = 294, Note = "D", Octave = 4},
            new MusicalNote {Sequence = 16, Frequency = 311, Note = "D#", Octave = 4},
            new MusicalNote {Sequence = 17, Frequency = 330, Note = "E", Octave = 4},
            new MusicalNote {Sequence = 18, Frequency = 349, Note = "F", Octave = 4},
            new MusicalNote {Sequence = 19, Frequency = 370, Note = "F#", Octave = 4},
            new MusicalNote {Sequence = 20, Frequency = 392, Note = "G", Octave = 4},
            new MusicalNote {Sequence = 21, Frequency = 415, Note = "G#", Octave = 4},
            new MusicalNote {Sequence = 22, Frequency = 440, Note = "A", Octave = 4},
            new MusicalNote {Sequence = 23,Frequency = 466, Note = "A#", Octave = 4},
            new MusicalNote {Sequence = 24,Frequency = 494, Note = "B", Octave = 4}
        };                
                            
        /// <summary>
        /// Gets a collection of all frequencies supported by MusicalNotes
        /// </summary>
        /// <returns>a collection of frequencies</returns>
        public List<int> GetAllFrequencies() {
            return Notes.Select(note => note.Frequency).ToList();
        }

        /// <summary>
        /// Gets a collection of all Musical Notes  
        /// </summary>
        /// <returns>a collection of Musical Notes</returns>
        public List<MusicalNote> GetAllNotes() {
            return Notes;
        }

        /// <summary>
        /// Given a frequency, return a note. An unhandled exception will be
        /// thrown if the frequency is not found.
        /// todo [Handle and test the exception]
        /// </summary>
        /// <param name="frequency"></param>
        /// <returns>the Note matching the passed frequency</returns>
        public MusicalNote GetNote(int frequency) {
            return Notes.Single(note => note.Frequency.Equals(frequency));
        }

        /// <summary>
        ///  Get the note represented by the passed index, zero-based.
        /// </summary>
        /// <param name="index"></param>
        /// <returns>the Note matching the passed index</returns>
        public MusicalNote GetNoteFromIndex(int index) {
            if (Notes.ElementAtOrDefault(index) == null) {
                throw new KeyNotFoundException($"No MusicalNote found with an index of [{index}]");
            }
            return Notes[index];
        }

        /// <summary>
        ///  Get the note represented by the passed sequence, one-based.
        /// </summary>
        /// <param name="sequence"></param>
        /// <returns>the Note matching the passed sequence</returns>
        public MusicalNote GetNoteFromSequence(int sequence) {
            if (!Notes.Any(x => x.Sequence.Equals(sequence))) { 
                throw new KeyNotFoundException($"No MusicalNote found with a sequence of [{sequence}]");
            }
            return Notes.First(x => x.Sequence.Equals(sequence));
        }
    }
}