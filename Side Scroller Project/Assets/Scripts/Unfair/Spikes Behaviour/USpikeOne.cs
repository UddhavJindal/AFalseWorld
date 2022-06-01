using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USpikeOne : MonoBehaviour
{
    [SerializeField] GameObject Spike;

    [SerializeField] bool showSpike;

    private void Start()
    {
        if(!showSpike)
        {
            Spike.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            if(!showSpike)
            {
                Spike.SetActive(true);
            }
        }
    }
}
