using System;
using System.Collections.Generic;
using System.Linq;

namespace NotesApp {
    public static class Intervals {

        private static readonly List<Interval> IntervalSet = new List<Interval> {
            new Interval {SemiToneCount = 0, Description = "Unison"},
            new Interval {SemiToneCount = 1, Description = "Minor second"},
            new Interval {SemiToneCount = 2, Description = "Major second"},
            new Interval {SemiToneCount = 3, Description = "Minor third"},
            new Interval {SemiToneCount = 4, Description = "Minor third"},
            new Interval {SemiToneCount = 5, Description = "Perfect fourth"},
            new Interval {SemiToneCount = 6, Description = "Tritone"},
            new Interval {SemiToneCount = 7, Description = "Perfect fifth"},
            new Interval {SemiToneCount = 8, Description = "Minor sixth"},
            new Interval {SemiToneCount = 9, Description = "Major sixth"},
            new Interval {SemiToneCount = 10, Description = "Minor seventh"},
            new Interval {SemiToneCount = 11, Description = "Major seventh"},
            new Interval {SemiToneCount = 12, Description = "Perfect octave"}
        };

        public static List<Interval> GetAllIntervals() {
            return IntervalSet;
        }

        public static string GetInterval(int semiToneCount) {
            if (semiToneCount < 0 || semiToneCount > 12) {
                throw new ArgumentException($"Requested semi-tone count of [{semiToneCount}] is outside the allowed range");
            }
            return IntervalSet.Single(interval => interval.SemiToneCount.Equals(semiToneCount)).Description;
        }
    }
}
