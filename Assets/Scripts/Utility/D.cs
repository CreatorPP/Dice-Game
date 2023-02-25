#define DEBUG_LEVEL_LOG

using UnityEngine;

public sealed class D
{
	[System.Diagnostics.Conditional("_DEV_MODE")]
	public static void Log(string format, params object[] paramList) => Debug.Log(string.Format(format, paramList));
	[System.Diagnostics.Conditional("_DEV_MODE")]
	public static void Warn( string format, params object[] paramList ) => Debug.LogWarning (string.Format (format, paramList));
	[System.Diagnostics.Conditional("_DEV_MODE")]
	public static void Error( string format, params object[] paramList ) => Debug.LogError (string.Format (format, paramList));
	[System.Diagnostics.Conditional("_DEV_MODE")]
	public static void Assert( bool condition ) => Assert(condition, string.Empty, true);
	[System.Diagnostics.Conditional("_DEV_MODE")]
	public static void Assert( bool condition, string assertString ) => Assert(condition, assertString, false);
	[System.Diagnostics.Conditional("_DEV_MODE")]
	public static void Assert( bool condition, string assertString, bool pauseOnFail )
	{
		if (condition) return;
		Debug.LogError( "assert failed! " + assertString );

		if (pauseOnFail)
			Debug.Break();
	}
}