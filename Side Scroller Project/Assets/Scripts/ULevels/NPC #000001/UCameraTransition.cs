using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UCameraTransition : MonoBehaviour
{
    [SerializeField] GameObject cameraZoomedOut;
    [SerializeField] GameObject cameraZoomedIn;

    private void Start()
    {
        cameraZoomedIn.SetActive(false);
        cameraZoomedOut.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            cameraZoomedIn.SetActive(true);
            cameraZoomedOut.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            cameraZoomedIn.SetActive(false);
            cameraZoomedOut.SetActive(true);
        }
    }
}
