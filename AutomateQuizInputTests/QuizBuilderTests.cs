using AutomateQuizInput;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;


namespace AutomateQuizInputTests
{
    [TestClass]
    public class QuizBuilderTests
    {
        IQuizBuilder _builder = new QuizBuilder();
        IReader _reader = new Reader();
        ITextChecker _checker = new TextChecker();

        [TestMethod]
        public void SeparateQuizzes_ShouldReturnListofListOfString()
        {
            // arrange
            IEnumerable<string> testObject = new List<string>();

            // act
            IEnumerable<IEnumerable<string>> result = _builder.SeparateQuizzes(testObject);
            string resultType = result.GetType().ToString();
            // assert
            Assert.AreEqual(resultType, "System.Collections.Generic.List`1[System.Collections.Generic.List`1[System.String]]");
        }

        [TestMethod]
        public void SeparateQuizzes_ShouldReturnTwoSeparateQuizzes_WhenQuizInTextTwice()
        {
            // arrange
            IEnumerable<string> testObject = TestAuxiliaryMethods.GetMockQuizInputData();
            int expected = 2;

            // act
            List<IEnumerable<string>> result = _builder.SeparateQuizzes(testObject).ToList();
            // assert
            Assert.AreEqual(expected, result.Count);
        }

        [TestMethod]
        public void ReadDocument_Shouldreturn_TwolinewheninputisTwo()
        {
            // arrange
            string expected = "World!";
            // act
            var result = _reader.ReadDocument(@"..\..\..\AutomateQuizInput\Docs\Test\TextFile.txt").ToList();
            // assert
            Assert.AreEqual(expected, result[1]);
        }

        [TestMethod]
        public void GenerateQuestions_ShouldReturnAnswersForEachQuestion()
        {
            // arrange
            var mockQuiz = TestAuxiliaryMethods.GetMockQuiz();
            var expected = 2;

            // act
            var answers = mockQuiz.Questions[0].Answers;

            // assert
            Assert.AreEqual(expected, answers.Count);
        }

        [TestMethod]
        public void GenerateQuestions_ShouldHaveQuestionNumberForSingleDigitQuestions()
        {
            // arrange
            var quiz = TestAuxiliaryMethods.GetMockQuiz();

            // act


            // assert
            foreach(var question in quiz.Questions)
            {
                Assert.IsTrue(question.QuestionId < 10);
            }
        }

        [TestMethod]
        public void GenerateQuestions_ShouldHaveQuestionNumberForDoubleDigitQuestions()
        {
            // arrange
            var quiz = new Quiz(new List<string> { "Quiz 10", "11) Size the water heater for a house with 3 Bathrooms and 4 Bedrooms.", "67", "80*", "", "12) Question 2", "1", "2*", "" }, "1");
            // act


            // assert
            foreach (var question in quiz.Questions)
            {
                Assert.IsTrue(question.QuestionId > 10);
            }
        }

        [TestMethod]
        public void GenerateQuestions_ShouldNotifyCorrectlyWhenMoreThanFiveAnswers()
        {
            // arrange
            QuizBuilder builder = new QuizBuilder();
            Question question = new Question(1, "question", new List<string> { "1", "2", "3", "4", "5", "6" }, 1);
            var expected = "The question with the following text";
            // act
            var actual = builder.NotifyIfFiveOrMoreAnswers(question.Answers);

            // assert
            Assert.IsTrue(actual.Contains(expected));
        }
    }
}
