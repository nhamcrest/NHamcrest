using System;
using System.Collections.Generic;
using NHamcrest.Tests.TestClasses;

namespace NHamcrest.Tests
{
    public class NullMustBeStructurallyEqualToNull : StructuralComparisonMatcherFactorySuccessTest<object>
    {
        protected override object CreateValue()
        {
            return null;
        }

        protected override string ExpectMatcherDescription()
        {
            return "null";
        }
    }

    public class NullMustNotBeStructurallyEqualToObject : StructuralComparisonMatcherFactoryFailureTest<object>
    {
        protected override string ExpectMatcherDescription()
        {
            return "null";
        }

        protected override object CreateExampleValue()
        {
            return null;
        }

        protected override object CreateMatchedValue()
        {
            return new object();
        }

        protected override string ExpectMismatchDescription()
        {
            return "was System.Object";
        }
    }

    public class ObjectMustNotBeStructurallyEqualToNull : StructuralComparisonMatcherFactoryFailureTest<object>
    {
        protected override string ExpectMatcherDescription()
        {
            return "a(n) Object";
        }

        protected override object CreateExampleValue()
        {
            return new object();
        }

        protected override object CreateMatchedValue()
        {
            return null;
        }

        protected override string ExpectMismatchDescription()
        {
            return "was null";
        }
    }

    public class IntMustBeEqualToItself : StructuralComparisonMatcherFactorySuccessTest<int>
    {
        protected override int CreateValue()
        {
            return 457;
        }

        protected override string ExpectMatcherDescription()
        {
            return "457";
        }
    }

    public class SimpleFlatClassInterfaceMustBeEqualToItself : StructuralComparisonMatcherFactorySuccessTest<ISimpleFlatClass>
    {
        protected override ISimpleFlatClass CreateValue()
        {
            return new SimpleFlatClass
            {
                IntProperty = 14,
                StringProperty = "foo"
            };
        }

        protected override string ExpectMatcherDescription()
        {
            return $"a(n) ISimpleFlatClass {{SimpleFlatClass}} where:{Environment.NewLine}" +
                $"    member IntProperty value is 14{Environment.NewLine}" +
                "    member StringProperty value is \"foo\"";
        }
    }
    
    public class SimpleFlatClassMustBeEqualToItself : StructuralComparisonMatcherFactorySuccessTest<SimpleFlatClass>
    {
        protected override SimpleFlatClass CreateValue()
        {
            return new SimpleFlatClass
            {
                IntProperty = 14,
                StringProperty = "foo"
            };
        }

        protected override string ExpectMatcherDescription()
        {
            return $"a(n) SimpleFlatClass where:{Environment.NewLine}" +
                $"    member IntProperty value is 14{Environment.NewLine}" +
                "    member StringProperty value is \"foo\"";
        }
    }


    public class DifferentSameInterfaceImplementationsMustBeEqualAsInterfaces : StructuralComparisonMatcherFactorySuccessTest<ISimpleFlatClass>
    {
        protected override ISimpleFlatClass CreateValue()
        {
            return new SimpleFlatClass
            {
                IntProperty = 14,
                StringProperty = "foo"
            };
        }

        protected override ISimpleFlatClass CreateMatchedValue()
        {
            return new AnotherSimpleFlatClass
            {
                IntProperty = 14,
                StringProperty = "foo"
            };
        }

        protected override string ExpectMatcherDescription()
        {
            return $"a(n) ISimpleFlatClass {{SimpleFlatClass}} where:{Environment.NewLine}" +
                $"    member IntProperty value is 14{Environment.NewLine}" +
                "    member StringProperty value is \"foo\"";
        }
    }

    public class DifferentSimpleFlatClassesMustNotBeEqual : StructuralComparisonMatcherFactoryFailureTest<SimpleFlatClass>
    {
        protected override SimpleFlatClass CreateExampleValue()
        {
            return new SimpleFlatClass
            {
                IntProperty = 14,
                StringProperty = "foo"
            };
        }

        protected override string ExpectMatcherDescription()
        {
            return $"a(n) SimpleFlatClass where:{Environment.NewLine}" +
                $"    member IntProperty value is 14{Environment.NewLine}" +
                "    member StringProperty value is \"foo\"";
        }

        protected override SimpleFlatClass CreateMatchedValue()
        {
            return new SimpleFlatClass
            {
                IntProperty = 1,
                StringProperty = "bar"
            };
        }

