using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AnimatorPro;

[RequireComponent(typeof(AnimatorPro))]
public class lever : MonoBehaviour
{
    #region inspector
    [Header("Object")]
    [SerializeField] private Transform _Baricade;
    [SerializeField] private Transform _Top;
    [SerializeField] private Transform _Bottom;
    [SerializeField] private bool _Switch = false;
    #endregion

    #region Component
    private AnimatorPro animatorPro;
    private Animator anim;

    private bool _Switch_Snd;
    #endregion

    #region Variable
    private Coroutine _Coroutine;
    private float _CurrentTime;
    private bool _BoolAction;
    #endregion

    private void Awake()
    {
        animatorPro = GetComponent<AnimatorPro>();
        anim = GetComponent<Animator>();
        animatorPro.Init(anim);

        _Switch_Snd = _Switch;
        _Coroutine = null;
        _CurrentTime = 0f;
        _BoolAction = false;

        if (_Switch) animatorPro.SetTrigger("_Right");
    }

    private void Update()
    {
        if (!Input.GetKey(KeySet.keys[KeyAction.ACTION]) && _BoolAction) _BoolAction = false;

        if (Time.time > _CurrentTime + 0.5f)
        {
            if (_Switch_Snd != _Switch)
            {
                _CurrentTime = Time.time;
                animatorPro.SetTrigger("_Move");

                if (_Coroutine != null) StopCoroutine(_Coroutine);

                if (_Switch_Snd)
                {
                    _Switch_Snd = false;

                    _Coroutine = StartCoroutine(_MoveToTop());
                }
                else
                {
                    _Switch_Snd = true;

                    _Coroutine = StartCoroutine(_MoveToBottom());
                }
            }
        }
    }

    private IEnumerator _MoveToTop()
    {
        while (_Baricade.transform.position != _Top.transform.position)
        {
            _Baricade.transform.position = Vector3.MoveTowards(_Baricade.transform.position,
                _Top.transform.position, 0.05f);

            yield return new WaitForSeconds(0.01f);
        }
    }

    private IEnumerator _MoveToBottom()
    {
        while (_Baricade.transform.position != _Bottom.transform.position)
        {
            _Baricade.transform.position = Vector3.MoveTowards(_Baricade.transform.position,
                _Bottom.transform.position, 0.05f);

            yield return new WaitForSeconds(0.01f);
        }
    }

    private void OnTriggerStay2D(Collider2D coll)
    {
        if (coll.CompareTag("Player"))
        {
            if (Input.GetKey(KeySet.keys[KeyAction.ACTION]) && !_BoolAction)
            {
                _BoolAction = true;

                if (_Switch) _Switch = false;
                else _Switch = true;
            }
        }
    }
}
