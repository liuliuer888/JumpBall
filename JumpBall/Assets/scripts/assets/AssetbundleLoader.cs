using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AssetbundleLoader : IAssetsLoader
{
    /// <summary>
    /// Assetbundle资源清单
    /// </summary>
    private AssetsManifestRef mAssetManifest;
    /// <summary>
    /// 当前已加载或正在加载的Assetbundle池
    /// </summary>
    private Dictionary<string, AssetItem> mAssetItemDic = new Dictionary<string, AssetItem>();

    public AssetbundleLoader()
    {
        mAssetManifest = AssetsManifestRef.LoadFromFile("gameassets");
    }

    /// <summary>
    /// 同步加载资源
    /// </summary>
    /// <param name="assetName">资源路径</param>
    /// <param name="cacheLevel">缓存级别</param>
    /// <returns>AssetItem对象</returns>
    public AssetItem LoadAsset(string assetName)
    {
        AssetItem tempItem = LoadAssetBundleSync(GetAssetbundlePath(assetName));
        return tempItem;
    }

    /// <summary>
    /// 从已创建的资源池中尝试获取对应资源
    /// </summary>
    /// <param name="bundleName">Assetbundle资源路径</param>
    /// <returns></returns>
    private AssetItem TryGetAssetItem(string bundleName)
    {
        AssetItem tempItem = null;
        if (mAssetItemDic.TryGetValue(bundleName, out tempItem))
        {
            return tempItem;

        }
        return null;
    }

    /// <summary>
    /// 卸载指定资源，会减引用计数，引用计数为0时，如果资源缓存等级不高，将会被立即回收
    /// </summary>
    /// <param name="assetName">资源路径</param>
    /// <param name="unloadLoadedObject">是否卸载从该资源创建的所有对象</param>
    public void UnLoadAsset(string assetName, bool unloadLoadedObject)
    {
        string bundleName = GetAssetbundlePath(assetName);
        UnLoadAssetbundle(bundleName, unloadLoadedObject);
    }

    public void UnLoadAssetbundle(string bundleName, bool unloadLoadedObj)
    {
        AssetItem tempItem = TryGetAssetItem(bundleName);
        if (tempItem == null)
        {
            return;
        }
        tempItem.RemoveRefernce();
        if (tempItem.IsNoReference())
        {
            mAssetItemDic.Remove(bundleName);
            tempItem.Unload(unloadLoadedObj);
            ObjectCache.CacheDirectly(tempItem);
        }

        string[] dependencies = mAssetManifest.GetDependencies(bundleName);
        if (dependencies != null && dependencies.Length > 0)
        {
            for (int i = 0; i < dependencies.Length; i++)
            {
                if (dependencies[i] == bundleName)
                {
                    continue;
                }

                UnLoadAssetbundle(dependencies[i], unloadLoadedObj);
            }
        }
    }

    /// <summary>
    /// 同步加载Assetbundle
    /// </summary>
    /// <param name="bundleName">Assetbundle资源路径</param>
    /// <param name="cacheLevel">缓存级别</param>
    /// <returns>AssetItem对象</returns>
    private AssetItem LoadAssetBundleSync(string bundleName)
    {
        AssetItem tempItem;
        string[] dependencies = mAssetManifest.GetDependencies(bundleName);
        if (dependencies != null && dependencies.Length > 0)
        {
            for (int i = 0; i < dependencies.Length; i++)
            {
                tempItem = TryGetAssetItem(dependencies[i]);
                if (tempItem == null)
                {
                    tempItem = ObjectCache.GetObject<AssetItem>(true);
                    mAssetItemDic[dependencies[i]] = tempItem;
                }
                LoadAssetBundleSync(dependencies[i]);
            }
        }

        tempItem = TryGetAssetItem(bundleName);
        if (tempItem == null)
        {
            tempItem = ObjectCache.GetObject<AssetItem>(true);
            mAssetItemDic[bundleName] = tempItem;
        }

        tempItem.assetPath = bundleName;
        if (tempItem.isLoaded)
        {
            return tempItem;
        }

        string innerPath = GameGlobal.ASSET_ROOT_PATH_INNER + bundleName;
        AssetBundle bundle = AssetBundle.LoadFromFile(innerPath);
        if (bundle)
        {
            tempItem.SetAssetBundle(bundle);
            return tempItem;
        }

        return null;
    }

    /// <summary>
    /// 根据资源路径，获取该资源对应的Assetbundle路径
    /// </summary>
    /// <param name="assetName">资源路径</param>
    /// <returns></returns>
    private string GetAssetbundlePath(string assetName)
    {
        string dir = Path.GetDirectoryName(assetName).Replace('\\', '/').ToLower();
        return dir + "/" + Path.GetFileNameWithoutExtension(assetName).ToLower() + GameGlobal.ASSETBUNDLE_SUFFIX;
    }

    public void Dispose()
    {
    }
}
