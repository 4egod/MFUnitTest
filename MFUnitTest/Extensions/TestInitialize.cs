namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    using System;
    using System.Runtime.CompilerServices;

    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public sealed class TestInitializeAttribute : Attribute
    {
        public TestInitializeAttribute()
        {
        }
    }
}