        protected override string ExpectMismatchDescription()
        {
            return $"was a(n) SimpleFlatClass where:{Environment.NewLine}" +
                $"    member IntProperty value was 1{Environment.NewLine}" +
                "    member StringProperty value was \"bar\"";
        }
    }

    public class NestedClassMustBeEqualToItself : StructuralComparisonMatcherFactorySuccessTest<NestedClass>
    {
        protected override NestedClass CreateValue()
        {
            return new NestedClass
            {
                InnerClass = new SimpleFlatClass
                {
                    IntProperty = 14,
                    StringProperty = "foo"
                },
                SomeNumber = 14.3m
            };
        }

        protected override string ExpectMatcherDescription()
        {
            return $"a(n) NestedClass where:{Environment.NewLine}" +
                $"    member InnerClass value is a(n) SimpleFlatClass where:{Environment.NewLine}" +
                $"        member IntProperty value is 14{Environment.NewLine}" +
                $"        member StringProperty value is \"foo\"{Environment.NewLine}" +
                "    member SomeNumber value is 14.3m";
        }
    }

    public class DifferentNestedClassesMustNotBeEqual : StructuralComparisonMatcherFactoryFailureTest<NestedClass>
    {
        protected override NestedClass CreateExampleValue()
        {
            return new NestedClass
            {
                InnerClass = new SimpleFlatClass
                {
                    IntProperty = 14,
                    StringProperty = "foo"
                },
                SomeNumber = 14.3m
            };
        }

        protected override string ExpectMatcherDescription()
        {
            return $"a(n) NestedClass where:{Environment.NewLine}" +
                $"    member InnerClass value is a(n) SimpleFlatClass where:{Environment.NewLine}" +
                $"        member IntProperty value is 14{Environment.NewLine}" +
                $"        member StringProperty value is \"foo\"{Environment.NewLine}" +
                "    member SomeNumber value is 14.3m";
        }

        protected override NestedClass CreateMatchedValue()
        {
            return new NestedClass
            {
                InnerClass = new SimpleFlatClass
                {
                    IntProperty = 14,
                    StringProperty = "bar"
                },
                SomeNumber = 14.3m
            };
        }

        protected override string ExpectMismatchDescription()
        {
            return $"was a(n) NestedClass where:{Environment.NewLine}" +
                $"    member InnerClass value was a(n) SimpleFlatClass where:{Environment.NewLine}" +
                "        member StringProperty value was \"bar\"";
        }
    }

    public class ClassWithArrayOfClassesMustBeEqualToItself : StructuralComparisonMatcherFactorySuccessTest<ClassWithArrayOfClasses>
    {
        protected override ClassWithArrayOfClasses CreateValue()
        {
            return new ClassWithArrayOfClasses
            {
                Id = new Guid("3617cbea-bc78-4aa6-84a5-8364234a2c67"),
                OtherThings = new[]
                {
                    new SimpleFlatClass
                    {
                        IntProperty = 3,
                        StringProperty = "foo"
                    },
                    new SimpleFlatClass
                    {
                        IntProperty = 7,
                        StringProperty = null
                    }
                }
            };
        }

        protected override string ExpectMatcherDescription()
        {
            return $"a(n) ClassWithArrayOfClasses where:{Environment.NewLine}" +
                $"    member Id value is 3617cbea-bc78-4aa6-84a5-8364234a2c67{Environment.NewLine}" +
                $"    member OtherThings value is a list containing:{Environment.NewLine}" +
                $"        a(n) SimpleFlatClass where:{Environment.NewLine}" +
                $"            member IntProperty value is 3{Environment.NewLine}" +
                $"            member StringProperty value is \"foo\",{Environment.NewLine}" +
                $"        a(n) SimpleFlatClass where:{Environment.NewLine}" +
                $"            member IntProperty value is 7{Environment.NewLine}" +
                "            member StringProperty value is null";
        }
    }

    public class DifferentClassesWithArrayOfClassesMustNotBeEqual : StructuralComparisonMatcherFactoryFailureTest<ClassWithArrayOfClasses>
    {
        protected override ClassWithArrayOfClasses CreateExampleValue()
        {
            return new ClassWithArrayOfClasses
            {
                Id = new Guid("3617cbea-bc78-4aa6-84a5-8364234a2c67"),
                OtherThings = new[]
                {
                    new SimpleFlatClass
                    {
                        IntProperty = 3,
                        StringProperty = "foo"
                    },
                    new SimpleFlatClass
                    {
                        IntProperty = 7,
                        StringProperty = null
                    }
                }
            };
        }

