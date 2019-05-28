using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateQuizInput
{
    public class Quiz
    {
        public int CourseId;
        public int QuizId;
        public string status;
        public int CoursePage;
        public int PassPage;
        public int FailPage;
        public decimal PassFailPercent;
        public string ImagePath;
        public string Comment;
        List<Question> questions;

        private class Question
        {
            public int QuestionId;
            public string[] Answers;
            public int CorrectAnswerIndex;
        }

        private List<Question> GenerateQuestions()
        {
            throw new NotImplementedException();
        }
    }
}
