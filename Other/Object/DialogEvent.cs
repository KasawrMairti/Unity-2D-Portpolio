using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR.WSA.Input;

public abstract class DialogEvent : MonoBehaviour
{
    #region Inspector
    [Header("Dialog")]
    public GameObject _Dialog = null;
    public RectTransform _DialogSize = null;
    public Text _DialogText = null;
    #endregion

    protected Player player;
    protected PlayerMove playerMove;

    protected string[] _TextEvent;

    protected virtual void Start()
    {
        player = PlayerManager.Instance.player;
        playerMove = PlayerManager.Instance.playerMove;
    }

    //private IEnumerator DialogEvent()
    //{
    //
    //}

    protected virtual IEnumerator DialogEventer(string[] Dialog)
    {
        playerMove.canMove = false;
        _DialogText.text = "";
        _DialogSize.sizeDelta = Vector2.zero;

        // Split 으로 이용하기
        // 0. 텍스트
        // 1. 대화창 이미지 X 길이
        // 2. 대화창 이미지 Y 길이
        // 3. 텍스트 종류 (일반텍스트, 크기가 큰 텍스트, 강조형 텍스트)
        // 4. 대화창 띄울 오브젝트

        for (int i = 0; i < Dialog.Length; i++)
        {
            _Dialog.SetActive(true);
            // 메세지 창 띄우기
            string[] msg = Dialog[i].Split(':');

            for (int j = 0; j < 10; j++)
            {
                // Split 이용하기
                _DialogSize.sizeDelta = new Vector2(float.Parse(msg[1]) / (10 - j), float.Parse(msg[2]) / (10 - j));
                yield return new WaitForSeconds(0.01f);
            }

            yield return new WaitForSeconds(0.2f);

            for (int j = 0; j < msg[0].Length; j++)
            {
                yield return new WaitForSeconds(0.2f);
                _DialogText.text += msg[0][j];
            }
            yield return new WaitUntil(() => Input.GetKeyDown(KeySet.keys[KeyAction.SHOT]));
            _DialogText.text = "";

            // 메세지 창 없애기
            for (int j = 1; j < 10; j++)
            {
                _DialogSize.sizeDelta = new Vector2(float.Parse(msg[1]) / j, float.Parse(msg[2]) / j);
                yield return new WaitForSeconds(0.01f);
            }

            _Dialog.SetActive(false);
            yield return new WaitForSeconds(0.2f);
        }

        _DialogSize.sizeDelta = Vector2.zero;
        playerMove.canMove = true;

        Destroy(gameObject);
    }
}
