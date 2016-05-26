using System;
using LanguageExt;
using NUnit.Framework;
using static LanguageExt.Prelude;

namespace language_ext_deneme
{
    [TestFixture]
    class NestedNullChecksFixture
    {
        private static readonly Func<string, string> AddressErrorMessage = (name) => $"No address is specified for {name}";

        [Test]
        public void Should_get_address_city()
        {
            var realPerson = new Person("Barış") { Address = Some(new Address(Some("İzmir"))) };
            Either<string, string> result = GetCityOfPerson(realPerson);

            result.Match(
                Right: cityName => Assert.AreEqual("İzmir", cityName),
                Left: Assert.Fail
            );
        }

        [Test]
        public void Should_get_error_message_from_none_address()
        {
            var noneAddressedPerson = new Person("Barış") { Address = None };
            Either<string, string> result = GetCityOfPerson(noneAddressedPerson);

            result.Match(
                Right: Assert.Fail,
                Left: error => Assert.AreEqual(AddressErrorMessage(noneAddressedPerson.Name), error)
            );
        }

        [Test]
        public void Should_get_empty_for_none_city()
        {
            var noneAddressedPerson = new Person("Barış") { Address = Some(new Address(None)) };
            Either<string, string> result = GetCityOfPerson(noneAddressedPerson);
            
            result.Match(
                Right: s1 =>
                {
                    Assert.IsEmpty(s1);
                    return s1;
                },
                Left: s1 => 
                {
                    Assert.Fail(s1);
                    return s1;
                });
        }

        [Test]
        public void Should_get_string_or_empty()
        {
            var realPerson = new Person("Barış") { Address = Some(new Address(Some("İzmir"))) };
            Assert.AreEqual("İzmir", GetCityOfPersonOrEmpty(realPerson));

            var noneAddressedPerson = new Person("Barış") { Address = None };
            Assert.AreEqual("", GetCityOfPersonOrEmpty(noneAddressedPerson));

            noneAddressedPerson = new Person("Barış") { Address = Some(new Address(None)) };
            Assert.AreEqual("", GetCityOfPersonOrEmpty(noneAddressedPerson));
        }

        private static string GetCityOfPersonOrEmpty(Person realPerson)
        {
            return match(
                realPerson.Address,
                Some: address => address.City.IfNone(() => ""),
                None: () => ""
            );
        }

        private static Either<string, string> GetCityOfPerson(Person realPerson)
        {
            return match(
                realPerson.Address,
                Some: address => Right<string, string>(address.City.IfNone("")),
                None: () => Left<string, string>(AddressErrorMessage(realPerson.Name))
            );
        }
    }

    class Person
    {
        public Person(string name)
        {
            Name = name;
        }

        public string Name { get; }

        public Option<Address> Address { get; set; }
    }

    class Address
    {
        public Address(Option<string> city)
        {
            City = city;
        }

        public Option<string> City { get; }
    }
}
