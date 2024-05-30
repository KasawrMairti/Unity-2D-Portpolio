using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class FireLv2 : MovementMonster, IDamagable
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

            SetActive(false, 0.55f);
        }
    }
    #endregion

    [SerializeField] private GameObject _suna = null;

    protected override void Awake()
    {
        base.Awake();

        Health = _Hp;
        MaxHealth = _MaxHp;
        Armor = _Armor;

        CheckDamageType(2);
        damageInfo.Amount = 15f;
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
                    if (hit.collider.CompareTag("Player"))
                    {
                        velocity.x = 0f;
                        GameObject suna = Instantiate(_suna, transform);
                        suna.transform.position = transform.position;
                        Destroy(suna, 1f);

                        animatorPro.SetTrigger(ID_Attack);

                        yield return new WaitForSeconds(1f);

                        for (int i = 0; i < 10; i++)
                        {
                            velocity.x += 0.1f * Right;
                            Debug.Log("1. velocity.x :" + velocity.x);
                            velocity.x *= PlayerDirection();
                            Debug.Log("2. velocity.x :" + velocity.x);
                            yield return new WaitForSeconds(0.1f);
                        }

                        yield return new WaitForSeconds(2f);

                        velocity.x = _MonsterMoveSpeed;
                    }
                }

            }
            else _MonsterMoveable = true;
        }
    }
}
