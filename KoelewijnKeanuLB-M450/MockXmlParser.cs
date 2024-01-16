using System.Xml;

namespace KoelewijnKeanuLB_M450
{
    class MockXmlParser : IXmlParser
    {
        private string _filePath;

        public MockXmlParser(string filePath)
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
            }
            return vocabularyAndTranslations;
        }

        public List<string> getRandomVocabularyAndTranslation(List<string> VocabularyAndTranslation)
        {
            List<string> notRandomVocabularyAndTranslation = new List<string>();
            int notRandomNumber = 1;
            while (notRandomVocabularyAndTranslation.Count < 10)
            {
                if (notRandomNumber % 2 == 1)
                {
                    notRandomNumber--;
                }
                if (notRandomVocabularyAndTranslation.Contains(VocabularyAndTranslation[notRandomNumber]))
                {
                    continue;
                }
                notRandomVocabularyAndTranslation.Add(VocabularyAndTranslation[notRandomNumber]);
                notRandomNumber++;
                notRandomVocabularyAndTranslation.Add(VocabularyAndTranslation[notRandomNumber]);
            }
            return notRandomVocabularyAndTranslation;
        }

        public List<string> GetAvailableLanguages()
        {
            XmlDocument xmlDocument = LoadXmlDocument();
            List<string> languages = new List<string>();

            XmlNodeList languageNodes = xmlDocument.SelectNodes("//language");
            foreach (XmlNode languageNode in languageNodes)
            {
                string languageName = languageNode.Attributes["name"].Value;
                languages.Add(languageName);
            }

            return languages;
        }
    }
}
