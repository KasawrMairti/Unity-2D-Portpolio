using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    [SerializeField] private Bullet _bulletNormal;
    [SerializeField] private Bullet _bulletFire;
    [SerializeField] private Bullet _bulletFireUp;
    [SerializeField] private Bullet _bulletFireDown;
    [SerializeField] private Bullet _bulletEarth;
    [SerializeField] private Bullet _bulletThurnder;
    [SerializeField] private Bullet _bulletIce;

    [SerializeField] private GameObject _Original_Front;
    [SerializeField] private GameObject _SpawnBullet_Front;
    [SerializeField] private GameObject _SpawnFire_Front;
    [SerializeField] private GameObject _SpawnBullet_Up;
    [SerializeField] private GameObject _SpawnFire_Up;
    [SerializeField] private GameObject _SpawnBullet_Down;
    [SerializeField] private GameObject _SpawnFire_Down;

    private Player player;
    private PlayerInput playerInput;
    private PlayerRightArm playerRightArm;
    private PlayerMove playerMove;

    private float LastTime;
    private float BulletTime;

    private void Start()
    {
        player = PlayerManager.Instance.player;
        playerInput = PlayerManager.Instance.playerInput;
        playerRightArm = PlayerManager.Instance.playerRightArm;
        playerMove = PlayerManager.Instance.playerMove;
    }

    private void Update()
    {
        ShootBullet();
    }

    private void ShootBullet()
    {
        if (Input.GetKey(KeySet.keys[KeyAction.SHOT]) && playerMove.canMove)
        {
            if (Time.time >= LastTime + BulletTime)
            {
                Bullet bullet;
                playerRightArm.ShotAnimation();

                LastTime = Time.time;

                switch (player.weaponCount)
                {
                    case 1:
                        BulletTime = 0.25f;
                        bullet = Instantiate(_bulletNormal);
                        Destroy(bullet.gameObject, 4);
                        break;
                    case 2:
                        BulletTime = 0.6f;
                        if (playerInput.InputKey.y >= 0.1f) bullet = Instantiate(_bulletFireUp);
                        else if (playerInput.InputKey.y <= -0.1f && !(playerMove.isGround)) bullet = Instantiate(_bulletFireDown);
                        else bullet = Instantiate(_bulletFire);
                        Destroy(bullet.gameObject, 0.4f);
                        break;
                    case 3:
                        BulletTime = 0.75f;
                        bullet = Instantiate(_bulletEarth);
                        Destroy(bullet.gameObject, 4);
                        break;
                    case 4:
                        BulletTime = 0.5f;
                        bullet = Instantiate(_bulletThurnder);
                        Destroy(bullet.gameObject, 4);
                        break;
                    case 5:
                        BulletTime = 0.5f;
                        bullet = Instantiate(_bulletIce);
                        Destroy(bullet.gameObject, 4);
                        break;
                    default:
                        BulletTime = 0.25f;
                        bullet = Instantiate(_bulletNormal);
                        Destroy(bullet.gameObject, 4);
                        break;
                }

                if (player.weaponCount != 2)
                {
                    if (playerInput.InputKey.y >= 0.1f) bullet.transform.position = _SpawnBullet_Up.transform.position;
                    else if (playerInput.InputKey.y <= -0.1f && !(playerMove.isGround)) bullet.transform.position = _SpawnBullet_Down.transform.position;
                    else bullet.transform.position = _SpawnBullet_Front.transform.position;

                }
                else
                {
                    if (playerInput.InputKey.y >= 0.1f) bullet.transform.position = _SpawnFire_Up.transform.position;
                    else if (playerInput.InputKey.y <= -0.1f && !(playerMove.isGround)) bullet.transform.position = _SpawnFire_Down.transform.position;
                    else bullet.transform.position = _SpawnFire_Front.transform.position;
                }


                bullet.moveX = 0f;
                bullet.moveY = 0f;

                if (playerInput.InputKey.y >= 0.1f)
                {
                    bullet.moveY = 1f;
                    if (player.playerRight) bullet.transform.localScale = new Vector2(1, 1);
                    else bullet.transform.localScale = new Vector2(1, -1);

                    if (player.weaponCount != 2) bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
                    else
                    {
                        bullet.transform.localScale = new Vector2(1, 1);
                        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    }
                }
                else if (playerInput.InputKey.y <= -0.1f && !(playerMove.isGround))
                {
                    bullet.moveY = -1f;
                    if (player.playerRight) bullet.transform.localScale = new Vector2(1, 1);
                    else bullet.transform.localScale = new Vector2(1, -1);

                    if (player.weaponCount != 2) bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, -90));
                    else
                    {
                        bullet.transform.localScale = new Vector2(1, 1);
                        bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    }
                }
                else if (playerInput.InputKey.y <= -0.1f && playerMove.isGround)
                {
                    if (player.playerRight)
                    {
                        bullet.moveX = 1f;
                        bullet.transform.localScale = new Vector2(1, 1);

                        _SpawnFire_Front.transform.position = new Vector2(_Original_Front.transform.position.x + 0.5f, _Original_Front.transform.position.y + 0.3f);

                    }
                    else
                    {
                        bullet.moveX = -1f;
                        bullet.transform.localScale = new Vector2(-1, 1);

                        _SpawnFire_Front.transform.position = new Vector2(_Original_Front.transform.position.x + -0.5f, _Original_Front.transform.position.y + 0.3f);
                    }

                    if (player.weaponCount != 2)
                    {
                        if (player.playerRight)
                            _SpawnBullet_Front.transform.position = new Vector2(_Original_Front.transform.position.x + 0.55f, _Original_Front.transform.position.y + -(0.02f));
                        else _SpawnBullet_Front.transform.position = new Vector2(_Original_Front.transform.position.x + -0.55f, _Original_Front.transform.position.y + -(0.02f));
                        bullet.transform.position = _SpawnBullet_Front.transform.position;
                    }

                    bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));

                }
                else if (player.playerRight)
                {
                    bullet.moveX = 1f;

                    _SpawnFire_Front.transform.position = new Vector2(_Original_Front.transform.position.x + 0.5f, _Original_Front.transform.position.y + 0.4f);

                    if (player.weaponCount != 2)
                    {
                        _SpawnBullet_Front.transform.position = new Vector2(_Original_Front.transform.position.x + 0.55f, _Original_Front.transform.position.y + 0.1f);
                        bullet.transform.position = _SpawnBullet_Front.transform.position;
                    }
                    bullet.transform.localScale = new Vector2(1, 1);
                    bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }
                else
                {
                    bullet.moveX = -1f;

                    _SpawnFire_Front.transform.position = new Vector2(_Original_Front.transform.position.x + -0.5f, _Original_Front.transform.position.y + 0.4f);

                    if (player.weaponCount != 2)
                    {
                        _SpawnBullet_Front.transform.position = new Vector2(_Original_Front.transform.position.x + -0.55f, _Original_Front.transform.position.y + 0.1f);
                        bullet.transform.position = _SpawnBullet_Front.transform.position;
                    }
                    bullet.transform.localScale = new Vector2(-1, 1);
                    bullet.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                }

                bullet.BulletType = player.weaponCount;
            }
        }
    }
}
