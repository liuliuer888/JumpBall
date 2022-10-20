using System.Collections.Generic;
using System.IO;

public class EditorAssetLoader : IAssetsLoader
{
    /// <summary>
    /// 缓存中的加载项
    /// </summary>
    private Dictionary<string, AssetItem> mAssetItemDic = new Dictionary<string, AssetItem>();

    public AssetItem LoadAsset(string assetName)
    {
        string strPath = GetFolderPath(assetName);
        AssetItem item = TryGetAssetItem(assetName);
        if (item == null)
        {
            item = ObjectCache.GetObject<AssetItem>(true);
            item.assetPath = strPath;
        }

        return item;
    }

    /// <summary>
    /// 获取Editor下的资源路径
    /// </summary>
    /// <param name="assetName"></param>
    /// <returns></returns>
    private string GetFolderPath(string assetName)
    {
        if (string.IsNullOrEmpty(assetName))
        {
            return "";
        }

        return "Assets/" + GameGlobal.ASSET_ROOT_NAME + "/" + Path.GetDirectoryName(assetName).Replace("\\", "/");
    }

    /// <summary>
    /// 尝试从已加载的池中取出对应的AssetItem
    /// </summary>
    /// <param name="assetName">资源路径</param>
    /// <returns></returns>
    private AssetItem TryGetAssetItem(string assetName)
    {
        AssetItem tempItem = null;
        if (mAssetItemDic.TryGetValue(assetName, out tempItem))
        {
            return tempItem;
        }

        return null;
    }

    public void UnLoadAsset(string assetName, bool unloadLoadedObject)
    {
        string assetPath = GetFolderPath(assetName);
        AssetItem tempItem = TryGetAssetItem(assetPath);
        if (tempItem == null)
        {
            return;
        }

        tempItem.Unload(unloadLoadedObject);
        ObjectCache.CacheDirectly(tempItem);
        mAssetItemDic.Remove(assetPath);
    }

    /// <summary>
    /// 销毁函数
    /// </summary>
    public void Dispose()
    {
        foreach (KeyValuePair<string, AssetItem> pair in mAssetItemDic)
        {
            pair.Value.Unload(true);
        }

        mAssetItemDic.Clear();
    }
}
