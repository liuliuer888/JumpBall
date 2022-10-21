using System;
using UnityEngine.UI;

public class LoginUI : BaseUI
{
    public static UIData Info = new UIData() { strPath = "prefab/LoginUI.prefab", isCloseDestory = false };

    public Button m_btnClass;
    public Button m_btnChallenge;

    public override void OnInit()
    {
        m_btnClass.GetComponent<Button>().onClick.AddListener(OnClickClass);
        m_btnChallenge.GetComponent<Button>().onClick.AddListener(OnClickChallenge);
    }

    public override void OnShow(Action action, object[] param)
    {
        action?.Invoke();
    }

    public override void RegisterMessage()
    {
    }

    public override void OnUpdate(float deltaTime)
    {
    }

    private void OnClickClass()
    {
        PlayerDataMgr.Instance.eCurGameType = GameType.CLASS;
        EnterBatter();
    }

    private void OnClickChallenge()
    {
        PlayerDataMgr.Instance.eCurGameType = GameType.CHALLENGE;
        EnterBatter();
    }

    private void EnterBatter()
    {
        Close(Info);
        SceneMgr.Instance.LoadSceneSync(SceneType.BattleScene);
    }

    public override void OnClose()
    {
    }
}
