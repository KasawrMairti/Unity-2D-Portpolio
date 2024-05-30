using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityProjectStartupFramework;

public sealed class GameManager : GameManagerBase
{
    protected override void InitializeManagerClasses()
    {
        RegisterManagerClass<PlayerManager>();
        RegisterManagerClass<GameSettingManager>();
    }
}
