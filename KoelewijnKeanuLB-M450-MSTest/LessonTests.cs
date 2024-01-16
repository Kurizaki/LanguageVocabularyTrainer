using KoelewijnKeanuLB_M450;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoelewijnKeanuLB_M450_MSTest
{
    [TestClass]
    public class LessonTests
    {
        private Lesson _lesson;
        private List<string> _vocabularyAndTranslation;

        [TestInitialize]
        public void Setup()
        {
            _vocabularyAndTranslation = new List<string>
            {
                "hello", "hola", "goodbye", "adiós", "friend", "amigo", "house", "casa", "thank you", "gracias"
            };

            _lesson = new Lesson(_vocabularyAndTranslation);
        }

        [TestMethod]
        public void ValidateAnswer_WrongAnswerOnFirstTry_ShouldNotIncreasePoints()
        {
            // Arrange
            int initialPoints = _lesson.GetPoints();
            int index = 0;
            string wrongAnswer = "WrongAnswer";

            // Act
            bool result = _lesson.ValidateAnswer(wrongAnswer, index);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(initialPoints, _lesson.GetPoints());
        }

        [TestMethod]
        public void ValidateAnswer_RightAnswerOnFirstTry_ShouldIncreasePoints()
        {
            // Arrange
            int initialPoints = _lesson.GetPoints();
            int index = 0;
            string rightAnswer = "hola";

            // Act
            bool result = _lesson.ValidateAnswer(rightAnswer, index);

            // Assert
            Assert.IsFalse(result);
            Assert.AreEqual(initialPoints, _lesson.GetPoints());
        }
    }
}
