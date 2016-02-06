using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Model.Test {

    public class ModelTestBase {

        public ModelTestBase() {
            TestInitialize();
        }

        //available from .Net 4.5...
        protected void TraceExecutingMethod([CallerMemberName] string caller = null) {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("[{0:H:mm:ss.fff}]: Executing test [{1}]", DateTime.UtcNow, caller);
        }

        protected static void ShowDictionary(int lowerLimit, SortedDictionary<int, int> tallyOfFoundNumbers, string prefixMessage) {
            for (var i = lowerLimit; i <= tallyOfFoundNumbers.Count; i++) {
                //Debug.WriteLine($"[{i}]{prefixMessage}: {tallyOfFoundNumbers[i]}");
            }
        }

        protected void TestInitialize() {
            Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Debug.AutoFlush = true;
            Rand = new Random();
        }

        protected Random Rand;
    }
}
