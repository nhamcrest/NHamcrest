using System;

namespace NHamcrest
{
    /// <summary>
    /// Marks a static factory method so tools can recognise them.
    /// A factory method is an equivalent to a named constructor.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class FactoryAttribute : Attribute
    {
    }
}