using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    #region Inspector
    [Header("Portal Position")]
    [SerializeField] private float VectorX = 0f;
    [SerializeField] private float VectorY = 0f;
    #endregion

    private Player player;
    private bool _BoolAction;

    private Vector3 _PortalMoving;

    private void Awake()
    {
        _PortalMoving = Vector3.zero;
    }

    private void Start()
    {
        player = PlayerManager.Instance.player;
    }

    private void Update()
    {
        if (!Input.GetKey(KeySet.keys[KeyAction.ACTION]) && _BoolAction) _BoolAction = false;
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            if (Input.GetKey(KeySet.keys[KeyAction.ACTION]) && !_BoolAction)
            {
                _BoolAction = true;

                _PortalMoving = new Vector2(VectorX, VectorY);

                player.transform.position = _PortalMoving;
            }
        }
    }
}
