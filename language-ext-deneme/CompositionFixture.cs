using System;
using LanguageExt;
using NUnit.Framework;
using static LanguageExt.Prelude;

namespace language_ext_deneme
{
    [TestFixture]
    class CompositionFixture
    {
        [Test]
        public void Should_compose_basic()
        {
            var composed = compose<int, int, int>(x => x * 2, x => x + 1);
            var secondComposed = compose(composed, x => x - 1);
            Assert.AreEqual(8, secondComposed(4));
        }

        [Test]
        public void Should_compose_option()
        {
            var composed = compose<int, Option<int>, int>(GetSome, GetSome2);
            Assert.AreEqual(5, composed(5));

            var composed2 = compose<int, Option<int>, int>(GetNone, GetSome2);
            Assert.AreEqual(0, composed2(5));
        }

        private Option<int> GetSome(int x)
        {
            return Some(x);
        }

        private Option<int> GetNone(int x)
        {
            return None;
        }

        private int GetSome2(Option<int> arg)
        {
            return match(arg, v => v, () => 0);
        }
    }
}
