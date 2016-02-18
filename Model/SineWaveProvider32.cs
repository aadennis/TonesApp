using System;
using NAudio.Wave;

namespace NotesApp {


    //http://mark-dot-net.blogspot.co.uk/2009/10/playback-of-sine-wave-in-naudio.html
    public class SineWaveProvider32 : WaveProvider32 {
        private int _sample;

        public SineWaveProvider32() {
            Frequency = 200;
            Amplitude = 0.25f; // let's not hurt our ears            
        }

        public SineWaveProvider32(int s, int c) : base(s,c) {
            Frequency = 2000;
            Amplitude = 0.25f; // let's not hurt our ears            
        }

        public float Frequency { get; set; }
        public float Amplitude { get; set; }

        public override int Read(float[] buffer, int offset, int sampleCount) {
            int sampleRate = WaveFormat.SampleRate;
            for (int n = 0; n < sampleCount; n++) {
                buffer[n + offset] = (float)(Amplitude * Math.Sin((2 * Math.PI * _sample * Frequency) / sampleRate));
                _sample++;
                if (_sample >= sampleRate) _sample = 0;
            }
            return sampleCount;
        }

    }
}