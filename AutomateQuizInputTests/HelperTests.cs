using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using AutomateQuizInput;

namespace AutomateQuizInputTests
{
    [TestClass]
    public class HelperTests
    {
        [TestMethod]
        public void SeparateQuizzes_ShouldReturnListofListOfString()
        {
            // arrange
            IEnumerable<string> testObject = new List<string>();

            // act
            IEnumerable<IEnumerable<string>> result = Helper.SeparateQuizzes(testObject);
            string resultType = result.GetType().ToString();
            // assert
            Assert.AreEqual(resultType, "System.Collections.Generic.List`1[System.Collections.Generic.List`1[System.String]]");
        }

        [TestMethod]
        public void SeparateQuizzes_ShouldReturnTwoSeparateQuizzes_WhenQuizInTextTwice()
        {
            // arrange
            IEnumerable<string> testObject = new List<string> {
        "Quiz 1", "1) Size the water heater for a house with 3 Bathrooms and 4 Bedrooms.", "67", "80*", "", "Quiz 2","1)  A chimney can have more than one(1) passageway.", "True", "False *"};

            // act
            List<IEnumerable<string>> result = Helper.SeparateQuizzes(testObject).ToList();
            // assert
            Assert.AreEqual(result.Count, 2);
        }
    }
}

