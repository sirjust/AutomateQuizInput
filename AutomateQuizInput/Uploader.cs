using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AutomateQuizInput
{
    public class Uploader : IUploader
    {

        public IWebDriver UploadTask(List<Quiz> quizzes)
        {
            IWebDriver driver;
            driver = new FirefoxDriver(@"../../../packages/Selenium.Firefox.WebDriver.0.24.0/driver/")
            {
                Url = $"https://{LoginInfo.username}:{LoginInfo.password}@www.anytimece.com/cgi-bin/admin/course_pick_form"
            };
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            UploadQuizzes(quizzes, driver);

            return driver;
        }
        private void TryToGetCourseId(IWebDriver driver, Quiz quiz, int attempts = 3)
        {
            try
            {
                new SelectElement(driver.FindElement(By.Name("course_id"))).SelectByText(Quiz.CourseId);
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Choose a Course ID'])[1]/following::input[1]")).Click();
            }
            catch (NoSuchElementException ex)
            {
                attempts--;
                if (attempts < 0) { throw ex; }
                Console.WriteLine("The Course ID was not found. Please input the title of a course that is on the server.");
                Quiz.CourseId = Console.ReadLine();
                TryToGetCourseId(driver, quiz, attempts);
            }
        }
        private void UploadQuizzes(List<Quiz> quizzes, IWebDriver driver)
        {
            //Loop through all the quiz fields on by one
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            foreach (Quiz quiz in quizzes)
            {
                driver.FindElement(By.Name("course_id")).Click();
                TryToGetCourseId(driver, quiz);

                IWebElement course_page = wait.Until(d => d.FindElement(By.Name("course_page")));
                course_page.SendKeys(quiz.CoursePage.ToString());
                IWebElement course_pass_page = wait.Until(d => d.FindElement(By.Name("course_pass_page")));
                course_pass_page.SendKeys(quiz.PassPage.ToString());
                driver.FindElement(By.XPath("//td/table[2]")).Click();
                IWebElement course_fail_page = wait.Until(d => d.FindElement(By.Name("course_fail_page")));
                course_fail_page.SendKeys(quiz.FailPage.ToString());
                IWebElement pass_fail_percent = wait.Until(d => d.FindElement(By.Name("pass_fail_percent")));
                pass_fail_percent.SendKeys(quiz.PassFailPercent.ToString());
                driver.FindElement(By.Name("button_action")).Click();

                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Success:'])[1]/following::input[2]")).Click();

                UploadQuestions(quiz.Questions, driver);

                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Success:'])[1]/following::input[1]")).Click();
            }
        }

        private void UploadQuestions(List<Question> questions, IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            //foreach quiz question
            for (int j = 0; j < questions.Count; j++)
            {
                int questionId = questions[j].QuestionId;
                string questionText = questions[j].QuestionText;
                // we add one to this value because the portal isn't zero-based
                int CorrectAnswerIndex = questions[j].CorrectAnswerIndex + 1;
                string questStatus = questions[j].QuestionStatus;
                string qType = questions[j].QuestionType;
                IWebElement qstatus = wait.Until(d => d.FindElement(By.Name("q_status")));
                qstatus.SendKeys(questStatus);
                IWebElement qtype = wait.Until(d => d.FindElement(By.Name("q_type")));
                qtype.Clear();
                qtype.SendKeys(qType = "M");
                IWebElement qtext = wait.Until(d => d.FindElement(By.Name("q_text")));
                qtext.Clear();
                qtext.SendKeys(questions[j].RemoveNumberFromQuestionText(questionText));
                for (int i = 0; i < questions[j].Answers.Count(); i++)
                {
                    IWebElement answerText = wait.Until(d => d.FindElement(By.Name($"q_a{i + 1}")));
                    answerText.SendKeys(questions[j].Answers[i]);
                }
                IWebElement qCorrect = wait.Until(d => d.FindElement(By.Name("q_correct")));
                qCorrect.SendKeys(CorrectAnswerIndex.ToString());
                Thread.Sleep(2000);
                driver.FindElement(By.Name("button_action")).Click();
                if (j < questions.Count - 1)
                {
                    driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Success:'])[1]/following::input[2]")).Click();
                }
            }
        }
    }
}
