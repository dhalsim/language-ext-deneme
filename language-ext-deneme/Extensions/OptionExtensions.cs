using LanguageExt;
using NUnit.Framework;

namespace language_ext_deneme.Extensions
{
    public static class OptionExtensions
    {
        public static T FailIfNone<T>(this Option<T> option)
        {
            return option.IfNone(() =>
            {
                Assert.Fail("Should not be none");
                return default(T);
            });
        }

        public static void FailIfSome<T>(this Option<T> option)
        {
            option.IfSome(x => Assert.Fail($"Should be none instead {x}"));
        }
    }
}