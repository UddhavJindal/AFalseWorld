using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ULevelFour : MonoBehaviour
{
    [SerializeField] Transform Portal;

    [SerializeField] USpikeTwo[] uSpikeTwoScripts;

    [SerializeField] Transform teleportDoorTo;

    //flags
    bool disableCollision;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" & !disableCollision)
        {
            foreach(var uSpikeTwoScript in uSpikeTwoScripts)
            {
                uSpikeTwoScript.canShoot = true;
            }
            Portal.position = teleportDoorTo.position;
            disableCollision = true;
        }
    }
}
