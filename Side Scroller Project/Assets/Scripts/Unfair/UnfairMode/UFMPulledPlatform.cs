using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFMPulledPlatform : MonoBehaviour
{
    public void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            this.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            this.gameObject.SetActive(false);
        }
    }
}
