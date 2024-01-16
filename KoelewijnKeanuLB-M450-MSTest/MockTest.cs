using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using KoelewijnKeanuLB_M450;
using System.Xml;

namespace KoelewijnKeanuLB_M450.Tests
{
    [TestClass]
    public class MockTest
    {
        [TestMethod]
        public void MockXmlParser_Reads_Correctly()
        {
            // Arrange
            MockXmlParser mockXmlParser = new MockXmlParser(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Vocabulary.xml"));


            // Act
            XmlDocument xmlDocument = mockXmlParser.LoadXmlDocument();
            List<string> actualList = mockXmlParser.getRandomVocabularyAndTranslation(mockXmlParser.GetVocabularyAndTranslations("Spanish", xmlDocument));
            List<string> expectedList = new List<string> { "hello", "hola", "goodbye", "adiós", "friend", "amigo", "house", "casa", "thank you", "gracias" };

            // Assert
            CollectionAssert.AreEqual(expectedList, actualList);
        }

        [TestMethod]
        public void MockLesson_Reads_Correctly()
        {
            // Arrange
            MockXmlParser mockXmlParser = new MockXmlParser(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Vocabulary.xml"));

            XmlDocument xmlDocument = mockXmlParser.LoadXmlDocument();
            List<string> actualList = mockXmlParser.getRandomVocabularyAndTranslation(mockXmlParser.GetVocabularyAndTranslations("Spanish", xmlDocument));

            MockLesson mockLesson = new MockLesson(actualList);

            // Act
            bool result = mockLesson.ValidateAnswer("hola", 1);

            // Assert
            Assert.IsTrue(result, "Validation of the answer failed. Expected true.");
        }
    }
}