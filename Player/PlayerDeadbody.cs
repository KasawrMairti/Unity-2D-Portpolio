using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AnimatorPro;

public class PlayerDeadbody : MonoBehaviour
{
    #region Inspector
    [Header("Original")]
    [SerializeField] private Material material;
    [SerializeField] private LayerMask[] layerMask;
    #endregion

    #region Variable
    private Vector3 position;
    private float positionX;
    private float positionY;
    #endregion

    #region Component
    private Player player;
    #endregion

    private void Awake()
    {
        PlayerManager.Instance.playerDeadbody = this;

        positionX = -8.0f;
        positionY = 0f;
        position = new Vector2(positionX, positionY);
    }

    private void Start()
    {
        player = PlayerManager.Instance.player;

        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        bool CheckGround;

        while (true)
        {
            CheckGround = false;

            for (int i = 0; i < layerMask.Length; i++)
            {
                position = new Vector2(positionX, positionY);

                CheckGround = Physics2D.Raycast(player.transform.position + position, Vector2.down, 10.0f, layerMask[i]);

                if (CheckGround) break;

                positionX += 0.1f;
            }

            if (CheckGround) break;
        }

        yield return new WaitForSeconds(3.0f);

        player.Health = player.MaxHealth;
        player.GetComponentInChildren<SpriteRenderer>().material = material;
        player.GetComponentInChildren<AnimatorPro>().gameObject.transform.position = player.transform.position;
        player.gameObject.SetActive(true);
        player.transform.position = transform.position;
        Destroy(gameObject);
    }
}
