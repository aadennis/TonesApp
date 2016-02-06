namespace NotesApp {

    /// <summary>
    /// POCO for a musical interval
    /// </summary>
    public class Interval {
        /// <summary>
        /// The inclusive count of semitones in the interval. Unison/0 is a valid count.
        /// </summary>
        public int SemiToneCount { get; set; }
        /// <summary>
        /// A description of the interval. For example, "Minor Third".
        /// </summary>
        public string Description { get; set; }
    }
}

