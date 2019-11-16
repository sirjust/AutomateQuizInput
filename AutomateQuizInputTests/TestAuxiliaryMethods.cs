using AutomateQuizInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateQuizInputTests
{
    public static class TestAuxiliaryMethods
    {
        public static List<string> GetMockQuizInputData()
        {
            return new List<string> {
        "Quiz 1", "1) Size the water heater for a house with 3 Bathrooms and 4 Bedrooms.", "67", "80*", "", "Quiz 2","1)  A chimney can have more than one(1) passageway.", "True", "False *", ""};
        }

        public static Quiz GetMockQuiz()
        {
            return new Quiz(new List<string> { "Quiz 10", "1) Size the water heater for a house with 3 Bathrooms and 4 Bedrooms.", "67", "80*", "", "2) Question 2", "1", "2*", "" }, "1");
        }

        public static List<string> GetMockPageInputData()
        {
            return new List<string>
            {
                "Quiz 1", "50", "51", "2", "", "Quiz 2", "100", "101", "51"
            };
        }

        public static List<PageContainer> GetMockPageContainers()
        {
            var pages = new List<PageContainer>();
            pages.Add(new PageContainer(new QuizBuilder()) { QuizPageNumber = 50, SuccessPageNumber = 51, FailPageNumber = 2 });
            pages.Add(new PageContainer(new QuizBuilder()) { QuizPageNumber = 100, SuccessPageNumber = 101, FailPageNumber = 51 });
            return pages;
        }

        public static Question GetMockQuestion()
        {
            return new Question { QuestionText = "9) This is a test question." };
        }
    }
}
