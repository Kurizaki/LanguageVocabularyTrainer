using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using KoelewijnKeanuLB_M450;
using System.Xml;

namespace KoelewijnKeanuLB_M450.Tests
{
    [TestClass]
    public class Class1
    {
        [TestMethod]
        public void MockXmlParser_Reads_Correctly()
        {
            try
            {
                // Arrange
                string filePath = Path.Combine(@"C:\Users\keanu\source\repos\KoelewijnKeanuLB-M450\KoelewijnKeanuLB-M450-MSTest\Vocabulary.xml");
                MockXmlParser mockXmlParser = new MockXmlParser(filePath);

                // Act
                XmlDocument xmlDocument = mockXmlParser.LoadXmlDocument();
                List<string> actualList = mockXmlParser.getRandomVocabularyAndTranslation(mockXmlParser.GetVocabularyAndTranslations("Spanish", xmlDocument));
                List<string> expectedList = new List<string> { "hello", "hola", "goodbye", "adiós", "friend", "amigo", "house", "casa", "thank you", "gracias" };

                // Assert
                CollectionAssert.AreEqual(expectedList, actualList);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test failed with exception: {ex}");
                throw;
            }
        }

        [TestMethod]
        public void MockLesson_Reads_Correctly()
        {
            try
            {
                // Arrange
                string filePath = Path.Combine(@"C:\Users\keanu\source\repos\KoelewijnKeanuLB-M450\KoelewijnKeanuLB-M450-MSTest\Vocabulary.xml");
                MockXmlParser mockXmlParser = new MockXmlParser(filePath);
                XmlDocument xmlDocument = mockXmlParser.LoadXmlDocument();
                List<string> actualList = mockXmlParser.getRandomVocabularyAndTranslation(mockXmlParser.GetVocabularyAndTranslations("Spanish", xmlDocument));

                MockLesson mockLesson = new MockLesson(actualList);

                // Act
                bool result = mockLesson.ValidateAnswer("hola", 0);

                // Assert
                Assert.IsTrue(result, "Validation of the answer failed. Expected true.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Test failed with exception: {ex}");
                throw;
            }
        }
    }
}
