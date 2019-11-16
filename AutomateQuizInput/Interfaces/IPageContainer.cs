using System.Collections.Generic;

namespace AutomateQuizInput
{
    public interface IPageContainer
    {
        int FailPageNumber { get; set; }
        int QuizNumber { get; set; }
        int QuizPageNumber { get; set; }
        int SuccessPageNumber { get; set; }

        IEnumerable<PageContainer> GetPages(IEnumerable<string> rawLines, int numberOfQuizzes);
        void InsertPages(List<Quiz> quizzes, IList<PageContainer> pageObjects);
    }
}