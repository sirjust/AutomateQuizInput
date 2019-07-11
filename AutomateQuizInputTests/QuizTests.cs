using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutomateQuizInput;

namespace AutomateQuizInputTests
{
    [TestClass]
    public class QuizTests
    {
        [TestMethod]
        public void QuizConstructor_ShouldProduceCorrectQuizId()
        {
            // arrange
            string expected = "10";

            // act
            Quiz mockQuiz = new Quiz(TestAuxiliaryMethods.GetMockQuizInputData(), "10");

            // asssert
            Assert.AreEqual(expected, mockQuiz.QuizId);
        }

        [TestMethod]
        public void QuizConstructor_ShouldProduceCorrectQuestionIds()
        {
            // arrange
            string expected = "10";
            var mockData = TestAuxiliaryMethods.GetMockQuizInputData();
            mockData.Add("10) A direct vent water heater is constructed and installed so that all air for combustion is derived directly from the outside atmosphere and all flue gases are discharged directly to the outside atmosphere.");
            mockData.Add("True*");
            mockData.Add("False");
            mockData.Add("");

            // act
            Quiz mockQuiz = new Quiz(mockData, "10");

            // asssert
            Assert.AreEqual(expected, mockQuiz.Questions[2].QuestionId);
        }
    }
}
