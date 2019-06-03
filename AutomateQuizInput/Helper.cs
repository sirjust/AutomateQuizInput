using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateQuizInput
{
    public static class Helper
    {
        public static IEnumerable<string> ReadDocument(string path)
        {
            var allLines = File.ReadAllLines(path);
            return allLines;
        }
        public static List<Quiz> GetQuizzes()
        {
            return new List<Quiz>();
        }
    }
}
