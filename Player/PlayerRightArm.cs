using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AnimatorPro;

[RequireComponent(typeof(AnimatorPro))]
public class PlayerRightArm : MonoBehaviour
{
    public static readonly int ID_HeroHorizontal = Animator.StringToHash("_Horizontal");
    public static readonly int ID_HeroVertical = Animator.StringToHash("_Vertical");
    public static readonly int ID_motionVer = Animator.StringToHash("_MotionVer");
    public static readonly int ID_isGround = Animator.StringToHash("_IsGround");
    public static readonly int ID_isShot = Animator.StringToHash("_IsShot");
    public static readonly int ID_Damaged = Animator.StringToHash("_Damaged");

    private PlayerMove playerMove;
    private PlayerInput playerInput;

    private Rigidbody2D rigidbody2d;
    private SpriteRenderer sprite;
    private Vector3 Axis;

    public AnimatorPro animatorPro;
    public Animator anim;

    private void Awake()
    {
        PlayerManager.Instance.playerRightArm = this;

        rigidbody2d = GetComponentInParent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        Axis = Vector3.zero;

        animatorPro = GetComponent<AnimatorPro>();
        animatorPro.Init(anim);
    }

    private void Start()
    {
        playerMove = PlayerManager.Instance.playerMove;
        playerInput = PlayerManager.Instance.playerInput;
    }

    private void Update()
    {
        HeroCheckAnimation();

        if (playerMove.canMove)
        {
            Axis = playerInput.InputKey;
            Flip(Axis);
        }
        else
        {
            Axis = Vector2.zero;
        }
    }

    private void HeroCheckAnimation()
    {
        // 좌, 우 이동 애니메이션
        animatorPro.SetParam(ID_HeroHorizontal, Axis.x);
        animatorPro.SetParam(ID_HeroVertical, rigidbody2d.velocity.y);

        // 위 아래 체크 확인 애니메이션
        animatorPro.SetParam(ID_motionVer, Axis.y);

        //땅 충돌을 실시간으로 애니메이터에 넘긴다.
        animatorPro.SetParam(ID_isGround, playerMove.isGround);
    }

    public void ShotAnimation()
    {
        animatorPro.SetTrigger(ID_isShot);
    }

    public void DamagedAnimation()
    {
        animatorPro.SetTrigger(ID_Damaged);
    }

    private void Flip(Vector3 Axis)
    {
        if (Axis.x > 0) sprite.flipX = false;
        else if (Axis.x < 0) sprite.flipX = true;
    }
}
