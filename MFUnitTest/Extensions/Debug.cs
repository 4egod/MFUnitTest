//-----------------------------------------------------------------------
// <copyright file="Debug.cs" company="Reasol, LLC">
//     Copyright (c) Reasol, LLC. All rights reserved.
// </copyright>
// <author>Dmitry Tarasov</author>
// <summary>Debug wrapper for better compatibility.</summary>
//-----------------------------------------------------------------------
//#if MF
namespace System.Diagnostics
{
    using MFDebug = Microsoft.SPOT.Debug;

    public static class Debug
    {
        //[Conditional("DEBUG")]
        public static void Print(string text)
        {
            MFDebug.Print(text); 
        }
    }
}
//#endif
