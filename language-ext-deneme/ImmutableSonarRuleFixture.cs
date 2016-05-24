using NUnit.Framework;
using System;
using System.Collections.Immutable;
using static LanguageExt.Prelude;

namespace language_ext_deneme
{
    [TestFixture]
    public class ImmutableSonarRuleFixture
    {
        [Test]
        public void Test2()
        {
            var list = ImmutableList.Create<int>(1, 2);
            var list2 = list.Add(3);

            Assert.AreEqual(3, list2[2]);
            Assert.AreEqual(2, list.Count);
        }

        [Test]
        public void Test3()
        {
            var dict = ImmutableDictionary.Create<string, MyType>(StringComparer.InvariantCultureIgnoreCase);
            dict = dict.Add("KEY1", new MyType(4));

            MyType value = null;

            dict.TryGetValue("key1", out value);

            if(value != null)
            {
                Assert.AreEqual(4, value.MyInt);
            }
        }
    }

    class MyType
    {
        public MyType(int myInt)
        {
            MyInt = myInt;
        }

        public int MyInt { get; set; }
    }
}
