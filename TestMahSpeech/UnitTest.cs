using System;
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
using SpeechAssets;

namespace TestMahSpeech
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var x2= new MahSpeech();
            Assert.AreEqual(1,2);
            var rr = 12;
        }
    }
}
