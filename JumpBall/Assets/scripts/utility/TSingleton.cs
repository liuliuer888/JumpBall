/**
 * 文件名：TSingleton.cs
 * 文件描述：全局单例
 * 作者：liuzhitao
 * 创建时间：2022/10/20 17:08:44
 * 修改记录：
 */

public class TSingleton<T> where T : class, IInitializeable, new()
{
    private static T m_Instance;
    public static T Instance
    {
        get
        {
            if (m_Instance == null)
            {
                m_Instance = SystemManager.Instance.AddSystemMgr<T>();
                m_Instance.OnInit();                // 获取单例就初始化数据
            }

            return m_Instance;
        }
    }
}
