using System;

namespace NHamcrest
{
    /// <summary>
    /// Marks a Hamcrest static factory method so tools recognise them.
    /// A factory method is an equivalent to a named constructor.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class FactoryAttribute : Attribute
    {
    }
}