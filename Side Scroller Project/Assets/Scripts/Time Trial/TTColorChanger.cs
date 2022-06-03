using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTColorChanger : MonoBehaviour
{
    [Header("Refrences")]
    [SerializeField] SpriteRenderer spriteRenderer;

    [Header("Components")]
    [SerializeField] Color32 safeColor;
    [SerializeField] Color32 triggerColer;
    [SerializeField] Collider2D collider;

    [Header("Settings")]
    [SerializeField] float changeTimer;

    float timer;
    float waitTime;

    [SerializeField] bool isSafe;
    [SerializeField] bool isDelayed;

    private void Start()
    {
        /*if(isDelayed)
        {
            isSafe = false;
        }
        else
        {
            isSafe = false;
        }*/

        if (isSafe)
        {
            spriteRenderer.color = safeColor;
            collider.enabled = true;
            isSafe = false;
        }
        else
        {
            spriteRenderer.color = triggerColer;
            collider.enabled = false;
            isSafe = true;
        }
        waitTime = changeTimer;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > waitTime)
        {
            timer = 0;
            if (isSafe)
            {
                spriteRenderer.color = safeColor;
                collider.enabled = true;
                isSafe = false;
            }
            else
            {
                spriteRenderer.color = triggerColer;
                collider.enabled = false;
                isSafe = true;
            }
        }
    }
}
