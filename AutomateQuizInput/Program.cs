using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Ninject;

namespace AutomateQuizInput
{
    class Program
    {
        static void Main(string[] args)
        {
            // get dependencies
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());
            var reader = kernel.Get<IReader>();
            var builder = kernel.Get<IQuizBuilder>();
            var checker = kernel.Get<ITextChecker>();
            var uploader = kernel.Get<IUploader>();
            var pageContainer = kernel.Get<IPageContainer>();

            // the text document needs to be in the Docs folder and in the right format
            Console.WriteLine("This program will automatically input quizzes into the AnytimeCE Admin UI.\nFirst we will go through the provided text documents.");
            Console.WriteLine("What is the Course Id?\n ***IMPORTANT*** This must match an available course in the portal.");
            string courseId = Console.ReadLine();
            string path = @"../../Docs/Quizzes.txt";

            Console.WriteLine("We will now check the document for invalid characters, such as the single quote.");

            var allLines = reader.ReadDocument(path);
            var allLinesList = checker.FindAndReplaceInvalidCharacters(allLines).ToList();

            for (int i = 0; i < allLinesList.Count(); i++)
            {
                allLinesList[i] = checker.CleanOutSmartQuotes(allLinesList[i]);
            }

            var separatedQuizzes = builder.SeparateQuizzes(allLinesList);
            List<Quiz> completeQuizzes = new List<Quiz>();
            foreach(var quizData in separatedQuizzes)
            {
                // instantiate a quiz using the data in the list
                Quiz quiz = new Quiz(quizData, courseId);
                completeQuizzes.Add(quiz);
            }

            // add three page numbers to each quiz using the PageInfo document
            var pageDocLines = reader.ReadDocument(@"../../Docs/PageInfo.txt");
            var pages = pageContainer.GetPages(pageDocLines, completeQuizzes.Count());
            pageContainer.InsertPages(completeQuizzes, pages.ToList());

            Console.WriteLine("The documents have been successfully read, and we are ready to input your quizzes.");
            // input data from the quizzes into the admin portal using the ui

            uploader.UploadTask(completeQuizzes);
            Console.WriteLine("The program has completed successfully. Please check your quizzes in the admin portal.");
            Console.ReadLine();
        }
    }
}
