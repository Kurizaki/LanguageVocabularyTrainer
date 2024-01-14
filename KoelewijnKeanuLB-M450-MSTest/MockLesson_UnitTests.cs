using KoelewijnKeanuLB_M450;
namespace KoelewijnKeanuLB_M450_MSTest
{
    [TestClass]
    internal class MockLesson_UnitTests
    {
        [TestMethod]
        public void ValidateAnswer_CorrectAnswer_ShouldReturnTrue()
        {
            // Arrange
            List<string> vocabularyAndTranslation = new List<string> { "Word", "Translation" };
            MockLesson mockLesson = new MockLesson(vocabularyAndTranslation);

            // Act
            bool result = mockLesson.ValidateAnswer("Word", 0);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void ValidateAnswer_IncorrectAnswerWithinTries_ShouldReturnFalse()
        {
            // Arrange
            List<string> vocabularyAndTranslation = new List<string> { "Word", "Translation" };
            MockLesson mockLesson = new MockLesson(vocabularyAndTranslation);

            // Act
            bool result = mockLesson.ValidateAnswer("Incorrect", 0);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void ValidateAnswer_IncorrectAnswerExceedingTries_ShouldReturnFalse()
        {
            // Arrange
            List<string> vocabularyAndTranslation = new List<string> { "Word", "Translation" };
            MockLesson mockLesson = new MockLesson(vocabularyAndTranslation);

            // Act
            for (int i = 0; i < 3; i++)
            {
                mockLesson.ValidateAnswer("Incorrect", 0);
            }
            bool result = mockLesson.ValidateAnswer("Incorrect", 0);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void LessonStatistic_Points15_ShouldReturnExcellent()
        {
            // Arrange
            List<string> vocabularyAndTranslation = new List<string> { "Word", "Translation" };
            MockLesson mockLesson = new MockLesson(vocabularyAndTranslation);

            // Act
            mockLesson.ValidateAnswer("Word", 0);
            mockLesson.ValidateAnswer("Word", 0);
            mockLesson.ValidateAnswer("Word", 0);

            string result = CaptureConsoleOutput(() => mockLesson.LessonStatistic());

            // Assert
            StringAssert.Contains(result, "Everything Right! Excellent work");
        }

        [TestMethod]
        public void PlayAgain_Yes_ShouldReturnTrue()
        {
            // Arrange
            List<string> vocabularyAndTranslation = new List<string> { "Word", "Translation" };
            MockLesson mockLesson = new MockLesson(vocabularyAndTranslation);

            // Act
            bool result = mockLesson.PlayAgain();

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void PlayAgain_No_ShouldReturnFalse()
        {
            // Arrange
            List<string> vocabularyAndTranslation = new List<string> { "Word", "Translation" };
            MockLesson mockLesson = new MockLesson(vocabularyAndTranslation);

            // Act
            bool result = mockLesson.PlayAgain();

            // Assert
            Assert.IsFalse(result);
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
