using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCollisions : MonoBehaviour
{
    [SerializeField] Transform SpawnPoint;
    [SerializeField] ParticleSystem deathParticleSYstem;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacles")
        {
            var Instance = Instantiate(deathParticleSYstem, transform.position, Quaternion.identity);
            Destroy(Instance.gameObject, 0.5f);
            transform.position = SpawnPoint.position;
        }
    }
}
