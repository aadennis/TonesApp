using System;
using System.Collections.Generic;
using System.Linq;

namespace NotesApp {

    public static class NumberUtilities {

        /// <summary>
        /// Enumerates whether a range request is ascending, descending or
        /// randomly assigned by this class.
        /// </summary>
        public enum Direction {
            Ascending = 0,
            Descending,
            Random
        }

        /// <summary>
        ///  Return a pair of random integers, where the first is always lower than
        ///  the second (or can be equal to the second, for unison), and there is not more than maxDistance 
        ///  between the returned integers.   Keeping this interval close is because the main usecase is a musical interval test
        ///  where a large distance would not help with aural training.
        ///  It helps if the Random passed in has been used in previous calls, to help entropy.
        /// 
        ///  Note that Random.Next returns an integer that is LESS than the upper limit argument,
        ///  whereas the returned integer is > OR EQUAL TO the lower limit argument.
        /// https://msdn.microsoft.com/en-us/library/2dx6wyd4(v=vs.110).aspx
        /// 
        /// </summary>
        public static List<int> GetRandomInterval(int lowerLimit, int upperLimit, int maxDistance, Random rand, Direction directionRequested = 0) {
            var direction = directionRequested.Equals(Direction.Random) ? GetRandomDirection() : (int)directionRequested;
            if (rand == null) {
                throw new ArgumentNullException(nameof(rand));
            }

            if (lowerLimit.Equals(upperLimit)) {
                return SortArrayBasedOnDirection(new List<int> { lowerLimit, upperLimit }, (Direction)direction);
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
            return SortArrayBasedOnDirection(lowerAndUpperLimit, (Direction)direction);
        }

        /// <summary>
        /// Given the interval with 2 boundaries, determine and return the direction.
        /// Note that if the 2 are equal then Ascending is returned. This means that Unison
        /// will be spoken as Ascending in the Musical Notes context.
        /// </summary>
        /// <param name="sortedInterval"></param>
        /// <returns></returns>
        public static Direction GetDirection(List<int> sortedInterval) {
            return sortedInterval[1] >= sortedInterval[0] ? Direction.Ascending : Direction.Descending;
        }

        private static int GetRandomDirection() {
            return RandomDirection.Next(0, 2);
        }

        private static List<int> SortArrayBasedOnDirection(IEnumerable<int> arrayToBeSorted, Direction direction) {
            if (direction != Direction.Ascending && direction != Direction.Descending) {
                throw new ArgumentException($"Direction {direction} is invalid");
            }
            return direction == Direction.Ascending ? arrayToBeSorted.OrderBy(i => i).ToList() :
                arrayToBeSorted.OrderByDescending(i => i).ToList();
        }

        private static readonly Random RandomDirection = new Random();
    }
}

