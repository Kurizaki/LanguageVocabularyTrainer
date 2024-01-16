

namespace KoelewijnKeanuLB_M450
{
    interface ILesson
    {
        void StartLesson();

        bool ValidateAnswer(string answer, int index);
    }
}
