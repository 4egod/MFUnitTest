
namespace Microsoft.VisualStudio.TestTools.UnitTesting
{
    using System;
    using System.Diagnostics;
    using System.Reflection;

    public static class Assert
    {
        public static void AreEqual(object expected, object actual)
        {
            if (!InternalEqual(expected, actual))
            {
                Assert.Fail("Assert.AreEqual", expected, actual);
            }
        }

        public static void AreNotEqual(object expected, object actual)
        {
            if (InternalEqual(expected, actual))
            {
                Assert.Fail("Assert.AreNotEqual", expected, actual);
            }
        }

        public static void AreSame(object expected, object actual)
        {
            if (!object.ReferenceEquals(expected, actual))
            {
                Assert.Fail("Assert.AreSame", expected, actual);
            }
        }

        public static void AreNotSame(object expected, object actual)
        {
            if (object.ReferenceEquals(expected, actual))
            {
                Assert.Fail("Assert.AreNotSame", expected, actual);
            }
        }

        public static void IsNull(object value)
        {
            if (value != null)
            {
                Assert.Fail("Assert.IsNull", value);
            }
        }

        public static void IsNotNull(object value)
        {
            if (value == null)
            {
                Assert.Fail("Assert.IsNotNull", value);
            }
        }

        public static void IsTrue(bool condition)
        {
            if (!condition)
            {
                Assert.Fail("Assert.IsTrue", condition);
            }
        }

        public static void IsFalse(bool condition)
        {
            if (condition)
            {
                Assert.Fail("Assert.IsFalse", condition);
            }
        }
    
        public static void Inconclusive()
        {
            Assert.Inconclusive(string.Empty, null);
        }

        public static void Inconclusive(string message)
        {
            Assert.Inconclusive(message, null);
        }

        public static void Inconclusive(string message, params object[] parameters)
        {
            throw new AssertInconclusiveException(Assert.Format("Assert.Inconclusive", message, parameters));
        }

        public static void Fail()
        {
            Assert.Fail(string.Empty, null);
        }

        public static void Fail(string message)
        {
            Assert.Fail(message, null);
        }

        public static void Fail(string message, params object[] parameters)
        {
            throw new AssertFailedException(Assert.Format("Assert.Fail", message, parameters));
        }

        public static new bool Equals(object objA, object objB)
        {
            Assert.Fail("Do not use Assert.Equals");
            return false;
        }

        private static bool InternalEqual(object expected, object actual)
        {
            if (expected == null && actual == expected) return true;

            if (expected == null || actual == null)
            {
                return false;
            }

            Type eType = expected.GetType();
            Type aType = actual.GetType();

            if (eType.IsValueType && !eType.IsEnum && aType.IsValueType && !aType.IsEnum)
            {
                long i1 = 0;
                long i2 = 0;
                ulong ui1 = 0;
                ulong ui2 = 0;
                double d1 = 0;
                double d2 = 0;
                bool b1 = false;
                bool b2 = false;
                char c1 = '\0';
                char c2 = '\0';

                switch (eType.Name)
                {
                    case "SByte": i1 = (SByte)expected; break;
                    case "Byte": i1 = (Byte)expected; break;
                    case "Int16": i1 = (Int16)expected; break;
                    case "UInt16": i1 = (UInt16)expected; break;
                    case "Int32": i1 = (Int32)expected; break;
                    case "UInt32": i1 = (UInt32)expected; break;
                    case "Int64": i1 = (Int64)expected; break;
                    case "UInt64": ui1 = (UInt64)expected; break;
                    case "Single": d1 = (Single)expected; break;
                    case "Double": d1 = (Double)expected; break;
                    case "Boolean": b1 = (Boolean)expected; break;
                    case "Char": c1 = (Char)expected; break;
                    default: Assert.Fail("Unsupported value type."); break;
                }

                switch (aType.Name)
                {
                    case "SByte": i2 = (SByte)actual; break;
                    case "Byte": i2 = (Byte)actual; break;
                    case "Int16": i2 = (Int16)actual; break;
                    case "UInt16": i2 = (UInt16)actual; break;
                    case "Int32": i2 = (Int32)actual; break;
                    case "UInt32": i2 = (UInt32)actual; break;
                    case "Int64": i2 = (Int64)actual; break;
                    case "UInt64": ui2 = (UInt64)actual; break;
                    case "Single": d2 = (Single)actual; break;
                    case "Double": d2 = (Double)actual; break;
                    case "Boolean": b2 = (Boolean)actual; break;
                    case "Char": c2 = (Char)actual; break;
                    default: Assert.Fail("Unsupported value type."); break;
                }

                if ((i1 == i2) && (ui1 == ui2) && (d1 == d2) && (b1 == b2) && (c1 == c2))
                {
                    return true;
                }
            }
            else
            {
                if (object.Equals(expected, actual))
                {
                    return true;
                }
            }

            return false;
        }

        private static string Format(string messageType, string message, params object[] parameters)
        {
            string res = messageType;

            if (message != null && message.Length != 0)
            {
                res += ": " + message;
            }

            if (parameters != null)
            {
                res += " (";

                for (int i = 0; i < parameters.Length; i++)
                {
                    if (parameters[i] != null)
                    {
                        res += parameters[i].ToString();
                    }
                    else
                    {
                        res += "null";
                    }
                    
                    if (i < parameters.Length - 1)
                    {
                        res += ", ";
                    }
                }

                res += ")";
            }

            return res;
        }
    }
}
