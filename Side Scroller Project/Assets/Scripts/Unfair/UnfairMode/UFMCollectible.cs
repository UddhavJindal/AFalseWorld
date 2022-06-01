using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFMCollectible : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.position = spawnPoint.position;
            //this.gameObject.SetActive(false);
        }
    }
}
