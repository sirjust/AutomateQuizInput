using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateQuizInput
{
    class Program
    {
        static void Main(string[] args)
        {
            // the text document needs to be in the Docs folder and in the right format
            Console.WriteLine("This program will automatically input quizzes into the AnytimeCE Admin UI.");
            Console.WriteLine("What is the CourseID?");
            string courseId = Console.ReadLine();
            string path = @"../../Docs/Quiz.txt";
            var allLines = Helper.ReadDocument(path);
            var separatedQuizzes = Helper.SeparateQuizzes(allLines);
            List<Quiz> completeQuizzes = new List<Quiz>();
            List<string> list = new List<string>();
            var genequestion = Helper.GenerateQuestions(list);
            foreach(var quizData in separatedQuizzes)
            {
                // instantiate a quiz using the data in the list
                Quiz quiz = new Quiz(quizData, courseId);
                completeQuizzes.Add(quiz);
            }

            // add three page numbers to each quiz using the PageInfo document
            PageContainer pageContainer = new PageContainer();
            var pageDocLines = Helper.ReadDocument(@"../../Docs/PageInfo.txt");
            var pages = pageContainer.GetPages(pageDocLines, completeQuizzes.Count());
            pageContainer.InsertPages(completeQuizzes, pages.ToList());

            // input data from the quizzes into the admin portal using the ui
            //List<string> complete = Helper.SeparateQuizzes(allLines);
            Helper.OpenWebpage(completeQuizzes);

            foreach(Quiz quiz in completeQuizzes)
            {
                var text = quiz.InputQuizTask(quiz);
                Console.WriteLine(text);
            }
            Console.ReadLine();
        }
    }
}
