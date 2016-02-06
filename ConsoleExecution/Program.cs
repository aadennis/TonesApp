using NotesApp;
using System;
using System.Speech.Synthesis;
using System.Threading;

namespace ConsoleExecution {

    class Program {
        public static void Main() {
            ToneGenerator toneGenerator = new ToneGenerator();
            toneGenerator.PlayAllNotes();
        }
    }

    // Full credit to this person: https://social.msdn.microsoft.com/profile/johnwein/
    // All I have done is to refactor, mostly in an effort to run it inside coreclr and VsCode. That failed, because
    // ultimately coreclr knows nothing about anything related to System.Media. Neither does dnx451, but at least the latter
    // lets you address an assembly in the GAC, using this in project.json:
    //  "frameworkAssemblies": {
    //          "System.Media": ""
    //      },


    public class ToneGenerator {

        private readonly SpeechSynthesizer _synth = new SpeechSynthesizer();
        private Random _random = new Random();

        public void PlayAllNotes() {

            const int totalIterations = 1000;
            const int delayInSecondsBetweenAudioSnippets = 5;
            var notes = new MusicalNotes();
            var setOfTones = new ToneUtility();
            var frequencies = notes.GetAllFrequencies();

            for (var i = 0; i < totalIterations; i++) {
                var intervalBoundaries = NumberUtilities.GetRandomInterval(0, 24, 12, _random);
                var frequency0 = frequencies[intervalBoundaries[0]];
                var frequency1 = frequencies[intervalBoundaries[1]];

                setOfTones.PlayNote(notes.GetNoteFromIndex(intervalBoundaries[0]));
                setOfTones.PlayNote(notes.GetNoteFromIndex(intervalBoundaries[1]));
                Console.WriteLine(frequency0);
                Console.WriteLine(frequency1);
                Console.WriteLine(frequency1 - frequency0);

                Thread.Sleep(delayInSecondsBetweenAudioSnippets * 1000);
                setOfTones.PlayNote(notes.GetNoteFromIndex(intervalBoundaries[0]));
                setOfTones.PlayNote(notes.GetNoteFromIndex(intervalBoundaries[1]));
                Thread.Sleep(delayInSecondsBetweenAudioSnippets * 1000);

                var semitoneCount = intervalBoundaries[1] - intervalBoundaries[0];
                var spokenInterval = Intervals.GetInterval(semitoneCount);

                _synth.Speak(spokenInterval);
                Thread.Sleep(delayInSecondsBetweenAudioSnippets * 1000);
            }
        }
    }
}