        protected override string ExpectMatcherDescription()
        {
            return $"a(n) ClassWithArrayOfClasses where:{Environment.NewLine}" +
                $"    member Id value is 3617cbea-bc78-4aa6-84a5-8364234a2c67{Environment.NewLine}" +
                $"    member OtherThings value is a list containing:{Environment.NewLine}" +
                $"        a(n) SimpleFlatClass where:{Environment.NewLine}" +
                $"            member IntProperty value is 3{Environment.NewLine}" +
                $"            member StringProperty value is \"foo\",{Environment.NewLine}" +
                $"        a(n) SimpleFlatClass where:{Environment.NewLine}" +
                $"            member IntProperty value is 7{Environment.NewLine}" +
                "            member StringProperty value is null";
        }

        protected override ClassWithArrayOfClasses CreateMatchedValue()
        {
            return new ClassWithArrayOfClasses
            {
                Id = new Guid("3617cbea-bc78-4aa6-84a5-8364234a2c67"),
                OtherThings = new[]
                {
                    new SimpleFlatClass
                    {
                        IntProperty = 3,
                        StringProperty = "foo"
                    }
                }
            };
        }

        protected override string ExpectMismatchDescription()
        {
            return $"was a(n) ClassWithArrayOfClasses where:{Environment.NewLine}" +
                "    member OtherThings value was too short (expected to be of length 2, was 1)";
        }
    }

    public class ClassWithListOfClassesMustBeEqualToItself : StructuralComparisonMatcherFactorySuccessTest<ClassWithListOfClasses>
    {
        protected override ClassWithListOfClasses CreateValue()
        {
            return new ClassWithListOfClasses
            {
                IntValue = 7,
                OtherThings = new List<SimpleFlatClass>()
                {
                    new SimpleFlatClass
                    {
                        IntProperty = 3,
                        StringProperty = "foo"
                    },
                    new SimpleFlatClass
                    {
                        IntProperty = 7,
                        StringProperty = null
                    }
                }
            };
        }

        protected override string ExpectMatcherDescription()
        {
            return $"a(n) ClassWithListOfClasses where:{Environment.NewLine}" +
                $"    member IntValue value is 7{Environment.NewLine}" +
                $"    member OtherThings value is a list containing:{Environment.NewLine}" +
                $"        a(n) SimpleFlatClass where:{Environment.NewLine}" +
                $"            member IntProperty value is 3{Environment.NewLine}" +
                $"            member StringProperty value is \"foo\",{Environment.NewLine}" +
                $"        a(n) SimpleFlatClass where:{Environment.NewLine}" +
                $"            member IntProperty value is 7{Environment.NewLine}" +
                "            member StringProperty value is null";
        }
    }

    public class DifferentClassesWithListOfClassesMustNotBeEqual : StructuralComparisonMatcherFactoryFailureTest<ClassWithListOfClasses>
    {
        protected override ClassWithListOfClasses CreateExampleValue()
        {
            return new ClassWithListOfClasses
            {
                IntValue = 45,
                OtherThings = new List<SimpleFlatClass>()
                {
                    new SimpleFlatClass
                    {
                        IntProperty = 3,
                        StringProperty = "foo"
                    },
                    new SimpleFlatClass
                    {
                        IntProperty = 7,
                        StringProperty = null
                    }
                }
            };
        }

        protected override string ExpectMatcherDescription()
        {
            return $"a(n) ClassWithListOfClasses where:{Environment.NewLine}" +
                $"    member IntValue value is 45{Environment.NewLine}" +
                $"    member OtherThings value is a list containing:{Environment.NewLine}" +
                $"        a(n) SimpleFlatClass where:{Environment.NewLine}" +
                $"            member IntProperty value is 3{Environment.NewLine}" +
                $"            member StringProperty value is \"foo\",{Environment.NewLine}" +
                $"        a(n) SimpleFlatClass where:{Environment.NewLine}" +
                $"            member IntProperty value is 7{Environment.NewLine}" +
                "            member StringProperty value is null";
        }

        protected override ClassWithListOfClasses CreateMatchedValue()
        {
            return new ClassWithListOfClasses
            {
                IntValue = 45,
                OtherThings = new List<SimpleFlatClass>()
                {
                    new SimpleFlatClass
                    {
                        IntProperty = 3,
                        StringProperty = "foo"
                    },
                    new SimpleFlatClass
                    {
                        IntProperty = 7,
                        StringProperty = "bar"
                    }
                }
            };
        }

