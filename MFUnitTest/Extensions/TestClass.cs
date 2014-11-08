
namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    using System;
    using System.Runtime.CompilerServices;

    [Serializable]
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class TestClassAttribute : Attribute
    {
        public TestClassAttribute()
        {
        }
    }
}
