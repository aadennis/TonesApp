using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StringUtilities.Test {
    [TestClass]
    public class FileNameBuilding {
        private string path = @"c:\temp";

        [TestMethod]
        public void StringWithSpacesAndMixedCaseGoesToPascalCaseWithSuffix() {
            var phrase = StringUtility.PascalCaseWithSuffix(path, "Perfect fourth");
            Assert.AreEqual(@"c:\temp\PerfectFourth.wav", phrase);

            phrase = StringUtility.PascalCaseWithSuffix(path, "Unison and her Friends minor and major");
            Assert.AreEqual(@"c:\temp\UnisonAndHerFriendsMinorAndMajor.wav", phrase);
        }
    }
}

