using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Speech.Synthesis;
using System.Threading;
using StringUtilities;
using Speaking;

namespace NotesApp {

    /// <summary>
    /// Wraps both the tone provision, and the voice used for speaking the intervals.
    /// The voice is spoken synchronously, as we don't want the tone and the voice happening at the same time.
    /// </summary>
    public class NoteUtility {

        private const int Duration = 1000;
        private const string StandardVoice = "Microsoft Hazel Desktop";

        private readonly MusicalNotes _notes = new MusicalNotes();
        private readonly SpeechSynthesizer _synth = new SpeechSynthesizer();
        private readonly IToneProvider _toneProvider;

        /// <summary>
        /// todo - the voice should be injected as an interface wrapping Speech.Synth, so we can test for invalid voice behaviour using a mock
        /// </summary>
        /// <param name="toneProvider"></param>
        /// <param name="voice"></param>
        public NoteUtility(IToneProvider toneProvider, string voice = StandardVoice) {
            _toneProvider = toneProvider;
            try {
                _synth.SelectVoice(voice);
            }
            catch (ArgumentException e) {
                if (e.Message.Contains("not set voice")) {
                    SetFallbackVoice();
                }
                else {
                    throw;
                }
            }
        }

        public MusicalNote GetNoteElements(int frequency) {
            var found = _notes.GetAllFrequencies().Any(x => x.Equals(frequency));
            if (!found) {
                throw new ArgumentException(
                    $"[The frequency [{frequency}] was not found in the set of available notes]");
            }
            return _notes.GetNote(frequency);
        }

        public void PlayNote(MusicalNote note) {
            _toneProvider.PlayTone(note.Frequency, 1);
        }

        /// <summary>
        /// Locate and play the audio file matching the passed note.
        /// This method is specifically for rendering notes, not their spoken representation
        /// </summary>
        /// <param name="note"></param>
        public void PlayNoteAsAudio(MusicalNote note) { 
            var tmp = note.Note + note.Octave;
            var x = StringUtility.MakeFileName(@"c:\temp", tmp, "wav");

            _toneProvider.PlayAudio(x);
        }

        /// <summary>
        /// Play the musical interval a number of times (todo: improve that), followed by the description,
        /// e.g. "Minor second descending". 
        /// The call to GetInterval does most of the hard work.
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="delayInSeconds"></param>
        /// <param name="audioNotePrefix"></param>
        /// <param name="isAudio">true if the commentary is sourced from a wav file</param>
        public void PlayIntervalWithCommentary(List<int> interval, int delayInSeconds, string audioNotePrefix = "", bool isAudio = false) {
            PlayAndDelay(interval, delayInSeconds, isAudio);
            PlayAndDelay(interval, delayInSeconds, isAudio);

            var semitoneCount = interval[1] - interval[0];
            var direction = NumberUtilities.GetSpokenDirection(NumberUtilities.GetDirection(interval));
            var isDescending = NumberUtilities.IsDescending(interval);
            var spokenInterval = Intervals.GetInterval(semitoneCount, isAudio, audioNotePrefix, isDescending);

            if (!isAudio) {
                _synth.Speak(
                    $"{spokenInterval}; {direction}");
            }
            else { //audio...
                var builder = new PromptBuilder();
                builder.AppendAudio(spokenInterval);
                _synth.Speak(builder);
            }
            Thread.Sleep(delayInSeconds * 1000);
        }

        private void PlayAndDelay(IReadOnlyList<int> interval, int delayInSeconds, bool isAudio = false ) {
            if (!isAudio) {
                PlayNote(_notes.GetNoteFromIndex(interval[0]));
                PlayNote(_notes.GetNoteFromIndex(interval[1]));

            }
            else {
                PlayNoteAsAudio(_notes.GetNoteFromIndex(interval[0]));
                PlayNoteAsAudio(_notes.GetNoteFromIndex(interval[1]));
            }
            Thread.Sleep(delayInSeconds * 1000);
        }

        private static void PlaySound(Stream mStream) {
            using (var ss = new SoundPlayer(mStream)) {
                ss.PlaySync();
            }
        }

        private void SetFallbackVoice() {
            _synth.SelectVoiceByHints(VoiceGender.Neutral);
        }

        public List<MusicalNote> GetAllNotes() {
            return _notes.GetAllNotes();
        }

        /// <summary>
        /// Wraps the synchronous call to the Speech Synth. 
        /// </summary>
        /// <param name="message"></param>

        public void Speak(string message) {
            _synth.Speak(message);
        }

    }
}