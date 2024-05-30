using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableMonster : MonoBehaviour
{
    #region Inspector
    [Header("MonsterList")]
    [SerializeField] private GameObject[] _Object;
    [SerializeField] private Transform[] _ObjectVector;
    #endregion


    private void Start()
    {
        for (int i = 0; i < _Object.Length; i++)
        {
            _ObjectVector[i] = _Object[i].transform;
            _Object[i].SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            for (int i = 0; i < _Object.Length; i++)
            {
                if (_Object[i] != null)
                {
                    _Object[i].SetActive(true);
                    _Object[i].transform.position = _ObjectVector[i].position;
                }
            }
        }
    }

    private void OnTriggerExit2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            for (int i = 0; i < _Object.Length; i++)
            {
                if (_Object[i] != null && _Object[i].activeInHierarchy)
                {
                    _Object[i].SetActive(false);
                }
            }
        }
    }
}
