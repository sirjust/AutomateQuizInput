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

            for (int i = 0; i < lineList.Count; i++)
            {
                quizList.Add(lineList[i]);
                if (i == lineList.Count - 1)
                {
                    List<string> temp = quizList.ToList();
                    initialSeparatedList.Add(temp);
                }
                else if (lineList[i + 1].Contains("Quiz"))
                {
                    List<string> temp = quizList.ToList();
                    initialSeparatedList.Add(temp);
                    quizList.Clear();
                }
            }
            return initialSeparatedList;
        }

        public static IWebDriver UploadTask(List<Quiz> quizzes)
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
        private static void TryToGetCourseId(IWebDriver driver, Quiz quiz, int attempts = 3)
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
        private static void UploadQuizzes(List<Quiz> quizzes, IWebDriver driver)
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

        private static void UploadQuestions(List<Question> questions, IWebDriver driver)
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
                driver.FindElement(By.Name("button_action")).Click();
                if (j < questions.Count - 1)
                {
                    driver.FindElement(By.XPath("(.//*[normalize-space(text()) and normalize-space(.)='Success:'])[1]/following::input[2]")).Click();
                }
            }
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
                    // Check if there are more than five answers and remove the last if so
                    if (question.HasMoreThanFiveAnswers(question.Answers))
                    {
                        Console.WriteLine($"The question with the following text: ---{question.QuestionText}--- has more than 5 answers. Please exit the program and modify the quiz document.");
                        Console.ReadLine();
                        Environment.Exit(0);
                    }

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
                }

                else if (Regex.IsMatch(quizDataList[i], @"(^[0-9]{2}\)+)"))
                {
                    // check if there are two numbers at the start
                    questionId = Convert.ToInt32(quizDataList[i].Substring(0, 2));
                    // If so, that line is the question line
                    questionText = quizDataList[i];
                }

                // The next lines until the blank line are the potential answers
                else
                {
                    answers.Add(quizDataList[i]);
                }
            }
            return questions;
        }

        public static bool TextHasApostrophes(string text)
        {
                if (text.Contains("'"))
                {
                    return true;
                }
            return false;
        }

        public static string ChangeApostrophesToTicks(string text)
        {
            var newText = text.Replace("'", "`");
            return newText;
        }

        public static bool TextHasDashes(string text)
        {
            if (text.Contains("-"))
            {
                return true;
            }
            return false;
        }

        public static string ChangeDashesToUnderscores(string text)
        {
            var newText = text.Replace("-", "_");
            return newText;
        }

        public static IEnumerable<string> FindAndReplaceInvalidCharacters(IEnumerable<string> lines)
        {
            var lineList = lines.ToList();
            for(int i = 0; i < lines.Count(); i++) 
            {
                if (TextHasApostrophes(lineList[i]))
                {
                    Console.WriteLine("The text has an apostrophe. Updating...");
                    lineList[i] = ChangeApostrophesToTicks(lineList[i]);
                }

                if (TextHasDashes(lineList[i]))
                {
                    Console.WriteLine("The text has a dash. Updating...");
                    lineList[i] = ChangeDashesToUnderscores(lineList[i]);
                }
            }
            return lineList;
        }
    }
}