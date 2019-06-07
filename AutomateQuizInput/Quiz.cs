using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutomateQuizInput
{
    public class Quiz
    {

        public string CourseId;
        public int QuizId;
        public string Status;
        public int CoursePage;
        public int PassPage;
        public int FailPage;
        public decimal PassFailPercent;
        public string ImagePath;
        public string Comment;
        List<Question> questions;

        public Quiz(IEnumerable<string> quizData, string courseId)
        {
            List<string> quizDataList = quizData.ToList(); 
            CourseId = courseId;
            // this will need to be validated
            var firstLine = quizDataList[0];
            QuizId = Convert.ToInt32(Char.GetNumericValue(firstLine[5]));
            Status = "Good";
            CoursePage = default;
            PassPage = default;
            FailPage = default;
            PassFailPercent = default;
            ImagePath = default;
            Comment = default;
            questions = GenerateQuestions(quizDataList);
        }

        private class Question
        {
            public int QuestionId;
            public string QuestionText;
            public List<string> Answers;
            public int CorrectAnswerIndex;

            public Question(int id = default, string questionText = default, List<string> answers = default, int correctAnswerIndex = default)
            {
                QuestionId = id;
                QuestionText = questionText;
                Answers = answers;
                CorrectAnswerIndex = correctAnswerIndex;
            }
        }

        private List<Question> GenerateQuestions(List<string> quizDataList)
        {
            List<Question> questions = new List<Question>();
            int questionId = default;
            string questionText = default;
            List<string> answers = new List<string>();
            int CorrectAnswerIndex = default;

            // iterate through the list and extract all questions
            for (int i = 1; i< quizDataList.Count; i++)
            {

                if(quizDataList[i] == "")
                {
                    Question question = new Question(questionId, questionText, answers, CorrectAnswerIndex);
                    // add a copy of the question to the quiz
                    questions.Add(question);
                    // remove values from the variable
                    question = new Question();
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

        public void InputQuizTask(Quiz quiz)
        {
            Console.WriteLine($"Working on quiz {quiz.QuizId}");
        }
    }
}
