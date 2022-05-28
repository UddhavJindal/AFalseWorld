using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TTSpikes : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            collision.transform.position = spawnPoint.position;
        }
    }
}
