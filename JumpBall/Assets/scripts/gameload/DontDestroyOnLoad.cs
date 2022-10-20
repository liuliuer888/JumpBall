using UnityEngine;

/**
 * 文件名：DontDestroyOnLoad.cs
 * 文件描述：不可销毁标识
 * 作者：liuzhitao
 * 创建时间：2022/10/20 17:59:11
 * 修改记录：
 */

public class DontDestroyOnLoad : MonoBehaviour
{
    private void Awake()
    {
        GameObject.DontDestroyOnLoad(this.gameObject);
    }
}
