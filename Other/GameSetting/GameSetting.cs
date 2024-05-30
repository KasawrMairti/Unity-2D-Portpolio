using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSetting : MonoBehaviour
{
    [SerializeField] private GameObject gameSetting = null;

    private MainTitle mainTitle;

    private void Awake()
    {
        GameSettingManager.Instance.gameSetting = this;
    }

    private void Start()
    {
        mainTitle = GameSettingManager.Instance.mainTitle;
    }

    public void OnExitClick()
    {
        gameSetting.gameObject.SetActive(false);
        if (mainTitle != null)  mainTitle.KeyEnable = true;
    }
}
