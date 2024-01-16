using System.Xml;

namespace KoelewijnKeanuLB_M450
{
    class XmlParser : IXmlParser
    {
        private string _filePath;

        public XmlParser(string filePath)
        {
            _filePath = filePath;
        }

        public XmlDocument LoadXmlDocument()
        {
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load(_filePath);
            return xmlDocument;
        }

        public List<string> GetVocabularyAndTranslations(string language, XmlDocument xmlDocument)
        {
            List<string> vocabularyAndTranslations = new List<string>();
            XmlNodeList wordNodes = xmlDocument.SelectNodes($"//language[@name='{language}']/word");
            for (int i = 0; i < wordNodes.Count; i++)
            {
                string word = wordNodes[i].InnerText;
                vocabularyAndTranslations.Add(word);

                string translation = wordNodes[i].Attributes["translation"].Value;
                vocabularyAndTranslations.Add(translation);

                Console.WriteLine($"{word} - {translation}");
            }
            Console.WriteLine("Press any Button to Start.");
            Console.ReadKey();
            return vocabularyAndTranslations;
        }

        public List<string> getRandomVocabularyAndTranslation(List<string> VocabularyAndTranslation)
        {
            List<string> RandomVocabularyAndTranslation = new List<string>();
            Random random = new Random();
            while (RandomVocabularyAndTranslation.Count < 10)
            {
                int randomIndex = random.Next(0, VocabularyAndTranslation.Count - 1);
                if (randomIndex % 2 == 1)
                {
                    randomIndex--;
                }

                if (RandomVocabularyAndTranslation.Contains(VocabularyAndTranslation[randomIndex]))
                {
                    continue;
                }
                RandomVocabularyAndTranslation.Add(VocabularyAndTranslation[randomIndex]);
                randomIndex++;
                RandomVocabularyAndTranslation.Add(VocabularyAndTranslation[randomIndex]);
            }

            return RandomVocabularyAndTranslation;
        }
    }
}
