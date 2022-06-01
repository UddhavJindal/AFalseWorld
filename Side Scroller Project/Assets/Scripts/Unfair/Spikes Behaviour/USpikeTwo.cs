using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class USpikeTwo : MonoBehaviour
{
    [SerializeField] GameObject Spike;

    [SerializeField] bool showSpike;

    [SerializeField] Rigidbody2D rb;

    public bool canShoot;

    [SerializeField] float force;

    [SerializeField] float disableSpikeIn;

    private void Start()
    {
        if (!showSpike)
        {
            Spike.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (!showSpike)
            {
                Spike.SetActive(true);
                showSpike = true;
            }

            if(canShoot)
            {
                canShoot = false;
                rb.AddForce(Spike.transform.up * force, ForceMode2D.Impulse);
                StartCoroutine(DisablingSpike());
            }
        }
    }

    IEnumerator DisablingSpike()
    {
        yield return new WaitForSeconds(disableSpikeIn);
        Spike.SetActive(false);
    }
}
