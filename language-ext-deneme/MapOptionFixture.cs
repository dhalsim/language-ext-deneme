using System;
using NUnit.Framework;
using LanguageExt;
using static LanguageExt.Prelude;

namespace language_ext_deneme
{
    [TestFixture]
    public class MapOptionFixture
    {
        [Test]
        public void Should_map_option_inline()
        {
            var result = map(Some(1), Some: i => i * 2, None: () => 0);
            Assert.AreEqual(2, result);
        }

        [Test]
        public void Should_get_none_with_map()
        {
            Func<int, int> func1 = (opt) => opt * 2;
            Func<int, int> func2 = (opt) => opt + 3;

            Option<int> optNone = None;

            Assert.True(map(optNone, func1).IsNone);
            Assert.True(map(optNone, func2).IsNone);
        }

        [Test]
        public void Should_get_none_with_map_composed()
        {
            Func<int, int> func1 = (opt) => opt * 2;
            Func<int, int> func2 = (opt) => opt + 3;

            Option<int> optNone = None;
            
            var composed = compose(func1, func2);
            Assert.True(map(optNone, composed).IsNone);

            var composedMap = lpar<Option<int>, Func<int, int>, Option<int>>(map, composed);
            Assert.That(composedMap(optNone).IsNone);

            var optSome = composedMap(2);
            Assert.That(optSome.IsSome);
            Assert.That(optSome.Equals(7));
        }

        [Test]
        public void Should_get_some_without_map()
        {
            Func<Option<int>, Option<int>> func1 = (opt) => opt * 2;
            Func<Option<int>, Option<int>> func2 = (opt) => opt + 3;

            Option<int> optNone = None;

            Assert.True(func1(optNone).IsNone);

            var option2 = func2(optNone);
            Assert.True(option2.IsSome); // None + Some(x) : Append => Some(x)
            Assert.True(option2.Equals(3)); // None + Some(x) : Append => Some(x)
        }

        [Test]
        public void Should_get_some_without_map_composed()
        {
            Func<Option<int>, Option<int>> func1 = (opt) => opt * 2;
            Func<Option<int>, Option<int>> func2 = (opt) => opt + 3;

            Option<int> optNone = None;
            
            var composed = compose(func1, func2);

            Assert.True(map(optNone, composed).IsSome);
            Assert.True(map(optNone, composed).Equals(3));

            var composedMap = lpar<Option<int>, Func<Option<int>, Option<int>>, Option<int>>(map, compose(func1, func2));
            Assert.That(composedMap(optNone).IsSome);
            Assert.That(composedMap(optNone).Equals(3));

            var optSome = composedMap(2);
            Assert.That(optSome.IsSome);
            Assert.That(optSome.Equals(7));
        }
    }
}