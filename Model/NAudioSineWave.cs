using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio;
using NAudio.Wave;

namespace NotesApp {

    //http://mark-dot-net.blogspot.co.uk/2009/10/playback-of-sine-wave-in-naudio.html
    public class NAudioSineWave : IToneProvider {

        private SineWaveProvider32 _sineWave;
        private WaveOut _waveOut;


        public NAudioSineWave() {
            _sineWave = new SineWaveProvider32();
            _sineWave.SetWaveFormat(44100, 1);
            _sineWave.Amplitude = 10.25f;
            
        }

        public void PlayTone(int frequency, double duration = 1.000000001) {

            using (_waveOut = new WaveOut()) {
                _waveOut.DeviceNumber = 0;
                _waveOut.Init(_sineWave);
                _waveOut.Play();
               System.Threading.Thread.Sleep(5000);
            }
        }

    }
}
