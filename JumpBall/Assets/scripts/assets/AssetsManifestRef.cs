using System.Collections.Generic;
using UnityEngine;

public class AssetsManifestRef
{
    /// <summary>
    /// 原始清单
    /// </summary>
    private AssetBundleManifest mRawManifest;

    /// <summary>
    /// 缓存的清单(资源名->引用关系)
    /// </summary>
    private Dictionary<string, string[]> mCacheManifests = new Dictionary<string, string[]>();

    /// <summary>
    /// 获取对应资源的所有引用
    /// </summary>
    /// <param name="assetName"></param>
    /// <returns></returns>
    public string[] GetDependencies(string assetName)
    {
        string[] dependencies = null;
        if (mCacheManifests.TryGetValue(assetName, out dependencies))
        {
            return dependencies;
        }

        dependencies = mRawManifest.GetAllDependencies(assetName);
        mCacheManifests[assetName] = dependencies;
        return dependencies;
    }

    /// <summary>
    /// 获取原始资源清单
    /// </summary>
    /// <returns></returns>
    public AssetBundleManifest GetManifest()
    {
        return mRawManifest;
    }

    /// <summary>
    /// 从文件中加载清单
    /// </summary>
    /// <param name="manifestFile">清单文件</param>
    /// <returns></returns>
    public static AssetsManifestRef LoadFromFile(string manifestFile)
    {
        AssetsManifestRef manifest = new AssetsManifestRef();
        string innerPath = GameGlobal.ASSET_ROOT_PATH_INNER + manifestFile;
        if (!innerPath.StartsWith("file") && !innerPath.StartsWith("jar"))
        {
            innerPath = "file://" + innerPath;
        }

        AssetBundle bundle = WebRequestManager.Instance.DownloadAssetbundle(innerPath, false);
        if (bundle != null)
        {
            manifest.mRawManifest = bundle.LoadAsset<AssetBundleManifest>("AssetBundleManifest");
            bundle.Unload(false);
        }

        return manifest;
    }
}
