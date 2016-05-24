using LanguageExt;
using NUnit.Framework;
using static LanguageExt.Prelude;

namespace language_ext_deneme
{
    [TestFixture]
    public class OptionFixture
    {
        [Test]
        public void Should_match_option()
        {
            var optional = GetSome(5);

            int result = match(optional,
                   v => v * 2,
                   () => 0);

            Assert.AreEqual(10, result);

            optional = GetNone();

            result = match(optional,
                   v => v * 2,
                   () => 0);

            Assert.AreEqual(0, result);
        }

        [Test]
        public void Should_match_none_option()
        {
            int result = match(
                GetOptionSome(None),
                x => match(x, v => v * 2, () => 0),
                () => 0
            );

            Assert.AreEqual(0, result);
        }

        private Option<Option<int>> GetOptionSome(Option<int> x)
        {
            return Some(x);
        }

        private Option<int> GetSome(int x)
        {
            return Some(x);
        }

        private Option<int> GetNone()
        {
            return None;
        }
    }
}