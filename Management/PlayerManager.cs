using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : ManagerClassBase<PlayerManager>
{
    public override void InitializeManagerClass() { }

    public Player player { get; set; } = null;

    public PlayerMove playerMove { get; set; } = null;

    public PlayerInput playerInput { get; set; } = null;

    public PlayerRightArm playerRightArm { get; set; } = null;

    public PlayerDeadbody playerDeadbody { get; set; } = null;
}
