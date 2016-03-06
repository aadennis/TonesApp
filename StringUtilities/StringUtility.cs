using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringUtilities
{
    public static class StringUtility {

        public static string PascalCaseWithSuffix(string path, string stringToConvert) {

            var WordsInString = stringToConvert.Split(' ').ToArray();
            var temp = "";
            foreach (var word in WordsInString) {
                temp += word.First().ToString().ToUpper() + word.Substring(1);
            }


           return $"{path}{@"\"}{temp}.wav";
        }
    }
}
