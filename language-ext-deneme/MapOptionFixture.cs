using System;
using NUnit.Framework;
using LanguageExt;
using language_ext_deneme.Extensions;
using static LanguageExt.Prelude;

namespace language_ext_deneme
{
    [TestFixture]
    public class MapOptionFixture
    {
        [Test]
        public void Should_map_option()
        {
            int result = map(Some(1), Some: i => i * 2, None: () => 0)
                .FailIfNone();

            Assert.AreEqual(2, result);
        }
    }
}