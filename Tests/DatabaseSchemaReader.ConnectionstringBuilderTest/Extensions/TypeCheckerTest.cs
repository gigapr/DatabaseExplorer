using System;
using DatabaseSchemaReader.ConnectionstringBuilder.Extensions;
using DatabaseSchemaReader.Contract.BusinessObjects;
using NUnit.Framework;

namespace DatabaseSchemaReader.ConnectionstringBuilderTest.Extensions
{
    [TestFixture]
    public class TypeCheckerTest
    {
        [Test]
        public void Should_return_true_if_type_to_verify_is_as_the_one_expected()
        {
            var type = new SqlServerConnectionstringArguments();

            var result = type.IsA<SqlServerConnectionstringArguments>();

            Assert.IsTrue(result);
        }

        [Test]
        public void Should_return_false_if_type_to_verify_is_not_as_the_one_expected()
        {
            var type = new SqlServerConnectionstringArguments();

            var result = type.IsA<AccessConnectionstringArguments>();

            Assert.IsFalse(result);
        }
    }
}