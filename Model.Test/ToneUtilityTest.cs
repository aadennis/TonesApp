// <copyright file="ToneUtilityTest.cs">Copyright ©  2016</copyright>
using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NotesApp;

namespace NotesApp.Tests
{
    /// <summary>This class contains parameterized unit tests for ToneUtility</summary>
    [PexClass(typeof(ToneUtility))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestClass]
    public partial class ToneUtilityTest
    {
        /// <summary>Test stub for PlayNote(Int32, String)</summary>
        [PexMethod]
        public void PlayNoteTest(
            [PexAssumeUnderTest]ToneUtility target,
            int frequency,
            string noteDescription
        )
        {
            target.PlayNote(frequency, noteDescription);
            // TODO: add assertions to method ToneUtilityTest.PlayNoteTest(ToneUtility, Int32, String)
        }
    }
}
