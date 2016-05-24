using System;
using language_ext_deneme.Extensions;
using LanguageExt;
using NUnit.Framework;
using static LanguageExt.Prelude;

namespace language_ext_deneme
{
    [TestFixture]
    public class LinqFixture
    {
        [Test]
        public void Should_Sum_Options()
        {
            var someDouble = compose(
                lpar<Option<int>, Func<Option<int>, Option<int>>, Option<int>>(map, x => x * 2),
                Some
            );

            var four = someDouble(2).FailIfNone();
            var six = someDouble(3).FailIfNone();
            Option<int> none = None;

            int result = match(
                from a in four
                from b in six
                from _ in none
                select a + b,
                Some: v => v + 1,
                None: () => 0);

            Assert.AreEqual(0, result);
        }
    }
}