using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettingManager : ManagerClassBase<GameSettingManager>
{
    public override void InitializeManagerClass() { }

    public GameSetting gameSetting { get; set; } = null;

    public KeySetting keySetting { get; set; } = null;

    public FollowPlayerCamera followPlayerCamera { get; set; } = null;

    public MainTitle mainTitle { get; set; } = null;

    public DialogEvent dialogEvent { get; set; } = null;
}
