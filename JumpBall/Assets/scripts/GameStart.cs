using UnityEngine;

/**
 * 文件名：GameStart.cs
 * 文件描述：游戏启动器
 * 作者：liuzhitao
 * 创建时间：2022/10/20 17:44:16
 * 修改记录：
 */

public class GameStart : MonoBehaviour
{
    void Awake()
    {
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
        GameConfiguration config = GameObject.Find("StartLoad").GetComponent<GameConfiguration>();
#if UNITY_EDITOR
        GameGlobal.EnableEditorLoadMode = !config.AssetbundleMode;
#else
        GameGlobal.EnableEditorLoadMode = false;
#endif
    }

    void Start()
    {
        UIMgr.Instance.ShowUI(LoginUI.Info);
    }

    public void ResetGameSystem()
    {
        SystemManager.Instance.OnReset();
    }

    public void FixedUpdate()
    {
        SystemManager.Instance.OnFixedUpdate(Time.fixedDeltaTime);
    }

    public void Update()
    {
        SystemManager.Instance.OnUpdate(Time.deltaTime);
    }

    public void LateUpdate()
    {
        SystemManager.Instance.OnLateUpdate(Time.deltaTime);
    }

    public void OnDestroy()
    {
        SystemManager.Instance.OnDispose();
    }
}
