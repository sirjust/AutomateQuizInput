using AutomateQuizInput;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateQuizInputTests
{
    [TestClass]
    public class QuestionTests
    {
        [TestMethod]
        public void RemoveNumberFromQuestionText_ShouldRemoveTextCorrectly()
        {
            // arrange
            var testQuestion = TestAuxiliaryMethods.GetMockQuestion();
            var expected = "This is a test question.";

            // act
            var actual = testQuestion.RemoveNumberFromQuestionText(testQuestion.QuestionText);

            // assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HasMoreThanFourAnswers_ShouldReturnTrueIfMoreThanFourAnswers()
        {
            // Arrange
            List<string> mockAnswers = new List<string>
            {
                "Answer 1",
                "Answer 2",
                "Answer 3",
                "Answer 4",
                "Answer 5",
                "Answer 6"
            };
            var testQuestion = TestAuxiliaryMethods.GetMockQuestion();
            testQuestion.Answers = mockAnswers;
            
            var expected = true;

            // Act
            var actual = testQuestion.HasMoreThanFiveAnswers(testQuestion.Answers);

            // Assert
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void HasMoreThanFourAnswers_ShouldReturnFalseIfLessThanFourAnswers()
        {
            // Arrange
            List<string> mockAnswers = new List<string>
            {
                "Answer 1",
                "Answer 2"
            };
            var testQuestion = TestAuxiliaryMethods.GetMockQuestion();
            testQuestion.Answers = mockAnswers;

            var expected = false;

            // Act
            var actual = testQuestion.HasMoreThanFiveAnswers(testQuestion.Answers);

            // Assert
            Assert.AreEqual(expected, actual);
        }
    }
}
