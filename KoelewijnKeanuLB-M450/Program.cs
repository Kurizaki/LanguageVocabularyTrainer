using System;
using System.Transactions;
using System.Xml;
using System.Xml.Linq;

namespace KoelewijnKeanuLB_M450
{
    internal class Program
    {
        static void Main(string[] args)
        {
            User user = new User();
            user.RunVocabularyTrainer();
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

            return language;
        }

        public List<string> VocabularyAndTranslation(string language)
        {
            XmlDocument xmlDocument = _xmlParser.LoadXmlDocument();
            return _xmlParser.GetVocabularyAndTranslations(language, xmlDocument);
        }
    }
    class User
    {
        private XmlParser _xmlParser;
        private Language _language;
        private Lesson _lesson;

        public User()
        {
            // Initialize XmlParser
            _xmlParser = new XmlParser(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Vocabulary.xml"));

            // Initialize Language with XmlParser
            _language = new Language(_xmlParser);
        }

        public void RunVocabularyTrainer()
        {
            // Choose language
            var languageSelection = _language.ChooseLanguage();

            // Start lesson with the random vocabulary
            _lesson = new Lesson(_xmlParser.getRandomVocabularyAndTranslation(_language.VocabularyAndTranslation(languageSelection)));
            _lesson.StartLesson();
        }
    }

    interface ILesson
    {
        void StartLesson();

        bool ValidateAnswer(string answer, int index);
    }
    class Lesson : ILesson
    {
        List<string> _vocabularyAndTranslation;

        public Lesson(List<string> vocabularyAndTranslation)
        {
            _vocabularyAndTranslation = vocabularyAndTranslation;
        }

        public void StartLesson()
        {
            for (int i = 0; i < _vocabularyAndTranslation.Count; i += 2)
            {
                bool isCorrect;
                do
                {
                    Console.WriteLine($"Hmm.. Do you still know what {_vocabularyAndTranslation[i + 1]} means?");
                    isCorrect = ValidateAnswer(Console.ReadLine(), i);
                } while (!isCorrect);
            }
        }

        public bool ValidateAnswer(string answer, int index)
        {
            if (answer == _vocabularyAndTranslation[index])
            {
                Console.WriteLine("Wow You got it Right!");
                return true;
            }
            else
            {
                Console.WriteLine("Wrong Answer");
                return false;
            }
        }
    }

    //Mock um Lektionen aufzuzeichnen
    //class MockLesson : ILesson
    //{
    //}

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

                RandomVocabularyAndTranslation.Add(VocabularyAndTranslation[randomIndex]);
                Console.WriteLine(VocabularyAndTranslation[randomIndex]);
                randomIndex++;
                RandomVocabularyAndTranslation.Add(VocabularyAndTranslation[randomIndex]);
                Console.WriteLine(VocabularyAndTranslation[randomIndex]);
            }

            return RandomVocabularyAndTranslation;
        }
    }
    //Mock mit fix werte für random voci für lektionen
    //class MockXmlParser
    //{
    //}
}
