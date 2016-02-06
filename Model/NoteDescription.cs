namespace NotesApp {
    /// <summary>
    /// POCO containing metadata about a musical note
    /// </summary>
    public class NoteDescription {
        /// <summary>
        /// The identifier of a note
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// The name of the note phrased to remove any doubt about its pronunciation in English
        /// </summary>
        public string NameAsSpoken { get; set; }
    }
}
