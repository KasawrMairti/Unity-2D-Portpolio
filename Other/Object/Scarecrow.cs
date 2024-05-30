using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AnimatorPro;

[RequireComponent(typeof(AnimatorPro))]
public class Scarecrow : MonoBehaviour, IDamagable
{
    #region IDamagable
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public float Armor { get; set; }

    public void GetDamage(DamageInfo damageInfo)
    {
        animatorPro.SetTrigger("_Damaged");
    }
    #endregion

    #region Inspector
    private AnimatorPro animatorPro;
    private Animator anim;
    #endregion

    private void Awake()
    {
        animatorPro = GetComponent<AnimatorPro>();
        anim = GetComponent<Animator>();
        animatorPro.Init(anim);

        MaxHealth = 99999999;
        Health = MaxHealth;
        Armor = 9999999;
    }
}
