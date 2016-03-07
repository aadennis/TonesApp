using System;
using System.Speech.Synthesis;
using System.Linq;

namespace Speaking
{
    /// <summary>
    /// Wrapper to the Microsoft SpeechSynthesizer class, restricted to methods etc
    /// of interest in my apps.
    /// </summary>
    public class Speech : TempSpeech  {
        private readonly SpeechSynthesizer _speechSynthesizer;
        //private const string PreferredVoice = "IVONA 2 Brian OEM";
        private const string PreferredVoice = "Microsoft Hazel Desktop";

        public Speech() {
            _speechSynthesizer = new SpeechSynthesizer();
            _speechSynthesizer.SetOutputToDefaultAudioDevice();
            SetVoice();
        }

        public override void Speak(string stringToSpeak) {
            _speechSynthesizer.Speak(stringToSpeak);
        }

        public void SpeakWithPromptBuilder() {
            var builder = new PromptBuilder();
            builder.AppendText("This is something of a test");
            builder.AppendAudio(@"E:\OneDrive\Music\mycomp\MusicalIntervals01\ExtractedPianoNotes\F#5.wav");
            _speechSynthesizer.Speak(builder);

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
