
namespace MFUnitTest
{
    using System;
    using System.Collections;
    using System.Diagnostics;
    using System.Reflection;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class TestManager
    {
        private static ArrayList classResults;

        private static ArrayList results;
        static TestManager()
        {
            TestResults = null;
            results = new ArrayList();
            classResults = new ArrayList();
            NamingPattern = "Test";
            NamingRule = NamingRules.Anywhere;
            TestInitializeMethod = "Initialize";
            TestCleanupMethod = "Cleanup";
        }

        public static string NamingPattern { get; set; }

        public static NamingRules NamingRule { get; set; } 

        public static string TestInitializeMethod { get; set; }

        public static string TestCleanupMethod { get; set; }

        public static TestResult[] TestResults { get; private set; }

        public static void RunTests(Assembly assembly)
        {
            if (assembly == null)
            {
                throw new ArgumentNullException("assembly");
            }

            TestResults = new TestResult[] { };
            results.Clear();
            var types = assembly.GetTypes();
            foreach (var type in types)
            {
                int index;

                switch (TestManager.NamingRule)
                {
                    case NamingRules.Prefix:
                        {
                            index = type.Name.IndexOf(TestManager.NamingPattern);
                            
                            if (index != 0)
                            {
                                index = -1;
                            }

                            break;
                        }

                    case NamingRules.Suffix:
                        {
                            index = type.Name.LastIndexOf(TestManager.NamingPattern);

                            if (index != (type.Name.Length - TestManager.NamingPattern.Length))
                            {
                                index = -1;
                            }

                            break;
                        }
                        
                    default:
                        {
                            index = type.Name.IndexOf(TestManager.NamingPattern);
                            break;
                        }    
                }
                
                if (index > -1)
                {
                    InternalRunTest(type, string.Empty);
                }
            }

            OnTestsCompleted();
        }

        public static void RunTest(Type type)
        {
            RunTest(type, string.Empty);
        }

        public static void RunTest(Type type, string methodName)
        {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }

            if (methodName == null)
            {
                throw new ArgumentNullException("methodName");
            }

            TestResults = new TestResult[] { };
            results.Clear();
            InternalRunTest(type, methodName);
            OnTestsCompleted();
        }

        private static void InternalRunTest(Type type, string methodName)
        {
            classResults.Clear();

            TestStatus status;
            Exception e;

            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.Instance);
            object instance = type.GetConstructor(new Type[] { }).Invoke(new object[] { });

            int initializeIndex = -1;
            int cleanupIndex = -1;
            int customMethodIndex = -1;

            for (int i = 0; i < methods.Length; i++)
            {
                if (methods[i].Name == TestInitializeMethod)
                {
                    initializeIndex = i;
                    continue;
                }

                if (methods[i].Name == TestCleanupMethod)
                {
                    cleanupIndex = i;
                    continue;
                }

                if (methods[i].Name == methodName)
                {
                    customMethodIndex = i;
                }
            }

            if (methodName != string.Empty && customMethodIndex == -1)
            {
                methods = new MethodInfo[] { };
            }

            if ((methodName != null) && (methodName != string.Empty) && (customMethodIndex == -1))
            {
                return;
            }

            if (initializeIndex != -1)
            {
                TestMethod(methods[initializeIndex], instance, out status, out e);
            }

            int j = 0, k = 0;
            if (customMethodIndex != -1)
            {
                j = customMethodIndex;
                k = j + 1;
            }
            else
            {
                j = 0;
                k = methods.Length;
            }

            for (int i = j; i < k; i++)
            {
                int index;

                switch (TestManager.NamingRule)
                {
                    case NamingRules.Prefix:
                        {
                            index = methods[i].Name.IndexOf(TestManager.NamingPattern);

                            if (index != 0)
                            {
                                index = -1;
                            }

                            break;
                        }

                    case NamingRules.Suffix:
                        {
                            index = methods[i].Name.LastIndexOf(TestManager.NamingPattern);

                            if (index != (methods[i].Name.Length - TestManager.NamingPattern.Length))
                            {
                                index = -1;
                            }

                            break;
                        }

                    default:
                        {
                            index = methods[i].Name.IndexOf(TestManager.NamingPattern);
                            break;
                        }
                }
        

                if ((index > -1) || (customMethodIndex > -1))
                {
                    Stopwatch sw = Stopwatch.StartNew();

                    TestMethod(methods[i], instance, out status, out e);

                    sw.Stop();

                    classResults.Add(new TestResult()
                    {
                        TestClass = type.Name,
                        TestMethod = methods[i].Name,
                        TestStatus = status,
                        Exception = e,
                        ElapsedMilliseconds = sw.ElapsedMilliseconds
                    });
                }                
            }

            foreach (var item in classResults)
            {
                results.Add(item);
            }

            if (cleanupIndex != -1)
            {
                TestMethod(methods[cleanupIndex], instance, out status, out e);
            }

            OnTestCompleted();
        }
    
        private static void TestMethod(MethodInfo method, object instance, out TestStatus status, out Exception e)
        {
            try
            {
                method.Invoke(instance, new object[] { });
                status = TestStatus.Passed;
                e = null;
            }
            catch (AssertInconclusiveException ex)
            {
                status = TestStatus.Skipped;
                e = ex;
            }
            catch (Exception ex)
            {
                status = TestStatus.Failed;
                e = ex;
            }
        }

        private static void OnTestCompleted()
        {
            TestResult[] res  = (TestResult[])classResults.ToArray(typeof(TestResult));

            if (TestCompleted != null) TestCompleted(new TestCompletedEventArgs(res));

            //Microsoft.SPOT.Debug.GC(true);
        }

        private static void OnTestsCompleted()
        {
            TestResults = (TestResult[])results.ToArray(typeof(TestResult));

            if (TestsCompleted != null) TestsCompleted(new TestCompletedEventArgs(TestResults));

            PrintHelper.Print(TestResults);
        }

        public static event TestCompletedHandler TestCompleted;

        public static event TestsCompletedHandler TestsCompleted;

        public delegate void TestCompletedHandler(TestCompletedEventArgs args);

        public delegate void TestsCompletedHandler(TestCompletedEventArgs args);
    }
}
