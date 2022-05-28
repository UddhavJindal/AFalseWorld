using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTDestroyer : MonoBehaviour
{
    bool isDead = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && !isDead)
        {
            isDead = true;
            GameObject.FindGameObjectWithTag("GameManager").GetComponent<TimeTrialMechanism>().isDead = true;
        }
    }
}
