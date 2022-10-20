using UnityEngine;
/**
 * 文件名：GameConfiguration.cs
 * 文件描述：游戏启动配置
 * 作者：liuzhitao
 * 创建时间：2022/10/20 17:28:08
 * 修改记录：
 */
public class GameConfiguration : MonoBehaviour
{
    [SerializeField]
    [Header("Assetbundle模式")]
    public bool AssetbundleMode = false;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
