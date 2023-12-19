using System.Xml;
using System.Xml.Linq;

namespace KoelewijnKeanuLB_M450
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Language language = new Language();
            var languageSelection = language.ChooseLanguage();
            language.GetVocabulary(languageSelection);
            language.GetTranslation(languageSelection);
        }
    }
    class Language
    {
        private string _filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Vocabulary.xml");
        private XmlDocument _xmlDocument;

        public Language()
        {
            _xmlDocument = new XmlDocument();
            _xmlDocument.Load(_filePath);
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
            List<string> words = new List<string>();

            XmlNodeList wordNodes = _xmlDocument.SelectNodes($"//language[@name='{language}']/word");

            foreach (XmlNode node in wordNodes)
            {
                string word = node.InnerText;
                words.Add(word);
            }

            return words;
        }

        public List<string> GetTranslation(string language)
        {
            List<string> words = new List<string>();

            XmlNodeList wordNodes = _xmlDocument.SelectNodes($"//language[@name='{language}']/word");

            foreach (XmlNode node in wordNodes)
            {
                string translation = node.Attributes["translation"].Value;
                words.Add(translation);
            }

            return words;
        }
    }
    class User
    {
        Language _language;
        Lesson _lesson;
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
        int _languageNum;
        public List<string> getRandomVocab(List<string> words) 
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

        public Lesson(int LanguageNum)
        {

        }
    }

    class MockLesson : ILesson
    {

    }
}
