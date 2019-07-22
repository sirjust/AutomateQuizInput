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
        [ExpectedException(typeof(ArgumentException))]
        public void GetPages_ShouldThrowException_WhenQuizzesAndPagesUnequal()
        {
            // arrange
            var quizzes = new List<Quiz>();
            var pages = TestAuxiliaryMethods.GetMockPageInputData();
            PageContainer pageContainer = new PageContainer();

            // act
            pageContainer.GetPages(pages, quizzes.Count);

            // assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetPages_ShouldThrowException_WhenPageValuesNotInteger()
        {
            // arrange
            var quizzes = new List<Quiz> {
                TestAuxiliaryMethods.GetMockQuiz(),
                TestAuxiliaryMethods.GetMockQuiz(),
                TestAuxiliaryMethods.GetMockQuiz()
            };
            var pages = TestAuxiliaryMethods.GetMockPageInputData();
            pages.Add("Quiz 3");
            pages.Add("hello");
            pages.Add("world");
            pages.Add("51"); ;

            PageContainer pageContainer = new PageContainer();

            // act
            pageContainer.GetPages(pages, quizzes.Count);

            // assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void GetPages_ShouldThrowException_WhenThereIsTheWrongNumberOfPages()
        {
            // arrange
            var quizzes = new List<Quiz> {
                TestAuxiliaryMethods.GetMockQuiz(),
                TestAuxiliaryMethods.GetMockQuiz(),
                TestAuxiliaryMethods.GetMockQuiz()
            };
            var pages = TestAuxiliaryMethods.GetMockPageInputData();
            pages.Add("Quiz 3");
            pages.Add("100");
            pages.Add("50");

            PageContainer pageContainer = new PageContainer();

            // act
            pageContainer.GetPages(pages, quizzes.Count);

            // assert
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InsertPages_ShouldThrowException_WhenCountOfQuizListAndPageObjectsUnequal()
        {
            // arrange
            var quizzes = new List<Quiz>();
            var pages = TestAuxiliaryMethods.GetMockPageContainers();
            PageContainer pageContainer = new PageContainer();

            // act 
            pageContainer.InsertPages(quizzes, pages);

            // assert
        }

        [TestMethod]
        public void InsertPages_ShouldInsertCorrectPagesToQuizzes()
        {
            // arrange
            var quizzes = new List<Quiz>
            {
                TestAuxiliaryMethods.GetMockQuiz(),
                TestAuxiliaryMethods.GetMockQuiz()
            };
            var pages = TestAuxiliaryMethods.GetMockPageContainers();
            PageContainer pageContainer = new PageContainer();

            // act 
            pageContainer.InsertPages(quizzes, pages);

            // assert
            Assert.AreEqual(pages[0].QuizPageNumber, quizzes[0].CoursePage);
            Assert.AreEqual(pages[1].QuizPageNumber, quizzes[1].CoursePage);
        }
    }
}
