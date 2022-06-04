using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UInviWallTwo : MonoBehaviour
{
    [Header("Componenets")]
    [SerializeField] SpriteRenderer spriteRenderer;

    [Header("Color")]
    [SerializeField] Color32 inviColor;
    [SerializeField] Color32 activeColor;

    bool disableCollision;

    private void Start()
    {
        spriteRenderer.color = inviColor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player" && !disableCollision)
        {
            spriteRenderer.color = activeColor;
            disableCollision = true;
        }
    }

}
