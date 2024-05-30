using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MainTitle : MonoBehaviour
{
    private enum ModCount { START, LOAD, SETTING, EXIT };

    #region inspector
    [Header("Left Image")]
    public Image _MainImage;
    public Image _Light;
    public Image _MainTitle;

    [Header("Right Text")]
    public Text[] _RightText;

    [Header("SelectCorsor"), Tooltip("선택 대상을 나타내는 이미지")]
    public Image _SelectCorser;
    public GameObject _SettingMenu;
    #endregion

    #region Variable
    private ModCount _ModCount;
    private Transform _EndTransform;
    private PlayerInput playerInput;
    private float _SelectMoveSpeed;
    [HideInInspector] public bool KeyEnable;
    #endregion

    private void Awake()
    {
        GameSettingManager.Instance.mainTitle = this;

        KeyEnable = false;
        _ModCount = ModCount.START;
        _EndTransform = _RightText[0].transform;
        _SelectMoveSpeed = 1500f;
        _SelectCorser.transform.position = _RightText[0].transform.position;

    }

    private void Start()
    {
        playerInput = PlayerManager.Instance.playerInput;

        StartCoroutine(Initialization());
    }

    private void Update()
    {
        if (KeyEnable)
        {
            CheckMode();
            CheckEnableMod();
        }
    }

    private void ImageChange()
    {
        for (int i = 0; i < _RightText.Length; i++)
        {
            _RightText[i].color = new Color(1, 1, 1, 1);
        }
    }

    private void CheckMode()
    {
        if (Input.GetKeyDown(KeySet.keys[KeyAction.UP]))
        {
            --_ModCount;
            _SelectMoveSpeed = 1500f;
        }
        else if (Input.GetKeyDown(KeySet.keys[KeyAction.DOWN]))
        {
            ++_ModCount;
            _SelectMoveSpeed = 1500f;
        }

        if (_ModCount < 0)
        {
            _ModCount = ModCount.EXIT;
            _SelectMoveSpeed = 5000f;
        }
        else if ((int)_ModCount > 3)
        {
            _ModCount = ModCount.START;
            _SelectMoveSpeed = 5000f;
        }

        switch (_ModCount)
        {
            case ModCount.START:
                _EndTransform = _RightText[0].transform;
                _SelectCorser.gameObject.transform.position = Vector3.MoveTowards(_SelectCorser.gameObject.transform.position, _EndTransform.position, _SelectMoveSpeed * Time.deltaTime);

                ImageChange();
                _RightText[0].color = new Color(0.5f, 1, 0, 1);
                break;
            case ModCount.LOAD:
                _EndTransform = _RightText[1].transform;
                _SelectCorser.gameObject.transform.position = Vector3.MoveTowards(_SelectCorser.gameObject.transform.position, _EndTransform.position, _SelectMoveSpeed * Time.deltaTime);

                ImageChange();
                _RightText[1].color = new Color(0.5f, 1, 0, 1);
                break;
            case ModCount.SETTING:
                _EndTransform = _RightText[2].transform;
                _SelectCorser.gameObject.transform.position = Vector3.MoveTowards(_SelectCorser.gameObject.transform.position, _EndTransform.position, _SelectMoveSpeed * Time.deltaTime);

                ImageChange();
                _RightText[2].color = new Color(0.5f, 1, 0, 1);
                break;
            case ModCount.EXIT:
                _EndTransform = _RightText[3].transform;
                _SelectCorser.gameObject.transform.position = Vector3.MoveTowards(_SelectCorser.gameObject.transform.position, _EndTransform.position, _SelectMoveSpeed * Time.deltaTime);

                ImageChange();
                _RightText[3].color = new Color(0.5f, 1, 0, 1);
                break;
        }
    }

    private void CheckEnableMod()
    {
        if (Input.GetKeyDown(KeySet.keys[KeyAction.SHOT]))
        {
            switch (_ModCount)
            {
                case ModCount.START:
                    KeyEnable = false;
                    SceneManager.LoadScene("Field_Fire");
                    break;
                case ModCount.LOAD:
                    break;
                case ModCount.SETTING:
                    KeyEnable = false;
                    _SettingMenu.SetActive(true);
                    break;
                case ModCount.EXIT:
                    KeyEnable = false;
                    Application.Quit();
                    break;
            }
        }
    }

    private IEnumerator Initialization()
    {
        yield return new WaitForSeconds(1f);

        for (int i = 0; i < 101; i++)
        {
            yield return new WaitForSeconds(0.02f);

            _MainImage.color = new Color(1, 1, 1, i * 0.01f);
            _Light.color = new Color(1, 0.5f, 0, i * 0.0025f);
        }

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 101; i++)
        {
            yield return new WaitForSeconds(0.02f);

            _MainTitle.color = new Color(1, 1, 1, i * 0.01f);
        }

        yield return new WaitForSeconds(0.5f);

        for (int i = 0; i < 101; i++)
        {
            yield return new WaitForSeconds(0.02f);

            for (int j = 0; j < _RightText.Length; j++)
            {
                _RightText[j].color = new Color(1, 1, 1, i * 0.01f);
            }
        }

        _SelectCorser.color = new Color(1, 1, 1, 1);
        KeyEnable = true;
    }
}