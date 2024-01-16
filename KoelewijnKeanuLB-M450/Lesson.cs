

namespace KoelewijnKeanuLB_M450
{
    public class Lesson : ILesson
    {
        private int _points;
        private int _tries;
        private List<string> _vocabularyAndTranslation;

        public Lesson(List<string> vocabularyAndTranslation)
        {
            _vocabularyAndTranslation = vocabularyAndTranslation;
        }

        public int GetPoints()
        {
            return _points;
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
            else if (_tries < 2)
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

        public void DisplayLessonStatistics()
        {
            Console.WriteLine("You finished this Lesson!");
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
                Console.WriteLine("Do you want to try another Lesson? [yes] [no]");
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
}
