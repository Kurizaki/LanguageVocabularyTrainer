using System.Xml;

namespace KoelewijnKeanuLB_M450
{
    interface IXmlParser
    {
        public XmlDocument LoadXmlDocument();
        public List<string> GetVocabularyAndTranslations(string language, XmlDocument xmlDocument);
        public List<string> getRandomVocabularyAndTranslation(List<string> VocabularyAndTranslation);
    }
}
