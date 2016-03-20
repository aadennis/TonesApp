using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StringUtilities
{
    // String utilities. These contain no knowledge about their caller's domain... of course.
    public static class StringUtility {
        /// <summary>
        /// Given a path, a file name, an optional prefix (e.g. "Emm"), and optional suffix (e.g.
        /// "Desc" and mandatory filename extension, return an absolute path to a file.
        /// For example: path: c:\temp, filename: "This is a test", extension: "wav", prefix: "Emm", suffix: "Desc" =>
        /// c:\temp\EmmThisIsATestDesc.wav
        /// </summary>
        /// <param name="path">path excluding the file name</param>
        /// <param name="fileName">file name (no path)</param>
        /// <param name="extension">file extension excluding the .</param>
        /// <param name="prefix">optional prefix to apply</param>
        /// <param name="suffix">optional suffix to apply</param>
        /// <returns></returns>
        public static string MakeFileName(string path, string fileName, string extension, string prefix = "", string suffix = "") {

            var wordsInString = fileName.Split(' ').ToArray();
            var temp = "";
            foreach (var word in wordsInString) {
                temp += word.First().ToString().ToUpper() + word.Substring(1).ToLower();
            }

            return $"{path}{@"\"}{prefix}{temp}{suffix}.{extension}";
        }
    }
}
