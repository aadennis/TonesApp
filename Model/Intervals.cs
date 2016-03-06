﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Synthesis;

namespace NotesApp {

    /// <summary>
    /// The collection of all intervals that can be used in an Equal-Tempered scale.
    /// There is a total of 13: the terms "Augmented fourth", "Diminished fifth", and "Tritone"
    /// are all covered by the term "Augmented fourth".
    /// </summary>
    public static class Intervals {

        private static readonly List<Interval> IntervalSet = new List<Interval> {
            new Interval {SemiToneCount = 0, Description = "Unison"},
            new Interval {SemiToneCount = 1, Description = "Minor second"},
            new Interval {SemiToneCount = 2, Description = "Major second"},
            new Interval {SemiToneCount = 3, Description = "Minor third"},
            new Interval {SemiToneCount = 4, Description = "Major third"},
            new Interval {SemiToneCount = 5, Description = "Perfect fourth"},
            new Interval {SemiToneCount = 6, Description = "Augmented fourth"},
            new Interval {SemiToneCount = 7, Description = "Perfect fifth"},
            new Interval {SemiToneCount = 8, Description = "Minor sixth"},
            new Interval {SemiToneCount = 9, Description = "Major sixth"},
            new Interval {SemiToneCount = 10, Description = "Minor seventh"},
            new Interval {SemiToneCount = 11, Description = "Major seventh"},
            new Interval {SemiToneCount = 12, Description = "Perfect octave"}
        };

        private static string GetAudioFolder()
        {
            return @"c:\temp";
        }

        /// <summary>
        /// Gets the collection of all intervals
        /// </summary>
        /// <returns>the set of intervals</returns>
        public static List<Interval> GetAllIntervals() {
            return IntervalSet;
        }

        /// <summary>
        /// Given an key expressed as a count of inclusive semitones, return the interval matching that 
        /// count. An exception is thrown if the requested key is outside the supported range.
        /// </summary>
        /// <param name="semiToneCount"></param>
        /// <param name="isAudio">If this is true, then the returned string is the location of the audio file,
        /// e.g. "{wav folder}/MinorSeventh.wav" </param>
        /// <returns>The description or audio location of the requested interval. For example "Minor seventh".</returns>
        public static string GetInterval(int semiToneCount, bool isAudio = false) {
            var absSemitoneCount = Math.Abs(semiToneCount);
            if (absSemitoneCount > 12) {
                throw new ArgumentException($"Requested semi-tone count of [{semiToneCount}] is outside the allowed range");
            }
            var xxx = IntervalSet.Single(interval => interval.SemiToneCount.Equals(absSemitoneCount)).Description;
            if (!isAudio)
            {
                return xxx;
            }
            return StringUtilities.StringUtility.PascalCaseWithSuffix(GetAudioFolder(), xxx);
        }
    }
}
