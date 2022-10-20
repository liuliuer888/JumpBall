/**
 * 文件名：SystemModuleType.cs
 * 文件描述：单例接口
 * 作者：liuzhitao
 * 创建时间：2022/10/20 17:08:44
 * 修改记录：
 */
using System;

public enum SystemModuleType
{
    None = 0,
    IInitializeable = 1,
    IResetable,
    IUpdateable,
    IFixedUpdateable,
    ILateUpdateable,
    Max,
}

public interface IInitializeable
{
    void OnInit();
    void OnDispose();
}

public interface IResetable
{
    void OnReset();
}

public interface IUpdateable
{
    void OnUpdate(float deltaTime);
}

public interface IFixedUpdateable
{
    void OnFixedUpdate(float deltaTime);
}

public interface ILateUpdateable
{
    void OnLateUpdate(float deltaTime);
}
