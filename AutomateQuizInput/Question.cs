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
        public int QuestionId { get; set; }
        public string QuestionText { get; set; }
        public List<string> Answers { get; set; }
        public int CorrectAnswerIndex { get; set; }
        public string QuestionStatus { get; set; }
        public string QuestionType { get; set; }

        public Question(int id = default, string questionText = default, List<string> answers = default, int correctAnswerIndex = default)
        {
            QuestionId = id;
            QuestionText = questionText;
            Answers = answers;
            CorrectAnswerIndex = correctAnswerIndex;
        }
    }
}
