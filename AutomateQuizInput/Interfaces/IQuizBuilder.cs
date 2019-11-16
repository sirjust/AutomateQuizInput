using System.Collections.Generic;

namespace AutomateQuizInput
{
    public interface IQuizBuilder
    {
        List<Question> GenerateQuestions(List<string> quizDataList);
        IEnumerable<IEnumerable<string>> SeparateQuizzes(IEnumerable<string> rawLines);
    }
}