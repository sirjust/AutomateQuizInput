using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomateQuizInput
{
    public class PageContainer : IPageContainer
    {
        IQuizBuilder _builder;
        public int QuizNumber { get; set; } = default;
        public int QuizPageNumber { get; set; }
        public int SuccessPageNumber { get; set; }
        public int FailPageNumber { get; set; }

        public PageContainer(IQuizBuilder builder)
        {
            _builder = builder;
        }

        public IEnumerable<PageContainer> GetPages(IEnumerable<string> rawLines, int numberOfQuizzes)
        {
            var quizCount = rawLines.Count(x => x.Contains("Quiz"));
            List<PageContainer> pages = new List<PageContainer>();
            if (numberOfQuizzes != quizCount)
            {
                throw new ArgumentException("The number of quizzes in Quizzes.txt and PageInfo.txt is different.");
            }
            List<IEnumerable<string>> separatedQuizzes = _builder.SeparateQuizzes(rawLines).ToList();
            foreach (var list in separatedQuizzes)
            {
                if (list.Count() < 4)
                {
                    throw new ArgumentException($"This {list} doesn't have the correct number of pages. It needs a QuizPage, a SuccessPage, and a FailPage.");
                }
                if (list.Count() > 5)
                {
                    throw new ArgumentException($"This {list} doesn't have the correct number of pages. It needs a QuizPage, a SuccessPage, and a FailPage.");
                }
                var myObject = list.ToList();
                bool pageOk = int.TryParse(myObject[1], out int pageNumber);
                bool successOk = int.TryParse(myObject[2], out int successPage);
                bool failOk = int.TryParse(myObject[3], out int failPage);
                if (!pageOk || !successOk || !failOk)
                {
                    throw new ArgumentException("One of the page values is not an integer.");
                }
                var pageObject = new PageContainer(_builder)
                {
                    QuizPageNumber = pageNumber,
                    SuccessPageNumber = successPage,
                    FailPageNumber = failPage
                };
                pages.Add(pageObject);
            }
            return pages;
        }

        public void InsertPages(List<Quiz> quizzes, IList<PageContainer> pageObjects)
        {
            if (quizzes.Count != pageObjects.Count)
            {
                throw new ArgumentException("The number of quizzes in Quizzes.txt and PageInfo.txt is different.");
            }

            for (int i = 0; i < quizzes.Count(); i++)
            {
                quizzes[i].CoursePage = pageObjects[i].QuizPageNumber;
                quizzes[i].PassPage = pageObjects[i].SuccessPageNumber;
                quizzes[i].FailPage = pageObjects[i].FailPageNumber;
            }
        }
    }
}