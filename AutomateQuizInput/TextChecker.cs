using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AutomateQuizInput
{
    public class TextChecker : ITextChecker
    {

        public bool TextHasApostrophes(string text) => text.Contains("'");

        public string ChangeApostrophesToTicks(string text) => text.Replace("'", "`");
        public bool TextHasCurlyQuotes(string text) => (text.Contains("“") || text.Contains("”"));

        public string StraightenCurlyQuotes(string text) => text.Replace("“", "\"").Replace("”", "\"");

        public bool TextHasDashes(string text) => text.Contains("-");

        public string ChangeDashesToUnderscores(string text) => text.Replace("-", "_");

        public string CleanOutSmartQuotes(string text)
        {
            if (text.Contains('\u2018') || text.Contains('\u2019') || text.Contains('\u201c') || text.Contains('\u201d'))
            {
                Console.WriteLine("Text has smart quote");
                return text.Replace('\u2018', '`').Replace('\u2019', '`').Replace('\u201c', '\"').Replace('\u201d', '\"');
            }
            return text;
        }

        public string CleanOutFractionSymbols(string text)
        {
            var quarter = "1/4";
            var half = "1/2";
            var threeQuarters = "3/4";
            StringBuilder builder = new StringBuilder(text);

            builder.Replace("¼", quarter).Replace("½", half).Replace("¾", threeQuarters);
            text = builder.ToString();
            return text;
        }

        public IEnumerable<string> FindAndReplaceInvalidCharacters(IEnumerable<string> lines)
        {
            var lineList = lines.ToList();
            for (int i = 0; i < lines.Count(); i++)
            {
                if (TextHasApostrophes(lineList[i]))
                {
                    Console.WriteLine("The text has an apostrophe. Updating...");
                    lineList[i] = ChangeApostrophesToTicks(lineList[i]);
                }

                if (TextHasDashes(lineList[i]))
                {
                    Console.WriteLine("The text has a dash. Updating...");
                    lineList[i] = ChangeDashesToUnderscores(lineList[i]);
                }

                if (TextHasCurlyQuotes(lineList[i]))
                {
                    Console.WriteLine("The text has curly quotation marks. Updating...");
                    lineList[i] = StraightenCurlyQuotes(lineList[i]);
                    lineList[i] = CleanOutSmartQuotes(lineList[i]);
                }
                lineList[i] = CleanOutFractionSymbols(lineList[i]);
            }
            return lineList;
        }
    }
}
