using AutomateQuizInput;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutomateQuizInputTests
{
    public static class TestAuxiliaryMethods
    {
        public static List<string> GetMockQuizInputData()
        {
            return new List<string> {
        "Quiz 1", "1) Size the water heater for a house with 3 Bathrooms and 4 Bedrooms.", "67", "80*", "", "Quiz 2","1)  A chimney can have more than one(1) passageway.", "True", "False *", ""};
        }

        public static Quiz GetMockQuiz()
        {
            return new Quiz(new List<string> { "Quiz 10", "1) Size the water heater for a house with 3 Bathrooms and 4 Bedrooms.", "67", "80*", "", "2) Question 2", "1", "2*", "" }, "1");
        }
    }
}
