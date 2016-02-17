using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Speech.Synthesis;
using System.Threading;

namespace NotesApp {

    /// <summary>
    /// 
    /// </summary>
    public class NoteUtility {



        private const int Duration = 1000;
        private static int _samples = 441 * Duration / 10;
        private static readonly int Bytes = _samples * 4;

        private readonly MusicalNotes _notes = new MusicalNotes();
        private readonly SpeechSynthesizer _synth = new SpeechSynthesizer();
        private readonly IToneProvider _toneProvider;

        public NoteUtility(IToneProvider toneProvider) {
            _toneProvider = toneProvider;
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
            _toneProvider.PlayTone(note.Frequency);
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

        public List<MusicalNote> GetAllNotes() {
            return _notes.GetAllNotes();
        }


    }
}