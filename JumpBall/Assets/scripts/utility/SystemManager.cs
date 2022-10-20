using System;
using System.Collections.Generic;
using System.Reflection;

public class SystemManager
{
    public readonly static SystemManager Instance = new SystemManager();

    private List<IInitializeable> m_Initializeables = new List<IInitializeable>();
    private List<IUpdateable> m_Updateables = new List<IUpdateable>();
    private List<IFixedUpdateable> m_FixedUpdateables = new List<IFixedUpdateable>();
    private List<ILateUpdateable> m_LateUpdateables = new List<ILateUpdateable>();
    private List<IResetable> m_Resetables = new List<IResetable>();

    public T AddSystemMgr<T>() where T : class, IInitializeable, new()
    {
        T sys = new T();

        IInitializeable initializeable = (sys as IInitializeable);
        if (initializeable != null)
        {
            m_Initializeables.Add(initializeable);
        }

        IUpdateable updateable = (sys as IUpdateable);
        if (updateable != null)
        {
            m_Updateables.Add(updateable);
        }

        ILateUpdateable lateUpdateable = (sys as ILateUpdateable);
        if (lateUpdateable != null)
        {
            m_LateUpdateables.Add(lateUpdateable);
        }

        IFixedUpdateable fixedUpdateable = (sys as IFixedUpdateable);
        if (fixedUpdateable != null)
        {
            m_FixedUpdateables.Add(fixedUpdateable);
        }

        IResetable resetable = (sys as IResetable);
        if (resetable != null)
        {
            m_Resetables.Add(resetable);
        }

        return sys;
    }

    public void OnInit()
    {
        for(int i = 0; i < m_Initializeables.Count; ++i)
        {
            m_Initializeables[i].OnInit();
        }
    }

    public void OnDispose()
    {
        for (int i = m_Initializeables.Count - 1; i >= 0; --i)
        {
            m_Initializeables[i].OnDispose();
        }
    }

    public void OnReset()
    {
        for (int i = 0; i < m_Resetables.Count; ++i)
        {
            m_Resetables[i].OnReset();
        }
    }

    public void OnFixedUpdate(float deltaTime)
    {
        for (int i = 0; i < m_FixedUpdateables.Count; i++)
        {
            m_FixedUpdateables[i].OnFixedUpdate(deltaTime);
        }
    }

    public void OnUpdate(float deltaTime)
    {
        for (int i = 0; i < m_Updateables.Count; i++)
        {
            m_Updateables[i].OnUpdate(deltaTime);
        }
    }

    public void OnLateUpdate(float deltaTime)
    {
        for (int i = 0; i < m_LateUpdateables.Count; i++)
        {
            m_LateUpdateables[i].OnLateUpdate(deltaTime);
        }
    }
}
