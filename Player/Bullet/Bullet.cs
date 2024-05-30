using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AnimatorPro;

[RequireComponent(typeof(AnimatorPro))]
public class Bullet : MonoBehaviour
{

    #region Animator
    public static readonly int ID_destroy = Animator.StringToHash("_Destroy");
    #endregion

    [SerializeField] private int bullet_Damage;
    [SerializeField] private float _bulletMove = 5f;

    public float moveX;
    public float moveY;
    public int BulletType;
    public DamageInfo damageInfo;

    private float DestroyBulletTime;

    public AnimatorPro animatorPro;
    public Animator anim;

    private Player player;

    private void Awake()
    {
        animatorPro = GetComponent<AnimatorPro>();
        animatorPro.Init(anim);

        CheckDamageType();
    }

    private void Start()
    {
        player = PlayerManager.Instance.player;
    }

    private void Update()
    {
        MovingBullet();

        CheckDamageType();
    }

    private void MovingBullet()
    {
        if (moveX <= -0.1f)
        {
            transform.Translate(Vector2.left * _bulletMove * Time.deltaTime);
        }
        else if (moveX >= 0.1f)
        {
            transform.Translate(Vector2.right * _bulletMove * Time.deltaTime);
        }
        else if (moveY <= -0.1f)
        {
            transform.Translate(Vector2.right * _bulletMove * Time.deltaTime);
        }
        else if (moveY >= 0.1f)
        {
            transform.Translate(Vector2.right * _bulletMove * Time.deltaTime);
        }
    }

    private void CheckDamageType()
    {
        switch (BulletType)
        {
            case 1:
                damageInfo.DamageType = DamageType.Normal;
                damageInfo.Amount = Random.Range(1, 4);
                damageInfo.IsAbsolute = false;
                DestroyBulletTime = 0f;
                break;
            case 2:
                damageInfo.DamageType = DamageType.Fire;
                damageInfo.Amount = 2;
                damageInfo.IsAbsolute = false;
                DestroyBulletTime = 0f;
                break;
            case 3:
                damageInfo.DamageType = DamageType.Earth;
                damageInfo.Amount = Random.Range(5, 12);
                damageInfo.IsAbsolute = true;
                DestroyBulletTime = 0.2f;
                break;
            case 4:
                damageInfo.DamageType = DamageType.Thunrder;
                damageInfo.Amount = Random.Range(2, 4);
                damageInfo.IsAbsolute = false;
                DestroyBulletTime = 0.2f;
                break;
            case 5:
                damageInfo.DamageType = DamageType.Ice;
                damageInfo.Amount = 4;
                damageInfo.IsAbsolute = false;
                DestroyBulletTime = 0.18f;
                break;
            default:
                damageInfo.DamageType = DamageType.Normal;
                damageInfo.Amount = Random.Range(1, 2);
                damageInfo.IsAbsolute = false;
                DestroyBulletTime = 0.18f;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        void DestroyBullet()
        {
            if (BulletType > 2) animatorPro.SetTrigger(ID_destroy);
            if (BulletType != 2) Destroy(gameObject, DestroyBulletTime);
            moveX = 0;
            moveY = 0;
        }

        if (coll.tag == "Tile")
        {
            DestroyBullet();

            return;
        }

        IDamagable enemy = coll.GetComponent<IDamagable>();

        if (enemy != null && (object)enemy != player)
        {
            if (enemy.Health >= 0)
            {
                enemy.GetDamage(damageInfo);

                DestroyBullet();
            }
        }
    }
}
