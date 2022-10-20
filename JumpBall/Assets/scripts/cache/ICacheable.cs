/**
 * 文件名：ICacheable.cs
 * 文件描述：标识一个对象是可缓存的接口
 * 作者：liuzhitao
 * 创建时间：2022/10/20 18:06
 * 修改记录：
 */
public interface ICacheable
{
    /// <summary>
    /// 在放入缓存池之前会调用的方法，用于重置成可复用状态
    /// </summary>
    void FreeToCache();
}