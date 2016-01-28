﻿using System;
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
        private readonly Dictionary<int, string> _notes = new Dictionary<int, string> {
            {131,"C.3"}, {139,"C#.3"}, {147,"D.3"}, {156,"D#.3"}, {165,"E.3"}, {175,"F.3"},
            {185,"F#.3"}, {196,"G.3"}, {208,"G#.3"}, {220,"A.3"}, {233,"A#.3"}, {247,"B.3"},
            {262,"C.4"}, {277,"C#.4"}, {294,"D.4"}, {311,"D#.4"}, {330,"E.4"}, {349,"F.4"},
            {370,"F#.4"}, {392,"G.4"}, {415,"G#.4"}, {440,"A.4"}, {466,"A#.4"}, {494,"B.4"}
        };

        public ToneUtility() {
            _samples = 441 * _duration / 10;
            _bytes = _samples * 4;
            _waveHeaderFileHeader = new List<int> { 0X46464952, 36 + _bytes, 0X45564157, 0X20746D66, 16, 0X20001, 44100, 176400, 0X100004, 0X61746164, _bytes };
        }

        public Dictionary<int, string> GetAllNotes() {
            return _notes;
        }

        public string GetNoteElements(int frequency) {
            var found = GetAllNotes().Any(n => n.Key.Equals(frequency));
            if (!found) {
                throw new ArgumentException(
                    $"[The frequency [{frequency}] was not found in the set of available notes]");
            }
            return GetAllNotes().Single(n => n.Key.Equals(frequency)).Value;
        }

        public void PlayNote(int frequency, string noteDescription) {
            Console.WriteLine("[{0}][{1}]", frequency, noteDescription);
            var deltaFt = 2 * Math.PI * frequency / 44100.0;

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


    }
}