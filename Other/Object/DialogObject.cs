using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogObject : DialogEvent, IDialogText
{
    #region Inspector
    [Header("Event Text")]
    [SerializeField] private string[] _EventText;
    #endregion

    public string[] _Text { get; set; }

    private void Awake()
    {
        _Text = new string[_EventText.Length];

        for (int i = 0; i < _EventText.Length; i++)
        {
            _Text[i] = _EventText[i];
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            StartCoroutine(DialogEventer(_Text));
        }
    }
}
