using System;
using System.Transactions;
using System.Xml.Linq;
using System.Runtime.CompilerServices;
[assembly: InternalsVisibleTo("KoelewijnKeanuLB-M450-MSTest")]


namespace KoelewijnKeanuLB_M450
{
    internal class Program
    {
        static void Main(string[] args)
        {
            XmlParser xmlParser = new XmlParser(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\Vocabulary.xml"));
            User user = new User(xmlParser);
            user.RunVocabularyTrainer();
        }
    }
}
