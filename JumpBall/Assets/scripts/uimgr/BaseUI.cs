using UnityEngine;
/**
 * 文件名：BaseUI.cs
 * 文件描述：UI基类
 * 作者：liuzhitao
 * 创建时间：2022/10/20 17:08:44
 * 修改记录：
 */

public class UIData
{
    public string strPath;
    public bool isCloseDestory;
}

public class BaseUI : MonoBehaviour
{
    public virtual void OnInit()
    {
    }

    public virtual void OnShow()
    {
    }

    public virtual void RegisterMessage()
    {
    }
}
