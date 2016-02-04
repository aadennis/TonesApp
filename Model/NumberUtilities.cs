using System;
using System.Collections.Generic;

namespace NotesApp {

    public static class NumberUtilities {

        /// <summary>
        ///  Return a pair of random integers, where the first is always lower than
        ///  the second (or can be equal to the second, for unison), and there is not more than maxDistance 
        /// between the returned integers.   Keeping this interval close is because the main usecase is a musical interval test
        ///  where a large distance would not help with aural training.
        ///  It helps if the Random passed in has been used in previous calls, to help entropy.
        ///  Note that Random.Next returns an integer that is LESS than the upper limit argument,
        ///  whereas the returned integer is > OR EQUAL TO the lower limit argument.
        /// https://msdn.microsoft.com/en-us/library/2dx6wyd4(v=vs.110).aspx
        /// 
        /// </summary>
        public static List<int> GetRandomInterval(int lowerLimit, int upperLimit, int maxDistance, Random rand) {
            if (rand == null) {
                throw new ArgumentNullException(nameof(rand));
            }
            if (lowerLimit.Equals(upperLimit)) {
                return new List<int> { lowerLimit, upperLimit };
            }

            const int maxIterations = 100;
            var lowerAndUpperLimit = new List<int> { rand.Next(lowerLimit, upperLimit) };
            var nextNote = rand.Next(lowerLimit, upperLimit);
            var count = 0;
            while (Math.Abs(lowerAndUpperLimit[0] - nextNote) > maxDistance) {
                if (count++ > maxIterations) {
                    throw new Exception("Too many iterations");
                }
                nextNote = rand.Next(lowerLimit, upperLimit + 1);
            }
            lowerAndUpperLimit.Add(nextNote);
            lowerAndUpperLimit.Sort();
            return lowerAndUpperLimit;
        }
    }
}

