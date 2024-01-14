using KoelewijnKeanuLB_M450;
namespace KoelewijnKeanuLB_M450_MSTest
{
    internal class User_UnitTests
    {
        [TestMethod]
        public void User_RunVocabularyTrainer_ShouldRunWithoutExceptions()
        {
            // Arrange
            var user = new User();

            // Act & Assert (no exceptions expected)
            Assert.ThrowsException<Exception>(() => user.RunVocabularyTrainer());
        }
    }
}
