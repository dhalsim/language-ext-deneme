// <copyright file="LanguageExtFixtureTest.cs">Copyright ©  2016</copyright>
using System;
using Microsoft.Pex.Framework;
using Microsoft.Pex.Framework.Validation;
using NUnit.Framework;
using language_ext_deneme;

namespace language_ext_deneme.Tests
{
    /// <summary>This class contains parameterized unit tests for LanguageExtFixture</summary>
    [PexClass(typeof(LanguageExtFixture))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(InvalidOperationException))]
    [PexAllowedExceptionFromTypeUnderTest(typeof(ArgumentException), AcceptExceptionSubtypes = true)]
    [TestFixture]
    public partial class LanguageExtFixtureTest
    {
        /// <summary>Test stub for Test()</summary>
        [PexMethod]
        public void TestTest([PexAssumeUnderTest]LanguageExtFixture target)
        {
            target.Test();
            // TODO: add assertions to method LanguageExtFixtureTest.TestTest(LanguageExtFixture)
        }
    }
}
