using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringUtilities
{
    // String utilities. These contain no knowledge about their caller's domain... of course.
    public static class StringUtility {


        public static string PascalCaseWithSuffix(string path, string stringToConvert) {

            var wordsInString = stringToConvert.Split(' ').ToArray();
            var temp = "";
            foreach (var word in wordsInString) {
                temp += word.First().ToString().ToUpper() + word.Substring(1);
            }

            return $"{path}{@"\"}{temp}.wav";
        }
    }
}
