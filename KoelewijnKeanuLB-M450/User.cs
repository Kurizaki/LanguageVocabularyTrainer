

namespace KoelewijnKeanuLB_M450
{
    class User
    {
        private XmlParser _xmlParser;
        private Language _language;
        private Lesson _lesson;

        public User(XmlParser xmlParser)
        {
            _xmlParser = xmlParser;
            _language = new Language(_xmlParser);
        }

        public void RunVocabularyTrainer()
        {
            do
            {
                var languageSelection = _language.ChooseLanguage();
                _lesson = new Lesson(_xmlParser.getRandomVocabularyAndTranslation(_language.VocabularyAndTranslation(languageSelection)));
                _lesson.StartLesson();
                _lesson.DisplayLessonStatistics();
            } while (_lesson.PlayAgain());
        }
    }
}
