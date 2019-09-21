using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutomateQuizInput
{
    public class Question
    {
        static readonly int _maxAnswers = 5;
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<string> Answers { get; set; }
        public int CorrectAnswerIndex { get; set; }
        public string QuestionStatus { get; set; } = "A";
        public string QuestionType { get; set; }

        public Question(int id = default, string questionText = default, List<string> answers = default, int correctAnswerIndex = default)
        {
            QuestionId = id;
            QuestionText = questionText;
            Answers = answers;
            CorrectAnswerIndex = correctAnswerIndex;
        }

        public string RemoveNumberFromQuestionText(string questionText)
        {
            string newQuestionText;
            string pattern = @"^[\d]+[)\s]+";
            if (Regex.IsMatch(questionText, pattern))
            {
                newQuestionText = Regex.Replace(questionText, pattern, "");
                return newQuestionText;
            }
            return questionText;
        }

        public bool HasMoreThanFiveAnswers(List<string> answers) => answers.Count() > 5 ? true : false;
    }
}
