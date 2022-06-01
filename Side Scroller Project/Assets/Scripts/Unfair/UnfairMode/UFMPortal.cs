using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFMPortal : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;

    void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(Input.GetKey(KeyCode.E))
            {
                collision.transform.position = spawnPoint.position;
                Debug.Log("Key Pressed");
            }
        }
    }
}
