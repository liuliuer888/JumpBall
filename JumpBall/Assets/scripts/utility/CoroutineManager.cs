using System;
using System.Collections;
using UnityEngine;

/**
    * 文件名：CoroutineManager.cs
    * 文件描述：协程工具
    * 作者：liuzhitao
    * 创建时间：2022/10/21 16:40
    * 修改记录：
    */
public class CoroutineManager:TSingleton<CoroutineManager>,IInitializeable
{
    public class CoroutineMonoObject : MonoBehaviour
    {
    }

    /// <summary>
    /// 协程驱动器
    /// </summary>
    private MonoBehaviour mBehavior;

    /// <summary>
    /// 初始化
    /// </summary>
    public void OnInit()
    {
        GameObject go = new GameObject("CoroutineObject");
        GameObject.DontDestroyOnLoad(go);
        mBehavior = go.AddComponent<CoroutineMonoObject>();
    }

    /// <summary>
    /// 启动协程
    /// </summary>
    /// <param name="routine"></param>
    /// <returns></returns>
    public Coroutine StartCoroutine(IEnumerator routine)
    {
        if (!mBehavior) { return null; }
        return mBehavior.StartCoroutine(routine);
    }

    /// <summary>
    /// 启动协程
    /// </summary>
    /// <param name="method"></param>
    /// <param name="value"></param>
    /// <returns></returns>
    public Coroutine StartCoroutine(string method, object value)
    {
        if (!mBehavior) { return null; }
        return mBehavior.StartCoroutine(method, value);
    }

    /// <summary>
    /// 停止一个协程
    /// </summary>
    /// <param name="coroutine"></param>
    public void StopCoroutine(Coroutine coroutine)
    {
        if (!mBehavior) { return; }
        if (coroutine != null)
        {
            mBehavior.StopCoroutine(coroutine);
        }
    }

    /// <summary>
    /// 停止一个协程
    /// </summary>
    /// <param name="coroutine"></param>
    public void StopCoroutine(IEnumerator coroutine)
    {
        if (!mBehavior) { return; }
        if (coroutine != null)
        {
            mBehavior.StopCoroutine(coroutine);
        }
    }

    /// <summary>
    /// 停止一个协程
    /// </summary>
    /// <param name="method"></param>
    public void StopCoroutine(string method)
    {
        if (!mBehavior) { return; }
        if (string.IsNullOrEmpty(method))
        {
            mBehavior.StopCoroutine(method);
        }
    }

    /// <summary>
    /// 停止全部协程
    /// </summary>
    public void StopAllCoroutines()
    {
        if (!mBehavior) { return; }
        mBehavior.StopAllCoroutines();
    }


    public void OnDispose()
    {
    }
}