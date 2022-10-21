using System.Collections;
using System.Collections.Generic;

public enum GameType
{
    NONE = 0,
    CLASS = 1,
    CHALLENGE = 2,
}

public class PlayerDataMgr : TSingleton<PlayerDataMgr>, IInitializeable
{
    public GameType eCurGameType = GameType.NONE;

    public void OnInit()
    {

    }
    public void OnDispose()
    {

    }
}
