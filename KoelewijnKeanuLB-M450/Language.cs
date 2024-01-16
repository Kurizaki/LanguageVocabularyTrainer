using System.Xml;

namespace KoelewijnKeanuLB_M450
{
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
                List<string> availableLanguages = _xmlParser.GetAvailableLanguages();

                Console.WriteLine("Please Choose a Language:");

                for (int i = 0; i < availableLanguages.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {availableLanguages[i]}");
                }

                try
                {
                    answer = int.Parse(Console.ReadLine());

                    if (answer >= 1 && answer <= availableLanguages.Count)
                    {
                        language = availableLanguages[answer - 1];
                    }
                    else
                    {
                        Console.WriteLine($"Invalid input. Please enter a number between 1 and {availableLanguages.Count}.");
                    }
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                    answer = 0;
                }

            } while (answer < 1 || answer > _xmlParser.GetAvailableLanguages().Count);

            return language;
        }

        public List<string> VocabularyAndTranslation(string language)
        {
            XmlDocument xmlDocument = _xmlParser.LoadXmlDocument();
            return _xmlParser.GetVocabularyAndTranslations(language, xmlDocument);
        }
    }
}
