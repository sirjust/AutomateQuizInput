using System;

namespace AutomateQuizInput
{
    public static class DocumentationWriter
    {
        public static void WriteDocumentationForUser()
        {
            var documentation = string.Format(
@"Here is the necessary format for Quizzes.txt
Quiz 1
1) Size the water heater for a house with 3 Bathrooms and 4 Bedrooms.
42
54
67
80*

2) Size the water heater for a house with 1 Bathroom and 3 Bedrooms.
42
54*
67
80

Quiz 2
1) Question 1, false is correct
True
False*

2) Question 2, true is correct
True*
False
");
            Console.WriteLine("This program will automatically input quizzes into the AnytimeCE Admin UI. There should be two documents in the Docs folder: Quizzes.txt and PageInfo.txt. See the readme.txt for more info.\n");
            Console.WriteLine(documentation);
        }
    }
}
