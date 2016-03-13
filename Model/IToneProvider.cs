namespace NotesApp {

    /// <summary>
    /// Supports the playing (i.e. stuff heard through speakers) of tones expressed as frequencies
    /// </summary>
    public interface IToneProvider {

        /// <summary>
        /// Play a tone 
        /// </summary>
        /// <param name="frequency">the required frequency in Hertz</param>
        /// <param name="duration">the required duration in seconds</param>
        void PlayTone(int frequency, double duration);

        /// <summary>
        /// Play an audio file, being a musical note
        /// </summary>
        /// <param name="noteName">The note to play, including its octave, e.g. F#3</param>
        void PlayAudio(string noteName);



    }
}
