using System;
using System.Collections.Concurrent;
using System.Collections.Generic;

/**
    * 文件名：ObjectCache.cs
    * 文件描述：对象缓存池
    * 作者：liuzhitao
    * 创建时间：2022/10/20 11:15
    * 修改记录：
    */
public class ObjectCache
{

    private static object lockObj = new object();
    /// <summary>
    /// 对象缓存池
    /// </summary>
    private static ConcurrentDictionary<Type, ConcurrentQueue<ICacheable>> mCachePool = new ConcurrentDictionary<Type, ConcurrentQueue<ICacheable>>();
    /// <summary>
    /// 缓存池上限设置
    /// </summary>
    private static ConcurrentDictionary<Type, int> mCacheCountSetting = new ConcurrentDictionary<Type, int>();

    /// <summary>
    /// 设置某一对象最多可缓存的对象数量
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="maxCount"></param>
    public static void SetCacheMaxCount<T>(int maxCount)
    {
        mCacheCountSetting[typeof(T)] = maxCount;
    }

    /// <summary>
    /// 缓存一个对象，这个对象必须实现ICacheable接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    public static void Cache<T>(T obj) where T : ICacheable
    {
        if (obj == null)
        {
            return;
        }

        obj.FreeToCache();
        Type t = obj.GetType();
        ConcurrentQueue<ICacheable> pool = GetPool(t, true);
        int maxCount = 0;
        mCacheCountSetting.TryGetValue(t, out maxCount);
        if (maxCount != 0 && pool.Count > maxCount)
        {
            return;
        }

        pool.Enqueue(obj);
    }

    public static void CacheDirectly(ICacheable obj)
    {
        if (obj == null) { return; }
        obj.FreeToCache();
        Type t = obj.GetType();
        ConcurrentQueue<ICacheable> pool = GetPool(t, true);
        int maxCount = 0;
        mCacheCountSetting.TryGetValue(t, out maxCount);
        if (maxCount != 0 && pool.Count > maxCount)
        {
            return;
        }
        pool.Enqueue(obj);
    }

    /// <summary>
    /// 从对象缓存池中取出或创建对应类型的对象
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="createIfNoCache">如果没有缓存，就创建一个新的</param>
    /// <returns>T instance</returns>
    public static T GetObject<T>(bool createIfNoCache = true) where T : ICacheable,new()
    {
        ConcurrentQueue<ICacheable> pool = GetPool<T>(false);
        if (pool != null && pool.Count > 0)
        {
            ICacheable outObj = default;
            if (pool.TryDequeue(out outObj))
            {
                return (T)outObj;
            }
        }
        if (createIfNoCache)
        {
            T tempObj = new T();
            return tempObj;
        }
        return default(T);
    }

    public static ICacheable GetObject(Type t, bool createIfNoCache = true)
    {
        ConcurrentQueue<ICacheable> pool = GetPool(t, false);
        if(pool != null && pool.Count > 0)
        {
            ICacheable outObj = default;
            if (pool.TryDequeue(out outObj))
            {
                return outObj;
            }
        }
        if (createIfNoCache)
        {
            return (ICacheable)Activator.CreateInstance(t);
        }
        return null;
    }

    /// <summary>
    /// 清除某类对象的全部缓存
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    public static void ClearCache<T>()
    {
        lock (lockObj)
        {
            Type t = typeof(T);
            if (mCachePool.ContainsKey(t))
            {
                mCachePool.TryRemove(t, out _);
            }
        }
    }

    /// <summary>
    /// 清空整个对象池
    /// </summary>
    public static void ClearAllCache()
    {
        lock (lockObj)
        {
            mCachePool.Clear();
        }
    }

    /// <summary>
    /// 获取某个类型对象的缓存池
    /// </summary>
    /// <typeparam name="T">对象类型</typeparam>
    /// <param name="createIfNotExists">如果还没有为该对象创建缓存池，就创建一个</param>
    /// <returns></returns>
    private static ConcurrentQueue<ICacheable> GetPool<T>(bool createIfNotExists)where T:ICacheable
    {
        ConcurrentQueue<ICacheable> tempPool = null;
        Type t = typeof(T);
        if(mCachePool.TryGetValue(t, out tempPool))
        {
            return tempPool;
        }
        if(tempPool == null && createIfNotExists)
        {
            lock (lockObj)
            {
                tempPool = new ConcurrentQueue<ICacheable>();
                mCachePool[t] = tempPool;
            }
        }
        return tempPool;
    }

    private static ConcurrentQueue<ICacheable> GetPool(Type t, bool createIfNotExists)
    {
        ConcurrentQueue<ICacheable> tempPool = null;
        if (mCachePool.TryGetValue(t, out tempPool))
        {
            return tempPool;
        }
        if (tempPool == null && createIfNotExists)
        {
            lock (lockObj)
            {
                tempPool = new ConcurrentQueue<ICacheable>();
                mCachePool[t] = tempPool;
            }
        }
        return tempPool; 
    }

    /// <summary>
    /// 获取某类型当前缓存的对象数
    /// </summary>
    /// <param name="t"></param>
    /// <returns></returns>
    public static int GetPoolCount(Type t)
    {
        ConcurrentQueue<ICacheable> pool = GetPool(t, false);
        if(pool != null) { return pool.Count; }
        return 0;
    }

}

