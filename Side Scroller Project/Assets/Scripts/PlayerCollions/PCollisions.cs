using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCollisions : MonoBehaviour
{
    [SerializeField] Transform SpawnPoint;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacles")
        {
            transform.position = SpawnPoint.position;
        }
    }
}
