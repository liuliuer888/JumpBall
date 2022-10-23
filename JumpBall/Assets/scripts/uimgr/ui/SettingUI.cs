using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingUI : BaseUI
{
    public static UIData Info = new UIData() { strPath = "prefab/SettingUI.prefab", isCloseDestory = false };

    public Button m_btnSetting;
    public Button m_btnPause;
    public Transform m_onDrag;

    public override void OnInit()
    {
        Input.multiTouchEnabled = false;
        m_btnSetting.GetComponent<Button>().onClick.AddListener(OnClickSetting);
        m_btnPause.GetComponent<Button>().onClick.AddListener(OnClickPause);
        OnDragHandle dragHandle = m_onDrag.GetComponent<OnDragHandle>();
        dragHandle.onBeginDrag = OnBeginDrag;
        dragHandle.onDrag = OnDrag;
        dragHandle.onEndDrag = OnEndDrag;
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

    private void OnClickSetting()
    {
        BallMgr.Instance.CreateBall();
    }

    private void OnClickPause()
    {
    }

    private void OnBeginDrag(PointerEventData eventData)
    {
    }

    private void OnDrag(PointerEventData eventData)
    {
        Vector2 vecTouchPos = eventData.delta;
        Vector3 vecDir = new Vector3(vecTouchPos.x, vecTouchPos.y, 0);
        float fAngle = Vector3.Angle(vecDir, Vector3.up);
        if(fAngle >= 0 && fAngle <= 10)
        {
            vecDir = Vector3.zero;
        }
        else
        {
            if(vecDir.x > 0)
            {
                vecDir = Vector3.right;
            }
            else
            {
                vecDir = Vector3.left;
            }
        }

        MoveBoxMgr.Instance.SetDragDir(vecDir);
    }

    private void OnEndDrag(PointerEventData eventData)
    {
        MoveBoxMgr.Instance.SetDragDir(Vector3.zero);
    }

    public override void OnClose()
    {
    }
}
