using Xunit;
using NHAssert = NHamcrest.Tests.Internal.Assert;
using static NHamcrest.Extensions.IsExtensions;

namespace NHamcrest.Tests.Core
{
    public class IsExtensionsTests
    {
        [Fact]
        public void Match_if_bool_is_equal_with_IsExtension()
        {
            NHAssert.That(true, Is(true));
        }

        [Fact]
        public void Match_if_string_is_equal_with_IsExtension()
        {
            NHAssert.That("test", Is("test"));
        }

        [Fact]
        public void Match_if_int_is_equal_with_IsExtension()
        {
            NHAssert.That(1, Is(1));
        }

        [Fact]
        public void Match_if_decimal_is_equal_with_IsExtension()
        {
            NHAssert.That(1m, Is(1m));
        }
    }
}
