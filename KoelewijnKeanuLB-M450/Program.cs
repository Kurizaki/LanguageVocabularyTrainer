using System.Xml;
using System.Xml.Linq;

namespace KoelewijnKeanuLB_M450
{
    internal class Program
    {
        static void Main(string[] args)
        {
            XmlParser _xmlParser;
            Language _language;
            ILesson _lesson;
            _xmlParser = new XmlParser(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Vocabulary.xml"));
             _language = new Language(_xmlParser);
            var languageSelection = _language.ChooseLanguage();
            _lesson = new Lesson(_language.GetVocabulary(languageSelection), _language.GetTranslation(languageSelection));
        }
    }
    class Language
    {
        private XmlParser _xmlParser;

        public Language(XmlParser xmlParser)
        {
            _xmlParser = xmlParser;
        }
        public string ChooseLanguage()
        {
            int answer;
            string language = "";

            do
            {
                Console.WriteLine("Please Choose a Language \n 1. Spanish \n 2. Italian \n 3. Dutch");

                try
                {
                    answer = int.Parse(Console.ReadLine());

                    if (answer >= 1 && answer <= 3)
                    {
                        switch (answer)
                        {
                            case 1:
                                language = "Spanish";
                                break;
                            case 2:
                                language = "Italian";
                                break;
                            case 3:
                                language = "Dutch";
                                break;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Please enter a number between 1 and 3.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    answer = 0;
                }

            } while (answer < 1 || answer > 3);

            return (language);
        }

        public List<string> GetVocabulary(string language)
        {
            XmlDocument xmlDocument = _xmlParser.LoadXmlDocument();
            return _xmlParser.GetWords(language, xmlDocument);
        }

        public List<string> GetTranslation(string language)
        {
            XmlDocument xmlDocument = _xmlParser.LoadXmlDocument();
            return _xmlParser.GetTranslations(language, xmlDocument);
        }
    }
    class User
    {
        Language _language;
        ILesson _lesson;
    }
    interface ILesson
    {
        public void StartLesson()
        {

        }
        public bool validateAnswer(string answer) 
        {
            return false;
        }
    }
    class Lesson : ILesson
    {
        public List<string> getRandomVocabulary(List<string> words) 
        { 

            return words;
        }
        public void startLesson()
        {

        }

        bool validateAnswer(string answer)
        {
            return false;
        }

        public Lesson(List<string> vocabulary, List<string> translation)
        {

        }
    }

    class MockLesson : ILesson
    {

    }

    class XmlParser
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

        public List<string> GetWords(string language, XmlDocument xmlDocument)
        {
            List<string> words = new List<string>();

            XmlNodeList wordNodes = xmlDocument.SelectNodes($"//language[@name='{language}']/word");

            foreach (XmlNode node in wordNodes)
            {
                string word = node.InnerText;
                words.Add(word);
                Console.WriteLine(word);
            }

            return words;
        }

        public List<string> GetTranslations(string language, XmlDocument xmlDocument)
        {
            List<string> translations = new List<string>();

            XmlNodeList wordNodes = xmlDocument.SelectNodes($"//language[@name='{language}']/word");

            foreach (XmlNode node in wordNodes)
            {
                string translation = node.Attributes["translation"].Value;
                translations.Add(translation);
                Console.WriteLine(translation);
            }

            return translations;
        }
    }
}
