using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum KeyAction 
{ SHOT, JUMP, WC, ACTION, SUICIDE, RESTART, WP1, WP2, WP3, WP4, WP5, LEFT, UP, RIGHT, DOWN}

public static class KeySet { public static Dictionary<KeyAction, KeyCode> keys = new Dictionary<KeyAction, KeyCode>(); }

public class KeySetting : MonoBehaviour
{
    #region 변수
    KeyCode[] defaultKeys = new KeyCode[]
    { KeyCode.Z, KeyCode.X, KeyCode.C, KeyCode.A, KeyCode.Q, KeyCode.R, KeyCode.Alpha1, KeyCode.Alpha2
    , KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.LeftArrow, KeyCode.UpArrow, KeyCode.RightArrow, KeyCode.DownArrow};

    private int key = -1;

    public GameObject GameSettingPanel;
    public Text[] txt;
    #endregion

    private void Awake()
    {
        GameSettingManager.Instance.keySetting = this;

    }

    private void Start()
    {
        for (int i = 0; i <= (int)KeyAction.DOWN; i++)
        {
            KeySet.keys.Add((KeyAction)i, defaultKeys[i]);
        }

        for (int i = 0; i < txt.Length; i++)
        {
            try
            {
                txt[i].text = KeySet.keys[(KeyAction)i].ToString();
            }
            catch
            {
                Debug.Log("KeySetting 텍스트 적용안됨");
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < txt.Length; i++)
        {
            try
            {
                txt[i].text = KeySet.keys[(KeyAction)i].ToString();
            }
            catch
            {
                Debug.Log("KeySetting 텍스트 적용안됨");
            }
        }

        //KeyCheckTest();
    }

    private void KeyCheckTest()
    {
        if (Input.GetKey(KeySet.keys[KeyAction.ACTION]))
            Debug.Log("ACTION");
        if (Input.GetKey(KeySet.keys[KeyAction.DOWN]))
            Debug.Log("DOWN");
        if (Input.GetKey(KeySet.keys[KeyAction.JUMP]))
            Debug.Log("JUMP");
        if (Input.GetKey(KeySet.keys[KeyAction.LEFT]))
            Debug.Log("LEFT");
        if (Input.GetKey(KeySet.keys[KeyAction.RESTART]))
            Debug.Log("RESTART");
        if (Input.GetKey(KeySet.keys[KeyAction.RIGHT]))
            Debug.Log("RIGHT");
        if (Input.GetKey(KeySet.keys[KeyAction.SHOT]))
            Debug.Log("SHOT");
        if (Input.GetKey(KeySet.keys[KeyAction.SUICIDE]))
            Debug.Log("SUICIDE");
        if (Input.GetKey(KeySet.keys[KeyAction.UP]))
            Debug.Log("UP");
        if (Input.GetKey(KeySet.keys[KeyAction.WC]))
            Debug.Log("WC");
        if (Input.GetKey(KeySet.keys[KeyAction.WP1]))
            Debug.Log("WP1");
        if (Input.GetKey(KeySet.keys[KeyAction.WP2]))
            Debug.Log("WP2");
        if (Input.GetKey(KeySet.keys[KeyAction.WP3]))
            Debug.Log("WP3");
        if (Input.GetKey(KeySet.keys[KeyAction.WP4]))
            Debug.Log("WP4");
        if (Input.GetKey(KeySet.keys[KeyAction.WP5]))
            Debug.Log("WP5");
    }

    private void OnGUI()
    {
        Event keyEvent = Event.current;

        if (keyEvent.isKey)
        {
            KeySet.keys[(KeyAction)key] = keyEvent.keyCode;
            key = -1;
        }
    }

    public void ChangeKey(int num)
    {
        key = num;
    }
}
