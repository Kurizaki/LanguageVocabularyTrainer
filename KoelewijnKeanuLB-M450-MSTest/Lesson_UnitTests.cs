using KoelewijnKeanuLB_M450;
namespace KoelewijnKeanuLB_M450_MSTest
{
    [TestClass]
    public class Lesson_UnitTests
    {
        [TestMethod]
        public void ValidateAnswer_CorrectAnswer_ShouldReturnTrue()
        {
            // Arrange
            List<string> vocabularyAndTranslation = new List<string> { "Word", "Translation" };
            Lesson lesson = new Lesson(vocabularyAndTranslation);

            // Act
            bool result = lesson.ValidateAnswer("Word", 0);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateAnswer_IncorrectAnswerWithinTries_ShouldReturnFalse()
        {
            // Arrange
            List<string> vocabularyAndTranslation = new List<string> { "Word", "Translation" };
            Lesson lesson = new Lesson(vocabularyAndTranslation);

            // Act
            bool result = lesson.ValidateAnswer("Incorrect", 0);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateAnswer_IncorrectAnswerExceedingTries_ShouldReturnFalse()
        {
            // Arrange
            List<string> vocabularyAndTranslation = new List<string> { "Word", "Translation" };
            Lesson lesson = new Lesson(vocabularyAndTranslation);

            // Act
            for (int i = 0; i < 3; i++)
            {
                lesson.ValidateAnswer("Incorrect", 0);
            }
            bool result = lesson.ValidateAnswer("Incorrect", 0);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void LessonStatistic_Points15_ShouldReturnExcellent()
        {
            // Arrange
            List<string> vocabularyAndTranslation = new List<string> { "Word", "Translation" };
            Lesson lesson = new Lesson(vocabularyAndTranslation);

            // Act
            lesson.ValidateAnswer("Word", 0);
            lesson.ValidateAnswer("Word", 0);
            lesson.ValidateAnswer("Word", 0);

            string result = CaptureConsoleOutput(() => lesson.LessonStatistic());

            // Assert
            StringAssert.Contains(result, "Everything Right! Excellent work");
        }

        // Utility method to capture console output for testing
        private string CaptureConsoleOutput(Action action)
        {
            using (var consoleOutput = new System.IO.StringWriter())
            {
                Console.SetOut(consoleOutput);
                action.Invoke();
                return consoleOutput.ToString();
            }
        }
    }
}
