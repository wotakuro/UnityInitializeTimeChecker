# UnityInitializeChecker について
Unityアプリで起動に掛かった時間を測るためのプログラムです。<br />
iOS/Androidに対応しています.<br />

Unityでアプリ開発中にアプリ起動に時間が掛かると思った時に計測用としてお使いください

アプリの起動でに時間が掛かる要因としては下記が考えられます.
 - Resourcesフォルダにファイルを置く
 - ネイティブプラグイン等で行っている初期化が重い
 - プログラム自体が膨らみすぎた

# 利用方法について
UnityInitializeTimeChecker.unitypackage をプロジェクトにインポートして利用してください。

UnityUtil.InitializeTimeChecker.GetCpuSecFromAppBoot();
と呼ぶことで、アプリ起動からの秒数をfloatとして取得することが出来ます。
(内部的にはプロセス起動からの時間を取得しています。)

RuntimeInitializeOnLoadMethod と組み合わせて使うことで、アプリにも組み込みやすいかと思います。
下記のような処理を挟むことでログで計測する形です。

	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
	public static void OnInitializedApp(){
		float initTime = UnityUtil.InitializeTimeChecker.GetCpuSecFromAppBoot();
		Debug.Log (string.Format ("アプリ起動までにかかった時間は {0:f4}秒でした", initTime) );
	}
 
	[RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
	public static void OnInitializedAfterSceneLoad(){
		float initTime = UnityUtil.InitializeTimeChecker.GetCpuSecFromAppBoot();
		Debug.Log (string.Format ("最初のシーンロードまでは {0:f4}秒でした", initTime));
	}


また、デモとして、下記シーンを用意いたしました
Assets/Demo/demo.unity
利用に関しても下記スクリプトを参照していただければと思います
Assets/Demo/DemoScript.cs

# 実時間との差異ついて
## Androidcでの挙動について
Androidでは Processが立ち上がってからのCPU時間を取得しています。
そのため多少のずれがある場合がございます。

## iOSでの挙動について
「iOS Human Interface Guidelines」によると、
 > 起動ファイルや起動画像は、アプリケーションが起動するまで、時間つなぎとしてiOSが表示する画像です。

という事でしたので、起動画面を表示していても、アプリケーション自体は立ち上がっていない時間があるようです。
そのため、起動画面の表示している実時間とコチラで取得する値に差異がある事をご了承ください
