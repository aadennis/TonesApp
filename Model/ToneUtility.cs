using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;

namespace NotesApp {

    public class ToneUtility {

        private readonly int _bytes;
        private readonly List<int> _waveHeaderFileHeader;
        private readonly double _amplitude = Math.Pow(2, 15) - 1;
        private readonly int _duration = 1000;
        private readonly int _samples;
        private readonly MusicalNotes _notes = new MusicalNotes();

        public ToneUtility() {
            _samples = 441 * _duration / 10;
            _bytes = _samples * 4;
            _waveHeaderFileHeader = new List<int> { 0X46464952, 36 + _bytes, 0X45564157, 0X20746D66, 16, 0X20001, 44100, 176400, 0X100004, 0X61746164, _bytes };
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
            var deltaFt = 2 * Math.PI * note.Frequency / 44100.0;

            using (var mStream = new MemoryStream(44 + _bytes)) {
                using (var bWriter = new BinaryWriter(mStream)) {
                    foreach (var headerElement in _waveHeaderFileHeader) {
                        bWriter.Write(headerElement);
                    }
                    for (var t = 0; t < _samples; t++) {
                        var sample = Convert.ToInt16(_amplitude * Math.Sin(deltaFt * t));
                        bWriter.Write(sample);
                        bWriter.Write(sample);
                    }
                    bWriter.Flush();
                    mStream.Seek(0, SeekOrigin.Begin);
                    using (var ss = new SoundPlayer(mStream)) {
                        ss.PlaySync();
                    }
                }
            }

        }

        public List<MusicalNote> GetAllNotes() {
            return _notes.GetAllNotes();
        }


    }
}