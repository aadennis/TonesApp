using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StringUtilities.Test {
    [TestClass]
    public class FileNameBuilding {
        private string path = @"c:\temp";

        [TestMethod]
        public void FullFilepathNameCanBeMadeFromParts() {
            var phrase = StringUtility.MakeFileName(path, "Perfect fourth", "wav");
            Assert.AreEqual(@"c:\temp\PerfectFourth.wav", phrase);

            phrase = StringUtility.MakeFileName(path, "Unison and her Friends minor and major", "wav");
            Assert.AreEqual(@"c:\temp\UnisonAndHerFriendsMinorAndMajor.wav", phrase);

            phrase = StringUtility.MakeFileName(path, "F#3", "wav");
            Assert.AreEqual(@"c:\temp\F#3.wav", phrase);

            phrase = StringUtility.MakeFileName(path, "Perfect foUrth", "afileext", "PutInFront");
            Assert.AreEqual(@"c:\temp\PutInFrontPerfectFourth.afileext", phrase);
        }
    }
}

