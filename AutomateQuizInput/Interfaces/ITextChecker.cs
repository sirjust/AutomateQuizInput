using System.Collections.Generic;

namespace AutomateQuizInput
{
    public interface ITextChecker
    {
        string ChangeApostrophesToTicks(string text);
        string ChangeDashesToUnderscores(string text);
        string CleanOutFractionSymbols(string text);
        string CleanOutSmartQuotes(string text);
        IEnumerable<string> FindAndReplaceInvalidCharacters(IEnumerable<string> lines);
        string StraightenCurlyQuotes(string text);
        bool TextHasApostrophes(string text);
        bool TextHasCurlyQuotes(string text);
        bool TextHasDashes(string text);
    }
}