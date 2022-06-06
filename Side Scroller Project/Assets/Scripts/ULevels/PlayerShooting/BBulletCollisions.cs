using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BBulletCollisions : MonoBehaviour
{
    [SerializeField] ParticleSystem destroyEffect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Boss" || collision.gameObject.tag == "Wall")
        {
            var Instance = Instantiate(destroyEffect, transform.position, Quaternion.identity);
            Destroy(Instance.gameObject, 1);
            Destroy(gameObject);
        }
    }
}
