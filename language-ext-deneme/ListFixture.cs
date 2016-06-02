using System;
using System.Collections.Generic;
using System.Linq;
using LanguageExt;
using NUnit.Framework;
using static LanguageExt.Prelude;
using static LanguageExt.List;

namespace language_ext_deneme
{
    [TestFixture]
    class ListFixture
    {
        [Test]
        public void Should_create_list()
        {
            var list = List(1, 2, 3);
            var regularList = new List<int> {1, 2, 3};

            Assert.That(regularList.SequenceEqual(list));
        }

        [Test]
        public void Should_add_to_list()
        {
            var list = List(1, 2, 3)
                .Add(4);

            var regularList = new List<int> { 1, 2, 3, 4 };

            Assert.That(regularList.SequenceEqual(list));
        }

        [Test]
        public void Should_combine_to_lists()
        {
            var list = List(1, 2, 3);
            var secondList = List(4, 5, 6);

            var regularList = new List<int> { 1, 2, 3};
            var secondRegularList = new List<int> { 4, 5, 6 };

            regularList.AddRange(secondRegularList);
            list = list.Append(secondList);

            Assert.That(regularList.SequenceEqual(list));
        }

        [Test]
        public void Should_map_list()
        {
            var list = List(1, 2, 3).Map(i => i * 2);
            var regularList = new List<int> { 2, 4, 6 };

            for (int i = 0; i < regularList.Count; i++)
            {
                Assert.AreEqual(list[i], regularList[i]);
            }

            list = map(List(1, 2, 3), i => i * 2).Freeze();

            Assert.That(regularList.SequenceEqual(list));
        }

        [Test]
        public void Should_fold_list()
        {
            var list = List(1, 2, 3, 4, 5);
            var total = list.Fold(0, (s, x) => s + x);

            Assert.AreEqual(15, total);

            total = fold(list, 0, (s, x) => s + x);

            Assert.AreEqual(15, total);
        }

        [Test]
        public void Should_reduce_list()
        {
            var list = List(1, 2, 3, 4, 5);
            var total = list.Reduce((s, x) => s + x);

            Assert.AreEqual(15, total);

            total = reduce(list, (s, x) => s + x);

            Assert.AreEqual(15, total);
        }

        [Test]
        public void Should_unfold_a_list()
        {
            var myFibonacci = take(unfold(Tuple(0, 1), (i, j) => Some(Tuple(i, j, i + j))), 15);
            var firstFifteenFibonacci = List(0, 1, 1, 2, 3, 5, 8, 13, 21, 34, 55, 89, 144, 233, 377);

            Assert.That(firstFifteenFibonacci.SequenceEqual(myFibonacci));
        }

        [Test]
        public void Should_match_list()
        {
            var lists = List(
                List(1, 2, 3, 4),
                LanguageExt.List.empty<int>(),
                List(1)
            );

            var results = List(24, 0, 1);
            var products = map(lists, Product);

            Assert.That(results.SequenceEqual(products));
        }

        private static int Product(IEnumerable<int> list) 
            => 
                match(list, 
                    () => 0,
                    (x) => x,
                    (x, xs) => x * Product(xs)
                );
    }
}
