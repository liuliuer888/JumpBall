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
    public bool isLoaded;

    private AssetBundle mAssetBundle;
    /// <summary>
    /// 当前引用数量
    /// </summary>
    private int mReferencesCount;

    public Object LoadAsset(string assetName, Type assetType)
    {
        Object result = null;
#if UNITY_EDITOR
        if (GameGlobal.EnableEditorLoadMode)
        {
            result = UnityEditor.AssetDatabase.LoadAssetAtPath(assetPath + "/" + assetName, assetType);
        }
        else
        {
            result = mAssetBundle.LoadAsset(assetName, assetType);
        }
#else
        if (mAssetBundle != null)
        {
            result = mAssetBundle.LoadAsset(assetName, assetType);
        }
#endif
        return result;
    }

    /// <summary>
    /// 绑定Assetbundle资源
    /// </summary>
    /// <param name="bundle"></param>
    public void SetAssetBundle(AssetBundle bundle)
    {
        mAssetBundle = bundle;
        isLoaded = true;
    }

    public void Unload(bool unloadLoadedObjects)
    {
        if (mAssetBundle != null)
        {
            mAssetBundle.Unload(unloadLoadedObjects);
            mAssetBundle = null;
        }
    }

    /// <summary>
    /// 添加一个引用
    /// </summary>
    public void AddReference()
    {
        mReferencesCount++;
    }

    /// <summary>
    /// 是否已经没有引用了
    /// </summary>
    /// <returns></returns>
    public bool IsNoReference()
    {
        return mReferencesCount <= 0;
    }

    /// <summary>
    /// 移除一个引用
    /// </summary>
    public void RemoveRefernce()
    {
        mReferencesCount--;
    }

    public void FreeToCache()
    {
        mAssetBundle = null;
    }
}
