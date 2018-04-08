using FluentAutomation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace FluentAutomation.Tests.Asserts
{
    public class ThrowsTests : AssertBaseTest
    {
        private void ThrowException()
        {
            throw new FluentException("wat");
        }

        [Test]
        [Category(Category.VERYSLOW)]
        public void TestThrow()
        {
            I.Assert.Throws(() => I.Assert.True(() => false));
            I.Assert.Not.Throws(() => I.Assert.True(() => true));

            Assert.Throws<FluentException>(() => I.Assert.Throws(() => I.Assert.True(() => true)));
            Assert.Throws<FluentException>(() => I.Assert.Not.Throws(() => I.Assert.True(() => false)));

            Assert.Throws<FluentAssertFailedException>(() => With.WaitOnAllAsserts(false).Then.Assert.Throws(() => I.Assert.True(() => true)));
        }
    }
}
