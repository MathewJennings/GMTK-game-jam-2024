using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movability : MonoBehaviour
{
    public Rigidbody2D body;
    public bool isMovable;

    public void SetMovability(bool isMovable)
    {
        this.isMovable = isMovable;

        if (isMovable)
        {
            body.constraints = RigidbodyConstraints2D.None;
        } else
        {
            body.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }

    private void Start()
    {
        SetMovability(isMovable);
    }

    [ContextMenu("Toggle Movability")]
    public void ToggleMovability ()
    {
        SetMovability(!isMovable);
    }
}
