using UnityEngine;
using System.Runtime.InteropServices;

namespace UnityUtil{
	public class InitializeTimeChecker{
		#if UNITY_IOS || UNITY_IPHONE
		[DllImport("__Internal")]
		private static extern float InitializeTimeCheckerGetCpuSecFromAppBoot();
		#endif

		public static float GetCpuSecFromAppBoot(){
			#if UNITY_EDITOR
			return 0.0f;
			#elif UNITY_ANDROID
			AndroidJavaClass javaProcess = new AndroidJavaClass("android.os.Process");
			long elapsedCpuTime = javaProcess.CallStatic<long>("getElapsedCpuTime");
			return (float)elapsedCpuTime * 0.001f;
			#elif UNITY_IOS || UNITY_IPHONE
			return InitializeTimeCheckerGetCpuSecFromAppBoot();
			#else
			return 0.0f;
			#endif
		}
	}
}