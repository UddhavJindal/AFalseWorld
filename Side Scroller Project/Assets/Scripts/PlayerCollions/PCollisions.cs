using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PCollisions : MonoBehaviour
{
    [SerializeField] Transform SpawnPoint;
    [SerializeField] ParticleSystem deathParticleSYstem;
    [SerializeField] GameObject deathCounter;

    [SerializeField] GameObject[] Cameras;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Obstacles")
        {
            PlayerPrefs.SetInt("DeathCounter", PlayerPrefs.GetInt("DeathCounter") + 1);
            var Instance = Instantiate(deathParticleSYstem, transform.position, Quaternion.identity);
            Instantiate(deathCounter, transform.position, Quaternion.identity);
            Destroy(Instance.gameObject, 0.5f);
            transform.position = SpawnPoint.position;
            if(Cameras != null)
            {
                foreach(var camera in Cameras)
                {
                    camera.GetComponent<NormalCameraShake>().start = true;
                }
            }
        }
    }
}
