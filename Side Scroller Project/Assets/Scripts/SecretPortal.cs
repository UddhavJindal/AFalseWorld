using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SecretPortal : MonoBehaviour
{
    bool canEnter;

    private void Update()
    {
        if(canEnter && Input.GetKeyDown(KeyCode.E))
        {
            canEnter = false;
            SceneManager.LoadScene("Secret");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canEnter = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            canEnter = false;
        }
    }
}
