using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;

namespace NotesApp {

    public class ToneUtility {

       
        private readonly double _amplitude = Math.Pow(2, 15) - 1;
        private const int Duration = 1000;
        private static int _samples = 441 * Duration / 10;
        private static readonly int Bytes = _samples * 4;
        private static readonly List<int> WaveHeaderFileHeader =
             new List<int> { 0X46464952, 36 + Bytes, 0X45564157, 0X20746D66, 16, 0X20001, 44100, 176400, 0X100004, 0X61746164, Bytes };
        private readonly MusicalNotes _notes = new MusicalNotes();

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
            var deltaFt = 2 * Math.PI * note.Frequency / 44100.0;

            using (var mStream = new MemoryStream(44 + Bytes)) {
                using (var bWriter = new BinaryWriter(mStream)) {
                    foreach (var headerElement in WaveHeaderFileHeader) {
                        bWriter.Write(headerElement);
                    }
                    for (var t = 0; t < _samples; t++) {
                        var sample = Convert.ToInt16(_amplitude * Math.Sin(deltaFt * t));
                        bWriter.Write(sample);
                        bWriter.Write(sample);
                    }
                    bWriter.Flush();
                    mStream.Seek(0, SeekOrigin.Begin);
                    PlaySound(mStream);
                }
            }

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