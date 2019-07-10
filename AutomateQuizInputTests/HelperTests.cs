using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using AutomateQuizInput;
using System;


namespace AutomateQuizInputTests
{
    [TestClass]
    public class HelperTests
    {
        [TestMethod]
        public void SeparateQuizzes_ShouldReturnListofListOfString()
        {
            // arrange
            IEnumerable<string> testObject = new List<string>();

            // act
            IEnumerable<IEnumerable<string>> result = Helper.SeparateQuizzes(testObject);
            string resultType = result.GetType().ToString();
            // assert
            Assert.AreEqual(resultType, "System.Collections.Generic.List`1[System.Collections.Generic.List`1[System.String]]");
        }

        [TestMethod]
        public void SeparateQuizzes_ShouldReturnTwoSeparateQuizzes_WhenQuizInTextTwice()
        {
            // arrange
            IEnumerable<string> testObject = GetMockQuizInputData();

            // act
            List<IEnumerable<string>> result = Helper.SeparateQuizzes(testObject).ToList();
            // assert
            Assert.AreEqual(result.Count, 2);
        }
        [TestMethod]
        public void ReadDocument_Shouldreturn_TwolinewheninputisTwo()
        {
            // arrange
            string expected = "World!";
            // act
            var result = Helper.ReadDocument(@"..\..\..\AutomateQuizInput\Docs\TextFile.txt").ToList();
            // assert
            Assert.AreEqual(expected, result[1]);
        }

        [TestMethod]
        public void InputQuizTask_ShouldReturnQuizzes()
        {
            // arrange
            Quiz quiz = new Quiz(GetMockQuizInputData(), "4545");
            //this is new note for tets
            string str = $"Working on quiz {quiz.QuizId}";

            // act
            var x = quiz.InputQuizTask(quiz);

            // assert
            Assert.AreEqual(str, x);
        }

        [TestMethod]
        public void GenerateQuestions_ShouldReturnAnswersForEachQuestion()
        {
            // arrange
            var mockQuiz = GetMockQuiz();
            var expected = 2;

            // act
            var answers = mockQuiz.Questions[0].Answers;

            // assert
            Assert.AreEqual(expected, answers.Count);
        }

        public List<string> GetMockQuizInputData()
        {
            return new List<string> {
        "Quiz 1", "1) Size the water heater for a house with 3 Bathrooms and 4 Bedrooms.", "67", "80*", "", "Quiz 2","1)  A chimney can have more than one(1) passageway.", "True", "False *"};
        }

        public Quiz GetMockQuiz()
        {
            return new Quiz(new List<string> { "Quiz 1", "1) Size the water heater for a house with 3 Bathrooms and 4 Bedrooms.", "67", "80*", "", "2) Question 2", "1", "2*", "" }, "1");
        }
    }
}

