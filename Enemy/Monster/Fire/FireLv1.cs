using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AnimatorPro;
using UnityEngine.PlayerLoop;

public class FireLv1 : MovementMonster, IDamagable
{

    #region IDamagable
    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public float Armor { get; set; }

    public void GetDamage(DamageInfo damageInfo)
    {
        animatorPro.SetTrigger(ID_Damaged);

        if (damageInfo.IsAbsolute)
        {
            Health -= damageInfo.Amount;
        }
        else
        {
            Health -= damageInfo.Amount - Armor;
        }

        if (Health <= 0)
        {
            animatorPro.SetTrigger(ID_Died);

            SetActive(false, 0.25f);
        }
    }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        Health = _Hp;
        MaxHealth = _MaxHp;
        Armor = _Armor;

        CheckDamageType(2);
        damageInfo.Amount = 7.5f;
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override IEnumerator OR_CheckPlayerRange()
    {
        RaycastHit2D hit;

        while (true)
        {
            yield return new WaitForSeconds(0.5f);

            if (Vector3.Distance(transform.position, player.transform.position) <= _FarInstance)
            {
                boxCollider2D.enabled = false;
                hit = Physics2D.Linecast(transform.position, player.transform.position, layerMask);
                boxCollider2D.enabled = true;

                if (player.gameObject.activeInHierarchy)
                {
                    if (hit.collider.CompareTag("Player") && _FarAttack)
                    {
                        _MonsterMoveable = false;
                        velocity.x = 0f;

                        animatorPro.SetTrigger(ID_Attack);

                        yield return new WaitForSeconds(0.5f);

                        MonsterBullet bullet = Instantiate(_FireBall);
                        bullet._ParentTransform = transform;
                        bullet.damageInfo = damageInfo;

                        yield return new WaitForSeconds(2f);
                    }
                }

            }
            else _MonsterMoveable = true;
        }
    }
}
