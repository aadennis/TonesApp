namespace NotesApp {
    public class MusicalNote {

        /// <summary>
        /// A POCO representing a musical note. Typically the sequence (an index from 1 to n) will
        /// be the search criterion, with the other columns being returned.
        /// </summary>
        public int Sequence { get; set; }
        public int Frequency { get; set; }
        public int Octave { get; set; }
        public string Note { get; set; }
    }
}
