using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AnimatorPro;

[RequireComponent(typeof(AnimatorPro))]
public class FireBossAnimation : MonoBehaviour
{
    public static readonly int ID_SitDown = Animator.StringToHash("SitDown");

    public AnimatorPro animatorPro;
    public Animator anim;
    private Transform transform;

    private void Awake()
    {
        if (anim == null) return;

        animatorPro = GetComponent<AnimatorPro>();
        animatorPro.Init(anim);
    }

    private void Start()
    {
        animatorPro.SetTrigger("SitDown");
    }

    private void Update()
    {
        
    }


}
