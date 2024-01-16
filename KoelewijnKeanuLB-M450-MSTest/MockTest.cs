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
            //Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Vocabulary.xml")
            string path = @"C:\Users\keanu\source\repos\KoelewijnKeanuLB-M450\KoelewijnKeanuLB-M450\Vocabulary.xml";
            MockXmlParser mockXmlParser = new MockXmlParser(path);
            //konnte nicht getestet werden, der Test hängt irgendwo fest.

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
            //Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Vocabulary.xml")
            string path = @"C:\Users\keanu\source\repos\KoelewijnKeanuLB-M450\KoelewijnKeanuLB-M450\Vocabulary.xml";
            MockXmlParser mockXmlParser = new MockXmlParser(path);
            //konnte nicht getestet werden, der Test hängt irgendwo fest.

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