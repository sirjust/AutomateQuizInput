using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutomateQuizInput;

namespace AutomateQuizInputTests
{
    [TestClass]
    public class PageTests
    {
        [TestMethod]
        public void GetPages_ShouldThrowException_WhenQuizzesAndPagesUnequal()
        {
            // arrange
            var quizzes = new List<Quiz>();
            var pages = TestAuxiliaryMethods.GetMockPageInputData();
            PageContainer pageContainer = new PageContainer();

            // act

            // assert
            Assert.ThrowsException<Exception>(pageContainer.GetPages(pages, quizzes.Count));
        }

        [TestMethod]
        public void InsertPages_ShouldThrowException_WhenCountOfQuizListAndPageObjectsUnequal()
        {

        }
    }
}
