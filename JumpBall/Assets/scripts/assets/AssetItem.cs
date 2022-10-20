using System;
using UnityEngine;
using Object = UnityEngine.Object;

/**
* 文件名：AssetItem.cs
* 文件描述：资源加载最小单位
* 作者：liuzhitao
* 创建时间：2022/10/20 18:06
* 修改记录：
*/
public class AssetItem : ICacheable
{
    public string assetPath;
    private AssetBundle mAssetBundle;

    public Object LoadAsset(string assetName, Type assetType)
    {
        Object result = null;
        if (GameGlobal.EnableEditorLoadMode)
        {
            result = UnityEditor.AssetDatabase.LoadAssetAtPath(assetName, assetType);
        }
        else if (mAssetBundle != null)
        {
            result = mAssetBundle.LoadAsset(assetName, assetType);
        }

        return result;
    }

    public void Unload(bool unloadLoadedObjects)
    {
        if (mAssetBundle != null)
        {
            mAssetBundle.Unload(unloadLoadedObjects);
            mAssetBundle = null;
        }
    }

    public void FreeToCache()
    {
        mAssetBundle = null;
    }
}
