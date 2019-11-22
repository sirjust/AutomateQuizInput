using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutomateQuizInput;

namespace AutomateQuizInputTests
{

    [TestClass]
    public class CheckerTests
    {
        IQuizBuilder _builder = new QuizBuilder();
        IReader _reader = new Reader();
        ITextChecker _checker = new TextChecker();
        public CheckerTests()
        {

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
            var expected = "the County Clerk`s office";

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
