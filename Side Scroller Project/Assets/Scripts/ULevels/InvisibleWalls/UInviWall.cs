using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UInviWall : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] GameObject Block;

    [Header("Refrence")]
    [SerializeField] SpriteRenderer spriteRendrer;

    [Header("Color")]
    [SerializeField] Color32 invisibleColor;
    [SerializeField] Color32 activeColor;

    private void Start()
    {
        spriteRendrer.color = invisibleColor;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            spriteRendrer.color = activeColor;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            spriteRendrer.color = invisibleColor;
        }
    }
}
