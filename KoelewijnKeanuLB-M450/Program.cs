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
        private MockXmlParser _mockXmlParser;
        private MockLesson _mockLesson;

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
            do
            {
                _lesson = new Lesson(_xmlParser.getRandomVocabularyAndTranslation(_language.VocabularyAndTranslation(languageSelection)));
                _lesson.StartLesson();
                _lesson.LessonStatistic();
            } while (!_lesson.PlayAgain());
            
        }
    }

    interface ILesson
    {
        void StartLesson();

        bool ValidateAnswer(string answer, int index);
    }
    public class Lesson : ILesson
    {
        private int _points;
        private int _tries;
        private List<string> _vocabularyAndTranslation;

        public Lesson(List<string> vocabularyAndTranslation)
        {
            _vocabularyAndTranslation = vocabularyAndTranslation;
        }

        public void StartLesson()
        {
            _points = 0;
            Console.Clear();
            for (int i = 0; i < _vocabularyAndTranslation.Count; i += 2)
            {
                bool isCorrect;
                do
                {
                    Console.WriteLine($"What is the translation of {_vocabularyAndTranslation[i + 1]}.");
                    isCorrect = ValidateAnswer(Console.ReadLine(), i);
                } while (!isCorrect);
            }
        }

        public bool ValidateAnswer(string answer, int index)
        {
            if (answer == _vocabularyAndTranslation[index])
            {
                Console.Clear();
                switch (_tries)
                {
                    case 0: _points += 3; break;
                    case 1: _points += 2; break;
                    case 2: _points += 1; break;
                    default: _points += 0; break;
                }
                Console.WriteLine("Wow! You got it right! You earned points.");
                _tries = 0;
                return true;
            }
            else if (_tries < 3)
            {
                Console.WriteLine("Wrong answer. Try again.");
                _tries++;
            }
            else
            {
                Console.WriteLine($"Incorrect. The answer is {_vocabularyAndTranslation[index]}. Try again.");
                _tries++;
            }

            return false;
        }

        public void LessonStatistic()
        {
            if (_points == 15)
            {
                Console.WriteLine($"You got Everything Right! Excellent work, keep it up! You got {_points} points.");
            }
            else if (_points >= 10)
            {
                Console.WriteLine($"Fantastic! You're doing really well. Keep pushing! You got {_points} points.");
            }
            else if (_points >= 5)
            {
                Console.WriteLine($"Great effort! You're getting there. Keep going! You got {_points} points.");
            }
            else
            {
                Console.WriteLine($"Good try! Every attempt is a step closer to mastery. You got {_points} points.");
            }
        }

        public bool PlayAgain()
        {
            do
            {
                Console.WriteLine("Do you want to try it again? [yes] [no]");
                string answer = Console.ReadLine().ToLower();
                switch (answer)
                {
                    case "yes": return true;
                    case "no": return false;
                    default: Console.WriteLine("Please enter either 'yes' or 'no'."); break;
                }
            } while (true);
        }
    }

    //Mock um Lektionen aufzuzeichnen
    public class MockLesson : ILesson
    {
        private int _points;
        private int _tries;
        private List<string> _vocabularyAndTranslation;

        public MockLesson(List<string> vocabularyAndTranslation)
        {
            _vocabularyAndTranslation = vocabularyAndTranslation;
        }

        public void StartLesson()
        {
            Console.WriteLine($"Protocol: void StartLesson is now running");
            _points = 0;
            Console.Clear();
            for (int i = 0; i < _vocabularyAndTranslation.Count; i += 2)
            {
                bool isCorrect;
                do
                {
                    Console.WriteLine($"Hmm.. Do you still know what {_vocabularyAndTranslation[i + 1]} means?");
                    Console.WriteLine($"Protocol: Vocabulary Index is {i}");
                    isCorrect = ValidateAnswer(Console.ReadLine(), i);
                } while (!isCorrect);
            }
        }

        public bool ValidateAnswer(string answer, int index)
        {
            Console.WriteLine($"Protocol: Bool ValidateAnswer is now running");
            Console.WriteLine($"Protocol: User answer is {answer}");
            if (answer == _vocabularyAndTranslation[index])
            {
                Console.Clear();
                switch (_tries)
                {
                    case 0: _points += 3; Console.WriteLine($"Protocol: points increased by 3"); break;
                    case 1: _points += 2; Console.WriteLine($"Protocol: points increased by 2"); break;
                    case 2: _points += 1; Console.WriteLine($"Protocol: points increased by 1"); break;
                    default: _points += 0; Console.WriteLine($"Protocol: points increased by 0"); break;
                }
                Console.WriteLine("Wow! You got it right! You earned points.");
                Console.WriteLine($"Protocol: User has {_points} points");
                Console.WriteLine($"Protocol: User used {_tries} tries");

                return true;
            }
            else if (_tries < 3)
            {
                Console.WriteLine("Wrong answer. Try again.");
                _tries++;
                Console.WriteLine($"Protocol: User used {_tries} tries");
            }
            else
            {
                Console.WriteLine($"Incorrect. The answer is {_vocabularyAndTranslation[index]}. Try again.");
                _tries++;
                Console.WriteLine($"Protocol: User used {_tries} tries");
            }

            return false;
        }

        public void LessonStatistic()
        {
            Console.WriteLine($"Protocol: void LessonStatistics is now running");
            if (_points == 15)
            {
                Console.WriteLine($"You got Everything Right! Excellent work, keep it up! You got {_points} points.");
            }
            else if (_points >= 10)
            {
                Console.WriteLine($"Fantastic! You're doing really well. Keep pushing! You got {_points} points.");
            }
            else if (_points >= 5)
            {
                Console.WriteLine($"Great effort! You're getting there. Keep going! You got {_points} points.");
            }
            else
            {
                Console.WriteLine($"Good try! Every attempt is a step closer to mastery. You got {_points} points.");
            }
        }

        public bool PlayAgain()
        {
            Console.WriteLine($"Protocol: Bool PlayAgain is now running");
            do
            {
                Console.WriteLine("Do you want to try it again? [yes] [no]");
                string answer = Console.ReadLine().ToLower();
                Console.WriteLine($"Protocol: User answer is {answer}");
                switch (answer)
                {
                    case "yes": return true;
                    case "no": return false;
                    default: Console.WriteLine("Please enter either 'yes' or 'no'."); break;
                }
            } while (true);
        }
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
    class MockXmlParser
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

                Console.WriteLine($"{word} - {translation}");
            }
            Console.WriteLine("Press any Button to Start.");
            Console.ReadKey();
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

                notRandomVocabularyAndTranslation.Add(VocabularyAndTranslation[notRandomNumber]);
                Console.WriteLine(VocabularyAndTranslation[notRandomNumber]);
                notRandomNumber++;
                notRandomVocabularyAndTranslation.Add(VocabularyAndTranslation[notRandomNumber]);
                Console.WriteLine(VocabularyAndTranslation[notRandomNumber]);
            }

            return notRandomVocabularyAndTranslation;
        }
    }
}
