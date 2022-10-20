
public interface IAssetsLoader
{
    AssetItem LoadAsset(string assetName);
    void UnLoadAsset(string assetName, bool unloadLoadedAsset);
    void Dispose();
}
