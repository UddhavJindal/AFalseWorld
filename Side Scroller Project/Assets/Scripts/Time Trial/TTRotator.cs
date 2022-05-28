using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTRotator : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] SpriteRenderer spriteRenderer;
    
    [Header("Components")]
    [SerializeField] Sprite SafePlatformSprite;
    [SerializeField] Sprite UnSafePlatformSprite;

    [Header("Settings")]
    [SerializeField] float changeTimer;

    float timer;
    float waitTime;

    bool isSafe;
    bool canDamage;

    private void Start()
    {
        waitTime = changeTimer;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if(timer > waitTime)
        {
            timer = 0;
            if(isSafe)
            {
                spriteRenderer.sprite = SafePlatformSprite;
                isSafe = false;
                canDamage = false;
            }
            else
            {
                spriteRenderer.sprite = UnSafePlatformSprite;
                isSafe = true;
                canDamage = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(canDamage)
        {

        }
    }
}
