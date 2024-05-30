using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AnimatorPro;

[RequireComponent(typeof(AnimatorPro))]
public class MonsterBullet : MonoBehaviour
{
    #region Animator
    public static readonly int ID_destroy = Animator.StringToHash("_Destroy");
    #endregion

    #region Inspector
    [Header("Spawn Check")]
    [SerializeField] private bool _FlipX = false;
    [SerializeField] private float _DestroyTimer = 5f;
    [SerializeField] private float _BulletSpeed = 5f;
    [SerializeField] private GameObject FireBoom;

    [Header("Spawn Scale")]
    [SerializeField] private bool _SpawnScaleBool = false;
    [SerializeField] private float _SpawnScaleHigher = 5f;

    [Header("Tracking")]
    [SerializeField] private bool _TrackingCheck = true;
    [SerializeField] private float _TrackingTimer = 1f;

    [Header("PlayerCheck")]
    [SerializeField] private float DestroyBulletTime = 0.5f;

    [Header("AnimatorPro")]
    private AnimatorPro animatorPro;
    private Animator anim;
    #endregion

    #region Variable
    private Player player;

    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidbody2D;

    [HideInInspector] public Transform _ParentTransform;
    public DamageInfo damageInfo;
    #endregion

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        animatorPro = GetComponent<AnimatorPro>();
        anim = GetComponent<Animator>();
        animatorPro.Init(anim);

        if (_SpawnScaleBool) transform.localScale = Vector3.zero;
    }

    private void Start()
    {
        Destroy(gameObject, _DestroyTimer);

        transform.position = _ParentTransform.position;

        player = PlayerManager.Instance.player;

        if (_FlipX) spriteRenderer.flipX = true;

        if (_SpawnScaleBool) StartCoroutine(SpawnScaleEvent());

        if (_TrackingCheck) StartCoroutine(Tracking());

        Vector2 dir = player.transform.position - transform.position;
        transform.right = dir; // rotation
        rigidbody2D.velocity = dir.normalized * _BulletSpeed; // 방향 + 크기 1고정
    }

    private IEnumerator SpawnScaleEvent()
    {
        for (int i = 0; i < _SpawnScaleHigher; i++)
        {
            transform.localScale += new Vector3(0.1f, 0.1f, 0.1f);

            yield return new WaitForSeconds(0.2f);
        }
    }

    private IEnumerator Tracking()
    {
        while (true)
        {
            yield return new WaitForSeconds(_TrackingTimer);

            Vector2 dir = player.transform.position - transform.position;
            transform.right = dir; // rotation
            rigidbody2D.velocity = dir.normalized * _BulletSpeed; // 방향 + 크기 1고정
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        void DestroyBullet()
        {
            animatorPro.SetTrigger(ID_destroy);
            GameObject fire = Instantiate(FireBoom, transform);
            rigidbody2D.velocity = Vector3.zero;

            Destroy(gameObject, DestroyBulletTime);
        }

        if (coll.CompareTag("Tile"))
        {
            DestroyBullet();

            return;
        }

        IDamagable player = coll.GetComponent<IDamagable>();

        if (player != null && (object)player == this.player)
        {
            player.GetDamage(damageInfo);

            DestroyBullet();
        }
    }
}
