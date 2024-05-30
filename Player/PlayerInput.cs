using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    // 키 입력받기
    public Vector2 InputKey;
    private float InputKey_X;
    private float InputKey_Y;

    private void Awake()
    {
        PlayerManager.Instance.playerInput = this;
    }

    private void Update()
    {
        InputKey_X = InputHorizontalKey();
        InputKey_Y = InputVerticalKey();


        InputKey = new Vector2(InputKey_X, InputKey_Y);
    }

    private float InputHorizontalKey()
    {
        if (Input.GetKey(KeySet.keys[KeyAction.LEFT])) return -1;
        else if (Input.GetKey(KeySet.keys[KeyAction.RIGHT])) return 1;
        else return 0;
    }
    private float InputVerticalKey()
    {
        if (Input.GetKey(KeySet.keys[KeyAction.UP])) return 1;
        else if (Input.GetKey(KeySet.keys[KeyAction.DOWN])) return -1;
        else return 0;
    }
}
