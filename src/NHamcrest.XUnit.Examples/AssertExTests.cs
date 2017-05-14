using System;
using System.Collections.Generic;
using System.Linq;
using NHamcrest.Core;
using Xunit;

namespace NHamcrest.XUnit.Examples
{
    public class AssertExTests
    {
        [Fact]
        public void Pass()
        {
            Assert.That(1, Is.EqualTo(1));

            //Assert.That(new[] { 1, 2, 3, 4, 5 }, Is.ListOf(new[] { 1, 2, 3 }.Select(x => Is.EqualTo(x))));

            var rand = new Random();

            var actual = new ClassWithEnumerableOfClasses()
            {
                IntValue = rand.Next(),
                OtherThings = Enumerable.Range(0, 3)
                    .Select(x => new SimpleFlatClass()
                    {
                        IntProperty = rand.Next(),
                        StringProperty = Guid.NewGuid().ToString()
                    })
            };

            Assert.That(actual, Describe.Object<ClassWithEnumerableOfClasses>()
                .Property(x => x.OtherThings, Is.SetOf(new[]
                {
                    Describe.Object<SimpleFlatClass>()
                        .Property(x=>x.IntProperty, Is.GreaterThan(5)),
                    Describe.Object<SimpleFlatClass>()
                    .Property(x=>x.IntProperty, Is.EqualTo(5))
                })));

            //Assert.That(actual, Describe.Object<ClassWithEnumerableOfClasses>()
            //    .Property(x => x.OtherThings, Is.ListOf(new[]
            //    {
            //        Describe.Object<SimpleFlatClass>()
            //            .Property(x=>x.IntProperty, Is.GreaterThan(5)),
            //        Describe.Object<SimpleFlatClass>()
            //            .Property(x=>x.IntProperty, Is.EqualTo(18))
            //    })));

        }


        public class ClassWithEnumerableOfClasses
        {
            public int IntValue { get; set; }
            public IEnumerable<SimpleFlatClass> OtherThings { get; set; }
        }

        public class SimpleFlatClass
        {
            public int IntProperty { get; set; }
            public string StringProperty { get; set; }
        }

        [Fact]
        public void Fail()
        {
            Assert.That(1, Is.EqualTo(3));
        }

        [Fact]
        public void One_more()
        {
            Assert.That(() => { throw new InvalidOperationException(); }, Throws.An<NotSupportedException>());
        }
    }
}
