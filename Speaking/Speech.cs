using System.Speech.Synthesis;
using System.Linq;

namespace Speaking
{
    /// <summary>
    /// Wrapper to the Microsoft SpeechSynthesizer class, restricted to methods etc
    /// of interest in my apps.
    /// </summary>
    public class Speech  {
        private readonly SpeechSynthesizer _speechSynthesizer;
        //private const string PreferredVoice = "IVONA 2 Brian OEM";
        private const string PreferredVoice = "Microsoft Hazel Desktop";

        public Speech() {
            _speechSynthesizer = new SpeechSynthesizer();
            SetVoice();
        }

        public void Speak(string stringToSpeak) {
            _speechSynthesizer.Speak(stringToSpeak);
        }

        private void SetVoice() {
            var availableVoices = _speechSynthesizer.GetInstalledVoices();
            var voice = availableVoices.SingleOrDefault(x => x.VoiceInfo.Name.Equals(PreferredVoice));
            
            if (voice != null) {
                _speechSynthesizer.SelectVoice(PreferredVoice);
            } else {
                _speechSynthesizer.SelectVoiceByHints(VoiceGender.Male);
            }
        }
    }

}
