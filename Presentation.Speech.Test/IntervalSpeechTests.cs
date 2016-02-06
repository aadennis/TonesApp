using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Speech.Synthesis;

namespace Presentation.Speech.Test {
    [TestClass]
    public class IntervalSpeechTests {
        [TestMethod]
        [TestCategory("SoundTest")]
        public void VoiceCanBeHeard() {
            var synth = new SpeechSynthesizer();
            synth.Speak("This is an example of what to do");
        }
    }
}
