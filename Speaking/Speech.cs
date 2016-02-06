using System.Speech.Synthesis;

namespace Speaking
{
    /// <summary>
    /// Wrapper to the Microsoft SpeechSynthesizer class, restricted to methods etc
    /// of interest in my apps.
    /// </summary>
    public class Speech  {
        private readonly SpeechSynthesizer _speechSynthesizer;

        public Speech() {
            _speechSynthesizer = new SpeechSynthesizer();
            //var x = _speechSynthesizer.GetInstalledVoices();
            _speechSynthesizer.SelectVoice("Microsoft David Desktop");

        }

        public void Speak(string stringToSpeak) {
            _speechSynthesizer.Speak(stringToSpeak);
        }
    }

}
