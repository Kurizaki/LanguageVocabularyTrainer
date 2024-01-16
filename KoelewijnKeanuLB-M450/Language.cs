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

        public string ChooseLanguage(int answer = 0)
        {
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
}
