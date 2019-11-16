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
        public void InputQuizTask_ShouldReturnQuizzes()
        {
            // arrange
            Quiz quiz = new Quiz(TestAuxiliaryMethods.GetMockQuizInputData(), "4545");
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
            var mockQuiz = TestAuxiliaryMethods.GetMockQuiz();
            var expected = 2;

            // act
            var answers = mockQuiz.Questions[0].Answers;

            // assert
            Assert.AreEqual(expected, answers.Count);
        }

        [TestMethod]
        public void TextHasApostrophes_ShouldReturnTrue_WhenApostrophesPresent()
        {
            // Arrange
            string text = "I'm a test.";
            var expected = true;

            // Act
            var actual = _checker.TextHasApostrophes(text);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ChangeApostropheToTick_ShouldChangeAllApostrophes()
        {
            // Arrange
            string text = "I'm a test and I'm glad to be here.";
            var expected = "I`m a test and I`m glad to be here.";

            // Act
            var actual = _checker.ChangeApostrophesToTicks(text);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TextHasDashes_ShouldReturnTrue_WhenDashesPresent()
        {
            // Arrange
            string text = "I'm a test - I'm glad to be here.";
            var expected = true;

            // Act
            var actual = _checker.TextHasApostrophes(text);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ChangeDashesToUnderscores_ShouldChangeAllApostrophes()
        {
            // Arrange
            string text = "I'm a test - I'm glad to be here.";
            var expected = "I'm a test _ I'm glad to be here.";

            // Act
            var actual = _checker.ChangeDashesToUnderscores(text);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CleanOutSmartQuotes_ShouldChangeCurlySingleQuotesToStraight()
        {
            // Arrange
            string text = "the County Clerk’s office";
            var expected = "the County Clerk's office";

            // Act
            var actual = _checker.CleanOutSmartQuotes(text);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CleanOutSmartQuotes_ShouldChangeCurlyDoubleQuotesToStraight()
        {
            // Arrange
            string text = "“CAUTION: DRINK ONLY WHEN RAINING.”";
            var expected = "\"CAUTION: DRINK ONLY WHEN RAINING.\"";
            
            // Act
            var actual = _checker.CleanOutSmartQuotes(text);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void CleanOutFractionSymbols_ShouldReplaceSymbols()
        {
            // Arrange
            string text = "½ inch ¾ inch 1 inch ¼ inch";
            var expected = "1/2 inch 3/4 inch 1 inch 1/4 inch";

            // Act
            var actual = _checker.CleanOutFractionSymbols(text);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}

