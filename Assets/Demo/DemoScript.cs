using UnityEngine;
using UnityEngine.UI;

public class DemoScript : MonoBehaviour {
	private static float initTime = 0.0f;

	public Text text;
	private float firstAwakeTime = 0.0f;

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	public static void OnInitializedApp(){
		initTime = UnityUtil.InitializeTimeChecker.GetCpuSecFromAppBoot();
	}
	void Awake(){
		this.firstAwakeTime= UnityUtil.InitializeTimeChecker.GetCpuSecFromAppBoot();
	}

	// Use this for initialization
	void Update () {
		float nowTime = UnityUtil.InitializeTimeChecker.GetCpuSecFromAppBoot();
		text.text = string.Format ("Initialize {0:f4}\nFirstAwake {1:f4}\nNowTime {2:f4}", initTime , firstAwakeTime,nowTime);
	}
	
}
