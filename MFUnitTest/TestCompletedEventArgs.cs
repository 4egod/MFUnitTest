
namespace MFUnitTest
{
    using System;
    using Microsoft.SPOT;

    public class TestCompletedEventArgs : EventArgs
    {
        public TestResult[] TestResults { get; private set; }

        public TestCompletedEventArgs(TestResult[] results)
        {
            TestResults = results;
        }
    }
}
