using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AnimatorPro;
using UnityEngine.UI;

public class Player : MonoBehaviour, IDamagable
{
    #region AnimatorPro
    public static readonly int ID_Damaged = Animator.StringToHash("_Damaged");
    public static readonly int ID_Died = Animator.StringToHash("_Died");
    #endregion

    #region InInspector 
    [Header("Status")]
    [SerializeField] private string playerNameValue = "";
    [SerializeField] private float _PlayerMaxHp = 100;

    [Header("Component")]
    [SerializeField] private Text playerName = null;
    [SerializeField] private Text playerhp = null;
    [SerializeField] private Image playerhpbar = null;
    [SerializeField] private GameObject _playerdeadbody = null;

    [Header("AnimatorPro")]
    public AnimatorPro animatorPro;
    public Animator anim;
    #endregion

    #region Variable
    private GameObject targetWeaponImage;
    private GameObject weaponPanel_01;
    private GameObject weaponPanel_02;
    private GameObject weaponPanel_03;
    private GameObject weaponPanel_04;
    private GameObject weaponPanel_05;

    private Vector2 positionCurrent;
    private float weaponCHmoveing;

    [HideInInspector] public int weaponCount;
    [HideInInspector] public bool playerRight;

    public float Health { get; set; }
    public float MaxHealth { get; set; }
    public float Armor { get; set; } = 0;
    #endregion

    #region Component
    [HideInInspector] public BoxCollider2D _collider2D;
    [HideInInspector] public Rigidbody2D _rigidbody2D;

    private PlayerMove playerMove;
    private PlayerInput playerInput;
    private PlayerRightArm playerRightArm;
    #endregion

    private void Awake()
    {
        PlayerManager.Instance.player = this;

        _collider2D = GetComponent<BoxCollider2D>();
        _rigidbody2D = GetComponent<Rigidbody2D>();

        weaponCount = 1;
        weaponCHmoveing = 5f;
        playerRight = true;

        MaxHealth = _PlayerMaxHp;
        Health = MaxHealth;

        playerName.text = playerNameValue;
        playerhp.text = $"Hp | {Health} | {MaxHealth}";
    }

    private void Start()
    {
        playerMove = PlayerManager.Instance.playerMove;
        playerInput = PlayerManager.Instance.playerInput;
        playerRightArm = PlayerManager.Instance.playerRightArm;
        targetWeaponImage = GameObject.Find("TargetWeaponImage");

        weaponPanel_01 = GameObject.Find("WeaponPanel_01");
        weaponPanel_02 = GameObject.Find("WeaponPanel_02");
        weaponPanel_03 = GameObject.Find("WeaponPanel_03");
        weaponPanel_04 = GameObject.Find("WeaponPanel_04");
        weaponPanel_05 = GameObject.Find("WeaponPanel_05");
    }

    private void Update()
    {
        WeaponChange();

        CheckPlayerHandle();

        HpChangeUpdate();
    }

    private void WeaponChange()
    {

        if (Input.GetKeyDown(KeySet.keys[KeyAction.WP1]))
        {
            weaponCount = 1;
            weaponCHmoveing = 5f;
        }
        else if (Input.GetKeyDown(KeySet.keys[KeyAction.WP2]))
        {
            weaponCount = 2;
            weaponCHmoveing = 5f;
        }
        else if (Input.GetKeyDown(KeySet.keys[KeyAction.WP3]))
        {
            weaponCount = 3;
            weaponCHmoveing = 5f;
        }
        else if (Input.GetKeyDown(KeySet.keys[KeyAction.WP4]))
        {
            weaponCount = 4;
            weaponCHmoveing = 5f;
        }
        else if (Input.GetKeyDown(KeySet.keys[KeyAction.WP5]))
        {
            weaponCount = 5;
            weaponCHmoveing = 5f;
        }

        if (Input.GetKeyDown(KeySet.keys[KeyAction.WC]))
        {
            if (weaponCount <= 4)
            {
                weaponCount++;
                weaponCHmoveing = 1f;
            }
            else
            {
                weaponCount = 1;
                weaponCHmoveing = 5f;
            }

        }

        switch (weaponCount)
        {
            case 1:
                positionCurrent = weaponPanel_01.transform.position;
                break;
            case 2:
                positionCurrent = weaponPanel_02.transform.position;
                break;
            case 3:
                positionCurrent = weaponPanel_03.transform.position;
                break;
            case 4:
                positionCurrent = weaponPanel_04.transform.position;
                break;
            case 5:
                positionCurrent = weaponPanel_05.transform.position;
                break;
        }

        WeaponImageChange();
    }

    private void WeaponImageChange()
    {
        targetWeaponImage.transform.position = Vector3.MoveTowards(targetWeaponImage.transform.position, positionCurrent, weaponCHmoveing);
    }

    private void CheckPlayerHandle()
    {
        if (playerInput.InputKey.x <= -1f) playerRight = false;
        else if (playerInput.InputKey.x >= 1f) playerRight = true;
    }

    private void HpChangeUpdate()
    {
        playerhpbar.fillAmount = Mathf.Clamp(Health / MaxHealth, 0, 1);
        playerhp.text = $"Hp | {Health} / {MaxHealth}";
    }

    public void GetDamage(DamageInfo damageInfo)
    {
        animatorPro.SetTrigger(ID_Damaged);
        playerRightArm.DamagedAnimation();


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
            HpChangeUpdate();

            Instantiate(_playerdeadbody, transform.position, transform.rotation);

            gameObject.SetActive(false);
        }
    }
}
