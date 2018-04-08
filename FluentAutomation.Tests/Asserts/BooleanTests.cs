using FluentAutomation.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace FluentAutomation.Tests.Asserts
{
    public class BooleanTests : AssertBaseTest
    {
        [Test]
        [Category(Category.SLOW)]
        public void True()
        {
            I.Assert.True(() => true);
            Assert.Throws<FluentException>(() => I.Assert.True(() => false));
        }

        [Test]
        [Category(Category.SLOW)]
        public void False()
        {
            I.Assert.False(() => false);
            Assert.Throws<FluentException>(() => I.Assert.False(() => true));
        }
    }
}
