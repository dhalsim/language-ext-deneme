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
        private static readonly Func<string, string> LengthErrorMessage = (name) => $"{name}'s length is expected to be at least one";

        [Test]
        public void Should_get_suitable_name_capitalized()
        {
            match(
                map(GetSuitableName("barış"), CapitalizeName),
                Right: s1 => Assert.AreEqual(s1, "Barış"),
                Left: Assert.Fail
            );
        }

        [Test]
        public void Should_get_error_for_non_suitable_name()
        {
            const string name = "mustafa";

            match(
                GetSuitableName(name)
                    .Map(CapitalizeName),
                Right: s1 => Assert.Fail(),
                Left: (n) => NameErrorMessage(n)
            );
        }

        /// <summary>
        /// string -> Either&lt;string, string&gt;
        /// Capitalizes the name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private Either<string, string> CapitalizeName(string name)
        {
            if (string.IsNullOrEmpty(name) || name.Length < 2)
                return Left<string, string>(LengthErrorMessage(name));

            return Right<string, string>(char.ToUpper(name[0]) + name.Substring(1));
        }

        /// <summary>
        /// string -> Either&lt;string, string&gt;
        /// Gets the name of the suitable.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        private Either<string, string> GetSuitableName(string name)
        {
            if (name == "barış")
                return Right<string, string>(name);

            return Left<string, string>(NameErrorMessage(name));
        }
    }
}
