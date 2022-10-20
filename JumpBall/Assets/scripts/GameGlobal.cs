using UnityEngine;

/**
 * 文件名：GameGlobal.cs
 * 文件描述：全局定义
 * 作者：liuzhitao
 * 创建时间：2022/10/20 17:08:44
 * 修改记录：
 */
public static class GameGlobal
{
    public static readonly string ASSET_ROOT_NAME = "gameassets";

    /// <summary>
    /// 安装包内的游戏资源根目录
    /// </summary>
#if UNITY_IOS && !UNITY_EDITOR
    public static readonly string ASSET_ROOT_PATH_INNER = "file://" + Application.streamingAssetsPath + "/gameassets/";
#else
    public static readonly string ASSET_ROOT_PATH_INNER = Application.streamingAssetsPath + "/gameassets/";
#endif

    /// <summary>
    /// Assetbundle资源后缀
    /// </summary>
    public static readonly string ASSETBUNDLE_SUFFIX = ".ab";

    /// <summary>
    /// 资源加载模式是否为Editor模式
    /// </summary>
    public static bool EnableEditorLoadMode = true;

    /// <summary>
    /// Unity路径
    /// </summary>
    public static string AppDataPath = Application.dataPath;
    public static string AppStreamingAssetPath = Application.streamingAssetsPath;
    public static string AppPersistentDataPath = Application.persistentDataPath;
    public static string AppTempCachePath = Application.temporaryCachePath;
}

