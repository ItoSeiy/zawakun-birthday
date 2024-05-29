#define DEBUG

using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Project.Framework.Utils
{
    public static class CustomDebug
    {
        [Conditional("DEBUG")]
        public static void Log(string message)
        {
            Debug.Log(message);
        }

        [Conditional("DEBUG")]
        public static void Log(string message, Object context)
        {
            Debug.Log(message, context);
        }

        [Conditional("DEBUG")]
        public static void LogWarning(string message)
        {
            Debug.LogWarning(message);
        }

        [Conditional("DEBUG")]
        public static void LogWarning(string message, Object context)
        {
            Debug.LogWarning(message, context);
        }

        [Conditional("DEBUG")]
        public static void LogError(string message)
        {
            Debug.LogError(message);
        }

        [Conditional("DEBUG")]
        public static void LogError(string message, Object context)
        {
            Debug.LogError(message, context);
        }
    }
}