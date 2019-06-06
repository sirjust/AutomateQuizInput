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
            string path = @"../../Docs/Quizzes.txt";
            var allLines = Helper.ReadDocument(path);
            var separatedQuizzes = Helper.SeparateQuizzes(allLines);
            List<Quiz> completeQuizzes = new List<Quiz>();
            foreach(var quizData in separatedQuizzes)
            {
                // instantiate a quiz using the data in the list
                Quiz quiz = new Quiz(quizData, courseId);
                // input quiz data into the admin using the UI
                completeQuizzes.Add(quiz);
            }

            Console.ReadLine();
        }
    }
}
