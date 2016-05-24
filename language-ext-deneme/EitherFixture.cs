using System;
using NUnit.Framework;
using LanguageExt;
using static LanguageExt.Prelude;

namespace language_ext_deneme
{
    [TestFixture]
    class EitherFixture
    {
        private static readonly Func<string, string> NameErrorMessage = (name) => $"{name} is not a suitable name";

        [Test]
        public void Should_get_suitable_name_capitalized()
        {
            match(
                CapitalizeName(GetSuitableName("barış")),
                Right: s1 => Assert.AreEqual(s1, "Barış"),
                Left: Assert.Fail
            );
        }

        [Test]
        public void Should_get_error_for_non_suitable_name()
        {
            const string name = "mustafa";
            match(
                CapitalizeName(GetSuitableName(name)),
                Right: s1 =>
                {
                    Assert.Fail(s1);
                    return s1;
                },
                Left: error => NameErrorMessage(name)
            );
        }

        private Either<string, string> CapitalizeName(Either<string, string> name)
        {
            return match(
                name,
                Right: n => Right<string, string>(char.ToUpper(n[0]) + n.Substring(1)),
                Left: Left<string, string>
            );
        }

        private Either<string, string> GetSuitableName(string name)
        {
            if (name == "barış")
                return Right<string, string>(name);

            return Left<string, string>(NameErrorMessage(name));
        }
    }
}
