using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneType
{
    public static string MainScene = "GameStart";
    public static string BattleScene = "Battle";
}

public class SceneMgr : TSingleton<SceneMgr>, IInitializeable
{
    public void OnInit()
    {
    }

    public void LoadSceneSync(string strName)
    {
        CoroutineManager.Instance.StartCoroutine(ChangeScene(strName));
    }

    private IEnumerator ChangeScene(string strName)
    {
        AsyncOperation asyncOp = SceneManager.LoadSceneAsync(strName);
        asyncOp.allowSceneActivation = true;
        while (!asyncOp.isDone && asyncOp.progress < 1)
        {
            yield return new WaitForEndOfFrame();
        }

        yield return asyncOp;
        LoadSceneFinish();
    }

    private void LoadSceneFinish()
    {
        BallMgr.Instance.CreateBall();
        UIMgr.Instance.ShowUI(SettingUI.Info);
    }

    public void OnDispose()
    {
    }
}

