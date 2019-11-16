using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AutomateQuizInput
{
    public class QuizBuilder : IQuizBuilder
    {
        public IEnumerable<IEnumerable<string>> SeparateQuizzes(IEnumerable<string> rawLines)
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

        public List<Question> GenerateQuestions(List<string> quizDataList)
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
    }
}
