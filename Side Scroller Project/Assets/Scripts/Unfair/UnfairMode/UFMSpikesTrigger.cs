using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFMSpikesTrigger : MonoBehaviour
{
    Rigidbody2D body;
    public float speed;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        body.gravityScale = 0;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            body.gravityScale = speed;
        }
    }
}
