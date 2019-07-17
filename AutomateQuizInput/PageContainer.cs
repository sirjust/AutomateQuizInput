using System;
using System.Collections.Generic;
using System.Linq;

namespace AutomateQuizInput
{
    public class PageContainer
    {
        public int QuizNumber { get; set; } = default;
        public int QuizPageNumber { get; set; }
        public int SuccessPageNumber { get; set; }
        public int FailPageNumber { get; set; }

        public IEnumerable<PageContainer> GetPages(IEnumerable<string> rawLines, int numberOfQuizzes)
        {
            var quizCount = rawLines.Count(x => x.Contains("Quiz"));
            List<PageContainer> pages = new List<PageContainer>();
            if(numberOfQuizzes != quizCount)
            {
                throw new Exception();
            }
            List<IEnumerable<string>> separatedQuizzes = Helper.SeparateQuizzes(rawLines).ToList();
            foreach(var l in separatedQuizzes)
            {
                var myObject = l.ToList();
                var pageObject = new PageContainer
                {
                    QuizPageNumber = Convert.ToInt32(myObject[1]),
                    SuccessPageNumber = Convert.ToInt32(myObject[2]),
                    FailPageNumber = Convert.ToInt32(myObject[3])
                };
                pages.Add(pageObject);
            }
            return pages;
        }

        public void InsertPages(List<Quiz> quizzes, IEnumerable<PageContainer> pageObjects)
        {
            var pageList = pageObjects.ToList();
            if(quizzes.Count != pageList.Count)
            {
                throw new Exception();
            }
            for (int i=0; i < quizzes.Count(); i++)
            {
                quizzes[i].CoursePage = pageList[i].QuizPageNumber;
                quizzes[i].PassPage = pageList[i].SuccessPageNumber;
                quizzes[i].FailPage = pageList[i].FailPageNumber;
            }
        }
    }
}