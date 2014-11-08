
namespace MFUnitTest
{
    using System;

    public struct TestResult
    {
        public string TestClass { get; internal set; }

        public string TestMethod { get; internal set; }

        public TestStatus TestStatus { get; internal set; }

        public long ElapsedMilliseconds { get; internal set; }

        public Exception Exception { get; internal set; }

        public override string ToString()
        {
            string s = TestClass + "." + TestMethod + " : ";

            switch (TestStatus)
            {
                case TestStatus.Passed:
                    {
                        s += "Passed";
                        s += " (" + PrintHelper.ParseTicks(ElapsedMilliseconds) + ")";
                        break;
                    }
                case TestStatus.Failed:
                    {
                        s += "Failed";
                        if (Exception.Message != null || Exception.Message.Length != 0)
                        {
                            s += " (" + Exception.Message + ")";
                        }

                        break;
                    }
                case TestStatus.Skipped:
                    {
                        s += "Skipped";
                        if (Exception.Message != null || Exception.Message.Length != 0)
	                    {
                            s += " (" + Exception.Message + ")";
                        }
                        
                        break;
                    }
                default: s += "Undefined"; break;
            }

            return s;
        }
    }
}
