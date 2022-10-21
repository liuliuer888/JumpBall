using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsserMgr : TSingleton<AsserMgr>, IInitializeable
{
    private IAssetsLoader mLoader;
    public void OnInit()
    {
        if (GameGlobal.EnableEditorLoadMode)
        {
            mLoader = new EditorAssetLoader();
        }
        else
        {
            mLoader = new AssetbundleLoader();
        }
    }

    /// <summary>
    /// 同步加载一个AssetItem
    /// </summary>
    /// <param name="assetName">资源路径</param>
    /// <param name="cacheLevel">缓存级别</param>
    /// <returns></returns>
    public UnityEngine.Object LoadAsset(string assetName, Type assetType)
    {
        AssetItem tempItem = mLoader.LoadAsset(assetName);
        if (tempItem != null)
        {
            return tempItem.LoadAsset(System.IO.Path.GetFileName(assetName), assetType);
        }

        return null;
    }

    public void OnDispose()
    {

    }
}
