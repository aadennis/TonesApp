using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Speech.Synthesis;
using NotesApp;

namespace Presentation.Speech.Test {
    [TestClass]
    public class IntervalSpeechTests {
        [TestMethod]
        [TestCategory("SoundTest")]
        public void VoiceCanBeHeard() {
            var synth = new SpeechSynthesizer();
            synth.SelectVoice("Microsoft Hazel Desktop");
            synth.Speak("This is an example of what to do");
            synth.Speak(NumberUtilities.Direction.Ascending.ToString());

        }


    }
}
