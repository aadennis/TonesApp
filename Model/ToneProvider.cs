using System;
using System.Collections.Generic;
using System.IO;
using System.Media;

namespace NotesApp {

    /// <summary>
    /// An implementation of IToneProvider, emitting a tone as an in-memory wav file
    /// </summary>
    public class ToneProvider : IToneProvider {
        private readonly double _amplitude; // = (double) Math.Pow(2, 15) - 1;
        private const int Duration = 1000;
        private const int CdQuality = 441;
        // It is evidently the samples that dictate the duration... which is confusing.
        private static readonly int _samples = 44100;
        private static readonly int Bytes = _samples * 4;
        private static readonly List<int> WaveHeaderFileHeader =
             new List<int> { 0X46464952, 36 + Bytes, 0X45564157, 0X20746D66, 16, 0X20001, 44100, 176400, 0X100004, 0X61746164, Bytes };


        public ToneProvider(float amplitude) {
            _amplitude = amplitude*32767;
        }

        public void PlayTone(int frequency, double duration) {
            PlaySineWave(frequency, duration * 1000);
        }

        /// <summary>
        ///  This is all a bit of a mess. todo refactor
        /// </summary>
        /// <param name="frequency">frequency in hz</param>
        /// <param name="durationInMs">duration in ms</param>
        private void PlaySineWave(int frequency, double durationInMs) {

            var deltaFt = 2*Math.PI*frequency/44100.0;

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
    }
}
