using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Speech.Synthesis;
using System.Threading;

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
            Console.WriteLine("[{0}][{1}]", note.Frequency, note.Note);
            _toneProvider.PlayTone(note.Frequency, 1);
        }

        public void PlayIntervalWithCommentary(List<int> interval, int delayInSeconds) {
            PlayAndDelay(interval, delayInSeconds);
            PlayAndDelay(interval, delayInSeconds);

            var semitoneCount = interval[1] - interval[0];
            var spokenInterval = Intervals.GetInterval(semitoneCount);

            _synth.Speak($"{spokenInterval}; {NumberUtilities.GetSpokenDirection(NumberUtilities.GetDirection(interval))}");
            Thread.Sleep(delayInSeconds * 1000);
        }

        private void PlayAndDelay(IReadOnlyList<int> interval, int delayInSeconds) {
            PlayNote(_notes.GetNoteFromIndex(interval[0]));
            PlayNote(_notes.GetNoteFromIndex(interval[1]));
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