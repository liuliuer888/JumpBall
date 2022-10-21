using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using System.IO;
using System.Net;

/**
* 文件名：WebRequestManager.cs
* 文件描述：UnityWebRequest加载接口
* 作者：zhouxiaogang
* 创建时间：2020/6/6 11:06
* 修改记录：
*/
public class WebRequestManager:TSingleton<WebRequestManager>,IInitializeable
{
    public void OnInit()
    {
    }

    /// <summary>
    /// 同步加载Assetbundle
    /// </summary>
    /// <param name="url">加载地址，支持本地或网络地址</param>
    /// <param name="cache">是否缓存到本地</param>
    /// <returns></returns>
    public AssetBundle DownloadAssetbundle(string url, bool cache)
    {
        url = AppendFilePreffix(url);
        UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(url);
        request.SendWebRequest();
        while (!request.isDone) { }
        if (!string.IsNullOrEmpty(request.error))
        {
            request.Dispose();
            return null;
        }

        AssetBundle bundle = (request.downloadHandler as DownloadHandlerAssetBundle).assetBundle;
        request.Dispose();
        return bundle;
    }

    /// <summary>
    /// 给本地文件添加前缀
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    private string AppendFilePreffix(string filePath)
    {
        if(filePath.StartsWith("jar:") || filePath.StartsWith("file:") || filePath.StartsWith("http"))
        {
            return filePath;
        }
        return "file://" + filePath;
    }

    public void OnDispose()
    {
    }
}