        protected override string ExpectMismatchDescription()
        {
            return $"was a(n) ClassWithListOfClasses where:{Environment.NewLine}" +
                $"    member OtherThings value was not matched at position 1:{Environment.NewLine}" +
                $"        expected: a(n) SimpleFlatClass where:{Environment.NewLine}" +
                $"            member IntProperty value is 7{Environment.NewLine}" +
                $"            member StringProperty value is null{Environment.NewLine}" +
                $"        but: was a(n) SimpleFlatClass where:{Environment.NewLine}" +
                "            member StringProperty value was \"bar\"";
        }
    }

    public class ClassWithEnumerableOfClassesMustBeEqualToItself : StructuralComparisonMatcherFactorySuccessTest<ClassWithEnumerableOfClasses>
    {
        protected override ClassWithEnumerableOfClasses CreateValue()
        {
            return new ClassWithEnumerableOfClasses
            {
                IntValue = 7,
                OtherThings = new List<SimpleFlatClass>()
                {
                    new SimpleFlatClass
                    {
                        IntProperty = 3,
                        StringProperty = "foo"
                    },
                    new SimpleFlatClass
                    {
                        IntProperty = 7,
                        StringProperty = null
                    }
                }
            };
        }

        protected override string ExpectMatcherDescription()
        {
            return $"a(n) ClassWithEnumerableOfClasses where:{Environment.NewLine}" +
                $"    member IntValue value is 7{Environment.NewLine}" +
                $"    member OtherThings value is a list containing:{Environment.NewLine}" +
                $"        a(n) SimpleFlatClass where:{Environment.NewLine}" +
                $"            member IntProperty value is 3{Environment.NewLine}" +
                $"            member StringProperty value is \"foo\",{Environment.NewLine}" +
                $"        a(n) SimpleFlatClass where:{Environment.NewLine}" +
                $"            member IntProperty value is 7{Environment.NewLine}" +
                "            member StringProperty value is null";
        }
    }

    public class DifferentClassesWithEnumerableOfClassesMustNotBeEqual : StructuralComparisonMatcherFactoryFailureTest<ClassWithEnumerableOfClasses>
    {
        protected override ClassWithEnumerableOfClasses CreateExampleValue()
        {
            return new ClassWithEnumerableOfClasses
            {
                IntValue = 45,
                OtherThings = new List<SimpleFlatClass>()
                {
                    new SimpleFlatClass
                    {
                        IntProperty = 3,
                        StringProperty = "foo"
                    },
                    new SimpleFlatClass
                    {
                        IntProperty = 7,
                        StringProperty = null
                    }
                }
            };
        }

        protected override string ExpectMatcherDescription()
        {
            return $"a(n) ClassWithEnumerableOfClasses where:{Environment.NewLine}" +
                $"    member IntValue value is 45{Environment.NewLine}" +
                $"    member OtherThings value is a list containing:{Environment.NewLine}" +
                $"        a(n) SimpleFlatClass where:{Environment.NewLine}" +
                $"            member IntProperty value is 3{Environment.NewLine}" +
                $"            member StringProperty value is \"foo\",{Environment.NewLine}" +
                $"        a(n) SimpleFlatClass where:{Environment.NewLine}" +
                $"            member IntProperty value is 7{Environment.NewLine}" +
                "            member StringProperty value is null";
        }

        protected override ClassWithEnumerableOfClasses CreateMatchedValue()
        {
            return new ClassWithEnumerableOfClasses
            {
                IntValue = 45,
                OtherThings = new List<SimpleFlatClass>()
                {
                    new SimpleFlatClass
                    {
                        IntProperty = 3,
                        StringProperty = "foo"
                    },
                    new SimpleFlatClass
                    {
                        IntProperty = 7,
                        StringProperty = null
                    },
                    new SimpleFlatClass
                    {
                        IntProperty = 1,
                        StringProperty = "bar"
                    }
                }
            };
        }

        protected override string ExpectMismatchDescription()
        {
            return $"was a(n) ClassWithEnumerableOfClasses where:{Environment.NewLine}" +
                "    member OtherThings value was too long (expected to be of length 2, was 3)";
        }
    }

