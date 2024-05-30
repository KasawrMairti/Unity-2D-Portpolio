using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSpiritBoss : MonoBehaviour
{
    [SerializeField] private LayerMask Ground;
    private Vector2 start;
    private Vector2 end;
    private Vector2 velocity;


    private void Update()
    {
        Checkground();
    }

    private void Checkground()
    {
        float raylength = 0.1f;

        RaycastHit2D hit = Physics2D.Raycast(start, Vector2.down * end, raylength, Ground);

        Debug.DrawRay(start, Vector2.down * end * raylength, Color.red);

        if (hit)
        {
            velocity.y = (hit.distance - 0.015f) * 0.1f;
        }

        velocity.x = 0;
        transform.Translate(velocity);
    }
}
