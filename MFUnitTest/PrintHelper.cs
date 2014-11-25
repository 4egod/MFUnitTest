
namespace MFUnitTest
{
    using System;
    using Microsoft.SPOT;

    internal static class PrintHelper
    {
        internal static void Print(TestResult[] results)
        {
            int classCount = 0;
            int p = 0, f = 0, s = 0;
            long ticks = 0;

            string cs = string.Empty;

            foreach (var item in results)
            {
                if (String.Compare(cs, item.TestClass) != 0)
                {
                    cs = item.TestClass;
                    classCount += 1;
                }

                switch (item.TestStatus)
                {
                    case TestStatus.Passed:
                        {
                            p += 1;
                            ticks += item.ElapsedMilliseconds;
                            break;
                        }
                    case TestStatus.Failed: f += 1; break;
                    case TestStatus.Skipped: s += 1; break;
                    default: break;
                }
            }

            if (p == 0)
            {
                ticks = 0;
            }
            else
            {
                ticks /= p;
            }

            Debug.Print("\n");
            Debug.Print("========================= Micro Framework Unit Test =========================");

            foreach (var item in results)
            {
                Debug.Print(item.ToString());
            }

            Debug.Print("=============================================================================");
            Debug.Print("Test classes count: " + classCount.ToString());
            Debug.Print("Test methods count: " + results.Length.ToString());
            Debug.Print("Passed tests: " + p.ToString());
            Debug.Print("Failed tests: " + f.ToString());
            Debug.Print("Skipped tests: " + s.ToString());
            Debug.Print("Average time: " + ParseTicks(ticks));
            Debug.Print("=============================================================================");
            Debug.Print("\n");
        }

        internal static string ParseTicks(long ms)
        {
            if (ms <= 0)
            {
                return "< 1 ms";
            }

            if (ms < 1000)
            {
                return ms.ToString() + " ms";
            }

            double d;

            if (ms < 60000)
            {
                d = ms / 1000.0;
                return d.ToString("F2") + " s";
            }

            if (ms < 3600000)
            {
                d = ms / 60000.0;
                return d.ToString("F2") + " m";
            }

            return "> 1 h";
        }
    }
}
