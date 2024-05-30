using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AnimatorPro;
using UnityEngine.Timeline;

public abstract class MovementMonster : MonoBehaviour
{
    #region Animator
    public static readonly int ID_Horizontal = Animator.StringToHash("_Horizontal");
    public static readonly int ID_Attack = Animator.StringToHash("_Attack");
    public static readonly int ID_Damaged = Animator.StringToHash("_Damaged");
    public static readonly int ID_Died = Animator.StringToHash("_Died");
    #endregion

    #region Player Public Unity
    [Header("Moving")]
    [SerializeField] protected float _MonsterMoveSpeed = 1.5f;
    [SerializeField] protected LayerMask[] _LayerMask;

    [Header("Status")]
    [SerializeField] protected float _Hp = 1f;
    [SerializeField] protected float _MaxHp = 1f;
    [SerializeField] protected float _Armor = 1f;

    [Header("Setting")]
    [SerializeField] protected bool _MonsterMoveable = true;
    [SerializeField] protected bool _MonsterActive = true;

    [Header("Attack")]
    [SerializeField] protected MonsterBullet _FireBall = null;
    [SerializeField] protected bool _FarAttack = true;
    [SerializeField] protected float _FarInstance = 5.0f;
    [SerializeField] protected LayerMask layerMask;

    [Header("Animator")]
    [SerializeField] protected AnimatorPro animatorPro;
    [SerializeField] protected Animator anim;
    [SerializeField] protected bool FlipX = false;
    #endregion

    #region variable
    protected Vector3 velocity;
    protected bool canMove;
    protected float Right;

    protected DamageInfo damageInfo;
    protected int monsterType;
    #endregion

    #region Component
    protected Player player;
    protected BoxCollider2D boxCollider2D;
    protected Rigidbody2D rigidbody2D;
    protected SpriteRenderer sprite;
    #endregion

    protected virtual void Awake()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        animatorPro = GetComponentInChildren<AnimatorPro>();
        animatorPro.Init(anim);

        velocity = Vector3.right;
        canMove = true;
    }

    protected virtual void Start()
    {
        player = PlayerManager.Instance.player;

        StartCoroutine(OR_CheckPlayerRange());
    }

    protected virtual void Update()
    {
        if (velocity.x > 0) Right = 1f;
        else if (velocity.x < 0) Right = -1f;

        CheckMoveAnimation();

        MovingMonster();
    }

    protected virtual void OnEnable()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        rigidbody2D = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        animatorPro = GetComponentInChildren<AnimatorPro>();
        animatorPro.Init(anim);

        velocity = Vector3.right;
        canMove = true;

        player = PlayerManager.Instance.player;

        StartCoroutine(OR_CheckPlayerRange());
    }

    protected virtual void CheckMoveAnimation()
    {
        animatorPro.SetParam(ID_Horizontal, velocity.x);

        if (velocity.x > 0) 
            if (FlipX) sprite.flipX = true;
            else sprite.flipX = false;
        else if (velocity.x < 0)
            if (FlipX) sprite.flipX = false;
            else sprite.flipX = true;
    }

    protected virtual void MovingMonster()
    {
        bool Front = false;
        Vector2 Point = Vector2.zero;
        Bounds Bound = boxCollider2D.bounds;
        if (velocity.x > 0) Point.x = Bound.max.x;
        else if (velocity.x < 0) Point.x = Bound.min.x;
        Point.y = Bound.center.y;

        for (int i = 0; i < _LayerMask.Length; i++)
        {
            Front = Physics2D.OverlapCircle(Point, 0.025f, _LayerMask[i]);

            if (Front) break;
        }

        if (Front && _MonsterMoveable) velocity.x = -velocity.x;

        if (_MonsterMoveable && velocity.x == 0)
            if (sprite.flipX) velocity.x = -1;
            else velocity.x = 1;

        if (!_MonsterMoveable)
        {
            Vector2 dir;

            dir.x = transform.position.x - player.transform.position.x;

            if (FlipX) sprite.flipX = dir.x <= 0 ? true : false;
            else sprite.flipX = dir.x <= 0 ? false : true;
            velocity.x = 0f;
        }

        rigidbody2D.velocity = velocity * (_MonsterMoveSpeed * 100) * Time.deltaTime;
    }

    protected virtual void CheckDamageType(int monsterType)
    {
        switch (monsterType)
        {
            case 1:
                damageInfo.DamageType = DamageType.Normal;
                damageInfo.IsAbsolute = false;
                break;
            case 2:
                damageInfo.DamageType = DamageType.Fire;
                damageInfo.IsAbsolute = false;
                break;
            case 3:
                damageInfo.DamageType = DamageType.Earth;
                damageInfo.IsAbsolute = true;
                break;
            case 4:
                damageInfo.DamageType = DamageType.Thunrder;
                damageInfo.IsAbsolute = false;
                break;
            case 5:
                damageInfo.DamageType = DamageType.Ice;
                damageInfo.IsAbsolute = false;
                break;
            default:
                damageInfo.DamageType = DamageType.Normal;
                damageInfo.IsAbsolute = false;
                break;
        }
    }

    protected virtual IEnumerator OR_CheckPlayerRange()
    {
        yield return null;
    }

    protected virtual void SetActive(bool Active, float LastTime)
    {
        StartCoroutine(SettingActive(Active, LastTime));
    }

    protected virtual IEnumerator SettingActive(bool Active, float LastTime)
    {
        yield return new WaitForSeconds(LastTime);

        gameObject.SetActive(Active);
    }

    protected virtual float PlayerDirection()
    {
        float direction;

        if (transform.position.x < player.transform.position.x) direction = 1f;
        else direction = -1f;

        return direction;
    }
}
