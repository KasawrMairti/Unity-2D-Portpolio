using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Information : MonoBehaviour
{
    #region inspector
    public GameObject _InformationObject;

    private TextMeshPro _Object;
    #endregion

    private void Awake()
    {
        _Object = _InformationObject.GetComponent<TextMeshPro>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PlayerManager.Instance.player.gameObject.activeInHierarchy)
        {
            if (collision.CompareTag("Player"))
            {
                StartCoroutine(movingEffect(true));
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (PlayerManager.Instance.player.gameObject.activeInHierarchy)
        {
            if (collision.CompareTag("Player"))
            {
                StartCoroutine(movingEffect(false));
            }
        }
    }

    IEnumerator movingEffect(bool Up)
    {
        if (Up) _Object.gameObject.SetActive(true);

        Color objectColor;


        for (int i = 0; i < 66; i++)
        {
            if (Up)
            {
                _Object.transform.Translate(0, 0.012f, 0);
                objectColor = _Object.color;
                objectColor.a = i * 0.01f;
                _Object.color = objectColor;
            }
            else
            {
                _Object.transform.Translate(0, -0.012f, 0);
                objectColor = _Object.color;
                objectColor.a = 0.6f - (i * 0.01f);
                _Object.color = objectColor;
            }

            yield return new WaitForSeconds(0.005f);
        }

        if (!Up) _Object.gameObject.SetActive(false);
    }
}
