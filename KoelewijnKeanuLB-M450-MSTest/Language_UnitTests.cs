using KoelewijnKeanuLB_M450;
namespace KoelewijnKeanuLB_M450_MSTest
{
    [TestClass]
    public class Language_UnitTests
    {
        private Language _language;

        [TestInitialize]
        public void TestInitialize()
        {
            string vocabularyFilePath = Path.Combine("..\\..\\..", "Vocabulary.xml");
            XmlParser xmlParser = new XmlParser(vocabularyFilePath);
            _language = new Language(xmlParser);
        }

        [TestMethod]
        public void ChooseLanguage_WhenValidInput_ReturnsSelectedLanguage()
        {
            // Arrange
            string expectedLanguage = "Spanish";

            // Act
            string selectedLanguage = CaptureConsoleOutput(() => _language.ChooseLanguage(1));

            // Assert
            Assert.AreEqual(expectedLanguage, selectedLanguage);
        }

        [TestMethod]
        public void ChooseLanguage_WhenInvalidInputThenValidInput_ReturnsSelectedLanguage()
        {
            // Arrange
            string expectedLanguage = "Italian";

            // Act
            CaptureConsoleOutput(() =>
            {
                Console.SetIn(new System.IO.StringReader("invalid\n2"));
                _language.ChooseLanguage(); // Call the method to set the internal state
            });
            string selectedLanguage = _language.ChooseLanguage(); // Call again to get the selected language

            // Assert
            Assert.AreEqual(expectedLanguage, selectedLanguage);
        }

        [TestMethod]
        public void VocabularyAndTranslation_WhenValidLanguage_ReturnsNonEmptyList()
        {
            // Arrange
            string validLanguage = "Spanish";

            // Act
            List<string> vocabularyList = _language.VocabularyAndTranslation(validLanguage);

            // Assert
            Assert.IsNotNull(vocabularyList);
            Assert.IsTrue(vocabularyList.Any());
        }

        [TestMethod]
        public void VocabularyAndTranslation_WhenInvalidLanguage_ReturnsEmptyList()
        {
            // Arrange
            string invalidLanguage = "InvalidLanguage";

            // Act
            List<string> vocabularyList = _language.VocabularyAndTranslation(invalidLanguage);

            // Assert
            Assert.IsNotNull(vocabularyList);
            Assert.IsFalse(vocabularyList.Any());
        }

        private string CaptureConsoleOutput(Action action)
        {
            using (var consoleOutput = new System.IO.StringWriter())
            {
                Console.SetOut(consoleOutput);
                action.Invoke();
                return consoleOutput.ToString().Trim();
            }
        }
    }
}
