using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using AutomateQuizInput;
using System;


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

        
    }
}

