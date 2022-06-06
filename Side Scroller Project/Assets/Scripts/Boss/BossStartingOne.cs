using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStartingOne : MonoBehaviour
{
    [SerializeField] GMechanism gMechanismScript;
    [SerializeField] BossStartingOne bossStartingScript;

    bool isInYet;

    private void Update()
    {
        if(isInYet && Input.GetKeyDown(KeyCode.E))
        {
            isInYet = false;
            gMechanismScript.canStart = true;
            bossStartingScript.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        isInYet = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        isInYet = false;
    }
}
