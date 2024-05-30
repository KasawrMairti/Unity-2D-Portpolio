using UnityEngine;
using UnityEngine.AnimatorPro;

[RequireComponent(typeof(AnimatorPro))]
public class PlayerMove : MonoBehaviour
{
    #region Animator
    public static readonly int ID_HeroHorizontal = Animator.StringToHash("_Horizontal");
    public static readonly int ID_HeroVertical = Animator.StringToHash("_Vertical");
    public static readonly int ID_motionVer = Animator.StringToHash("_MotionVer");
    public static readonly int ID_isGround = Animator.StringToHash("_IsGround");
    #endregion

    #region Player Public Unity
    [Header ("Moving")]
    [SerializeField] private float _PlayerMove = 1.5f;
    [SerializeField] private float _PlayerJumping = 5.0f;
    [SerializeField] private float _GravityJump = 5;
    [SerializeField] private float _GravityFall = 6;
    [SerializeField] private LayerMask[] _LayerMask;

    [Header ("Setting")]
    public bool _PlayerJump = true;
    public bool _PlayerInfinityJump = false;
    public bool _PlayerActive = true;

    [Header("Animator")]
    [SerializeField] private SpriteRenderer sprite;
    public AnimatorPro animatorPro;
    public Animator anim;
    #endregion

    #region variable
    [HideInInspector] public Vector2 velocity;
    [HideInInspector] public bool canMove;
    [HideInInspector] public bool isGround;
    [HideInInspector] public Vector3 Axis;
    #endregion

    #region Component
    private Player player;
    private PlayerInput playerInput;
    private Rigidbody2D rigidbody2D;
    //private BoxCollider2D boxCollider;
    CapsuleCollider2D boxCollider;
    #endregion

    #region Awake Start Update
    private void Awake()
    {
        PlayerManager.Instance.playerMove = this;

        rigidbody2D = GetComponentInParent<Rigidbody2D>();
        boxCollider = GetComponentInParent<CapsuleCollider2D>();
        animatorPro = GetComponent<AnimatorPro>();
        animatorPro.Init(anim);

        canMove = true;
        isGround = false;

        Axis = Vector3.zero;
        velocity = Vector2.zero;
    }

    private void Start()
    {
        player = PlayerManager.Instance.player;
        playerInput = PlayerManager.Instance.playerInput;
    }

    private void Update()
    {
        if (canMove) Axis = playerInput.InputKey;
        else
        {
            Axis = Vector2.zero;
            rigidbody2D.velocity = Vector2.zero;
        }

        playerAnimator();
    }

    private void FixedUpdate()
    {
        GroundCheck();
        FixedGravityCheck(); // 중력체크

        if (canMove)
        {
            Flip(Axis); // 이미지 좌우 뒤집기

            FixedHorizontalMove(); // 좌우 이동
            FixedVerticalMove(); // 상하 이동
        }
    }
    #endregion

    #region Update
    private void playerAnimator()
    {
        animatorPro.SetParam(ID_HeroHorizontal, Axis.x);
        animatorPro.SetParam(ID_motionVer, Axis.y);
    }
    #endregion

    #region FixedUpdate
    private void GroundCheck()
    {
        var Point = Vector2.zero;
        var Bound = boxCollider.bounds;
        Point.x = Bound.center.x;
        Point.y = Bound.min.y;

        for (int i = 0; i < _LayerMask.Length; i++)
        {
            isGround = Physics2D.OverlapCircle(Point, 0.025f, _LayerMask[i]);

            if (isGround) break;
        }

        animatorPro.SetParam(ID_isGround, isGround);
    }

    private void Flip(Vector3 Axis)
    {
        if (Axis.x > 0) sprite.flipX = false;
        else if (Axis.x < 0) sprite.flipX = true;
    }

    private void FixedGravityCheck()
    {
        rigidbody2D.gravityScale = 1f;

        if (rigidbody2D.velocity.y > 0f) rigidbody2D.gravityScale = _GravityJump;
        else if (rigidbody2D.velocity.y < 0f) rigidbody2D.gravityScale = _GravityFall;

        animatorPro.SetParam(ID_HeroVertical, rigidbody2D.velocity.y);
    }

    private void FixedHorizontalMove()
    {
        var vel = rigidbody2D.velocity;

        if (canMove) vel.x = Axis.x * (_PlayerMove * 100) * Time.deltaTime;
        else vel.x = 0f;

        rigidbody2D.velocity = vel;
    }

    private void FixedVerticalMove()
    {
        if ((!animatorPro.GetParam<bool>(ID_isGround) || !(Input.GetKey(KeySet.keys[KeyAction.JUMP]))) && !_PlayerInfinityJump) return;

        var vel = rigidbody2D.velocity;
        vel.y = 0f;
        rigidbody2D.velocity = vel;

        rigidbody2D.AddForce(Vector2.up * _PlayerJumping, ForceMode2D.Impulse);
    }
    #endregion
}
