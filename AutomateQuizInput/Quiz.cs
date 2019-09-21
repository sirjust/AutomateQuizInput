using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutomateQuizInput
{
    public class Quiz
    {
        public static string CourseId { get; set; }
        public int QuizId { get; set; }
        public string Status { get; set; }
        public int CoursePage { get; set; }
        public int PassPage { get; set; }
        public int FailPage { get; set; }
        public decimal PassFailPercent { get; set; }
        public string ImagePath { get; set; }
        public string Comment { get; set; }
        public List<Question> Questions { get; set; }

        public Quiz() {}
        public Quiz(IEnumerable<string> quizData, string courseId)
        {
            List<string> quizDataList = quizData.ToList(); 
            CourseId = courseId;
            // this will need to be validated
           var firstLine = quizDataList[0];
            
            string[] number = Regex.Split(firstLine, @"\D+");
            foreach(string value in number)
            {
                if(!string.IsNullOrEmpty(value))
                {
                    int i = int.Parse(value);
                    //Console.WriteLine("number:{0}", i);

                    QuizId = Convert.ToInt32(i);
                }
            }

            Status = "Good";
            CoursePage = default;
            PassPage = default;
            FailPage = default;
            PassFailPercent = default;
            ImagePath = default;
            Comment = default;
            Questions = Helper.GenerateQuestions(quizDataList);
        }

        public string InputQuizTask(Quiz quiz)
        {
            // dummy method
            return $"Working on quiz {quiz.QuizId}";
        }
    }
}
