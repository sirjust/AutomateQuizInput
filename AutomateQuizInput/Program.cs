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
            string path = @"../../Docs/Quizzes.txt";
            var allLines = Helper.ReadDocument(path);
            foreach (string line in allLines)
            {
                Console.WriteLine(line);
            }
            Console.ReadLine();
        }
    }
}
