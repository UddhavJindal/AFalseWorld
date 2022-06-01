using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UFMInvisbleBlocks : MonoBehaviour
{
    [SerializeField] Color visible;
    [SerializeField] Color invisible;

    void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().color = invisible;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            gameObject.GetComponent<SpriteRenderer>().color = visible;
        }
    }
}
