

namespace KoelewijnKeanuLB_M450
{
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
            else if (_tries < 2)
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
                Console.WriteLine("Do you want to try another Lesson? [yes] [no]");
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
}
