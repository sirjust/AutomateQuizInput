using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace AutomateQuizInput
{
    public static class Helper
    {
        public static IEnumerable<string> ReadDocument(string path)
        {
            var allLines = File.ReadAllLines(path);
            return allLines;
        }

        public static IEnumerable<IEnumerable<string>> SeparateQuizzes(IEnumerable<string> rawLines)
        {
            List<string> lineList = rawLines.ToList();
            List<List<string>> initialSeparatedList = new List<List<string>>();
            List<string> quizList = new List<string>();

            for (int i = 0; i< lineList.Count; i++)
            {
                quizList.Add(lineList[i]);
                if(i == lineList.Count - 1)
                {
                    List<string> temp = quizList.ToList();
                    initialSeparatedList.Add(temp);
                }
                else if (lineList[i+1].Contains("Quiz"))
                {
                    List<string> temp = quizList.ToList();
                    initialSeparatedList.Add(temp);
                    quizList.Clear();
                }
            }
            return initialSeparatedList;
        }

     
        public static IWebDriver OpenWebpage()
        {
            IWebDriver driver;
            List<Quiz> quiz = new List<Quiz>();
            Quiz quizzes = new Quiz();
            string courseid = quizzes.CourseId;
            int quizid = quizzes.QuizId;
            string status = quizzes.Status;
            int coursepage = quizzes.CoursePage;
            int coursepasspage = quizzes.PassPage;
            int coursefailpag = quizzes.FailPage;
            decimal passfailpercant = quizzes.PassFailPercent;
            string imagepath = quizzes.ImagePath;
            string comment = quizzes.Comment;
            driver = new FirefoxDriver(@"../../../packages/Selenium.Firefox.WebDriver.0.24.0/driver/")
            {
                Url = $"https://{LoginInfo.username}:{LoginInfo.password}@www.anytimece.com/cgi-bin/admin/course_pick_form"
            };
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            driver.Manage().Window.Maximize();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.FindElement(By.Name("course_id")).Click();
                new SelectElement(driver.FindElement(By.Name("course_id"))).SelectByText("W2006UPC5WaterHeaterOR_SC");
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Choose a Course ID'])[1]/following::option[15]")).Click();
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Choose a Course ID'])[1]/following::input[1]")).Click();
            //IWebElement addbtn = wait.Until(d => d.FindElement(By.XPath("//input[@value='Add Quiz']")));
            //addbtn.Click();
                IWebElement element6 = wait.Until(d => d.FindElement(By.Name("quiz_status")));
                element6.Click();
                element6.SendKeys("A");
                IWebElement element = wait.Until(d => d.FindElement(By.Name("course_page")));
                element.Click();
                element.SendKeys("30");
                IWebElement element2 = wait.Until(d => d.FindElement(By.Name("course_pass_page")));
                element2.Click();
                element2.SendKeys("34");
                driver.FindElement(By.XPath("//td/table[2]")).Click();
                IWebElement element3 = wait.Until(d => d.FindElement(By.Name("course_fail_page")));
                element3.Click();
                element3.SendKeys("5");
                IWebElement element4 = wait.Until(d => d.FindElement(By.Name("pass_fail_percent")));
                element4.Click();
                element4.SendKeys("60");
                driver.FindElement(By.Name("quiz_image_path")).Click();
                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Quiz Image Path'])[1]/following::option[1]")).Click();
                driver.FindElement(By.Name("quiz_image_path")).Click();
                IWebElement element5 = wait.Until(d => d.FindElement(By.Name("quiz_comment")));
                element5.Click();
                element5.SendKeys("checking wait method");
                driver.FindElement(By.Name("button_action")).Click();

                driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Success:'])[1]/following::input[2]")).Click();
                IWebElement element9 = wait.Until(d => d.FindElement(By.XPath("//input[@value='Add another Quiz']")));
                element9.Click();
                IWebElement element7 = wait.Until(d => d.FindElement(By.XPath("//input[@value='Add questions to this Quiz']")));
                element7.Click();
                IWebElement element8 = wait.Until(d => d.FindElement(By.XPath("//input[@value='Go Back to Admin Console']")));
                element8.Click();
                driver.FindElement(By.Name("button_action")).Click();
                //driver.FindElement(By.XPath("//input[@value='Add questions to this Quiz']")).Click();
            return driver;
        }

        public static List<Question> GenerateQuestions(List<string> quizDataList)
        {
            List<Question> questions = new List<Question>();
            int questionId = default;
            string questionText = default;
            List<string> answers = new List<string>();
            int CorrectAnswerIndex = default;

            // iterate through the list and extract all questions
            for (int i = 1; i < quizDataList.Count; i++)
            {

                if (quizDataList[i] == "")
                {
                    Question question = new Question(questionId, questionText, answers.ToList(), CorrectAnswerIndex);
                    // add a copy of the question to the quiz
                    questions.Add(question);
                    // remove values from the variable
                    question = new Question();
                    answers.Clear();
                    continue;
                }

                // Find the star among the answers, remove the star, add it to the answers list and set the CorrectAnswerIndex to the index where the answer is put
                if (quizDataList[i].Contains("*"))
                {
                    quizDataList[i] = quizDataList[i].TrimEnd('*');
                    answers.Add(quizDataList[i]);
                    CorrectAnswerIndex = answers.Count - 1;
                }

                // Check if there is a number and a close parentheses
                else if (Regex.IsMatch(quizDataList[i], @"(^[0-9]{1}\)+)"))
                {
                    // check if there is one number at the start
                    questionId = Convert.ToInt32(char.GetNumericValue(quizDataList[i].First()));
                    // If so, that line is the question line
                    questionText = quizDataList[i];
                    Console.WriteLine($"Question {questionId}: {questionText}");
                }

                else if (Regex.IsMatch(quizDataList[i], @"(^[0-9]{2}\)+)"))
                {
                    // check if there are two numbers at the start
                    questionId = Convert.ToInt32(quizDataList[i].Substring(0, 2));
                    // If so, that line is the question line
                    questionText = quizDataList[i];
                    Console.WriteLine($"Question {questionId}: {questionText}");
                }

                // The next lines until the blank line are the potential answers
                else
                {
                    answers.Add(quizDataList[i]);
                }
            }
            return questions;
        }

        public static void InsertPages(IEnumerable<Quiz> quizzes, IEnumerable<PageObject> pages)
        {

        }
    }
}