    public class ClassWithDictionaryOfClassesMustBeEqualToItself : StructuralComparisonMatcherFactorySuccessTest<ClassWithDictionaryOfClasses>
    {
        protected override ClassWithDictionaryOfClasses CreateValue()
        {
            return new ClassWithDictionaryOfClasses
            {
                StringValue = "foo",
                Map = new Dictionary<string, SimpleFlatClass>
                {
                    {
                        "bar", new SimpleFlatClass
                        {
                            IntProperty = 2,
                            StringProperty = "baz"
                        }
                    },
                    {
                        "quux", new SimpleFlatClass
                        {
                            IntProperty = 1,
                            StringProperty = null
                        }
                    }
                }
            };
        }

        protected override string ExpectMatcherDescription()
        {
            return $"a(n) ClassWithDictionaryOfClasses where:{Environment.NewLine}" +
                $"    member Map value is a dictionary consisting of:{Environment.NewLine}" +
                $"        an entry where:{Environment.NewLine}" +
                $"            key: \"bar\"{Environment.NewLine}" +
                $"            value: a(n) SimpleFlatClass where:{Environment.NewLine}" +
                $"                member IntProperty value is 2{Environment.NewLine}" +
                $"                member StringProperty value is \"baz\",{Environment.NewLine}" +
                $"        an entry where:{Environment.NewLine}" +
                $"            key: \"quux\"{Environment.NewLine}" +
                $"            value: a(n) SimpleFlatClass where:{Environment.NewLine}" +
                $"                member IntProperty value is 1{Environment.NewLine}" +
                $"                member StringProperty value is null{Environment.NewLine}" +
                "    member StringValue value is \"foo\"";
        }
    }

    public class DifferentClassesWithDictionaryOfClassesMustNotBeEqual : StructuralComparisonMatcherFactoryFailureTest<ClassWithDictionaryOfClasses>
    {
        protected override ClassWithDictionaryOfClasses CreateExampleValue()
        {
            return new ClassWithDictionaryOfClasses
            {
                StringValue = "whatever",
                Map = new Dictionary<string, SimpleFlatClass>
                {
                    {
                        "key2", new SimpleFlatClass
                        {
                            IntProperty = 7,
                            StringProperty = "anything"
                        }
                    },
                    {
                        "key1", new SimpleFlatClass
                        {
                            IntProperty = 44,
                            StringProperty = "baz"
                        }
                    }
                }
            };
        }

        protected override string ExpectMatcherDescription()
        {
            return $"a(n) ClassWithDictionaryOfClasses where:{Environment.NewLine}" +
                $"    member Map value is a dictionary consisting of:{Environment.NewLine}" +
                $"        an entry where:{Environment.NewLine}" +
                $"            key: \"key2\"{Environment.NewLine}" +
                $"            value: a(n) SimpleFlatClass where:{Environment.NewLine}" +
                $"                member IntProperty value is 7{Environment.NewLine}" +
                $"                member StringProperty value is \"anything\",{Environment.NewLine}" +
                $"        an entry where:{Environment.NewLine}" +
                $"            key: \"key1\"{Environment.NewLine}" +
                $"            value: a(n) SimpleFlatClass where:{Environment.NewLine}" +
                $"                member IntProperty value is 44{Environment.NewLine}" +
                $"                member StringProperty value is \"baz\"{Environment.NewLine}" +
                "    member StringValue value is \"whatever\"";
        }

        protected override ClassWithDictionaryOfClasses CreateMatchedValue()
        {
            return new ClassWithDictionaryOfClasses
            {
                StringValue = "whatever",
                Map = new Dictionary<string, SimpleFlatClass>
                {
                    {
                        "key1", new SimpleFlatClass
                        {
                            IntProperty = 43,
                            StringProperty = "baz"
                        }
                    }
                }
            };
        }

        protected override string ExpectMismatchDescription()
        {
            return $"was a(n) ClassWithDictionaryOfClasses where:{Environment.NewLine}" +
                $"    member Map value was a dictionary that:{Environment.NewLine}" +
                $"        had a different entry where:{Environment.NewLine}" +
                $"            key: \"key1\"{Environment.NewLine}" +
                $"            value: was a(n) SimpleFlatClass where:{Environment.NewLine}" +
                $"                member IntProperty value was 43,{Environment.NewLine}" +
                $"        did not have an entry where:{Environment.NewLine}" +
                $"            key: \"key2\"{Environment.NewLine}" +
                $"            value: a(n) SimpleFlatClass where:{Environment.NewLine}" +
                $"                member IntProperty value is 7{Environment.NewLine}" +
                "                member StringProperty value is \"anything\"";
        }
    }
}