using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Xaml.Controls;

namespace SpeechAssets {
    public class MahSpeech {
        private SpeechSynthesizer _speech;

        public MahSpeech() {
            _speech = new SpeechSynthesizer();
            var voices = SpeechSynthesizer.AllVoices;
            _speech.Voice = voices.FirstOrDefault();

            SpeakTheText("this is a wild test");
            var x = 22;
            
        }

        public async void SpeakTheText(string textToSpeak) {
            var stream = await _speech.SynthesizeTextToStreamAsync(textToSpeak);
            var media = new MediaElement();
            media.SetSource(stream, stream.ContentType);
            media.Play();
        }

    }
}
