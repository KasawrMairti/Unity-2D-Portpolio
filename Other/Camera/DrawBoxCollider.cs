using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  > BoxCollider 를 Scene 에 그리는 컴포넌트입니다.
[ExecuteInEditMode]
[RequireComponent(typeof(BoxCollider2D))]
public sealed class DrawBoxCollider : MonoBehaviour
{
    // 그릴 BoxCollider2D Component 를 참조할 변수입니다.
    private BoxCollider2D _BoxCollider = null;

    [SerializeField] private Color _DrawColor = new Color(80, 240, 240, 255);

    private void Awake()
    {
        _BoxCollider = GetComponent<BoxCollider2D>();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = _DrawColor;

        Gizmos.DrawWireCube(
            _BoxCollider.bounds.center,
            _BoxCollider.bounds.size);  
    }
}
