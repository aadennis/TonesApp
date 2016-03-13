using System;
using System.Speech.Synthesis;
using NAudio.Wave;

namespace NotesApp {

    //http://mark-dot-net.blogspot.co.uk/2009/10/playback-of-sine-wave-in-naudio.html
    public class NAudioSineWave : IToneProvider {

        private readonly SineWaveProvider32 _sineWave;
        private static WaveOut _waveOut;

        public NAudioSineWave(float amplitude) {
            _sineWave = new SineWaveProvider32 {Amplitude = amplitude};
        }

        public void PlayTone(int frequency, double duration) {
            _sineWave.Frequency = frequency;
            ;
            _sineWave.SetWaveFormat(44100, 1);
            using (_waveOut = new WaveOut()) {
                _waveOut.DeviceNumber = 0;
                _waveOut.Init(_sineWave);
                _waveOut.Play();
                System.Threading.Thread.Sleep(2000);
                _waveOut.Stop();
                _waveOut = null;
            }
        }

        public void PlayAudio(string noteName) {
            throw new NotImplementedException();
        }

        void IToneProvider.PlayAudio(string noteName) {
           var p = new PromptBuilder();
            p.AppendAudio(noteName);
            var s = new SpeechSynthesizer();
            s.Speak(p);
        }
    }
}
