using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class baricade : MonoBehaviour
{
    private Player player;
    private DamageInfo damageInfo;

    private void Start()
    {
        player = PlayerManager.Instance.player;

        damageInfo.DamageType = DamageType.Normal;
        damageInfo.Amount = 10.0f;
        damageInfo.IsAbsolute = true;
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        IDamagable player = coll.GetComponent<IDamagable>();

        if (player != null && (object)player == this.player)
            player.GetDamage(damageInfo);
    }
}
