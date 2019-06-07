using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

            driver = new FirefoxDriver(@"../../../packages/Selenium.Firefox.WebDriver.0.24.0/driver/")
            {
                Url = $"https://{LoginInfo.username}:{LoginInfo.password}@www.anytimece.com/cgi-bin/admin/course_pick_form"
            };
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            driver.Manage().Window.Maximize();
            return driver;
        }
    }
}
